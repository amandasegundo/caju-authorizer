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
  public class MealHandlerTests
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
    [Category("Meal-Handler")]
    [Description("Handle When Has Sufficient Balance")]
    public void Handle_WhenHasSufficientBalance()
    {
      // Arrange
      var authorizerRequest = _fixture.Build<AuthorizerRequest>()
        .With(e => e.TotalAmount, 100)
        .Create();

      var account = _fixture.Build<Account>()
        .With(e => e.MealBalance, 100)
        .Create();

      var mealHandler = new MealHandler(authorizerRequest, account, _accountRepository.Object);

      // Act
      var result = mealHandler.Handle();

      // Assert
      Assert.That(result, Is.EqualTo(ResponseCodes.Approved.GetDescription()));

      _accountRepository.Verify(c => c.UpdateAccount(authorizerRequest.Account, account));
    }

    [Test]
    [Category("Meal-Handler")]
    [Description("Handle When Fallback And Has Sufficient Cash Balance")]
    public void Handle_WhenFallbackAndHasSufficientCashBalance()
    {
      // Arrange
      var authorizerRequest = _fixture.Build<AuthorizerRequest>()
        .With(e => e.TotalAmount, 100)
        .Create();

      var account = _fixture.Build<Account>()
        .With(e => e.MealBalance, 50)
        .With(e => e.CashBalance, 100)
        .Create();

      var mealHandler = new MealHandler(authorizerRequest, account, _accountRepository.Object);

      // Act
      var result = mealHandler.Handle();

      // Assert
      Assert.That(result, Is.EqualTo(ResponseCodes.Approved.GetDescription()));

      _accountRepository.Verify(c => c.UpdateAccount(authorizerRequest.Account, account));
    }

    [Test]
    [Category("Meal-Handler")]
    [Description("Handle When Fallback And Has No Sufficient Cash Balance")]
    public void Handle_WhenFallbackAndHasNoSufficientCashBalance()
    {
      // Arrange
      var authorizerRequest = _fixture.Build<AuthorizerRequest>()
        .With(e => e.TotalAmount, 100)
        .Create();

      var account = _fixture.Build<Account>()
        .With(e => e.MealBalance, 50)
        .With(e => e.CashBalance, 40)
        .Create();

      var mealHandler = new MealHandler(authorizerRequest, account, _accountRepository.Object);

      // Act
      var result = mealHandler.Handle();

      // Assert
      Assert.That(result, Is.EqualTo(ResponseCodes.Rejected.GetDescription()));
    }
  }
}
