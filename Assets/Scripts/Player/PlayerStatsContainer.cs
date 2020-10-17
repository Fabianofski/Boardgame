using UnityEngine.UI;
using UnityEngine;

public class PlayerStatsContainer : MonoBehaviour
{

    public int currentWaypoint;

    [HideInInspector]
    public GameObject LeaderboardDisplay;

    [Header("Leaderboard")]
    public Sprite PlayerImage;
    public GameObject LeaderboardDisplayPrefab;
    public Transform Parent;

    void Awake()
    {
        Parent = GameObject.FindGameObjectWithTag("Leaderboard").GetComponent<RectTransform>();

        LeaderboardDisplay = Instantiate(LeaderboardDisplayPrefab, Parent);
        LeaderboardDisplay.GetComponentInChildren<Image>().sprite = PlayerImage;
    }

}
