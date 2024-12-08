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
  public class TransactionServiceTests
  {
    private Fixture _fixture;
    private Mock<ITransactionRepository> _transactionRepository;
    private TransactionService _transactionService;

    [SetUp]
    public void SetUp()
    {
      _fixture = new();
      _transactionRepository = new();
      _transactionService = new TransactionService(_transactionRepository.Object);
    }

    [Test]
    [Category("TransactionService-Service")]
    [Description("GetTransactionsByAccountId When Success")]
    public void GetTransactionsByAccountId_WhenSuccess()
    {
      // Arrange
      var account = _fixture.Create<IEnumerable<Transaction>>();
      var id = "101";

      _transactionRepository.Setup(c => c.GetTransactionsByAccountId(id)).Returns(account);

      // Act
      var result = _transactionService.GetTransactionsByAccountId(id);

      // Assert
      Assert.That(result, Is.Not.Null);

      _transactionRepository.Verify(c => c.GetTransactionsByAccountId(id));
    }
  }
}
