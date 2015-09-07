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
/// Summary description for DataStructure
/// </summary>
public class DataStructure
{
	public DataStructure()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
}
//----------------------------------------search Structure---------------------------
public struct SearchStaff
{
    public string txtstaffID;
    public string txtstaffJob;
    public string txtstaffName;
    public string txtstaffSex;
    public string txtstaffUnit;
    public string txtstaffBirthdayEnd;
    public string txtstaffBirthdayStart;
    public string txtstaffYear;//StaffMerit
}
public struct SearchClassName
{
    public string txtstaffID;
    public string txtstaffName;
    public string txtClassID;
    public string txtClassName;
}

public struct SearchStudent
{
    public string txtstudentID;
    public string txtstudentName;
    public string txtstudentSex;
    public string txtbirthdayend;
    public string txtbirthdaystart;
    public string txtcaseStatu;
    public string txtendReasonDateend;
    public string txtendReasonDatestart;
    public string txtendReasonType;
    public string txtjoindayend;
    public string txtjoindaystart;
    public string txtnomembershipType;
    public string txtAcademicYearstart;//Add by Awho(教學管理搜尋使用)
    public string txtAcademicYearend;//Add by Awho(教學管理搜尋使用)
}
public struct SearchStudentService
{
    public string txtstudentID;
    public string txtstudentName;
    public string txtstudentSex;
    public string txtbirthdayend;
    public string txtbirthdaystart;
    public string txtcaseStatu;
    public string txtendviewend;
    public string txtendviewstart;
}
public struct SearchStudentAid
{
    public string txtstudentID;
    public string txtstudentName;
    public string txtstudentSex;
    public string txtbirthdayend;
    public string txtbirthdaystart;
    public string txtsubsidyitem;
    public string txtfillInDatestart;
    public string txtfillInDateend;
}
public struct SearchStudentISP {
    public string txtstudentID;
    public string txtstudentName;
    public string txtstudentSex;
    public string txtbirthdayend;
    public string txtbirthdaystart;
    public string txtteachername;
}
public struct SearchStudentActivity{
    public string txteventName;
    public string txteventDatestart;
    public string txteventDateend;
}

public struct SearchRemindSystem
{
    public string txtdesignee;
    public string txtrecipient;
    public string txtdesigneeDatestart;
    public string txtdesigneeDateend;
    public string txtexecutionDatestart;
    public string txtexecutionDateend;
    public string txtfulfillmentDatestart;
    public string txtfulfillmentDateend;
}
public struct SearchStaffCredit
{
    public string txtstaffName;
    public string txtProve;
    public string txtLecturer;
    public string txtDateStart;
    public string txtDateEnd;

}
public struct SearchStaffBehave
{
    public string txtstaffName1;
    public string txtType;
    public string txtDateStart1;
    public string txtDateEnd1;

}
public struct SearchTeacherRstimate
{
    public string txtTeacher;
    public string txtTeacherSex;
    public string txtTeacherUnit;
    public string txtassessmentStart;
    public string txtassessmentEnd;
}
public struct SearchExternal
{
    public string txtstaffName;
}



public struct SearchBook
{
    public string txtbookNumber;
    public string txtbookTitle;
    public string txtbookClassification;
    public string txtbookAuthor;
    public string txtbookPress;
}

public struct SearchUser
{
    public string txtpeopleID;
    public string txtbookBorrowID;
    public string txtbookReturnID;
}

public struct SearchBookStatistics
{
    public string txtbookDayType;
    public string txtbookStartDay;
    public string txtbookEndDay;
    public string txtbookDateStartDate;
    public string txtbookDateEndDate;
    public string txtrecordBookStartDate;
    public string txtrecordBookEndDate;
    public string txtrecordBookID;
    public string txtrecordBorrowerStartDate;
    public string txtrecordBorrowerEndDate;
    public string txtrecordBorrowerType;
    public string txtrecordBorrowerName;
    public string txtrecordBorrowerClassID;
    public string txtrecordBorrowerClassName;
}

public struct SearchStationery
{
    public string txtstationeryID;
    public string txtstationeryName;
    public string txtstationeryType;
    public string txtstationeryUnit;
    public string txtsafeQuantityStart;
    public string txtsafeQuantityEnd;
}
public struct SearchPurchase
{
    public string txtpurchaseID;
    public string txtfirmName;
    public string txtpurchaseDateStart;
    public string txtpurchaseDateEnd;
    public string txtstationeryID;
    public string txtrstationeryName;
}
public struct SearchReceive
{
    public string txtreceiveID;
    public string txtreceiveDateStart;
    public string txtreceiveDateEnd;
    public string txtreceiveBy;
    public string txtrstationeryID;
    public string txtrstationeryName;
}
public struct SearchScrap
{
    public string txtscrappedID;
    public string txtscrappedDateStart;
    public string txtscrappedDateEnd;
    public string txtscrappedBy;
    public string txtsstationeryID;
    public string txtsstationeryName;
}
public struct SearchReturn
{
    public string txtreturnedID;
    public string txtreturnedDateStart;
    public string txtreturnedDateeEnd;
    public string txtgetgoodsBy;
    public string txtrestationeryID;
    public string txtrestationeryName;
}

public struct SearchAudiometryAppointment
{
    public string txtstudentName;
    public string txtstudentID;
    public string txtstudentSex;
    public string txtstudentDateStart;
    public string txtstudentDateEnd;
    public string txtcheckDateStart;
    public string txtcheckDateEnd;
}
public struct SaveAudiometryAppointmentContent
{
    public Int64 aID;
    public string aContent;
}
public struct SearchResourceCard {
    public string txtresourceName;
    public string txtaddressCity;
    public string txtresourceType;
    public string txtresourceItem;
}
public struct SearchVolunteer
{
    public string txtvID;
    public string txtvName;
    public string txtvSex;
}
public struct SearchVisitRecord
{
    public string txtstudentID;
    public string txtstudentName;
    public string txtstudentSex;
    public string txtbirthdayend;
    public string txtbirthdaystart;
    public string txtvisitType;
    public string txtvisitDatestart;
    public string txtvisitDateend;
    public string txtvisitSocial;
}
public struct SearchTransRecord
{
    public string txtstudentName;
    public string txttransYear;
    public string txttransActivity;
    public string txtmeeting;
    public string txtschoolVisit;
}

public struct SearchAidsUse
{
    public string txtstudentName;
    public string txtstudentID;
    public string txtbirthdayend;
    public string txtbirthdaystart;
    public string txtaidstypeL;
    public string txtaidstypeR;
}

public struct SearchApplyProperty
{
    public string txtapplyType;
    public string txtapplyStatus;
    public string txtapplyID;
    public string txtapplyDateStart;
    public string txtapplyDateEnd;
}

public struct SearchPropertyRecord
{
    public string txtpropertyCode;
    public string txtpropertyID;
    public string txtpropertyName;
    public string txtapplyID;
    public string txtlocation;
    public string txtcustody;
}
public struct SearchHearingAssessment
{
    public string txtstudentName;
    public string txtstudentSex;
    public string txtbirthdayend;
    public string txtbirthdaystart;
    public string txtcheckDatestart;
    public string txtcheckDateend;
}
public struct SearchTeachCaseItem
{
    public string txtClassName;
    public string txtTeacherName;
    public string txtCourseName;
    public string txtSPeriod;
    public string txtEPeriod;
}
public struct SearchAidsManageResult
{
    public string ID;
    public string txtaStatu;
    public string txtaID;
    public string txtassistmanage;
    public string txtbrand;
    public string txtmodel;
    public string txtaNo;
    public string txtaSource;
    public string checkNo;
    public string errorMsg;
}
public struct SearchFMAidsAssess
{
    public string ID;
    public string txtstudentName;
    public string txtstudentSex;
    public string txtbirthdaystart;
    public string txtbirthdayend;
    public string txtAssessDatestart;
    public string txtAssessDateend;
    public string checkNo;
    public string errorMsg;
}
//---------------------------------------end search Structure------------------------

//--------------------------------------return search result Structure---------------
public struct SearchStudentResult
{
    public Int64 ID;
    public string txtstudentID;
    public string txtstudentStatu;
    public string txtstudentName;
    public string txtLegalRepresentative;
    public string txtLegalRepresentativeTel;
    public string txtLegalRepresentativePhone;
    public int txtstudentSex;
    public DateTime txtstudentbirthday;
    public string checkNo;
    public string errorMsg;
}
public struct SearchStudentServiceResult
{
    public Int64 ID;
    public string txtstudentID;
    public string txtstudentName;
    public string txtstudentStatu;
    public string txtviewData;
    public string txtviewTitle;
    public string txtviewStyle;
    public string txtviewPeople;
    public string checkNo;
    public string errorMsg;
}
public struct SearchStudentTrackResult
{
    public Int64 ID;
    public string txtstudentID;
    public string txtstudentName;
    public string txtendReasonDate;
    public string txtendReasonDateType;
    public string txtfPName2;
    public string txtfpHPhone2;
    public string txtfPPhone2;
    public string txtemail;
    public string checkNo;
    public string errorMsg;

/**/
    public string txtTel;
}
public struct SearchStudentAidResult
{
    public string ID;
    public string txtstudentID;
    public string txtstudentName;
    public string txtstudentTWID;
    public string txtsubsidyitem;
    public string txtsubsidydate1;
    public string txtsubsidydate2;
    public string txtfillInDate;
    public string txtsubsidymoney;
    public string checkNo;
    public string errorMsg;
}
public struct StaffDataList
{
    public string ID;
    public string sID;
    public string sName;
    public string sEmail;
    public string sSex;
    public string sUnit;
    public string sJob;
    public string FileDate;
    public string Phone;
    public string officeDate;
    public string resignDate;
    public string[] Roles;
    public string pw;
    public string checkNo;
    public string errorMsg;
}
public struct SearchRemindSystemResult
{
    public string Number;
    public string rID;
    public string rType;
    public string recipient;
    public string executionContent;
    public string executionDate;
    public string fulfillmentDate;
    public string designee;
    public string designeeDate;
    public string checkNo;
    public string errorMsg;
}
public struct SearchStudentActivityResult
{
    public string ID;
    public string txteventName;
    public string txteventDate;
    public string txttotalNumber;
    public string checkNo;
    public string errorMsg;
}
public struct StudentResult {
    public Int64 Column;
    public DateTime UpDate;
    public int StudentStatu;
    public CreateStudent StudentData;
    public StudentHearingInformation HearingData;
    public StudentTeachingInformation TeachData;
    public List<StudentBodyInformation> BodyData;
    public string checkNo;
    public string errorMsg;

}
public struct SearchTeacherRstimateResult
{
    public string Number;
    public string ID;
    public string Unit;
    public string TeacherName;
    public string AssessmentDate;
}
public struct SearchExternalResult
{
    public string ID;
    public string sName;
    public string sEmail;
    public int sSex;
    public int sUnit;
    public string Phone;
    public string Phone2;
    public string checkNo;
    public string errorMsg;
}


