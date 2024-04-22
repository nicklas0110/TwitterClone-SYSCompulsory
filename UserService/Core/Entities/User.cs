namespace UserService.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Role { get; set; } = "User";
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public DateTime CreationDate { get; set; }
    
}