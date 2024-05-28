using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class UserAggregate
    {
        public UserAggregate()
        {
            // only db
        }
        public UserAggregate(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = HashPassword(password); 

            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 

        public DateTime CreatedDate { get; set; }
        public virtual List<AddressAggregate> Addresses { get; set; }
        public virtual List<OrderAggregate> Orders { get; }


        // Parolanın hashlenmiş halini hesaplayan yardımcı metot
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
