using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameObject player1, player2;
    private static GameObject whoWinsText, player1MoveText, player2MoveText;
    //button to move to level 2, using TMPro
    private static Button nextLevelButton;
    private static GameObject board1;
    private static GameObject board2;

    public static bool gameOver = false;
    public static int diceSideThrown = 0;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;
    public static int currentLevel { get; set; } = 1;

    private void Start()
    {
        board1 = GameObject.Find("Board");
        board2 = GameObject.Find("Board2");
        if (currentLevel == 1)
        {
            board1.gameObject.SetActive(true);
            board2.gameObject.SetActive(false);
        }
        else
        {
            board1.gameObject.SetActive(false);
            board2.gameObject.SetActive(true);
        }
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        whoWinsText = GameObject.Find("WhoWinsText");
        player1MoveText = GameObject.Find("Player1MoveText");
        player2MoveText = GameObject.Find("Player2MoveText");

        player1.GetComponent<PathFinding>().moveAllowed = false;
        player2.GetComponent<PathFinding>().moveAllowed = false;

        player1MoveText.SetActive(true);
        player2MoveText.SetActive(false);
        whoWinsText.SetActive(false);
        //find TMPro button
        nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
        nextLevelButton.onClick.AddListener(NextLevel);
        //set the button to false at the start
        nextLevelButton.gameObject.SetActive(false);
        //set button listener

    }

    private void Update()
    {
        if (!gameOver)
        {
            CheckTurn();
            CheckGameOver();
        }
    }
    private void CheckTurn()
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
    }
    private void CheckGameOver()
    {
        if (player1.GetComponent<PathFinding>().currentWaypointIndex == player1.GetComponent<PathFinding>().newWaypoints.Length)
        {
            whoWinsText.GetComponent<Text>().text = "Player 1 Wins!";
            whoWinsText.SetActive(true);
            player1MoveText.SetActive(false);
            player2MoveText.SetActive(false);
            gameOver = true;
            nextLevelButton.gameObject.SetActive(true);
        }
        if (player2.GetComponent<PathFinding>().currentWaypointIndex == player2.GetComponent<PathFinding>().newWaypoints.Length)
        {
            whoWinsText.GetComponent<Text>().text = "Player 2 Wins!";
            whoWinsText.SetActive(true);
            player1MoveText.SetActive(false);
            player2MoveText.SetActive(false);
            gameOver = true;
            nextLevelButton.gameObject.SetActive(true);
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
    //function to add the listener to the button
    public void NextLevel()
    {
        currentLevel = 2;
        board1.gameObject.SetActive(false);
        board2.gameObject.SetActive(true);
        player1.GetComponent<PathFinding>().ResetPlayer();
        player2.GetComponent<PathFinding>().ResetPlayer();
        player1StartWaypoint = 0;
        player2StartWaypoint = 0;
        player1MoveText.SetActive(true);
        player2MoveText.SetActive(false);
        whoWinsText.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        gameOver = false;

        player1.GetComponent<PathFinding>().moveAllowed = true;
        player2.GetComponent<PathFinding>().moveAllowed = false;

    }
}
