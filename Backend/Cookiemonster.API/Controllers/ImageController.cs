using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

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
            _imageRepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/images
        [HttpGet("AllImages")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get all images",
            Description = "Retrieve a list of all images.",
            OperationId = "GetAllImages"
        )]
        public ActionResult<IEnumerable<ImageDTOGet>> GetAllImages()
        {
            _logger.LogInformation("GetAllImages - Fetching all images");
            var images = _imageRepository.GetAllAsync();
            return Ok(_mapper.Map<List<ImageDTOGet>>(images));
        }

        // GET: api/images/5
        [HttpGet("ImageById/{id}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get an image by ID",
            Description = "Retrieve an image by its ID.",
            OperationId = "GetImageById"
        )]
        public ActionResult<ImageDTOGet> GetImageById(int id)
        {
            _logger.LogInformation($"GetImageById - Fetching image with ID {id}");
            var image = _imageRepository.GetAsync(id);
            if (image == null)
            {
                _logger.LogWarning($"GetImageById - Image with ID {id} not found");
                return NotFound();
            }
            return Ok(_mapper.Map<ImageDTOGet>(image));
        }

        // POST: api/images
        [HttpPost("Image")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Create an image",
            Description = "Create a new image.",
            OperationId = "CreateImage"
        )]
        public ActionResult CreateImage(ImageDTOPost image)
        {
            if (image == null || !ModelState.IsValid)
            {
                _logger.LogWarning("CreateImage - Invalid model state");
                return BadRequest(ModelState);
            }

            var createdImage = _imageRepository.CreateAs(_mapper.Map<Image>(image));
            _logger.LogInformation($"CreateImage - Image created with ID: {createdImage.ImageId}");
            return CreatedAtAction(nameof(GetImageById), new { id = createdImage.ImageId }, _mapper.Map<ImageDTOGet>(createdImage));
        }

        // PATCH: api/images/5
        [HttpPatch("Image/{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Update an image by ID",
            Description = "Update an image by its ID.",
            OperationId = "PatchImage"
        )]
        public ActionResult PatchImage(int id, [FromBody] ImageDTOPost image)
        {
            if (image == null || !ModelState.IsValid)
            {
                _logger.LogWarning($"PatchImage - Invalid model state for image ID {id}");
                return BadRequest(ModelState);
            }

            var previousImage = _imageRepository.GetAsync(id);
            if (previousImage == null)
            {
                _logger.LogInformation($"PatchImage - Image with ID {id} not found");
                return NotFound();
            }

            Image mappedImage = _mapper.Map<Image>(image);
            mappedImage.ImageId = id;
            _imageRepository.UpdateAsync(mappedImage, x => x.ImageId);

            _logger.LogInformation($"PatchImage - Image with ID {id} updated");
            return Ok();
        }

        // DELETE: api/images/5
        [HttpDelete("Image/{id}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Delete an image by ID",
            Description = "Delete an image by its ID.",
            OperationId = "DeleteImage"
        )]
        public ActionResult DeleteImage(int id)
        {
            var deleted = _imageRepository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogInformation($"DeleteImage - Image with ID {id} not found or could not be deleted");
                return NotFound();
            }

            _logger.LogInformation($"DeleteImage - Image with ID {id} deleted");
            return Ok();
        }
    }
}
