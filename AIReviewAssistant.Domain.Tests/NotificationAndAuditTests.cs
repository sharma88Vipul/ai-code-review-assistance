using AIReviewAssistant.Domain;

namespace AIReviewAssistant.Domain.Tests;

public class NotificationAndAuditTests
{
    [Fact]
    public void Notification_Create_InitializesPendingNotification()
    {
        var notification = Notification.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            NotificationType.ReviewCompleted,
            "Review completed",
            "The pull request review has completed.");

        Assert.NotEqual(Guid.Empty, notification.Id);
        Assert.Equal(NotificationStatus.Pending, notification.Status);
        Assert.Null(notification.SentAtUtc);
        Assert.Null(notification.ErrorMessage);
    }

    [Fact]
    public void Notification_MarkSent_CompletesNotification()
    {
        var notification = CreateNotification();

        notification.MarkSent();

        Assert.Equal(NotificationStatus.Sent, notification.Status);
        Assert.NotNull(notification.SentAtUtc);
    }

    [Fact]
    public void Notification_CannotBeFinalizedTwice()
    {
        var notification = CreateNotification();
        notification.MarkSent();

        Assert.Throws<InvalidOperationException>(
            () => notification.MarkFailed("Delivery failed."));
    }

    [Fact]
    public void AuditLog_Create_InitializesImmutableEntry()
    {
        var organizationId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var auditLog = AuditLog.Create(
            organizationId,
            userId,
            "RepositoryConnected",
            "Repository",
            "repo-123",
            "  Connected from GitHub.  ");

        Assert.NotEqual(Guid.Empty, auditLog.Id);
        Assert.Equal(organizationId, auditLog.OrganizationId);
        Assert.Equal(userId, auditLog.UserId);
        Assert.Equal("Connected from GitHub.", auditLog.Details);
    }

    [Fact]
    public void AuditLog_Create_WithEmptyOptionalUser_Throws()
    {
        Assert.Throws<ArgumentException>(() => AuditLog.Create(
            Guid.NewGuid(),
            Guid.Empty,
            "RepositoryConnected",
            "Repository",
            "repo-123"));
    }

    private static Notification CreateNotification()
    {
        return Notification.Create(
            Guid.NewGuid(),
            null,
            NotificationType.SecurityIssueDetected,
            "Security issue detected",
            "A critical issue requires attention.");
    }
}
