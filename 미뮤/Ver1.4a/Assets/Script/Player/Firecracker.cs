using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firecracker : MonoBehaviour,IWeapon
{
    public Animator animator;
    public Animator effectAnimator;
    public bool isAttacking;

    public GameObject effect;
    public const int defaultLayer = 0;
    public const int attackLayer = 0;

    public float attackMaxDelay = 2.0f;    //공격 가능 시간
    public float attackDelay = 0.0f;       //현재 딜레이


    private void Awake()
    {
        effect = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        effectAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        attackDelay += Time.deltaTime;
    }

    public void Attack(string attackDirect)
    {
        if(attackDelay >= attackMaxDelay)
        {
            switch (attackDirect)
            {
                case "up":
                    animator.SetTrigger("attack");
                    effectAnimator.SetTrigger("attack");
                    transform.localPosition = new Vector3(0f, 0.6f, 0f);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    break;
                case "down":
                    animator.SetTrigger("attack");
                    effectAnimator.SetTrigger("attack");
                    transform.localPosition = new Vector3(0f, -0.6f, 0f);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    break;
                case "left":
                    animator.SetTrigger("attack");
                    effectAnimator.SetTrigger("attack");
                    transform.localPosition = new Vector3(-0.75f, 0.2f, 0f);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case "right":
                    animator.SetTrigger("attack");
                    effectAnimator.SetTrigger("attack");
                    transform.localPosition = new Vector3(0.75f, 0.2f, 0f);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    break;
                default:
                    break;
            }

            attackDelay = 0.0f;
        }
    }
}
