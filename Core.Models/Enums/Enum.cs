
namespace Core.Models.Enums
{
    public enum Enum:int
    {
       Created=1,
       Approved=2,
       Rejected=3,
       Pending=4

    }
    public enum FILETYPE
    {

        NOTEPAD = 1,
        EXCEL = 2,
        GROUPS = 3,
        DB = 4,
        WEBSERVICE = 5,
    }
    public enum SOURCETYPE
    {
        DEFAULT = 1,
        ADT = 2,
        ERX = 3,
        INSURANCE = 4,
        BTW = 5,
        BDAY = 6,
        INSAPPROVAL = 7,
        LABRESULTS = 8
    }
    public enum FileType
    {
        XML = 1,
        HL7 = 2,
        CSV = 3,
        ADT = 4,
        INS = 5,
        BTW = 6,
        BDAY = 7,
        ERX = 8,
        INSAPP = 9,
        LABRESULT = 10
    }
    public enum REFRESHMBC
    {
        WM_TIMER = 2,
        MSG_MBC_REFRESH = 10,
        MSG_MBC_DELETE = 11,
        MSG_ALLMBC_REFRESH = 12,
        MSG_MBC_ADD = 13,
        MSG_MBC_ADDPROCESS = 14,
        MSG_MBC_DELETEPROCESS = 15,
        MSG_ADD_QUICKSMS = 16,
        MSG_MBC_DELSUSPENDED = 17,
        MSG_MBC_RESUME = 18,
        MSG_MBC_PAUSE = 19,
        MSG_MBC_CANCEL = 20,
        MSG_MBC_PAUSETORESUME = 21,
    }
    public enum MBXMODULES
    {
        QUICKSMS = 0,
        CampaignManagement = 1,
        DataQuery = 2,
        DBAlerts = 3,
        MailNotifications = 4,
        SNMPAlerts = 5,
        GENERAL = 10,
        Reports = 11,
        MBCManagement = 12,
        UserManagement = 13,
        SMSCManagement = 14,
        ConfigParameters = 15,
        UserSite = 16,
        AdminSite = 17,
    }
}
