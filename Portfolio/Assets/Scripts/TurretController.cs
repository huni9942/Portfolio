using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    // ** target�� Ʈ������
    private Transform target;
    
    [Header("Attributes")]
    // ** turret�� ��Ÿ�
    public float range = 15.0f;
    // ** ���� �ӵ�
    public float fireRate = 1.0f;
    // ** ���� ī��Ʈ�ٿ�
    private float fireCountdown = 0.0f;

    [Header("Unity Setup Fields")]

    // ** Enemy�� �±� ����
    public string enemyTag = "Enemy";

    // ** ȸ���� �κ��� Ʈ������
    public Transform partToRotate;
    // ** ȸ�� �ӵ�
    public float turnSpeed = 10.0f;

    // ** �Ѿ� ������
    public GameObject bulletPrefab;
    // ** �Ѿ� �߻� ��ġ
    public Transform firePoint;

    void Start()
    {
        // ** 'UpdateTarget' �޼ҵ带 0.0�� ��, 0.5�ʸ��� ����
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    // ** target ���� �Լ�
    void UpdateTarget()
    {
        // ** enemyTag�� ���� ���� ������Ʈ�� ã��, enemies �迭�� ����
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // ** Enemy���� �ִܰŸ�, Enemy�� �������� ���� ���� �Ÿ��� ���Ѵ�
        float shortestDistance = Mathf.Infinity;
        // ** ���� ����� Enemy
        GameObject nearestEnemy = null;
        // ** enemies�� ���� enemy��ŭ �ݺ�
        foreach (GameObject enemy in enemies)
        {
            // ** ���� enemy�� Turret ������ �Ÿ� ����
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            // ** ���� enemy�� Turret ������ �Ÿ��� ���� enemy���� �ִܰŸ����� ª�� ��
            if (distanceToEnemy < shortestDistance)
            {
                // ** �ִܰŸ� = ���� enemy�� Turret ������ �Ÿ�
                shortestDistance = distanceToEnemy;
                // ** ���� ����� Enemy = ���� enemy
                nearestEnemy = enemy;
            }
        }

        // ** ���� ����� Enemy�� null�� �ƴϸ�, ��Ÿ� �̳��� ������ ���
        if (nearestEnemy != null && shortestDistance <= range)
        {
            // ** target = ���� ����� Enemy
            target = nearestEnemy.transform;
        }
    }

    void Update()
    {
        // ** target�� �������� ���� ��� ����
        if (target == null)
            return;

        // ** Turret�� �� ����
        Vector3 dir = target.position - transform.position;
        // ** Turret�� �� �������� ȸ��
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        // ** ȸ���� ���Ϸ������� ��ȯ, ���������Ͽ� �ε巴�� ȸ��
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        // ** Turret�� ȸ���� �κ��� y�����θ� ȸ��
        partToRotate.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);

        // ** ī��Ʈ�ٿ��� 0���ϰ� �Ǿ��� ���
        if (fireCountdown <= 0)
        {
            // ** ����
            Shoot();
            // ** ���� �ӵ��� ���� ī��Ʈ�ٿ�
            fireCountdown = 1.0f / fireRate;
        }

        // ** ī��Ʈ�ٿ��� �ð��� ���� ����
        fireCountdown -= Time.deltaTime;
    }

    // ** ����
    void Shoot()
    {
        // ** �Ѿ� �������� �߻� ��ġ�� ���� ����
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // ** ���� ������ �Ѿ��� BulletController ������Ʈ
        BulletController bullet = bulletGO.GetComponent<BulletController>();

        // ** BulletController ������Ʈ�� ������ ��
        if (bullet != null)
            // ** target ����
            bullet.Seek(target);
    }

    // ** Turret�� ��Ÿ��� ǥ���ϴ� Gizmos
    private void OnDrawGizmosSelected()
    {
        // ** Gizmos�� ������ ���������� ����
        Gizmos.color = Color.red;
        // ** Gizmos�� Turret�� ��ġ�� �������� ��Ÿ���ŭ�� WireSphere�� ǥ��
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
