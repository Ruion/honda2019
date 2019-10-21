using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "Score")]
public class ScriptableScore : ScriptableObject {

    public int score;
    public int maximumScore = 999;
    public int minimumScore = 0;


    public void AddScore(int amount)
    {
        score += amount;
    }
}
