using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCancelPopup : MonoBehaviour
{
    public KeyCode Action = KeyCode.Space;
    public GameObject currentMenu;
    [SerializeField] private ButtonAnim buttonIsActive;
    public bool allowInput = true;

    public void Transit()
    {
        SoundManager.Instance.PlayButtonAccept();
        currentMenu.SetActive(false);
    }

    void Update()
    {
        if(buttonIsActive.active)
        {
            if(allowInput)
            {
                if(Input.GetKeyDown(Action) && buttonIsActive.active)
                {
                    Transit();
                }
            }
        }
    }
}
