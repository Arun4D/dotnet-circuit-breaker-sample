﻿namespace CgCustomer.Models
{
    public class CgCustomerDetails
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateOnly? DateOfBirth { get; set; }
        public string? Email { get; set; }
        
        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Area { get; set; }
        public string? Region { get; set; } 
        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string? Phone { get; set; }


    }
}