public struct SearchBookResult
{
    public Int64 bID;
    public string executionTitle;
    public string executionAuthor;
    public string executionPress;
    public string executionPressDate;
    public string executionRemark;
}


public struct SearchStationeryResult
{
    public Int64 sID;
    public string executionName;
    public string executionUnit;
    public string executionQuantity;
    public string executionRemark;
}
public struct SearchPurchaseResult
{
    public Int64 pID;
    public string executionFirmName;
    public string executionFirmTel;
    public string executionPurchaseDate;
    public string executionQuantity;
    public string executionPrice;
}
public struct SearchReceiveResult
{
    public Int64 rID;
    public string executionreceiveDate;
    public string executionQuantity;
    public string executionRemark;
}
public struct SearchScrapResult
{
    public Int64 sID;
    public string executionscrapDate;
    public string executionQuantity;
    public string executionRemark;
}
public struct SearchReturnResult
{
    public Int64 rID;
    public string executionreturnedDate;
    public string executiongetgoodsDate;
    public string executionQuantity;
    public string executionReason;
}
public struct SearchResourceCardResult
{
    public string ID;
    public string txtresourceName;
    public string txtaddressCity;
    public string txtresourceType;
    public string txtresourceItem;
    public string checkNo;
    public string errorMsg;
}
public struct SearchVisitRecordResult
{
    public string ID;
    public string txtstudentID;
    public string txtstudentName;
    public string txtbirthday;
    public string txtvisitType;
    public string txtvisitDate;
    public string txtvisitSocial;
    public string checkNo;
    public string errorMsg;

}
public struct SearchTransRecordResult
{
    public string ID;
    public string txtstudentName;
    public string txttransYear;
    public string txtstudentAge;
    public string txtstudentMonth;
    public string txttransStage;
    public string checkNo;
    public string errorMsg;
}

public struct SearchAidsUseResult
{
    public string ID;
    public string txtstudentName;
    public string txtstudentID;
    public string txtaidstypeL;
    public string txtbuyingtimeL;
    public string txtaidstypeR;
    public string txtbuyingtimeR;
    public string txtfmAidstypeL;
    public string txtfmAidstypeR;
    public string txtassessDate;
    public string checkNo;
    public string errorMsg;
}

public struct SearchApplyPropertyResult
{
    public string ID;
    public string txtapplyID;
    public string txtapplyDate;
    public string txtapplyType;
    public string txtapplyPay;
    public string txtapplyByID;
    public string txtapplyBy;
    public string txtapplyStatus;
    public string txtapplySum;
    public string checkNo;
    public string errorMsg;
}

public struct SearchPropertyRecordResult
{
    public string ID;
    public string txtpropertyID;
    public string txtUnit;
    public string txtcode;
    public string txtapplyID;
    public string txtpropertyName;
    public string txtpropertyState;
    public string txtlocation;
    public string txtcustody;
    public string txtwriteDate;
    public string txtbuyDate;
    public string txtpropertyPrice;
    public string checkNo;
    public string errorMsg;
}
public struct SearchHearingAssessmentResult
{
    public string ID;
    public string txtstudentName;
    public string txtbirthday;
    public string txtcheckDate;
    public string txtcheckAge;
    public string txtcheckAgeMonth;
    public string txtaudiologistName;
}
public struct SearchHearingInspectionResult
{
    public string ID;
    public string txtstudentName;
    public string txtbirthday;
    public string txtcheckDate;
    public string txtcheckMode;
    public string txtaudiologistName;
    public string checkNo;
    public string errorMsg;
}
public struct SearchFMAidsAssessResult
{
    public string ID;
    public string txtstudentName;
    public string txtstudentSex;
    public string txtbirthday;
    public string txtAssessDate;
    public string txtaudiologist;
    public string checkNo;
    public string errorMsg;
}
//--------------------------------------end return search result Structure---------------

//--------------------------------------Create Data Structure---------------
public struct StudentBasicData
{
    public string studentID;
    public string studentName;
    public string studentbirthday;
    public string studentTWID;
    public string studentSex;
    public string ParentName;
    public string ParentTel;
    public string ParentPhone;
    public string addressZip;
    public string addressCity;
    public string address;
    public string email;
    public string checkNo;
    public string errorMsg;

}
/*Case-Student**/
public class CreateStudent
{
    public string ID;
    public string assessDate;
    public string consultationDate;
    public string upDate;
    public string caseStatu;
    public string fillInName;
    public string Unit;
    public string studentID;
    public string studentName;
    public string studentTWID;
    public string censusAddressZip;
    public string censusAddressCity;
    public string censusAddress;
    public string addressZip;
    public string addressCity;
    public string address;
    public string studentbirthday;
    public string studentSex;
    public string firstClassDate;
    public string guaranteeDate;
    public string BackGuaranteeDate;
    public string endReasonDate;
    public string endReasonType;
    public string endReason;
    public string sendDateSince;
    public string sendDateUntil;
    public string nomembershipType;
    public string nomembershipReason;
    public string studentPhoto;
    public string wCare;
    public string wCareName;
    public string bCare;
    public string bCareName;
    public string fPRelation1;
    public string fPName1;
    public string fPTel1;
    public string fPPhone1;
    public string fPHPhone1;
    public string fPFax1;
    public string fPRelation2;
    public string fPName2;
    public string fPTel2;
    public string fPPhone2;
    public string fPHPhone2;
    public string fPFax2;
    public string fPRelation3;
    public string fPName3;
    public string fPTel3;
    public string fPPhone3;
    public string fPHPhone3;
    public string fPFax3;
    public string fPRelation4;
    public string fPName4;
    public string fPTel4;
    public string fPPhone4;
    public string fPHPhone4;
    public string fPFax4;
    public string email;
    public string sourceType;
    public string sourceName;
    public string manualWhether;
    public string manualCategory1;
    public string manualGrade1;
    public string manualCategory2;
    public string manualGrade2;
    public string manualCategory3;
    public string manualGrade3;
    public string manualNo;
    public string manualUnit;
    public string studentManualImg;
    public string notificationWhether;
    public string notificationUnit;
    public string notificationManage;
    public string notificationTel;
    public string notificationDate;
    public string notificationCity;
    public string economy;
    public string economyLow;


    public string fAppellation1;
    public string fName1;
    public string fBirthday1;
    public string fEDU1;
    public string fProfession1;
    public string fLive1;
    public string fHearing1;
    public string fHealthy1;
    public string familyText01;
    public string fAppellation2;
    public string fName2;
    public string fBirthday2;
    public string fEDU2;
    public string fProfession2;
    public string fLive2;
    public string fHearing2;
    public string fHealthy2;
    public string familyText02;
    public string fAppellation3;
    public string fName3;
    public string fBirthday3;
    public string fEDU3;
    public string fProfession3;
    public string fLive3;
    public string fHearing3;
    public string fHealthy3;
    public string familyText03;
    public string fAppellation4;
    public string fName4;
    public string fBirthday4;
    public string fEDU4;
    public string fProfession4;
    public string fLive4;
    public string fHearing4;
    public string fHealthy4;
    public string familyText04;
    public string fAppellation5;
    public string fName5;
    public string fBirthday5;
    public string fEDU5;
    public string fProfession5;
    public string fLive5;
    public string fHearing5;
    public string fHealthy5;
    public string familyText05;
    public string fAppellation6;
    public string fName6;
    public string fBirthday6;
    public string fEDU6;
    public string fProfession6;
    public string fLive6;
    public string fHearing6;
    public string fHealthy6;
    public string familyText06;
    public string fAppellation7;
    public string fName7;
    public string fBirthday7;
    public string fEDU7;
    public string fProfession7;
    public string fLive7;
    public string fHearing7;
    public string fHealthy7;
    public string familyText07;
    public string fAppellation8;
    public string fName8;
    public string fBirthday8;
    public string fEDU8;
    public string fProfession8;
    public string fLive8;
    public string fHearing8;
    public string fHealthy8;
    public string familyText08;
    public string lang1;
    public string lang1t01;
    public string lang2;
    public string lang2t01;

    public string familyEcological;
    public string famailySituation;
    public string famailyMedical;
    public string famailyActionSituation;
    public string fswAssess;
    public string socialID;
    public string socialName;
    public string socialDate;

    public string checkNo;
    public string errorMsg;
}

public class StudentData1
{
    public string ID;
    public string assessDate;
    public string consultationDate;
    public string upDate;
    public string caseStatu;
    public string fillInName;
    public string Unit;
    public string studentID;
    public string studentName;
    public string studentTWID;
    public string censusAddressZip;
    public string censusAddressCity;
    public string censusAddress;
    public string addressZip;
    public string addressCity;
    public string address;
    public string studentbirthday;
    public string studentSex;
    public string firstClassDate;
    public string guaranteeDate;
    public string BackGuaranteeDate;
    public string endReasonDate;
    public string endReasonType;
    public string endReason;
    public string sendDateSince;
    public string sendDateUntil;
    public string nomembershipType;
    public string nomembershipReason;
    public string studentPhoto;
    public string wCare;
    public string wCareName;
    public string bCare;
    public string bCareName;
    public string fPRelation1;
    public string fPName1;
    public string fPTel1;
    public string fPPhone1;
    public string fPHPhone1;
    public string fPFax1;
    public string fPRelation2;
    public string fPName2;
    public string fPTel2;
    public string fPPhone2;
    public string fPHPhone2;
    public string fPFax2;
    public string fPRelation3;
    public string fPName3;
    public string fPTel3;
    public string fPPhone3;
    public string fPHPhone3;
    public string fPFax3;
    public string fPRelation4;
    public string fPName4;
    public string fPTel4;
    public string fPPhone4;
    public string fPHPhone4;
    public string fPFax4;
    public string email;
    public string sourceType;
    public string sourceName;
    public string manualWhether;
    public string manualCategory1;
    public string manualGrade1;
    public string manualCategory2;
    public string manualGrade2;
    public string manualCategory3;
    public string manualGrade3;
    public string manualNo;
    public string manualUnit;
    public string studentManualImg;
    public string notificationWhether;
    public string notificationUnit;
    public string notificationManage;
    public string notificationTel;
    public string notificationDate;
    public string notificationCity;
    public string economy;
    public string economyLow;

