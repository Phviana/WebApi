using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebAPI.DAL.Model
{
    public static class BookExtension
    {
        public static byte[] ConvertToBytes(this IFormFile image)
        {
            if (image == null)
            {
                return null;
            }
            using (var inputStream = image.OpenReadStream())
            using (var stream = new MemoryStream())
            {
                inputStream.CopyTo(stream);
                return stream.ToArray();
            }
        }

        public static Book ToBook(this BookUpload model)
        {
            return new Book
            {
                Id = model.Id,
                Title = model.Title,
                Subtitle = model.Subtitle,
                Resume = model.Resume,
                Author = model.Author,
                CoverImage = model.CoverImage.ConvertToBytes(),
                List = model.List
            };
        }

        public static BookApi ToApi(this Book book)
        {
            return new BookApi
            {
                Id = book.Id,
                Title = book.Title,
                Subtitle = book.Subtitle,
                Resume = book.Resume,
                Author = book.Author,
                CoverImage = $"/api/books/{book.Id}/cover-image",
                List = book.List.ToString()
            };
        }

        public static BookUpload ToModel(this Book book)
        {
            return new BookUpload
            {
                Id = book.Id,
                Title = book.Title,
                Subtitle = book.Subtitle,
                Resume = book.Resume,
                Author = book.Author,
                List = book.List
            };
        }
    }
}
