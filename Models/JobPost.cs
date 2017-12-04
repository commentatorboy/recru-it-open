using recru_it.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace recru_it.Models
{
    public class JobPost
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string CreatedBy { get; set; }

        public ApplicationUser User { get; set; }
        public Tag Tags { get; set; }
    }
}
