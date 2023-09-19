using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private Sprite damagedSprite;
    private bool isDamaged = false;
    public void TakeDamage()
    {
        if (isDamaged) return;
        isDamaged = true;
        GetComponent<SpriteRenderer>().sprite = damagedSprite;
        Debug.Log("hit");
    }
}
