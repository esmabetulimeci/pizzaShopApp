﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public UserAggregate(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<AddressAggregate> Addresses { get; set; }
        public virtual List<OrderAggregate> Orders { get; }

        public static UserAggregate Create(string firstName, string lastName, string email)
        {
            return new UserAggregate(firstName, lastName, email);
        }

        public UserAggregate Update(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            return this;
        }
    }
}
