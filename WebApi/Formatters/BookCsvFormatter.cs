using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Model;

namespace WebApi.Formatters
{
    public class BookCsvFormatter : TextOutputFormatter
    {
        public BookCsvFormatter()
        {
            var textCsvMediaType = MediaTypeHeaderValue.Parse("text/csv");
            var appCsvMediaType = MediaTypeHeaderValue.Parse("application/csv");
            SupportedMediaTypes.Add(textCsvMediaType);
            SupportedMediaTypes.Add(appCsvMediaType);
            SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(BookApi);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var bookInCsv = "";

            if (context.Object is BookApi)
            {
                var book = context.Object as BookApi;

                bookInCsv = $"{book.Author};{book.Title};{book.Subtitle};{book.List}";
            }

            using (var writer = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding))
            {
              return writer.WriteAsync(bookInCsv);
            }
        }
    }
}
