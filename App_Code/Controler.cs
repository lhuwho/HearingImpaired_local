using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

/// <summary>
/// Summary description for Controler
/// </summary>
public class Controler
{
    const int _Index_ID = 0;
    const int _Index_Name = 1;
    const int _Index_Unit = 2;
    const string _Error_Alert = "-1";
    public string _Staff_ID="";
    public string _Staff_Name="";
    public string _Staff_UnitID="";
    public string _Staff_UnitName = "";
    public string _Header_Info_innerString = "";
	public Controler( )
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void setStartControl()
    {
        AspAjax Ajax = new AspAjax();
        List<string> StaffBaseData = Ajax.IsAuthenticated();
        if (StaffBaseData.Count == 0)
        {
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        }
        else if (StaffBaseData[_Index_ID] != _Error_Alert)
        {
            loginsuccess(StaffBaseData);
        }
        else {
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        }

        

        /*
        if (!(result == null || result.length == 0 || result == undefined)) {
                var inner = '<option value="0">請選擇</option>';
                $("select[name='propertyLocation']").find("option").remove();
                for (var i = 0; i < result.length; i++) {
                    inner += '<option value="' + result[i][0] + '">' + result[i][1] + '</option>';
                }
                $("select[name='propertyLocation']").html(inner);
            }
         */
    }
    public List<string[]> PropertyLocation(string unit)
    {
       
        PropertyDataBase pDB = new PropertyDataBase();
        List<string[]> Location = pDB.getPropertyLocation(unit);

        return Location;
    }
    public List<string[]> getPropertyCustodyData(string unit)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        return pDB.getPropertyCustody(unit);
    }
    public void loginsuccess(List<string> result) {
        _Staff_ID = result[_Index_ID];
        _Staff_Name = result[_Index_Name];
        _Staff_UnitID = result[_Index_Unit];
        StaffDataBase myDataBase = new StaffDataBase();
        _Staff_UnitName = myDataBase.getUnitName(_Staff_UnitID);
        _Header_Info_innerString = getLoginInfoString();
       
    }
    private string getLoginInfoString()
    {
        string inner = "";
        DateTime now = DateTime.Now;
        string DOW = now.DayOfWeek.ToString().ToLower();
        if (DOW == "monday")
        {
            DOW = "星期一";
        }
        else if (DOW == "tuesday")
        {
            DOW = "星期二";
        }
        else if (DOW == "wednesday")
        {
            DOW = "星期三";
        }
        else if (DOW == "thursday")
        {
            DOW = "星期四";
        }
        else if (DOW == "friday")
        {
            DOW = "星期五";
        }
        else if (DOW == "saturday")
        {
            DOW = "星期六";
        }
        else if (DOW == "sunday")
        {
            DOW = "星期日";
        }
        inner = "今天是" + (now.Year - 1911) + "年" + now.Month + "月" + now.Day + "日 " + DOW +
            "　歡迎 " + _Staff_Name + " 使用本系統　　<a href='javascript:logout()' id='logout'>登出</a>";
       // $("#loginInfo").html("今天是" + (today.getFullYear() - 1911) + "年" + (today.getMonth() + 1) + "月" + today.getDate() + "日 " + 
    //weekArray[today.getDay()] + "　歡迎 " + _uName + " 使用本系統　　<a href='#' id='logout'>登出</a>");
        return inner;
    }
}
