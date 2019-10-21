using UnityEngine;

public class TimerDestroyer : MonoBehaviour
{
    public float DestroyAfterSeconds;
    public GameObject target;

    private void OnEnable()
    {
        Invoke("DestroyTarget", DestroyAfterSeconds);
    }

    void DestroyTarget()
    {
       Destroy(target);
    }


}
