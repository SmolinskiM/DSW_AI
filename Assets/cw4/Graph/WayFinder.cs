using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class WayFinder : MonoBehaviour
{
    [SerializeField] public GameObject curentPositon;

    [SerializeField] private List<GameObject> wayPoints;

    [System.Serializable]
    public struct Edge
    {
        public GameObject wayPoint_1;
        public GameObject wayPoint_2;
    }

    private int numberOfEdge = 0;

    public Material lineMaterial;

    public Dictionary<GameObject, List<GameObject>> possibleWay = new Dictionary<GameObject, List<GameObject>>();

    public List<Edge> edgeList = new List<Edge>();

    private List<GameObject> trace = new List<GameObject>();

    private void Awake()
    {
        foreach(Edge edge in edgeList)
        {
            DrawGraph(edge);

            CreateGraph(edge);
        }
    }

    public List<GameObject> CalculationTrace(GameObject destination)
    {
        trace = new List<GameObject>();

        foreach(GameObject way in possibleWay[curentPositon])
        {
            List<GameObject> traceToCheck = FindWay(way, destination, new List<GameObject>());
            if (traceToCheck.Count == 0)
            {
                continue;
            }

            if(traceToCheck.Count >= trace.Count && trace.Count != 0)
            {
                continue;
            }

            trace = traceToCheck;
        }

        if(trace.Count == 0)
        {
            return null;
        }

        trace.Add(destination);
        curentPositon = destination;
        Debug.Log(trace.Count);
        return trace;
    }

    public List<GameObject> FindWay(GameObject current, GameObject destination, List<GameObject> visitedPoints)
    {
        List<GameObject> wayPoints = new List<GameObject>();
        int minLength = int.MaxValue;

        if (possibleWay[current].Contains(destination))
        {
            wayPoints.Add(current);
            return wayPoints;
        }

        visitedPoints.Add(current);
        foreach (GameObject nextPoint in possibleWay[current])
        {
            if (visitedPoints.Contains(nextPoint))
            {
                continue;
            }

            List<GameObject> path = FindWay(nextPoint, destination, visitedPoints);

            if (path.Count > 0 && path.Count + 1 < minLength)
            {
                minLength = path.Count + 1;
                wayPoints.Clear();
                wayPoints.Add(current);
                wayPoints.AddRange(path);
            }
        }

        return wayPoints;
    }

    private void DrawGraph(Edge edge)
    {
        GameObject child = new GameObject();
        child.transform.SetParent(transform);

        LineRenderer lr = child.AddComponent<LineRenderer>();

        lr.material = lineMaterial;
        lr.positionCount = 2;
        lr.widthMultiplier = 0.1f;
        lr.SetPosition(0, edge.wayPoint_1.transform.position);
        lr.SetPosition(1, edge.wayPoint_2.transform.position);
    }

    private void CreateGraph(Edge edge)
    {
        if (!possibleWay.ContainsKey(edge.wayPoint_1))
        {
            possibleWay.Add(edge.wayPoint_1, new List<GameObject>());
        }
        possibleWay[edge.wayPoint_1].Add(edge.wayPoint_2);

        if (!possibleWay.ContainsKey(edge.wayPoint_2))
        {
            possibleWay.Add(edge.wayPoint_2, new List<GameObject>());
        }
        possibleWay[edge.wayPoint_2].Add(edge.wayPoint_1);
    }
}
