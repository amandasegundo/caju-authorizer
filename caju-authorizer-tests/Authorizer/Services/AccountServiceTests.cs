using AutoFixture;
using caju_authorizer_domain.Authorizer.Entities;
using caju_authorizer_domain.Authorizer.Repositories;
using caju_authorizer_domain.Authorizer.Services;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace caju_authorizer_tests.Authorizer.Services
{
  [TestFixture]
  public class AccountServiceTests
  {
    private Fixture _fixture;
    private Mock<IAccountRepository> _accountRepository;
    private AccountService _accountService;

    [SetUp]
    public void SetUp()
    {
      _fixture = new();
      _accountRepository = new();
      _accountService = new AccountService(_accountRepository.Object);
    }

    [Test]
    [Category("Account-Service")]
    [Description("GetAccounts When Success")]
    public void GetAccounts_WhenSuccess()
    {
      // Arrange
      var accounts = _fixture.Create<IEnumerable<Account>>();

      _accountRepository.Setup(c => c.GetAccounts()).Returns(accounts);

      // Act
      var result = _accountService.GetAccounts();

      // Assert
      Assert.That(result, Is.Not.Null);

      _accountRepository.Verify(c => c.GetAccounts());
    }

    [Test]
    [Category("Account-Service")]
    [Description("GetAccount When Success")]
    public void GetAccount_WhenSuccess()
    {
      // Arrange
      var account = _fixture.Create<Account>();
      var id = "101";

      _accountRepository.Setup(c => c.GetAccount(id)).Returns(account);

      // Act
      var result = _accountService.GetAccount(id);

      // Assert
      Assert.That(result, Is.Not.Null);

      _accountRepository.Verify(c => c.GetAccount(id));
    }
  }
}
