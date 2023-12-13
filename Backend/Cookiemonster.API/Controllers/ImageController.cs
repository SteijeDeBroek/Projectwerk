using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cookiemonster.API.Controllers
{
    [Route("Images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IRepository<Image> _imageRepository;
        private readonly IMapper _mapper;

        public ImageController(IRepository<Image> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        // GET: api/images
        [HttpGet("AllImages")]
        public ActionResult<IEnumerable<ImageDTOGet>> Get()
        {
            var images = _imageRepository.GetAll();
            return Ok(_mapper.Map<List<ImageDTOGet>>(images));
        }


        // GET: api/images/5
        [HttpGet("Images/{id}")]
        public ActionResult<ImageDTOGet> Get(int id)
        {
            var image = _imageRepository.Get(id);
            if (image == null)
            {
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
                return BadRequest(ModelState);
            }

            var createdImage = _imageRepository.Create(_mapper.Map<Image>(image));
            return CreatedAtAction(nameof(Get), _mapper.Map<ImageDTOGet>(createdImage));
        }
        // PATCH: api/images/5
        [HttpPatch("Recipe/{id}")]
        public ActionResult PatchImage(int id, [FromBody] ImageDTOPost image)
        {
            if (image == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var previousImage = _imageRepository.Get(id);
            if (previousImage == null)
            {
                return NotFound();
            }

            Image mappedImage = _mapper.Map<Image>(image);
            mappedImage.ImageId = id;

            _imageRepository.Update(mappedImage);
            return Ok();
        }

        // DELETE: api/images/5
        [HttpDelete("{id}")]
        public ActionResult DeleteImage(int id)
        {
            var deleted = _imageRepository.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
