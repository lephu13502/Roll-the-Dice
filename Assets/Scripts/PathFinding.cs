using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Transform[] waypoints;

    [SerializeField] private float moveSpeed = 1f;

    [HideInInspector]
    public int currentWaypointIndex = 0;
    public bool moveAllowed = false;

    private void Start()
    {
        //add 0.5f to the y position of every waypoint to make the player move to the center of the waypoint
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].transform.position = new Vector2(waypoints[i].transform.position.x, waypoints[i].transform.position.y + 0.3f);
        }
        transform.position = waypoints[currentWaypointIndex].transform.position;
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
        if (currentWaypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == waypoints[currentWaypointIndex].transform.position)
            {
                currentWaypointIndex += 1;
            }
        }
    }
}
