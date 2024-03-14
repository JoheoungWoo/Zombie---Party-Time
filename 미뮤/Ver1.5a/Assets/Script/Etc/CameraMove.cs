using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target; // 플레이어 위치
    public Transform map; // 맵 제한범위

    float height; // 카메라의 세로
    float width; // 카메라의 가로

    public float smoothTime = 0f; // 부드러운 이동을 위한 시간

    private Vector3 velocity = Vector3.zero; // SmoothDamp에 필요한 속도 벡터

    //처음에 맵 크기 불러오기
    private void Awake()
    {
        map = GameObject.Find("Map").transform;
        gameObject.transform.parent = gameObject.transform.parent.parent;   //플레이어 밖으로 위치시키기
    }

    void Start()
    {
        // 카메라의 범위설정
        height = Camera.main.orthographicSize; // 카메라의 크기
        width = height * Screen.width / Screen.height;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f);

        // 부드러운 이동을 위해 SmoothDamp 함수 사용
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        float lx = map.localScale.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + map.position.x, lx + map.position.x);

        float ly = map.localScale.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + map.position.y, ly + map.position.y);

        // 실제 카메라 움직임
        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
