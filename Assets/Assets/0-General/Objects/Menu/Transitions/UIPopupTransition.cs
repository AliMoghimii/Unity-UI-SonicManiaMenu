using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupTransition : MonoBehaviour
{
    public KeyCode Action = KeyCode.Escape;
    public GameObject CurrentMenu;
    public GameObject TargetMenu;
    public bool allowInput = true;

    void Update()
    {
        if(allowInput)
        {
            if(Input.GetKeyDown(Action))
            {
                CurrentMenu.GetComponent<UIMenuScrollController>().allowInput = false;
                //int index = CurrentMenu.GetComponent<MenuScroll>().currentButton;
                //CurrentMenu.GetComponent<MenuScroll>().Button[index].GetComponent<ButtonAnim>().allowInput = false;

                SoundManager.Instance.PlayButtonAccept();
                TargetMenu.SetActive(true);
            }
        }
    }
}
