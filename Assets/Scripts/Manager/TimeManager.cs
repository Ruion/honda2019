using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimeManager : MonoBehaviour {

    public int countDownSeconds
    {
        get { return second; }
        set { second = value; }
    }
    public int second = 60;

    [Header("Optional Countdown TextMeshPro")]
    public TextMeshProUGUI[] countDownTexts;
    public UnityEvent countdownEndEvents;

    void Start()
    {
        UpdateText();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            second = 1;
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1f);
        second--;

        // update textmesh text if textmesh components exists
        if (countDownTexts.Length > 0) { UpdateText(countDownTexts, second); }

        
        if (second <= 0)
        {
            // Execute event on countdown ended
            countdownEndEvents.Invoke();
            StopAllCoroutines();
            yield return null;
        }

        // repeat countdown until time become 0
        StartCoroutine(StartCountdown());

    }

    void UpdateText(TextMeshProUGUI[] texts_, int number)
    {
        for (int t = 0; t < texts_.Length; t++)
        {
            texts_[t].text = number.ToString();
        }
    }

    public void UpdateText()
    {
        for (int t = 0; t < countDownTexts.Length; t++)
        {
            countDownTexts[t].text = second.ToString();
        }
    }
}
