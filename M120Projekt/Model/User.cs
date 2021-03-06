﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace M120Projekt.Data
{
    public class User
    {
        #region Datenbankschicht
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Anrede")]
        public string Salutation { get; set; }
        [Required]
        [Display(Name = "Vorname")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Nachname")]
        public string Lastname { get; set; }
        [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "E-mail ist ungültig.")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Passwort")]
       
        public string Password { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreatedAt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime UpdatedAt { get; set; }
        #endregion
        #region Applikationsschicht
        public User() { }

        public static List<User> All()
        {
            using (var db = new Context())
            {
                return (from record in db.Users select record).ToList();
            }
        }
        public static User ReadById(int id)
        {
            using (var db = new Context())
            {
                return (from record in db.Users where record.Id == id select record).FirstOrDefault();
            }
        }

        public static User ReadByEmail(string email)
        {
            using (var db = new Context())
            {
                return (from record in db.Users where record.Email == email.ToLower() select record).FirstOrDefault();
            }
        }

        public static bool DoesEmailExist(string email)
        {
            using (var db = new Context())
            {
                User user = (from record in db.Users where record.Email == email.ToLower() select record).FirstOrDefault();
                return user != null;
            }
        }

        public static List<User> WhereEmail(string term)
        {
            using (var db = new Context())
            {
                return (from record in db.Users where record.Email == term select record).ToList();
            }
        }
        public static List<User> LikeEmail(string term)
        {
            using (var db = new Context())
            {
                return (from record in db.Users where record.Email.Contains(term) select record).ToList();
            }
        }
        public int Create()
        {
            if (string.IsNullOrEmpty(Email)) Email = "leer@hotmail.com";
        
            using (var db = new Context())
            {
                this.Email = Email.ToLower();
                this.CreatedAt = DateTime.Now;
                this.UpdatedAt = DateTime.Now;
                db.Users.Add(this);
                db.SaveChanges();
                return Id;
            }
        }
        public int Update()
        {
            using (var db = new Context())
            {
                this.UpdatedAt = DateTime.Now;
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

        public bool IsPasswordCorrect(string submittedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(submittedPassword, Password);
        }

        public override string ToString()
        {
            return Id.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
