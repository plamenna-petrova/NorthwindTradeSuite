using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthwindTradeSuite.Application.Features.Categories.Queries.GetAllCategories;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using System.Net;
using static NorthwindTradeSuite.Common.GlobalConstants.HttpConstants;

namespace NorthwindTradeSuite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private const string CategoriesName = "categories";

        private const string SingleCategoryName = "category";

        private const string CategoryDetailsRouteName = "CategoryDetails";

        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<GetCategoriesResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetCategoriesResponseDTO>>> GetAllCategories()
        {
            var allQueriedCategories = await _mediator.Send(new GetAllCategoriesQuery());

            if (allQueriedCategories != null)
            {
                return Ok(allQueriedCategories);
            }

            return NotFound(string.Format(EntitiesNotFoundResult, CategoriesName));
        }
    }
}
