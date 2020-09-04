using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using WebAPI.Model;

namespace WebAPI.DAL.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Resume { get; set; }
        public byte[] CoverImage { get; set; }
        public string Author { get; set; }
        public TypeReadingList List { get; set; }
    }
    [XmlType("Book")]
    public class BookApi
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Author { get; set; }
        public string Resume { get; set; }
        public string CoverImage { get; set; }
        public string List { get; set; }
    }

    public class BookUpload
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Author { get; set; }
        public string Resume { get; set; }
        public IFormFile CoverImage { get; set; }
        public TypeReadingList List { get; set; }
    }
}
