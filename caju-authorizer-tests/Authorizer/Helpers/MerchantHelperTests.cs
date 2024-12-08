using caju_authorizer_domain.Authorizer.Helpers;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace caju_authorizer_tests.Authorizer.Helpers
{
  [TestFixture]
  public class MerchantHelperTests
  {
    [Test]
    [Category("Merchant-Helper")]
    [Description("NormalizeName When Null")]
    public void NormalizeName_WhenNull() {
      // Arrange
      string name = null;
      
      // Act
      var result = MerchantHelper.NormalizeName(name);

      // Assert
      Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    [Category("Merchant-Helper")]
    [Description("NormalizeName When Success")]
    [TestCase("PADARIA DO ZE SAO PAULO BR", "PADARIA DO ZE SAO PAULO BR")]
    [TestCase("PADARIA DO ZE SAO PAULO BR", "  PADARIA DO ZE SAO PAULO BR  ")]
    [TestCase("PADARIA DO ZE SAO PAULO BR", "PADARIA DO ZE          SAO PAULO BR")]
    [TestCase("PADARIA DO ZE SAO PAULO BR", "PADARIA   DO    ZE   SAO  PAULO   BR")]
    public void NormalizeName_WhenSuccess(string expected, string name)
    {
      // Act
      var result = MerchantHelper.NormalizeName(name);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }
  }
}
