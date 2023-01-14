using System.Collections;
using System.Collections.Generic;
using TheCity.UI;
using TMPro;
using UnityEngine;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheCity.UI
{
    public class ADController : MonoBehaviour
    {
        public static ADController instance;
        public Slider slider;
        public TextMeshProUGUI loadingText;
        public GameObject loadingPanel;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void ResetScreen()
        {
            loadingPanel.SetActive(true);
            //loadingText.text = "0%";
            slider.value = 0;
        }

        public void SetLoadingValue(float value)
        {
            //loadingText.text = (int)(value * 100) + " %";
            slider.value = value;
        }

        public void HideLoadingScreen()
        {
            loadingPanel.SetActive(false);
        }

        [SerializeField] private float _timer = 0;
        [SerializeField] private float _timerDefault = 0;
        [SerializeField] private float _timerLimit = 7;

        private void Update()
        {
            if (ADController.instance.loadingPanel.activeInHierarchy == true)
            {
                ADController.instance.SetLoadingValue(_timer / _timerLimit);
                if (_timer < _timerLimit)
                {
                    _timer += Time.deltaTime;
                }
                else
                {
                    ADController.instance.HideLoadingScreen();
                    _timer = _timerDefault;
                }
            }
        }
    }
}