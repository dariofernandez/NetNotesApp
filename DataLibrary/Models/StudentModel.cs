using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLibrary.Models
{
    public class StudentModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int age { get; set; }
        public string standard { get; set; }
        public decimal percent { get; set; }
        public DateTime addedOn { get; set; }
        public bool status { get; set; }
    }
}
