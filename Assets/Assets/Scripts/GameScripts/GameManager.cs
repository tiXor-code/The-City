using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheCity.GameMaster
{
    public class GameManager : MonoBehaviour
    {
        public static bool GameIsOver;

        public GameObject gameOverUI;
        public GameObject completeLevelUI;

        void Start()
        {
            GameIsOver = false;
        }

        void Update()
        {
            if(GameIsOver)
                return;

            if(PlayerStats.Lives <= 0)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            GameIsOver = true;    
            gameOverUI.SetActive(true);
        }

        public void WinLevel()
        {            
            completeLevelUI.SetActive(true);
        }
    }
}