using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Assessment.Models
{
    public enum Roles
    {
        Admin = 1,
        Trainer = 2,
        Trainee = 3,
    }
    public class UserModel
    {
        
      
        [Key]
        public int Uid { get; set; }
        public string FName { get; set; } = null;
        public string LName { get; set; }  = null;
        public string Email { get; set; } = null;
        public string Pswd { get; set; } = null;
        public Roles Role { get; set; }


        public UserModel(int Uid,string FName, string LName,string Email, string Pswd, Roles Role)
        {
            this.Uid = Uid; 
            this.FName = FName;
            this.LName = LName;
            this.Email = Email;
            this.Pswd  = Pswd;
            this.Role = Role;
           
        }
        public UserModel()
        { }


    }
}
