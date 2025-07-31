using EGramPanchayat.App_Code;
using EGramPanchayat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace EGramPanchayat.Controllers
{
    public class AdminController : Controller
    {
        Panchyat_DBEntities db =new Panchyat_DBEntities();
        // GET: Admin
        [AdminAuthorization]
        public ActionResult AdminDashboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddServices()
        {
            return View();            
        }
        [HttpPost]
        public ActionResult AddServices(Service_Master sm)
        {
            sm.Service_DT = DateTime.Now.ToString();
            db.Service_Master.Add(sm);
            db.SaveChanges();
            ViewBag.res = "Services Added  Successfully";
            return View();

        }
        public ActionResult DeleteServices(int id)
        {
            var n = db.Service_Master.Find(id);
            db.Service_Master.Remove(n);
            db.SaveChanges();
            TempData["msg"] = "Services Deleted Successfully";
            return RedirectToAction("ManageServices");
        }
        [HttpGet]
        public ActionResult SendSMS()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendSMS(string SendTo, string Subject, string Message)
        {
           
            EmailSender es = new EmailSender();
            bool b = es.SendEmail(SendTo, Subject, Message);
            if (b == true)
                ViewBag.Message = "Email send Successfully";
            else
                ViewBag.Message = "Sorry!Email Send Unsuccessfully";
            return View();
        }
        [HttpGet]

        public ActionResult Notification()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Notification(Notification_Master n)
        {
            n.NDT = DateTime.Now.ToString();
            db.Notification_Master.Add(n);
            db.SaveChanges();
            ViewBag.msg = "Notification Saved Successfullty...";
            return View();
        }
        public ActionResult ManageNotification()
        {
            var n = db.Notification_Master.OrderBy(x => x.NDT).ToList();
            List<Notification_Master> nl = n;
            return View(nl);
        }
        [HttpGet]
        public ActionResult UpdateNotification(int id)
        {
            var n = db.Notification_Master.Find(id);
            Notification_Master ni = n;
            return View(ni);
        }
        [HttpPost]
        public ActionResult UpdateNotification(Notification_Master n)
        {
            var on = db.Notification_Master.Find(n.Nid);
            on.NMsg = n.NMsg;
            db.Entry(on);
            db.SaveChanges();
            Notification_Master ni = n;
            ViewBag.Message = "Notification Updated Successfully";
            return View(ni);
        }
        public ActionResult DeleteNotification(int id)
        {
            var n = db.Notification_Master.Find(id);
            db.Notification_Master.Remove(n);
            db.SaveChanges();
            TempData["msg"] = "Notification Deleted Successfully";
            return RedirectToAction("ManageNotification");
        }
        public ActionResult ManageServices()
        {
            var ap = db.Service_Master.OrderBy(x => x.Service_DT).ToList();
            List<Service_Master> bpl = ap;
            return View(bpl);
        }
        public ActionResult BeneficiaryDetails()
        {
            var ap = db.Apply_Master.OrderBy(x => x.Apply_DT).ToList();
            List<Apply_Master> bpl = ap;
            return View(bpl);
        }

        public ActionResult ManageBeneficiary()
        {
            var bp=db.BeneficiaryMasters.OrderBy(x=>x.RegDT).ToList();
            List<BeneficiaryMaster> bpl = bp;
            return View(bpl);
        }
        public ActionResult FeedbackManagement()
        {
            var fd = db.Feedback_Master.OrderBy(x => x.Feedback_DT).ToList();
            List<Feedback_Master> fdl = fd;
            return View(fdl);
            
        }
        public ActionResult EnquiryManagement()
        {
            var ed = db.Enquiry_Master.OrderBy(x => x.Enquiry_dt).ToList();
            List<Enquiry_Master> edl = ed;
            return View(edl);
        }
        public ActionResult ChangePassword()
        {    
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string CurPass, string NewPass, string ConfPass)
        {
            
            string msg = "";
            if (NewPass == ConfPass)
            {
                string uid = Session["aid"].ToString();
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
        public ActionResult QNA()
        {
            return View();
        }
        //    [WebMethod(EnableSession = true)]
        public  JsonResult Save_QNA_Data(PL_QNA_Master pobj)
        {
            string msg = "";
            if (pobj.URL != "" && pobj.Question != "" && pobj.Answer != "")
            {
                //UserDetail objUserDetail = HttpContext.Current.Session["User_Detail"] as UserDetail;
                //string CreatedBy = objUserDetail.userFirstName + objUserDetail.userLastName;
                // pobj.CreatedBy = CreatedBy;
                BL_QNA_Master.save(pobj);
                msg = "Record Saved successfully.";
            }
            else
            {
                //return "All fields are mandatory";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        //    [WebMethod(EnableSession = true)]
        public JsonResult GetQNA_Data(PL_QNA_Master pobj)
        {
            BL_QNA_Master.Show(pobj);
            //if (!pobj.isException)
            //{
            //    return pobj.Ds.GetXml();
            //}
            //else
            //{
            //    return "false";
            //}
            return Json(pobj.Ds.GetXml(), JsonRequestBehavior.AllowGet);

        }
        //    [WebMethod(EnableSession = true)]
        //    public static string removeQNA_Data(PL_QNA_Master pobj)
        //    {

        //            BL_QNA_Master.removeQNA(pobj);
        //            if (!pobj.isException)
        //            {
        //                return "true";
        //            }
        //            else
        //            {
        //                return pobj.exceptionMessage;
        //            }

        //    }
        //    [WebMethod(EnableSession = true)]
        //    public static string EditQNA_Data(PL_QNA_Master pobj)
        //    {
        //        BL_QNA_Master.editQNA(pobj);
        //        if (!pobj.isException)
        //        {
        //            return pobj.Ds.GetXml();
        //        }
        //        else
        //        {
        //            return "false";
        //        }
        //    }
        //    [WebMethod(EnableSession = true)]
        //    public static string UpdateQNA_Data(PL_QNA_Master pobj)
        //    {

        //            if (pobj.URL != "" && pobj.Question != "" && pobj.Answer != "")
        //            {
        //                BL_QNA_Master.UpdateQNA(pobj);
        //                if (!pobj.isException)
        //                {
        //                    return "true";
        //                }
        //                else
        //                {
        //                    return pobj.exceptionMessage;
        //                }
        //            }
        //            else
        //            {
        //                return "All fields are mandatory";
        //            }
        //    }
    }
    }