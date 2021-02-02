using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using User_Management.Bases;

namespace User_Management.Models
{
    [Table("TB_M_Degree")]
    public class Degree : IEntityString
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
