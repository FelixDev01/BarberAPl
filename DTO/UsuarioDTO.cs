using Scalar.AspNetCore;

namespace BarberAPI.DTO
{
    public class UsuarioDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
