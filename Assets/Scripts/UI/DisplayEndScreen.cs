using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayEndScreen : MonoBehaviour
{

    public GameObject prefab;
    public Transform Parent;
    public Leaderboard leaderboard;

    private GameController gameController;

    void OnEnable()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        for (int i = 0; i < gameController.Players.Length; i++)
        {
            GameObject PlayerRank = Instantiate(prefab, Parent);

            PlayerRank.GetComponent<Image>().sprite = gameController.PlayerInGoalInOrder[i].GetComponent<PlayerStatsContainer>().PlayerImage;

            TextMeshProUGUI text = PlayerRank.GetComponentInChildren<TextMeshProUGUI>();
            text.text = i + 1 + ".";
            switch (i)
            {
                case 0:
                    text.color = leaderboard.LeaderboardColors[0];
                    break;
                case 1:
                    text.color = leaderboard.LeaderboardColors[1];
                    break;
                case 2:
                    text.color = leaderboard.LeaderboardColors[2];
                    break;
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
