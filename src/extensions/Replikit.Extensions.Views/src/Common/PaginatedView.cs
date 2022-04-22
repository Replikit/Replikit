using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Models.Tokens;
using Replikit.Extensions.State;
using Replikit.Extensions.Views.Common.Models;
using Replikit.Extensions.Views.Messages;

namespace Replikit.Extensions.Views.Common;

public abstract class PaginatedView<TItem> : View
{
    private readonly PaginationState _paginationState;

    protected PaginatedView(IState<PaginationState> paginationState)
    {
        _paginationState = paginationState.Value;
    }

    protected virtual string PreviousArrow => "←";
    protected virtual string NextArrow => "→";

    protected abstract int PageSize { get; }

    protected virtual string RenderTitlePrefix() => "";

    protected virtual void RenderTitle(ViewMessageBuilder messageBuilder, int currentPage, int? totalPages)
    {
        var text = $"{RenderTitlePrefix()} {RenderPaginationInfo(currentPage, totalPages)}\n";
        messageBuilder.AddText(text, TextTokenModifiers.Bold);
    }

    protected virtual string RenderPaginationInfo(int currentPage, int? totalPages) =>
        $"[{currentPage}/{(totalPages.HasValue ? totalPages.Value : "-")}]";

    protected virtual long? GetTotalCount() => CreateQuery().Count();

    protected virtual Task<long?> GetTotalCountAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(GetTotalCount());

    protected virtual IReadOnlyList<TItem> GetItems(int skip, int take) =>
        ApplyPagination(CreateQuery(), skip, take).ToArray();

    protected virtual Task<IReadOnlyList<TItem>> GetItemsAsync(int skip, int take,
        CancellationToken cancellationToken) =>
        Task.FromResult(GetItems(skip, take));

    protected virtual void RenderItems(ViewMessageBuilder messageBuilder, IEnumerable<TItem> items) =>
        throw new InvalidOperationException("You must implement either RenderItems or RenderItemsAsync");

    protected virtual Task RenderItemsAsync(ViewMessageBuilder messageBuilder, IEnumerable<TItem> items,
        CancellationToken cancellationToken = default)
    {
        RenderItems(messageBuilder, items);
        return Task.CompletedTask;
    }

    protected virtual int GetPageCount(long totalCount) => (int) totalCount / PageSize;

    protected virtual IQueryable<TItem> CreateQuery() =>
        throw new InvalidOperationException(
            "You must implement either CreateQuery or GetItems & GetTotalCount or their async overloads");

    protected virtual (int Skip, int Take) GetPaginationData() => (_paginationState.CurrentPage * PageSize, PageSize);

    protected virtual IQueryable<TItem> ApplyPagination(IQueryable<TItem> queryable, int skip, int take)
    {
        return queryable.Skip(skip).Take(take);
    }

    public override async Task<ViewResult> RenderAsync(CancellationToken cancellationToken)
    {
        var totalCount = await GetTotalCountAsync(cancellationToken);
        int? pageCount = totalCount.HasValue ? GetPageCount(totalCount.Value) : null;

        var (skip, take) = GetPaginationData();

        var messageBuilder = new ViewMessageBuilder();

        RenderTitle(messageBuilder, _paginationState.CurrentPage + 1, pageCount + 1);
        messageBuilder.AddTextLine();

        var items = await GetItemsAsync(skip, take, cancellationToken);
        await RenderItemsAsync(messageBuilder, items, cancellationToken);

        RenderNavigationButtons(messageBuilder, pageCount);

        return messageBuilder;
    }

    protected virtual void RenderNavigationButtons(ViewMessageBuilder messageBuilder, int? pageCount)
    {
        if (pageCount is null or > 0)
        {
            messageBuilder.AddActionRow()
                .AddAction(PreviousArrow, () => MovePrevious())
                .AddAction(NextArrow, () => MoveNext());
        }
    }

    [Action(AutoUpdate = false)]
    public async Task MoveNext()
    {
        var totalCount = await GetTotalCountAsync();

        if (!totalCount.HasValue)
        {
            var (skip, take) = GetPaginationData();
            var items = await GetItemsAsync(skip, take, CancellationToken);

            if (items.Count < PageSize) return;
        }
        else
        {
            var pageCount = GetPageCount(totalCount.Value);
            if (_paginationState.CurrentPage >= pageCount) return;
        }

        _paginationState.CurrentPage++;
        Update();
    }

    [Action(AutoUpdate = false)]
    public void MovePrevious()
    {
        if (_paginationState.CurrentPage == 0) return;

        _paginationState.CurrentPage--;
        Update();
    }
}
