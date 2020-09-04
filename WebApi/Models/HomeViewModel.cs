using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DAL.Model;

namespace WebApi.Models
{
    public class HomeViewModel
    {
        public IEnumerable<BookApi> ToRead { get; set; }
        public IEnumerable<BookApi> Reading { get; set; }
        public IEnumerable<BookApi> Read { get; set; }
    }
}
