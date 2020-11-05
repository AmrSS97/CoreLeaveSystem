using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Models
{
    public class VacationTypeVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 25, ErrorMessage = "Please Enter A Valid Number")]
        public int Balance { get; set; }
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }
    }

}
