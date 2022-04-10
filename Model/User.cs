namespace DocsControl.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public partial class User : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string UserName { get; set; }
           
        
        //private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public string Sex { get; set; }

        public int RoleID { get; set; }

        public virtual Role Role { get; set; }

        dbDocs db = new dbDocs();

        
        public event PropertyChangedEventHandler PropertyChanged;
        

        //public IQueryable<User> GetUserInfo()
        //{                      
        //    return new dbDocs().Users.Where(x => x.UserName.Equals(this.UserName) && x.Password.Equals(this.Password));
        //}

        public string GetNickname()
        {
            var nickname = db.Users.Where(x => x.UserName.Equals(this.UserName) && x.Password.Equals(this.Password)).Select(x => string.Concat( x.RoleID, "|", x.NickName)).FirstOrDefault();
            if (nickname != null)
                return nickname;
            else
                return "Invalid Username and Password";
        }

        public void addUser()
        {
            var u = new User()
            {
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                NickName = NickName,
                RoleID = RoleID,
                Password = Password,
                Sex = Sex,                
            };
            db.Users.Add(u);
            db.SaveChanges();
        }

        public void editUser()
        {
            var u = db.Users.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            u.UserName = UserName;
            u.FirstName = FirstName;
            u.LastName = LastName;
            u.NickName = NickName;
            u.RoleID = RoleID;
            u.Password = Password;
            u.Sex = Sex;
            db.SaveChanges();
        }

        public void deleteUser()
        {

        }

    
     
    }
}
