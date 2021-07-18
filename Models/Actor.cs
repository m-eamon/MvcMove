using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set;}

        public DateTime DOB { get; set; }

    }
}