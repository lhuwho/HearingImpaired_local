using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class property_record : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Controler myControler = new Controler();
        myControler.setStartControl();

        sUnit.InnerHtml = myControler._Staff_UnitName;
        loginInfo.InnerHtml = myControler._Header_Info_innerString;
        string Unit = "";
        if (!string.IsNullOrEmpty(Context.Request.QueryString["id"]))
        {
            AspAjax Ajax = new AspAjax();
            CreatePropertyRecord myRecord = Ajax.getPropertyRecordDataBase(Context.Request.QueryString["id"].ToString());
            Unit = myRecord.Unit;
        }
        else
        {
            Unit = "0";
        }
        List<string[]> Location = myControler.PropertyLocation(Unit);
        propertyLocation.Items.Add(new ListItem("請選擇", "0"));
        foreach (string[] atom in Location)
        {
            //inner += "<option value='" + atom[0] + "'>" + atom[1] + "</option>";
            propertyLocation.Items.Add(new ListItem(atom[1], atom[0]));
            gosrhLocation.Items.Add(new ListItem(atom[1], atom[0]));
        }

        List<string[]> CustodyData = myControler.getPropertyCustodyData(Unit);
        propertyCustody.Items.Add(new ListItem("請選擇", "0"));
        foreach (string[] atom in CustodyData)
        {
            //inner += "<option value='" + atom[0] + "'>" + atom[1] + "</option>";
            propertyCustody.Items.Add(new ListItem(atom[1], atom[0]));
            gosrhCustody.Items.Add(new ListItem(atom[1], atom[0]));
        }
        
        //
        // _uId = result[0];
        // _uName = result[1];
        // _uUnit = parseInt(result[2], 10);
        /*
        $("#Unit").html(_UnitList[_uUnit]);
        isLogin = true;
        var today = new Date();
        var weekArray = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
        $("#loginInfo").html("今天是" + (today.getFullYear() - 1911) + "年" + (today.getMonth() + 1) + "月" + today.getDate() + "日 " + weekArray[today.getDay()] + "　歡迎 " + _uName + " 使用本系統　　<a href='#' id='logout'>登出</a>");
        $("#logout").click(function() {
            AspAjax.Logout(SucceededCallbackAll);
        });
        
        AspAjax.getStaffRoles(result[0],SucceededCallbackAll);
         * */

    }
}
