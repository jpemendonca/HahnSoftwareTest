using Hahn.Domain.Enums;

namespace Hahn.Domain.Dtos;

public record CryptoQueryParams(
    EnumSortBy SortBy = EnumSortBy.Name,
    EnumSortDirection Direction = EnumSortDirection.Asc
);