using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;

/// <summary>
/// Summary description for AspAjax
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AspAjax : System.Web.Services.WebService {
    public static string _errorMsg = "您無權限做此動作";
    public static string _getcheckNo = "-2";
    public static string _noRole = _noRole; //aaron fix 2015/5/10
    public AspAjax () {
       /* if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
           // HttpContext.Current.Response.Redirect("~/Default.aspx",false);
           //HttpContext.Current.ApplicationInstance.CompleteRequest();
               // .Write("<script> window.location.href='./Default.aspx';</script> ");
        }*/
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    public int PageMinNumFunction()
    {
        return 9;
    }
    [WebMethod]
    public List<string> IsAuthenticated()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            StaffDataBase sDB = new StaffDataBase();
            return sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        }
        else
        {
            List<string> Value = new List<string>();
            return Value;
        }
    }
    [WebMethod]
    public void Logout()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
            FormsAuthentication.SignOut();
    }
    [WebMethod]
    public string getUnitAutoNumber(string FieldName, string Unit)
    {
        CaseDataBase sDB = new CaseDataBase();
        string stuIDName = "";
        if (Unit.Length == 0)
        {
            StaffDataBase StaffDB = new StaffDataBase();
            List<string> CreateFileName = StaffDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
            Unit = CreateFileName[2];
        }
        int stuNumber = 4;
        if (FieldName == "StaffDB_")
        {
            stuNumber = 3;
        }
        string StudentDBitem = "";
        if (FieldName == "StudentDB_")
        {
            StudentDBitem = "0";
        }
        int AutoNumber = int.Parse(sDB.getUnitAutoNumber(FieldName + Unit));
        AutoNumber = AutoNumber + 1;
        stuIDName = Unit + AutoNumber.ToString().PadLeft(stuNumber, '0') + StudentDBitem;
        
        if (FieldName == "Volunteer_")
        {
            stuIDName = "62" + Unit + AutoNumber.ToString().PadLeft(3, '0');
        }
        
        return stuIDName;
    }

    [WebMethod]
    public string ManagePageRoles()
    {
        string returnValue = "";
        ManageDataBase msg = new ManageDataBase();
        string[] MembershipStaffRoles = msg.getMembershipStaffRoles(HttpContext.Current.User.Identity.Name);
        bool StaffRolesBool = false;
        foreach (string item in MembershipStaffRoles)
        {
            if (item == "4") {
                StaffRolesBool = true;
            }
        }
        if (StaffRolesBool)
        {
            returnValue = "<input type=\"button\" value=\"系統管理頁\" onclick=\"window.location.href='./manage/admin.aspx';\"/>";
            	
        }
        return returnValue;
    }

    [WebMethod]
    public string[] CreateUserMemberData(CreateStaff StaffData)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.CreateUserMemberData(StaffData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public int UpOneStaffDataBase(CreateStaff StaffData)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.UpOneStaffDataBase(StaffData);
    }
    [WebMethod]
    public string[] setCreateUserMemberPhotoData(CreateStaff StaffData)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.setCreateUserMemberPhotoData(StaffData);
    }
    [WebMethod]
    public RolesStruct getStaffRoles(string sID)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.getStaffRoles(sID);
    }
    [WebMethod]
    public RolesStruct getStaffRolesnoVar()
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.getStaffRoles();
    }
    [WebMethod]
    public StudentDataBasic getStudentAidsDataBaseBasic(string cID)
    {
        CaseDataBase SDB = new CaseDataBase();
        StudentResult stuData = SDB.getStudentDataWho(cID);
        StudentDataBasic returnValue = new StudentDataBasic();
        returnValue.ID = stuData.Column.ToString();
        returnValue.studentID = stuData.StudentData.studentID;
        returnValue.studentName = stuData.StudentData.studentName;
        returnValue.studentSex = stuData.StudentData.studentSex;
        returnValue.studentbirthday = stuData.StudentData.studentbirthday;
        returnValue.assistmanageL = stuData.HearingData.assistmanageL;
        returnValue.assistmanageR = stuData.HearingData.assistmanageR;
        returnValue.brandL = stuData.HearingData.brandL;
        returnValue.brandR = stuData.HearingData.brandR;
        returnValue.modelL = stuData.HearingData.modelL;
        returnValue.modelR = stuData.HearingData.modelR;
        returnValue.buyingtimeR = stuData.HearingData.buyingtimeR;
        returnValue.buyingtimeL = stuData.HearingData.buyingtimeL;
        returnValue.buyingPlaceR = stuData.HearingData.buyingPlaceR;
        returnValue.buyingPlaceL = stuData.HearingData.buyingPlaceL;
        returnValue.openHzDateR = stuData.HearingData.openHzDateR;
        returnValue.openHzDateL = stuData.HearingData.openHzDateL;
        returnValue.insertHospitalR = stuData.HearingData.insertHospitalR;
        returnValue.insertHospitalL = stuData.HearingData.insertHospitalL;
        returnValue.checkNo = stuData.checkNo;
        returnValue.errorMsg = stuData.errorMsg;
        return returnValue;
    }

    /* 聽力相關 */
    [WebMethod]
    public string[] createAudiometryAppointment(CreateAudiometryAppointment StructData)
    {
        Audiometry aDB = new Audiometry();
       // if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
      //  {
            return aDB.createAudiometryAppointment(StructData);
       // }
      //  else
      // // {
       ///     return new string[2] { _noRole, _errorMsg };
      //  }
    }

    [WebMethod]
    public string[] setAudiometryAppointment(CreateAudiometryAppointment StructData)
    {
        Audiometry aDB = new Audiometry();
        //if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        //{
            return aDB.setAudiometryAppointment(StructData);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }

    [WebMethod]
    public string[] delAudiometryAppointment(Int64 eventID)
    {
        Audiometry aDB = new Audiometry();
        //if (int.Parse(aDB._StaffhaveRoles[0]) == 1)
        //{
            return aDB.delAudiometryAppointment(eventID);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }

    [WebMethod]
    public string[] SearchAudiometryAppointmentDataBaseCount(SearchAudiometryAppointment SearchAudiometryAppointmentData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.SearchAudiometryAppointmentDataBaseCount(SearchAudiometryAppointmentData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<AADataList> SearchAudiometryAppointmentDataBase(int index, SearchAudiometryAppointment SearchAudiometryAppointmentData)
    {
        Audiometry aDB = new Audiometry();
        return aDB.SearchAudiometryAppointmentDataBase(index, SearchAudiometryAppointmentData);
    }

    [WebMethod]
    public string[] setAudiometryAppointmentContent(SaveAudiometryAppointmentContent AaSystemData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        {
            return aDB.setAudiometryAppointmentContent(AaSystemData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] createHearingAssessmentDataBase(CreateHearingAssessment StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.createHearingAssessmentData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    } 
    [WebMethod]
    public CreateHearingAssessment getHearingAssessmentDataBase(string sID)
    {
        Audiometry aDB = new Audiometry();
        CreateHearingAssessment returnValue = new CreateHearingAssessment();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            returnValue = aDB.getHearingAssessmentData(sID);
        }
        else
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.caseUnit != UserFile[2] && int.Parse(aDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public string[] setHearingAssessmentDataBase(CreateHearingAssessment StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        {
            return aDB.setHearingAssessmentData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchHearingAssessmentDataBaseCount(SearchHearingAssessment SearchData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.SearchHearingAssessmentDataCount(SearchData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchHearingAssessmentResult> SearchHearingAssessmentDataBase(int index,SearchHearingAssessment SearchData)
    {
        Audiometry aDB = new Audiometry();
        return aDB.SearchHearingAssessmentData(index, SearchData);
    }
    [WebMethod]
    public string[] createStudentAidsUseData(CreateStudentAidsUse StudentData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.createStudentAidsUse(StudentData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateStudentAidsUse getStudentAidsUseDataBase(string ID)
    {
        Audiometry aDB = new Audiometry();
        CreateStudentAidsUse returnValue = new CreateStudentAidsUse();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            returnValue= aDB.getStudentAidsUse(ID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public string[] setStudentAidsUseData(CreateStudentAidsUse StudentData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        {
            return aDB.setStudentAidsUse(StudentData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchStudentAidsUseBaseCount(SearchAidsUse SearchData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.SearchStudentAidsUseCount(SearchData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchAidsUseResult> SearchStudentAidsUseBase(int index, SearchAidsUse SearchData)
    {
        Audiometry aDB = new Audiometry();
        return aDB.SearchStudentAidsUse(index, SearchData);
    }
    [WebMethod]
    public CreateHearingInspection getStudentAidsUseNewDataBase(string SID)
    {
        Audiometry aDB = new Audiometry();
        return aDB.getStudentAidsUseNewData(SID);
    }
    [WebMethod]
    public string[] createHearingInspectionDataBase(CreateHearingInspection StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.createHearingInspectionData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
        
    } 
    [WebMethod]
    public CreateHearingInspection getHearingInspectionDataBase(string sID)
    {
        Audiometry aDB = new Audiometry();
        CreateHearingInspection returnValue=new CreateHearingInspection();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=aDB.getHearingInspectionData(sID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
        
    }
    [WebMethod]
    public string[] setHearingInspectionDataBase(CreateHearingInspection StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        {
            return aDB.setHearingInspectionData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public string[] SearchHearingInspectionDataBaseCount(SearchHearingAssessment SearchData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.SearchHearingInspectionDataCount(SearchData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
        
    }
    [WebMethod]
    public List<SearchHearingInspectionResult> SearchHearingInspectionDataBase(int index,SearchHearingAssessment SearchData)
    {
        Audiometry aDB = new Audiometry();
        return aDB.SearchHearingInspectionData(index, SearchData);
    }
    [WebMethod]
    public List<setHearingTests> getHearingTestDataBase(string stuID)
    {
        Audiometry aDB = new Audiometry();
        List<setHearingTests> returnValue = new List<setHearingTests>();
        if (int.Parse(aDB._StaffhaveRoles[0]) == 1)
        {
            returnValue= aDB.getHearingTestData(stuID);
        }
        else
        {
            setHearingTests addValue = new setHearingTests();
            addValue.checkNo = _noRole;
            addValue.errorMsg = _errorMsg;
            returnValue.Add(addValue);
        }
        return returnValue;
        
    }
    [WebMethod]
    public string[] setHearingTestDataBase(setHearingTests StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        {
            return aDB.setHearingTestData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchAidsManageDataBaseCount(SearchAidsManageResult SearchData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.SearchAidsManageDataCount(SearchData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchAidsManageResult> SearchAidsManageDataBase(int index, SearchAidsManageResult SearchData)
    {
        Audiometry aDB = new Audiometry();
        return aDB.SearchAidsManageData(index, SearchData);
    }
    [WebMethod]
    public string[] createAidsManageDataBase(createAidsManage StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.createAidsManageData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public createAidsManage getAidsManageDataBase(string SID)
    {
        Audiometry aDB = new Audiometry();
        createAidsManage returnValue = new createAidsManage();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=aDB.getAidsManageData(SID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public string[] setAidsManageDataBase(createAidsManage StructData)
    {
        Audiometry aDB = new Audiometry();
        return aDB.setAidsManageData(StructData);
    }
    [WebMethod]
    public string[] createLendingDataBase(createAidsManageLoan StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.createLendingData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setLendingDataBase(createAidsManageLoan StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        {
            return aDB.setLendingData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delLendingDataBase(string SID)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[0]) == 1)
        {
            return aDB.delLendingData(SID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
     public string[] createServiceDataBase(createAidsManageService StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.createServiceData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setServiceDataBase(createAidsManageService StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        {
            return aDB.setServiceData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delServiceDataBase(string SID)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[0]) == 1)
        {
            return aDB.delServiceData(SID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] createFmAssessmentDataBase(CreateFMAidsAssess StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.createFmAssessmentData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateFMAidsAssess getFmAssessmentDataBase(string SID)
    {
        Audiometry aDB = new Audiometry();
        CreateFMAidsAssess returnValue = new CreateFMAidsAssess();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=aDB.getFmAssessmentData(SID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public string[] setFmAssessmentDataBase(CreateFMAidsAssess StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        {
            return aDB.setFmAssessmentData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchFmAssessmentDataBaseCount(SearchFMAidsAssess StructData)
    {
        Audiometry aDB = new Audiometry();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.SearchFmAssessmentDataCount(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchFMAidsAssessResult> SearchFmAssessmentDataBase(int index,SearchFMAidsAssess StructData)
    {
        Audiometry aDB = new Audiometry();
        return aDB.SearchFmAssessmentData(index, StructData);
    }
    /* 教學相關 */


    [WebMethod]//教案 取得件數
    public int[] GetSingleTeachCaseCount(int studentID,string StartDate,string EndDate)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.GetSingleTeachCaseCount(studentID, StartDate, EndDate);
    }
    [WebMethod]//教案 取得ISP資料
    public List<SingleClassShortTermTarget> GetSingleTeachCase(int studentID, string StartDate, string EndDate)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.GetSingleTeachCase(studentID, StartDate, EndDate);
    }

    [WebMethod]//教案 新增
    public int CreateSingleTeachCase(SingleClassShortTerm StructData)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.CreateSingleTeachCase(StructData);
    }

    [WebMethod]//教案 取得教案資料
    public SingleClassShortTerm GetSingleTeachShortTerm(int SCSTID)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.GetSingleTeachShortTerm(SCSTID);
    }
    [WebMethod]//教案 
    public string[] SearchSingleTeachCount(SearchCaseISPRecord SearchStructure)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchSingleTeachCount(SearchStructure, 1);
    }

    [WebMethod]//教案 
    public List<SingleClassShortTerm> SearchSingleTeach(int index, SearchCaseISPRecord SearchStructure)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchSingleTeach(index, SearchStructure, 0);
    }
    [WebMethod]//教案 更新
    public int UpdateSingleTeachCase(SingleClassShortTerm StructData)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.UpdateSingleTeachCase(StructData);
    }
    [WebMethod]//課表 新增
    public int createTeacherSchudule(TeacherSchudule StructData)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.createTeacherSchudule(StructData);
        //return 1;
    }
    [WebMethod]//課表 更新
    public int UpdateTeacherSchudule(TeacherSchudule StructData)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.UpdateTeacherSchudule(StructData);
        //return 1;
    }
    [WebMethod]//課表 更新
    public int delTeacherSchudule(Int32 StructData)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.delTeacherSchudule(StructData);
        //return 1;
    }

    // createTeacherSchudule



    [WebMethod]
    public int createCoursePlanTemplate(int Unit, int ClassID, int TeacherID, int CourseID, string StartPeriod, string EndPeriod, List<string[]> targetContent)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.createCoursePlanTemplate(Unit, ClassID, TeacherID, CourseID, StartPeriod, EndPeriod, targetContent);
    }
    [WebMethod]
    public int setCoursePlanTemplate(setCoursePlan CoursePlanTemplate)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.setCoursePlanTemplate(CoursePlanTemplate);
    }
    [WebMethod]
    public int delCoursePlanTemplate(Int64 LID)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.delCoursePlanTemplate(LID);
    }
    [WebMethod]
    public List<string> SearchClassDataBaseCount(int cID, string cName, int cUnit)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.SearchClassDataBaseCount(cID, cName, cUnit);
    }
    [WebMethod]
    public List<ClassNameDataList> SearchClassDataBase(int index, int cID, string cName, int cUnit)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.SearchClassDataBase(index, cID, cName, cUnit);
    }
    [WebMethod]
    public List<string> SearchCourseDataBaseCount(int cID, string cName, int cUnit)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.SearchCourseDataBaseCount(cID, cName, cUnit);
    }
    [WebMethod]
    public List<CourseNameDataList> SearchCourseDataBase(int index, int cID, string cName, int cUnit)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.SearchCourseDataBase(index, cID, cName, cUnit);
    }
    
    [WebMethod]
    public string[] SearchCPTDataBaseCount(SearchTeachCaseItem SearchData)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.SearchCPTDataBaseCount(SearchData);
    }
    [WebMethod]
    public List<CPTDataList> SearchCPTDataBase(int index, SearchTeachCaseItem SearchData)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.SearchCPTDataBase(index, SearchData);
    }

    [WebMethod]
    public setCoursePlan getCoursePlanTemplate(string CPTID)
    {
        TeachDataBase tDB = new TeachDataBase();
        return tDB.getCoursePlan(CPTID);
    }

    [WebMethod]
    public List<CreaHearing_Loss_Tool> GetLossToolQuestion(int Category)//學前聽損幼兒教育課程檢核 Q1
    {
        AdministrationDataBase aDB = new AdministrationDataBase();

            return aDB.GetLossToolQuestion(Category);
        return null;
    }
    [WebMethod]
    public List<CreaHearing_Loss_Skill> GetLossSkillQuestion()//學前聽損幼兒教育課程檢核 Q2
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
            return aDB.GetLossSkillQuestion();
        return null;
    }

    [WebMethod]
    public string[] updateHearLossDataBase(List<UpdateHearLoss> sTemperatureData ,int SID)//學前聽損幼兒教育課程檢核 存檔
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.caseBTFunction();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.UpdateHearLoss(sTemperatureData,SID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public List<UpdateHearLoss> searchHearLossDataBase(int SID,int page)//學前聽損幼兒教育課程檢核 search by StudentID Aaron
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.searchHearLossDataBase(SID,page);
        //}
        //else
        //{
        //    return null;
        //}
    }
    [WebMethod]
    public List<SearchStudentResult> ShowStudent(int SID)//學前聽損幼兒教育課程檢核 顯示學生資料 AWho
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.ShowStudent(SID);
        //}
        //else
        //{
        //    return null;
        //}
    }
    [WebMethod]
    public List<AchievementAssessment> CreatAchievementAssessment(AchievementAssessment sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.CreatAchievementAssessment(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}
    }
    [WebMethod]
    public string[] SearchAchievementAssessmentCount(SearchStudent SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.SearchAchievementAssessmentCount(SearchStructure, 1);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchAchievementAssessment> SearchAchievementAssessment(int index, SearchStudent SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.SearchAchievementAssessment(index, SearchStructure, 0);
    }
    [WebMethod]
    public List<AchievementAssessmentLoad> ShowAchievementAssessment(int ID)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.ShowAchievementAssessment(ID);
        //}
        //else
        //{
        //    return null;
        //}
    }
    [WebMethod]
    public List<AchievementAssessment> UpdateAchievementAssessment(AchievementAssessment sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.UpdateAchievementAssessment(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}
    }


    [WebMethod]
    public List<CaseStudy> CreatCaseStudy(CaseStudy sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.CreatCaseStudy(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}
    }
    [WebMethod]
    public string[] GetWriteName()
    {
        StaffDataBase sDB = new StaffDataBase();
        List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        string[] returnValue = new string[2];
        returnValue[0] = CreateFileName[0];
        returnValue[1] = CreateFileName[1];
        return returnValue;
        //return CreateFileName;
    }
    

    [WebMethod]
    public string[] SearchCaseStudyCount(SearchStudent SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.SearchCaseStudyCount(SearchStructure, 1);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchAchievementAssessment> SearchCaseStudy(int index, SearchStudent SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.SearchCaseStudy(index, SearchStructure, 0);
    }
    [WebMethod]
    public List<AchievementAssessmentLoad> ShowCaseStudy(int ID)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[0]) == 1)
        //{
            return aDB.ShowCaseStudy(ID);
        //}
        //else
        //{
        //    return null;
        //}
    }
    [WebMethod]
    public List<CaseStudy> UpdateCaseStudy(CaseStudy sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.UpdateCaseStudy(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}
    }


    [WebMethod]
    public List<CaseISPRecord> CreatCaseISPRecord(CaseISPRecord sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.CreatCaseISPRecord(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}
    }



    [WebMethod]
    public string[] SearchCaseISPRecordCount(SearchCaseISPRecord SearchStructure)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)

        //{
            return aDB.SearchCaseISPRecordCount(SearchStructure, 1);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public List<ShowCaseISPRecord> SearchCaseISPRecord(int index, SearchCaseISPRecord SearchStructure)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchCaseISPRecord(index, SearchStructure, 0);
    }

    [WebMethod]
    public List<AchievementAssessmentLoad> ShowCaseIspRecord(int id)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.ShowCaseIspRecord(id);
    }

    [WebMethod]
    public List<CaseISPRecord> UpdateCaseISPRecord(CaseISPRecord sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.UpdateCaseISPRecord(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}
    }

    [WebMethod]
    public string[] CreatVoiceDistanceDatabase(List<VoiceDistance> sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.CreatVoiceDistanceDatabase(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}
    }

    [WebMethod]
    public string[] SearchVoiceDistanceCount(SearchStudent SearchStructure)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.SearchVoiceDistanceCount(SearchStructure, 1);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public List<VoiceDistance> SearchVoiceDistance(int index, SearchStudent SearchStructure)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchVoiceDistance(index, SearchStructure, 0);
    }
    [WebMethod]
    public string[] GetVoiceDistanceCount(int ID)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.GetVoiceDistanceCount(ID);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }

    [WebMethod]
    public List<VoiceDistance> showVoiceDistance(int ID)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.showVoiceDistance(ID);

    }
    [WebMethod]
    public string[] InsertVoiceDistance(List<VoiceDistance> sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.InsertVoiceDistance(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}
    }
    [WebMethod]
    public string[] UpdateVoiceDistance(List<VoiceDistance> sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.UpdateVoiceDistance(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}
    }

    [WebMethod]
    public string[] CreatTeachServiceSupervisor(TeachServiceSupervisor sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
            return aDB.CreatTeachServiceSupervisor(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}
    
    }


    [WebMethod]
    public string[] SearchTeachServiceSupervisorCount(SearchStudent SearchStructure)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
        return aDB.SearchTeachServiceSupervisorCount(SearchStructure, 1);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public List<TeachServiceSupervisor> SearchTeachServiceSupervisor(int index, SearchStudent SearchStructure)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchTeachServiceSupervisor(index, SearchStructure, 0);
    }


    [WebMethod]
    public List<AchievementAssessmentLoad> ShowTeachServiceSupervisor(int id)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.ShowTeachServiceSupervisor(id);
    }

    [WebMethod]
    public string[] UpdateTeachServiceSupervisor(TeachServiceSupervisor sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
        return aDB.UpdateTeachServiceSupervisor(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}

    }


    [WebMethod]
    public string[] CreateTeachServiceInspect(TeachServiceInspect sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
        return aDB.CreateTeachServiceInspect(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}

    }
    [WebMethod]
    public string[] SearchTeachServiceInspectCount(SearchStudent SearchStructure)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
        return aDB.SearchTeachServiceInspectCount(SearchStructure, 1);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public List<TeachServiceInspect> SearchTeachServiceInspect(int index, SearchStudent SearchStructure)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchTeachServiceInspect(index, SearchStructure, 1);
    }

    [WebMethod]
    public List<AchievementAssessmentLoad> ShowTeachServiceInspect(int id)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.ShowTeachServiceInspect(id);
    }
    [WebMethod]
    public string[] UpdateTeachServiceInspect(TeachServiceInspect sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        //aDB.caseBTFunction();
        //if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        //{
        return aDB.UpdateTeachServiceInspect(sTemperatureData);
        //}
        //else
        //{
        //    return null;
        //}

    }

    [WebMethod]
    public string[] SearchClassNameCountCase(SearchClassName SearchStaffCondition)//無法使用權限，會使程式部分功能喪失 add by Awho
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchClassNameCountCase(SearchStaffCondition);
    }
    [WebMethod]
    public List<SearchClassName> SearchClassNameDataBaseCase(int index, SearchClassName SearchStaffCondition)// add by Awho
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchClassNameDataBaseCase(index, SearchStaffCondition);
    }

    
    

    /*Staff****************************************************************************************/
    [WebMethod]
    public string[] SearchStaffDataBaseWorkCount(SearchStaff SearchStaffCondition)//人事管理-出勤記錄管理
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBaseWorkCount(SearchStaffCondition);
    }
    [WebMethod]
    public List<WorkRecordManagePeople> SearchStaffDataBaseWork(int index, SearchStaff SearchStaffCondition)//人事管理-出勤記錄管理
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBaseWork(index, SearchStaffCondition);
    }
    [WebMethod]
    public string[] SetWorkRecordManage(List<WorkRecordManage> SearchStaffCondition)//人事管理-出勤記錄管理
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SetWorkRecordManage(SearchStaffCondition);
    }
    [WebMethod]
    public List<WorkRecordManage> GetWorkRecordManage(WorkRecordManage SearchStaffCondition)//人事管理-出勤記錄管理
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.GetWorkRecordManage(SearchStaffCondition);
    }
    [WebMethod]
    public List<WorkRecordDetail> SearchStaffDataBaseWorkDetail(string StaffID, int Year, int Month)//人事管理-出勤記錄管理
    {
        StaffDataBase sDB = new StaffDataBase();
        return  sDB.SearchStaffDataBaseWorkDetail(StaffID,  Year, Month);
    }
    [WebMethod]
    public string[] SearchStaffDataBaseWorkAllCount(int Year , int Month,int Day)//人事管理-出勤記錄管理
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBaseWorkAllCount(Year, Month,Day);
    }
    [WebMethod]
    public List<WorkRecordAll> SearchStaffDataBaseWorkAll(int Year, int Month, int indexpage, int Day)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBaseWorkAll(Year, Month,indexpage, Day);
    }

    [WebMethod]
    public string[] AddYearVacation(YearVacationDataBase YVStructure) 
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.AddYearVacation(YVStructure);
        //return string[0];
    }
    [WebMethod]
    public List<YearVacationDataBase> GetYearVacation(string StaffID)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.GetYearVacation(StaffID);
    
    }
