using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Parameters")]
    public float speed;
    public float Offset;

    private GameObject Target;
    private WaypointContainer waypointContainer;
    private GameController gameController;
    private Vector3 dir;
    public List<SetPlayersStartingPos> targetOffsets;


    void Start()
    {
        waypointContainer = GameObject.FindGameObjectWithTag("WaypointContainer").GetComponent<WaypointContainer>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();


        foreach (GameObject x in gameController.Players)
        {
            targetOffsets.Add(x.GetComponent<SetPlayersStartingPos>());
        }
    }

    void Update()
    {
        SelectTarget();
        MoveCameraToTarget();
        SetOrthographicSize();
    }

    private void SelectTarget()
    {
        Target = gameController.Players[gameController.PlayersTurn];
    }

    private void SetOrthographicSize()
    {
       Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3f, 2f * Time.deltaTime);
    }

    private void MoveCameraToTarget()
    {
        Vector3 targetPos = Target.transform.position + CalculateDirectionToNextWaypoint() * Offset;
        targetPos = new Vector3(targetPos.x, targetPos.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    Vector3 CalculateDirectionToNextWaypoint()
    {
        int waypoint = Target.GetComponent<PlayerStatsContainer>().currentWaypoint;
        

        if (waypoint < waypointContainer.waypoints.Length)
              dir = waypointContainer.waypoints[waypoint].position - Target.transform.position;
        else
            dir = waypointContainer.waypoints[waypointContainer.waypoints.Length - 1].position - Target.transform.position;

        dir += targetOffsets[gameController.PlayersTurn].targetOffset;

        dir = dir.normalized;

        return dir;
    }
}
