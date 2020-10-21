using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    //[HideInInspector]
    public GameObject[] Players;
    [HideInInspector]
    public bool[] PlayerPassesTurn;
    public bool[] PlayerReachedEnd;
    public List<GameObject> PlayerInGoalInOrder;
    public bool GameEnded;
    public MovePlayer[] MovePlayerScripts;
    public int PlayersTurn;
    public GameObject EndScreen;
    public GameObject EndScreenPanel;
    public int PlayersReachedGoal;
    public Transform start;

    public TMP_InputField inputField;

    private int PlayerIndex;
    private WaypointContainer waypointContainer;


    void Awake()
    {
        FindObjectOfType<CharacterContainer>().SpawnPlayers(start.position);
        Destroy(FindObjectOfType<CharacterContainer>().gameObject);

        Players = GameObject.FindGameObjectsWithTag("Player");
        MovePlayerScripts = new MovePlayer[Players.Length];
        waypointContainer = GameObject.FindGameObjectWithTag("WaypointContainer").GetComponent<WaypointContainer>();
        PlayerPassesTurn = new bool[Players.Length];
        PlayerReachedEnd = new bool[Players.Length];

        for (int i = 0; i < Players.Length; i++)
        {
            MovePlayerScripts[i] = Players[i].GetComponent<MovePlayer>();
        }
    }

    void Start()
    {
        PlayerIndex = -1;
        SetPlayersPositons();
        DisOrEnablePlayersTurnImage();
    }

    public void SetPlayersPositons()
    {
        PlayerIndex++;

        if (MovePlayerScripts.Length <= PlayerIndex) return;

        Players[PlayerIndex].GetComponent<SetPlayersStartingPos>().SetPosition();
    }

    public void StartMove(int amount)
    {
        if (MovePlayerScripts[PlayersTurn].PlayerIsMoving) return;

        MovePlayerScripts[PlayersTurn].SetPlayersDestinationTarget(amount);
        Camera.main.GetComponent<CameraToggler>().EnableAutomaticCameraMovement();
    }

    public void NextPlayersTurn()
    {
        if (GameEnded)
            return;

        FindObjectOfType<Dice>().DiceRolled = false;

        if (PlayersTurn < MovePlayerScripts.Length - 1)
        {
            DisOrEnablePlayersTurnImage();
            PlayersTurn++;
            DisOrEnablePlayersTurnImage();
        }
        else
        {
            DisOrEnablePlayersTurnImage();
            PlayersTurn = 0;
            DisOrEnablePlayersTurnImage();
        }

        SkipPlayerIfPlayerPassesTurn();
        SkipIfPlayerReachedEnd();
        CheckIfAllPlayersReachedGoal();

        Camera.main.GetComponent<CameraToggler>().EnableAutomaticCameraMovement();
    }

    void SkipPlayerIfPlayerPassesTurn()
    {
        if (PlayerPassesTurn[PlayersTurn])
        {
            Debug.Log("Player Passes");
            DisOrEnablePlayerPassesImage(false);
            PlayerPassesTurn[PlayersTurn] = false;
            NextPlayersTurn();
        }
    }

    void SkipIfPlayerReachedEnd()
    {
        
        if (PlayerReachedEnd[PlayersTurn])
        {
            NextPlayersTurn();
            Debug.Log("Player reached End");
        }
    }

    void CheckIfAllPlayersReachedGoal()
    {
        PlayersReachedGoal = Players.Length;

        foreach (bool boolean in PlayerReachedEnd)
        {
            if (boolean)
                PlayersReachedGoal--;
        }

        if (PlayersReachedGoal <= 1)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (!PlayerReachedEnd[i])
                {
                    PlayerInGoalInOrder.Add(Players[i]);
                }
            }

            Debug.Log("End");
            EndScreen.SetActive(true);
            EndScreenPanel.SetActive(true);
            LeanTween.scale(EndScreen, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
            PreviousPlayersTurn();
            GameEnded = true;
        }
    }

    public void PreviousPlayersTurn()
    {
        if (PlayersTurn > 0)
        {
            DisOrEnablePlayersTurnImage();
            PlayersTurn--;
            DisOrEnablePlayersTurnImage();
        }
        else
            PlayersTurn = MovePlayerScripts.Length - 1;
    }

    void DisOrEnablePlayersTurnImage()
    {
        Image image = Players[PlayersTurn].GetComponent<PlayerStatsContainer>().LeaderboardDisplay.transform.GetChild(0).GetComponent<Image>();
        image.enabled = !image.enabled;

    }

    public void DisOrEnablePlayerPassesImage(bool enable)
    {
        Image image = Players[PlayersTurn].GetComponent<PlayerStatsContainer>().LeaderboardDisplay.transform.GetChild(1).GetComponent<Image>();
        image.enabled = enable;

    }
}
