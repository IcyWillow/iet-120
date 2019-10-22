using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;
using M120Projekt.Data;

namespace M120Projekt.Model
{
    public class Word
    {
        #region Datenbankschicht
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "{0}länge muss zwischen {2} und {1} sein.", MinimumLength = 3)]
        [Display(Name = "Wort")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [NotMapped]
        public string Creator
        {
            get
            {
                User = User.ReadById(UserId);
                return $"{User.Firstname} {User.Lastname}";
            }
        }
        [Display(Name = "Erstellt am")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreatedAt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime UpdatedAt { get; set; }
        #endregion
        #region Applikationsschicht
        public Word() { }
      
        public static List<Word> All()
        {
            using (var db = new Context())
            {
                return (from record in db.Words select record).ToList();
            }
        }

        public static List<Word> AllActive()
        {
            using (var db = new Context())
            {
                return (from record in db.Words where record.IsActive == true select record).ToList();
            }
        }

        public static List<Word> LikeByCreator(string term, int userId)
        {
            using (var db = new Context())
            {
                return (from record in db.Words where record.Name.Contains(term) && record.UserId == userId select record).ToList();
            }
        }

        public static List<Word> ReadByCreatorId(int id)
        {
            using (var db = new Context())
            {
                return (from record in db.Words where record.UserId == id select record).ToList();
            }
        }

        public static Word ReadById(int id)
        {
            using (var db = new Context())
            {
                return (from record in db.Words where record.Id == id select record).FirstOrDefault();
            }
        }
        public static List<Word> Where(string term)
        {
            using (var db = new Context())
            {
                return (from record in db.Words where record.Name == term select record).ToList();
            }
        }
        public static List<Word> Like(string term)
        {
            using (var db = new Context())
            {
                return (from record in db.Words where record.Name.Contains(term) select record).ToList();
            }
        }

        public int Create()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            using (var db = new Context())
            {
                db.Words.Add(this);
                db.SaveChanges();
                return Id;
            }
        }
        public int Update()
        {
            UpdatedAt = DateTime.Now;
            using (var db = new Context())
            {
                db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Id;
            }
        }
        public void Delete()
        {
            using (var db = new Context())
            {
                db.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public override string ToString()
        {
            return Id.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
