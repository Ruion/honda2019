using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Honda
{
    public class GameManager : MonoBehaviour
    {

        public UnityEvent StartGameEvents;
        public UnityEvent GameOverEvents;

        [HideInInspector]
        public bool isGameEnded;

        // Use this for initialization
        public void StartGame()
        {
            StartGameEvents.Invoke();
        }

        // Update is called once per frame
        public void GameOver()
        {
            isGameEnded = true ;
            GameOverEvents.Invoke();
        }
    }
}
