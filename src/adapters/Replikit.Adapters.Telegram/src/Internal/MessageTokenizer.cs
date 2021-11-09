using Replikit.Abstractions.Messages.Models.Tokens;
using Replikit.Adapters.Common.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Replikit.Adapters.Telegram.Internal;

internal class MessageTokenizer
{
    private static readonly MessageEntityType[] Special =
    {
        MessageEntityType.Mention,
        MessageEntityType.TextLink,
        MessageEntityType.TextMention
    };

    private MessageEntity _currentEntity = null!;
    private int _lastIndex;
    private TextTokenModifiers _modifiers = TextTokenModifiers.None;

    private readonly string _text;
    private readonly List<MessageEntity> _entities;
    private readonly List<TextToken> _tokens = new();

    public MessageTokenizer(string text, IEnumerable<MessageEntity> entities)
    {
        _text = text;
        _entities = entities.ToList();
        SortEntities(_entities);
    }

    public IReadOnlyList<TextToken> Tokenize()
    {
        foreach (var entity in _entities)
        {
            _currentEntity = entity;
            if (entity.Offset > _lastIndex)
            {
                ResetEntity();
                _lastIndex = _currentEntity.Offset;
            }

            HandleEntity();
        }

        if (_text.Length > _lastIndex)
        {
            var plainText = _text.Substring(_lastIndex, _text.Length);
            PushText(plainText);
        }

        return _tokens;
    }

    private void PushText(string? text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            var textToken = new TextToken(text, _modifiers);
            _tokens.Add(textToken);
        }

        _modifiers = TextTokenModifiers.None;
    }

    private void HandleEntity()
    {
        switch (_currentEntity.Type)
        {
            case MessageEntityType.TextLink:
            {
                var linkToken = new LinkTextToken(GetTokenText(), _currentEntity.Url, _modifiers);
                _tokens.Add(linkToken);

                ResetEntity();
                break;
            }
            case MessageEntityType.Mention:
            {
                var mentionToken = new MentionTextToken(GetTokenText(), _text.Substring(1), Modifiers: _modifiers);
                _tokens.Add(mentionToken);

                ResetEntity();
                break;
            }
            case MessageEntityType.TextMention:
            {
                var textMentionToken = new MentionTextToken(GetTokenText(),
                    AccountId: _currentEntity.User.Id,
                    Modifiers: _modifiers);

                _tokens.Add(textMentionToken);

                ResetEntity();
                break;
            }
            case MessageEntityType.Bold:
                _modifiers |= TextTokenModifiers.Bold;
                break;
            case MessageEntityType.Italic:
                _modifiers |= TextTokenModifiers.Italic;
                break;
            case MessageEntityType.Underline:
                _modifiers |= TextTokenModifiers.Underline;
                break;
            case MessageEntityType.Strikethrough:
                _modifiers |= TextTokenModifiers.Strikethrough;
                break;
            case MessageEntityType.Code:
                _modifiers |= TextTokenModifiers.InlineCode;
                break;
            case MessageEntityType.Pre:
                _modifiers |= TextTokenModifiers.Code;
                break;
        }
    }

    private string GetTokenText()
    {
        return _text.Substring(_currentEntity.Offset, _currentEntity.Length);
    }

    private void ResetEntity()
    {
        var text = _text.Slice(_lastIndex, _currentEntity.Offset);
        PushText(text);
        ResetLastIndex();
    }

    private void ResetLastIndex()
    {
        _lastIndex = _currentEntity.Offset + _currentEntity.Length;
    }

    private static void SortEntities(List<MessageEntity> entities)
    {
        entities.Sort((a, _) => Special.Contains(a.Type) ? 1 : -1);
        entities.Sort((a, b) => a.Offset - b.Offset);
    }
}
