﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Admin
{
    public class AdminLoggedInUserUpdateDto
    {
        public string? IdentityId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? CountryCode { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile? Image { get; set; }
        public string? FileType { get; set; }
        public string? Address { get; set; }
    }
}
