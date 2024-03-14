using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Player : MonoBehaviour
{
    public int nowFrame = 300;

    public AnimationManager animationManager;
    private Animator animator;
    public int skinIndex;
    public int skinDebug;

    private const float playerDirectUp = 0.25f;
    private const float playerDirectDown = 0.5f;
    private const float playerDirectLeft = 0.75f;
    private const float playerDirectRight = 1f;

    private float animationDelay = 0.5f;

    private PlayerMove playerMove;
    
    private FixedJoystick fixedJoystick;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private IWeapon weapon;

    public Utility.CharList choiceChar;

    public TMP_Text nickName;
    public TMP_Text nameBoardnickName;

    public Image attackImg;

    public bool isTouchNPC;


    void Awake()
    {

        playerMove = gameObject.AddComponent<PlayerMove>();
        playerMove.Init(this);

        animator = GetComponent<Animator>();

        fixedJoystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();

        animationManager = GetComponent<AnimationManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        weapon = GetComponentInChildren<IWeapon>();

        attackImg = GameObject.Find("Attack_Button").GetComponentInChildren<Image>();
    }

    private void Start()
    {
        animator.SetFloat("MoveDirect", playerDirectDown);
        choiceChar = (Utility.CharList)(int)Utility.Prefs.Load<int>("useSkin");
        nameBoardnickName.text = Utility.Prefs.Load<string>("NickName").ToString();
        nickName.text = Utility.Prefs.Load<string>("NickName").ToString();
        InitSkin(choiceChar);

    }


    void InitSkin(Utility.CharList choiceChar)
    {
        animator.runtimeAnimatorController = animationManager.GetRuntimeAnimatorController((int)choiceChar);
    }

    public void ChangeOrderByLayer(int layerIndex)
    {
        spriteRenderer.sortingOrder = layerIndex;
    }

    public string GetDirection(float playerDirect)
    {
        var resultString = string.Empty;
        switch(playerDirect)
        {
            case playerDirectUp:
                resultString = "up";
                break;
            case playerDirectDown:
                resultString = "down";
                break;
            case playerDirectLeft:
                resultString = "left";
                break;
            case playerDirectRight:
                resultString = "right";
                break;
            default:
                break;
        }
        return resultString;
    }
    public string GetDirection(float inputX, float inputY)
    {
        if (Mathf.Abs(inputX) >= Mathf.Abs(inputY) + 0.1f)
        {
            if (inputX > 0)
            {
                return "right";
            }
            else
            {
                return "left";
            }
        }
        else
        {
            if (inputY < 0)
            {
                return "down";
            }
            else
            {
                return "up";
            }
        }
    }

    public bool CheckWall(Vector3 direct)
    {
        //Debug Raycast
        Debug.DrawRay(rigidbody2D.position, direct , new Color(1,0,0));

        RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position, direct,1,LayerMask.GetMask("Wall"));
        if(hit.collider != null)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void Move(float inputX, float inputY)
    {
        //Check Input
        if (inputX != 0 || inputY != 0)
        {
            if(playerMove.isMove)
            {
                return;
            }

            //Get Direct
            var directReuslt = GetDirection(inputX, inputY);
            var direct = new Vector3(0,0,0);
            switch (directReuslt)
            {
                case "right":
                    animator.SetFloat("MoveDirect", playerDirectRight);
                    animationDelay = playerDirectRight;
                    direct = new Vector3(1, 0, 0);
                    right.gameObject.SetActive(true);
                    left.gameObject.SetActive(false);
                    up.gameObject.SetActive(false);
                    down.gameObject.SetActive(false);
                    weapon.SetDirect("right");
                    break;
                case "left":
                    animator.SetFloat("MoveDirect", playerDirectLeft);
                    animationDelay = playerDirectLeft;
                    direct = new Vector3(-1, 0, 0);
                    right.gameObject.SetActive(false);
                    left.gameObject.SetActive(true);
                    up.gameObject.SetActive(false);
                    down.gameObject.SetActive(false);
                    weapon.SetDirect("left");
                    break;
                case "up":
                    animator.SetFloat("MoveDirect", playerDirectUp);
                    animationDelay = playerDirectUp;
                    direct = new Vector3(0, 1, 0);
                    right.gameObject.SetActive(false);
                    left.gameObject.SetActive(false);
                    up.gameObject.SetActive(true);
                    down.gameObject.SetActive(false);
                    weapon.SetDirect("up");
                    break;
                case "down":
                    animator.SetFloat("MoveDirect", playerDirectDown);
                    animationDelay = playerDirectDown;
                    direct = new Vector3(0, -1, 0);
                    right.gameObject.SetActive(false);
                    left.gameObject.SetActive(false);
                    up.gameObject.SetActive(false);
                    down.gameObject.SetActive(true);
                    weapon.SetDirect("down");
                    break;
                
            }

            //Check Wall
            if(CheckWall(direct))
            {
                animator.SetBool("IsMove", false);
                return;
            }

            //Move
            animator.SetFloat("InputX", direct.x);
            animator.SetFloat("InputY", direct.y);
            playerMove.moveDirection = direct;
            animator.SetBool("IsMove", true);
        }
        else if (inputX == 0 && inputY == 0)
        {
            animator.SetBool("IsMove", false);
            animator.SetFloat("InputX", 0);
            animator.SetFloat("InputY", 0);
        }
    }

    private void FixedUpdate()
    {
        Application.targetFrameRate = nowFrame;

        //Direct
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        
        if(inputX == 0 && inputY == 0)
        {
            inputX = fixedJoystick.Horizontal;
            inputY = fixedJoystick.Vertical;
        }

        if(playerMove.isMove == false)
        {
            right.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
            up.gameObject.SetActive(false);
            down.gameObject.SetActive(false);
        }

        Move(inputX, inputY);
    }

    public void Attack()
    {
        weapon.Attack();
    }

    public void SkinShiftDebug() // 스킨 교체 디버그 버튼
    {
        if (skinDebug < System.Enum.GetValues(typeof(Utility.CharList)).Length)
        {
            skinIndex++;
            skinDebug++;
            choiceChar = (Utility.CharList)skinIndex;
        }
        else
        {
            skinDebug = 0;
            skinIndex = 0;
        }
        InitSkin(choiceChar);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("NPC"))
        {
            attackImg.color = new Color(0,0,0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            attackImg.color = new Color(1, 1, 1);
        }
    }
}
