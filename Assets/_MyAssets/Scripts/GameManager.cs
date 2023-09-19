using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isStarted = false;
    private PlayerController _player;

    
    public event Action OnGameStarted;
    public bool IsStarted => isStarted;

    public PlayerController Player => _player;
    
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
