using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    private GameController gamecontroller;
    public List<GameObject> tempList;

    [Header("Leaderboard")]
    public GameObject[] Players;
    public List<GameObject> PlayersSorted;
    public Color[] LeaderboardColors;

    void Start()
    {
        gamecontroller = GetComponent<GameController>();
        Players = gamecontroller.Players;
    }

    void Update()
    {
        if(!gamecontroller.GameEnded)
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        PlayersSorted = new List<GameObject>(Players.Length);

        GetAllPlayersNotInGoal();
        GetAllPlayersInGoal();

        tempList = tempList.OrderByDescending(x => x.GetComponent<PlayerStatsContainer>().currentWaypoint).ToList();
        foreach(GameObject go in tempList)
        {
            PlayersSorted.Add(go);
        }
        tempList.Clear();

        UpdateLeaderboardDisplay();
    }

    private void GetAllPlayersInGoal()
    {
        for (int i = 0; i < gamecontroller.PlayerInGoalInOrder.Count; i++)
        {
            PlayersSorted.Add(gamecontroller.PlayerInGoalInOrder[i]);
        }
    }

    private void GetAllPlayersNotInGoal()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (!gamecontroller.PlayerReachedEnd[i])
                tempList.Add(Players[i]);
        }
    }

    void UpdateLeaderboardDisplay()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            PlayerStatsContainer playerstats = PlayersSorted[i].GetComponent<PlayerStatsContainer>();
            TextMeshProUGUI text = playerstats.LeaderboardDisplay.GetComponentInChildren<TextMeshProUGUI>();
            text.text = i + 1 + ".";
            text.color = LeaderboardColors[3];

            switch (i)
            {
                case 0: text.color = LeaderboardColors[0];
                    break;
                case 1:
                    text.color = LeaderboardColors[1];
                    break;
                case 2:
                    text.color = LeaderboardColors[2];
                    break;
            }
        }
    }
}
