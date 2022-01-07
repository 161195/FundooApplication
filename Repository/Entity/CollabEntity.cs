using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabsId { get; set; }
        public Note Note { get; set; }

        [ForeignKey("Note")]
        public long NoteId { get; set; }
        public User User { get; set; }
        public string EmailId { get; set; }

    }
}
