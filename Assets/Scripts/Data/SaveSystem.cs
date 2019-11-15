using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;

public class SaveSystem : MonoBehaviour
{
    public Users userCard;
    private string TextPath;
    private string TextPath2;
    private string syncPath;

    public int numberToPopulate = 50;

    void Start()
    {
            TextPath = Application.persistentDataPath + "/LocalData.txt";
            TextPath2 = Application.persistentDataPath + "/LocalDataPermanent.txt";
            syncPath = Application.persistentDataPath + "/SyncedData.txt";
            //if the file does not exist then create a new one
            if (!File.Exists(TextPath))
            {
                File.WriteAllText(TextPath, "");
            }
            if (!File.Exists(TextPath2))
            {
                File.WriteAllText(TextPath2, "");
            }

            if (!File.Exists(syncPath))
            {
                File.WriteAllText(syncPath, "");
            }
    }

    private void SetFilePath()
    {
        TextPath = Application.persistentDataPath + "/LocalData.txt";
        TextPath2 = Application.persistentDataPath + "/LocalDataPermanent.txt";
        syncPath = Application.persistentDataPath + "/SyncedData.txt";
    }

    public void SavePlayer()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

            string datetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //to get the datetime format in string correct
                                                                                   //define the text to write inside txt file
            string serializedData = "Data," + userCard.tempUser.name + "," + userCard.tempUser.email + "," + userCard.tempUser.phone + "," + userCard.tempUser.score + "," + datetime + "," + userCard.tempUser.is_sync + "\n";
            //string serializedData = "Data," + Name_t.text + "," + Phone_t.text + "," + Email_t.text + "," + sharenew_text + "," + above18_text + "," + happyrecieve_text + "," + pdpa_text + "\n";
            StreamWriter writer = new StreamWriter(TextPath, true); //open txt file (doesnt actually open it inside the game)
            StreamWriter writer2 = new StreamWriter(TextPath2, true); //save data in another file for permanent local data that is not going to be delete after send data to database
            writer.Write(serializedData); //write into txt file the string declared above
            writer2.Write(serializedData);
            writer.Close(); //close the txt file again
            writer2.Close();

