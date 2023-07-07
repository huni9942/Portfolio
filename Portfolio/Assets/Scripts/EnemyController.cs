using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ** Enemy의 속도
    public float speed = 10.0f;

    // ** target의 트랜스폼
    private Transform target;
    // ** waypoint의 index를 가리킬 변수 wavepointIndex
    private int wavepointIndex = 0;

    private void Start()
    {
        // ** 첫 번째 Waypoint를 target으로 지정
        target = Waypoints.points[0];
    }

    private void Update()
    {
        // ** Waypoint에 도달하기 위한 방향 벡터 설정
        Vector3 dir = target.position - transform.position;
        // ** Enemy의 Transform이 Space.World 좌표 기준, 정규화된 방향 * 속도 * 시간에 따라 이동
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // ** Enemy와 Waypoint 사이의 거리가 0.4f 이하일 때 함수 호출
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    // ** 다음 Waypoint를 찾는 함수
    void GetNextWaypoint()
    {
        // ** 최종 Waypoint에 도달했을 때, Enemy를 파괴
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        // ** wavepointIndex 값 증가
        wavepointIndex++;
        // ** 다음 Waypoint를 target으로 지정
        target = Waypoints.points[wavepointIndex];
    }
}
