using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelParent : MonoBehaviour
{
    private bool _gameStarted;
    private Vector3 _speed;
    private void OnEnable()
    {
        GameManager.instance.OnGameStarted += StartMoving;
    }
    private void OnDisable()
    {
        GameManager.instance.OnGameStarted -= StartMoving;
    }

    private void Update()
    {
        if(!_gameStarted) return;

        transform.position -= _speed * Time.deltaTime;
    }

    private void StartMoving()
    {
        _gameStarted = true;
        _speed = new Vector2(GameManager.instance.Player.Speed, 0f);
        // Mas logica para el nivel al iniciar
    }
}
