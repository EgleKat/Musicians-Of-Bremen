using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollower : MonoBehaviour {

    public Transform transformToFollow;
    public int speed;
    public int followDistance;
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = transform.position;
        Vector3 leaderPosition = transformToFollow.position;

        int xDistance = (int)(leaderPosition.x - targetPosition.x);
        int yDistance = (int)(leaderPosition.y - targetPosition.y);

        if (Math.Abs(xDistance) > followDistance)
        {
            xDistance += Math.Sign(-xDistance) * followDistance;
            targetPosition.x += xDistance;
        }
        if (Math.Abs(yDistance) > followDistance)
        {
            yDistance += Math.Sign(-yDistance) * followDistance;
            targetPosition.y += yDistance;
        }

        Vector3 directionToMove = (targetPosition - transform.position).normalized;

        Vector3 newPosition = Vector3Int.RoundToInt(transform.position + Time.deltaTime * speed * directionToMove);
        if (Vector3.Magnitude(newPosition - transform.position) > Vector3.Magnitude(targetPosition - transform.position))
        {
            // if we've overshot where we're going towards
            newPosition = targetPosition;
        }

        transform.position = newPosition;
    }
}
