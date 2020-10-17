using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    private GameController gamecontroller;
    //[HideInInspector]
    public List<PlayerStatsContainer> playerStats;
    //[HideInInspector]
    public List<PlayerStatsContainer> tempStats;
    //[HideInInspector]
    public PlayerStatsContainer[] playersStatsSorted;

    [Header("Leaderboard")]
    public GameObject[] Players;
    public GameObject[] PlayersSorted;
    public Color[] LeaderboardColors;
    int index;

    void Start()
    {
        gamecontroller = GetComponent<GameController>();
        Players = gamecontroller.Players;
        playersStatsSorted = new PlayerStatsContainer[Players.Length];
        PlayersSorted = new GameObject[Players.Length];

        for (int i = 0; i < Players.Length; i++)
        {
            playerStats.Add(Players[i].GetComponent<PlayerStatsContainer>());
        }
    }

    void Update()
    {
        if(!gamecontroller.GameEnded)
            UpdateLeaderboard();
    }
    public void UpdateLeaderboard()
    {
        playersStatsSorted = new PlayerStatsContainer[Players.Length];
        PlayersSorted = new GameObject[Players.Length];
        index = 0;
        
        for (int i = 0; i < Players.Length; i++)
        {
            if(!gamecontroller.PlayerReachedEnd[i])
                tempStats.Add(playerStats[i]);
        }

        for (int i = 0; i < gamecontroller.PlayerInGoalInOrder.Count; i++)
        {
            playersStatsSorted[i] = gamecontroller.PlayerInGoalInOrder[i].GetComponent<PlayerStatsContainer>();
            PlayersSorted[i] = gamecontroller.PlayerInGoalInOrder[i];
            index++;
        }


        for (int i = index; i < Players.Length; i++) // Loop through every Rank on Leaderboard
        {
            playersStatsSorted[i] = tempStats[0];
            PlayersSorted[i] = tempStats[0].gameObject;
            PlayerStatsContainer storedStat = tempStats[0];

            for (int z = 0; z < tempStats.Count; z++) // Loop through every Player not already sorted
            {
                if (playersStatsSorted[i].currentWaypoint < tempStats[z].currentWaypoint)
                {
                    playersStatsSorted[i] = tempStats[z];
                    PlayersSorted[i] = playersStatsSorted[i].gameObject;
                    tempStats.RemoveAt(z);
                }
            }

            if(storedStat == playersStatsSorted[i])
                tempStats.RemoveAt(0);
        }

        UpdateLeaderboardDisplay();
    }

    void UpdateLeaderboardDisplay()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            TextMeshProUGUI text = playersStatsSorted[i].LeaderboardDisplay.GetComponentInChildren<TextMeshProUGUI>();
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
