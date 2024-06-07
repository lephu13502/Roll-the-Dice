using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform[] waypoints2;
    public Transform[] newWaypoints;

    [SerializeField] private float moveSpeed = 1f;

    [HideInInspector]
    public int currentWaypointIndex = 0;
    public bool moveAllowed = false;

    private void Start()
    {
        //check the current level
        if (GameManager.currentLevel == 1)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i].transform.position = new Vector2(waypoints[i].transform.position.x, waypoints[i].transform.position.y + 0.3f);
            }
            transform.position = waypoints[currentWaypointIndex].transform.position;

            newWaypoints = new Transform[waypoints.Length];
            for (int i = 0; i < waypoints.Length; i++)
            {
                newWaypoints[i] = waypoints[i];
            }
        }
        if (GameManager.currentLevel == 2)
        {
            for (int i = 0; i < waypoints2.Length; i++)
            {
                waypoints2[i].transform.position = new Vector2(waypoints2[i].transform.position.x, waypoints2[i].transform.position.y + 0.3f);
            }
            transform.position = waypoints2[currentWaypointIndex].transform.position;

            newWaypoints = new Transform[waypoints2.Length];
            for (int i = 0; i < waypoints2.Length; i++)
            {
                newWaypoints[i] = waypoints2[i];
            }
        }
    }

    private void Update()
    {
        if (moveAllowed)
        {
            Move();
        }
    }

    private void Move()
    {
        if (currentWaypointIndex <= newWaypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, newWaypoints[currentWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == newWaypoints[currentWaypointIndex].transform.position)
            {
                currentWaypointIndex += 1;
            }
        }
    }
    public void ResetPlayer()
    {
        currentWaypointIndex = 0;
        //update the new waypoints to level 2 waypoints
        if (GameManager.currentLevel == 2)
        {
            newWaypoints = new Transform[waypoints2.Length];
            for (int i = 0; i < waypoints2.Length; i++)
            {
                newWaypoints[i] = waypoints2[i];
            }
        }
        transform.position = newWaypoints[currentWaypointIndex].transform.position;
    }
}
