using AutoFixture;
using caju_authorizer_domain.Abstractions.Extensions;
using caju_authorizer_domain.Authorizer.Config;
using caju_authorizer_domain.Authorizer.Dtos;
using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Enums;
using caju_authorizer_domain.Authorizer.Handlers;
using caju_authorizer_domain.Authorizer.Handlers.Interfaces;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_domain.Authorizer.Services;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Transactions;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using Transaction = caju_authorizer_domain.Authorizer.Entities.Transaction;

namespace caju_authorizer_tests.Authorizer.Services
{
  [TestFixture]
  public class AuthorizerServiceTests
  {
    private Fixture _fixture;
    private Mock<IAuthorizerHandlerFactory> _authorizerHandlerFactory;
    private Mock<IMerchantRepository> _merchantRepository;
    private Mock<ITransactionRepository> _transactionRepository;
    private Mock<ICacheRepository> _cacheRepository;
    private TransactionConfig _transactionConfig;
    private AuthorizerService _authorizerService;

    [SetUp]
    public void SetUp()
    {
      _fixture = new Fixture();
      _authorizerHandlerFactory = new();
      _merchantRepository = new();
      _transactionRepository = new();
      _cacheRepository = new();
      _transactionConfig = new TransactionConfig(){
        ExpirationTimeInMinutes = 1
      };

      _authorizerService = new AuthorizerService(
        _authorizerHandlerFactory.Object,
        _merchantRepository.Object,
        _transactionRepository.Object,
        _cacheRepository.Object,
        _transactionConfig
      );
    }

    [Test]
    [Category("Authorizer-Service")]
    [Description("Authorize When Merchant Is Null")]
    public void Authorize_WhenMerchantIsNull()
    {
      // Arrange
      var request = _fixture.Create<AuthorizerRequest>();

      // Act
      var result = _authorizerService.Authorize(request);

      // Assert
      Assert.That(result.Code, Is.EqualTo(ResponseCodes.Error.GetDescription()));

      _merchantRepository.Verify(c => c.GetMerchantByName(request.Merchant));
      _transactionRepository.Verify(c => c.InsertTransaction(It.IsAny<Transaction>()));
    }

    [Test]
    [Category("Authorizer-Service")]
    [Description("Authorize When Has Transaction In Cash")]
    public void Authorize_WhenHasTransactionInCash()
    {
      // Arrange
      var request = _fixture.Create<AuthorizerRequest>();
      var merchant = _fixture.Create<Merchant>();
      var cacheResponse = JsonConvert.SerializeObject(_fixture.Create<AuthorizerRequest>());

      _merchantRepository.Setup(c => c.GetMerchantByName(request.Merchant)).Returns(merchant);
      _cacheRepository.Setup(c => c.Get(It.IsAny<string>())).Returns(cacheResponse);

      // Act
      var result = _authorizerService.Authorize(request);

      // Assert
      Assert.That(result.Code, Is.EqualTo(ResponseCodes.Error.GetDescription()));

      _merchantRepository.Verify(c => c.GetMerchantByName(request.Merchant));
      _cacheRepository.Verify(c => c.Get(It.IsAny<string>()));
      _transactionRepository.Verify(c => c.InsertTransaction(It.IsAny<Transaction>()));
    }

    [Test]
    [Category("Authorizer-Service")]
    [Description("Authorize When Success")]
    public void Authorize_WhenSuccess()
    {
      // Arrange
      var request = _fixture.Create<AuthorizerRequest>();
      var merchant = _fixture.Create<Merchant>();

      _merchantRepository.Setup(c => c.GetMerchantByName(request.Merchant)).Returns(merchant);

      _authorizerHandlerFactory.Setup(c => c.Create(It.IsAny<AuthorizerRequest>())).Returns(new AuthorizerHandlerClassTest());

      // Act
      var result = _authorizerService.Authorize(request);

      // Assert
      Assert.That(result.Code, Is.EqualTo(ResponseCodes.Approved.GetDescription()));

      _merchantRepository.Verify(c => c.GetMerchantByName(request.Merchant));
      _cacheRepository.Verify(c => c.Get(It.IsAny<string>()));
      _transactionRepository.Verify(c => c.InsertTransaction(It.IsAny<Transaction>()));
    }

    private class AuthorizerHandlerClassTest : AuthorizerHandler
    {
      public override string Handle()
      {
        return "00";
      }
    }
  }
}
