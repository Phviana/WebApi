using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL.Books;
using WebAPI.DAL.Model;

namespace WebApi.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IRepository<Book> _repo;

        public BookController(IRepository<Book> repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public IActionResult New()
        {
            return View(new BookUpload());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(BookUpload model)
        {
            if (ModelState.IsValid)
            {
                _repo.Insert(model.ToBook());
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
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
            return File("~/images/covers/empty-cover.png", "image/png");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model.ToModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookUpload model)
        {
            if (ModelState.IsValid)
            {
                var livro = model.ToBook();
                if (model.CoverImage == null)
                {
                    livro.CoverImage = _repo.All
                        .Where(l => l.Id == livro.Id)
                        .Select(l => l.CoverImage)
                        .FirstOrDefault();
                }
                _repo.Update(livro);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            _repo.Delete(model);
            return RedirectToAction("Index", "Home");
        }
    }
}