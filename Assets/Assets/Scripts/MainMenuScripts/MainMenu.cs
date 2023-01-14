using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheCity.UI
{
    public class MainMenu : MonoBehaviour
    {
        private string levelToLoad = "2TDScene";

        public SceneFader sceneFader;

        public void Play()
        {
            sceneFader.FadeTo(levelToLoad);
        }

        public void Quit()
        {
            Debug.Log("Exiting...");
            Application.Quit();
        }
    }
}