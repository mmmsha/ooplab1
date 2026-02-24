namespace UniversityManagement.Core.Courses;

public sealed class OnlineCourse : Course
{
    public string Platform { get; }
    public string MeetingUrl { get; }

    public OnlineCourse(int id, string title, string platform, string meetingUrl)
        : base(id, title)
    {
        if (string.IsNullOrWhiteSpace(platform))
            throw new ArgumentException("Platform не может быть пустым.", nameof(platform));
        if (string.IsNullOrWhiteSpace(meetingUrl))
            throw new ArgumentException("MeetingUrl не может быть пустым.", nameof(meetingUrl));

        Platform = platform;
        MeetingUrl = meetingUrl;
    }

    public override string GetDetails()
        => $"Online course: {Title}. Platform: {Platform}, Url: {MeetingUrl}";
}