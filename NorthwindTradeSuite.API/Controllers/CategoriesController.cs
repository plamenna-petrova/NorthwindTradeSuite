﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthwindTradeSuite.Application.Features.Categories.Queries.GetAllCategories;
using NorthwindTradeSuite.Application.Features.Categories.Queries.GetCategoryById;
using NorthwindTradeSuite.Application.Features.Categories.Queries.GetCategoryDetails;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using System.Net;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.DTOs.Requests.Categories;
using NorthwindTradeSuite.Application.Features.Categories.Commands.CreateCategory;
using NorthwindTradeSuite.API.Extensions;
using NorthwindTradeSuite.Application.Features.Categories.Commands.UpdateCategory;
using NorthwindTradeSuite.Application.Features.Categories.Commands.DeleteCategory;
using static NorthwindTradeSuite.Common.GlobalConstants.HttpConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.CategoryConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationRoleConstants;
using NorthwindTradeSuite.Application.Features.Categories.Commands.HardDeleteCategory;
using NorthwindTradeSuite.Application.Features.Categories.Commands.RestoreCategory;
using NorthwindTradeSuite.Application.Features.Categories.Queries.GetAllDeletedCategories;
using NorthwindTradeSuite.Application.Features.Categories.Queries.GetDeletedCategoryById;

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

        [HttpGet("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CategoryResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategories()
        {
            var getAllCategoriesQuery = new GetAllCategoriesQuery();
            var allQueriedCategories = await _mediator.Send(getAllCategoriesQuery);

            if (allQueriedCategories == null)
            {
                return NotFound(string.Format(ENTITTIES_NOT_FOUND_RESULT, CATEGORIES_NAME));
            }

            return Ok(allQueriedCategories);
        }

        [HttpGet("all-deleted")]
        [Authorize(Policy = ADMINISTRATOR_POLICY)]
        [ProducesResponseType(typeof(List<CategoryResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllDeletedCategories()
        {
            var getAllDeletedCategoriesQuery = new GetAllDeletedCategoriesQuery();
            var allDeletedQueriesCategories = await _mediator.Send(getAllDeletedCategoriesQuery);

            if (allDeletedQueriesCategories == null)
            {
                return NotFound(string.Format(ENTITTIES_NOT_FOUND_RESULT, CATEGORIES_NAME));
            }

            return Ok(allDeletedQueriesCategories);
        }

        [HttpGet("{id}", Name = CATEGORY_BY_ID_ROUTE_NAME)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoryResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryById(string id)
        {
            var getCategoryByIdQuery = new GetCategoryByIdQuery(id);
            var queriedCategoryById = await _mediator.Send(getCategoryByIdQuery);

            return queriedCategoryById;
        }

        [HttpGet("deleted/{id}")]
        [Authorize(Policy = ADMINISTRATOR_POLICY)]
        [ProducesResponseType(typeof(CategoryDetailsResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDetailsResponseDTO>> GetDeletedCategoryById(string id)
        {
            var getDeletedCategoryByIdQuery = new GetDeletedCategoryByIdQuery(id);
            var queriedDeletedCategoryById = await _mediator.Send(getDeletedCategoryByIdQuery);

            return queriedDeletedCategoryById;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("details/{id}", Name = CATEGORY_DETAILS_ROUTE_NAME)]
        [ProducesResponseType(typeof(CategoryDetailsResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDetailsResponseDTO>> GetCategoryDetails(string id)
        {
            var getCategoryDetailsQuery = new GetCategoryDetailsQuery(id);
            var queriedCategoryDetails = await _mediator.Send(getCategoryDetailsQuery);

            return Ok(queriedCategoryDetails);
        }

        [HttpPost("create")]
        [Authorize(Policy = ADMINISTRATOR_POLICY)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RequestResult<CategoryResponseDTO>>> CreateCategory([FromBody] CreateCategoryRequestDTO createCategoryRequestDTO) 
        { 
            if (createCategoryRequestDTO == null)
            {
                return BadRequest(string.Format(BAD_REQUEST_MESSAGE, SINGLE_CATEGORY_NAME, CREATE_ACTION));
            }

            var currentUserId = User.GetCurrentUserId();
            var createCategoryCommand = new CreateCategoryCommand(createCategoryRequestDTO, currentUserId);
            var createdCategoryRequestResult = await _mediator.Send(createCategoryCommand);

            if (!createdCategoryRequestResult.IsSuccess)
            {
                return BadRequest(new { Message = createdCategoryRequestResult.Errors.FirstOrDefault() });
            }

            string createdCategorySuccessMessage = string.Format(CREATED_ENTITY_SUCCESS_MESSAGE, SINGLE_CATEGORY_NAME);
            var createdCategoryCommandResult = new CommandResult<CategoryResponseDTO>(createdCategorySuccessMessage, createdCategoryRequestResult.Value!);

            return CreatedAtRoute(CATEGORY_BY_ID_ROUTE_NAME, new { createdCategoryCommandResult.Details!.Id }, createdCategoryCommandResult);
        }

        [HttpPut("update/{id}")]
        [Authorize(Policy = ADMINISTRATOR_POLICY)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RequestResult<CategoryResponseDTO>>> UpdateCategory(string id, [FromBody] UpdateCategoryRequestDTO updateCategoryRequestDTO)
        {
            var currentUserId = User.GetCurrentUserId();
            var updateCategoryCommand = new UpdateCategoryCommand(id, updateCategoryRequestDTO, currentUserId);
            var updatedCategory = await _mediator.Send(updateCategoryCommand);
            string updatedCategorySuccessMessage = string.Format(UPDATED_ENTITY_SUCCESS_MESSAGE, SINGLE_CATEGORY_NAME);
            var updatedCategoryCommandResult = new CommandResult<CategoryResponseDTO>(updatedCategorySuccessMessage, updatedCategory);

            return Ok(updatedCategoryCommandResult);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = ADMINISTRATOR_POLICY)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDetailsResponseDTO>> DeleteCategory(string id)
        {
            var currentUserId = User.GetCurrentUserId();
            var deleteCategoryCommand = new DeleteCategoryCommand(id, currentUserId);
            var deletedCategoryDetails = await _mediator.Send(deleteCategoryCommand);
            string deletedCategorySuccessMessage = string.Format(DELETED_ENTITY_SUCCESS_MESSAGE, SINGLE_CATEGORY_NAME);
            var deletedCategoryCommandResult = new CommandResult<CategoryDetailsResponseDTO>(deletedCategorySuccessMessage, deletedCategoryDetails);

            return Ok(deletedCategoryCommandResult);
        }

        [HttpDelete("confirm-deletion/{id}")]
        [Authorize(Policy = ADMINISTRATOR_POLICY)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryConciseInformationResponseDTO>> HardDeleteCategory(string id)
        {
            var hardDeleteCategoryCommand = new HardDeleteCategoryCommand(id);
            var hardDeletedCategoryConciseInformation = await _mediator.Send(hardDeleteCategoryCommand);
            string confirmedCategoryDeletionSuccessMessage = string.Format(CONFIRMED_ENTITY_DELETION_SUCCESS_MESSAGE, SINGLE_CATEGORY_NAME); ;
            var confirmedCategoryDeletionCommandResult = new CommandResult<CategoryConciseInformationResponseDTO>(confirmedCategoryDeletionSuccessMessage, hardDeletedCategoryConciseInformation);

            return Ok(confirmedCategoryDeletionCommandResult);
        }

        [HttpPost("restore/{id}")]
        [Authorize(Policy = ADMINISTRATOR_POLICY)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDetailsResponseDTO>> RestoreCategory(string id)
        {
            var currentUserId = User.GetCurrentUserId();
            var restoreCategoryCommand = new RestoreCategoryCommand(id, currentUserId);
            var restoredCategoryDetails = await _mediator.Send(restoreCategoryCommand);
            string restoredCategorySuccessMessage = string.Format(RESTORED_ENTITY_SUCCESS_MESSAGE, SINGLE_CATEGORY_NAME);
            var restoredCategoryCommandResult = new CommandResult<CategoryDetailsResponseDTO>(restoredCategorySuccessMessage, restoredCategoryDetails);

            return Ok(restoredCategoryCommandResult);
        }
    }
}
