using System;

namespace Dtos
{
    [Serializable]
    public class RegisterDto
    {
        public string Email;
        public string Password;
        public string Username;
    }
}