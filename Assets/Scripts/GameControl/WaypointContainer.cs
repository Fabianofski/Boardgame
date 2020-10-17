using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointContainer : MonoBehaviour
{

    public Transform[] waypoints;

    void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(waypoints[i].position, waypoints[i+1].position);
        }
    }

}
