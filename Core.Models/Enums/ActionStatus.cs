namespace Core.Models.Enums
{
    public enum ActionStatus : int
    {
        Success = 1,
        InvalidUser = 2,
        Fail = 3,
        Errored = 4,
        Locked = 5,
        NotFound = 6,
        CustomerExpired = 7,
        UserSessionsExceeded = 8,
        NetworkInstenceError = 9,
        CustomerPendingOrRejectStatus = 10,
        InValidRequest = 11,
        IncompleteConfiguration = 12,
        OTP=13,
        InvalidIpAddress= 14,
        OtpExpire=15,
        Freeze=16,
        Unsuccess =17,
    }
    
    public enum EventStatus:int
    {
        Active=1,
        InActive=0
    }
}
