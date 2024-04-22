namespace UserService.Core.Services.DTOs;

public class GetUserDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public DateTime CreationDate { get; set; }
}