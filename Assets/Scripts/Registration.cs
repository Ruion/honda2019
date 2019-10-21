using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Diagnostics;

public class Registration : MonoBehaviour
{

    #region variables
    public Toggle ConsentF;
    bool Text1OK = false;
    bool Text2OK = false;
    bool Text3OK = false;
    bool userIsUnique = true;

    public Button Submit;
    public Button virtualSubmit;

    public Text NameText;
    public Text PhoneText;
    public Text EmailText;

    string MailPattern = @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$";


    string PhonePattern = @"^6?01\d{7,9}$";

    public GameObject emailWarning;
    public GameObject phoneWarning;
    public GameObject nameTick;
    public GameObject emailTick;
    public GameObject phoneTick;
    public DataManager dm;

    private int oskID;
    public float validateFrequency;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Text1OK && Text2OK && Text3OK && ConsentF.isOn && userIsUnique)
        {
            Submit.interactable = true;
            virtualSubmit.interactable = true;
        }
        else
        {
            Submit.interactable = false;
            virtualSubmit.interactable = false;
        }

    }

    public void StartValidateOnFrequency()
    {
        InvokeRepeating("Validate", 2f, validateFrequency);
    }

    public void StopValidateOnFrequency()
    {
        CancelInvoke("Validate");
    }

    private void Validate()
    {
        T1Change();
        T2Change();
        T3Change();
        CheckUserExists();
    }

    public void T1Change()
    {
        Text1OK = InputNotEmpty(NameText);
    }

    public void T2Change()
    {
        Text2OK = Regex.IsMatch(PhoneText.text, PhonePattern);
    }

    public void T3Change()
    {
        Text3OK = Regex.IsMatch(EmailText.text, MailPattern);
    }

    private bool InputNotEmpty(Text text)
    {
        bool notEmpty = true;

        if (text.text == "" || text.text == null) notEmpty = false;

        return notEmpty;
    }

    public bool CheckUserExists()
    {
        return ToggleWarnings();
    }

    private bool ToggleWarnings()
    {
        bool isUnique = true;

        bool emailIsUnique = ToggleEmailWarning();
        bool phoneIsUnique = TogglePhoneWarning();

        if(!emailIsUnique || !phoneIsUnique)
        {
            isUnique = false;
        }

        return isUnique;
    }

    private bool ToggleEmailWarning()
    {
        bool hasSame = false;

        if (dm.CheckSameUserEmail())
        {
            emailWarning.SetActive(true);
            hasSame = true;
        }
        else
        {
            emailWarning.SetActive(false);
            hasSame = false;
        }

        return hasSame;
    }

    private bool TogglePhoneWarning()
    {
        bool hasSame = false;

        if (dm.CheckSameUserPhone())
        {
            phoneWarning.SetActive(true);
            hasSame = true;
        }
        else
        {
            phoneWarning.SetActive(false);
            hasSame = false;
        }

        return hasSame;
    }
}
