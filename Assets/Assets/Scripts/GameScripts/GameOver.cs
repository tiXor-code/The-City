using TheCity.GameMaster;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TheCity.UI;

namespace TheCity.AI
{
    public class GameOver : MonoBehaviour
    {
        private string menuSceneName = "0MainMenu";

        public SceneFader sceneFader;

        public void Retry()
        {
            sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        }   
        
        public void Menu()
        {            
            sceneFader.FadeTo(menuSceneName);
        }
    }
}