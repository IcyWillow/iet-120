using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iet_120.Notification;

namespace iet_120.Model
{
    public class Word
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "{0}länge muss zwischen {2} und {1} sein.", MinimumLength = 3)]
        [Display(Name = "Wort")]
        public string Name { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Display(Name = "Erstellt am")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreatedAt { get; set; }

    }
}
