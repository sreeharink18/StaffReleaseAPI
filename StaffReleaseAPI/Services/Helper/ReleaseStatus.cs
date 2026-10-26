namespace StaffReleaseAPI.Services.Helper
{
    public enum ReleaseStatus
    {
        Draft = 1,
        ClearancePending = 2,
        ClearanceInProgress = 3,
        ChangesRequired = 4,
        ReadyForFinalRelease = 5,
        Completed = 6,
        Cancelled = 7
    }
}
