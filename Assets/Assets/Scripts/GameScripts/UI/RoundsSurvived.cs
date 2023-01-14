using System.Collections;
using System.Collections.Generic;
using TheCity.GameMaster;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheCity.UI
{
    public class RoundsSurvived : MonoBehaviour
    {
        public TextMeshProUGUI roundsText;

        void OnEnable()
        {
            StartCoroutine(AnimateText());
        }

        IEnumerator AnimateText()
        {
            roundsText.text = "0";
            int round = 0;

            yield return new WaitForSeconds(.7f);

            while (round < PlayerStats.Rounds)
            {
                round++;
                roundsText.text = round.ToString();

                yield return new WaitForSeconds(.05f);
            }
        }
    }
}