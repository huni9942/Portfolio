using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    // ** enemyPrefab�� Ʈ������
    public Transform enemyPrefab;
    // ** spawnPoint�� Ʈ������
    public Transform spawnPoint;

    // ** Wave ���� �ð� ����
    public float timeBetweenWaves = 5.0f;
    // ** ���� Wave���� �ɸ��� �ð�
    private float countdown = 2.0f;

    // ** countdown�� ǥ���ϴ� �ؽ�Ʈ
    public Text waveCountdownText;

    // ** ���� Wave �ܰ�
    private int waveIndex = 0;

    private void Update()
    {
        // ** countdown�� 0������ �� SpawnWave �Լ� ���� �� countdown �ʱ�ȭ
        if (countdown <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        // ** countdown�� ���� �ð���ŭ ����
        countdown -= Time.deltaTime;

        // ** countdown�� �ݿø��� ��, ���ڿ��� ��ȯ�Ͽ� �ؽ�Ʈ�� ǥ��
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    // ** Wave ���� �Լ�
    IEnumerator SpawnWave()
    {
        // ** Wave �ܰ� ����
        waveIndex++;

        // ** Wave �ܰ迡 ���� 0.5�ʸ��� �Լ� ȣ�� �ݺ�
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    // ** Enemy ���� �Լ�
    void SpawnEnemy()
    {
        // ** spawnPoint�� ��ġ�� ȸ������ ���� enemyPrefab�� �����Ͽ� ����
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
