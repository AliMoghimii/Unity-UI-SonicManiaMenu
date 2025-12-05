using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticButtonAdjacentHolder : MonoBehaviour
{   
    //use only if the button is meant to be in a static menu container (not a scrollable or swipable menu)
    public KeyCode UpAction = KeyCode.UpArrow;
    public GameObject UpButton;
    public KeyCode DownAction = KeyCode.DownArrow;
    public GameObject DownButton;
    public KeyCode RightAction = KeyCode.RightArrow;
    public GameObject RightButton;
    public KeyCode LeftAction = KeyCode.LeftArrow;
    public GameObject LeftButton;
}