    public string checkNo;
    public string errorMsg;
}
public class StudentData2
{
    public string ID;
    public string studentID;
    public string fAppellation1;
    public string fName1;
    public string fBirthday1;
    public string fEDU1;
    public string fProfession1;
    public string fLive1;
    public string fHearing1;
    public string fHealthy1;
    public string familyText01;
    public string fAppellation2;
    public string fName2;
    public string fBirthday2;
    public string fEDU2;
    public string fProfession2;
    public string fLive2;
    public string fHearing2;
    public string fHealthy2;
    public string familyText02;
    public string fAppellation3;
    public string fName3;
    public string fBirthday3;
    public string fEDU3;
    public string fProfession3;
    public string fLive3;
    public string fHearing3;
    public string fHealthy3;
    public string familyText03;
    public string fAppellation4;
    public string fName4;
    public string fBirthday4;
    public string fEDU4;
    public string fProfession4;
    public string fLive4;
    public string fHearing4;
    public string fHealthy4;
    public string familyText04;
    public string fAppellation5;
    public string fName5;
    public string fBirthday5;
    public string fEDU5;
    public string fProfession5;
    public string fLive5;
    public string fHearing5;
    public string fHealthy5;
    public string familyText05;
    public string fAppellation6;
    public string fName6;
    public string fBirthday6;
    public string fEDU6;
    public string fProfession6;
    public string fLive6;
    public string fHearing6;
    public string fHealthy6;
    public string familyText06;
    public string fAppellation7;
    public string fName7;
    public string fBirthday7;
    public string fEDU7;
    public string fProfession7;
    public string fLive7;
    public string fHearing7;
    public string fHealthy7;
    public string familyText07;
    public string fAppellation8;
    public string fName8;
    public string fBirthday8;
    public string fEDU8;
    public string fProfession8;
    public string fLive8;
    public string fHearing8;
    public string fHealthy8;
    public string familyText08;
    public string lang1;
    public string lang1t01;
    public string lang2;
    public string lang2t01;
    public string checkNo;
    public string errorMsg;
}

public class StudentData4
{
    public string ID;
    public string studentID;
    public string familyEcological;
    public string famailySituation;
    public string famailyMedical;
    public string famailyActionSituation;
    public string fswAssess;
    public string socialID;
    public string socialName;
    public string socialDate;

    public string checkNo;
    public string errorMsg;
}

public struct StudentWelfareResource {
    public string wID;
    public string studentID;
    public string resourceDate;
    public string resourceID;
    public string resourceName;
    public string resourceItem;
    public string resourceType;
    public string checkNo;
    public string errorMsg;
}
public struct CreateStudentView
{
    public string ID;
    public string caseUnit;
    public string studentID;
    public string studentName;
    public string visitType;
    public string target;
    public string viewSocialWork;
    public string viewDate;
    public string viewTime1;
    public string viewTime2;
    public string viewUnit;
    public string viewTel;
    public string addressZip;
    public string addressCity;
    public string address;
    public string viewPeople1;
    public string viewContent1;
    public string viewContent2;
    public string viewRemark1;
    public string viewPeople2;
    public string viewPlace;
    public string pedigree;
    public string ecological;
    public string cureUnit;
    public string cureDate;
    public string cureType;
    public string cureType1;
    public string cureUnit1;
    public string cureNumber1;
    public string cureNumberTime1;
    public string cureType2;
    public string cureUnit2;
    public string cureNumber2;
    public string cureNumberTime2;
    public string preSchool;
    public string studyDate1;
    public string studyDate2;
    public string studyType;
    public string familyType;
    public string familyTypeText;
    public string careProple;
    public string carePropleText;
    public string careTime;
    public string homeOwnership;
    public string homeAround;
    public string homeAroundText;
    public string homeType;
    public string homeTypeText;
    public string transportType;
    public string leisureType;
    public string leisureTypeText;
    public string explanation2;
    public string homeSpace1;
    public string homeSpace2;
    public string furniture;
    public string electric;
    public string place;
    public string hydro;
    public string dining1;
    public string dining2;
    public string indoor1;
    public string indoor2;
    public string indoor3;
    public string indoor4;
    public string explanation3;
    public string informal1;
    public string informal2;
    public string informal3;
    public string informal4;
    public string informal5;
    public string informal6;
    public string informal7;
    public string informal7Text;
    public string informal8;
    public string informal8Text;
    public string informal9;
    public string informal9Text;
    public string informal10;
    public string explanation4;
    public string formalText1;
    public string formalText2;
    public string formalText3;
    public string formalText4;
    public string formalText5;
    public string formalText6;
    public string formalText7;
    public string formalText8;
    public string formalText9;
    public string formal1;
    public string formal2;
    public string formal3;
    public string explanation5;
    public string familyFunction1;
    public string familyFunction2;
    public string familyFunction3;
    public string familyFunction4;
    public string familyFunction5;
    public string familyFunction6;
    public string viewSituation;
    public string familyStrengths;
    public string familyLimit;
    public string caseStrengths;
    public string caseLimit;
    public string primaryCaseStrengths;
    public string primaryCaseLimit;
    public string familyDemand;
    public string assessment;
    public string trafficRoute;
    public string explanation6;
    public string viewPhoto1;
    public string viewPhotoText1;
    public string viewPhoto2;
    public string viewPhotoText2;
    public string viewPhoto3;
    public string viewPhotoText3;
    public string viewPhoto4;
    public string viewPhotoText4;
    public string viewPhoto5;
    public string viewPhotoText5;
    public string viewPhoto6;
    public string viewPhotoText6;
    public string viewPhoto7;
    public string viewPhotoText7;
    public string viewPhoto8;
    public string viewPhotoText8;
    public string teachingSpace;
    public string familySupport;
    public string familyInteraction;
    public string teachingDemonstration;
    public string viewConclusion;
    public string teachingAnalysis;
    public string checkNo;
    public string errorMsg;
}

public struct CreateVolunteer{
    public string ID;
    public string sUnit;
    public string fillInDate;
    public string volunteerId;
    public string volunteerName;
    public string vSex;
    public string vBirthday;
    public string nowJob;
    public string telDaytime;
    public string telNight;
    public string volunteerPhone;
    public string volunteerFax;
    public string addressZip;
    public string addressCity;
    public string address;
    public string vEmail;
    public string Expertise;
    public string Experience;
    public string VOLExpect;
    public string servicedate;
    public string servicecontent;
    public string otherService;
    public string vSource;
    public List<CreateVolunteerService> Service;
    public string checkNo;
    public string errorMsg;

}
public struct CreateVolunteerService
{
    public string ID;
    public string vID;
    public string vsDate;
    public string vsStartTime;
    public string vsEndTime;
    public string vsHours;
    public string vsContent;
    public string checkNo;
    public string errorMsg;
}
public struct CreateStudentService
{
    public string ID;
    public string studentID;
    public string studentName;
    public string studentbirthday;
    public string studentSex;
    public string sUnit;
    public string viewData;
    public string viewTime;
    public string viewStyle;
    public string viewTitle;
    public string viewPeople;
    public string viewPlace;
    public List<string> viewStaff;
    public string viewContent;
    public string checkNo;
    public string errorMsg;

}
public struct StudentHearingInformation {
    public Int64 ID;
    public string studentID;
    public string history;
    public string history02t01;
    public string history02t02;
    public string history03t01;
    public string history06t01;
    public string history06t02;
    public string history08t01;
    public string history10t01;
    public string history11t01;
    public string history12t01;

    public string problems01t01;
    public string problems01t02;
    public string hearingQ;
    public string hearingQText;
    public string problems02t01;
    public string problems02t02;
    public string problems02t03;
    public string problems02t04;
    public string problems02t05;
    public string hearingcheck;
    public string hearingYescheck;
    public string hearingYesPlace;
    public string hearingYesResultR;
    public string hearingYesResultL;
    public string sleepcheck;
    public string sleepcheckTime1;
    public string sleepcheckPlace1;
    public string sleepcheckCheckItem1;
    public string sleepcheckResultL1;
    public string sleepcheckResultR1;
    public string sleepcheckTime2;
    public string sleepcheckPlace2;
    public string sleepcheckCheckItem2;
    public string sleepcheckResultL2;
    public string sleepcheckResultR2;
    public string sleepcheckTime3;
    public string sleepcheckPlace3;
    public string sleepcheckCheckItem3;
    public string sleepcheckResultL3;
    public string sleepcheckResultR3;
    public string sleepcheckTime4;
    public string sleepcheckPlace4;
    public string sleepcheckCheckItem4;
    public string sleepcheckResultL4;
    public string sleepcheckResultR4;
    public string sleepcheckTime5;
    public string sleepcheckPlace5;
    public string sleepcheckCheckItem5;
    public string sleepcheckResultL5;
    public string sleepcheckResultR5;
    public string ctmri;
    public string ctmriTime;
    public string ctmriPlace;
    public string ctmriResultL;
    public string ctmriResultR;
    public string gene;
    public string geneTime;
    public string genePlace;
    public string geneResult;
    public string familyhistory;
    public string familyhistoryText;
    public string changehistory;
    public string changehistoryText;
    public string assistmanage;
    public string accessory;
    public string assistmanageR;
    public string brandR;
    public string modelR;
    public string buyingPlaceR;
    public string buyingtimeR;
    public string insertHospitalR;
    public string openHzDateR;
    public string assistmanageL;
    public string brandL;
    public string modelL;
    public string buyingtimeL;
    public string buyingPlaceL;
    public string insertHospitalL;
    public string openHzDateL;
    public string allassis;
    public string allassisNoText;
    public string assis1;
    public string assis1NoText;
    public string assis2;
    public string assis2NoText;
    public string assisFM;
    public string assisFMBrand;
    public string assessnotes;
    public string assessnotes1;
    public string assessnotes102Text;
    public string problems11t02;
    public string assessnotes2;
    public string problems11t03;
    public string problems11t04;
    public string problems11t05;
    public string problems11t06;
    public string problems11t07;

    public string inspectorID;
    public string inspectorDate;
    public string inspectorName;
    public string checkNo;
    public string errorMsg;

}

