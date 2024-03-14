using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target; // �÷��̾� ��ġ
    public Transform map; // �� ���ѹ���

    float height; // ī�޶��� ����
    float width; // ī�޶��� ����

    public float smoothTime = 0f; // �ε巯�� �̵��� ���� �ð�

    private Vector3 velocity = Vector3.zero; // SmoothDamp�� �ʿ��� �ӵ� ����

    //ó���� �� ũ�� �ҷ�����
    private void Awake()
    {
        map = GameObject.Find("Map").transform;
        gameObject.transform.parent = gameObject.transform.parent.parent;   //�÷��̾� ������ ��ġ��Ű��
    }

    void Start()
    {
        // ī�޶��� ��������
        height = Camera.main.orthographicSize; // ī�޶��� ũ��
        width = height * Screen.width / Screen.height;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f);

        // �ε巯�� �̵��� ���� SmoothDamp �Լ� ���
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        float lx = map.localScale.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + map.position.x, lx + map.position.x);

        float ly = map.localScale.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + map.position.y, ly + map.position.y);

        // ���� ī�޶� ������
        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
