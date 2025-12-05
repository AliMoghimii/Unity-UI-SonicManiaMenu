using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManiaModeAnim : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private float len;
    private float dest;
    public float speed = 5;

    void Start()
    {
        speed = speed * 0.05f;
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        len = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void FixedUpdate()
    {
        dest += speed;
        transform.position += new Vector3(speed,0,0);
        if(dest > len)
        {
            GetComponent<Transform>().position = new Vector3(startPosX,startPosY,1);
            dest = 0;
        }
    }
}
