﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Models
{
    public class VacationAllocationVM
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public int Period { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public VacationTypeVM VacationType { get; set; }
        public int VacationTypeId { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> VacationTypes { get; set; }
    }

    public class CreateVacationAllocationVM
    {
        public int NumberUpdated { get; set; }
        public List<VacationTypeVM> VacationTypes { get; set; }
    }
}
