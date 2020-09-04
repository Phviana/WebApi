using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL.Books;
using WebAPI.DAL.Model;
using WebAPI.Model;

namespace WebApi.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadingListController : ControllerBase
    {
        private readonly IRepository<Book> _repo;

        public ReadingListController(IRepository<Book> repository)
        {
            _repo = repository;
        }

        private ReadingList CreateList(TypeReadingList type)
        {
            return new ReadingList
            {
                Type = type.ToString(),
                Books = _repo.All
                    .Where(l => l.List == type)
                    .Select(l => l.ToApi())
                    .ToList()
            };
        }

        [HttpGet]
        public IActionResult AllLists()
        {
            ReadingList toRead = CreateList(TypeReadingList.ToRead);
            ReadingList reading = CreateList(TypeReadingList.Reading);
            ReadingList read = CreateList(TypeReadingList.Read);
            var colection = new List<ReadingList> { toRead, reading, read };
            return Ok(colection);
        }

        [HttpGet("{type}")]
        public IActionResult Get(TypeReadingList type)
        {
            var list = CreateList(type);
            return Ok(list);
        }
    }
}