using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class NoteRegistration
    {
        public long UserId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Title:")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Message is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Message:")]
        public string Message { get; set; }
        [Required(ErrorMessage = "Reminder is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Reminder:")]
        public DateTime? Reminder { get; set; }
        [Required(ErrorMessage = "Color is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Color:")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Image is required")]
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

    }
}
