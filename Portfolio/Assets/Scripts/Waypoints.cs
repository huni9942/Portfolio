using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // ** Waypoint를 담을 트랜스폼 배열 변수 points
    public static Transform[] points;

    private void Awake()
    {
        // ** points는 Waypoints의 자식 갯수 길이 만큼의 배열
        points = new Transform[transform.childCount];
        // ** Waypoints의 i번째 자식을 색인하여 i번째 배열 요소로 저장, 자식 갯수만큼 반복
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
