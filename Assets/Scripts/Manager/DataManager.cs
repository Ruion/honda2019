using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour {

    public InputField nameInput;
    public InputField emailInput;
    public InputField phoneInput;
    public ScriptableScore scoreFile;
    public Users usersCard;
    public SaveSystem ss;
    public Button submitBtn;

    /// <summary>
    /// //////////////////////////////// remember change back URL
    /// </summary>
    private const string databaseURL = "http://190905-my-honda.unicom-interactive-digital.com/submit-data.php";
   // private const string databaseURL = "http://localhost/honda/honda/submit-data.php";

    public Text sentText;
    private int totalSent;
    public GameObject emptyHandler;
    public GameObject ErrorHandler;
    public GameObject LoadingHandler;
    public GameObject SuccessSendDataHandler;
    public GameObject blockDataHandler;

    private bool canSaveToLocal = true;
    private bool canSync = true;

    private void Start()
    {
        LoadPermanentPlayer();
    }

    private void LoadPermanentPlayer()
    {
        usersCard.users = ss.LoadPermanentPlayer();
    }

    public void ClearTempUser()
    {
        usersCard.tempUser = new User();
    }

    public void SaveToLocal()
    {
        usersCard.tempUser.name = nameInput.text.ToString();
        usersCard.tempUser.email = emailInput.text.ToString();
        usersCard.tempUser.phone = phoneInput.text.ToString();
        usersCard.tempUser.register_datetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        PlayerPrefs.SetString("name", nameInput.text.ToString());
        PlayerPrefs.SetString("email", emailInput.text.ToString());
        PlayerPrefs.SetString("phone", phoneInput.text.ToString());
        PlayerPrefs.SetString("register_datetime", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    public void SaveScoreToLocal()
    {
        if (canSaveToLocal == false) return;

        canSaveToLocal = false;

        usersCard.tempUser.name = PlayerPrefs.GetString("name");
        usersCard.tempUser.email = PlayerPrefs.GetString("email");
        usersCard.tempUser.phone = PlayerPrefs.GetString("phone");
        usersCard.tempUser.register_datetime = PlayerPrefs.GetString("register_datetime");

        usersCard.tempUser.score = scoreFile.score.ToString();
           
        ss.SavePlayer();        
       
        ClearTempUser();
    }

    private string CheckSameUser(List<User> usersList, User tempUser)
    {
        string isSame = "not";

        for (int i = 0; i < usersList.Count; i++)
        {
            // if the email & phone is same
            if (usersList[i].email == tempUser.email) {
                isSame = "email";
            } 
            if(usersList[i].phone == tempUser.phone)
            {
                isSame = "phone";
            }
        }

        return isSame;      
    }

    public string CheckSameUserInput()
    {
        LoadPermanentPlayer();

        string type = "";

        for (int i = 0; i < usersCard.users.Count; i++)
        {
            // if the email & phone is same
            if (emailInput.text == usersCard.users[i].email)
            {
                type = "email";
            }
            if (phoneInput.text == usersCard.users[i].phone)
            {
                type = "phone";
            }
        }

        return type;
    }

    public bool CheckSameUserEmail()
    {
        bool emailIsSame = false;

        for (int i = 0; i < usersCard.users.Count; i++)
        {
            // if the email & phone is same
            if (emailInput.text == usersCard.users[i].email)
            {
                emailIsSame = true;
            }
        }

            return emailIsSame;
    }

    public bool CheckSameUserPhone()
    {
        bool phoneIsSame = false;

        for (int i = 0; i < usersCard.users.Count; i++)
        {
            // if the email & phone is same
            if (phoneInput.text == usersCard.users[i].phone)
            {
                phoneIsSame = true;
            }
        }

        return phoneIsSame;
    }

    private User GetSameUser(List<User> usersList, User tempUser)
    {
        for (int i = 0; i < usersList.Count; i++)
        {
            // if the email & phone is same
            if (usersList[i].email == tempUser.email &&
            usersList[i].phone == tempUser.phone)
            {
                return usersList[i];
            }
        }
        return null;
    }

    private bool UpdateSameUserScore(User oldData, User newData)
    {
        bool isUpdated = false;

        if (int.Parse(oldData.score) < int.Parse(newData.score))
        {
            oldData.score = newData.score;
            isUpdated = true;
        }

        return isUpdated;
    }

    public void SendDataToDatabase()
    {
        if (canSync == false) return;

        canSync = false;
        StartCoroutine(DataToSend());
    }

    IEnumerator DataToSend()
    {
       // Debug.Log("File Path: " + Application.persistentDataPath + "/LocalData.txt");
        ///////////////////////
        List<User> unSyncUsers = new List<User>();
        unSyncUsers = usersCard.GetUnSync(ss.LoadPlayer(), ss.LoadSyncedPlayer());
        totalSent = 0;

        

        if(unSyncUsers.Count < 1)
        {
            emptyHandler.SetActive(true);
            canSync = true;
            StartCoroutine(ReEnableSubmitButton());
            yield break;
           
        }
        else
        {
            emptyHandler.SetActive(false);
            blockDataHandler.SetActive(true);
        }
        ////////////////////////
        Debug.Log(unSyncUsers.Count +" unsync");
        for (int i = 0; i < unSyncUsers.Count; i++)
        {
            WWWForm form = new WWWForm();
            form.AddField("name", unSyncUsers[i].name);
            form.AddField("email", unSyncUsers[i].email);
            form.AddField("phone", unSyncUsers[i].phone);
            form.AddField("score", unSyncUsers[i].score);
            form.AddField("register_datetime", unSyncUsers[i].register_datetime);
            

            using (UnityWebRequest www = UnityWebRequest.Post(databaseURL, form))
            {
                
                LoadingHandler.SetActive(true);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    ErrorHandler.SetActive(true);
                    canSync = true;
                    StartCoroutine(ReEnableSubmitButton());
                }
                else
                {                  
                    var jsonData = JsonUtility.FromJson<JSONResponse>(www.downloadHandler.text);
                    Debug.Log(jsonData.result);

                    if (jsonData.result == "success")
                    {

                        totalSent++;
                        sentText.text = totalSent.ToString();
                        //   usersCard.SyncUser(unSyncUsers[i]);
                        //   usersCard.userToSync.Remove(unSyncUsers[i]);

                        ss.SaveSyncPlayer(unSyncUsers[i]);

                        SuccessSendDataHandler.SetActive(true);
                    }
                    else
                    {
                        LoadingHandler.SetActive(false);
                        canSync = true;
                        ErrorHandler.SetActive(true);
                        StartCoroutine(ReEnableSubmitButton());                        
                        yield break;
                    }
                }
            }
            LoadingHandler.SetActive(false);

            yield return new WaitForSeconds(0.2f);
        }

        string TextPath = Application.persistentDataPath + "/LocalData.txt";
        // reset users
        File.WriteAllText(TextPath, "");

        blockDataHandler.SetActive(false);
        canSync = true;
        submitBtn.interactable = true;
    }


    public void GetUnSyncUsers()
    {
        List<User> unSyncUsers = new List<User>();
        unSyncUsers = usersCard.GetUnSync(ss.LoadPlayer(), ss.LoadSyncedPlayer());

    }

    IEnumerator ReEnableSubmitButton()
    {
        yield return new WaitForSeconds(1);
        submitBtn.interactable = true;
    }
}

public class JSONResponse
{
    public string result;
}
