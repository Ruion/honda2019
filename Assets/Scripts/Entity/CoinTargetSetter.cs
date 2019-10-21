using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTargetSetter : MonoBehaviour {

    public ObjectMover objMover;

    public float targetInterval = 1f;

    void OnEnable()
    {
        StartCoroutine(SetObjectsTarget());
    }

    public void RunSetObjectTargets()
    {
        StartCoroutine(SetObjectsTarget());
    }

	IEnumerator SetObjectsTarget()
    {
        yield return new WaitForEndOfFrame();

        objMover.enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject children = transform.GetChild(i).gameObject;
            ObjectMover objectMover = children.GetComponentInChildren<ObjectMover>();
            objectMover.target = objMover.target;
            children.SetActive(true);
            objectMover.enabled = true;
            
            yield return new WaitForSeconds(targetInterval);
        }
    }

}
