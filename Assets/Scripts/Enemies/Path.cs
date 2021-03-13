using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    List<Transform> pointsLst;
    private void Start()
    {
        pointsLst = GetComponentsInChildren<Transform>().Where(t => t != transform).ToList();
    }
    public (int, Vector3) GetNearestChild(Vector3 position)
    {
        Vector3 nearestChild = position;
        float distance = float.MaxValue;

        int i;
        int index = 0;
        for (i = 0; i < pointsLst.Count; i++)
        {

            float newDis = Vector3.Distance(position, pointsLst[i].position);
            if (newDis < distance)
            {
                nearestChild = pointsLst[i].position;
                distance = newDis;
                index = i;

            }

        }
        return (index, nearestChild);
    }

    public (int, Vector3) GetNextChild(int currentIndex)
    {
        int newIndex = (currentIndex + 1) % pointsLst.Count;
        return (newIndex, pointsLst[newIndex].position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild((i + 1) % transform.childCount).position);
        }
    }

    public static object Combine(string dataPath, string v)
    {
        throw new NotImplementedException();
    }
}


