using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;
    Vector3 offset;
    float lenght;

    private void Start()
    {
        Vector3 start = new Vector3(0, 3.46f, -3.13f);
        transform.position = start;
        if (target == null)
        {
            PlayerBase player = FindObjectOfType<PlayerBase>();
            target = player.transform.GetChild(2);
        }

        offset = transform.position - target.position;
        lenght = offset.magnitude;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Slerp(     // ȣ�� �׸��� �����̰� �����
            transform.position,                 // ���� ��ġ���� 
            target.position + Quaternion.LookRotation(target.forward) * offset, // offset��ŭ ������ ��ġ��(ȸ�� �����)
            1.0f);       // Time.fixedDeltaTime * speed��ŭ ����

        /*transform.position = transform.position - target.position;*/

        transform.LookAt(target);               // ī�޶� ��ǥ���� �ٶ󺸱�

        // target���� ī�޶�� ������ ����
        Ray ray = new Ray(target.position, transform.position - target.position);
        if (Physics.Raycast(ray, out RaycastHit hit, lenght))  // �浹 üũ
        {
            transform.position = hit.point;                     // �浹�ϸ� �浹�� ��ġ�� ī�޶� �ű�
        }
    }
}
