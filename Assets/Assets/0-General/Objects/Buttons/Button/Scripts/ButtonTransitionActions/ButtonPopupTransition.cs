using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPopupTransition : MonoBehaviour
{
    public KeyCode Action = KeyCode.Space;
    public UIMenuScrollController CurrentMenu;
    public GameObject TargetMenu;
    [SerializeField] private ButtonAnim buttonIsActive;
    public bool allowInput = true;

    public void Transit()
    {
        CurrentMenu.allowInput = false;
        SoundManager.Instance.PlayButtonAccept();
        TargetMenu.SetActive(true);
    }

    void Update()
    {
        if(buttonIsActive.active)
        {
            if(allowInput)
            {
                if(Input.GetKeyDown(Action))
                {
                    Transit();
                }
            }
        }
    }
}
