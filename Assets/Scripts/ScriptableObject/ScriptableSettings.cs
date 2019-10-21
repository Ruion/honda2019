using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameSettings", menuName = "GameSettings")]
public class ScriptableSettings : ScriptableObject {

    public float swipeThreshold;
    public float exitTime;
}
