using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExitTransition : MonoBehaviour
{
    public KeyCode Action = KeyCode.Space;
    public GameObject Canvas;
    [SerializeField] private ButtonAnim buttonIsActive;
    public bool allowInput = true;
    public void Transit()
    {
        SoundManager.Instance.PlayButtonAccept();
        Application.Quit();
        Canvas.SetActive(false);
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
