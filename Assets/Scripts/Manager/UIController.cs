using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine.Networking;

public class UIController : MonoBehaviour
{
    public GameObject ErrorHandler;
    public GameObject LoadingHandler;
    public GameObject ThankyouHandler;
    public GameObject SuccessSendDataHandler;

    public bool NameText = false;
    public bool PhoneText = false;
    public bool EmailText = false;

    public Text Name_t;
    public Text Phone_t;
    public Text Email_t;
    public Toggle PDPA;
    public Scrollbar pdpascroll;

    public Button SubmitBtn;

    string MailPattern = @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$";

    string PhonePattern = @"^6?01\d{7,9}$";

    private const string databaseURL = "http://origins.unicom-interactive-digital.com/savedata.php";

    StreamReader reader;
    string TextPath;
    List<string> lines = new List<string>();

    float timeS = 0f;
    bool OnClick = false;

    // Start is called before the first frame update
    void Start()
    {
        //TextPath = Application.dataPath + "/LocalData.txt";
        TextPath = Application.persistentDataPath + "/LocalData.txt";
        if (!File.Exists(TextPath))
        {
            File.WriteAllText(TextPath, "");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(NameText && PhoneText && EmailText && PDPA.isOn)
        {
            SubmitBtn.interactable = true;
        }
        else
        {
            SubmitBtn.interactable = false;
        }

        if(OnClick)
        {
            timeS += Time.deltaTime;
        }
    }

    public void ClickOn()
    {
        OnClick = true;
    }
    public void ClickOff()
    {
        OnClick = false;
        if(timeS >= 1)
        {
            SendDataToDatabase();
        }
        timeS = 0;
    }

    public void NamePut()
    {
        NameText = true;
    }
    public void PhonePut()
    {
        PhoneText = Regex.IsMatch(Phone_t.text, PhonePattern);
    }
    public void EmailPut()
    {
        EmailText = Regex.IsMatch(Email_t.text, MailPattern);
    }

    public void SubmitEvent()
    {
        string serializedData = "Data," + Name_t.text + "," + Phone_t.text + "," + Email_t.text + "\n";
        StreamWriter writer = new StreamWriter(TextPath, true);
        writer.Write(serializedData);
        writer.Close();
        ThankyouHandler.SetActive(true);
        StartCoroutine(ReloadPage());
    }

    IEnumerator ReloadPage()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Registration");
    }

    public void SendDataToDatabase()
    {
         StartCoroutine(DataToSend());
    }

    IEnumerator DataToSend()
    {
        foreach (string dataline in lines)
        {
            string[] DataRegistration = dataline.Split(',');
            Debug.Log(DataRegistration[0]);
            Debug.Log(DataRegistration[1]);
            Debug.Log(DataRegistration[2]);
            Debug.Log(DataRegistration[3]);
            
            WWWForm form = new WWWForm();
            form.AddField("name", DataRegistration[1]);
            form.AddField("phone", DataRegistration[2]);
            form.AddField("email", DataRegistration[3]);
            using (UnityWebRequest www = UnityWebRequest.Post(databaseURL, form))
            {
                LoadingHandler.SetActive(true);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    ErrorHandler.SetActive(true);
                    reader.Close();
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    SuccessSendDataHandler.SetActive(true);
                    reader.Close();
                    File.WriteAllText(TextPath, "");
                }
            }
            LoadingHandler.SetActive(false);
            //yield return new WaitForSeconds(0.2f);
        }
    }

    public void CloseError()
    {
        ErrorHandler.SetActive(false);
        SceneManager.LoadScene("Registration");
    }
    public void CloseSuccess()
    {
        SuccessSendDataHandler.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
