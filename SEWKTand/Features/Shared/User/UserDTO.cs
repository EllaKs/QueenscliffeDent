using SEWKTand.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.Shared.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        [Required]
        [Display(Name = "Firstname")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} letters long.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Lastname")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} letters long.", MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phonenumber")]
        [StringLength(10, ErrorMessage = "The {0} must be {2} digits long.", MinimumLength = 10)]
        public string PhoneNumber { get; set; }
        [Required]
        public string HashedPassword { get; set; }
    }

    public class UserLoginDTO
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Password { get; set; }
        public Role Role { get; set; }
    }

    public class RegisterUserDTO
    {
        [Required]
        [Display(Name = "Firstname")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} letters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} letters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        //[Required]
        //[StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        //public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phonenumber")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} digits long.", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string HashedPassword { get; set; }
    }

    public class UpdateUserDTO
    {
        //[Required]
        //public string Email { get; set; }
        [Required]
        [Display(Name = "Firstname")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} letters long.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Lastname")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} letters long.", MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be {2} digits long.", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
