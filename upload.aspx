<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
    <style type="text/css">
        * { margin:0; padding:0; }
        body {}
        #bag { position:absolute; left:0px; top:0px; width:500px; height:500px; background:#000; z-index:-100;}
        #content{position:absolute;left:20px; top:10px;}
        #content p{color:#999; font-family:"Verdana","新細明體"; font-size:11px;}
        .little_window_bg {
            /*background:url('../image/littlewindow/little_window_bg.png');
            background-color: #FFF;
            top:0px; 
            left:20px;
            width:320px;
            height:180px;*/
        }
        #main {
            /*position:absolute;
            top:55px; 
            left:45px;*/
        }
    </style>
    <script type="text/javascript">
        //這裡控制要檢查的項目，true表示要檢查，false表示不檢查
        var isCheckImageType = true; //是否檢查圖片副檔名
        var isCheckImageWidth = false; //是否檢查圖片寬度
        var isCheckImageHeight = false; //是否檢查圖片高度
        var isCheckImageSize = true; //是否檢查圖片檔案大小

        var ImageSizeLimit = 3*1024*1024; //上傳上限，單位:byte
        var ImageWidthLimit = 150; //圖片寬度上限
        var ImageHeightLimit = 150; //圖片高度上限
        var img = new Image();
        function checkFile(el) {

            var f = document.FileForm;
            var re = /\.(jpg|png|bmp|gif)$/i; //允許的圖片副檔名
            //alert(document.getElementById('file1').value);
            if (isCheckImageType && !re.test(document.getElementById('file1').value)) {
                alert("只允許上傳JPG,GIF,BMP,PNG等檔案格式");
            } else {
                el.fadeOut("show");
                parent.uploading('<%=itemID %>');
                $("#<%=frmUpload.ClientID %>").submit();
            }
        }
        function checkImage(el) {
           
            if (isCheckImageWidth && this.width > ImageWidthLimit) {
                showMessage('寬度', 'px', this.width, ImageWidthLimit);
            } else if (isCheckImageHeight && this.height > ImageHeightLimit) {
                showMessage('高度', 'px', this.height, ImageHeightLimit);
            } else if (isCheckImageSize && this.fileSize > ImageSizeLimit) {
                showMessage('檔案大小', 'kb', this.fileSize / 1000, ImageSizeLimit / 1000);
            } else {
                el.fadeOut("show");
                parent.uploading('<%=itemID %>');
                $("#<%=frmUpload.ClientID %>").submit();
              //  document.FileForm.submit();
            }
        }
        function showMessage(kind, unit, real, limit) {
            var msg = "您所選擇的圖片kind為 real unit\n超過了上傳上限 limit unit\n不允許上傳！"
            alert(msg.replace(/kind/, kind).replace(/unit/g, unit).replace(/real/, real).replace(/limit/, limit));
        }



        var uploadSelect = function(el) {
        checkFile(el);
            
            //el.fadeOut("show");
            //parent.uploading('<%=itemID %>');
            //$("#<%=frmUpload.ClientID %>").submit();
        };
        $(document).ready(function() {
        $("#bag").css('opacity', 80);  
        });
    </script>
</head>
<body>
<div id="content">
    <form runat="server" id="frmUpload" method="post" enctype="multipart/form-data">
    <div class="little_window_bg">
        <div id="main">
            <input type="file" runat="server"  id="file1" size="0" onchange="uploadSelect($(this));" />  
            <p >小於 3MB 的圖片，</p>  
            <p>僅支援 png、jpg、bmp 和 gif（無動畫）格式.</p>   
        </div> 
        </div>
       
    </form>
    </div>
</body>

</html>
