using recru_it.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace recru_it.Models
{
    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TypeOfCompany { get; set; }
        public string CVR { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
