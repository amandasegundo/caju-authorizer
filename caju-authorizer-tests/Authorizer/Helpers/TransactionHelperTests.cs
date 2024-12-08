using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Helpers;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace caju_authorizer_tests.Authorizer.Helpers
{
  public class TransactionHelperTests
  {
    [Test]
    [Category("Transaction-Helper")]
    [Description("GetTransactionCacheKey When Null")]
    public void GetTransactionCacheKey_WhenNull()
    {
      // Arrange
      string name = null;

      // Act
      var result = TransactionHelper.GetTransactionCacheKey(null);

      // Assert
      Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    [Category("Transaction-Helper")]
    [Description("GetTransactionCacheKey When Success")]
    public void GetTransactionCacheKey_WhenSuccess()
    {
      // Arrange
      var authorizerRequest = new AuthorizerRequest()
      {
        Account = "101",
        MCC = "5811",
        Merchant = "PARADIA",
        TotalAmount = 100
      };

      // Act
      var result = TransactionHelper.GetTransactionCacheKey(authorizerRequest);

      // Assert
      Assert.That(result, Is.EqualTo($"{authorizerRequest.Account}_{authorizerRequest.MCC}_{authorizerRequest.Merchant}_{authorizerRequest.TotalAmount}"));
    }
  }
}
