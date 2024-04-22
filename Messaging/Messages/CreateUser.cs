namespace Messaging.Messages;

public class CreateUser
{
    public string Message { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Role { get; set; }
    
    public CreateUser(string message, string username, string email, string password, string firstName, string surname,
        string role = "User")
    {
        Message = message;
        Username = username;
        Email = email;
        Password = password;
        FirstName = firstName;
        Surname = surname;
        Role = role;
    }
}