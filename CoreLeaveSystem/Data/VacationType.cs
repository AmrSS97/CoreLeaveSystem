using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Data
{
    public class VacationType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Balance { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
