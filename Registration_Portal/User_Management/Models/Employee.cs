﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using User_Management.Bases;

namespace User_Management.Models
{
    [Table("TB_M_Employee")]
    public class Employee : IEntityInt
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int ReligionID { get; set; }
        public Religion Religion { get; set; }
        public Education Education { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

    }
}
