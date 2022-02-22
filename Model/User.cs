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


        public List<User> GetUserInfo(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
            var list = new List<User>();
            var z = new dbDocs().Users.Where(x => x.UserName.Equals(this.UserName) && x.Password.Equals(this.Password)).ToList();
            foreach (var item in z)
            {

                list.Add(new User { 
                    FirstName = item.FirstName, 
                    LastName = item.LastName, 
                    NickName = item.NickName, 
                    UserName = item.UserName, 
                    Password = item.Password, 
                    Sex = item.Sex, 
                    RoleID = item.RoleID, 
                    Id = item.Id });                
            }
            return list;            
        }

        public string GetRole()
        {
            var role = db.Users.Where(x => x.UserName.Equals(this.UserName) && x.Password.Equals(this.Password)).Select(x => x.Role.RoleName).FirstOrDefault();
            if (role != null)
                return role;
            else
                return "Invalid Username and Password";
        }

        //public User(string userName, string password)
        //{
        //    this.UserName = userName;
        //    this.Password = password;

        //    var z = new dbDocs().Users.Where(x => x.UserName.Equals(this.UserName) && x.Password.Equals(this.Password)).ToList();
        //    foreach (var item in z)
        //    {
        //        this.FirstName = item.FirstName;
        //        this.LastName = item.LastName;
        //        this.NickName = item.NickName;
        //        this.Sex = item.Sex;
        //        this.RoleID = item.RoleID;
        //    }
        //}

        //public string GetRole()
        //{
        //    var role = db.Users.Where(x => x.UserName.Equals(this.UserName) && x.Password.Equals(this.Password)).Select(x => x.Role.RoleName).FirstOrDefault();
        //    if (role != null)
        //        return role;
        //    else
        //        return "Invalid Username and Password";
        //}
        public class UserHanlder
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            dbDocs db = new dbDocs();
            public UserHanlder(string userName, string password)
            {                                
                this.UserName = userName;
                this.Password = password;
            }

            public string GetRole()
            {
                var role = db.Users.Where(x => x.UserName.Equals(this.UserName) && x.Password.Equals(this.Password)).Select(x => x.Role.RoleName).FirstOrDefault();
                if (role != null)
                    return role;
                else
                    return "Invalid Username and Password";
            }
        }
        //public class UserHandler
        //{
        //    private List<User> users;
        //    public List<User> Users
        //    {
        //        get
        //        {
        //            return users;
        //        }
        //        set
        //        {
        //            users = value;
        //        }
        //    }
        //    public UserHandler()
        //    {
        //        Users = new List<User>();
        //    }
        //    public void Add(User users)
        //    {
        //        Users.Add(users);
        //    }
        //}
    }
}