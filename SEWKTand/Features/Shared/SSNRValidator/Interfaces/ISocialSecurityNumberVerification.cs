namespace SEWKTand.Features.Shared.SSNRValidator.Interfaces
{
    public interface ISocialSecurityNumberVerification
    {
        bool VerifyIfSocialSecurityNumberIsValid(string ssnumber);
    }
}
