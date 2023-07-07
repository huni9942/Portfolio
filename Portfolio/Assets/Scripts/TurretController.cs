using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    // ** target의 트랜스폼
    private Transform target;
    
    [Header("Attributes")]
    // ** turret의 사거리
    public float range = 15.0f;
    // ** 공격 속도
    public float fireRate = 1.0f;
    // ** 공격 카운트다운
    private float fireCountdown = 0.0f;

    [Header("Unity Setup Fields")]

    // ** Enemy의 태그 지정
    public string enemyTag = "Enemy";

    // ** 회전할 부분의 트랜스폼
    public Transform partToRotate;
    // ** 회전 속도
    public float turnSpeed = 10.0f;

    // ** 총알 프리펩
    public GameObject bulletPrefab;
    // ** 총알 발사 위치
    public Transform firePoint;

    void Start()
    {
        // ** 'UpdateTarget' 메소드를 0.0초 후, 0.5초마다 실행
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    // ** target 변경 함수
    void UpdateTarget()
    {
        // ** enemyTag를 가진 게임 오브젝트를 찾아, enemies 배열에 저장
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // ** Enemy와의 최단거리, Enemy가 존재하지 않을 때의 거리는 무한대
        float shortestDistance = Mathf.Infinity;
        // ** 가장 가까운 Enemy
        GameObject nearestEnemy = null;
        // ** enemies에 속한 enemy만큼 반복
        foreach (GameObject enemy in enemies)
        {
            // ** 현재 enemy와 Turret 사이의 거리 변수
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            // ** 현재 enemy와 Turret 사이의 거리가 기존 enemy와의 최단거리보다 짧을 때
            if (distanceToEnemy < shortestDistance)
            {
                // ** 최단거리 = 현재 enemy와 Turret 사이의 거리
                shortestDistance = distanceToEnemy;
                // ** 가장 가까운 Enemy = 현재 enemy
                nearestEnemy = enemy;
            }
        }

        // ** 가장 가까운 Enemy가 null이 아니며, 사거리 이내에 존재할 경우
        if (nearestEnemy != null && shortestDistance <= range)
        {
            // ** target = 가장 가까운 Enemy
            target = nearestEnemy.transform;
        }
    }

    void Update()
    {
        // ** target이 존재하지 않을 경우 리턴
        if (target == null)
            return;

        // ** Turret이 볼 방향
        Vector3 dir = target.position - transform.position;
        // ** Turret이 볼 방향으로 회전
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        // ** 회전을 오일러각으로 변환, 선형보간하여 부드럽게 회전
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        // ** Turret의 회전할 부분을 y축으로만 회전
        partToRotate.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);

        // ** 카운트다운이 0이하가 되었을 경우
        if (fireCountdown <= 0)
        {
            // ** 공격
            Shoot();
            // ** 공격 속도에 따른 카운트다운
            fireCountdown = 1.0f / fireRate;
        }

        // ** 카운트다운은 시간에 따라 감소
        fireCountdown -= Time.deltaTime;
    }

    // ** 공격
    void Shoot()
    {
        // ** 총알 프리펩을 발사 위치에 복사 생성
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // ** 복사 생성한 총알의 BulletController 컴포넌트
        BulletController bullet = bulletGO.GetComponent<BulletController>();

        // ** BulletController 컴포넌트가 존재할 때
        if (bullet != null)
            // ** target 추적
            bullet.Seek(target);
    }

    // ** Turret의 사거리를 표시하는 Gizmos
    private void OnDrawGizmosSelected()
    {
        // ** Gizmos의 색깔을 붉은색으로 변경
        Gizmos.color = Color.red;
        // ** Gizmos를 Turret의 위치에 반지름이 사거리만큼인 WireSphere로 표시
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
