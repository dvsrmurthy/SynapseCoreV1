namespace Core.Models.Enums
{
    public enum ProcReturnType : int
    {
        Success = 0,
        UserNotExisted = 1,
        CustomerExpired = 2,
        UserSessionsExceeded = 3,
        UserNotLdapUser = 4,
        NetworkInstenceError = 5,
        CustomerPendingOrRejectStatus = 6,
        UserLocked=9,
        IncompleteConfiguration = 10,
        OTP =11,
        InvalidIpAddress=12,
        OtpExpire =13,
        Freeze=15, InvalidUser = 16
    }
}
