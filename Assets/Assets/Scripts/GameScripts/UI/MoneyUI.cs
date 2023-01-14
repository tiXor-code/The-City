using System.Collections;
using System.Collections.Generic;
using TheCity.GameMaster;
using TMPro;
using UnityEngine;

namespace TheCity.UI
{
    public class MoneyUI : MonoBehaviour
    {
        public TextMeshProUGUI moneyText;

        // Update is called once per frame
        void Update()
        {
            moneyText.text = PlayerStats.Money.ToString() + " $";
        }
    }
}