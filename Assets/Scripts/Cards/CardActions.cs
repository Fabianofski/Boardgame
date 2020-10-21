using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardActions : MonoBehaviour
{

    public Button button;
    public GameObject Panel;
    private GameController gamecontroller;
    private WaypointContainer waypointContainer;
    private Leaderboard leaderboard;

    private CardContainer cardContainer;

    void Awake()
    {
        gamecontroller = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        leaderboard = GameObject.FindWithTag("GameController").GetComponent<Leaderboard>();
        waypointContainer = GameObject.FindGameObjectWithTag("WaypointContainer").GetComponent<WaypointContainer>();
        cardContainer = GameObject.FindGameObjectWithTag("WaypointContainer").GetComponent<CardContainer>();
    }

    public void DisableCard()
    {
        LeanTween.scale(gameObject, Vector3.zero, .5f).setEase(LeanTweenType.easeInBack).setOnComplete(Deactivate);
        cardContainer.CardHiding.Play();

        button.onClick.RemoveAllListeners();
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
        Panel.SetActive(false);
    }

    public void NothingHappens()
    {
        Invoke("NextTurn", .5f);
    }

    public void PassTurn()
    {
        gamecontroller.PlayerPassesTurn[gamecontroller.PlayersTurn] = true;
        gamecontroller.DisOrEnablePlayerPassesImage(true);
        Invoke("NextTurn", .5f);
    }

    public void MovePlayerXPoints(int number)
    {
        gamecontroller.MovePlayerScripts[gamecontroller.PlayersTurn].SetPlayersDestinationTarget(number);
    }

    public void MovePlayerToWaypoint(int number)
    {
        gamecontroller.MovePlayerScripts[gamecontroller.PlayersTurn].TargetField = number;

        GameObject Player = gamecontroller.Players[gamecontroller.PlayersTurn];
        Player.GetComponent<PlayerStatsContainer>().currentWaypoint = number;
        Player.transform.position = waypointContainer.waypoints[number - 1].position;

        Player.GetComponentInChildren<BlendCharacter>().HideCharacter();
        Camera.main.GetComponent<CameraToggler>().EnableAutomaticCameraMovement();
        Invoke("NextTurn", 4f);
    }

    public void SwitchPlayerWithFirstPlayer()
    {
        SwitchPositionOfPlayers(leaderboard.PlayersSorted[0 + gamecontroller.PlayersReachedGoal - gamecontroller.Players.Length]);
    }

    public void SwitchPlayerWithLastPlayer()
    {
        SwitchPositionOfPlayers(leaderboard.PlayersSorted[leaderboard.PlayersSorted.Count - 1]);
    }

    private PlayerStatsContainer Player1Stats, Player2Stats;
    private int Player1Waypoint, Player2Waypoint;
    private Vector3 Player1Pos, Player2Pos;
    private GameObject p1, p2;

    public void SwitchPositionOfPlayers(GameObject Player2)
    {
        GameObject Player1 = gamecontroller.Players[gamecontroller.PlayersTurn];

        p1 = Player1;
        p2 = Player2;

        p1.GetComponentInChildren<BlendCharacter>().HideCharacter();
        p2.GetComponentInChildren<BlendCharacter>().HideCharacter();

        GetValues();

        Invoke("ApplyValues", p1.GetComponentInChildren<BlendCharacter>().BlendTime);

    }
    private void GetValues()
    {
        // Get Values
        Player1Stats = p1.GetComponent<PlayerStatsContainer>();
        Player2Stats = p2.GetComponent<PlayerStatsContainer>();

        Player1Waypoint = Player1Stats.currentWaypoint;
        Player2Waypoint = Player2Stats.currentWaypoint;

        Player1Pos = p1.transform.position;
        Player2Pos = p2.transform.position;
    }

    private void ApplyValues()
    {
        // Apply Values
        p1.transform.position = Player2Pos;
        p2.transform.position = Player1Pos;

        Player1Stats.currentWaypoint = Player2Waypoint;
        Player2Stats.currentWaypoint = Player1Waypoint;
        p1.GetComponent<MovePlayer>().TargetField = Player2Waypoint;
        p2.GetComponent<MovePlayer>().TargetField = Player1Waypoint;

        Invoke("NextTurn", .5f);
    }

    public void NextTurn()
    {
        gamecontroller.NextPlayersTurn();
        Camera.main.GetComponent<CameraToggler>().EnableAutomaticCameraMovement();
    }

}
