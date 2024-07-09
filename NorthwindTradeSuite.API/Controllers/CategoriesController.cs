using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthwindTradeSuite.Application.Features.Categories.Queries.GetAllCategories;
using NorthwindTradeSuite.Application.Features.Categories.Queries.GetCategoryById;
using NorthwindTradeSuite.Application.Features.Categories.Queries.GetCategoryDetails;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using System.Net;
using static NorthwindTradeSuite.Common.GlobalConstants.HttpConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.CategoryConstants;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.DTOs.Requests.Categories;
using NorthwindTradeSuite.Application.Features.Categories.Commands.CreateCategory;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationRoleConstants;
using NorthwindTradeSuite.API.Extensions;

namespace NorthwindTradeSuite.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CategoryResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategories()
        {
            var getAllCategoriesQuery = new GetAllCategoriesQuery();
            var allQueriedCategories = await _mediator.Send(getAllCategoriesQuery);

            return allQueriedCategories != null 
                ? Ok(allQueriedCategories) : NotFound(string.Format(EntitiesNotFoundResult, CategoriesName));
        }

        [HttpGet("{id}", Name = CategoryByIdRouteName)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoryResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryById(string id)
        {
            var getCategoryByIdQuery = new GetCategoryByIdQuery(id);
            var queriedCategoryById = await _mediator.Send(getCategoryByIdQuery);

            return queriedCategoryById != null 
                ? Ok(queriedCategoryById) : NotFound(string.Format(EntityByIdNotFoundResult, SingleCategoryName)); 
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("details/{id}", Name = CategoryDetailsRouteName)]
        [ProducesResponseType(typeof(CategoryDetailsResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDetailsResponseDTO>> GetCategoryDetails(string id)
        {
            var getCategoryDetailsQuery = new GetCategoryDetailsQuery(id);
            var queriedCategoryDetails = await _mediator.Send(getCategoryDetailsQuery);

            return queriedCategoryDetails != null
                ? Ok(queriedCategoryDetails) : NotFound(string.Format(EntityByIdNotFoundResult, SingleCategoryName));
        }

        [HttpPost("create")]
        [Authorize(Policy = ADMINISTRATOR_POLICY)]
        public async Task<ActionResult<Result<CategoryResponseDTO>>> CreateCategory([FromBody] CreateCategoryRequestDTO createCategoryRequestDTO) 
        { 
            if (createCategoryRequestDTO == null)
            {
                return BadRequest(string.Format(BadRequestMessage, SingleCategoryName, "creation"));
            }

            var currentUserId = User.GetCurrentUserId();
            var createCategoryCommand = new CreateCategoryCommand(createCategoryRequestDTO, currentUserId);
            var createdCategoryResult = await _mediator.Send(createCategoryCommand);

            return CreatedAtRoute(CategoryByIdRouteName, new { createdCategoryResult.Value!.Id }, createdCategoryResult.Value!);
        }
    }
}