public struct StudentTeachingInformation
{
    public Int64 ID;
    public string case1;
    public string case1t04;
    public string case1t05;
    public string case2;
    public string case21;
    public string case21t01;
    public string case22;
    public string case22t01;
    public string case23;
    public string case23t01;
    public string case24;
    public string case24t01;
    public string case25;
    public string case25t01;
    public string case26;
    public string case26t01;
    public string case3;
    public string case31;
    public string case31t01;
    public string case32;
    public string case32t01;
    public string case33;
    public string case33t01;
    public string case34;
    public string case34t01;
    public string attend;
    public string attend01t01;
    public string accompany;
    public string accompany01t01;
    public string teach;
    public string teach01t01;
    public string caseQ;
    public string caseQ01t01;
    public string OtherRemark1;
    public string case4;
    public string case5;
    public string case6;
    public string case7;
    public string case8;
    public string case9;
    public string case10;
    public string case11;
    public string case12;
    public string case12t01;
    public string case13;
    public string wear;
    public string mind;
    public string mind01t01;
    public string connectwish;
    public string studywish;
    public string related;
    public string related01t01;
    public string OtherRemark2;
    public string case14;
    public string trusteeship;
    public string case14t01;
    public string proceed;
    public string proceedt01;
    public string preschools;
    public string case15;
    public string Rehabilitation1;
    public string Rehabilitation2;
    public string Rehabilitation3;
    public string Rehabilitation3Text;
    public string case16;
    public string OtherRemark3;
    public string case17;
    public string OtherRemark4;
    public string teacherID;
    public string teacherName;
    public string teacherDate;
    public string checkNo;
    public string errorMsg;
}
public struct StudentBodyInformation
{
    public string studentID;
    public string RecordID;
    public string RecordHeight;
    public string RecordWeight;
    public string RecordRemark;
    public string RecordDate;
    public string checkNo;
    public string errorMsg;
}
public struct CreateResourceCard
{
    public string ID;
    public string fillInDate;
    public string resourceName;
    public string resourceItem;
    public string resourceType;
    public string addressZip;
    public string addressCity;
    public string address;
    public string contact_1;
    public string contactPhone_1;
    public string contactFax_1;
    public string contactEmail_1;
    public string contact_2;
    public string contactPhone_2;
    public string contactFax_2;
    public string contactEmail_2;
    public string contact_3;
    public string contactPhone_3;
    public string contactFax_3;
    public string contactEmail_3;
    public string referral;
    public string sObject;
    public string sTime;
    public string sItem;
    public string sExpense;
    public string sProgram;
    public string sInformation;
    public string sLink;
    public string upBy;
    public string upDate;
    public string fillInBy;
    public string sUnit;
    public string checkNo;
    public string errorMsg;
}
public struct CreateStudentTracked
{
    public string ID;
    public string StudentIdentity;
    public string sUnit;
    public string studentID;
    public string studentName;
    public string studentbirthday;
    public string studentSex;
    public string guaranteeDate;
    public string endReasonDate;
    public string firstClassDate; 
    public string studentNameNew;/*改名用*/
    public string email;
    public string censusAddressZip;
    public string censusAddressCity;
    public string censusAddress;
    public string addressZip;
    public string addressCity;
    public string address;
    public string fPRelation1;
    public string fPName1;
    public string fPTel1;
    public string fPPhone1;
    public string fPHPhone1;
    public string fPFax1;
    public string fPRelation2;
    public string fPName2;
    public string fPTel2;
    public string fPPhone2;
    public string fPHPhone2;
    public string fPFax2;
    public string fPRelation3;
    public string fPName3;
    public string fPTel3;
    public string fPPhone3;
    public string fPHPhone3;
    public string fPFax3;
    public string fPRelation4;
    public string fPName4;
    public string fPTel4;
    public string fPPhone4;
    public string fPHPhone4;
    public string fPFax4;
    public string manualCategory1;
    public string manualGrade1;
    public string manualCategory2;
    public string manualGrade2;
    public string manualCategory3;
    public string manualGrade3;
    public string assistmanageR;
    public string brandR;
    public string modelR;
    public string buyingPlaceR;
    public string buyingtimeR;
    public string insertHospitalR;
    public string openHzDateR;
    public string assistmanageL;
    public string brandL;
    public string modelL;
    public string buyingtimeL;
    public string buyingPlaceL;
    public string insertHospitalL;
    public string openHzDateL;
    public string remark;
    public string esName;
    public string esDepartment;
    public string esSince1;
    public string esSince2;
    public string esUntil1;
    public string esUntil2;
    public string esType;
    public string jsName;
    public string jsDepartment;
    public string jsSince1;
    public string jsSince2;
    public string jsUntil1;
    public string jsUntil2;
    public string jsType;
    public string hsName;
    public string hsDepartment;
    public string hsSince1;
    public string hsSince2;
    public string hsUntil1;
    public string hsUntil2;
    public string hsType;
    public string uName;
    public string uDepartment;
    public string uSince1;
    public string uSince2;
    public string uUntil1;
    public string uUntil2;
    public string uType;
    public string mName;
    public string mDepartment;
    public string mSince1;
    public string mSince2;
    public string mUntil1;
    public string mUntil2;
    public string mType;
    



    public string jobUnit;
    public List<TeackedData> Teack;
    public string checkNo;
    public string errorMsg;

    /**/
    public string Tel;
    public string otherType;
    public string otherName;
    public string masterType;
    public string masterName;
    public string masterDepartment;


    public string ElementarySY;
    public string ElementarySM;
    public string ElementaryEY;
    public string ElementaryEM;
    public string JuniorHighSY;
    public string JuniorHighSM;
    public string JuniorHighEY;
    public string JuniorHighEM;
    public string HighSY;
    public string HighSM;
    public string HighEY;
    public string HighEM;
    public string UniversitySY;
    public string UniversitySM;
    public string UniversityEY;
    public string UniversityEM;
    public string JobSY;
    public string JobSM;
    public string JobEY;
    public string JobEM;
    public string OtherSY;
    public string OtherSM;
    public string OtherEY;
    public string OtherEM;
    
}
public struct createStudentActivity
{
    public string ID;
    public string caseUnit;
    public string eventName;
    public string eventDate;
    public List<string> eventStaffList;
    public List<string[]> Participants;
    public string creatFileName;
    public string checkNo;
    public string errorMsg;

}

public struct TeackedData
{
    public string tID;
    public string offID;
    public string tDate;
    public string tStaff;
    public string tStaffName;
    public string tContent;
}
public struct CreateStudentAid {
    public string ID;
    public string sUnit;
    public string fillInDate;
    public string studentID;
    public string studentName;
    public string studentbirthday;
    public string studentTWID;
    public string ParentName;
    public string ParentTel;
    public string ParentPhone;
    public string addressZip;
    public string addressCity;
    public string address;
    public string subsidyitem;
    public string subsidytext;
    public string payitem;
    public string manualCategory;
    public string manualGrade;
    public string othertext;
    public string subsidymoney;
    public string subsidydate1;
    public string subsidydate2;
    public string checkNo;
    public string errorMsg;
}
public struct CreateStudentTrans
{
    public string ID;
    public string caseUnit;
    public string studentID;
    public string studentName;
    public string studentAge;
    public string studentMonth;
    public string transYear;
    public string transStage;
    public string bSchool;
    public string aSchool;
    public string contact;
    public string contactTel;
    public string transActivity;
    public string transActivityName1;
    public string transActivityDate1;
    public string transActivityContent1;
    public string transActivityName2;
    public string transActivityDate2;
    public string transActivityContent2;
    public string transActivityName3;
    public string transActivityDate3;
    public string transActivityContent3;
    public string transActivityName4;
    public string transActivityDate4;
    public string transActivityContent4;
    public string antonMessage;
    public string sendDocument;
    public string transReport;
    public string transReportFile;
    public string transFamilyReportFile;
    public string meeting;
    public string meetingVisitReport;
    public string adaptation;
    public string adaptationReport;
    public string schoolVisit;
    public string schoolVisitRecord;
    public string schooladvocacyReport;
    public string checkNo;
    public string errorMsg;
}
public struct CreateStudentAidsUse
{
    public string ID;
    public string caseUnit;
    public string studentID;
    public string studentName;
    public string studentbirthday;
    public string studentAge;
    public string studentMonth;
    public string assistmanageR;
    public string brandR;
    public string modelR;
    public string buyingPlaceR;
    public string buyingtimeR;
    public string insertHospitalR;
    public string openHzDateR;
    public string sourceR;
    public string sourceTextR;
    public string assistmanageL;
    public string brandL;
    public string modelL;
    public string buyingtimeL;
    public string buyingPlaceL;
    public string insertHospitalL;
    public string openHzDateL;
    public string sourceL;
    public string sourceTextL;  
    public string gainDate;
    public string fmAidssource;
    public string fmAidstype;
    public string fmBrand;
    public string fmModel;
    public string fmChannel;
    public string fmAidstypeR;
    public string DPAIsettingR;
    public string fmProgramR;
    public string fmAudioR;
    public string fmUIR;
    public string fmUITextR;
    public string fmReceptorR;
    public string fmVolumeR;
    public string fmGainR;
    public string fmAidstypeL;
    public string DPAIsettingL;
    public string fmProgramL;
    public string fmAudioL;
    public string fmUIL;
    public string fmUITextL;
    public string fmReceptorL;
    public string fmVolumeL;
    public string fmGainL;
    public string checkNo;
    public string errorMsg;
    public string assessDate;
}
/*Teach-ISP**/
public struct CreateTeachISP
{
    public string studentID;
    public string LegalrepresentativeName;
    public string LegalrepresentativePhone;
    public string LegalrepresentativePhoneHome;
    public string LegalrepresentativePhoneMobile;
    public string LegalrepresentativePhoneFax;
    public string manualWhether;
    public string manualCategory1;
    public string manualGrade1;
    public string manualCategory2;
    public string manualGrade2;
    public string manualCategory3;
    public string manualGrade3;
    public string manualNo;
    public string manualUnit;
    public string studentManualImg;
    public string assistmanage;
    public string Accessory;
    public string assistmanageR;
    public string BrandR1;
    public string BuyingtimeR;
    public string BuyingPlaceR;
    public string InsertHospitalR;
    public string InsertDateR; //需刪除
    public string OpenHzDateR;
    public string assistmanageL;
    public string BrandL1;
    public string BuyingtimeL;
    public string BuyingPlaceL;
    public string InsertHospitalL;
    public string InsertDateL; //需刪除
    public string OpenHzDateL;
    public string edu;
    public string edu1;
    public string PandF1;
    public string PandF2;
    public string PandF3;
    public string PandF4;
    public string PandF5;
    public string PandF6;
    public string PandF7;
    public string startPlanDate;
    public string endPlanDate;
    public string ServiceDate1;
    public string Parent1;
    public string Teacher1;
    public string Sociality1;
    public string ListenTutor1;
    public string Manager1;
    public string RelationalPeople1;
    public string ServiceDate2;
    public string Parent2;
    public string Teacher2;
    public string Sociality2;
    public string ListenTutor2;
    public string Manager2;
    public string RelationalPeople2;

}
/*Staff*/
public struct CreateStaff
{
    public string ID;
    public string unit;
    public string staffID;
    public string staffName;
    public string officeDate;
    public string resignDate;
    public string applyJob;
    public string jobTitle;
    public string jobLevel;
    public string staffTWID;
    public string staffPhoto;
    public string comeCity;
    public string staffbirthday;
    public string staffsex;
    public string marriage;
    public int studentSex;
    public string censusAddress;
    public string censusAddressZip;
    public string censusCity;
    public string address;
    public string addressCity;
    public string addressZip;
    public string TDaytime;
    public string TNight;
    public string Phone;
    public string staffemail;
    public string EmergencyName;
    public string EmergencyAddress;
    public string EmergencyPhone;
    public string DSchoolName;
    public string DDepartment;
    public string DSince;
    public string DUntil;
    public string study1;
    public string MSchoolName;
    public string MDepartment;
    public string MSince;
    public string MUntil;
    public string study2;
    public string USchoolName;
    public string UDepartment;
    public string USince;
    public string UUntil;
    public string study3;
    public string VSchoolName;
    public string VDepartment;
    public string VSince;
    public string VUntil;
    public string study4;
    public string JDateSince1;
    public string JDateUntil1;
    public string JCname1;
    public string Jposition1;
    public string Jsalary1;
    public string prove1;
    public string JTitle1;
    public string JTitleName1;
    public string JDateSince2;
    public string JDateUntil2;
    public string JCname2;
    public string Jposition2;
    public string Jsalary2;
    public string prove2;
    public string JTitle2;
    public string JTitleName2;
    public string JDateSince3;
    public string JDateUntil3;
    public string JCname3;
    public string Jposition3;
    public string Jsalary3;
    public string prove3;
    public string JTitle3;
    public string JTitleName3;
    public string JDateSince4;
    public string JDateUntil4;
    public string JCname4;
    public string Jposition4;
    public string Jsalary4;
    public string prove4;
    public string JTitle4;
    public string JTitleName4;
    public List<string[]> FamilyStatu;
    public string bailName;
    public string bailUnit;
    public string bailJob;
    public string bailRelationship;
    public string bailContact;
    public string bailContactTime;
    public string recruited;
    public string recruitedText;
    public string langAbility1;
    public string langL1;
    public string langS1;
    public string langR1;
    public string langW1;
    public string langAbility2;
    public string langL2;
    public string langS2;
    public string langR2;
    public string langW2;
    public string langAbility3;
    public string langL3;
    public string langS3;
    public string langR3;
    public string langW3;
    public List<string[]> SpecialtySkill;
    public string disease;
    public string diseaseText;
    public string fillInDate;
}
public struct StaffResult
{
    public Int64 ID;
    public CreateStaff StaffBaseData;
    public List<staffWorkData> WorkData;
    public string checkNo;
    public string errorMsg;
}

