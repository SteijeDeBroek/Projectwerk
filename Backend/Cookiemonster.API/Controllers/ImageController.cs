using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookiemonster.API.Controllers
{
    [Route("Images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IRepository<Image> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ImageController> _logger;

        public ImageController(IRepository<Image> imageRepository, IMapper mapper, ILogger<ImageController> logger)
        {
            _logger?.LogTrace("-> ImageController::ImageController");
            _imageRepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
            _logger?.LogTrace("-> ImageController::ImageController");
        }

        [HttpGet("GetAsync", Name = "GetAllImagesAsync")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get all images",
            Description = "Returns a list of all images.",
            OperationId = "GetAllImages")]
        [SwaggerResponse(200, "Request successful")]
        [SwaggerResponse(404, "Images not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<IEnumerable<ImageDTOGet>>> GetAllImagesAsync()
        {
            _logger.LogInformation("GetAllImages - Fetching all images");
            try
            {
                var images = await _imageRepository.GetAllAsync();
                if (images == null) return NotFound();
                return Ok(_mapper.Map<List<ImageDTOGet>>(images));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet("{id}", Name = "GetImageByIdAsync")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get image by id",
            Description = "Returns a single image by its ID.",
            OperationId = "GetImageById")]
        [SwaggerResponse(200, "Request successful")]
        [SwaggerResponse(404, "Image not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<ImageDTOGet>> GetAsync(int id)
        {
            _logger.LogInformation($"Get (ImageById) - Attempting to fetch image with ID {id}");
            try
            {
                var image = await _imageRepository.GetAsync(id);
                if (image == null)
                {
                    _logger.LogWarning($"Get (ImageById) - Image with ID {id} not found");
                    return NotFound();
                }
                return Ok(_mapper.Map<ImageDTOGet>(image));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost(Name = "AddImageAsync")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Create an image",
            Description = "Creates a new image.",
            OperationId = "CreateImage")]
        [SwaggerResponse(201, "Image created")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult> CreateImageAsync(ImageDTOPost image)
        {
            try
            {
                if (image == null || !ModelState.IsValid)
                {
                    _logger.LogWarning("CreateImage - Invalid model state");
                    return BadRequest(ModelState);
                }

                var createdImage = await _imageRepository.CreateAsync(_mapper.Map<Image>(image));
                _logger.LogInformation($"CreateImage - Image created with ID: {createdImage.ImageId}");
                return CreatedAtAction("AddImageAsync", _mapper.Map<ImageDTOGet>(createdImage));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPatch("{id}", Name = "UpdateImageAsync")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Update an image by ID",
            Description = "Updates an existing image by its ID.",
            OperationId = "UpdateImage")]
        [SwaggerResponse(200, "Image updated")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(404, "Image not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult> PatchImageAsync(int id, [FromBody] ImageDTOPost image)
        {
            try
            {
                if (image == null || !ModelState.IsValid)
                {
                    _logger.LogWarning($"PatchImage - Invalid model state for image ID {id}");
                    return BadRequest(ModelState);
                }

                var previousImage = await _imageRepository.GetAsync(id);
                if (previousImage == null)
                {
                    _logger.LogInformation($"PatchImage - Image with ID {id} not found");
                    return NotFound();
                }

                Image mappedImage = _mapper.Map<Image>(image);
                mappedImage.ImageId = id;
                await _imageRepository.UpdateAsync(mappedImage, x => x.ImageId);

                _logger.LogInformation($"PatchImage - Image with ID {id} updated");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}", Name = "DeleteImageAsync")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Delete an image by ID",
            Description = "Deletes an image by its ID.",
            OperationId = "DeleteImage")]
        [SwaggerResponse(200, "Image deleted")]
        [SwaggerResponse(404, "Image not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult> DeleteImageAsync(int id)
        {
            try
            {
                var deleted = await _imageRepository.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogInformation($"DeleteImage - Image with ID {id} not found or could not be deleted");
                    return NotFound();
                }

                _logger.LogInformation($"DeleteImage - Image with ID {id} deleted");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
