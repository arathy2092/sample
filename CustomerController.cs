using niccrud.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace niccrud.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            customerhandler dbhandle = new customerhandler();
            ModelState.Clear();
            return View(dbhandle.GetStudent());
        }

        // 2. *************ADD NEW STUDENT ******************
        // GET: Student/Create
        public ActionResult Create()
        {

            customerhandler dbhandle = new customerhandler();
            customer MD = new customer();

            MD.countryList = dbhandle.Populatecountry();
            ViewBag.IdList = MD.countryList;
            return View(MD);
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(customer smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string FileName = Path.GetFileNameWithoutExtension(smodel.ImageFile.FileName);

                    //To Get File Extension  
                    string FileExtension = Path.GetExtension(smodel.ImageFile.FileName);

                    //Add Current Date To Attached File Name  
                    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                    //Get Upload path from Web.Config file AppSettings.  
                    //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

                    //Its Create complete path to store in server.  
                   // smodel.photo = UploadPath + FileName;
                    smodel.photo = "~/image/"+ FileName;
                    FileName = Path.Combine(Server.MapPath("~/image/"), FileName);
                    //To copy and save file into server.  
                    smodel.ImageFile.SaveAs(FileName);
                    customerhandler sdb = new customerhandler();
                    if (sdb.AddStudent(smodel))
                    {
                        ViewBag.Message = "Student Details Added Successfully";
                        ModelState.Clear();
                    }
                }
                customer MD = new customer();
                customerhandler dbhandle = new customerhandler();
                MD.countryList = dbhandle.Populatecountry();
                return View(MD);
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // 3. ************* EDIT STUDENT DETAILS ******************
        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            customerhandler sdb = new customerhandler();
            return View(sdb.GetStudent().Find(smodel => smodel.customerID == id));
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, customer smodel)
        {
            try
            {
                customerhandler sdb = new customerhandler();
                sdb.UpdateDetails(smodel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // 4. ************* DELETE STUDENT DETAILS ******************
        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                customerhandler sdb = new customerhandler();
                if (sdb.DeleteStudent(id))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
