namespace DocsControl.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public string Sex { get; set; }

        public int RoleID { get; set; }

        public virtual Role Role { get; set; }

        dbDocs db = new dbDocs();


        public IQueryable<User> GetUserInfo()
        {                      
            return new dbDocs().Users.Where(x => x.UserName.Equals(this.UserName) && x.Password.Equals(this.Password));
        }

        public string GetNickname()
        {
            var nickname = db.Users.Where(x => x.UserName.Equals(this.UserName) && x.Password.Equals(this.Password)).Select(x => x.NickName).FirstOrDefault();
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
