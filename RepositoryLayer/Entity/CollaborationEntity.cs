using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CollaborationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaborationID { get; set; }
        public string CollaborationEmail { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        [JsonIgnore]
        public UserEntity User { get; set; }

        [ForeignKey("Note")]
        public int NoteID { get; set; }
        [JsonIgnore]
        public virtual NoteEntity Note { get; set; }
    }
}
