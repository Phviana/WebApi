using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPI.DAL.Model;

namespace WebAPI.Model
{
    public static class ReadingListExtensions
    {
        private static Dictionary<string, TypeReadingList> map =
            new Dictionary<string, TypeReadingList>
            {
                { "ToRead", TypeReadingList.ToRead },
                { "Reading", TypeReadingList.Reading },
                { "Read", TypeReadingList.Read }
            };

        public static string ToString(this TypeReadingList type)
        {
            return map.First(s => s.Value == type).Key;
        }

        public static TypeReadingList ToType(this string text)
        {
            return map.First(t => t.Key == text).Value;
        }
    }

    public enum TypeReadingList
    {
        ToRead,
        Reading,
        Read
    }

    public class ReadingList
    {
        public string Type { get; set; }
        public IEnumerable<BookApi> Books { get; set; }
    }
}
