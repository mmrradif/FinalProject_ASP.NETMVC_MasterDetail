using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.Entity;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public Employee()
        {
            this.employeeMasterTable = new List<EmployeeMasterTable>();
        }

        public int EmployeeId { get; set; }
        [Required, StringLength(80)]
        public string Name { get; set; }


        [Required, Display(Name = "Date of Birth"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }
        public bool MaritalStatus { get; set; }

        public ICollection<EmployeeMasterTable> employeeMasterTable { get; set; }
    }

    public class Skill
    {
        public Skill()
        {
            this.employeeMasterTable = new List<EmployeeMasterTable>();
        }
        public int SkillId { get; set; }
        public string Type { get; set; }

        public ICollection<EmployeeMasterTable> employeeMasterTable { get; set; }
    }

    public class EmployeeMasterTable
    {
        public int Id { get; set; }


        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }


        [ForeignKey("Skill")]
        public int SkillId { get; set; }

        //Nav
        public virtual Employee Employee { get; set; }
        public virtual Skill Skill { get; set; }
    }

    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> tblEmployee { get; set; }
        public DbSet<Skill> tblSkill { get; set; }
        public DbSet<EmployeeMasterTable> tblEmployeeMasterTable { get; set; }
    }
}