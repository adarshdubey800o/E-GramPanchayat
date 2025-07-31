using EGramPanchayat.App_Code;
using EGramPanchayat.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EGramPanchayat.Controllers
{
    public class UserController : Controller
    {
        DBManager dbm = new DBManager();

        // GET: User
        Panchyat_DBEntities1 db = new Panchyat_DBEntities1();


        [NonAction]
        void ShowUserPicName()
        {
            if (Session["bid"] != null)
            {
                string uid = Session["bid"].ToString();
                if (uid != null)
                {
                    BeneficiaryMaster bm = db.BeneficiaryMasters.Find(uid);
                    string upicName = string.Empty;
                    if (bm.BeneficiaryPicName.Trim().Length == 0)
                    {
                        if (bm.Gender == "Male")
                            upicName = "/Content/Images/Male.png";
                        else
                            upicName = "/Content/Images/Male.png";
                    }
                    else
                        upicName = "/Content/BeneficiaryPic/" + bm.BeneficiaryPicName;
                    ViewBag.PicFileName = upicName;
                    ViewBag.Name = bm.Name;
                }
            }
            else
            {
                RedirectToAction("Dashboard", "User");
            }
            
        }
        [UserAuthorrizeSession]
        public ActionResult Dashboard()
        {
            ShowUserPicName();
            return View();
        }
        public ActionResult ApplicationStatus()
        {
            ShowUserPicName();
            return View();
        }
        [HttpGet]
        public ActionResult ApplySevices(string services)
        {
            Apply_Master AM = new Apply_Master();
            AM.Services = services;
            ShowUserPicName();
            ViewBag.Services = AM.Services;
            return View();
        }
        [HttpPost]
        public ActionResult ApplySevices(Apply_Master AM)
        {
            ShowUserPicName();
            AM.Apply_DT = DateTime.Now;
            db.Apply_Master.Add(AM);
            db.SaveChanges();

            ViewBag.res = "Services Applied  Successfully";
            return View();
        }

        public ActionResult SearchSevices()
        {
            ShowUserPicName();
            List<Service_Master> lst = db.Service_Master.ToList();
            return View(lst);

        }
        public ActionResult Feedback()
        {
            ShowUserPicName();
            return View();
        }
        [HttpPost]
        public ActionResult Feedback(Feedback_Master fm)
        {
            ShowUserPicName();
            string msg = "";
            if (fm != null)
            {
                fm.UserId = Session["bid"].ToString();
                fm.Feedback_DT = DateTime.Now;
                db.Feedback_Master.Add(fm);
                db.SaveChanges();
                msg = "Feedback added successfully";
            }
            else
            {
                msg = "Sorry unable to add your feedback";
            }
            ViewBag.Message = msg;
            return View();
        }
        [NonAction]
        void BindState()
        {
            string cmd = "select * from State_Master";
            DataTable dt = dbm.executeQuery(cmd);
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                SelectListItem item = new SelectListItem();
                item.Text = dr["StateName"].ToString();
                item.Value = dr["StateId"].ToString();
                lst.Add(item);
            }
            ViewData["RelatedStateId"] = lst;
        }
        [HttpGet]
        public ActionResult MyProfile()
        {
            BeneficiaryMaster bm = new BeneficiaryMaster();
            bm.EmailId = Session["bid"].ToString();
            BeneficiaryMaster bms = db.BeneficiaryMasters.Find(bm.EmailId);
            BindState();
            ShowUserPicName();
            return View(bms);
        }
        [HttpPost]
        public ActionResult MyProfile(BeneficiaryMaster bm)
        {
            string msg = string.Empty;
            try
            {
                // To manage user uploaded file...
                FileManager fm = new FileManager();
                fm.FileControl = Request.Files["UserImg"];
                fm.AllowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                fm.MaxAllowedFileSizeInKB = 1024;
                fm.FolderName = "BeneficiaryPic";
                msg = fm.UploadMyFile();
                if (msg == "SUCCESS" || msg == "NOT FOUND")
                {
                    // Uploading adhar pic
                    FileManager fm2 = new FileManager();
                    fm2.FileControl = Request.Files["AdharImg"];
                    fm2.AllowedExtensions = new string[] { ".jpg", ".png", ".jpeg", ".pdf" };
                    fm2.MaxAllowedFileSizeInKB = 3000;
                    fm2.FolderName = "AdharPic";
                    string msgAdhar = fm2.UploadMyFile();
                    if (msgAdhar == "SUCCESS")
                    {
                        // Setting remaing attributes in Benificiery master table
                        if (msg == "SUCCESS")
                            bm.BeneficiaryPicName = fm.FileName;
                        else
                            bm.BeneficiaryPicName = "";
                        bm.AdharPicName = fm2.FileName;
                        bm.IsDel = false;
                        //Saving record on new user in registration and login table...
                        BeneficiaryMaster master = db.BeneficiaryMasters.Find(bm.EmailId);
                        master.Name = bm.Name;
                        master.Gender = bm.Gender;
                        master.FatherName = bm.FatherName;
                        master.MobileNo = bm.MobileNo;
                        master.EmailId = bm.EmailId;
                        master.AdharNo = bm.AdharNo;
                        master.RelatedStateId = bm.RelatedStateId;
                        master.RelatedCityId = bm.RelatedCityId;
                        master.BeneficiaryPicName = bm.BeneficiaryPicName;
                        master.AdharPicName = bm.AdharPicName;
                        master.RelatedBlockId = bm.RelatedBlockId;
                        master.VillageName = bm.VillageName;
                        master.Address = bm.Address;
                        master.PinCode = bm.PinCode;

                        db.Entry(master);
                        db.SaveChanges();
                        msg = "Record Updated Successfully.";
                        //Sending welcome email to user.
                    }
                    else
                        msg = msgAdhar == "NOT FOUND" ? "Please choose picture of Adhar card." : msgAdhar;
                }
                // closing of ==null if              

            }
            catch
            {
                msg = "Sorry! due to some technical issue; we are unable to update your Record. Please try after some time.";
            }
            BindState();

            ShowUserPicName();
            TempData["Message"] = msg;
            return RedirectToAction("MyProfile");
        }
        public ActionResult ChangePassword()
        {
            ShowUserPicName();
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string CurPass, string NewPass, string ConfPass)
        {
            ShowUserPicName();
            string msg = "";
            if (NewPass == ConfPass)
            {
                string uid = Session["bid"].ToString();
                Cryptography cm = new Cryptography();
                string NPass = cm.EncryptMyData(NewPass);
                string CPass = cm.EncryptMyData(CurPass);
                Login_Master lm = db.Login_Master.SingleOrDefault(x => x.UserId == uid && x.Pass == CPass);
                if (lm != null)
                {
                    lm.Pass = NPass;
                    db.Entry(lm);
                    db.SaveChanges();
                    msg = "Password Updates successfully";
                }
                else
                {
                    msg = "Invalid Id and Password";
                }
            }
            else
            {
                msg = "Newpassword and conform password must be same";
            }
            ViewBag.Message = msg;
            return View();
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "General");
        }
    }
}