using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "users", menuName ="users")]
public class Users : ScriptableObject
{
    public List<User> users;
    public List<User> syncUser;
    public List<User> userToSync;
    public List<User> userToUpdate;

    public User tempUser;

    [SerializeField]
    private int playAmount;

    public void SaveData(User user)
    {
        playAmount++;
        user.ID = playAmount;

        users.Add(user);
        
    }

    public List<User> GetUnSync(List<User> users_, List<User> syncUser_)
    {
        ClearUserToSync();

        List<User> unSyncUser = users_;
        
        // uncomment this part later

        List<User> toBeRemove = new List<User>();

        foreach (User u in unSyncUser)
        {
            for (int i = 0; i < syncUser_.Count; i++)
            {
                if(u.email == syncUser_[i].email)
                {
                    toBeRemove.Add(u);
                }
            }
        }

        for (int i = 0; i < toBeRemove.Count; i++)
        {
            unSyncUser.Remove(toBeRemove[i]);
        }

        foreach (User u in unSyncUser)
        {
            Debug.Log(u.name);
        }

        return unSyncUser;
    }

    public void ClearUserToSync()
    {
        userToSync.Clear();
    }

    public void SyncUser(User user)
    {
        user.Sync();
        syncUser.Add(user);
    }

    public void UpdateUserScore(User user, int score)
    {
        user.score = score.ToString();
    }

    public void AddUserToUpdateList(User user_)
    {
        userToUpdate.Add(user_);
    }
}
