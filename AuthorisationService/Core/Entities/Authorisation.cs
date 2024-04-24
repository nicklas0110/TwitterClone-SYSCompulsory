﻿namespace AuthorisationService.Core.Entities;

public class Authorisation
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash  { get; set; }
    public string Salt { get; set; }
    public string Role { get; set; } = "User";
}