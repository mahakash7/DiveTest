using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private LineRenderer ln;
    
    [SerializeField] private Button playButton;
    [SerializeField] private Joystick joystick;

    [SerializeField] private FixedButton diveInButton;
    [SerializeField] private FixedButton diveUpButton;
    [SerializeField] private Transform lengthStartPosition;
    [SerializeField] private Transform lengthEndPosition;
    [SerializeField] private Transform floatPoint;

    protected bool playerFloating;
    protected bool updown;
    [SerializeField]  protected int speedCofactor = 10;
    [SerializeField]  protected int minimumYTrashHold = 25;
    [SerializeField]  protected GameObject suctionEffect;

    int speed;
    int maxLength;
    int distance;
    int maxOxygen;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        ResetData();
    }

    void ResetData()
    {
        suctionEffect.SetActive(false);
        animator = GetComponent<Animator>();
        ln = GetComponent<LineRenderer>();
        SetLengthRope();

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(OnPlayButtonClick);

        updown = false;
        playerFloating = false;
        RenderSettings.fog = playerFloating;
        timer = GameManager.Instance.Data.GetPlayerdata().oxygen;
    }

    void SetLengthRope()
    {
        ln.SetPosition(0, lengthStartPosition.transform.position);
        ln.SetPosition(1, lengthEndPosition.transform.position);
    }

    private void Update()
    {
        SetLengthRope();
        if (GameManager.Instance.gameMode == 1)
        {
            if (playerFloating)
            {
                UpdateTimer();
            }
                                 
            distance = (int) (lengthStartPosition.transform.position.y -  lengthEndPosition.transform.position.y);
            GameManager.Instance.GameplayManager.UpdateLength(distance);
            MovePlayer();
            

        }
        else
        {
            ResetData();
        }
        
    }

    void MovePlayer()
    {
        if (playerFloating == true && updown == false)
        {
            animator.SetBool("float", playerFloating);
            RenderSettings.fog = playerFloating;
            suctionEffect.SetActive(!updown);

            if (joystick.Horizontal > 0)
            {
                transform.eulerAngles = new Vector3(90, -90, 0);
            }
            else if (joystick.Horizontal < 0)
            {
                transform.eulerAngles = new Vector3(90, 90, 0);
            }
            else if (joystick.Vertical > 0)
            {
                transform.eulerAngles = new Vector3(90, 180, 0);
            }
            else if (joystick.Vertical < 0)
            {
                transform.eulerAngles = new Vector3(90, 0, 0);
            }

            var step = speed * Time.deltaTime * -1; 
            var target = transform.position + new Vector3(joystick.Horizontal * speed, 0, joystick.Vertical * speed);
            transform.position = Vector3.MoveTowards(transform.position, target, step);
           
        }

        if (diveInButton.Pressed)
        {
            updown = true;
            if (distance <= maxLength)
            {
                var step = speed * Time.deltaTime; // calculate distance to move
                var target = transform.position + new Vector3(0, -1, 0);
                transform.position = Vector3.MoveTowards(transform.position, target, step);
                transform.eulerAngles = new Vector3(180, 0, 0);
            }

        }
        else
        {
            updown = false;
        }

        if (diveUpButton.Pressed)
        {
            updown = true;
            if (distance >= minimumYTrashHold)
            {
                var step = speed * Time.deltaTime; // calculate distance to move
                var target = transform.position + new Vector3(0, 1, 0);
                transform.position = Vector3.MoveTowards(transform.position, target, step);
                transform.eulerAngles = new Vector3(0, 0, 0);

            }
        }
        else
        {
            updown = false;
        }
        
    }
    void UpdateTimer()
    {
        timer -= Time.deltaTime;
        GameManager.Instance.GameplayManager.UpdateOxygen((int)timer);
    }
    
    void OnPlayButtonClick()
    {
        GameManager.Instance.GameplayManager.OnGameStart();
        Player player = GameManager.Instance.Data.GetPlayerdata();
        speed = player.speed;
        maxLength = player.length;
        maxOxygen = player.oxygen;
       
        animator.SetTrigger("dive");
        Invoke("AfterDive", 0.965f);

    }

    void AfterDive()
    {
        StartCoroutine("MoveToFloatLevel");
    }

    IEnumerator MoveToFloatLevel()
    {
        var target = this.transform.position - new Vector3(0, 20, 0);
        float elapsedTime = 0;
        Vector3 startingPos = this.transform.position;
        while (elapsedTime < 2)
        {
            this.transform.position = Vector3.Lerp(startingPos, target, (elapsedTime / 2));
            elapsedTime += Time.deltaTime;
            transform.eulerAngles = new Vector3(180, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        
        Invoke("TriggerFloat", .5f);
    }
    void TriggerFloat()
    {
        playerFloating = true;
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
