using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputInTime
{
    [SerializeField] public InputType type;
    [SerializeField] public float beatPlayed;
    [SerializeField] public float height;
    [SerializeField] public float length;

    public InputInTime(InputType type, float beatPlayed, float height, float length)
    {
        this.type = type;
        this.beatPlayed = beatPlayed;
        this.length = length;
    }
}
