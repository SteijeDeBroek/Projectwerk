
using Cookiemonster.Interfaces;
using Cookiemonster.Models;
    using Cookiemonster.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    namespace Cookiemonster.Controllers
    {
        [Route("api/images")]
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
                _imageRepository.Create(image);
                return Ok();
            }

            // PATCH: api/images
            [HttpPatch]
            public ActionResult PatchImage(Image image)
            {
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
