using AutoFixture;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Handlers;
using caju_authorizer_domain.Authorizer.Repositories;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace caju_authorizer_tests.Authorizer.Handlers
{
  [TestFixture]
  public class AuthorizerHandlerFactoryTests
  {
    private Fixture _fixture;
    private Mock<IAccountRepository> _accountRepository;
    private AuthorizerHandlerFactory _authorizerHandlerFactory;

    [SetUp]
    public void SetUp()
    {
      _fixture = new ();
      _accountRepository = new();
      _authorizerHandlerFactory = new AuthorizerHandlerFactory(_accountRepository.Object);
    }

    [Test]
    [Category("Authorizer-Handler-Factory")]
    [Description("Create When InvalidOperationException")]
    public void Create_WhenInvalidOperationException()
    {
      // Arrange
      var authorizerRequest = _fixture.Create<AuthorizerRequest>();

      // Act & Assert
      Assert.Throws<InvalidOperationException>(() => _authorizerHandlerFactory.Create(authorizerRequest));

      _accountRepository.Verify(c => c.GetAccount(authorizerRequest.Account));
    }

    [Test]
    [Category("Authorizer-Handler-Factory")]
    [Description("Create When FoodHandler")]
    [TestCase("5411", nameof(FoodHandler))]
    [TestCase("5412", nameof(FoodHandler))]
    [TestCase("5811", nameof(MealHandler))]
    [TestCase("5812", nameof(MealHandler))]
    [TestCase("1111", nameof(CashHandler))]
    public void Create_WhenFoodHandler(string mcc, string className)
    {
      // Arrange
      var authorizerRequest = _fixture.Build<AuthorizerRequest>()
        .With(e => e.MCC, mcc)
        .Create();

      var account = _fixture.Create<Account>();

      _accountRepository.Setup(c => c.GetAccount(authorizerRequest.Account)).Returns(account);

      // Act
      var result = _authorizerHandlerFactory.Create(authorizerRequest);

      // Assert
      Assert.That(result.GetType().Name, Is.EqualTo(className));

      _accountRepository.Verify(c => c.GetAccount(authorizerRequest.Account));
    }
  }
}
