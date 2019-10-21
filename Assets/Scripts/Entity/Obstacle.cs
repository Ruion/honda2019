using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour {

    public UnityEvent eventOnTrigger;

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ScoreManager.instance.MinusScore(1);
            eventOnTrigger.Invoke();
            Destroy(transform.parent.gameObject);
        }
    }
}
