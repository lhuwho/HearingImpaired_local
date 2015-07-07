using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
public partial class upload : System.Web.UI.Page
{
    
    string picPath = "";
    string picServer = "./Edit";
    protected string itemID = "";
    protected string returnvalue = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] != null)
        {
            itemID = Request.QueryString["id"];
        }

        if (IsPostBack)
        {
            picPath = Server.MapPath("~/Edit/");
            doUpload();
        }
    }

    protected void doUpload()
    {
        string tempPath = "";
        int Width = 0;
        int Height = 0;
        string urlPath;
        if (string.IsNullOrEmpty((string)HttpContext.Current.Session["App"]))
        {
            urlPath = "noSession";
            WriteJs("parent.uploadsuccess('" + urlPath + "','" + itemID + "','" + Height + "','" + Width + "'); ");
        }
        else
        {
            try
            {
                HttpPostedFile file = file1.PostedFile;
                string extension = Path.GetExtension(file.FileName).ToUpper();
                string strNewPath = picPath + (string)HttpContext.Current.Session["App"] + "\\";
                string savePath = strNewPath + itemID + extension;
                int MaxLength = 640;

               
                if (!Directory.Exists(strNewPath))
                {
                    Directory.CreateDirectory(strNewPath);
                }

                tempPath = strNewPath + "org" + itemID + extension;

                file.SaveAs(tempPath);

                System.Drawing.Image Photo = System.Drawing.Image.FromFile(tempPath);

                if (Photo.Width > Photo.Height)
                {
                    Width = MaxLength;
                    Height = Convert.ToInt32(Convert.ToSingle(Photo.Height) / Convert.ToSingle(Photo.Width) * MaxLength);
                }
                else
                {
                    Height = MaxLength;
                    Width = Convert.ToInt32(Convert.ToSingle(Photo.Width) / Convert.ToSingle(Photo.Height) * MaxLength);
                }

                Bitmap b = new Bitmap(Photo, Width, Height);
    
                b.Save(savePath);
                b.Dispose();


                Photo.Dispose();

              
                urlPath = picServer + "/" + (string)HttpContext.Current.Session["App"] + "/" + itemID + extension;
                urlPath = urlPath.Replace("\\", "/");
                WriteJs("parent.uploadsuccess('" + urlPath + "','" + itemID + "','" + Height + "','" + Width + "'); ");

            }
            catch
            {
                File.Delete(tempPath);
                WriteJs("parent.uploaderror('" + returnvalue + "');");
            }
        }
    }
    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }
    private string GetFileName(string fileName)
    {
        try
        {
            int startPos = fileName.LastIndexOf("\\");
            string ext = fileName.Substring(startPos + 1, fileName.Length - startPos - 1);
            return ext;
        }
        catch
        {
            WriteJs("parent.uploaderror('" + itemID + "');");
            return string.Empty;
        }
    }


    private string GetExtension(string fileName)
    {
        try
        {
            int startPos = fileName.LastIndexOf(".");
            string ext = fileName.Substring(startPos, fileName.Length - startPos);
            return ext;
        }
        catch (Exception ex)
        {
            WriteJs("parent.uploaderror('" + ex.ToString() + "');");
            return string.Empty;
        }
    }

    private string GetSaveFilePath()
    {
        try
        {
            DateTime dateTime = DateTime.Now;
            string yearStr = dateTime.Year.ToString(); ;
            string monthStr = dateTime.Month.ToString();
            string dayStr = dateTime.Day.ToString();
            string hourStr = dateTime.Hour.ToString();
            string minuteStr = dateTime.Minute.ToString();
            string dir = dateTime.ToString(@"\\yyyyMMdd");
            if (!Directory.Exists(picPath + dir))
            {
                Directory.CreateDirectory(picPath + dir);
            }
            return dir + dateTime.ToString("\\\\yyyyMMddhhmmssffff");
        }
        catch (Exception ex)
        {
            WriteJs("parent.uploaderror('" + ex.ToString() + "');");
            return string.Empty;
        }
    }

    protected void WriteJs(string jsContent)
    {
        this.Page.RegisterStartupScript("writejs", "<script type='text/javascript'>" + jsContent + "</script>");
    }

}
