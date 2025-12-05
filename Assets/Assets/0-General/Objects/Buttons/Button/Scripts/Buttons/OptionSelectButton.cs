using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSelectButton : ParentButton
{
    public enum Direction
    {
        Vertical,
        Horizontal
    }

    [Space(10)]
    [Header("Feature Select Options :")]
    public int currentCharacter = -1;
    public List<GameObject> Option;

    private Image _rightArrow;
    private Image _leftArrow;

    [Space(10)]
    [Header("Animation Controllers :")]

    public float coolDownUntilNextSwitch = 0.25f;
    public bool canSwitchTarget = true;
    public Direction ScrollDirection = Direction.Vertical;

    [Space(10)]
    public bool active = false;
    private Transform t;
    private bool swapSoundPlayed;
    void Start()
    {
        _rightArrow = transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>();
        _leftArrow = transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<Image>();
        
        Option[0].SetActive(true);
    }

    public override void Active()
    {
        ActiveSaveButton();
    }

    public override void Deactive()
    {
        DeactiveSaveButton();
    }

    public void ActiveSaveButton()
    {
        _rightArrow.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
        _leftArrow.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
        
        active = true;

        if(!swapSoundPlayed)
        {
            SoundManager.Instance.PlayButtonSwap();
            swapSoundPlayed = true;
        }
    }

    public void DeactiveSaveButton()
    {
        GetComponent<Image>().color = Color.black;
        active = false;
        swapSoundPlayed = false;
        Option[currentCharacter].SetActive(false);
        Option[0].SetActive(true);
    }

    void Update()
    {        
        if(active)
        {
            float direction = Input.GetAxis(ScrollDirection.ToString());

            if(direction != 0)
            {
                StartCoroutine(SwitchTargetRoutine(coolDownUntilNextSwitch,direction));
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SoundManager.Instance.PlayButtonAccept();
            }
        }
    }

    IEnumerator SwitchTargetRoutine(float duration,double direction)
    {
        if(canSwitchTarget)
        {
            canSwitchTarget = false;
            SwitchTarget(direction);
            yield return new WaitForSeconds( duration );
            canSwitchTarget = true;
        }
    }

    public void SwitchTarget(double direction)
    {
        if(direction > 0 && currentCharacter != Option.Count-1)
        {
            Option[currentCharacter].SetActive(false);
            currentCharacter++;
            Option[currentCharacter].SetActive(true);
            SoundManager.Instance.PlayButtonAccept();
        }
        if(direction < 0 && currentCharacter != 0)
        {
            Option[currentCharacter].SetActive(false);
            currentCharacter--;
            Option[currentCharacter].SetActive(true);
            SoundManager.Instance.PlayButtonAccept();
        }
    }
}

//event / return update va functions haye joda
