﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApIHotelListing.Models
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to 15")]
        public string Password { get; set; }
    }

    public class UserDTO : LoginDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }        
        public ICollection<string> Roles { get; set; }
    }
}
