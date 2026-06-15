using AIReviewAssistant.Domain;

namespace AIReviewAssistant.Domain.Tests;

public class EntityCreationTests
{
    [Fact]
    public void Organization_Create_InitializesOrganization()
    {
        var organization = Organization.Create("  Acme  ");

        Assert.NotEqual(Guid.Empty, organization.Id);
        Assert.Equal("Acme", organization.Name);
        AssertRecent(organization.CreatedAtUtc);
    }

    [Fact]
    public void Organization_Create_WithBlankName_Throws()
    {
        Assert.Throws<ArgumentException>(() => Organization.Create(" "));
    }

    [Fact]
    public void Repository_Connect_InitializesActiveRepository()
    {
        var organizationId = Guid.NewGuid();

        var repository = Repository.Connect(
            organizationId,
            "review-service",
            "acme",
            "12345",
            "https://github.com/acme/review-service");

        Assert.NotEqual(Guid.Empty, repository.Id);
        Assert.Equal(organizationId, repository.OrganizationId);
        Assert.Equal("main", repository.DefaultBranch);
        Assert.True(repository.IsActive);
        AssertRecent(repository.ConnectedAtUtc);
    }

    [Fact]
    public void Repository_Connect_WithoutOrganization_Throws()
    {
        Assert.Throws<ArgumentException>(() => Repository.Connect(
            Guid.Empty,
            "review-service",
            "acme",
            "12345",
            "https://github.com/acme/review-service"));
    }

    [Fact]
    public void User_Create_NormalizesEmail()
    {
        var user = User.Create(
            Guid.NewGuid(),
            "Ada Lovelace",
            "  ADA@EXAMPLE.COM ",
            "ada");

        Assert.Equal("ada@example.com", user.Email);
        AssertRecent(user.CreatedAtUtc);
    }

    [Fact]
    public void ReviewComment_Create_WithInvalidLineNumber_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ReviewComment.Create(
            Guid.NewGuid(),
            "Program.cs",
            0,
            "Use a guard clause.",
            ReviewCommentSeverity.Warning,
            ReviewCommentCategory.Maintainability));
    }

    [Fact]
    public void SecurityFinding_Create_InitializesFinding()
    {
        var reviewSessionId = Guid.NewGuid();

        var finding = SecurityFinding.Create(
            reviewSessionId,
            "Authentication.cs",
            42,
            "Unsafe token validation",
            "Token signature is not validated.",
            SecuritySeverity.Critical);

        Assert.Equal(reviewSessionId, finding.ReviewSessionId);
        Assert.Equal(42, finding.LineNumber);
        Assert.Equal(SecuritySeverity.Critical, finding.Severity);
        AssertRecent(finding.CreatedAtUtc);
    }

    private static void AssertRecent(DateTime timestamp)
    {
        Assert.InRange(timestamp, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow);
    }
}
