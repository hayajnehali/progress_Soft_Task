using BusinessCardInformation.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.Models.Request
{ 
    public class BusinessCardDTO 
    {
        public int BusinessCardId { get; set; }
        public string Name { get; set; } 
        public string Gender { get; set; } 
        public DateTime DateOfBirth { get; set; } 
        public string Email { get; set; } 
        public string Phone { get; set; }
         
        public string Photo { get; set; } 
        public string Address { get; set; }

    }
    public class BusinessCardFilter : BaseFilter
    { 
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; } 
    }
}
