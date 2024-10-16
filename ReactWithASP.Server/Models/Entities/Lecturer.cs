namespace ReactWithASP.Server.Models.Entities;

public class Lecturer(string firstName, string lastName, string email, string qualification) : Entity<int>
{
    [MaxLength(30)] public string FirstName { get; private set; } = firstName;
    [MaxLength(30)] public string LastName { get; private set; } = lastName;
    [MaxLength(40)] public string Email { get; private set; } = email;
    [MaxLength(100)] public string Qualification { get; private set; } = qualification;

    public void SetValues(string firstName, string lastName, string email, string qualification) => 
        (FirstName, LastName, Email, Qualification) = (firstName, lastName, email, qualification);
}