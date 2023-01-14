using TheCity.GameMaster;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheCity.UI
{
    public class LivesUI : MonoBehaviour
    {
        public TextMeshProUGUI livesText;

        // Update is called once per frame
        void Update()
        {
            livesText.text = PlayerStats.Lives.ToString() + " LIVES";
        }
    }
}