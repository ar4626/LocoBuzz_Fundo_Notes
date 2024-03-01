using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        [ForeignKey("LabelBy")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity LabelBy {  get; set; }

        [ForeignKey("LabelFor")]
        public int NoteId { get; set; }
        [JsonIgnore]
        public virtual NoteEntity LabelFor { get; set; }
    }
}
