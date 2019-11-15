using UnityEngine;
using TMPro;

public class ErrorHandlePopUp : MonoBehaviour
{
    public GameObject PopUp;
    public TextMeshProUGUI title;
    public TextMeshProUGUI msg;
    public bool DebugMode;
    string error;

    void Awake()
    {
        if (!DebugMode) gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {}

    void OnEnable()
    {
        if (!DebugMode) gameObject.SetActive(false);

        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error)
        {
            error = error + "\n" + " Error: " + logString;
            PopUp.SetActive(true);
            title.text = "Error";
            msg.text = error;
        }

    }
}