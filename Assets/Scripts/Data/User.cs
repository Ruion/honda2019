using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class User
    {
        public int ID;
        public string name;
        public string email;
        public string phone;
        public string score;
        public string register_datetime;
        public string is_sync = "false";

        public void Sync()
        {
            is_sync = "true";
        }

        public User(User user)
        {
            ID = user.ID;
            name = user.name;
            email = user.email;
            phone = user.phone;
            score = user.score;
            register_datetime = user.register_datetime;
            is_sync = user.is_sync;
        }

    public User()
    {

    }

    }
