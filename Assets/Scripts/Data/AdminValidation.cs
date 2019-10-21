using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdminValidation : MonoBehaviour
{
    public string password = "hondaBoss";
    public InputField passwordInput;

    public UnityEvent OnPasswordCorrect;


    public void Validate()
    {
        if(passwordInput.text == password)
        {
            OnPasswordCorrect.Invoke();
            passwordInput.text = "";
           
        }
    }
}
