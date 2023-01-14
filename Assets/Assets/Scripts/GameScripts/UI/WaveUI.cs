using System.Collections;
using System.Collections.Generic;
using TheCity.EnemyAI;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _waveText;

    [SerializeField]
    private GameObject _waveGameObject;

    private int _waveIndex;

    private void Update()
    {
        _waveIndex = WaveSpawner.waveIndexPublic;

        _waveText.text = "Wave " + _waveIndex;

        if (_waveIndex == 0) _waveGameObject.SetActive(false);
        else _waveGameObject.SetActive(true);        
    }
}
