using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ** Enemy�� �ӵ�
    public float speed = 10.0f;

    // ** target�� Ʈ������
    private Transform target;
    // ** waypoint�� index�� ����ų ���� wavepointIndex
    private int wavepointIndex = 0;

    private void Start()
    {
        // ** ù ��° Waypoint�� target���� ����
        target = Waypoints.points[0];
    }

    private void Update()
    {
        // ** Waypoint�� �����ϱ� ���� ���� ���� ����
        Vector3 dir = target.position - transform.position;
        // ** Enemy�� Transform�� Space.World ��ǥ ����, ����ȭ�� ���� * �ӵ� * �ð��� ���� �̵�
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // ** Enemy�� Waypoint ������ �Ÿ��� 0.4f ������ �� �Լ� ȣ��
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    // ** ���� Waypoint�� ã�� �Լ�
    void GetNextWaypoint()
    {
        // ** ���� Waypoint�� �������� ��, Enemy�� �ı�
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        // ** wavepointIndex �� ����
        wavepointIndex++;
        // ** ���� Waypoint�� target���� ����
        target = Waypoints.points[wavepointIndex];
    }
}
