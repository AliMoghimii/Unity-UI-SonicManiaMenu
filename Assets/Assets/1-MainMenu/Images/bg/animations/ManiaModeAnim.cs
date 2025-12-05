using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiaModeAnim : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    [SerializeField] private float len;
    [SerializeField] private float dest;
    public float speed = 5;

    void Start()
    {
        if(Screen.width != 1920)
        {
            float scaleRatio = 1920 / speed;
            speed = Screen.width / scaleRatio‬;

            scaleRatio = 1920 / GetComponent<RectTransform>().rect.width;;
            len = Screen.width / scaleRatio‬;    
        }
        else
        {
            len = GetComponent<RectTransform>().rect.width;
        }

        startPosX = GetComponent<RectTransform>().position.x;
        startPosY = GetComponent<RectTransform>().position.y;
    }
    void FixedUpdate()
    {
        dest += speed;
        transform.position += new Vector3(speed,0,0);
        if(dest > len)
        {
            GetComponent<RectTransform>().position = new Vector3(startPosX,startPosY,0);
            dest = 0;
        }
    }
}
