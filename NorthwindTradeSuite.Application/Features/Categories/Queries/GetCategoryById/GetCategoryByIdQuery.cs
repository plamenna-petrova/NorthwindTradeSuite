﻿using NorthwindTradeSuite.Application.Abstraction;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetCategoryById
{
    public sealed record GetCategoryByIdQuery(string Id) : CachedQuery<CategoryResponseDTO>;
}
