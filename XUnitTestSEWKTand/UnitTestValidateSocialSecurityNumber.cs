using SEWKTand.Features.Shared.Helpers;
using Xunit;

namespace XUnitTestSEWKTand
{
    public class UnitTestValidateSocialSecurityNumber
    {
        [Fact]
        public void TestValidateSSNRMethod_WithInvalidSSNR()
        {
            var fakeSSNR = "196409012518";
            var SSNRVerification = new SocialSecurityNumberVerification();

            var result = SSNRVerification.VerifyIfSocialSecurityNumberIsValid(fakeSSNR);
   
            Assert.False(result);
        }

        [Fact]
        public void TestValidateSSNRMethod_WithBlankSSNR()
        {
            var fakeSSNR = "";
            var SSNRVerification = new SocialSecurityNumberVerification();

            var result = SSNRVerification.VerifyIfSocialSecurityNumberIsValid(fakeSSNR);

            Assert.False(result);
        }

        [Fact]
        public void TestValidateSSNRMethod_WithValidSSNR()
        {
            var fakeSSNR = "196410035510";
            var SSNRVerification = new SocialSecurityNumberVerification();

            var result = SSNRVerification.VerifyIfSocialSecurityNumberIsValid(fakeSSNR);

            Assert.True(result);
        }

        [Fact]
        public void TestValidateSSNRMethod_WithInvalidDateTime()
        {
            var fakeSSNR = "196406315510";
            var SSNRVerification = new SocialSecurityNumberVerification();

            var result = SSNRVerification.VerifyIfSocialSecurityNumberIsValid(fakeSSNR);

            Assert.False(result);
        }
    }
}
