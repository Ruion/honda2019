using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource addScore;
    public AudioSource minusScore;
    public AudioSource start;
    public AudioSource end;

	public void AddScore(){ addScore.Play(); }
	public void MinusScore(){ minusScore.Play(); }
	public void StartGame(){ start.Play(); }
	public void GameOver(){ end.Play(); }

}
