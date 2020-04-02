using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLibrary.Models
{
    public class PeopleModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string biography { get; set; }
        public DateTime addedOn { get; set; }
        public bool status { get; set; }

    }

}