public struct staffWorkData
{
    public string ID;
    public string staffID;
    public string RecordDate;
    public string Record;
    public string RecordRemark;
    public string Type;
}
public struct StaffMeritDataList
{
    public StaffDataList sData;
    public string MID;
    public int AY;
    public int sUnit; //有可能他調職，紀錄之前的
    public int AScore;
    public int PScore;
    public int WScore;
    public int AddScore;
    public int LowerScore;
}
public struct CreateRemind {
    public string rType;
    public string recipient;
    public string recipientID;
    public string executionContent;
    public string executionDate;
    public string fulfillmentDate;
    public string designee;
    public string designeeDate;

}
public struct CreateStaffUpgrade
{
    public string ID;
    public string courseDate;
    public string courseLecturer;
    public string courseName;
    public string courseTime;
    public string courseProve;
    public string courseCredit;
    public string otherExplanation;
    public List<StaffDataList> Participants;
    public string checkNo;
    public string errorMsg;
}
public struct CreateStaffUpgradeSeries
{
    public string ID;
    public string author;
    public string articleDate;
    public string serialNumber;
    public string seriesTitle;
    public string volume;
    public string articleTitle;
    public string articleType;
    public string checkNo;
    public string errorMsg;
}

public struct CreateNewTeacher
{
    public string ID;
    public string Unit;
    public string officeDate;
    public string teacher;
    public string teacherName;
    public string evaluationDate;
    public string reportPer;
    public string basicPer;
    public string teachPer;
    public string rated;
    public string contentMerit;
    public string contentDefect;
    public string useMerit;
    public string useDefect;
    public string langMerit;
    public string langDefect;
    public string skillMerit;
    public string skillDefect;
    public string ExchangeMerit;
    public string ExchangeDefect;
    public string AdvisoryMerit;
    public string AdvisoryDefect;
    public string OverallMerit;
    public string OverallDefect;
    public string director;
    public string prison;
    public string trainPerson;
    public string file;
    public string directorName;
    public string prisoNamen;
    public string trainPersonName;
    public string fileName;
    public string checkNo;
    public string errorMsg;
}
/*
 public struct Temperature
{
    public string ID;
    public string StudentID;
    public string ParentTemperature;
    public string StudentTemperature;
    public string LeaveItem;
    public string LeaveState;
    //public string CreateFileBy;
    public string CreateFileDate;
    public string Day;
    //public string UpFileBy;
    //public string UpFileDate;
    //public string isDeleted;
    public string checkNo;
    public string errorMsg;
}
 */
public struct CreateTemperatureSystem
{
    public string tID;
    public string txtpeopleID;
    public string StudentName;
    public string peopleTemp;
    public string parentsTemp;
    public string leaveItem;
    public string leaveStatus;
    public string Remark;
    public string tempDate;
    public string checkNo;
    public string errorMsg;
    public string Year;
    public string Month;
    public string Day;
}

public struct CreateTeacherSystem
{
    public string tID;
    public string txtpeopleID;
    public string TeacherName;
    public string TeacherTemp;
    public string CheckContent;
    public string errorMsg;
    public string Year;
    public string Month;
    public string Day;
}

public struct CreateBook
{
    public string bID;
    public string bookNumber;
    public string bookTitle;
    public string bookClassification;
    public string bookClassificationCode;
    public string bookClassificationName;
    public string bookAuthor;
    public string bookPress;
    public string bookPressDate;
    public string bookRemark;
    public string bookFilingDate;
    public string bookStatus;
    public string checkNo;
    public string errorMsg;
}

public struct CreateBookResult
{
    public string bID;
    public string bookNumber;
    public string bookTitle;
    public string bookClassificationCode;
    public string bookClassificationName;
    public string bookAuthor;
    public string bookPress;
    public string checkNo;
    public string errorMsg;
}

public struct CreateBookUser
{
    public string bID;
    public string borrowStatus;
    public string borrowerClassID;
    public string borrowerName;
    public string borrowerID;
    public string borrowerStatus;
    public string checkNo;
    public string errorMsg;
}

public struct CreateBookSystem
{
    public string bID;
    public string borrowStatus;
    public string borrowerClassID;
    public string borrowerName;
    public string borrowerID;
    public string borrowerStatus;
    public string bookCode;
    public string bookReturnCode;
    public string bookName;
    public string borrowDate;
    public string expireDate;
    public string restoreDate;
    public string checkNo;
    public string errorMsg;
}

public struct CreateBookRecordBorrower
{
    public string borrowStatus;
    public string borrowerClassID;
    public string borrowerName;
    public string borrowerID;
    public string borrowerStatus;
    public string borrowQuantity;
    public string checkNo;
    public string errorMsg;
}

public struct CreateClassification
{
    public string cID;
    public string classificationCode;
    public string classificationName;
    public string checkNo;
    public string errorMsg;
}

public struct CreateBookBorrow
{
    public string studentBorrowBookSum;
    public string studentBorrowerSum;
    public string staffBorrowBookSum;
    public string staffBorrowerSum;
    public string bookBorrowStartDate;
    public string bookBorrowEndDate;
    public string checkNo;
    public string errorMsg;
}

public struct CreateStationery
{
    public string sID;
    public string Unit;
    public string stationeryID;
    public string stationeryName;
    public string stationeryUnit;
    public string safeQuantity;
    public string stationeryType;
    public string remark;
    public string recentPrice;
    public string inventory;
    public string checkNo;
    public string errorMsg;
}

public struct CreatePurchase
{
    public string pID;
    public string purchaseID;
    public string purchaseDate;
    public string firmName;
    public string firmTel;
    public string stationeryID;
    public string Unit;
    public string stationeryQuantity;
    public string stationeryPrice;
    public string stationeryName;
    public string stationeryUnit;
    public string stationeryType;
    public string checkNo;
    public string errorMsg;
}

public struct CreateReceive
{
    public string rID;
    public string Unit;
    public string receiveID;
    public string receiveDate;
    public string receiveByID;
    public string receiveByName;
    public string rstationeryID;
    public string receiveQuantity;
    public string receiveRemark;
    public string rstationeryName;
    public string rstationeryUnit;
    public string checkNo;
    public string errorMsg;
}

public struct CreateScrap
{
    public string sID;
    public string scrappedID;
    public string Unit;
    public string scrappedDate;
    public string scrappedByID;
    public string scrappedByName;
    public string sstationeryID;
    public string scrappedQuantity;
    public string scrappedRemark;
    public string sstationeryName;
    public string sstationeryUnit;
    public string checkNo;
    public string errorMsg;
}

public struct CreateReturn
{
    public string rID;
    public string returnedID;
    public string Unit;
    public string returnedDate;
    public string getgoodsDate;
    public string getgoodsByID;
    public string getgoodsByName;
    public string restationeryID;
    public string returnedQuantity;
    public string returnedReason;
    public string restationeryName;
    public string restationeryUnit;
    public string checkNo;
    public string errorMsg;
}

public struct CreateApplyProperty
{
    public string ID;
    public string Unit;
    public string applyDate;
    public string applyID;
    public string applyType;
    public string applyPay;
    public string applyByID;
    public string applyBy;
    public string applyStatus;
    public string applySum;
    public List<PropertyDetailData> DetailArray;
    public string checkNo;
    public string errorMsg;
}
public struct PropertyDetailData
{
    public string pID;
    public string Name;
    public string Unit;
    public string Quantity;
    public string Format;
    public string Price;
    public string Explain;
    public string Bill;
    public string Sum;
    public string aID;
    public string checkNo;
    public string errorMsg;
}

public struct CreatePropertyRecord
{
    public string repairID;
    public string Unit;
    public string fillInDate;
    public string propertyStatus;
    public string propertyChange;
    public string changeStatus;
    public string propertyID;
    public string propertyCode;
    public string applyID;
    public string propertyCategory;
    public string propertyName;
    public string propertyLabel;
    public string propertyUnit;
    public string propertyQuantity;
    public string propertyFitting;
    public string propertyLocation;
    public string propertyCustody;
    public string stopDate;
    public string propertySummons;
    public string propertyReceipt;
    public string propertyAccounting;
    public string inputDate;
    public string outputDate;
    public string fundSource;
    public string fundAssist;
    public string fundDonate;
    public string buyDate;
    public string Remnants;
    public string buySource;
    public string userYear;
    public string propertyPrice;
    public string Depreciation;
    public string selfFunds;
    public string Purchaser;
    public string PurchaserName;
    public string Grant;
    public string Remark;
    public string attachment1;
    public string attachment2;
    public string attachment3;
    public string attachment4;
    public string attachment5;
    public string attachment6;
    public string attachment7;
    public string attachment8;
    public List<PropertyChangesExplainData> ChangesArray;
    public string checkNo;
    public string errorMsg;
}
public struct PropertyChangesExplainData
{
    public string cID;
    public string recordID;
    public string moveDate;
    public string moveAbstract;
    public string relatedBy;
    public string relatedByID;
}


public struct CreateAudiometryAppointment
{
    public string ID;
    public string appDate;
    public string startTime;
    public string endTime;
    public string authorID;
    public string studentID;
    public string item;
    public string itemExplain;
    public string sContent;
    public string State;
    public string AssessWho1;
    public string AssessWho2;
    public string Unit;

    }
public struct CreateHearingAssessment
{
    public string ID;
    public string caseUnit;
    public string checkDate;
    public string studentID;
    public string studentName;
    public string studentbirthday;
    public string studentAge;
    public string studentMonth;
    public string studentUseAids;
    public string assistmanageR;
    public string brandR;
    public string modelR;
    public string buyingPlaceR;
    public string buyingtimeR;
    public string insertHospitalR;
    public string openHzDateR;
    public string assistmanageL;
    public string brandL;
    public string modelL;
    public string buyingtimeL;
    public string buyingPlaceL;
    public string insertHospitalL;
    public string openHzDateL;
    public string detectionPurposes;
    public string detectionPurposesText;
    public string explain;
    public string observation;
    public string observationExplain;
    public string checkR;
    public string checkL;
    public string checkRecibelR;
    public string checkLossR;
    public string checkCategoryR;
    public string checkRecibelL;
    public string checkLossL;
    public string checkCategoryL;
    public string checkResult;
    public string checkAidsResultR;
    public string checkAidsResultRText;
    public string checkAidsResultL;
    public string checkAidsResultLText;
    public string effectR;
    public string effectRText;
    public string effectL;
    public string effectLText;
    public string effectOther;
    public string effectOtherText;
    public string suggestion;
    public string suggestionHour;
    public string suggestion2;
    public string suggestion3;
    public string suggestion3Text;
    public string checkNextDate;
    public string audiologist;
    public string audiologistName;
    public string checkNo;
    public string errorMsg;
}

