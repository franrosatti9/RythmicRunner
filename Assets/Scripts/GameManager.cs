using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isStarted = false;

    public event Action OnGameStarted;
    public bool IsStarted => isStarted;
    
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && !isStarted)
        {
            isStarted = true;
            OnGameStarted.Invoke();
        }
    }
}
