using SEWKTand.Features.Patient.Interfaces;
using SEWKTand.Features.Shared.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEWKTand.Data.Entities
{
    public abstract class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2)]
        public virtual string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public virtual string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(30, MinimumLength = 5)]
        public virtual string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(10, MinimumLength = 10)]
        public virtual string PhoneNumber { get; set; }
    }

    public enum Role
    {
        Admin,
        Dentist
    }

    public class EntityAdmin : Person, IUser
    {
        [Key]
        public override int Id { get; set; }// Remove? Already exist in Person

        public Role Role { get { return Role.Admin; } set { value = Role.Admin; } }

        [Required]
        public string HashedPassword { get; set; }
    }

    public class EntityDentist : Person, IUser
    {
        [Key]
        public override int Id { get; set; }// Remove? Already exist in Person

        public Role Role { get { return Role.Dentist; } set { value = Role.Dentist; } }

        [Required]
        public string HashedPassword { get; set; }
    }

    public class EntityPatient : Person, IPatient
    {
        [Key]
        public override int Id { get; set; }// Remove? Already exist in Person

        [Required]
        [StringLength(12, MinimumLength = 10)]
        public string SocialSecurityNumber { get; set; }

        [Required]
        public Insurance Insurance { get; set; }

        [ForeignKey("PatientId")]
        public virtual ICollection<EntityMedicalRecord> MedicalRecords { get; set; }
    }

    public enum Insurance
    {
        Valid,
        Invalid
    }
}





