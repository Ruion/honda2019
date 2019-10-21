using UnityEngine;
using TMPro;

public class ScoreVisualizer : MonoBehaviour {

    public TextMeshProUGUI[] scoreTexts;
    private int score;
    public ScriptableScore scriptableScore;
    [Header("Caution")]
    public bool clearScoreOnNewGame;

    void OnEnable()
    {
        if (clearScoreOnNewGame) scriptableScore.score = 0;
    }

    public void UpdateText(int amount)
    {
        score += amount;
        float scoreF = score;

        scoreF = Mathf.Clamp(scoreF, scriptableScore.minimumScore, scriptableScore.maximumScore);

        score = (int)scoreF;
        VisualiseScore();

        scriptableScore.score = score;
    }

    public void VisualiseScore()
    {
        for (int t = 0; t < scoreTexts.Length; t++)
        {
            scoreTexts[t].text = score.ToString();
        }
    }

    public void VisualiseScore(ScriptableScore scriptableScore)
    {
        for (int t = 0; t < scoreTexts.Length; t++)
        {
            scoreTexts[t].text = scriptableScore.score.ToString();
        }
    }

}
