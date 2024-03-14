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

    private float attackMaxDelay = 0.6f;    //공격 딜레이
    private float attackDelay = 0.0f;       //현재 딜레이

    private SpriteRenderer weaponSpriteRenderer;
    private SpriteRenderer effectSpriteRenderer;
    private GameObject hitDecision;

    private float attackAnimaionSpeed = 1f;   //공격 애니메이션 배속 설정
    private float attakTimePercent = 0.3f;  //애니메이션 재생시간 기준으로 33%만큼

    private float hitDecisionTime;      //공격 애니메이션 시간


    private void Awake()
    {
        animator = GetComponent<Animator>();
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        effect = transform.GetChild(0).gameObject;
        hitDecision = transform.GetChild(1).gameObject;
        effectAnimator = effect.GetComponent<Animator>();
        effectSpriteRenderer = effect.GetComponent<SpriteRenderer>();

        //공격속도 설정
        animator.SetFloat("attackSpeed",attackAnimaionSpeed);
        effectAnimator.SetFloat("attackSpeed", attackAnimaionSpeed);

        //애니메이션 시간 가져오기 (공격 판정 시간임)
        hitDecisionTime = GetAnimationClipFromController(animator.runtimeAnimatorController, "Attack").length / attackAnimaionSpeed * attakTimePercent;
    }

    private void Update()
    {
        attackDelay += Time.deltaTime;
    }


    AnimationClip GetAnimationClipFromController(RuntimeAnimatorController controller, string clipName)
    {
        // AnimatorController를 얻어온 후, 애니메이션 클립 가져오기
        AnimationClip[] clips = controller.animationClips;

        for (int clipIndex = 0; clipIndex < clips.Length; clipIndex++)
        {
            if (clips[clipIndex].name.Equals(clipName))
            {
                Debug.Log(clips[clipIndex].name);
                return clips[clipIndex];
            }
        }

        // 찾지 못한 경우 예외 처리 또는 기본값 반환
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
