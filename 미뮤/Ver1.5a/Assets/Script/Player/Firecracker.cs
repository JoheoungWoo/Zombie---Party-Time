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

    private float attackMaxDelay = 0.6f;    //���� ������
    private float attackDelay = 0.0f;       //���� ������

    private SpriteRenderer weaponSpriteRenderer;
    private SpriteRenderer effectSpriteRenderer;
    private GameObject hitDecision;

    private float attackAnimaionSpeed = 1f;   //���� �ִϸ��̼� ��� ����
    private float attakTimePercent = 0.3f;  //�ִϸ��̼� ����ð� �������� 33%��ŭ

    private float hitDecisionTime;      //���� �ִϸ��̼� �ð�


    private void Awake()
    {
        animator = GetComponent<Animator>();
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        effect = transform.GetChild(0).gameObject;
        hitDecision = transform.GetChild(1).gameObject;
        effectAnimator = effect.GetComponent<Animator>();
        effectSpriteRenderer = effect.GetComponent<SpriteRenderer>();

        //���ݼӵ� ����
        animator.SetFloat("attackSpeed",attackAnimaionSpeed);
        effectAnimator.SetFloat("attackSpeed", attackAnimaionSpeed);

        //�ִϸ��̼� �ð� �������� (���� ���� �ð���)
        hitDecisionTime = GetAnimationClipFromController(animator.runtimeAnimatorController, "Attack").length / attackAnimaionSpeed * attakTimePercent;
    }

    private void Update()
    {
        attackDelay += Time.deltaTime;
    }


    AnimationClip GetAnimationClipFromController(RuntimeAnimatorController controller, string clipName)
    {
        // AnimatorController�� ���� ��, �ִϸ��̼� Ŭ�� ��������
        AnimationClip[] clips = controller.animationClips;

        for (int clipIndex = 0; clipIndex < clips.Length; clipIndex++)
        {
            if (clips[clipIndex].name.Equals(clipName))
            {
                Debug.Log(clips[clipIndex].name);
                return clips[clipIndex];
            }
        }

        // ã�� ���� ��� ���� ó�� �Ǵ� �⺻�� ��ȯ
        Debug.LogWarning("Clip not found: " + clipName);
        return null;
    }


    public void SetDirect(string attackDirect)
    {
        switch (attackDirect)
        {
            case "up":
                transform.localPosition = new Vector3(0f, 0.6f, 0f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                weaponSpriteRenderer.sortingOrder = 3;
                effectSpriteRenderer.sortingOrder = 3;
                break;
            case "down":
                transform.localPosition = new Vector3(0f, -0.6f, 0f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                weaponSpriteRenderer.sortingOrder = 5;
                effectSpriteRenderer.sortingOrder = 6;
                break;
            case "left":
                transform.localPosition = new Vector3(-0.6f, 0.2f, 0f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                weaponSpriteRenderer.sortingOrder = 5;
                effectSpriteRenderer.sortingOrder = 6;
                break;
            case "right":
                transform.localPosition = new Vector3(0.6f, 0.2f, 0f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                weaponSpriteRenderer.sortingOrder = 5;
                effectSpriteRenderer.sortingOrder = 6;
                break;
            default:
                break;
        }
    }

    public void Attack()
    {
        if(attackDelay >= attackMaxDelay)
        {
            animator.SetTrigger("attack");
            effectAnimator.SetTrigger("attack");
            attackDelay = 0.0f;
            hitDecision.SetActive(true);
            Invoke(nameof(OFFHitDecision), hitDecisionTime);
        }
    }

    public void OFFHitDecision()
    {
        hitDecision.SetActive(false);
    }
}
