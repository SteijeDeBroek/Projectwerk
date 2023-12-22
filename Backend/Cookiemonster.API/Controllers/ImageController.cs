using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

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
        public ActionResult<IEnumerable<ImageDTOGet>> GetAllImages()
        {
            _logger.LogInformation("GetAllImages - Fetching all images");
            var images = _imageRepository.GetAll();
            return Ok(_mapper.Map<List<ImageDTOGet>>(images));
        }

        // GET: api/images/5
        [HttpGet("ImageById/{id}")]
        public ActionResult<ImageDTOGet> GetImageById(int id)
        {
            _logger.LogInformation($"GetImageById - Fetching image with ID {id}");
            var image = _imageRepository.Get(id);
            if (image == null)
            {
                _logger.LogWarning($"GetImageById - Image with ID {id} not found");
                return NotFound();
            }
            return Ok(_mapper.Map<ImageDTOGet>(image));
        }

        // POST: api/images
        [HttpPost("Image")]
        public ActionResult CreateImage(ImageDTOPost image)
        {
            if (image == null || !ModelState.IsValid)
            {
                _logger.LogWarning("CreateImage - Invalid model state");
                return BadRequest(ModelState);
            }

            var createdImage = _imageRepository.Create(_mapper.Map<Image>(image));
            _logger.LogInformation($"CreateImage - Image created with ID: {createdImage.ImageId}");
            return CreatedAtAction(nameof(GetImageById), new { id = createdImage.ImageId }, _mapper.Map<ImageDTOGet>(createdImage));
        }

        // PATCH: api/images/5
        [HttpPatch("Image/{id}")]
        public ActionResult PatchImage(int id, [FromBody] ImageDTOPost image)
        {
            if (image == null || !ModelState.IsValid)
            {
                _logger.LogWarning($"PatchImage - Invalid model state for image ID {id}");
                return BadRequest(ModelState);
            }

            var previousImage = _imageRepository.Get(id);
            if (previousImage == null)
            {
                _logger.LogInformation($"PatchImage - Image with ID {id} not found");
                return NotFound();
            }

            Image mappedImage = _mapper.Map<Image>(image);
            mappedImage.ImageId = id;
            _imageRepository.Update(mappedImage, x => x.ImageId);

            _logger.LogInformation($"PatchImage - Image with ID {id} updated");
            return Ok();
        }

        // DELETE: api/images/5
        [HttpDelete("Image/{id}")]
        public ActionResult DeleteImage(int id)
        {
            var deleted = _imageRepository.Delete(id);
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
