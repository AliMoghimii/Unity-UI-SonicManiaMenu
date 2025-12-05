using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool rotate = true;
    public float rotationSpeed = 60f;

    void Update()
    {        
        if(rotate)
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
