using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using iet_120.Commands;
using iet_120.Model;
using iet_120.Notification;

namespace iet_120.ViewModel
{
    public class UserViewModel : PropertyChangedNotification
    {
        private static UserViewModel userViewModel;
        private static string Filename = Directory.GetCurrentDirectory() + "\\Output.xml";

        public ObservableCollection<User> Users
        {
            get { return GetValue(() => Users); }
            set { SetValue(() => Users, value); }
        }

        public User NewUser
        {
            get { return GetValue(() => NewUser); }
            set { SetValue(() => NewUser, value); }
        }

        public RelayCommand SaveCommand { get; set; }

        public RelayCommand ClearCommand { get; set; }

        public RelayCommand SaveDataCommand { get; set; }

        public static int Errors { get; set; }

        public static UserViewModel SharedViewModel()
        {
            return userViewModel ?? (userViewModel = new UserViewModel());
        }

        private UserViewModel()
        {
            if (File.Exists(Filename))
            {
                Users = Deserialize<ObservableCollection<User>>();
            }
            else
            {
                Users = new ObservableCollection<User>();
                //Users.Add(new User { Id = 1, Name = "William", Email = "william@hotmail.com", RepeatEmail = "william@hotmail.com", Age = 23, Gender = Gender.Male });
            }
            NewUser = new User();
            SaveCommand = new RelayCommand(Save, CanSave);
            ClearCommand = new RelayCommand(Clear);
            SaveDataCommand = new RelayCommand(SaveData);
        }

        public void Save(object parameter)
        {
            Users.Add(NewUser);
            Clear(this);
        }

        public bool CanSave(object parameter)
        {
            if (Errors == 0)
                return true;
            else
                return false;
        }

        public void Clear(object parameter)
        {
            NewUser = new User();
        }

        public void SaveData(object parameter)
        {
            var result = Serialize<ObservableCollection<User>>(Users);
            if (result) MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            else MessageBox.Show("Data Not Saved", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool Serialize<T>(T value)
        {
            if (value == null)
            {
                return false;
            }
            try
            {
                XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));
                Stream stream = new FileStream(Filename, FileMode.CreateNew);
                _xmlserializer.Serialize(stream, value);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private T Deserialize<T>()
        {
            if (string.IsNullOrEmpty(Filename))
            {
                return default(T);
            }
            try
            {
                XmlSerializer _xmlSerializer = new XmlSerializer(typeof(T));
                Stream stream = new FileStream(Filename, FileMode.Open, FileAccess.Read);
                var result = (T)_xmlSerializer.Deserialize(stream);
                stream.Close();
                return result;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}