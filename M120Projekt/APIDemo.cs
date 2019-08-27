using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using M120Projekt.Data;
using M120Projekt.Model;

namespace M120Projekt
{
    static class APIDemo
    {
        #region User

        private static int _newUserId;
        private static int _newWordId;
        private static int _newHighScoreId;
        // Create
        public static void DemoUserCreate()
        {
            Debug.Print("--- DemoUserCreate ---");
            // User
            Data.User user = new User();
            user.Email = "test@hotmail.com";
            user.Salutation = "Herr";
            user.Lastname = "Testmann";
            user.Firstname = "Test";
            user.Password = "TestPassword";
            _newUserId = user.Create();
            Debug.Print("User erstellt mit Id:" + _newUserId);
        }
        /*
        public static void DemoACreateKurz()
        {
            Data.User klasseA2 = new User { Email = "change@hotmail.com", CreateAt = DateTime.Today };
            int klasseA2Id = klasseA2.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + klasseA2Id);
        }*/

        // Read
        public static void DemoUserRead()
        {
            Debug.Print("--- DemoUserRead ---");
            // Demo liest alle
            foreach (User user in User.All())
            {
                Debug.Print("User Id:" + user.Id + " Email:" + user.Email);
            }
        }
        // Update
        public static void DemoUserUpdate()
        {
            Debug.Print("--- DemoUserUpdate ---");
            // User ändert Attribute
            User user = User.ReadById(_newUserId);
            user.Email = "changed@hotmail.com";
            user.Update();
        }
        // Delete
        public static void DemoUserDelete()
        {
            Debug.Print("--- DemoUserDelete ---");
            User.ReadById(_newUserId).Delete();
            Debug.Print($"User mit Id {_newUserId} gelöscht");
        }
        #endregion

        #region Word
        public static void DemoWordCreate()
        {
            Debug.Print("--- DemoWordCreate ---");
            Word word = new Word();
            word.Name = "Winter";
            word.IsActive = true;
            word.UserId = _newUserId;
            word.CreatedAt = DateTime.Now;
            word.UpdatedAt = DateTime.Now;
            _newWordId = word.Create();
            Debug.Print("Word erstellt mit Id:" + _newWordId);
        }

        // Read
        public static void DemoWordRead()
        {
            Debug.Print("--- DemoWordRead ---");
            foreach (Word word in Word.All())
            {
                Debug.Print("Word Id:" + word.Id + " Word Name:" + word.Name);
            }
        }
        // Update
        public static void DemoWordUpdate()
        {
            Debug.Print("--- DemoWordUpdate ---");
            Word word = Word.ReadById(_newWordId);
            word.Name = "Testing Word";
            word.Update();
        }
        // Delete
        public static void DemoWordDelete()
        {
            Debug.Print("--- DemoWordDelete ---");
            Word.ReadById(_newWordId).Delete();
            Debug.Print($"Word mit Id {_newWordId} gelöscht");
        }
        #endregion

        #region Highscore
        public static void DemoHighscoreCreate()
        {
            Debug.Print("--- DemoHighscoreCreate ---");
            Highscore highscore = new Highscore();
            highscore.GameWord = "Winter";
            highscore.Points = 500;
            highscore.UserId = _newUserId;
            highscore.CreatedAt = DateTime.Now;
            _newHighScoreId = highscore.Create();
            Debug.Print("Highscore erstellt mit Id:" + _newHighScoreId);
        }

        // Read
        public static void DemoHighscoreRead()
        {
            Debug.Print("--- DemoHighscoreRead ---");
            foreach (Highscore highscore in Highscore.All())
            {
                Debug.Print("Highscore Id:" + highscore.Id + " Points:" + highscore.Points);
            }
        }
        // Update
        public static void DemoHighscoreUpdate()
        {
            Debug.Print("--- DemoHighscoreUpdate ---");
            Highscore highscore = Highscore.ReadById(_newHighScoreId);
            highscore.Points = 1000;
            highscore.Update();
        }
        // Delete
        public static void DemoHighscoreDelete()
        {
            Debug.Print("--- DemoHighscoreDelete ---");
            Highscore.ReadById(_newHighScoreId).Delete();
            Debug.Print($"Highscore mit Id {_newHighScoreId} gelöscht");
        }
        #endregion
    }
}
