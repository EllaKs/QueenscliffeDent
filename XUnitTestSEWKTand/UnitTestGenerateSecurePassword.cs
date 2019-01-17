using FakeItEasy;
using SEWKTand.Features.Shared.Security;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestSEWKTand
{
    public class UnitTestGenerateSecurePassword
    {
        //[Fact]
        //void GenerateSaltMethodTest()
        //{
        //    var fakeRNG = A.Fake<IRNGCryptoServiceProvider>();
        //    var fakeIRfc = A.Fake<IRfc2898DeriveBytes>();
        //    var gsp = new GenerateSecurePassword(fakeRNG, fakeIRfc);

        //    var result = gsp.GenerateSalt();

        //    Assert.Equal(16, result.Length);
        //}

        //[Fact]
        //void HashAndSaltPasswordMethodTest()
        //{
        //    var fakeRNG = A.Fake<IRNGCryptoServiceProvider>();
        //    var fakeIRfc = A.Fake<IRfc2898DeriveBytes>();
        //    var gsp = new GenerateSecurePassword(fakeRNG, fakeIRfc);
        //    var fakePassword = "11235564654";

        //    var result = gsp.HashAndSaltPassword(fakePassword);

        //    Assert.NotNull(result);
        //    Assert.Equal(48, result.Length);
        //}

        //[Fact]
        //void HashPasswordMethodTest()
        //{
        //    var fakeRNG = A.Fake<IRNGCryptoServiceProvider>();
        //    var fakeIRfc = A.Fake<IRfc2898DeriveBytes>();
        //    var gsp = new GenerateSecurePassword(fakeRNG, fakeIRfc);
        //    var fakePass = "123";
        //    var salt = gsp.GenerateSalt();

        //    var result = gsp.HashPassword(fakePass, salt);

        //    Assert.NotNull(result);
        //    Assert.Equal(20, result.Length);
        //}
    }
}