public struct CreateHearingInspection
{
    public string ID;
    public string caseUnit;
    public string checkDate;
    public string studentID;
    public string studentName;
    public string studentbirthday;
    public string checkMode;
    public string credibility;
    public string assistmanageR;
    public string brandR;
    public string modelR;
    public string buyingPlaceR;
    public string buyingtimeR;
    public string insertHospitalR;
    public string openHzDateR;
    public string assistmanageL;
    public string brandL;
    public string modelL;
    public string buyingtimeL;
    public string buyingPlaceL;
    public string insertHospitalL;
    public string openHzDateL;
    public string checkInstrument;
    public string headphone;
    public string nudetonety;
    public string hearingtone;
    public string hearingtoneImg;
    public string toneR;
    public string toneL;
    public string hearingtoneR;
    public string hearingtoneRText;
    public string hearingtoneL;
    public string hearingtoneLText;
    public string hearingInstrument;
    public string hearingImgR;
    public string hearingImgL;
    public string hearingVolumeR;
    public string hearingVolumeL;
    public string conformR;
    public string conformL;
    public string pressureR;
    public string pressureL;
    public string aidsR;
    public string aidsdetectR;
    public string aidsL;
    public string aidsdetectL;
    public string aidsOther;
    public string material1;
    public string voice1;
    public string state1;
    public string volume1;
    public string result1;
    public string remark1;
    public int material2;
    public string voice2;
    public string state2;
    public string volume2;
    public string result2;
    public string remark2;
    public string project3;
    public string material3;
    public string voice3;
    public string state3;
    public string volume3;
    public string result3;
    public string remark3;
    public string project4;
    public string material4;
    public string voice4;
    public string state4;
    public string volume4;
    public string result4;
    public string remark4;
    public string explain;
    public string audiologist;
    public string audiologistName;
    public string checkNo;
    public string errorMsg;
    public string SATREarBefore;
    public string SATLEarBefore;
    public string SATEarBefore;
    public string SATREarAfter;
    public string SATLEarAfter;
    public string SATEarAfter;
    public string SATVolumeBefore;
    public string SATVolumeAfter;
    
    public string WRSVolumeBefore;
    public string WRSREarBefore;
    public string WRSLEarBefore;
    public string WRSEarBefore;
    public string WRSVolumeAfter;
    public string WRSREarAfter;
    public string WRSLEarAfter;
    public string WRSEarAfter;
    public string project3VolumeBefore;
    public string project3REarBefore;
    public string project3LEarBefore;
    public string project3EarBefore;
    public string project3VolumeAfter;
    public string project3REarAfter;
    public string project3LEarAfter;
    public string project3EarAfter;
    public string project4VolumeBefore;
    public string project4LEarBefore;
    public string project4REarBefore;
    public string project4EarBefore;
    public string project4VolumeAfter;
    public string project4REarAfter;
    public string project4LEarAfter;
    public string project4EarAfter;
    public int checkPurpose;
    public string checkPurposeText;
    public string VoiceItem3;
    public string VoiceItem4;
    
}
public struct setHearingTests
{
    public string ID;
    public string checkDate;
    public string voice;
    public string state;
    public string project;
    public string material;
    public string volume;
    public string result;
    public string remark;
    public string itemNumber; 
    public string checkNo;
    public string errorMsg;
}
public struct createAidsManage
{
    public string ID;
    public string Unit;
    public string aStatu;
    public string aID;
    public string assistmanage;
    public string brand;
    public string model;
    public string aNo;
    public string aSource;
    public string fillInDate;
    public string remark;
    public string checkNo;
    public string errorMsg;
    public List<createAidsManageLoan> LoanList;
    public List<createAidsManageService> ServiceList;
}
public struct createAidsManageLoan
{
    public string ID;
    public string mID;
    public string lendDate;
    public string lendPeople;
    public string lendPeopleID;
    public string returnDate;
    public string returnDate2;
    public string remark;
    public string checkNo;
    public string errorMsg;
}
public struct createAidsManageService
{
    public string ID;
    public string mID;
    public string serviceDate;
    public string serviceItem;
    public string serviceFirm;
    public string serviceFirmDate;
    public string sRemark;
    public string checkNo;
    public string errorMsg;
}
/*薪資管理*/

public struct CreateExternal
{
    public string ID;
    public string staffName;
    public string staffTWID;
    public string staffemail;
    public string censusAddress;
    public string censusAddressZip;
    public string censusCity;
    public string address;
    public string addressCity;
    public string addressZip;
    public string Phone;
    public string Phone2;
    public List<ExternalWorkData> WorkData;
    public string checkNo;
    public string errorMsg;
}

public struct ExternalWorkData
{
    public string ID;
    public string ExternalID;
    public string CourseDate1;
    public string CourseDate2;
    public string Course;
    public string CoursePrice;
}
public struct CreateStaffContractedSalary
{
    public string ID;
    public string Unit;
    public string staffID;
    public string staffName;
    public string fillInDate;
    public string laborInsurance;
    public string healthInsurance;
    public string pensionFunds;
    public string pensionFundsPer;
    public string withholdingTax;
    public string education;
    public string count1;
    public string years;
    public string count2;
    public string applyJob;
    public string jobLevel;
    public string count3;
    public string director;
    public string count4;
    public string special;
    public string count5;
    public string total;
    public string totalSalary;
    public string explanation;
    public string checkNo;
    public string errorMsg;
}
public struct CreateStaffSalary
{
    public string ID;
    public string Unit;
    public string yearDate;
    public string monthDate;
    public string staffID;
    public string staffName;
    public string fillInDate;
    public string laborInsurance;
    public string salaryExplain1;
    public string healthInsurance;
    public string salaryExplain2;
    public string pensionFunds;
    public string pensionFundsPer;
    public string salaryExplain3;
    public string withholdingTax;
    public string salaryExplain4;
    public string totalSalary;
    public string salaryExplain5;
    public string salaryDeductions;
    public string salaryExplain6;
    public string realWages;
    public List<StaffSalaryList> addItem;
    public List<StaffSalaryList> minusItem;
    public string checkNo;
    public string errorMsg;
}
public class StaffSalaryList
{
    public string ID;
    public string salaryID;
    public string project;
    public string projectMoney;
    public string explain;
    public string checkNo;
    public string errorMsg;
}
public struct AidsTypestruct
{
    public List<CreateAidsBrand> HearingList;
    public List<CreateAidsBrand> eEarList;
    public List<CreateAidsBrand> FMList;
    public string checkNo;
    public string errorMsg;
}
public struct CreateAidsBrand
{
    public string ID;
    public string brandType;
    public string brandName;
    public string checkNo;
    public string errorMsg;
}

public struct CreateFMAidsAssess
{
    public string ID;
    public string Unit;
    public string studentID;
    public string studentName;
    public string studentbirthday;
    public string audiologist;
    public string audiologistID;
    public string assessDate;
    public string aSource;
    public string aSourceText;
    public string fmModel;
    public string fmChannel;
    public string fmAidstypeR;
    public string DPAIsettingR;
    public string fmProgramR;
    public string fmAudioR;
    public string fmUIR;
    public string fmUITextR;
    public string fmReceptorR;
    public string fmVolumeR;
    public string fmAidstypeL;
    public string DPAIsettingL;
    public string fmProgramL;
    public string fmAudioL;
    public string fmUIL;
    public string fmUITextL;
    public string fmReceptorL;
    public string fmVolumeL;
    public string fm1L_1;
    public string fm1L_2;
    public string fm1L_3;
    public string fm1L_4;
    public string fm1R_1;
    public string fm1R_2;
    public string fm1R_3;
    public string fm1R_4;
    public string fm2L_1;
    public string fm2L_2;
    public string fm2L_3;
    public string fm2L_4;
    public string fm2R_1;
    public string fm2R_2;
    public string fm2R_3;
    public string fm2R_4;
    public string fm3L_1;
    public string fm3L_2;
    public string fm3L_3;
    public string fm3L_4;
    public string fm3R_1;
    public string fm3R_2;
    public string fm3R_3;
    public string fm3R_4;
    public string fm4L_1;
    public string fm4L_2;
    public string fm4L_3;
    public string fm4L_4;
    public string fm4R_1;
    public string fm4R_2;
    public string fm4R_3;
    public string fm4R_4;
    public string fm5L_1;
    public string fm5L_2;
    public string fm5L_3;
    public string fm5L_4;
    public string fm5R_1;
    public string fm5R_2;
    public string fm5R_3;
    public string fm5R_4;
    public string fm6L_1;
    public string fm6L_2;
    public string fm6L_3;
    public string fm6L_4;
    public string fm6R_1;
    public string fm6R_2;
    public string fm6R_3;
    public string fm6R_4;
    public string result;
    public string testmaterial;
    public string true1L_1;
    public string true1L_2;
    public string true1L_3;
    public string true1R_1;
    public string true1R_2;
    public string true1R_3;
    public string true1D_1;
    public string true1D_2;
    public string true1D_3;
    public string testmaterial2;
    public string true2L_1;
    public string true2L_2;
    public string true2L_3;
    public string true2R_1;
    public string true2R_2;
    public string true2R_3;
    public string true2D_1;
    public string true2D_2;
    public string true2D_3;
    public string result2;
    public string checkNo;
    public string errorMsg;
}
//--------------------------------------end Create Data Structure---------------
public struct setTeachISP1
{
    public Int64 Column;
    public string Unit;
    public string studentID;
    public string studentName;
    public string studentSex;
    public string studentbirthday;
    public string LegalrepresentativeName;
    public string LegalrepresentativePhone;
    public string LegalrepresentativePhoneHome;
    public string LegalrepresentativePhoneMobile;
    public string LegalrepresentativePhoneFax;
    public string manualWhether;
    public string manualCategory1;
    public string manualGrade1;
    public string manualCategory2;
    public string manualGrade2;
    public string manualCategory3;
    public string manualGrade3;
    public string manualNo;
    public string manualUnit;
    public string studentManualImg;
    public string assistmanage;
    public string Accessory;
    public string assistmanageR;
    public string BrandR1;
    public string BuyingtimeR;
    public string BuyingPlaceR;
    public string InsertHospitalR;
    public string InsertDateR; //需刪除
    public string OpenHzDateR;
    public string assistmanageL;
    public string BrandL1;
    public string BuyingtimeL;
    public string BuyingPlaceL;
    public string InsertHospitalL;
    public string InsertDateL; //需刪除
    public string OpenHzDateL;
    public string edu;
    public string edu1;
    public string PandF1;
    public string PandF2;
    public string PandF3;
    public string PandF4;
    public string PandF5;
    public string PandF6;
    public string PandF7;
    public string startPlanDate;
    public string endPlanDate;
    public string ServiceDate1;
    public string Parent1;
    public string Teacher1;
    public string Sociality1;
    public string ListenTutor1;
    public string Manager1;
    public string RelationalPeople1;
    public string ServiceDate2;
    public string Parent2;
    public string Teacher2;
    public string Sociality2;
    public string ListenTutor2;
    public string Manager2;
    public string RelationalPeople2;

}
public struct setTeachISP2 {

