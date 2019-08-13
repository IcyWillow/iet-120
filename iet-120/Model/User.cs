using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using iet_120.CustomValidationAttributes;
using iet_120.Notification;

namespace iet_120.Model
{
    public class User : PropertyChangedNotification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id {  
            get { return GetValue(() => Id); }
            set { SetValue(() => Id, value); } }
        [Required]
        [Display(Name = "Anrede")]
        public string Salutation { get; set; }
        [Required]
        [Display(Name = "Vorname")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Nachname")]
        public string Surname { get; set; }
        [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "E-mail ist ungültig.")]
        [Display(Name = "E-Mail")]
        [Unique(ErrorMessage = "Email existiert bereits.")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Passwort")]
        [Compare("Password_Confirm", ErrorMessage = "Bitte geben Sie das Passwort erneut.")] 
        public string Password { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreateAt { get; set; }
    }
}
