using System;
using System.Collections.Generic;

namespace ModContabilidad.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
