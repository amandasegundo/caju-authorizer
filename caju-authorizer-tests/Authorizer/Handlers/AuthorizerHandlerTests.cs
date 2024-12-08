using caju_authorizer_domain.Authorizer.Handlers;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace caju_authorizer_tests.Authorizer.Handlers
{
  [TestFixture]
  public class AuthorizerHandlerTests
  {
    private ClassTest _classTest;

    [SetUp]
    public void SetUp(){
      _classTest = new ClassTest();
    }

    [Test]
    [Category("Authorizer-Handler")]
    [Description("HasSufficientBalance WhenSuccess")]
    [TestCase(true, 100, 100)]
    [TestCase(true, 100, 50)]
    [TestCase(false, 100, 101)]
    [TestCase(false, 100, 200)]
    public void HasSufficientBalance_WhenSuccess(bool expected, decimal balance, decimal amount)
    {
      // Act
      var result = _classTest.HasSufficientBalance(balance, amount);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [Category("Authorizer-Handler")]
    [Description("CalculateNewBalance WhenSuccess")]
    [TestCase(0, 100, 100)]
    [TestCase(50, 100, 50)]
    [TestCase(-1, 100, 101)]
    [TestCase(-100, 100, 200)]
    public void CalculateNewBalance_WhenSuccess(decimal expected, decimal balance, decimal amount)
    {
      // Act
      var result = _classTest.CalculateNewBalance(balance, amount);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [Category("Authorizer-Handler")]
    [Description("Valid Handle")]
    public void ValidHandle()
    {
      // Act
      var result = _classTest.Handle();

      // Assert
      Assert.That(result, Is.EqualTo("Handler"));
    }

    private class ClassTest : AuthorizerHandler
    {
      public override string Handle()
      {
        return "Handler";
      }
    }
  }
}
