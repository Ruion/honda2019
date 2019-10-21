using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartEventsOnEnable : MonoBehaviour {

    public UnityEvent eventsOEnable;

	void OnEnable()
    {
        eventsOEnable.Invoke();
    }

}