    public string ID;
    public string ISPID;
    public string PlanWriter;
    public string PlanWriterName;

    public string FrameDate;
    public string PlanExecutor;

    public string PlanRevise;
    public string PlanReviseName;

    public string ReviseDate;
    public string ReviseExecutor;
    public string EconomicNeedResource;
    public string EconomicNeedSituation;
    public string ServicesResource;
    public string ServicesSituation;
    public string ServicesActiivity;
    public string ServicesStatus;
    public string MedicalResource;
    public string MedicalSituation;
    public string MedicalReason;
    public string MedicalOther;
    public string EducationResource;
    public string EducationSituation;
    public string EducationOther;
    public string HSMID;
    public string MasterOrder;
    public string Target;
    public string DetailOrder;
    public string Manner;
    public string StartDate;
    public string EndDate;
    public string Executor;
    public string TrackDate;
    public string Results;
}
public struct setTeachISP3
{
   
    public string ISPID;
    public string PlanWriter2;
    public string PlanWriter2Name;
    public string PlanWriteFrameDate2;
    public string PlanWriteExecutor2;
    public string PlanRevise2;
    public string PlanRevise2Name;
    public string PlanReviseDate2;
    public string PlanReviseExecutor2;
    public string Naked250L;
    public string Naked250R;
    public string Naked250;
    public string Naked500L;
    public string Naked500R;
    public string Naked500;
    public string Naked1000L;
    public string Naked1000R;
    public string Naked1000;
    public string Naked2000L;
    public string Naked2000R;
    public string Naked2000;
    public string Naked4000L;
    public string Naked4000R;
    public string Naked4000;
    public string Naked8000L;
    public string Naked8000R;
    public string Naked8000;
    public string NakedAverageL;
    public string NakedAverageR;
    public string NakedAverage;
    public string After250L;
    public string After250R;
    public string After250;
    public string After500L;
    public string After500R;
    public string After500;
    public string After1000L;
    public string After1000R;
    public string After1000;
    public string After2000L;
    public string After2000R;
    public string After2000;
    public string After4000L;
    public string After4000R;
    public string After4000;
    public string After8000L;
    public string After8000R;
    public string After8000;
    public string AfterAverageL;
    public string AfterAverageR;
    public string AfterAverage;
    public string AudiometryOther;
    public string AudiometryAssessmentBy;
    public string AudiometryAssessmentByName;
    public string AudiometryAssessmentDate;
    public string AudiometrygAssessmentScoring;

    public string HearingAssessmentBy;
    public string HearingAssessmentByName;
    public string HearingAssessmentDate;
    public string HearingAssessmentScoring1;
    public string HearingAssessmentScoring2;
    public string AidsState1;
    public string AidsState2;
    public string AidsState3;
    public string AidsState4;
    public string AidsState5;
    public string AidsAssessmentBy;
    public string AidsAssessmentByName;
    public string AidsAssessmentDate;
    public string AidsAssessmentScoring;
    public string Summary;

    public List<HearingManagerDetail> HearingManagerDetail;
   
}

public struct HearingManagerDetail//聽力學下方答案
{
    public string ID;
    public string ISPID;
    public string PlanOrder;
    public string DetailOrder;
    public string HMDateStart;
    public string HMDateEnd;
    public string HMADate;
    public string HMAMode;
    public string HMAResult;
    public string HMTeachingDecision;
}

public struct TeachingPlan
{ 
    //教學主檔
    public string ID;
    public string TeachOrder;
    public string MasterOrder;
    public string TargetLong;
    public List<TeachingPlanDetail> TeachingPlanDetail;
}

public struct TeachingPlanDetail
{
    //教學明細
    public string ID;
    public string TPMID;
    public string DetailOrder;
    public string TargetShort;
    public string DateStart;
    public string DateEnd;
    public string EffectiveDate;
    public string EffectiveMode;
    public string EffectiveResult;
    public string Decide;
}
public struct ISPLong
{
    public Int64 ISPColumn;
    public Int64 LongColumn;
    public string Content;
    public int Domain;
    public List<ISPShort> ShortTarget;
}
public struct ISPShort
{
    public Int64 LongColumn;
    public Int64 ShortColumn;
    public string Content;
}
public struct setTeachISP4
{
    public string ISPID;

    public string PlanWriter3;
    public string PlanWriter3Name;

    public string PlanWriteFrameDate3;
    public string PlanWriteExecutor3;
    public string PlanRevise3;
    public string PlanRevise3Name;

    public string PlanReviseDate3;
    public string PlanReviseExecutor3;
    public string HearingAssessment;
    public string HearingAssessmentBy;
    public string HearingAssessmentDate;
    public string HearingAssessmentTool;
    public string VocabularyAssessment;
    public string VocabularyAssessmentBy;
    public string VocabularyAssessmentDate;
    public string VocabularyAssessmentTool;
    public string LanguageAssessment;
    public string LanguageAssessmentBy;
    public string LanguageAssessmentDate;
    public string LanguageAssessmentTool;
    public string intelligenceAssessment;
    public string intelligenceAssessmentBy;
    public string intelligenceAssessmentDate;
    public string intelligenceAssessmentTool;
    public string OtherAssessment;
    public string OtherAssessmentBy;
    public string OtherAssessmentDate;
    public string OtherAssessmentTool;
    public string Hearing;
    public string CognitiveAbility;
    public string ConnectAbility;
    public string ActAbility;
    public string Relationship;
    public string EmotionalManagement;
    public string SensoryFunction;
    public string HealthState;
    public string DailyLiving;
    public string LearningAchievement;
    public string Advantage;
    public string WeakCapacity;

    public string HearingAssessmentByName;
    public string VocabularyAssessmentByName;
    public string LanguageAssessmentByName;
    public string intelligenceAssessmentByName;
    public string OtherAssessmentByName;



    public List<TeachingPlan> TeachingPlan;
   
}
public struct setTeachISPAllData {
    public setTeachISP1 ISP1Data;
    public setTeachISP2 ISP2Data;
    public setTeachISP3 ISP3Data;
    public setTeachISP4 ISP4Data;
}
public struct setCoursePlan {
    public Int64 Column;
    public int unitNum;
    public string classIDName;
    public int classID;
    public string teacherIDName;
    public int teacherID;
    public string courseIDName;
    public int courseID;
    public DateTime startPlanDate;
    public DateTime endPlanDate;
    public List<ISPLong> targetDomain;
}

public class RolesStruct
{
    public string[] caseStu = new string[2] { "0000", "0" };
    public string[] hearing = new string[2] { "0000", "0" };
    public string[] teach = new string[2] { "0000", "0" };
    public string[] salary = new string[2] { "0000", "0" };
    public string[] attendance = new string[2] { "0000", "0" };
    public string[] personnel = new string[2] { "0000", "0" };
    public string[] apply = new string[2] { "0000", "0" };
    public string[] property = new string[2] { "0000", "0" };
    public string[] library = new string[2] { "0000", "0" };
    public string[] serviceFees = new string[2] { "0000", "0" };
    public string[] caseBT = new string[2] { "0000", "0" };
    public string[] teachBT = new string[2] { "0000", "0" };
    public string[] stationery = new string[2] { "0000", "0" };
    public string[] remind = new string[2] { "0000", "0" };
    public string checkNo;
    public string errorMsg;
}

public struct StudentDataBasic
{
    public string ID;
    public string sUnit;
    public string studentID;
    public string studentName;
    public string studentbirthday;
    public string studentSex;
    public string assistmanageR;
    public string brandR;
    public string modelR;
    public string buyingPlaceR;
    public string buyingtimeR;
    public string insertHospitalR;
    public string openHzDateR;
    public string assistmanageL;
    public string brandL;
    public string modelL;
    public string buyingtimeL;
    public string buyingPlaceL;
    public string insertHospitalL;
    public string openHzDateL;
    public string checkNo;
    public string errorMsg;
}

/*****教學管理 開始 *******/

public struct CreaHearing_Loss_Tool
{
    public string SummeryID;
    public string SummeryDescription;
    public string QuestionID;
    public string QuestionDescription;
    public string Category;
    
}

public struct CreaHearing_Loss_Skill
{
    public string SkillID;
    public string Title;
    public string SkillDescription;

}

public struct UpdateHearLoss
{
    
    public string Date;
    public string tool;
    public string anser;
    public string page;
}

public struct AchievementAssessment //成就評估(個案評估)
{
    public string ID;
    public string Unit;
    public string StudentID;
    public string AcademicYear;
    public string AssessedTheReasons;
    public string AssessedTheReasonsText;
    public string LAidsHearing;
    public string RAidsHearing;
    public string StudentAge;
    public string StudentMonth;
    public string Intelligence_Type;
    public string Intelligence_Date;
    public string Intelligence_Rater;
    public string Intelligence_StudentAge;
    public string Intelligence_RawScore;
    public string Intelligence_ScorePer;
    public string Intelligence_Grade;
    public string Intelligence_Result;
    public string AuditorySkills_Date;
    public string AuditorySkills_Rater;
    public string AuditorySkills_Text1;
    public string AuditorySkills_Text2;
    public string AuditorySkills_Summary;
    public string Vocabulary_Type;
    public string Vocabulary_Date;
    public string Vocabulary_Rater;
    public string Vocabulary_StudentAge;
    public string Vocabulary_RawScore;
    public string Vocabulary_ScorePer;
    public string Vocabulary_Result;
    public string Vocabulary1_Date;
    public string Vocabulary1_Rater;
    public string Vocabulary1_StudentAge;
    public string Vocabulary1_RawScore1;
    public string Vocabulary1_Score1;
    public string Vocabulary1_ScorePer1;
    public string Vocabulary1_Grade;
    public string Vocabulary1_RawScore2;
    public string Vocabulary1_Score2;
    public string Vocabulary1_ScorePer2;
    public string Vocabulary1_Grade2;
    public string Vocabulary1_RawScore3;
    public string Vocabulary1_Score3;
    public string Vocabulary1_ScorePer3;
    public string Vocabulary1_Grade3;
    public string Vocabulary1_RawScore4;
    public string Vocabulary1_Score4;
    public string Vocabulary1_ScorePer4;
    public string Vocabulary1_RawScore5;
    public string Vocabulary1_Score5;
    public string Vocabulary1_ScorePer5;
    public string Vocabulary1_RawScore6;
    public string Vocabulary1_Score6;
    public string Vocabulary1_ScorePer6;
    public string Vocabulary1_RawScore7;
    public string Vocabulary1_Score7;
    public string Vocabulary1_ScorePer7;
    public string Vocabulary1_Text;
    public string Vocabulary1_Summary;
    public string Language1_Date;
    public string Language1_Rater;
    public string Language1_StudentMonth;
    public string Language1_RawScore;
    public string Language1_ScorePer;
    public string Language1_Result;
    public string Language1_Summary;
    public string Language2_Date;
    public string Language2_Rater;
    public string Language2_StudentAge;
    public string Language2_RawScore1;
    public string Language2_ScorePer1;
    public string Language2_Grade1;
    public string Language2_RawScore2;
    public string Language2_ScorePer2;
    public string Language2_Grade2;
    public string Language2_RawScore3;
    public string Language2_ScorePer3;
    public string Language2_Grade3;
    public string Language2_Summary;
    public string Language3_Date;
    public string Language3_Rater;
    public string Language3_StudentAge1;
    public string Language3_RawScore1;
    public string Language3_ScorePer1;
    public string Language3_Degree1;
    public string Language3_Grade1;
    public string Language3_RawScore2;
    public string Language3_ScorePer2;
    public string Language3_Degree2;
    public string Language3_Grade2;
    public string Language3_RawScore3;
    public string Language3_ScorePer3;
    public string Language3_Degree3;
    public string Language3_Grade3;
    public string Language3_Summary1;
    public string Language3_Text;
    public string Summary;
    public string CreateFileBy;
    public DateTime CreateFileDate;
    public string UpFileBy;
    public string UpFileDate;
    public string isDeleted;



}

