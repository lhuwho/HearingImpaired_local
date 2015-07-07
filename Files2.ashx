<%@ WebHandler Language="C#" Class="Files2" Debug="true" %>

using System;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

public class Files2 : IHttpHandler {

    protected string PicName;
    protected string Patch, Patch2;
    protected string returnvalue;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Request.ContentEncoding = System.Text.Encoding.UTF8;
        string type = context.Request.QueryString["type"].ToString();
        string PID = context.Request.QueryString["ID"].ToString();
        string mainPatch = "~/uploads/property/";
        if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(mainPatch))) //檢查文件夾是否存在。
        {
            System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(mainPatch)); //不存在，創建資料夾。
        }
        mainPatch = "~/uploads/property/" + PID + "/";
        if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(mainPatch))) //檢查文件夾是否存在。
        {
            System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(mainPatch)); //不存在，創建資料夾。
        }
        switch (type)
        {
            case "PropertyAnnex":
                Patch = "~/uploads/property/" + PID + "/org/";
                Patch2 = "~/uploads/property/" + PID + "/print/";

                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(Patch))) //檢查文件夾是否存在。
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(Patch)); //不存在，創建資料夾。
                }
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(Patch2))) //檢查文件夾是否存在。
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(Patch2)); //不存在，創建資料夾。
                }
                returnvalue = PicSave(context);
                break;

            /*case "TransRecord":
                Patch = mainPatch;
                returnvalue = PicSave2(context);
                break;*/
        }
        context.Response.Write(returnvalue);
        context.Response.End();
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    
    public bool SaveImage(System.Drawing.Bitmap b, String SavePathFilename)
    {
        try
        {
            System.Drawing.Imaging.ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            System.Drawing.Imaging.EncoderParameter myEncoderParameter;
            System.Drawing.Imaging.EncoderParameters myEncoderParameters;

            myImageCodecInfo = this.GetEncoderInfo("image/jpeg");
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);

            myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            b.Save(SavePathFilename, myImageCodecInfo, myEncoderParameters);
            return true;
        }
        catch (Exception e)
        {
            string exception = e.Message.ToString();
            string inner = e.InnerException.Message.ToString();
            string source = e.Source.ToString();
            string method = e.TargetSite.ToString();
            return false;
        }
    }
    private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        System.Drawing.Imaging.ImageCodecInfo[] encoders;
        encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }
    
    public string PicSave(HttpContext context)
    {
        string Value = "";
        int new_width = 200;
        int new_height = 200;
        HttpFileCollection files = context.Request.Files;
        if (files.Count > 0)
        {
            try
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];

                    if (file.ContentLength > 0)
                    {
                        PicName = Guid.NewGuid().ToString();
                        string fileName = file.FileName;
                        string extension = Path.GetExtension(fileName).ToLower();
                        string path = Patch + PicName + extension;
                        string valuePath = PicName + extension;
                        string savePath = System.Web.HttpContext.Current.Server.MapPath(path);
                        string FNAME = files.AllKeys[i].ToString();

                        if (extension == ".jpg" || extension == ".jpeg")
                        {
                            System.Drawing.Bitmap pinsrc = new System.Drawing.Bitmap(file.InputStream);
                            try
                            {
                                this.SaveImage(pinsrc, savePath);
                                pinsrc = new System.Drawing.Bitmap(savePath);
                                path = Patch2 + PicName + extension;
                                path = System.Web.HttpContext.Current.Server.MapPath(path);
                                ResizePic(pinsrc, path, extension, new_width, new_height);
                                pinsrc.Dispose();
                            }
                            catch (Exception e)
                            {
                                string ee = e.Message.ToString();
                                pinsrc.Dispose();
                            }
                        }
                        else
                        {
                            file.SaveAs(savePath);
                            path = Patch2 + PicName + extension;
                            path = System.Web.HttpContext.Current.Server.MapPath(path);
                            System.Drawing.Bitmap pinsrc = new System.Drawing.Bitmap(savePath);
                            ResizePic(pinsrc, path, extension, new_width, new_height);
                        }

                        /*
                                returnvalue = "{\"Msg\":{\"aaa\":\"12345.jpg\",\"bbb\":\"23456.pmg\"}}";
                         */
                        Value += "\"" + FNAME + "\"" + ":" + "\"" + PicName + extension + "\",";
                    }
                }

                return "{\"Msg\":{" + Value + "}}";
            }
            catch (Exception e)
            {
                return "{\"error\":\"" + e.Message.ToString() + "\"}";
            }
        }
        else
        {
            return "{\"error\":\"NO PIC\"}";
        }
    }
    
    public string PicSave2(HttpContext context)
    {
        string Value = "";
        HttpFileCollection files = context.Request.Files;
        if (files.Count > 0)
        {
            try
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];

                    if (file.ContentLength > 0)
                    {
                        PicName = Guid.NewGuid().ToString();
                        string fileName = file.FileName;
                        string extension = Path.GetExtension(fileName).ToLower();
                        string path = Patch + PicName + extension;
                        string valuePath = PicName + extension;
                        string savePath = System.Web.HttpContext.Current.Server.MapPath(path);
                        string FNAME = files.AllKeys[i].ToString();
                        if (extension != ".exe" && extension != ".rar" && extension != ".zip")
                        {
                            file.SaveAs(savePath);
                            Value += "\"" + FNAME + "\"" + ":" + "\"" + PicName + extension + "\",";
                        }
                        else
                        {
                            return "{\"error\":\"檔案格式不正確\"}";
                        }
                    }
                }
                return "{\"Msg\":{" + Value + "}}";
            }
            catch (Exception e)
            {
                return "{\"error\":\"" + e.Message.ToString() + "\"}";
            }
        }
        else
        {
            return "{\"error\":\"NO FILE\"}";
        }
    }
    
    private void ResizePic(System.Drawing.Bitmap pinsrc, string sfileName, string extension, int target_width, int target_height)
    {
        try
        {
            int Width = pinsrc.Width;
            int Height = pinsrc.Height;
            int new_height, new_width;
            float widthDividend, heightDividend, commonDividend;

            target_width = (Width < target_width) ? Width : target_width;
            target_height = (Height < target_height) ? Height : target_height;

            widthDividend = (float)Width / (float)target_width;
            heightDividend = (float)Height / (float)target_height;

            commonDividend = (heightDividend > widthDividend) ? heightDividend : widthDividend;
            new_width = (int)(Width / commonDividend);
            new_height = (int)(Height / commonDividend);

            System.Drawing.Bitmap resizeIMG = Resize(pinsrc, new_width, new_height);
            if (extension == ".png")
            {
                resizeIMG.Save(sfileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            else if (extension == ".jpg" || extension == ".jpeg")
            {
                //resizeIMG.Save(sfileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                this.SaveImage(resizeIMG, sfileName);
            }
            else if (extension == ".bmp")
            {
                resizeIMG.Save(sfileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            resizeIMG.Dispose();
        }
        catch (Exception e)
        {
            string item = e.Message;
        }
        finally
        {
            pinsrc.Dispose();
        }
    }

    private System.Drawing.Bitmap Resize(System.Drawing.Bitmap src, int resizewidth, int resizeheight)
    {
        System.Drawing.Bitmap resizeb = new System.Drawing.Bitmap(resizewidth, resizeheight);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(resizeb);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        g.Clear(System.Drawing.Color.Transparent);
        g.DrawImage(src, new System.Drawing.Rectangle(0, 0, resizewidth, resizeheight), new System.Drawing.Rectangle(0, 0, src.Width, src.Height), System.Drawing.GraphicsUnit.Pixel);

        g.Dispose();
        src.Dispose();
        return resizeb;
    }
    
}