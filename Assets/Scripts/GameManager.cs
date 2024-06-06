using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameObject player1, player2;
    private static GameObject whoWinsText, player1MoveText, player2MoveText;

    public static bool gameOver = false;
    public static int diceSideThrown = 0;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;

    private void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        whoWinsText = GameObject.Find("WhoWinsText");
        player1MoveText = GameObject.Find("Player1MoveText");
        player2MoveText = GameObject.Find("Player2MoveText");

        player1.GetComponent<PathFinding>().moveAllowed = false;
        player2.GetComponent<PathFinding>().moveAllowed = false;

        player1MoveText.SetActive(false);
        player2MoveText.SetActive(false);
        whoWinsText.SetActive(false);
    }

    private void Update()
    {
        if (player1.GetComponent<PathFinding>().currentWaypointIndex > player1StartWaypoint + diceSideThrown)
        {
            player1.GetComponent<PathFinding>().moveAllowed = false;
            player1MoveText.SetActive(false);
            player2MoveText.SetActive(true);
            player1StartWaypoint = player1.GetComponent<PathFinding>().currentWaypointIndex - 1;
        }
        if (player2.GetComponent<PathFinding>().currentWaypointIndex > player2StartWaypoint + diceSideThrown)
        {
            player2.GetComponent<PathFinding>().moveAllowed = false;
            player2MoveText.SetActive(false);
            player1MoveText.SetActive(true);
            player2StartWaypoint = player2.GetComponent<PathFinding>().currentWaypointIndex - 1;
        }
        if (player1.GetComponent<PathFinding>().currentWaypointIndex == player1.GetComponent<PathFinding>().waypoints.Length)
        {
            whoWinsText.GetComponent<Text>().text = "Player 1 Wins!";
            whoWinsText.SetActive(true);
            player1MoveText.SetActive(false);
            player2MoveText.SetActive(false);
            gameOver = true;
        }
        if (player2.GetComponent<PathFinding>().currentWaypointIndex == player2.GetComponent<PathFinding>().waypoints.Length)
        {
            whoWinsText.GetComponent<Text>().text = "Player 2 Wins!";
            whoWinsText.SetActive(true);
            player1MoveText.SetActive(false);
            player2MoveText.SetActive(false);
            gameOver = true;
        }
    }

    public static void MovePlayer(int playerToMove)
    {
        switch (playerToMove)
        {
            case 1:
                player1.GetComponent<PathFinding>().moveAllowed = true;
                break;
            case 2:
                player2.GetComponent<PathFinding>().moveAllowed = true;
                break;
        }
    }
}
