using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace EmployeeManagementSystem.ViewModel
{
    public class EmployeeVM
    {
        public EmployeeVM()
        {
            this.SkillList = new List<int>();
        }

        public int EmployeeId { get; set; }
        [Required, StringLength(80)]
        public string Name { get; set; }


        [Required, Display(Name = "Date of Birth"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }

        public HttpPostedFileBase PictureFile { get; set; }
        public string Picture { get; set; }
        public bool MaritalStatus { get; set; }

        public List<int> SkillList { get; set; }
    }
}