using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEWKTand.Data.Entities
{
    public class EntityMedicalRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // [ForeignKey("EntityPatient")]
         public int PatientId { get; set; } //Foreign key
        public virtual EntityPatient Patient { get; set; } //Navigation property

        public DateTime DateOfNote { get; set; }
        public string Note { get; set; }
    }
}
