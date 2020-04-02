using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace NetNotes.Models
{
    public class PeopleModel
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        public string biography { get; set; }

        //[Required]
        //[RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid should be like 12.34")]
        //public decimal percent { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime addedOn { get; set; }

        [Required]
        public bool status { get; set; }

    }
}