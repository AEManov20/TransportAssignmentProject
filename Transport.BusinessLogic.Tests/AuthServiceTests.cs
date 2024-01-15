using NUnit.Framework;

[TestFixture]
public class AuthServiceTests
{
    private IAuthService authService;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Transport")
            .Options;


        authService = new AuthService();
    }

    [Test]
    public void Test_Login_Success()
    {
        // Arrange
        string username = "testuser";
        string password = "testpassword";

        // Act
        bool result = authService.Login(username, password);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Test_Login_Failure()
    {
        // Arrange
        string username = "testuser";
        string password = "wrongpassword";

        // Act
        bool result = authService.Login(username, password);

        // Assert
        Assert.IsFalse(result);
    }
}
