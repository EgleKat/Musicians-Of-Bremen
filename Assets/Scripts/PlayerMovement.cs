using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public enum Direction {Right = 0, Down = 1, Up = 2, Left = 3};
	private static readonly Dictionary<Direction, Vector3> directionVectors = new Dictionary<Direction, Vector3> {
		{Direction.Up, Vector3.up},
		{Direction.Down, Vector3.down},
		{Direction.Right, Vector3.right*2},
		{Direction.Left, Vector3.left*2},
	};
	private static readonly Dictionary<KeyCode, Direction> keyDirections = new Dictionary<KeyCode, Direction> {
		{KeyCode.W, Direction.Up},
		{KeyCode.S, Direction.Down},
		{KeyCode.D, Direction.Right},
		{KeyCode.A, Direction.Left},
	};
	private Direction currentDirection = Direction.Right;
	private KeyCode currentKey = KeyCode.None;
	private int movementSpeed = 2;
	private Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (KeyValuePair<KeyCode, Direction> _keyCode in keyDirections) {
			KeyCode keyCode = _keyCode.Key;
			if (currentKey == keyCode && Input.GetKeyUp(keyCode)) {
				currentKey = KeyCode.None;
				animator.SetBool("Moving", false);
			}
		}
		foreach (KeyValuePair<KeyCode, Direction> _keyCode in keyDirections) {
			KeyCode keyCode = _keyCode.Key;
			if (currentKey == KeyCode.None && Input.GetKey(keyCode)) {
				currentKey = keyCode;
				currentDirection = keyDirections[keyCode];
				animator.SetInteger("Direction", (int)currentDirection);
				animator.SetBool("Moving", true);
			}
		}

		// if (currentKey != KeyCode.None) {
		// 	transform.localPosition += movementSpeed * directionVectors[currentDirection];
		// }
	}

	public void MoveMe(Direction direction) {
		transform.localPosition += movementSpeed * directionVectors[currentDirection];
	}
}
