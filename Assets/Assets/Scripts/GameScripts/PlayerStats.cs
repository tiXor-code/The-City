using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheCity.GameMaster
{
    public class PlayerStats : MonoBehaviour
    {
        public static int Money;
        public int startMoney = 400;

        public static int Lives;
        public int startLives = 50;

        public static int Rounds;

        void Start()
        {
            Money = startMoney;
            Lives = startLives;

            Rounds = 0;
        }
    }
}