public struct SearchAchievementAssessment
{
    public string RowNum;
    public string txtAcademicYear;
    public Int64 ID;
    public string txtstudentID;
    public string txtstudentName;
    public int txtstudentSex;
    public DateTime txtstudentbirthday;

    public DateTime WriteDate;
    public string StudentAge;
    public string StudentMonth;
    
    public string checkNo;
    public string errorMsg;
}


public struct AchievementAssessmentLoad //成就評估(個案評估)--讀取使用 by AWho 
{
    public string IDname;
    public string ThisValue;
}

public struct CaseStudy
{
    public string ID;
    public string Unit;
    public string StudentID;
    public string WriteName;
    public string WriteNameName;
    public string WriteDate;
    public string TeacherScore1;
    public string ParentScore1;
    public string TeacherScore2;
    public string ParentScore2;
    public string TeacherScore3;
    public string ParentScore3;
    public string TeacherScore4;
    public string ParentScore4;
    public string TeacherScore5;
    public string ParentScore5;
    public string TeacherScore6;
    public string ParentScore6;
    public string TeacherScore7;
    public string ParentScore7;
    public string TeacherScore8;
    public string ParentScore8;
    public string TeacherScore9;
    public string ParentScore9;
    public string TeacherScore10;
    public string ParentScore10;
    public string TeacherScore11;
    public string ParentScore11;
    public string TeacheRemark;
    public string ParentRemark;
    public string OtherRemark;
    public string WriteName1;
    public string WriteName1Name;
    public string RecordedByName;
    public string RecordedBy;
    public string RecordedDateTime;
    public string Recorded;
    public string Title1;
    public string Participants1;
    public string Title2;
    public string Participants2;
    public string Title3;
    public string Participants3;
    public string Title4;
    public string Participants4;
    public string Title5;
    public string Participants5;
    public string Title6;
    public string Participants6;
    public string CreateFileBy;
    public string CreateFileDate;
    public string UpFileBy;
    public string UpFileDate;
    public string isDeleted;


}

public struct CaseISPRecord
{ 
     public string ID;
 public string StudentID;
 public string WriteName;
 public string TeacherID;
 public string ConventionName;
 public string ConventionDate;
 public string ConventionFrameTime;
 public string ConventionOverTime;
 public string ConventionPlace;
 public string PlanExecutionFrameDate;
 public string PlanExecutionOverDate;
 public string Record;
 public string ParticipantParent;
 public string ParticipantTeache;
 public string ParticipantSocialWorker;
 public string ParticipantAudiologist;
 public string ParticipantHead;
 public string ParticipantProfessionals;
 public string Remark;
 public string CreateFileBy;
 public string CreateFileDate;
 public string UpFileBy;
 public string UpFileDate;
 public string isDeleted;


}

public struct SearchCaseISPRecord
{
    public string txtstudentName;
    public string txtteacherName;
    public string txtConventionDatestart;
    public string txtConventionDateend;
}


public struct ShowCaseISPRecord
{
    public string RowNum;
    public string ID;
    public string StudentID;
    public string StudentName;
    public string WriteName;
    public string TeacherID;
    public string TeacherName;
    public string ConventionName;
    public string ConventionDate;
    public string ConventionFrameTime;
    public string ConventionOverTime;
    public string ConventionPlace;
    public string PlanExecutionFrameDate;
    public string PlanExecutionOverDate;
    public string Record;
    public string ParticipantParent;
    public string ParticipantTeache;
    public string ParticipantSocialWorker;
    public string ParticipantAudiologist;
    public string ParticipantHead;
    public string ParticipantTeacheName;
    public string ParticipantSocialWorkerName;
    public string ParticipantAudiologistName;
    public string ParticipantHeadName;
    public string ParticipantProfessionals;
    public string Remark;
    public string CreateFileBy;
    public string CreateFileDate;
    public string UpFileBy;
    public string UpFileDate;
    public string isDeleted;
    //增加錯誤訊息
    public string checkNo;
    public string errorMsg;


}

public struct VoiceDistance
{
    public string RowNum;
    public string ID;
    public string StudentID;
    public string StudentName;
    public string StudentAge;
    public string StudentMonth;
    public string AcademicYear;
    public string AcademicTerm;
    public string remark;
    public DateTime Date;
    public string ListOrder;
    public string up;
    public string Question;
    public string Anser;
    public string HidID;
    public DateTime txtstudentbirthday;
    //增加錯誤訊息
    public string checkNo;
    public string errorMsg;
}


public struct TeachServiceSupervisor
{
    public string RowNum;
    public string ID;
    public string Unit;
    public string AcademicYear;
    public string ClassDate;
    public string TeacherID;
    public string TeacherName;
    public string SupervisorName;
    public string StudentID;
    public string StudentName;
    public string StudentAge;
    public string StudentMonth;
    public string ChecklistPurpose;
    public string ChecklistPurposeOther;
    public string CourseType;
    public string CourseTypeOther;
    public string CaseChoose;
    public string CaseChooseReason;
    public string CaseChooseOther;
    public string SupervisorType;
    public string SupervisorTypeOther;
    public string Remark;
    public string Result;
    public string ResultScore;
    public string Following;
    public string Resolution;
    public string CreateFileBy;
    public string CreateFileDate;
    public string UpFileBy;
    public string UpFileDate;
    public string isDeleted; 

    public DateTime txtstudentbirthday;
    //增加錯誤訊息
    public string checkNo;
    public string errorMsg;
}

public struct TeachServiceInspect
{
    public string RowNum;

    public string ID;
    public string Unit;
    public string ClassType;
    public string AcademicYear;
    public string StudentID;
    public string StudentName;

    public string ClassNameID;
    public string ClassIDName;

    public string TeacherID;
    public string TeacherName;

    public string StudentAgeFrom;
    public string StudentAgeTo;
    public string InspectDate;
    public string CourseType;
    public string CourseOther;
    public string Score1;
    public string Score2;
    public string Score3;
    public string Score4;
    public string Score5;
    public string Score6;
    public string Score7;
    public string Score8;
    public string Score9;
    public string Score10;
    public string Score11;
    public string Score12;
    public string Score13;
    public string Score14;
    public string Score15;
    public string Score16;
    public string Score17;
    public string Score18;
    public string Score19;
    public string Score20;
    public string Score21;
    public string Score22;
    public string Score23;
    public string Score24;
    public string Score25;
    public string Score26;
    public string Score27;
    public string Score28;
    public string Score29;
    public string Score30;
    public string Score31;
    public string Score32;
    public string Score33;
    public string Score34;
    public string Score35;
    public string Score36;
    public string SumScore;
    public string SupervisorName;
    public string CourseDate;
    public string Director;
    public string DirectorName; 
    public string Date;
    public string CreateFileBy;
    public string CreateFileDate;
    public string UpFileBy;
    public string UpFileDate;
    public string isDeleted;

    public DateTime txtstudentbirthday;
    //增加錯誤訊息
    public string checkNo;
    public string errorMsg;
}


public struct SingleClassShortTerm
{
    public string RowNum;
    public string ID;
    public string Unit;
    public string studentID;
    public string studentName;
    public string teacherName;
    public string teacherID;
    public string PlanDateStart;
    public string PlanDateEnd;
    public string Remark;
    public List<SingleClassShortTermTarget> SingleClassShortTermTarget;
}


public struct SingleClassShortTermTarget
{
    public string ID;
    public string PlanOrder;
    public string DetailOrder;
    public string SCSTID;
    public string TPDID;
    public string TargetMain;
    public string TargetContent;
    public string PlanExecutionDate1;
    public string PlanExecutionDate2;
    public string PlanExecutionDate3;
    public string PlanExecutionDate4;
    public string PlanExecutionDate5;
    public string Assessment1;
    public string Assessment2;
    public string Assessment3;
    public string Assessment4;
    public string Assessment5;
    public string Performance1;
    public string Performance2;
    public string Performance3;
    public string Performance4;
    public string Performance5;


}


public struct TeacherSchudule
{
    public string ID;
    public string TeacherID;
    public string Date;
    public string StartTime;
    public string EndTime;
    public string CreateDateTime;
    public string isDeleted;
    public string Unit;
    public string ClassID;
    public List<TeacherSchuduleStudent> TeacherSchuduleStudent;
}
public struct TeacherSchuduleStudent
{
    public string ID;
    public string TeacherScheduleID;
    public string StudentID;


}

/*****教學管理 結束 *******/


/****  人事  ****/
public struct WorkRecordManagePeople
{

    public string StaffID;
    public string StaffName;
    public List<WorkRecord> WorkRecord;
    public List<WorkRecordManage> WorkRecordManage;

}

public struct WorkRecord
{
    public string StaffID;
    public string CreateFileDate;
}

public struct WorkRecordManage
{
    public string ID;
    public string StaffID;
    public string Date;
    public string StartTime;
    public string EndTime;
    public string VacationType;
}

public struct WorkRecordAll
{
    public string StaffID;
    public string StaffName;
    public string V1;//'事假'
    public string V2;//'病假'
    public string V3;//'遲到'
    public string V4;//'特休'
    public string V5;//'公假'
    public string V6;// '婚假'
    public string V7;// '產假'
    public string V8;//'喪假'
    public string V9;//'公傷'
    public string V10;// '未打卡'
    public string V11;//'工作異動'


}
public struct WorkRecordDetail
{
    public string StaffID;
    public string StaffName;
    public string V1;//'事假'
    public string V2;//'病假'
    public string V3;//'遲到'
}




