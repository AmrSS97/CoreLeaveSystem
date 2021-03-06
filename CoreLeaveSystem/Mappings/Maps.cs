﻿using AutoMapper;
using CoreLeaveSystem.Data;
using CoreLeaveSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLeaveSystem.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<VacationType, VacationTypeVM>().ReverseMap();
            CreateMap<VacationAllocation, VacationAllocationVM>().ReverseMap();
            CreateMap<VacationAllocation, EditVacationAllocationVM>().ReverseMap();
            CreateMap<VacationRequest, VacationRequestVM>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
        }
    }
}
