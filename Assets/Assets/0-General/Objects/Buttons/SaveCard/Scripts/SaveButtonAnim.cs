using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButtonAnim : ParentButton
{
    public enum Direction
    {
        Vertical,
        Horizontal
    }

    [Space(10)]
    [Header("Character Select Options :")]
    public int currentCharacter = -1;
    public List<GameObject> Character;

    [Space(10)]
    [Header("Animation Controllers :")]

    public float coolDownUntilNextSwitch = 0.25f;
    public bool canSwitchTarget = true;
    public Direction ScrollDirection = Direction.Vertical;

    [Space(10)]
    public bool active = false;
    public bool select = false;
    private Transform t;
    private bool swapSoundPlayed;

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
        GetComponent<Image>().color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
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
        Character[currentCharacter].SetActive(false);
        Character[0].SetActive(true);
    }

    void Start()
    {
        t = GetComponent<Transform>();
        Character[0].SetActive(true);
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
                select = true;
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
        if(direction > 0 && currentCharacter != Character.Count-1)
        {
            Character[currentCharacter].SetActive(false);
            currentCharacter++;
            Character[currentCharacter].SetActive(true);
            SoundManager.Instance.PlayButtonAccept();
        }
        if(direction < 0 && currentCharacter != 0)
        {
            Character[currentCharacter].SetActive(false);
            currentCharacter--;
            Character[currentCharacter].SetActive(true);
            SoundManager.Instance.PlayButtonAccept();
        }
    }
}

//event / return update va functions haye joda
