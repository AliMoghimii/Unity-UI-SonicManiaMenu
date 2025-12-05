using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundManager : MonoBehaviour
{
    [Space(10)]
    [Header("Camera Background Properties :")]
    public Camera cameraObject;
    public Color CamColor;
    void OnEnable()
    {
        cameraObject.backgroundColor = CamColor;
    }
}
