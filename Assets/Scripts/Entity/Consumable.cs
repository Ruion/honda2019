using UnityEngine;
using UnityEngine.Events;

public class Consumable : MonoBehaviour {

    public UnityEvent eventOnTrigger;

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ScoreManager.instance.AddScore(2);
            eventOnTrigger.Invoke();
            Destroy(transform.parent.gameObject, .02f);
        }
    }
}
