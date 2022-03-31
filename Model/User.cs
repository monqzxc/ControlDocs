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

        private string username;
        public string UserName {
            get
            {
                return username;
            }
            set
            {
                username = value;
                NotifyPropertyChanged();
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public string Sex { get; set; }

        public int RoleID { get; set; }

        public virtual Role Role { get; set; }

        dbDocs db = new dbDocs();

        
        public event PropertyChangedEventHandler PropertyChanged;
        

        public IQueryable<User> GetUserInfo()
        {                      
            return new dbDocs().Users.Where(x => x.UserName.Equals(this.UserName) && x.Password.Equals(this.Password));
        }

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

        }

        public void editUser()
        {

        }

        public void deleteUser()
        {

        }

    
     
    }
}
