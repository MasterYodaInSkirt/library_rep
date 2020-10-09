using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDB.Models
{
    public class Book
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Country { get; set; }
        public DateTime Released { get; set; }
    }
}
