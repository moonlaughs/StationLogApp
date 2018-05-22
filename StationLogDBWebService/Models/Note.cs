namespace StationLogDBWebService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Note
    {
        [Key]
        public int NotesID { get; set; }

        [Column("Note")]
        [Required]
        [StringLength(200)]
        public string Note1 { get; set; }

        public DateTime? DueDate { get; set; }

        public int? StationID { get; set; }

        public int? UserID { get; set; }

        public virtual Station Station { get; set; }

        public virtual UserTable UserTable { get; set; }
    }
}
