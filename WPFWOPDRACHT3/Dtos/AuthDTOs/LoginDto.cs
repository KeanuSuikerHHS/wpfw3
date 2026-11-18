using System;
using System.ComponentModel.DataAnnotations;

namespace WPFWOPDRACHT3.Dtos.AuthDTOs
{
    public record LoginDto
    (
        [Required] string Username,
        [Required] string Password
    );
}
