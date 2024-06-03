﻿using System.ComponentModel.DataAnnotations;

namespace Application.Features.UserFeatures.UserCommand.Registration
{
    public class RegisterUserDTO
    {
        [Required]
        public string? Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [Required]
        public string? Password { get; set; } = string.Empty;
        [Required, Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; } = string.Empty;
    }
}
