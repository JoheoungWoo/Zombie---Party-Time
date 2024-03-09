using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Player player;
    public Vector3 _delta;
    public CameraMode _mode; // CameraMode enum을 사용

    public float smoothSpeed = 5f;

    public enum CameraMode
    {
        QuaterView,
        // 다른 CameraMode 값들을 추가할 수 있습니다.
    }

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        _mode = CameraMode.QuaterView; // 초기 CameraMode 설정
    }

    void LateUpdate()
    {
        _delta = transform.position - player.transform.position;
        if (_mode == CameraMode.QuaterView)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - transform.position).magnitude * 0.8f;

                // 벽과 충돌한 경우에만 위치를 업데이트
                transform.position = Vector3.Lerp(transform.position, transform.position + _delta.normalized * dist, Time.deltaTime * smoothSpeed);
            }
            else
            {
                // 벽과 충돌하지 않은 경우에는 위치와 방향을 부드럽게 업데이트
                transform.position = Vector3.Lerp(transform.position, transform.position + _delta, Time.deltaTime * smoothSpeed);
                transform.LookAt(transform.position + _delta);
            }
        }
    }
}
