using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Player player;
    public Vector3 _delta;
    public CameraMode _mode; // CameraMode enum�� ���

    public float smoothSpeed = 5f;

    public enum CameraMode
    {
        QuaterView,
        // �ٸ� CameraMode ������ �߰��� �� �ֽ��ϴ�.
    }

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        _mode = CameraMode.QuaterView; // �ʱ� CameraMode ����
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

                // ���� �浹�� ��쿡�� ��ġ�� ������Ʈ
                transform.position = Vector3.Lerp(transform.position, transform.position + _delta.normalized * dist, Time.deltaTime * smoothSpeed);
            }
            else
            {
                // ���� �浹���� ���� ��쿡�� ��ġ�� ������ �ε巴�� ������Ʈ
                transform.position = Vector3.Lerp(transform.position, transform.position + _delta, Time.deltaTime * smoothSpeed);
                transform.LookAt(transform.position + _delta);
            }
        }
    }
}
