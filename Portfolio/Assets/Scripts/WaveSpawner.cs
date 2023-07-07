using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    // ** enemyPrefab의 트랜스폼
    public Transform enemyPrefab;
    // ** spawnPoint의 트랜스폼
    public Transform spawnPoint;

    // ** Wave 사이 시간 간격
    public float timeBetweenWaves = 5.0f;
    // ** 다음 Wave까지 걸리는 시간
    private float countdown = 2.0f;

    // ** countdown을 표기하는 텍스트
    public Text waveCountdownText;

    // ** 현재 Wave 단계
    private int waveIndex = 0;

    private void Update()
    {
        // ** countdown이 0이하일 때 SpawnWave 함수 실행 및 countdown 초기화
        if (countdown <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        // ** countdown은 실제 시간만큼 감소
        countdown -= Time.deltaTime;

        // ** countdown을 반올림한 뒤, 문자열로 변환하여 텍스트로 표시
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    // ** Wave 생성 함수
    IEnumerator SpawnWave()
    {
        // ** Wave 단계 증가
        waveIndex++;

        // ** Wave 단계에 따라 0.5초마다 함수 호출 반복
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    // ** Enemy 생성 함수
    void SpawnEnemy()
    {
        // ** spawnPoint의 위치와 회전값을 가진 enemyPrefab을 복제하여 생성
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
