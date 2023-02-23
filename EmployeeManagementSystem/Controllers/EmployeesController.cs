using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModel;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeDbContext db = new EmployeeDbContext();

        public ActionResult Index()
        {
            return View(db.tblEmployee.Include(x=>x.employeeMasterTable).ToList());
        }

        public ActionResult AddNewSkill(int? id)
        {
            ViewBag.skills = new SelectList(db.tblSkill.ToList(), "SkillId", "Type", id.ToString() ?? "");
            return PartialView("_addNewSkill");
        }

 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeVM empoyeeVM, int[] SkillId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Name = empoyeeVM.Name,
                    BirthDate = empoyeeVM.BirthDate,
                    Age = empoyeeVM.Age,
                    MaritalStatus = empoyeeVM.MaritalStatus
                };


                HttpPostedFileBase file = empoyeeVM.PictureFile;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    employee.Picture = filePath;
                }


                foreach (var item in SkillId)
                {
                    EmployeeMasterTable employeeMasterTable = new EmployeeMasterTable()
                    {
                        Employee = employee,
                        EmployeeId = employee.EmployeeId,
                        SkillId = item
                    };
                    db.tblEmployeeMasterTable.Add(employeeMasterTable);
                }
                db.SaveChanges();
                
            }

            return RedirectToAction("Index");
        }


        //public ActionResult Edit(int? id)
        //{
        //    Employee employee = db.tblEmployee.First(x => x.EmployeeId == id);
        //    EmployeeVM employeeVM = new EmployeeVM()
        //    {
        //        EmployeeId = employee.EmployeeId,
        //        Name = employee.Name,
        //        BirthDate = employee.BirthDate,
        //        Age = employee.Age,
        //        MaritalStatus = employee.MaritalStatus
        //    };

        //    var employeeskills = db.tblEmployeeMasterTable.Where(x => x.EmployeeId == id).ToList();

        //    foreach (var item in employeeskills)
        //    {
        //        employeeVM.SkillList.Add(item.SkillId);
        //    }

        //    return View(employeeVM);
        //}

        //[HttpPost]
        //public ActionResult Edit(EmployeeVM empoyeeVM, int[] SkillId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Employee employee = new Employee()
        //        {
        //            EmployeeId = empoyeeVM.EmployeeId,
        //            Name = empoyeeVM.Name,
        //            BirthDate = empoyeeVM.BirthDate,
        //            Age = empoyeeVM.Age,
        //            MaritalStatus = empoyeeVM.MaritalStatus
        //        };


        //        HttpPostedFileBase file = empoyeeVM.PictureFile;
        //        if (file != null)
        //        {
        //            string filePath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
        //            file.SaveAs(Server.MapPath(filePath));
        //            employee.Picture = filePath;
        //        }

        //        var employeeSkills = db.tblEmployeeMasterTable.Where(x => x.EmployeeId == empoyeeVM.EmployeeId).ToList();

        //        foreach (var item in employeeSkills)
        //        {
        //            db.tblEmployeeMasterTable.Remove(item);
        //        }

        //        foreach (var item in SkillId)
        //        {
        //            EmployeeMasterTable employeeMasterTable = new EmployeeMasterTable()
        //            {
        //                EmployeeId = employee.EmployeeId,
        //                SkillId = item
        //            };
        //            db.tblEmployeeMasterTable.Add(employeeMasterTable);
        //        }
        //        db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();

        //    }

        //    return RedirectToAction("Index");
        //}


        public ActionResult Edit(int? id)
        {
            Employee client = db.tblEmployee.First(x => x.EmployeeId == id);
            EmployeeVM clientVM = new EmployeeVM()
            {
                EmployeeId = client.EmployeeId,
                Name = client.Name,
                Age = client.Age,
                BirthDate = client.BirthDate,
                MaritalStatus = client.MaritalStatus
            };

            var clientSpots = db.tblEmployeeMasterTable.Where(x => x.EmployeeId == id).ToList();

            foreach (var item in clientSpots)
            {
                clientVM.SkillList.Add(item.SkillId);
            }

            return View(clientVM);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeVM clientVM, int[] SkillId)
        {
            if (ModelState.IsValid)
            {
                Employee client = new Employee()
                {
                    EmployeeId = clientVM.EmployeeId,
                    Name = clientVM.Name,
                    BirthDate = clientVM.BirthDate,
                    Age = clientVM.Age,
                    MaritalStatus = clientVM.MaritalStatus
                };
                HttpPostedFileBase file = clientVM.PictureFile;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    client.Picture = filePath;
                }

                var clientSpots = db.tblEmployeeMasterTable.Where(x => x.EmployeeId == clientVM.EmployeeId).ToList();

                foreach (var item in clientSpots)
                {
                    db.tblEmployeeMasterTable.Remove(item);
                }

                foreach (var item in SkillId)
                {
                    EmployeeMasterTable bookingEntry = new EmployeeMasterTable()
                    {
                        EmployeeId = client.EmployeeId,
                        SkillId = item
                    };
                    db.tblEmployeeMasterTable.Add(bookingEntry);
                }
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            Employee client = db.tblEmployee.First(x => x.EmployeeId == id);

            var clientSpots = db.tblEmployeeMasterTable.Where(x => x.EmployeeId == id).ToList();

            foreach (var item in clientSpots)
            {
                db.tblEmployeeMasterTable.Remove(item);
            }
            db.Entry(client).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
