using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObjectManager : MonoBehaviour
{
    public Transform ButtonContainer;
    public bool ChangeToDefault = true;
    private Transform DefaultButtonContainer;
    void Start()
    {
        DefaultButtonContainer = ButtonContainer;
    }
    public void DefaultSettings()
    {
        if(ChangeToDefault)
            ButtonContainer = DefaultButtonContainer;
    }
}
