using System.ComponentModel.DataAnnotations;

namespace ReactWithASP.Server.Models.Entities;

public class Student(string firstName, string lastName, string email, string course, string address): Entity<int>
{
    [MaxLength(30)] public string FirstName { get; private set; } = firstName;
    [MaxLength(30)] public string LastName { get; private set; } = lastName;
    [MaxLength(40)] public string Email { get; private set; } = email;
    [MaxLength(30)] public string Course { get; private set; } = course;
    [MaxLength(30)] public string Address { get; private set; } = address;
}
