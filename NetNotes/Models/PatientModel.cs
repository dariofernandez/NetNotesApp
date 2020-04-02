using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace NetNotes.Models
{
    public class PatientModel
    {
        public int ID { get; set; }

        public bool Active { get; set; }

        [Required]
        public string First_Name { get; set; }

        public string Middle_Name { get; set; }

        [Required]
        public string Last_Name { get; set; }

        [DataType(DataType.DateTime)]
        //public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        public DateTime Date_Of_Birth { get; set; }

        public string Home_Phone { get; set; }
        public string Work_Phone { get; set; }
        public string Alternate_Phone { get; set; }
        public string Sex { get; set; }
        public string Directions { get; set; }
        public string SSN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int LanguageID { get; set; }
        public string Email { get; set; }

    }
}