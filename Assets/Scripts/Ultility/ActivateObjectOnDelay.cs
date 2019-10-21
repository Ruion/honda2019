using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectOnDelay : MonoBehaviour
{
    public bool activateOnStart = true;
    public GameObject target;
    public float delay;

    private void Start()
    {
        if(activateOnStart) Invoke("ActivateObject", delay);
    }

    public void ActivateObject() { target.SetActive(true); }
}
