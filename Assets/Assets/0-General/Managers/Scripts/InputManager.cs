using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum Input
    {
        Keyboard,
        Touch
    }
    public List<UIMenuScrollController> KeyboardObjects; 
    public List<UIStaticMenuContainer> KeyboardStaticButtonObjects; 
    public List<UIMenuSwipeDetection> TouchObjects;
    public Input inputType = Input.Keyboard;
    public bool GlobalInput = true; 
    private static InputManager _instance;
    public static InputManager Instance 
    {
        get
        {
            if (_instance) return _instance;
            _instance = FindObjectOfType<InputManager>();
            DontDestroyOnLoad(_instance.gameObject);
            return _instance;
        }
    }
    
    private void Awake()
    {
        if(_instance && _instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        if(inputType == Input.Touch)
        {
            foreach(UIMenuScrollController controller in KeyboardObjects){
                controller.enabled = false; 
            }
            foreach(UIStaticMenuContainer controller in KeyboardStaticButtonObjects){
                controller.enabled = false; 
            }
        }
        else if(inputType == Input.Keyboard)
        {
            foreach(UIMenuSwipeDetection controller in TouchObjects){
                controller.enabled = false; 
            }
        }
    }
}