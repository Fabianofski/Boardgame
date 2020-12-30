using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private WaypointContainer waypointContainer;
    private PlayerStatsContainer playerStatsContainer;
    private GameController gamecontroller;
    private Animator anim;

    [SerializeField] float speed;
    private Vector2 destination;
    public  bool PlayerIsMoving;
    public int TargetField = 0;
    private bool WrongDirection;
    public Vector3 targetOffset;

    public AudioSource RunSound;

    void Awake()
    {
        playerStatsContainer = GetComponent<PlayerStatsContainer>();
        waypointContainer = GameObject.FindWithTag("WaypointContainer").GetComponent<WaypointContainer>();
        gamecontroller = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        anim = GetComponent<Animator>();

        
    }


    public void SetPlayersDestinationTarget(int number)
    {
        PlayerIsMoving = true;
        TargetField = playerStatsContainer.currentWaypoint + number;

        if (TargetField < playerStatsContainer.currentWaypoint)
            WrongDirection = true;
        else
            WrongDirection = false;
    }

    void Update()
    {
        anim.SetBool("isRunning", PlayerIsMoving);

        if (RunSound.isPlaying && !PlayerIsMoving)
            RunSound.Stop();

        if (!PlayerIsMoving) return;

        if (!RunSound.isPlaying)
            RunSound.Play();

        if (Input.GetMouseButtonDown(0))
            Skip();

        if (playerStatsContainer.currentWaypoint >= waypointContainer.waypoints.Length) 
        { 
            TargetField = waypointContainer.waypoints.Length;
            playerStatsContainer.currentWaypoint = TargetField;

            gamecontroller.PlayerReachedEnd[gamecontroller.PlayersTurn] = true;
            gamecontroller.PlayerInGoalInOrder.Add(gameObject);
            CheckIfRouteHasEndedAndShowCard();
        }
        else
            FollowPathUntilWaypoint();
    }

    void FollowPathUntilWaypoint()
    {
        if (!PlayerReachednextWaypoint())
        {
            Vector3 player = transform.position;
            Vector3 target = SetTargetPosition(player);
            transform.position = Vector3.MoveTowards(player, target, speed * Time.deltaTime);

            SetDirectionsInAnimator(player, target);
        }
        else
            SetDestinationToNextWaypoint();
    }

    private void SetDirectionsInAnimator(Vector3 player, Vector3 target)
    {
        Vector2 dir = target - player;
        dir = dir.normalized;
        anim.SetFloat("horizontal", dir.x);
        anim.SetFloat("vertical", dir.y);
    }

    bool PlayerReachednextWaypoint()
    {
        if (LevelHasEnded()) { return false; }

        CheckIfRouteHasEndedAndShowCard();

        Vector2 player = transform.position;
        Vector2 target = SetTargetPosition(player);

        if (player == target)
            return true;
        else
            return false;
    }

    bool LevelHasEnded()
    {
        if (playerStatsContainer.currentWaypoint >= waypointContainer.waypoints.Length)
        {
            PlayerIsMoving = false;
            return true;
        }
        if (playerStatsContainer.currentWaypoint <= 1 && WrongDirection)
        {
            PlayerIsMoving = false;
            return true;
        }

        return false;
    }

    private void CheckIfRouteHasEndedAndShowCard()
    {
        if (playerStatsContainer.currentWaypoint == TargetField)
        {
            PlayerIsMoving = false;
            if (!WrongDirection)
                waypointContainer.waypoints[playerStatsContainer.currentWaypoint - 1].gameObject.SendMessage("ReceiveActionCall");
            else
                gamecontroller.NextPlayersTurn();

        }
    }

    private Vector3 SetTargetPosition(Vector3 player)
    {
        if(!WrongDirection)
          return new Vector3(waypointContainer.waypoints[playerStatsContainer.currentWaypoint].position.x,
                             waypointContainer.waypoints[playerStatsContainer.currentWaypoint].position.y, 
                             player.z) + targetOffset;

        return new Vector3(waypointContainer.waypoints[playerStatsContainer.currentWaypoint - 2].position.x,
                           waypointContainer.waypoints[playerStatsContainer.currentWaypoint - 2].position.y, 
                           player.z) + targetOffset;

    }
    private void SetDestinationToNextWaypoint()
    {
        if (!WrongDirection && PlayerIsMoving)
            playerStatsContainer.currentWaypoint++;
        else if(PlayerIsMoving)
            playerStatsContainer.currentWaypoint--;
    }

    
    void Skip()
    {
        PlayerIsMoving = false;

        if (TargetField + 1 > waypointContainer.waypoints.Length)
            TargetField = waypointContainer.waypoints.Length;

        transform.position = waypointContainer.waypoints[TargetField - 1].position + targetOffset;
        playerStatsContainer.currentWaypoint = TargetField;
    }

}
