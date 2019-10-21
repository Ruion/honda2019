using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardScript : MonoBehaviour
{

    public InputField TextField;

    public InputField inputField
    {
        get { return inputTextField; }
        set
        {
            inputTextField = value;
            TextField.text = inputTextField.text;
        }
    }

    public InputField inputTextField;
    public GameObject RusLayoutSml, RusLayoutBig, EngLayoutSml, EngLayoutBig, SymbLayout;



    public void alphabetFunction(string alphabet)
    {


        TextField.text=TextField.text + alphabet;
        inputTextField.text=inputTextField.text + alphabet;

    }

    public void ClearAlphabet()
    {
        TextField.text = "";
    }

    public void BackSpace()
    {

        if(TextField.text.Length>0) TextField.text= TextField.text.Remove(TextField.text.Length-1);
        if(inputTextField.text.Length>0) inputTextField.text= inputTextField.text.Remove(inputTextField.text.Length-1);

    }

    public void CloseAllLayouts()
    {

        RusLayoutSml.SetActive(false);
        RusLayoutBig.SetActive(false);
        EngLayoutSml.SetActive(false);
        EngLayoutBig.SetActive(false);
        SymbLayout.SetActive(false);

    }

    public void ShowLayout(GameObject SetLayout)
    {
        
        CloseAllLayouts();
        SetLayout.SetActive(true);

    }

}
