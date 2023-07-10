using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class LableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LableID { get; set; }
        public string LableName { get; set; }


        [ForeignKey("User")]
        public int UserID { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }


        [ForeignKey("Note")]
        public int NoteID { get; set; }
        [JsonIgnore]
        public virtual NoteEntity Note { get; set; }
    }
}
