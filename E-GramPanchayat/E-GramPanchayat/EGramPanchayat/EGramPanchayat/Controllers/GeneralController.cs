using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGramPanchayat.App_Code;
using EGramPanchayat.Models;
namespace EGramPanchayat.Controllers
{
    public class GeneralController : Controller
    {
        Panchyat_DBEntities1 dbs= new Panchyat_DBEntities1();
        DBManager db = new DBManager();
        HumenDetector hd=new HumenDetector();
        static string[] CaptchaImgAndCode = new string[2];
        public ActionResult Home()
        {
            var n = dbs.Notification_Master.OrderByDescending(x => x.NDT).Take(5).ToList();
            List<Notification_Master> nl = n;
            return View(nl);
        }
        public ActionResult ImageGalary()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Developer()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Services1()
        {
            return View();
        }
        public ActionResult Services2 ()
        {
            return View();
        }
        public ActionResult Services3 ()
        {
            return View();
        }
        public ActionResult Services4 ()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult Login(Login_Master lm)
        {
            string msg = string.Empty;
            Login_Master lgdb = dbs.Login_Master.Find(lm.UserId);
            if (lgdb != null)
            {
                Cryptography cg = new Cryptography();
                string decrPass = cg.DecryptMyData(lgdb.Pass);
                if (decrPass == lm.Pass)
                {
                    if(lgdb.Status==true)
                    {
                        // Setting log of login in table...
                        lgdb.LCount = lgdb.LCount + 1;
                        lgdb.LastLogin_dt = DateTime.Now;
                        dbs.Entry(lgdb);
                        dbs.SaveChanges();
                        //Create session and transfer user to specific zone
                        if(lgdb.UserType== "BENEFICIERY")
                        {
                            Session["bid"] = lgdb.UserId;
                            return RedirectToAction("Dashboard", "User");
                        }
                        else
                        {
                            Session["aid"] = lgdb.UserId;
                            return RedirectToAction("AdminDashboard", "Admin");
                        }
                    }
                    else
                    {
                        msg = "Sorry! your account is suspended.";
                    }
                }
                else
                    msg = "Invalid Password. Please try again.";
            }
            else
                msg = "Invalid User Id. Please try again.";
            ViewBag.Result = msg;
            return View();
        }
        [HttpGet]
        public ActionResult UserRegistration()
        {
            BindState();
            CaptchaImgAndCode=hd.GetCaptchaImageAndCode();
            ViewBag.CaptchaImg = CaptchaImgAndCode[0];
            return View();
        }
        [HttpPost]
        public ActionResult UserRegistration(BeneficiaryMaster bm)
        {
            string msg = string.Empty;
            try
            {
                string UserCode = Request["CaptchaCode"];
                if (UserCode == CaptchaImgAndCode[1])
                {
                    //Verifying that is there any existing user with same EmailId
                    BeneficiaryMaster bmdb = dbs.BeneficiaryMasters.Find(bm.EmailId);
                    if (bmdb == null)
                    {
                        // To manage user uploaded file...
                        FileManager fm = new FileManager();
                        fm.FileControl = Request.Files["UserImg"];
                        fm.AllowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                        fm.MaxAllowedFileSizeInKB = 1024;
                        fm.FolderName = "BeneficiaryPic";
                        msg = fm.UploadMyFile();
                        if (msg == "SUCCESS" || msg=="NOT FOUND")
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
                                bm.RegDT = DateTime.Now;
                                bm.IsDel = false;
                                //Doing password encryption...
                                string Pass = Request["Passs"];
                                Cryptography cg = new Cryptography();
                                string ePass = cg.EncryptMyData(Pass);
                                //Creating and setting login attributes...
                                Login_Master lm = new Login_Master();
                                lm.UserId = bm.EmailId;
                                lm.Pass = ePass;
                                lm.Status = true;
                                lm.LCount = 0;
                                lm.UserType = "BENEFICIERY";
                                //Saving record on new user in registration and login table...
                                dbs.BeneficiaryMasters.Add(bm);
                                dbs.Login_Master.Add(lm);
                                dbs.SaveChanges();
                                msg = "Congratulations!! you are successfully added as a benificiary.";
                                //Sending welcome email to user.
                                EmailSender es = new EmailSender();
                                string EmailMsg = "Hello " + bm.Name + ", Congratulations!! you are registerred successfully in E-Gram Panchayat Portal.\n\nYour Login User Id is: " + bm.EmailId + "\nand Login Password is: " + Pass + "\n\n\nFrom-\nTeam E-Gram Panchayat\nContact No: +91-0522-67890";
                                es.SendEmail(bm.EmailId, "Welcome to E-Gram Panchayat", EmailMsg);
                            }
                            else
                              msg = msgAdhar == "NOT FOUND" ? "Please choose picture of Adhar card." : msgAdhar;
                        }
                    }  // closing of ==null if
                    else
                    {
                        msg = "Sorry! unable to create your account because there is already one registered benificiery with same emailid. Please try with a different email id.";
                    }
                }
                else
                {
                    msg = "Invalid captcha code. Please try again.";
                }
            }
            catch
            {
                  msg = "Sorry! due to some technical issue; we are unable to add you as a benificiery. Please try after some time.";
            }
            BindState();
            CaptchaImgAndCode = hd.GetCaptchaImageAndCode();
            ViewBag.CaptchaImg = CaptchaImgAndCode[0];
            ViewBag.Result = msg;
            return View();
        }
        public JsonResult GetNewCode()
        {
            CaptchaImgAndCode = hd.GetCaptchaImageAndCode();
            string CaptchaPic = CaptchaImgAndCode[0];
            return Json(CaptchaPic, JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        void BindState()
        {
            string cmd = "select * from State_Master";
            DataTable dt = db.executeQuery(cmd);
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
        public JsonResult getCity(int StateId)
        {
            string cmd = "select * from City_Master where StateId='" + StateId + "'";
            DataTable dt = db.executeQuery(cmd);
            List<City_Master> cm = new List<City_Master>();
            foreach (DataRow dr in dt.Rows)
            {
                City_Master smItem = new City_Master();
                smItem.CityId = int.Parse(dr["CityId"].ToString());
                smItem.CityName = dr["CityName"].ToString();
                cm.Add(smItem);
            }
            return Json(cm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getBlock(int CityId)
        {
            string cmd = "select * from Block_Master where CityId='" + CityId + "'";
            DataTable dt = db.executeQuery(cmd);
            List<Block_Master> cm = new List<Block_Master>();
            foreach (DataRow dr in dt.Rows)
            {
                Block_Master smItem = new Block_Master();
                smItem.BlockId = int.Parse(dr["BlockId"].ToString());
                smItem.BlockName = dr["BlockName"].ToString();
                cm.Add(smItem);
            }
            return Json(cm, JsonRequestBehavior.AllowGet);
        }

        //To save enquiry using Ajax
        public JsonResult SaveEnquiry(Enquiry_Master em)
        {
            string msg = "";
            try
            {
                if(em.Name!= null && em.EmailId!=null && em.MobNo!=null && em.Topic!=null && em.Enquiry_Msg!=null)
                {
                em.Enquiry_dt= DateTime.Now;
                dbs.Enquiry_Master.Add(em);
                dbs.SaveChanges();
                msg = "SUCCESS";
                //To send Email
                EmailSender ems=new EmailSender();
                string message = "Hii" + em.Name + "\nThanks for enquiry to us" + "\nWe will contact to you soon\n" + "from=\n EGramPanchayat";
                ems.SendEmail(em.EmailId, "Thanks from GramPanchayat", message);
                }
                else
                {
                    msg = "please fill all field.";
                }
            }
            catch
            {
                msg = "ERROR";
            }
            return Json(msg,JsonRequestBehavior.AllowGet);   
        }
    }
}