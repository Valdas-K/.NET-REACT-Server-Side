namespace ReactWithASP.Server.Models.Entities;
public class Programme(string title, string description) : Entity<int>
{
    [MaxLength(30)] public string Title { get; private set; } = title;
    [MaxLength(100)] public string Description { get; private set; } = description;
    public void SetValues(string title, string description) => (Title, Description) = (title, description);
}