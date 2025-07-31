using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EGramPanchayat.App_Code
{
    public class FileManager
    {
        public HttpPostedFileBase FileControl { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string Extension { get; set; }
        public string FileExtension { get; set; }
        public string[] AllowedExtensions { get; set; }
        public int FileSizeInKB { get; set; }
        public int MaxAllowedFileSizeInKB { get; set; }
        public int MinAllowedFileSizeInKB { get; set; }
        string Message;
        public FileManager()
        {
            FileControl = null;
            FileName = string.Empty;
            FolderName = "UserFiles";
            Extension = string.Empty;
            AllowedExtensions = new string[]
            {
            ".JPG",".PNG",".JPEG",".DOC",".XLS",".XALX","PPT",".TXT",".MP3",".MP4"};
            FileSizeInKB = 0;
            MaxAllowedFileSizeInKB = 2048;
            Message = string.Empty;
            MinAllowedFileSizeInKB = 10;

        }
        internal string validateMyFile()
        {
            try

            {
                if (FileControl != null)

                {
                    if (FileControl.ContentLength > 0)
                    {
                        FileName = Path.GetRandomFileName() + "_" + FileControl.FileName;
                        FileExtension = FileName.Substring(FileName.LastIndexOf('.'));
                        FileSizeInKB = FileControl.ContentLength / 1024;
                        //validate file type
                        //  bool status=AllowedExtensions.Contains(FileExtension,StringComparer.OrdinalIgnoreCase);
                        bool status = false;
                        foreach (string ext in AllowedExtensions)
                        {
                            if (ext.ToUpper() == FileExtension.ToUpper())
                            {
                                status = true;
                                break;
                            }
                        }
                        if (status == true)
                        {
                            //validate file size
                            if (FileSizeInKB <= MaxAllowedFileSizeInKB && FileSizeInKB >= MinAllowedFileSizeInKB)
                            {
                                Message = "SUCCESS";
                            }
                            else
                                Message = "filesize must be between " + MinAllowedFileSizeInKB + "KB and" + MaxAllowedFileSizeInKB + "KB.";
                        }
                        else
                            Message = "Invalid file type.Please choose a file";

                    }
                    else
                        Message = "NOT FOUND";

                }
                else
                    Message = "NOT FOUND";
            }
            catch
            {
                Message = "Sorry! due to some technical issue. We are unable to process your file.";
            }
            return Message;
        }
        internal string UploadMyFile()
        {
            Message = validateMyFile();
            {
                try
                {

                    if (Message == "SUCCESS")
                    {
                        string FilePath = HttpContext.Current.Server.MapPath("/Content/" + FolderName);
                        if (Directory.Exists(FilePath) == false)
                        {
                            Directory.CreateDirectory(FilePath); 
                        }
                        FileControl.SaveAs(FilePath + "/" + FileName);
                        Message = "SUCCESS";
                    }
                }
                catch
                {
                    Message = "Sorry! due to some technical issue; we are unable to upload your file.";

                }
                return Message;
            }

        }
    }
}