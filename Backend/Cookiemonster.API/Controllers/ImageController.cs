using Cookiemonster.Interfaces;
using Cookiemonster.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cookiemonster.API.Controllers
{
    [Route("images")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IRepository<Image> _imageRepository;

        public ImageController(IRepository<Image> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        // GET: api/images
        [HttpGet]
        public ActionResult<IEnumerable<Image>> Get()
        {
            var images = _imageRepository.GetAll();
            return Ok(images);
        }

        // GET: api/images/5
        [HttpGet("{id}")]
        public ActionResult<Image> Get(int id)
        {
            var image = _imageRepository.Get(id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        // POST: api/images
        [HttpPost]
        public ActionResult CreateImage(Image image)
        {
            if (image == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _imageRepository.Create(image);
            return Ok();
        }

        // PATCH: api/images/5
        [HttpPatch("{id}")]
        public ActionResult PatchImage(int id, [FromBody] Image image)
        {
            if (image == null || image.ImageId != id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _imageRepository.Update(image);
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
