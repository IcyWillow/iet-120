using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using M120Projekt.Data;

namespace M120Projekt.Model
{
    public class Highscore
    {
        #region Datenbankschicht
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public string GameWord { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Display(Name = "Erstellt am")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreatedAt { get; set; }
        #endregion
        #region Applikationsschicht
        public Highscore() { }
        [NotMapped]
        public string CalculatedAttr
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
        public static List<Highscore> All()
        {
            using (var db = new Context())
            {
                return (from record in db.Highscores select record).ToList();
            }
        }
        public static Highscore ReadById(int id)
        {
            using (var db = new Context())
            {
                return (from record in db.Highscores where record.Id == id select record).FirstOrDefault();
            }
        }
        public static List<Highscore> Where(string term)
        {
            using (var db = new Context())
            {
                return (from record in db.Highscores where record.GameWord == term select record).ToList();
            }
        }
        public static List<Highscore> Like(string term)
        {
            using (var db = new Context())
            {
                return (from record in db.Highscores where record.GameWord.Contains(term) select record).ToList();
            }
        }
        public int Create()
        {
            this.CreatedAt = DateTime.Now;
            using (var db = new Context())
            {
                db.Highscores.Add(this);
                db.SaveChanges();
                return Id;
            }
        }
        public int Update()
        {
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
