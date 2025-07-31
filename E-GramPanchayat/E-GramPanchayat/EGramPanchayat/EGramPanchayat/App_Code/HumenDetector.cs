using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
namespace EGramPanchayat.App_Code
{
    public class HumenDetector
    {
        private string GetCaptchaCode()
        {
            string Code=string.Empty;
            char ch;
            Random r=new Random();
            ch=(char)r.Next(65,90);
            Code += ch;
            ch = (char)r.Next(49, 57);
            Code += ch;
            ch = (char)r.Next(91, 96);
            Code += ch;
            ch = (char)r.Next(70, 88);
            Code += ch;
            ch = (char)r.Next(100, 122);
            Code += ch;
            ch = (char)r.Next(65, 79);
            Code += ch;
            int n = r.Next(1, 100);
            if(n%2==0)
            {
                ch = (char)r.Next(97, 115);
                Code += ch;
            }
            if(n>50)
            {
                ch = (char)r.Next(49, 57);
                Code += ch;
            }
            return Code;

        }
        internal string[] GetCaptchaImageAndCode()
        {
            string[] ImgAndCode = new string[2];
            SolidBrush sbBlue = new SolidBrush(Color.Blue);
            Pen maroonPen= new Pen(Color.Maroon);
            Bitmap bmp = new Bitmap(150, 40);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Beige);
            // Rectangle rt = new Rectangle(2, 2, 148, 38);
            g.DrawRectangle(maroonPen, 2, 2, 146, 36);
            string code=GetCaptchaCode();
            Font f = new Font("Veradana", 18, FontStyle.Strikeout);
            Point pt = new Point(10, 4);
            g.DrawString(code,f,sbBlue,pt);
            g.Flush();
            // To save image in server.....
            string fpath = HttpContext.Current.Server.MapPath("/Content/Captcha");
            string ImgName = Path.GetRandomFileName() + "_" + DateTime.Now.ToShortDateString() + ".jpg";
            //Deleting all previous captcha image dfrfom folder..
            string[] allPicsName = Directory.GetFiles(fpath);
            foreach (string fname in allPicsName)
            {
                File.Delete(fname);
            }
           
                //Saving new captcha in image in folder
            bmp.Save(fpath+ "/"+ImgName);
            ImgAndCode[0] = ImgName;
            ImgAndCode[1] = code;
            return ImgAndCode;

        }
    }
}