using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Admin
{
    public class UserAssignRoleDto
    {
        public string? IdentityId { get; set; }
        public bool isAdmin { get; set; }
        public bool isTrainer { get; set; }
        public bool isStudent { get; set; }
        public Guid? BranchId { get; set; }
    }
}
