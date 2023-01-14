using System.Collections;
using System.Collections.Generic;
using TheCity.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheCity.AI
{
    public class CompleteLevel : MonoBehaviour
    {
        private string menuSceneName = "0MainMenu";

        //public string nextLevel = "NextLevel";

        public SceneFader sceneFader;

        public void Continue()
        {
            //sceneFader.FadeTo(nextLevel);
            sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        }

        public void Menu()
        {
            sceneFader.FadeTo(menuSceneName);
        }
    }
}