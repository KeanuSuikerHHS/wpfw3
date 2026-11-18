using System;
using System.ComponentModel.DataAnnotations;

namespace WPFWOPDRACHT3.Dtos.AuthDTOs
{
    public record RegisterDto
    (
        [Required] string Email,
        [Required] string Username,
        [Required] string Password
    );
}
