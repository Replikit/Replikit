namespace Replikit.Core.Common;

public delegate IQueryable<T> QueryBuilder<T>(IQueryable<T> query);
