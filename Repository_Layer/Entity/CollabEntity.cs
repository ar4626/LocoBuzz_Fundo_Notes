using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollabId { get; set; }
        public string CollabEmail { get; set; }
        public bool IsRemove{ get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        [ForeignKey ("CollabBy")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity CollabBy { get; set; }
        [ForeignKey("CollabFor")]
        public int NoteId { get; set; }
        [JsonIgnore]
        public virtual NoteEntity CollabFor { get; set; }

    }
}
