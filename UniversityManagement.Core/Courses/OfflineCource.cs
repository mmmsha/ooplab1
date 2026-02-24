namespace UniversityManagement.Core.Courses;

public sealed class OfflineCourse : Course
{
    public string Room { get; }
    public string Address { get; }

    public OfflineCourse(int id, string title, string room, string address)
        : base(id, title)
    {
        if (string.IsNullOrWhiteSpace(room))
            throw new ArgumentException("Room не может быть пустым.", nameof(room));
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address не может быть пустым.", nameof(address));

        Room = room;
        Address = address;
    }

    public override string GetDetails()
        => $"Offline course: {Title}. Room: {Room}, Address: {Address}";
}