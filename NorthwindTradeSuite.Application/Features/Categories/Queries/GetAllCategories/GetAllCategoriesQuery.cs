﻿using NorthwindTradeSuite.Application.Abstraction;
using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetAllCategories
{
    public sealed record GetAllCategoriesQuery() : CachedQuery<List<CategoryResponseDTO>>();
}
