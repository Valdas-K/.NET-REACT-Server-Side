namespace ReactWithASP.Server.Models.Entities;
public class Group(string title) : Entity<int>
{
    [MaxLength(10)] public string Title { get; private set; } = title;
    public void SetValues(string title) => (Title) = (title);
}