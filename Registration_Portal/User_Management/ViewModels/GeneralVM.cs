using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User_Management.ViewModels
{
    public class GeneralVM
    {
        // Model User
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsUpdatePassword { get; set; }

        // Model Employee
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

        // Model Religion
        public int ReligionId { get; set; }
        public string ReligionName { get; set; }

        // Model Education
        public int EducationId { get; set; }
        public string GPA { get; set; }
        public string GraduateYear { get; set; }

        // Model University
        public string UniversityId { get; set; }
        public string UniversityName { get; set; }

        // Model Department
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        // Model Degree
        public string DegreeId { get; set; }
        public string DegreeName { get; set; }

        // Model Role
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
