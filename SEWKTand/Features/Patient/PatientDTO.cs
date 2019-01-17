using SEWKTand.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEWKTand.Features.Patient
{
    public class PatientDTO
    {
        public int Id { get; set; }

        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        [Display(Name = "Lastname")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phonenumber")]
        public string PhoneNumber { get; set; }
        [Display(Name = "SSN")]
        [StringLength(12, MinimumLength = 10)]
        public string SocialSecurityNumber { get; set; }
        public Insurance Insurance { get; set; }
        public ICollection<EntityMedicalRecord> MedicalRecords { get; set; }
    }

    public class RegisterPatientDTO
    {
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
        [StringLength(2, ErrorMessage = "You have to choose a {0}.", MinimumLength = 2)]
        public string Day { get; set; }
        [Required]
        [StringLength(2, ErrorMessage = "You have to choose a {0}.", MinimumLength = 2)]
        public string Month { get; set; }
        [Required]
        [StringLength(4, ErrorMessage = "You have to choose a {0}.", MinimumLength = 2)]
        public string Year { get; set; }
        [Required]
        [Display(Name = "Four last digits")]
        [StringLength(4, ErrorMessage = "The {0} must be {2} digits long.", MinimumLength = 4)]
        public string LastDigits { get; set; }

        //[StringLength(12, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 12)]
        //public string SocialSecurityNumber { get; set; }

        [Required]
        public Insurance Insurance { get; set; }
    }

    public class UpdatePatientDTO
    {
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
        [Display(Name = "Insurance")]
        [Required]
        public Insurance Insurance { get; set; }
    }
}