        // the code that you want to measure comes here
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Debug.LogError(elapsedMs.ToString() + "ms");

    }

    public void SaveSyncPlayer()
    {
        foreach (var s in userCard.syncUser)
        {
            string datetime2 = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //to get the datetime format in string correct
                                                                                   //define the text to write inside txt file
            string serializedData2 = "Data," + s.name + "," + s.email + "," + s.phone + "," + s.score + "," + datetime2 + "," + s.is_sync + "\n";
            //string serializedData = "Data," + Name_t.text + "," + Phone_t.text + "," + Email_t.text + "," + sharenew_text + "," + above18_text + "," + happyrecieve_text + "," + pdpa_text + "\n";
            StreamWriter writer2 = new StreamWriter(syncPath, true); //open txt file (doesnt actually open it inside the game)
            writer2.Write(serializedData2); //write into txt file the string declared above
            writer2.Close();
        }

        
        // reset users
        File.WriteAllText(TextPath, "");
    }

    public void SaveSyncPlayer(User s)
    {

            string datetime2 = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //to get the datetime format in string correct
                                                                                    //define the text to write inside txt file
            string serializedData2 = "Data," + s.name + "," + s.email + "," + s.phone + "," + s.score + "," + datetime2 + "," + s.is_sync + "\n";
            //string serializedData = "Data," + Name_t.text + "," + Phone_t.text + "," + Email_t.text + "," + sharenew_text + "," + above18_text + "," + happyrecieve_text + "," + pdpa_text + "\n";
            StreamWriter writer2 = new StreamWriter(syncPath, true); //open txt file (doesnt actually open it inside the game)
            writer2.Write(serializedData2); //write into txt file the string declared above

        writer2.Close();
    }


    public List<User> LoadPlayer()
    {
        SetFilePath();

        string line;

        List<string> lines = new List<string>();

        List<User> users = new List<User>();

        // suspect this line get record from non-permanent user
        // suppose to be Textpath2
        StreamReader reader = new StreamReader(TextPath, true); //open the txt file to read
                                                                //loop read each line until end
       // Debug.Log(reader);

        while ((line = reader.ReadLine()) != null)
        {
          //  Debug.Log("Reader read line: " + line);
            lines.Add(line); //add each line into list
        }

        reader.Close();

        foreach (string dataline in lines)
        {
            string[] DataRegistration = dataline.Split(','); //to split the data into array by ','
            User user = new User();
            users.Add(user);

            user.name = DataRegistration[1];
            user.email = DataRegistration[2];
            user.phone = DataRegistration[3];
            user.score = DataRegistration[4];
            user.register_datetime = DataRegistration[5];
            user.is_sync = DataRegistration[6];

            
        }

        return users;
    }

    public List<User> LoadSyncedPlayer()
    {
        SetFilePath();

        string line;

        List<string> lines = new List<string>();

        List<User> users = new List<User>();

        StreamReader reader = new StreamReader(syncPath, true); //open the txt file to read
                                                                //loop read each line until end
       // Debug.Log(reader);

        while ((line = reader.ReadLine()) != null)
        {
          //  Debug.Log("Reader read line: " + line);
            lines.Add(line); //add each line into list
        }

        reader.Close();

      //  Debug.Log("Finish fetched data.");

        foreach (string dataline in lines)
        {
            string[] DataRegistration = dataline.Split(','); //to split the data into array by ','
            User user = new User();
            user.name = DataRegistration[1];
            user.email = DataRegistration[2];
            user.phone = DataRegistration[3];
            user.score = DataRegistration[4];
            user.register_datetime = DataRegistration[5];
            user.is_sync = DataRegistration[6];

            users.Add(user);
        }

        return users;
    }


    public List<User> LoadPermanentPlayer()
    {
        SetFilePath();

        string line;

        List<string> lines = new List<string>();

        List<User> users = new List<User>();

        StreamReader reader = new StreamReader(TextPath2, true); //open the txt file to read
                                                                //loop read each line until end
       // Debug.Log(reader);

        while ((line = reader.ReadLine()) != null)
        {
          //  Debug.Log("Reader read line: " + line);
            lines.Add(line); //add each line into list
        }

        reader.Close();

       // Debug.Log("Finish fetched data.");

        foreach (string dataline in lines)
        {
            string[] DataRegistration = dataline.Split(','); //to split the data into array by ','
            User user = new User();
            

            user.name = DataRegistration[1];
            user.email = DataRegistration[2];
            user.phone = DataRegistration[3];
            user.score = DataRegistration[4];
            user.register_datetime = DataRegistration[5];
            user.is_sync = DataRegistration[6];
           
            users.Add(user);
        }

        return users;
    }

    public void Populate()
    {
        SetFilePath();

        for (int i = 0; i < numberToPopulate; i++)
        {
            User user = new User();
            user.name = "p" + i.ToString();
            user.email = "p" + i.ToString() + "gmail.com";
            user.phone = "014686930" + i.ToString();
            user.score = i.ToString();
            user.register_datetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string serializedData = "Data," + user.name + "," + user.email + "," + user.phone + "," + user.score + "," + user.register_datetime + "," + "false" + "\n";
            //string serializedData = "Data," + Name_t.text + "," + Phone_t.text + "," + Email_t.text + "," + sharenew_text + "," + above18_text + "," + happyrecieve_text + "," + pdpa_text + "\n";
            StreamWriter writer = new StreamWriter(TextPath, true); //open txt file (doesnt actually open it inside the game)
            StreamWriter writer2 = new StreamWriter(TextPath2, true); //save data in another file for permanent local data that is not going to be delete after send data to database
            writer.Write(serializedData); //write into txt file the string declared above
            writer2.Write(serializedData);
            writer.Close(); //close the txt file again
            writer2.Close();
        }
    
    }

    public void ClearPermanentUser()
    {
        SetFilePath();

        // reset users
        File.WriteAllText(TextPath2, "");
    }

    public void ClearTemporaryUser()
    {
        SetFilePath();

        // reset users
        File.WriteAllText(TextPath, "");
    }

}
