namespace StaffReleaseAPI.Services.Helper
{
    public class StaticData
    {
        public static string Role_Admin = "ADMIN";
        public static string Role_HRStaff = "HRSTAFF";
        public static string Role_HRManager = "HRMANAGER";
        public static string Role_DepartmentManager = "DEPARTMENTMANAGER";

        public static string Role_User = "USER";

        //Overall release status for request staff release
        public static string ReleaseStatus_ClearancePending = "CLEARANCEPENDING";
        public static string ReleaseStatus_ClearanceInProgress = "CLEARANCEINPROGRESS";
        public static string ReleaseStatus_ChangesRequired = "CHANGESREQUIRED";
        public static string ReleaseStatus_ReadyForFinalRelease = "READYFORFINALRELEASE";
        public static string ReleaseStatus_Completed = "COMPLETED";
        public static string ReleaseStatus_Cancelled = "CANCELLED";


        //For department clearance status by manager
        public static string ClearanceStatus_Pending = "PENDING";
        public static string ClearanceStatus_Rejected = "REJECTED";
        public static string ClearanceStatus_Approved = "APPROVED";
    }
}
