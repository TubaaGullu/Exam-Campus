﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Students
{
    public class StudentUpdateDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? Address { get; set; }
        public IFormFile? Image { get; set; }
        public string? FileType { get; set; }
        public Guid BranchId { get; set; }
    }
}
