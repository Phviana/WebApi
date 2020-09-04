using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebAPI.DAL.Books;
using WebAPI.DAL.Model;
using WebAPI.Model;

namespace WebApi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRepository<Book> _repo;

        public HomeController(IRepository<Book> repository)
        {
            _repo = repository;
        }

        private IEnumerable<BookApi> TypeList(TypeReadingList type)
        {
            return _repo.All
                .Where(l => l.List == type)
                .Select(l => l.ToApi())
                .ToList();
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                ToRead = TypeList(TypeReadingList.ToRead),
                Reading = TypeList(TypeReadingList.Reading),
                Read = TypeList(TypeReadingList.Read)
            };
            return View(model);
        }
    }
}