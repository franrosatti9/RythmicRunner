using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelParent;
    [SerializeField] private GameObject dashObstacle;
    [SerializeField] private GameObject jumpObstacle;
    [SerializeField] private GameObject slideObstacle;
    [SerializeField] private GameObject damageableObstacle;
    [SerializeField] private float obstacleOffset;
    private float beatsPerSecond;
    private PlayerController _player;

    private void Awake()
    {
        _player = GameManager.instance.Player;
        beatsPerSecond = BeatManager.instance.Bpm / 60f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateObstacle(InputType input, float beat, float height, float length)
    {
        Vector3 pos = Vector3.zero;
        pos.x = GetDistanceByBeat(beat) + obstacleOffset;
        switch (input)
        {
            case InputType.Jump:
                Instantiate(jumpObstacle, pos, Quaternion.identity, levelParent.transform);
                break;
            case InputType.Dash:
                pos.y = height;
                Instantiate(dashObstacle, pos, Quaternion.identity, levelParent.transform);
                break;
            case InputType.Slide:
                Instantiate(slideObstacle, pos, Quaternion.identity, levelParent.transform);
                break;
            case InputType.Attack:
                pos.y = height;
                Instantiate(damageableObstacle, pos, Quaternion.identity, levelParent.transform);
                break;
        }
    }

    public float GetDistanceByBeat(float beat)
    {
        // La distancia a la que estaria el player en determinado beat de la cancion
        return (beat / beatsPerSecond) * _player.Speed;
    }
}
