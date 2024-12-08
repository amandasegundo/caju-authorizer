using AutoFixture;
using caju_authorizer_domain.Abstractions.Extensions;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Enums;
using caju_authorizer_domain.Authorizer.Handlers;
using caju_authorizer_domain.Authorizer.Repositories;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace caju_authorizer_tests.Authorizer.Handlers
{
  [TestFixture]
  public class CashHandlerTests
  {
    private Fixture _fixture;
    private Mock<IAccountRepository> _accountRepository;

    [SetUp]
    public void SetUp()
    {
      _fixture = new();
      _accountRepository = new();
    }

    [Test]
    [Category("Cash-Handler")]
    [Description("Handle When Has No Sufficient Balance")]
    public void Handle_WhenHasNoSufficientBalance()
    {
      // Arrange
      var authorizerRequest = _fixture.Build<AuthorizerRequest>()
        .With(e => e.TotalAmount, 101)
        .Create();

      var account = _fixture.Build<Account>()
        .With(e => e.CashBalance, 100)
        .Create();

      var cashHandler = new CashHandler(authorizerRequest, account, _accountRepository.Object);

      // Act
      var result = cashHandler.Handle();

      // Assert
      Assert.That(result, Is.EqualTo(ResponseCodes.Rejected.GetDescription()));
    }

    [Test]
    [Category("Cash-Handler")]
    [Description("Handle When Has Sufficient Balance")]
    public void Handle_WhenHasSufficientBalance()
    {
      // Arrange
      var authorizerRequest = _fixture.Build<AuthorizerRequest>()
        .With(e => e.TotalAmount, 100)
        .Create();

      var account = _fixture.Build<Account>()
        .With(e => e.CashBalance, 100)
        .Create();

      var cashHandler = new CashHandler(authorizerRequest, account, _accountRepository.Object);

      // Act
      var result = cashHandler.Handle();

      // Assert
      Assert.That(result, Is.EqualTo(ResponseCodes.Approved.GetDescription()));

      _accountRepository.Verify(c => c.UpdateAccount(authorizerRequest.Account, account));
    }
  }
}
