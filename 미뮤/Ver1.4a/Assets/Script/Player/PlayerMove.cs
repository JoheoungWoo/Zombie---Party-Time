using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector3 moveDirection;
    public bool isMove;
    public const float moveTime = 0.12f;
    public const float goalPercent = 0.9f;

    private Player player;

    /// <summary>
    /// Playerµî·Ï
    /// </summary>
    public void Init(Player player) {
        this.player = player;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (moveDirection != Vector3.zero && !isMove)
            {
                Vector3 end = transform.position + moveDirection;
                yield return StartCoroutine(GridSmoothMovement(end));
            }
            yield return null;
        }
    }

    private IEnumerator GridSmoothMovement(Vector3 end)
    {
        Vector3 start = transform.position;

        float current = 0;
        float percent = 0;

        isMove = true;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.position = Vector3.Lerp(start, end, percent);

            if(percent >= goalPercent)
            {
                isMove = false;
            }
            yield return null;
        }
        moveDirection = new Vector3(0,0,0);
        isMove = false;
    }

}
