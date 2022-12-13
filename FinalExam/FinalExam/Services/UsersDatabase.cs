using System.Collections.Generic;

namespace FinalExam.Models
{
    public class UsersDatabase
    {
        private Dictionary<string, UserModel> users = new Dictionary<string, UserModel>();

        public UsersDatabase()
        {
            this.users["finalexam"] = new UserModel() { Username = "finalexam", Password = "12345" };
        }

        public bool ValidatePassword(string username, string password)
        {
            if (users.TryGetValue(username, out UserModel model))
            {
                return model.Password == password;
            }
            return false;
        }

        public IEnumerable<string> AllUsernames
        {
            get { return users.Keys; }
        }
    }
}
