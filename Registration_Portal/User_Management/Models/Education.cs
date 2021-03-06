﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using User_Management.Bases;

namespace User_Management.Models
{
    [Table("TB_T_Education")]
    public class Education : IEntityInt
    {
        [Key]
        public int Id { get; set; }
        public University University { get; set; }
        public Department Department { get; set; }
        public Degree Degree { get; set; }
        public string GPA { get; set; }
        public string GraduateYear { get; set; }
    }
}
