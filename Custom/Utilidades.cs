using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using WebApi.Models;
using System.Runtime.Intrinsics.Arm;

namespace WebApi.Custom
{
    public class Utilidades
    {
        private readonly IConfiguration _configuration; //Para acceder a la info de appsettings.json

        public Utilidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string encriptarSHA256(string texto)
        {
            using(SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));
                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }   
        }

        public string generarJWT(Usuario modelo) 
        {
            // Crear la información del usuario para el token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, modelo.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, modelo.Correo!),
            };

            // Crear la clave de seguridad
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            // Crear las credenciales del token
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Crear el token
            var token = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            // Retornar el token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
