
    using Cookiemonster.Models;
    using Cookiemonster.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    namespace Cookiemonster.Controllers
    {
        [Route("api/images")]
        [ApiController]
        public class ImageController : ControllerBase
        {
            private readonly ImageRepository _imageRepository;

            public ImageController(ImageRepository imageRepository)
            {
                _imageRepository = imageRepository;
            }

            // GET: api/images
            [HttpGet("getImages")]
            public ActionResult<IEnumerable<Image>> Get()
            {
                var images = _imageRepository.GetAllImages();
                return Ok(images);
            }

            // GET: api/images/5
            [HttpGet("getImagesById")]
            public ActionResult<Image> Get(int id)
            {
                var image = _imageRepository.GetImage(id);
                if (image == null)
                {
                    return NotFound();
                }
                return Ok(image);
            }

            // POST: api/images
            [HttpPost("postImages")]
            public ActionResult CreateImage(Image image)
            {
                _imageRepository.CreateImage(image);
                return Ok();
            }

            // PATCH: api/images
            [HttpPatch("patchImages")]
            public ActionResult PatchImage(Image image)
            {
                _imageRepository.UpdateImage(image);
                return Ok();
            }

            // DELETE: api/images/5
            [HttpDelete("deleteImageById")]
            public ActionResult DeleteImage(int id)
            {
                var deleted = _imageRepository.DeleteImage(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return Ok();
            }
        }
   }
