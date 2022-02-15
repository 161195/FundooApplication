using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class Note
    {     
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
         
        [DataType(DataType.Text)]
        [Display(Name = "Title:")]
        public string Title { get; set; } 

        [DataType(DataType.Text)]
        [Display(Name = "Message:")]
        public string Message { get; set; }
       
        [DataType(DataType.DateTime)]
        [Display(Name = "Remainder:")]
        public DateTime? Reminder { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Color:")]
        public string Color { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Image:")]
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedAt { get; set; }


        public User User { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }

        public ICollection<Collaborator> Collaborator { get; set; }

        public ICollection<Lable> Lable { get; set; }
    }
}
