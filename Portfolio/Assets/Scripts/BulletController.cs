using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // ** target의 트랜스폼
    private Transform target;

    // ** 총알의 속도
    public float speed = 70.0f;
    // ** 충돌 이펙트
    public GameObject impactEffect;
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        // ** target이 존재하지 않을 경우
        if(target == null)
        {
            // ** 총알 파괴
            Destroy(gameObject);
            return;
        }

        // ** 총알이 target을 향해 날아갈 방향 벡터
        Vector3 dir = target.position - transform.position;
        // ** 현재 프레임에서 총알이 지나온 거리
        float distanceThisFrame = speed * Time.deltaTime;

        // ** 현재 프레임에서 총알이 지나온 거리가 발사될 때의 총알과 target 사이의 거리와 같거나 길 경우
        if (dir.magnitude <= distanceThisFrame)
        {
            // ** 총알이 target에 충돌
            HitTarget();
            return;
        }

        // ** 총알이 월드 좌표 내에서 (정규화된 방향 벡터 * 현재 프레임에서 총알이 지나온 거리)를 향해 이동
        // ** target에 얼마나 가까이 있든, 속도가 변하게 하지 않기 위함
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    // ** 충돌 시
    void HitTarget()
    {
        // ** 충돌 효과 복사
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2.0f);

        Destroy(target.gameObject);
        // ** 총알 파괴
        Destroy(gameObject);
    }
}
