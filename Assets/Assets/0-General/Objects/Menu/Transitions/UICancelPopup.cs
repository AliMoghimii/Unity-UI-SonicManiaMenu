using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICancelPopup : MonoBehaviour
{
    public KeyCode Action = KeyCode.Escape;
    public UIMenuScrollController TargetMenu;
    public GameObject currentMenu;
    public bool allowInput = true;
    void Update()
    {
        if(allowInput)
        {
            if(Input.GetKeyDown(Action))
            {
                TargetMenu.allowInput = true;
                SoundManager.Instance.PlayButtonAccept();
                currentMenu.SetActive(false);
            }
        }
    }
}
