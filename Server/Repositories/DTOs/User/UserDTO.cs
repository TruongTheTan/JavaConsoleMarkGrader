﻿namespace Repositories.DTOs.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? RoleName { get; set; }
    }
}