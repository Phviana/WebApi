using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL.Books;
using WebAPI.DAL.Model;

namespace WebApi.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> _repo;

        public BooksController(IRepository<Book> repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public IActionResult BookList()
        {
            var list = _repo.All.Select(l => l.ToApi()).ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model.ToApi());
        }

        [HttpGet("{id}/cover-image")]
        public IActionResult CoverImage(int id)
        {
            byte[] img = _repo.All
               .Where(l => l.Id == id)
               .Select(l => l.CoverImage)
               .FirstOrDefault();
            if (img != null)
            {
                return File(img, "image/png");
            }
            return File("~/images/capas/capa-vazia.png", "image/png");
        }

        [HttpPost]
        public IActionResult Insert([FromBody] BookUpload model)
        {
            if (ModelState.IsValid)
            {
                var book = model.ToBook();
                _repo.Insert(book);
                var uri = Url.Action("Recuperar", new { id = book.Id });
                return Created(uri, book); //201
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromBody] BookUpload model)
        {
            if (ModelState.IsValid)
            {
                var book = model.ToBook();
                if (model.CoverImage == null)
                {
                    book.CoverImage = _repo.All
                        .Where(l => l.Id == book.Id)
                        .Select(l => l.CoverImage)
                        .FirstOrDefault();
                }
                _repo.Update(book);
                return Ok(); //200
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            _repo.Delete(model);
            return NoContent(); //203
        }
    }
}