//  


    [WebMethod]
    public string[] SearchStaffDataBaseCountCase(SearchStaff SearchStaffCondition, string getid)//無法使用權限，會使程式部分功能喪失 add by Awho
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBaseCountCase(SearchStaffCondition, getid);
    }
    [WebMethod]
    public List<StaffDataList> SearchStaffDataBaseCase(int index, SearchStaff SearchStaffCondition, int getid)// add by Awho
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBaseCase(index, SearchStaffCondition, getid);
    }
    [WebMethod]
    public string[] SearchStaffDataBaseCount(SearchStaff SearchStaffCondition)//無法使用權限，會使程式部分功能喪失
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBaseCount(SearchStaffCondition);
    }
    [WebMethod]
    public List<StaffDataList> SearchStaffDataBase(int index, SearchStaff SearchStaffCondition)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBase(index, SearchStaffCondition);
    }
    [WebMethod]
    public StaffResult getOneStaffDataBase(string sID)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        StaffResult returnValue = new StaffResult();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=sDB.getOneStaffDataBase(sID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.StaffBaseData.unit != UserFile[2] && int.Parse(sDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public string[] SearchStaffMeritDataBaseCount(SearchStaff SearchStaffMeritCondition)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchStaffMeritDataBaseCount(SearchStaffMeritCondition);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<StaffMeritDataList> SearchStaffMeritDataBase(int index, SearchStaff SearchStaffMeritCondition)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffMeritDataBase(index, SearchStaffMeritCondition);
    }
    [WebMethod]
    public string[] createStaffMeritDataBase(List<string> MeritData, int Grade, string sID, int sUnit)
    { 
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.createStaffMeritDataBase(MeritData, Grade, sID, sUnit);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setStaffMeritDataBase(Int64 TrID, int Grade, List<string> MeritData)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1)
        {
            return sDB.setStaffMeritDataBase(TrID, Grade, MeritData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delStaffMeritDataBase(Int64 TrID)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[0]) == 1)
        {
            return sDB.delStaffMeritDataBase(TrID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] createStaffWorkData(staffWorkData WorkData)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.createStaffWorkData(WorkData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setStaffWorkData(staffWorkData WorkData)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1)
        {
            return sDB.setStaffWorkData(WorkData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delStaffWorkData(Int64 ID, string StaffID)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[0]) == 1)
        {
            return sDB.delStaffWorkData(ID, StaffID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<StaffDataList> getAllStaffDataList(List<int> item)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.getAllStaffDataList(item);
    }
    [WebMethod]
    public string[] createStaffCreditDataBase(CreateStaffUpgrade StaffUpgradeData)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.createStaffCreditData(StaffUpgradeData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
        
    }
    [WebMethod]
    public string[] SearchStaffCreditDataBaseCount(SearchStaffCredit SearchStaffUpgradeData)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchStaffCreditDataBaseCount(SearchStaffUpgradeData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateStaffUpgrade> SearchStaffCreditDataBase(int index,SearchStaffCredit SearchStaffUpgradeData)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffCreditDataBase(index, SearchStaffUpgradeData);
    }
    [WebMethod]
    public string[] setStaffCreditDataBase(CreateStaffUpgrade setStaffUpgradeData)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1)
        {
            return sDB.setStaffCreditDataBase(setStaffUpgradeData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delStaffCreditDataBase(Int64 cID)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[0]) == 1)
        {
            return sDB.delStaffCreditDataBase(cID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setStaffCreditParticipantDataBase(string cID, List<string> DelParticipantsID, List<string> NewParticipantsValue)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1)
        {
            return sDB.setStaffCreditParticipantDataBase(cID, DelParticipantsID, NewParticipantsValue);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchStaffBehaveDataBaseCount(SearchStaffBehave SearchStaffUpgrade2)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchStaffBehaveDataBaseCount(SearchStaffUpgrade2);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateStaffUpgradeSeries> SearchStaffBehaveDataBase(int index, SearchStaffBehave SearchStaffUpgradeData)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffBehaveDataBase(index, SearchStaffUpgradeData);
    }
    [WebMethod]
    public string[] createSerialDataBase(CreateStaffUpgradeSeries StaffUpgradeData)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.createSerialDataBase(StaffUpgradeData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public string[] createNewTeacherDataBase(CreateNewTeacher StaffData)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.createNewTeacherDataBase(StaffData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateNewTeacher getNewTeacherDataBase(string tID)
    {
        StaffDataBase sDB = new StaffDataBase();
        CreateNewTeacher returnValue = new CreateNewTeacher();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=sDB.getNewTeacherDataBase(tID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.Unit != UserFile[2] && int.Parse(sDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
        
    }    
    [WebMethod]
    public string[] setNewTeacherDataBase(CreateNewTeacher NewTeacherData)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1)
        {
            return sDB.setNewTeacherDataBase(NewTeacherData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchTeacherRstimateDataBaseCount(SearchTeacherRstimate SearchTeacher)
    {
        StaffDataBase sDB = new StaffDataBase();
        sDB.personnelFunction();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchTeacherRstimateDataBaseCount(SearchTeacher);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchTeacherRstimateResult> SearchTeacherRstimateDataBase(int index, SearchTeacherRstimate SearchTeacher)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchTeacherRstimateDataBase(index, SearchTeacher);
    }

    [WebMethod]
    public string[] createWorkDateDataBase(string StaffID)
    {
        StaffDataBase sDB = new StaffDataBase();
      //  sDB.attendanceFunction();
        //sDB.attendanceFunction(StaffID);
        //if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        //{
            return sDB.createWorkDateData(StaffID);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }


    /*Case-Studen個案管理********************************************************/
     [WebMethod]
     public List<SearchStudentResult> getAllStudentDataList(int item)
     {
         CaseDataBase sDB = new CaseDataBase();
         return sDB.getAllStudentDataList(item);
     }
     [WebMethod]
     public List<string> SearchStudent(string SearchString)
     {
        
         CaseDataBase sDB = new CaseDataBase();

        // string SearchString = HttpContext.Current.Request["data"];
         return sDB.SearchStudent(SearchString);
     }
     [WebMethod]
     public List<string> SearchStaff(string SearchString)
     {

         StaffDataBase sDB = new StaffDataBase();

         // string SearchString = HttpContext.Current.Request["data"];
         return sDB.SearchStaff(SearchString);
     }
     /*[WebMethod]
     public bool staffhaveRoles(string RolesObjectValue)
     { 
         bool retuenValue=false;
         RolesStruct StaffAllRoles=this.getStaffRoles(HttpContext.Current.User.Identity.Name);
         
         foreach (System.Reflection.FieldInfo fldInfo in StaffAllRoles.GetType().GetFields())
         {
             System.Diagnostics.Debug.WriteLine(fldInfo.Name + "=" + fldInfo.GetValue(StaffAllRoles));
         }
         return retuenValue;
     }*/
     [WebMethod]
     public string[] SearchHearingLossPreschoolCount(SearchStudent SearchStructure)
     {
         CaseDataBase SDB = new CaseDataBase();
         if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
         {
             return SDB.SearchHearingLossPreschoolCount(SearchStructure, 1);
         }
         else
         {
             return new string[2] { _noRole, _errorMsg };
         }
     }
     [WebMethod]
     public string[] SearchStudentDataBaseCount1(SearchStudent SearchStructure)
     {
         CaseDataBase SDB = new CaseDataBase();
         if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
         {
             return SDB.SearchStudentCount(SearchStructure,1);
         }
         else
         {
             return new string[2] { _noRole, _errorMsg };
         }
     }
    
    
     [WebMethod]
     public List<SearchStudentResult> SearchStudentDataBase1(int index, SearchStudent SearchStructure)
     {
         CaseDataBase SDB = new CaseDataBase();
         return SDB.SearchStudent(index, SearchStructure, 1);
     }
     [WebMethod]
     public string[] SearchStudentDataBaseCount(SearchStudent SearchStructure)
     {
         CaseDataBase SDB = new CaseDataBase();
         if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
         {
             return SDB.SearchStudentCount(SearchStructure, 0);
         }
         else {
             return new string[2] { _noRole, _errorMsg };
         }
     }
     [WebMethod]
     public List<SearchStudentResult> SearchStudentDataBase(int index, SearchStudent SearchStructure)
     {
         CaseDataBase SDB = new CaseDataBase();
         return SDB.SearchStudent(index, SearchStructure, 0);
     }
    [WebMethod]
    public string[] createStudentDataBase(CreateStudent SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createStudentDataBase(SearchStructure);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] createStudentDataBase3(StudentWelfareResource StudentWelfare) //學生基本資料(分頁)->身高體重-新增
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createStudentDataBase3(StudentWelfare);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] createStudentDataBase8(StudentBodyInformation StudentBody) //學生基本資料(分頁)->身高體重-新增
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createStudentDataBase8(StudentBody);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public StudentResult getStudentDataBase(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.getStudentData(ID);
    }
    [WebMethod]
    public StudentData1 getStudentDataBase1(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        StudentData1 returnValue=new StudentData1();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {

            returnValue=SDB.getStudentData1(ID);
        }
        else
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.Unit != UserFile[2] && int.Parse(SDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public StudentData2 getStudentDataBase2(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        StudentData2 returnValue = new StudentData2();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {

            returnValue = SDB.getStudentData2(ID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public List<StudentWelfareResource> getStudentDataBase3(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        List<StudentWelfareResource> returnValue = new List<StudentWelfareResource>();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {

            returnValue = SDB.getStudentData3(ID);
        }
        else
        {
            StudentWelfareResource addValue = new StudentWelfareResource();
            addValue.checkNo = _noRole;
            addValue.errorMsg = _errorMsg;
            returnValue.Add(addValue);
        }
        return returnValue;
    }
    [WebMethod]
    public StudentData4 getStudentDataBase4(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        StudentData4 returnValue = new StudentData4();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {

            returnValue = SDB.getStudentData4(ID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public StudentHearingInformation getstudentdhearInfo(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        StudentHearingInformation returnValue = new StudentHearingInformation();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {

            returnValue = SDB.getStudentHearingInfo(ID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public StudentTeachingInformation getStudentDataBase7(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        StudentTeachingInformation returnValue = new StudentTeachingInformation();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {

            returnValue = SDB.getStudentData7(ID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public List<StudentBodyInformation> getStudentDataBase8(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        List<StudentBodyInformation> returnValue = new List<StudentBodyInformation>();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {

            returnValue = SDB.getStudentData8(ID);
        }
        else
        {
            StudentBodyInformation addValue = new StudentBodyInformation();
            addValue.checkNo = _noRole;
            addValue.errorMsg = _errorMsg;
            returnValue.Add(addValue);
        }
        return returnValue;
    }
    [WebMethod]
    public string[] delStudentDataBase3(string wID)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[0]) == 1)
        {
            return SDB.delStudentData3(wID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setCreateStudentPhotoDataBase(CreateStudent StudentData) //學生基本資料(分頁)->個人基本資料
    {
        CaseDataBase SDB = new CaseDataBase();
        //if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        //{
            return SDB.setStudentPhotoData(StudentData);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod] //2015/5/10 aaron fix function name
    public string[] setStudentBaseData(CreateStudent StudentData) //學生基本資料(分頁)->個人基本資料
    {
        CaseDataBase SDB = new CaseDataBase();
        //if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        //{
            return SDB.setStudentBaseData(StudentData);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public string[] setStudentDataBase2(StudentData2 StudentData) //學生基本資料(分頁)->家庭背景資料
    {
        CaseDataBase SDB = new CaseDataBase();
        //if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        //{
            return SDB.setStudentDataBase2(StudentData);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public string[] setStudentDataBase4(StudentData4 StudentData) //學生基本資料(分頁)->家庭概況
    {
        CaseDataBase SDB = new CaseDataBase();
        //if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        //{
            return SDB.setStudentDataBase4(StudentData);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public string[] setStudentDataBase5(StudentHearingInformation StudentHearingData) //學生基本資料(分頁)->生產、發展及醫療史
    {
        Audiometry aDB = new Audiometry();
        CaseDataBase SDB = new CaseDataBase();
        //if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        //{
            return SDB.setStudentDataBase5(StudentHearingData);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public string[] setStudentDataBase6(StudentHearingInformation StudentHearingData) //學生基本資料(分頁)->聽力
    {
        Audiometry aDB = new Audiometry();
        CaseDataBase SDB = new CaseDataBase();
        //if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        //{
            return SDB.setStudentDataBase6(StudentHearingData);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public string[] setStudentDataBase7(StudentTeachingInformation StudentTeachingData) //學生基本資料(分頁)->教學
    {
        TeachDataBase aDB = new TeachDataBase();
        aDB.personnelFunction();
        CaseDataBase SDB = new CaseDataBase();
        //if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        //{
            return SDB.setStudentDataBase7(StudentTeachingData);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public string[] setStudentDataBase8(StudentBodyInformation StudentBody) //學生基本資料(分頁)->身高體重-更新
    {
        TeachDataBase aDB = new TeachDataBase();
        aDB.personnelFunction();
        CaseDataBase SDB = new CaseDataBase();
        //if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        //{
            return SDB.setStudentDataBase8(StudentBody);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public string[] delStudentDataBase8(string bID) //學生基本資料(分頁)->身高體重-更新
    {
        CaseDataBase SDB = new CaseDataBase();
        //if (int.Parse(SDB._StaffhaveRoles[0]) == 1)
        //{
            return SDB.delStudentDataBase8(bID);
        //}
        //else
        //{
        //    return new string[2] { _noRole, _errorMsg };
        //}
    }
    [WebMethod]
    public string[] creatVolunteerDataBase(CreateVolunteer VolunteerData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.creatVolunteerDataBase(VolunteerData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public string[] setVolunteerDataBase(CreateVolunteer VolunteerData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        {
            return SDB.setVolunteerDataBase(VolunteerData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateVolunteer getVolunteerDataBase(string vID)
    {
        CaseDataBase SDB = new CaseDataBase();
        CreateVolunteer returnValue = new CreateVolunteer();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=SDB.getVolunteerData(vID);
        }
        else
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.sUnit != UserFile[2] && int.Parse(SDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]                                    
    public string[] searchVolunteerDataBaseCount(SearchVolunteer SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.searchVolunteerDataCount(SearchStructure);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]                                   
    public List<CreateVolunteer> searchVolunteerDataBase(int index,SearchVolunteer SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.searchVolunteerData(index, SearchStructure);
    }
    [WebMethod]
    public string[] createVolunteerServiceDataBase(CreateVolunteerService VolunteerData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createVolunteerServiceDataBase(VolunteerData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] getVolunteerServiceDataBaseCount(string vID)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.getVolunteerServiceDataCount(vID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateVolunteer getVolunteerServiceDataBase(int index,string vID)
    {
        CaseDataBase SDB = new CaseDataBase();
        CreateVolunteer returnValue = new CreateVolunteer();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=SDB.getVolunteerServiceData(index, vID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public string[] setVolunteerServiceDataBase(CreateVolunteerService VolunteerData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        {
            return SDB.setVolunteerServiceDataBase(VolunteerData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delVolunteerServiceDataBase(string vID)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[0]) == 1)
        {
            return SDB.delVolunteerServiceData(vID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public string[] SearchStudentServiceDataBaseCount(SearchStudentService SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.SearchStudentServiceCount(SearchStructure);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchStudentServiceResult> SearchStudentServiceDataBase(int index, SearchStudentService SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.SearchStudentService(index, SearchStructure);
    }
    [WebMethod]
    public string[] createStudentServiceData(CreateStudentService StudentService)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createStudentService(StudentService);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateStudentService getStudentServiceData(Int64 ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        CreateStudentService returnValue = new CreateStudentService();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=SDB.getStudentService(ID);
        }
        else
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.sUnit != UserFile[2] && int.Parse(SDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
        
    }
    [WebMethod]
    public string[] setStudentServiceData(CreateStudentService StudentService)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        {
            return SDB.setStudentService(StudentService);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateStudentTracked getStudentDataBaseForTrack(string stuID)
    {
        CaseDataBase SDB = new CaseDataBase();
        StudentResult stuData = this.getStudentDataBase(stuID);
        CreateStudentTracked returnValue = new CreateStudentTracked();

        returnValue.studentID = stuData.StudentData.studentID;
        returnValue.studentName = stuData.StudentData.studentName;
        returnValue.studentSex = stuData.StudentData.studentSex;
        returnValue.studentbirthday = stuData.StudentData.studentbirthday;
        returnValue.email = stuData.StudentData.email;
        returnValue.address = stuData.StudentData.address;
        returnValue.addressCity = stuData.StudentData.addressCity;
        returnValue.addressZip = stuData.StudentData.addressZip;
        returnValue.manualCategory1 = stuData.StudentData.manualCategory1;
        returnValue.manualCategory2 = stuData.StudentData.manualCategory2;
        returnValue.manualCategory3 = stuData.StudentData.manualCategory3;
        returnValue.manualGrade1 = stuData.StudentData.manualGrade1;
        returnValue.manualGrade2 = stuData.StudentData.manualGrade2;
        returnValue.manualGrade3 = stuData.StudentData.manualGrade3;
        returnValue.assistmanageL = stuData.HearingData.assistmanageL;
        returnValue.assistmanageR = stuData.HearingData.assistmanageR;
        returnValue.brandL = stuData.HearingData.brandL;
        returnValue.brandR = stuData.HearingData.brandR;
        returnValue.Tel = stuData.StudentData.fPPhone2;
        returnValue.checkNo = stuData.checkNo;
        returnValue.errorMsg = stuData.errorMsg;
        return returnValue;
    }

    [WebMethod]
    public string[] SearchStudentTrackedDataBaseCount(SearchStudent SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.SearchStudentTrackedCount(SearchStructure);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchStudentTrackResult> SearchStudentTrackedDataBase(int index, SearchStudent SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.SearchStudentTracked(index, SearchStructure);
    }
    [WebMethod]
    public string[] createStudentTrackedDataBase(CreateStudentTracked StudentTracked)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createStudentTrackedDataBase(StudentTracked);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setStudentTrackedDataBase(CreateStudentTracked StudentTracked)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        {
            return SDB.setStudentTrackedDataBase(StudentTracked);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
   
    [WebMethod]
    public CreateStudentTracked getStudentTrackedDataBase(Int64 tID)
    {
        CaseDataBase SDB = new CaseDataBase();
        CreateStudentTracked returnValue = new CreateStudentTracked();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=SDB.getStudentTrackedDataBase(tID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.sUnit != UserFile[2] && int.Parse(SDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
        
    } 
    [WebMethod]
    public string[] createStudentTrackedRecord(TeackedData StudentTracked)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createStudentTrackedRecord(StudentTracked);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setStudentTrackedRecord(TeackedData StudentTracked)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        {
            return SDB.setStudentTrackedRecord(StudentTracked);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }    
    [WebMethod]
    public string[] delStudentTrackedRecord(string tID)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[0]) == 1)
        {
            return SDB.delStudentTrackedRecord(tID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public StudentBasicData getStudentDataBaseForBasic(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        StudentResult stuData = this.getStudentDataBase(ID);
        StudentBasicData returnValue = new StudentBasicData();

        returnValue.studentID = stuData.StudentData.studentID;
        returnValue.studentName = stuData.StudentData.studentName;
        returnValue.studentSex = stuData.StudentData.studentSex;
        returnValue.studentbirthday = stuData.StudentData.studentbirthday;
        returnValue.studentTWID = stuData.StudentData.studentTWID;
        returnValue.email = stuData.StudentData.email;
        returnValue.address = stuData.StudentData.address;
        returnValue.addressCity = stuData.StudentData.addressCity;
        returnValue.addressZip = stuData.StudentData.addressZip;
        returnValue.ParentName = stuData.StudentData.fPName2;
        returnValue.ParentTel = stuData.StudentData.fPHPhone2;
        returnValue.ParentPhone = stuData.StudentData.fPPhone2;
        returnValue.checkNo = stuData.checkNo;
        returnValue.errorMsg = stuData.errorMsg;
        return returnValue;
    }
    [WebMethod]
    public string[] createStudentVisitDataBase(CreateStudentView StudentTracked)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createStudentVisitData(StudentTracked);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
        
    }
    [WebMethod]
    public CreateStudentView getStudentVisitDataBase(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        CreateStudentView returnValue = new CreateStudentView();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=SDB.getStudentVisitData(ID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.caseUnit != UserFile[2] && int.Parse(SDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public string[] SearchStudentVisitDataBaseCount(SearchVisitRecord SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.SearchStudentVisitRecordCount(SearchStructure);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchVisitRecordResult> SearchStudentVisitDataBase(int index, SearchVisitRecord SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.SearchStudentVisitRecord(index, SearchStructure);
    }
    [WebMethod]
    public string[] setStudentVisitDataBase(CreateStudentView StudentTracked)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        {
            return SDB.setStudentVisitData(StudentTracked);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public string[] SearchStudentActivityDataBaseCount(SearchStudentActivity SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.SearchStudentActivityCount(SearchStructure);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchStudentActivityResult> SearchStudentActivityDataBase(int index, SearchStudentActivity SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.SearchStudentActivity(index, SearchStructure);
    }
    [WebMethod]
    public string[] createStudentActivityData(createStudentActivity StudentData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createStudentActivityData(StudentData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
        
    }
    [WebMethod]
    public string[] setStudentActivityData(createStudentActivity StudentData, List<string> DelParticipantsID, List<string> NewParticipantsValue)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        {
            return SDB.setStudentActivityData(StudentData, DelParticipantsID, NewParticipantsValue);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public createStudentActivity getStudentActivityData(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        createStudentActivity returnValue = new createStudentActivity();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=SDB.getStudentActivityData(ID);

        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.caseUnit != UserFile[2] && int.Parse(SDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }

    [WebMethod]
    public string[] createStudentTransData(CreateStudentTrans StudentData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createStudentTrans(StudentData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setStudentTransData(CreateStudentTrans StudentData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        {
            return SDB.setStudentTrans(StudentData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateStudentTrans getStudentTransDataBase(string ID)
    {
        CaseDataBase SDB = new CaseDataBase();
        CreateStudentTrans returnValue = new CreateStudentTrans();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            returnValue= SDB.getStudentTrans(ID);

        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.caseUnit != UserFile[2] && int.Parse(SDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    
    [WebMethod]
    public string[] SearchStudentTransDataBaseCount(SearchTransRecord SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.SearchStudentTransCount(SearchStructure);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchTransRecordResult> SearchStudentTransDataBase(int index, SearchTransRecord SearchStructure)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.SearchStudentTrans(index, SearchStructure);
    }
    
    /**/

    [WebMethod]
    public string[] createStudentAidData(CreateStudentAid StructData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createStudentAid(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }    
    [WebMethod]
    public string[] setStudentAidData(CreateStudentAid StructData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        {
            return SDB.setStudentAid(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateStudentAid getStudentAidData(string aID)
    {
        CaseDataBase SDB = new CaseDataBase();
        CreateStudentAid returnValue = new CreateStudentAid();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=SDB.getStudentAidData(aID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.sUnit != UserFile[2] && int.Parse(SDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
        
    }
    [WebMethod]
    public string[] searcStudentAidDataCount(SearchStudentAid SearchData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.searcStudentAidCount(SearchData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchStudentAidResult> searchStudentAidData(int index, SearchStudentAid SearchData)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.searcStudentAidt(index, SearchData);
    }    
    [WebMethod]
    public string[] searchResourceDataBaseCount(SearchResourceCard SearchData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            return SDB.SearchResourceDataCount(SearchData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchResourceCardResult> searchResourceDataBase(int index, SearchResourceCard SearchData)
    {
        CaseDataBase SDB = new CaseDataBase();
        return SDB.searchResourceDataBase(index, SearchData);
    }
    [WebMethod]
    public string[] createResourcedDataBase(CreateResourceCard StructData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[2]) == 1)
        {
            return SDB.createResourcedData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    } 
    [WebMethod]
    public CreateResourceCard getResourcedDataBase(string sID)
    {
        CaseDataBase SDB = new CaseDataBase();
        CreateResourceCard returnValue = new CreateResourceCard();
        if (int.Parse(SDB._StaffhaveRoles[3]) == 1)
        {
            returnValue = SDB.getResourcedData(sID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.sUnit != UserFile[2] && int.Parse(SDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }   
    [WebMethod]
    public string[] setResourceDataBase(CreateResourceCard StructData)
    {
        CaseDataBase SDB = new CaseDataBase();
        if (int.Parse(SDB._StaffhaveRoles[1]) == 1)
        {
            return SDB.setResourceData(StructData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    
    /*Teach-ISP********************************************************/

    [WebMethod]
    public string[] createCaseISPData(Int64 cID, CreateTeachISP StudentISP) //ISP(分頁)->個案基本資料
    {
        TeachDataBase SDB = new TeachDataBase();
        return SDB.createCaseISP(cID, StudentISP);
    }
    [WebMethod]
    public int setTeachISPDate1(setTeachISP1 StudentISP) //ISP(分頁)->個案基本資料
    {
        TeachDataBase SDB = new TeachDataBase();
        return SDB.setTeachISPPage1(StudentISP);
    }
    [WebMethod]
    public int setTeachISPDate2(List<setTeachISP2> StudentISP) //ISP(分頁)->家庭服務計畫
    {
        TeachDataBase SDB = new TeachDataBase();
        return SDB.setTeachISPPage2(StudentISP);
    }
    [WebMethod]
    public int setTeachISPDate3(setTeachISP3 StudentISP) //ISP(分頁)->聽力學管理
    {
        TeachDataBase SDB = new TeachDataBase();
        return SDB.setTeachISPPage3(StudentISP);
    }
    [WebMethod]
    public int setTeachISPDate4(setTeachISP4 StudentISP) //ISP(分頁)->教學計劃
    {
        TeachDataBase SDB = new TeachDataBase();
        return SDB.setTeachISPPage4(StudentISP);
    }

    [WebMethod]
    public setTeachISPAllData getTeachISPDate(Int64 StudentISP) //ISP(分頁)->個案基本資料
    {
        TeachDataBase SDB = new TeachDataBase();
        return SDB.getTeachISP(StudentISP);
    }

     [WebMethod]
    public List<setTeachISP2> GetHomeService(Int64 StudentISP) //ISP(分頁)->家庭服務計畫
    {
        TeachDataBase SDB = new TeachDataBase();
        return SDB.GetHomeService(StudentISP);
    }
     [WebMethod]
     public setTeachISP3 GetTeachISPPage3(Int64 StudentISP) //ISP(分頁)->聽力學
     {
         TeachDataBase SDB = new TeachDataBase();
         return SDB.GetTeachISPPage3(StudentISP);
     }
     [WebMethod]
     public int[] GetTeachISPPage4Count(Int64 StudentISP) //ISP(分頁)->教學計劃
     {
         TeachDataBase SDB = new TeachDataBase();
         return SDB.GetTeachISPPage4Count(StudentISP);
     }
     [WebMethod]
     public setTeachISP4 GetTeachISPPage4(Int64 StudentISP) //ISP(分頁)->教學計劃
     {
         TeachDataBase SDB = new TeachDataBase();
         return SDB.GetTeachISPPage4(StudentISP);
     }
    
    [WebMethod]
    public int SearchTeachISPDateCount(SearchStudentISP searchData)
    {
        TeachDataBase sDB = new TeachDataBase();
        return sDB.SearchTeachISPCount(searchData);
    }
    
    [WebMethod]
    public List<SearchStudentResult> SearchTeachISPData(int index, SearchStudentISP searchData)
    {
        TeachDataBase sDB = new TeachDataBase();
        return sDB.SearchTeachISP(index, searchData);
    }

    /*Other-********************************************************/
    [WebMethod]
    public string[] CreateRemindSystemData(CreateRemind RemindSystemData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.CreateRemindSystem(RemindSystemData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchRemindSystemDataCount(SearchRemindSystem RemindSystemData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchRemindCount(RemindSystemData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchRemindSystemResult> SearchRemindSystemData(int index, SearchRemindSystem RemindSystemData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.SearchRemind(index, RemindSystemData);
    }
    [WebMethod]
    public string[] setRemindSystemData1(SearchRemindSystemResult RemindSystemData,string vIndex)
    {
        OtherDataBase sDB = new OtherDataBase();
        SearchRemindSystemResult oldData=sDB.SearchRemindOne(RemindSystemData.rID);
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1 && oldData.designee == HttpContext.Current.User.Identity.Name && oldData.checkNo ==null)
        {
            return sDB.setRemindSystemData1(RemindSystemData);
        }
        else
        {
            return new string[4] { _getcheckNo, _errorMsg, RemindSystemData.rID, vIndex };
        }
    }
    [WebMethod]
    public string[] delRemindSystemData(Int64 rID)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.delRemindSystemData(rID);
    }
    [WebMethod]
    public List<SearchRemindSystemResult> getMyselfRemindSystemData()
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.getMyselfRemindSystemData(HttpContext.Current.User.Identity.Name);
    }
    [WebMethod]
    public string[] setRemindSystemData2(SearchRemindSystemResult RemindSystemData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.setRemindSystemData2(RemindSystemData);
    }

    /* 文具管理 */
    [WebMethod]
    public string[] createstationeryDataBase(CreateStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.createstationeryDataBase(stationeryData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchStationeryDataCount(SearchStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchStationeryCount(stationeryData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateStationery> SearchStationeryData(int index, SearchStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.SearchStationery(index, stationeryData);
    }
    [WebMethod]
    public string[] setStationeryData1(SearchStationeryResult stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1)
        {
            return sDB.setStationeryData1(stationeryData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delStationeryData(Int64 sID)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[0]) == 1)
        {
            return sDB.delStationeryData(sID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public string[] SearchStationeryResultCount(SearchStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchStationeryResultCount(stationeryData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateStationery> SearchStationeryResult(int index, SearchStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.SearchStationeryResult(index, stationeryData);
    }
    [WebMethod]
    public string[] createPurchaseDataBase(CreatePurchase purchaseData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.createPurchaseDataBase(purchaseData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchPurchaseDataCount(SearchPurchase purchaseData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchPurchaseCount(purchaseData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreatePurchase> SearchPurchaseData(int index, SearchPurchase purchaseData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.SearchPurchase(index, purchaseData);
    }
    [WebMethod]
    public string[] setPurchaseData1(SearchPurchaseResult purchaseDataResult)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1)
        {
            return sDB.setPurchaseData1(purchaseDataResult);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delPurchaseData(Int64 pID)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[0]) == 1)
        {
            return sDB.delPurchaseData(pID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchStationeryResultCount2(SearchStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchStationeryResultCount(stationeryData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateStationery> SearchStationeryResult2(int index, SearchStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.SearchStationeryResult(index, stationeryData);
    }
    [WebMethod]
    public string[] createReceiveDataBase(CreateReceive receiveData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.createReceiveDataBase(receiveData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchReceiveDataCount(SearchReceive receiveData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchReceiveCount(receiveData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateReceive> SearchReceiveData(int index, SearchReceive receiveData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.SearchReceive(index, receiveData);
    }
    [WebMethod]
    public string[] setReceiveData1(SearchReceiveResult receiveDataResult)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1)
        {
            return sDB.setReceiveData1(receiveDataResult);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delReceiveData(Int64 rID)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[0]) == 1)
        {
            return sDB.delReceiveData(rID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchStaffDataBaseCount2(SearchStaff SearchStaffCondition)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBaseCount(SearchStaffCondition);//不能加搜尋權限
    }
    [WebMethod]
    public List<StaffDataList> SearchStaffDataBase2(int index, SearchStaff SearchStaffCondition)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBase(index, SearchStaffCondition);
    }
    [WebMethod]
    public string[] SearchStationeryResultCount3(SearchStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchStationeryResultCount(stationeryData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateStationery> SearchStationeryResult3(int index, SearchStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.SearchStationeryResult(index, stationeryData);
    }
    [WebMethod]
    public string[] createScrapDataBase(CreateScrap scrapData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.createScrapDataBase(scrapData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchScrapDataCount(SearchScrap scrapData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchScrapCount(scrapData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateScrap> SearchScrapData(int index, SearchScrap scrapData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.SearchScrap(index, scrapData);
    }
    [WebMethod]
    public string[] setScrapData1(SearchScrapResult ScrapDataResult)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1)
        {
            return sDB.setScrapData1(ScrapDataResult);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delScrapData(Int64 sID)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[0]) == 1)
        {
            return sDB.delScrapData(sID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] createReturnDataBase(CreateReturn returnData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[2]) == 1)
        {
            return sDB.createReturnDataBase(returnData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] SearchStaffDataBaseCount3(SearchStaff SearchStaffCondition)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBaseCount(SearchStaffCondition);
    }
    [WebMethod]
    public List<StaffDataList> SearchStaffDataBase3(int index, SearchStaff SearchStaffCondition)
    {
        StaffDataBase sDB = new StaffDataBase();
        return sDB.SearchStaffDataBase(index, SearchStaffCondition);
    }
    [WebMethod]
    public string[] SearchStationeryResultCount4(SearchStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchStationeryResultCount(stationeryData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateStationery> SearchStationeryResult4(int index, SearchStationery stationeryData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.SearchStationeryResult(index, stationeryData);
    }
    [WebMethod]
    public string[] SearchReturnDataCount(SearchReturn returnData)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[3]) == 1)
        {
            return sDB.SearchReturnCount(returnData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateReturn> SearchReturnData(int index, SearchReturn returnData)
    {
        OtherDataBase sDB = new OtherDataBase();
        return sDB.SearchReturn(index, returnData);
    }
    [WebMethod]
    public string[] setReturnData1(SearchReturnResult ReturnDataResult)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[1]) == 1)
        {
            return sDB.setReturnData1(ReturnDataResult);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delReturnData(Int64 rID)
    {
        OtherDataBase sDB = new OtherDataBase();
        if (int.Parse(sDB._StaffhaveRoles[0]) == 1)
        {
            return sDB.delReturnData(rID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }


    /* 圖書管理 */
    [WebMethod]
    public List<string[]> getClassificationData()
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.getClassification();
    }
    [WebMethod]
    public string[] createBookDataBase(CreateBook bookData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.createBookDataBase(bookData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
      [WebMethod]
    public string[] GetBookIDData(CreateBook bookData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.GetBookIDData(bookData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    
    [WebMethod]
    public string[] searchBookDataCount(SearchBook bookData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.searchBookCount(bookData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateBook> searchBookData(int index, SearchBook bookData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        return aDB.searchBook(index, bookData);
    }
    [WebMethod]
    public string[] setBookData1(SearchBookResult bookDataResult)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        if (int.Parse(aDB._StaffhaveRoles[1]) == 1)
        {
            return aDB.setBookData1(bookDataResult);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delBookData(Int64 bID)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        if (int.Parse(aDB._StaffhaveRoles[0]) == 1)
        {
            return aDB.delBookData(bID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }


    [WebMethod]
    public string[] searchUserData(SearchUser userData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.searchUserData(userData);
    }

    [WebMethod]
    public string[] searchUserDataCardNum(SearchUser userData)//Creat BY Who
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.searchUserDataCardNum(userData);
    }

    [WebMethod]
    public List<CreateBookSystem> createBookSystemDataBase(CreateBookSystem bookSystemData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        List<CreateBookSystem> returnValue = new List<CreateBookSystem>();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            returnValue=aDB.createBookSystemDataBase(bookSystemData);
        }
        else
        {
            CreateBookSystem addValue = new CreateBookSystem();
            addValue.checkNo = _noRole;
            addValue.errorMsg = _errorMsg;
            returnValue.Add(addValue);
        }
        return returnValue;
    }
    [WebMethod]
    public List<CreateBookSystem> setBookReturnDataBase(CreateBookSystem bookSystemData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        List<CreateBookSystem> returnValue = new List<CreateBookSystem>();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=aDB.setBookReturnDataBase(bookSystemData);

        }
        else
        {
            CreateBookSystem addVlaue = new CreateBookSystem();
            addVlaue.checkNo = _noRole;
            addVlaue.errorMsg = _errorMsg;
            returnValue.Add(addVlaue);
        }
        return returnValue;
        
    }
    [WebMethod]
    public string[] searchBookDayDataCount(SearchBookStatistics BookStatisticsData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.SearchBookDayCount(BookStatisticsData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateBookSystem> searchBookDayData(int index, SearchBookStatistics BookStatisticsData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchBookDay(index, BookStatisticsData);
    }
    [WebMethod]
    public List<CreateBookBorrow> searchBookDateData(SearchBookStatistics BookStatisticsData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        List<CreateBookBorrow> returnValue = new List<CreateBookBorrow>();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            returnValue = aDB.SearchBookDate(BookStatisticsData);
        }
        else
        {
            CreateBookBorrow addValue=new CreateBookBorrow();
            addValue.checkNo = _noRole;
            addValue.errorMsg = _errorMsg;
            returnValue.Add(addValue);
        }
        return returnValue;
    }
    [WebMethod]
    public string[] searchBookDataResultCount(SearchBook bookData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.searchBookResultCount(bookData);
        }
        else
        {
            return new string[2] { _getcheckNo, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateBookResult> searchBookDataResult(int index, SearchBook bookData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.searchBookResult(index, bookData);
    }
    [WebMethod]
    public string[] searchBookRecordDataCount(SearchBookStatistics BookStatisticsData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.SearchBookRecordCount(BookStatisticsData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateBookSystem> searchBookRecordData(int index, SearchBookStatistics BookStatisticsData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchBookRecord(index, BookStatisticsData);
    }
    [WebMethod]
    public string[] searchBookRecordBorrowerDataCount(SearchBookStatistics BookStatisticsData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.libraryFunction();
        if (int.Parse(aDB._StaffhaveRoles[3]) == 1)
        {
            return aDB.SearchBookRecordBorrowerCount(BookStatisticsData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<CreateBookRecordBorrower> searchBookRecordBorrowerData(int index, SearchBookStatistics BookStatisticsData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        return aDB.SearchBookRecordBorrower(index, BookStatisticsData);
    }


    /* 體溫系統 */
    [WebMethod]
    public string[] updateStudentTemperatureDataBase(List<CreateTemperatureSystem> sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.caseBTFunction();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.updateStudentTemperatureData(sTemperatureData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public List<CreateTemperatureSystem> getStudentTemperatureData(string studentID, string Year, string Month)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.caseBTFunction();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.getStudentTemperatureData(studentID, Year, Month);
        }
        return null;
    }

    [WebMethod]
    public string[] createStudentTemperatureDataBase(CreateTemperatureSystem temperatureDataSystem)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.caseBTFunction();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.createStudentTemperatureData(temperatureDataSystem);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public List<CreateTeacherSystem> getTeacherTemperatureData(string StaffID, string Year, string Month)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.caseBTFunction();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.getTeacherTemperatureData(StaffID, Year, Month);
        }
        return null;
    }
    [WebMethod]
    public string[] updateTeacherTemperatureDataBase(List<CreateTeacherSystem> sTemperatureData)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.caseBTFunction();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.updateTeacherTemperatureData(sTemperatureData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }




    [WebMethod]
    public string[] createTeacherTemperatureDataBase(CreateTemperatureSystem temperatureDataSystem)
    {
        AdministrationDataBase aDB = new AdministrationDataBase();
        aDB.teachBTFunction();
        if (int.Parse(aDB._StaffhaveRoles[2]) == 1)
        {
            return aDB.createTeacherTemperatureData(temperatureDataSystem);
        }
        else
        {
            return new string[2] { _getcheckNo , _errorMsg };
        }
    }



    /* 財產管理 */
    [WebMethod]
    public string[] createApplyPropertyDataBase(CreateApplyProperty applyPropertyData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.applyFunction();
        if (int.Parse(pDB._StaffhaveRoles[2]) == 1)
        {
            return pDB.createApplyPropertyData(applyPropertyData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] searchApplyPropertyDataBaseCount(SearchApplyProperty searchApplyData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.applyFunction();
        if (int.Parse(pDB._StaffhaveRoles[3]) == 1)
        {
            return pDB.SearchApplyPropertyDataCount(searchApplyData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchApplyPropertyResult> searchApplyPropertyDataBase(int index, SearchApplyProperty searchApplyData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        return pDB.SearchApplyPropertyData(index, searchApplyData);
    }
    [WebMethod]
    public CreateApplyProperty getApplyPropertyDataBase(string ID)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        CreateApplyProperty returnValue = new CreateApplyProperty();
        pDB.applyFunction();
        if (int.Parse(pDB._StaffhaveRoles[3]) == 1)
        {
            returnValue=pDB.getApplyPropertyDataBase(ID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.Unit != UserFile[2] && int.Parse(pDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
        
    }
    [WebMethod]
    public string[] setApplyPropertyDataBase(CreateApplyProperty applyPropertyData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.applyFunction();
        if (int.Parse(pDB._StaffhaveRoles[1]) == 1)
        {
            return pDB.setApplyPropertyDataBase(applyPropertyData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setApplyPropertyDetail(PropertyDetailData PropertyDetail)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.applyFunction();
        if (int.Parse(pDB._StaffhaveRoles[1]) == 1)
        {
            return pDB.setApplyPropertyDetail(PropertyDetail);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }    
    [WebMethod]
    public string[] delApplyPropertyDetail(string tID)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.applyFunction();
        if (int.Parse(pDB._StaffhaveRoles[0]) == 1)
        {
            return pDB.delApplyPropertyDetail(tID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public string[] printApplyPropertyDataBase(string tID)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        CreateApplyProperty getData =pDB.getApplyPropertyDataBase(tID);
        pDB.applyFunction();
        if (int.Parse(pDB._StaffhaveRoles[3]) == 1 && getData.applyByID == HttpContext.Current.User.Identity.Name)
        {
            return pDB.printApplyPropertyDataBase(tID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    [WebMethod]
    public string[] searchApplyPropertyDataBaseCount2(SearchApplyProperty searchApplyData)
    {
        //SearchApplyProperty searchApplyData = new SearchApplyProperty();
        searchApplyData.txtapplyType = "1";
        searchApplyData.txtapplyStatus = "1";
        PropertyDataBase pDB = new PropertyDataBase();
        return pDB.SearchApplyPropertyDataCount(searchApplyData);
    }
    [WebMethod]
    public List<SearchApplyPropertyResult> searchApplyPropertyDataBase2(int index, SearchApplyProperty searchApplyData)
    {
        //SearchApplyProperty searchApplyData = new SearchApplyProperty();
        searchApplyData.txtapplyType = "1";
        searchApplyData.txtapplyStatus = "1";
        PropertyDataBase pDB = new PropertyDataBase();
        return pDB.SearchApplyPropertyData(index, searchApplyData);
    }
    [WebMethod]
    public List<PropertyDetailData> getPropertyDetailDataBase(string ID)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        return pDB.getPropertyDetailDataBase(ID);
    }
    [WebMethod]
    public List<string[]> getPropertyCategoryData()
    {
        PropertyDataBase pDB = new PropertyDataBase();
        return pDB.getPropertyCategory();
    }
    [WebMethod]
    public List<string[]> getPropertyLocationData(string unit)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        return pDB.getPropertyLocation(unit);
    }
    [WebMethod]
    public List<string[]> getPropertyCustodyData(string unit)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        return pDB.getPropertyCustody(unit);
    }

    [WebMethod]
    public string[] createPropertyRecordDataBase(CreatePropertyRecord propertyRecordData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.propertyFunction();
        if (int.Parse(pDB._StaffhaveRoles[2]) == 1)
        {
            return pDB.createPropertyRecordData(propertyRecordData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setPropertyAnnexDataBase(CreatePropertyRecord propertyRecordData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.propertyFunction();
        if (int.Parse(pDB._StaffhaveRoles[1]) == 1)
        {
            return pDB.setPropertyAnnexDataBase(propertyRecordData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setPropertyRecordDataBase(CreatePropertyRecord propertyRecordData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.propertyFunction();
        if (int.Parse(pDB._StaffhaveRoles[1]) == 1)
        {
            return pDB.setPropertyRecordDataBase(propertyRecordData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setPropertyAnnexDataBase2(CreatePropertyRecord propertyRecordData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.propertyFunction();
        if (int.Parse(pDB._StaffhaveRoles[1]) == 1)
        {
            return pDB.setPropertyAnnexDataBase(propertyRecordData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreatePropertyRecord getPropertyRecordDataBase(string ID)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        CreatePropertyRecord returnValue = new CreatePropertyRecord();
        pDB.propertyFunction();
        if (int.Parse(pDB._StaffhaveRoles[3]) == 1)
        {
            returnValue= pDB.getPropertyRecordDataBase(ID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.Unit != UserFile[2] && int.Parse(pDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
        
    }
    [WebMethod]
    public string[] createPropertyChangesRecord(PropertyChangesExplainData propertyChangesData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.propertyFunction();
        if (int.Parse(pDB._StaffhaveRoles[2]) == 1)
        {
            return pDB.createPropertyChangesRecord(propertyChangesData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] setPropertyChangesRecord(PropertyChangesExplainData propertyChangesData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.propertyFunction();
        if (int.Parse(pDB._StaffhaveRoles[1]) == 1)
        {
            return pDB.setPropertyChangesRecord(propertyChangesData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delPropertyChangesRecord(string tID)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.propertyFunction();
        if (int.Parse(pDB._StaffhaveRoles[0]) == 1)
        {
            return pDB.delPropertyChangesRecord(tID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] searchPropertyRecordDataBaseCount(SearchPropertyRecord searchRecordData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        pDB.propertyFunction();
        if (int.Parse(pDB._StaffhaveRoles[3]) == 1)
        {
            return pDB.SearchPropertyRecordDataCount(searchRecordData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchPropertyRecordResult> searchPropertyRecordDataBase(int index, SearchPropertyRecord searchRecordData)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        return pDB.SearchPropertyRecordData(index, searchRecordData);
    }

    [WebMethod]
    public string[] OutPropertyRecordDataBase(string rID, string unit)
    {
        PropertyDataBase pDB = new PropertyDataBase();
        StaffDataBase sDB = new StaffDataBase();
        ManageDataBase msg = new ManageDataBase();
        string[] MembershipStaffRoles = msg.getMembershipStaffRoles(HttpContext.Current.User.Identity.Name);
        string StaffRoles = string.Join(",",MembershipStaffRoles);
        int aa = StaffRoles.IndexOf("15");
        int aab = StaffRoles.IndexOf("4");
        if (StaffRoles.IndexOf("15") > -1 || StaffRoles.IndexOf("4") > -1)
        {
            return pDB.OutPropertyRecordData(rID, unit);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }

    /*薪資管理*/

    [WebMethod]
    public string[] SearchExternalTeacherDataBaseCount(SearchExternal SearchStaffCondition)
    {

        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[3]) == 1)
        {
            return smDB.SearchExternalTeacherDataBaseCount(SearchStaffCondition);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<SearchExternalResult> SearchExternalTeacherDataBase(int index, SearchExternal SearchStaffCondition)
    {
        SalaryManagement smDB = new SalaryManagement();
        return smDB.SearchExternalTeacherDataBase(index, SearchStaffCondition);
    }

    [WebMethod]
    public string[] createExternalTeacherWorkDataBase(ExternalWorkData SearchTeacher)
    {
        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[2]) == 1)
        {
            return smDB.createExternalTeacherWorkDataBase(SearchTeacher);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] delExternalTeacherDataBase(string eID)
    {
        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[0]) == 1)
        {
            return smDB.delExternalTeacherDataBase(eID);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }  
    [WebMethod]
    public string[] createExternalTeacherData(CreateExternal StaffData)
    {
        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[2]) == 1)
        {
            return smDB.createExternalTeacherData(StaffData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateExternal getExternalTeacherDataBase(string sID)
    {
        SalaryManagement smDB = new SalaryManagement();
        CreateExternal returnValue = new CreateExternal();
        if (int.Parse(smDB._StaffhaveRoles[3]) == 1)
        {

            returnValue = smDB.getExternalTeacherDataBase(sID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
        
    }
    [WebMethod]
    public string[] setExternalTeacherDataBase(CreateExternal StaffData)
    {
        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[1]) == 1)
        {
            return smDB.setExternalTeacherDataBase(StaffData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public string[] createStaffContractedSalaryDataBase(CreateStaffContractedSalary structData)
    {
        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[2]) == 1)
        {
            return smDB.createStaffContractedSalaryData(structData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateStaffContractedSalary getStaffContractedSalaryDataBase(string cID)
    {
        SalaryManagement smDB = new SalaryManagement();
        CreateStaffContractedSalary returnValue = new CreateStaffContractedSalary();
        if (int.Parse(smDB._StaffhaveRoles[3]) == 1)
        {
            returnValue= smDB.getStaffContractedSalaryData(cID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.Unit != UserFile[2] && int.Parse(smDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public string[] setStaffContractedSalaryDataBase(CreateStaffContractedSalary structData)
    {
        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[1]) == 1)
        {
            return smDB.setStaffContractedSalaryData(structData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<string[]> getStaffSalaryexplanationDataBase(string staffID)
    {
        SalaryManagement smDB = new SalaryManagement();
        return smDB.getStaffSalaryexplanationData(staffID);
    }
    [WebMethod]
    public CreateStaffContractedSalary getStaffContractedSalaryLatestDataBase(string staffID,string LimitDate,string nowID)
    {
        SalaryManagement smDB = new SalaryManagement();
        CreateStaffContractedSalary returnValue = new CreateStaffContractedSalary();
        if (int.Parse(smDB._StaffhaveRoles[3]) == 1)
        {
            returnValue = smDB.getStaffContractedSalaryLatestData(staffID, LimitDate, nowID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.Unit != UserFile[2] && int.Parse(smDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
        
    }
    [WebMethod]
    public CreateStaffContractedSalary getStaffContractedSalaryLatestDataBase1(string staffID, string LimitDate, string nowID)
    {
        return this.getStaffContractedSalaryLatestDataBase( staffID, LimitDate, nowID);
    }
    [WebMethod]
    public string[] SearchStaffContractedSalaryBaseCount(SearchStaff SearchStaffCondition)
    {
        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[3]) == 1)
        {
            return smDB.SearchStaffContractedSalaryBaseCount(SearchStaffCondition);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<StaffDataList> SearchStaffContractedSalaryDataBase(int index, SearchStaff SearchStaffCondition)
    {
        SalaryManagement smDB = new SalaryManagement();
        return smDB.SearchStaffContractedSalaryData(index, SearchStaffCondition);
    }
    [WebMethod]
    public string[] createStaffSalaryDataBase(CreateStaffSalary structData)
    {
        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[2]) == 1)
        {
            return smDB.createStaffSalaryData(structData);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public CreateStaffSalary getStaffSalaryDataBase(string sID)
    {
        SalaryManagement smDB = new SalaryManagement();
        CreateStaffSalary returnValue = new CreateStaffSalary();
        if (int.Parse(smDB._StaffhaveRoles[3]) == 1)
        {
            returnValue= smDB.getStaffSalaryData(sID);
        }
        else
        {
            returnValue.checkNo = _noRole;
            returnValue.errorMsg = _errorMsg;
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (returnValue.Unit != UserFile[2] && int.Parse(smDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            returnValue.checkNo = _getcheckNo;
            returnValue.errorMsg = _errorMsg;
        }
        return returnValue;
    }
    [WebMethod]
    public string[] setStaffSalaryDataBase(CreateStaffSalary structData, List<string> DelAddItem, List<string> DelMinusItem)
    {
        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[1]) == 1)
        {
            return smDB.setStaffSalaryData(structData, DelAddItem, DelMinusItem);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    //
    [WebMethod]
    public string[] SearchStaffSalaryBaseCount(SearchStaff SearchStaffCondition)
    {
        SalaryManagement smDB = new SalaryManagement();
        if (int.Parse(smDB._StaffhaveRoles[3]) == 1)
        {
            return smDB.SearchStaffSalaryBaseCount(SearchStaffCondition);
        }
        else
        {
            return new string[2] { _noRole, _errorMsg };
        }
    }
    [WebMethod]
    public List<StaffDataList> SearchStaffSalaryDataBase(int index, SearchStaff SearchStaffCondition)
    {
        SalaryManagement smDB = new SalaryManagement();
        return smDB.SearchStaffSalaryData(index, SearchStaffCondition);
    }

    /*後台*/
    [WebMethod]
    public List<StaffDataList> getMembershipStaffDataList()
    {
        ManageDataBase sDB = new ManageDataBase();
        return sDB.getMembershipStaffDataList();
    }
    [WebMethod]
    public List<StaffDataList> getNoMembershipStaffDataList()
    {
        ManageDataBase sDB = new ManageDataBase();
        return sDB.getNoMembershipStaffDataList();
    }

    [WebMethod]
    public int changePWStaffMember(string sID, string sPassword)
    {
        //要加--判斷管理者帳號權限
        int returnvalue = 0;
        var UserName = System.Web.Security.Membership.GetUser(sID);
        if (UserName != null)
        {
            try
            {
                bool ChkP = UserName.ChangePassword(UserName.GetPassword(), sPassword);
                if (ChkP)
                {
                    returnvalue = 1;
                }
            }
            catch (Exception e)
            {
                returnvalue = -1;
            }
        }
        else
        {
            returnvalue = -2;
        }
        return returnvalue;
    }
    [WebMethod]
    public string CreateStaffMember(string sID, string sPassword, string sEmail)
    {
        //要加--判斷管理者帳號權限
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();//引用stopwatch物件

        sw.Reset();//碼表歸零

        sw.Start();//碼表開始計時

        StaffDataBase sDB = new StaffDataBase();
        string CreateMessage = string.Empty;
        object ouser = null;
        MembershipCreateStatus msCrCreateStatus;
        try
        {
            Membership.CreateUser(
                sID, sPassword, sEmail, null,
                null, true, ouser, out msCrCreateStatus);

            CreateMessage = msCrCreateStatus.ToString();
        }
        catch (Exception ee)
        {
            CreateMessage = ee.ToString();
        }
        sw.Stop();//碼錶停止

        //印出所花費的總豪秒數

        string result1 = sw.Elapsed.TotalMilliseconds.ToString();
        return CreateMessage;
    }
    [WebMethod]
    public string[] getSalaryValue()
    {
        ManageDataBase mDB = new ManageDataBase();
        return mDB.getSalaryValue();
    }
    [WebMethod]
    public string[] createSalaryValue(string Value)
    {
        ManageDataBase mDB = new ManageDataBase();
        return mDB.createSalaryValue(Value);
    }
    [WebMethod]
    public string[] createAidsBrandList(CreateAidsBrand structValue)
    {
        ManageDataBase mDB = new ManageDataBase();
        return mDB.createAidsBrandList(structValue);
    }
    [WebMethod]
    public string[] setAidsBrandList(CreateAidsBrand structValue)
    {
        ManageDataBase mDB = new ManageDataBase();
        return mDB.setAidsBrandList(structValue);
    }
    [WebMethod]
    public string[] delAidsBrandList(string sID)
    {
        ManageDataBase mDB = new ManageDataBase();
        return mDB.delAidsBrandList(sID);
    }
    [WebMethod]
    public AidsTypestruct getAidsBrandList()
    {
        ManageDataBase mDB = new ManageDataBase();
        return mDB.getAidsBrandList();
    }
    [WebMethod]
    public List<string[]> getRolesDataList()
    {
        ManageDataBase mDB = new ManageDataBase();
        return mDB.getRolesDataList();
    }
    [WebMethod]
    public string[] setStaffRolesData(List<string> RolesList,string sID)
    {
        ManageDataBase mDB = new ManageDataBase();
        return mDB.setStaffRolesData(RolesList, sID);
    }
}

