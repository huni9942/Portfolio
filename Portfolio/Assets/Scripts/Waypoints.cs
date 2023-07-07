using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // ** Waypoint�� ���� Ʈ������ �迭 ���� points
    public static Transform[] points;

    private void Awake()
    {
        // ** points�� Waypoints�� �ڽ� ���� ���� ��ŭ�� �迭
        points = new Transform[transform.childCount];
        // ** Waypoints�� i��° �ڽ��� �����Ͽ� i��° �迭 ��ҷ� ����, �ڽ� ������ŭ �ݺ�
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
