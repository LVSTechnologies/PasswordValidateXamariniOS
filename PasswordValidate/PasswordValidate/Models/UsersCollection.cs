using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace PasswordValidate.Models
{
    public class UsersCollection
    {
        public List<User> Users;

        public UsersCollection()
        {
            LoadUsers().GetAwaiter();
        }

        private async Task LoadUsers()
        {
            var stringUsers = await SecureStorage.GetAsync("SavedUsers");
            Users = string.IsNullOrEmpty(stringUsers) ? new List<User>() : JsonConvert.DeserializeObject<List<User>>(stringUsers);
        }

        public async Task AddUser(User user)
        {
            if (user == null || Users == null)
                return;

            //First Add the users to the list of users
            Users.Add(user);

            //Next Serialize the list of users
            var jsonUsersList = JsonConvert.SerializeObject(Users);
            if (string.IsNullOrEmpty(jsonUsersList))
                return;

            //Next Remove all users in Secure Storage
            SecureStorage.RemoveAll();

            try
            {
                //Next Re-Add all list of users to the secure storage
                await SecureStorage.SetAsync("SavedUsers", jsonUsersList);
            }
            catch (Exception ex)
            {
                //TODO: Need to do better error handling here
                var test = ex.Message;
            }

        }

    }
}
