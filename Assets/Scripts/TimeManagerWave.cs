using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManagerWave : MonoBehaviour
{
    [SerializeField] private float _durationWave = 120f;
    [SerializeField] private TextMeshProUGUI _timeLeftText;


    private float _timeLeft;
    private bool _isWave = false;


    public event Action onWaveStart;
    public event Action onWaveEnd;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (_isWave)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                _isWave = false;
                SpawnBoss();
            }
            else
            {
                _timeLeftText.text = "Boss In: " + Mathf.Round(_timeLeft);
            }
        }
    }

    public void StartWave()
    {
        _timeLeft = _durationWave;
        _isWave = true;
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        onWaveStart?.Invoke();
    }

    void SpawnBoss()
    {
        onWaveEnd?.Invoke();
    }

}
