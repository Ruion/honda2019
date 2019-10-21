using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartEventsOnAwake : MonoBehaviour {

    public UnityEvent eventsOnAwake;

	void Awake()
    {
        eventsOnAwake.Invoke();
    }

}
