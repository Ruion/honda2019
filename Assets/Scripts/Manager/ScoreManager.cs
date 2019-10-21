using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance;

    public ScoreVisualizer scoreVisualizer;
    public ScoreVisualizer bestScoreVisualizer;

    public SoundManager soundManager;
    public Transform player;
    public Transform scoreEffectContainer;
    public GameObject addScoreEffectPrefab;
    public GameObject minusScoreEffectPrefab;

    void Awake()
    {
        instance = this;
    }

	public void AddScore(int amount)
    {
        soundManager.AddScore();
        SpawnScoreEffect(addScoreEffectPrefab);
       scoreVisualizer.UpdateText(amount);             
    }

    public void MinusScore(int amount)
    {
        soundManager.MinusScore();
        SpawnScoreEffect(minusScoreEffectPrefab);
        scoreVisualizer.UpdateText(-amount);       
    }

    public void CompareBestScore()
    {
        if (scoreVisualizer.scriptableScore.score > bestScoreVisualizer.scriptableScore.score)
        {
            bestScoreVisualizer.scriptableScore.score = scoreVisualizer.scriptableScore.score;
        }

    }

    public void SpawnScoreEffect(GameObject effectPrefab)
    {
        Instantiate(effectPrefab, player.position + Vector3.up , Quaternion.identity, scoreEffectContainer);
    }

}
