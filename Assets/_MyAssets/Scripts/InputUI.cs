using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUI : MonoBehaviour
{
    public float time;
    RectTransform rectTransform;
    bool moveRight = true;
    float posX;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
        posX = rectTransform.anchoredPosition.x;
        
    }

    public void Init(float time)
    {
        this.time = time;
        iTween.MoveTo(gameObject, iTween.Hash("position", Vector3.zero, "time", time, "islocal", true, "easetype", "linear"));
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(2f, 2f), "time", time, "easetype", "linear"));
    }

    // Update is called once per frame
    void Update()
    {
        if(rectTransform.anchoredPosition.x >= -2 && rectTransform.anchoredPosition.x <= 2)
        {
            Destroy(gameObject);
        }
    }
}
