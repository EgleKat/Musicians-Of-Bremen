using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public enum Direction {Right = 0, Down = 1, Up = 2, Left = 3};
	private static readonly Dictionary<Direction, Vector2> directionVectors = new Dictionary<Direction, Vector2> {
		{Direction.Up, Vector2.up*2},
		{Direction.Down, Vector2.down*2},
		{Direction.Right, Vector2.right*3},
		{Direction.Left, Vector2.left*3},
	};
	private static readonly Dictionary<KeyCode, Direction> keyDirections = new Dictionary<KeyCode, Direction> {
		{KeyCode.W, Direction.Up},
		{KeyCode.S, Direction.Down},
		{KeyCode.D, Direction.Right},
		{KeyCode.A, Direction.Left},
	};
	private Direction currentDirection = Direction.Right;
	private KeyCode currentKey = KeyCode.None;
	private int movementSpeed = 3;
	private Animator animator;
	private Rigidbody2D rb;

	void Start() {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (KeyValuePair<KeyCode, Direction> _keyCode in keyDirections) {
			KeyCode keyCode = _keyCode.Key;
			if (currentKey == KeyCode.None && Input.GetKey(keyCode)) {
				currentKey = keyCode;
				currentDirection = keyDirections[keyCode];
				animator.SetInteger("Direction", (int)currentDirection);
				animator.SetTrigger("MovingTrigger");
			}
		}
		foreach (KeyValuePair<KeyCode, Direction> _keyCode in keyDirections) {
			KeyCode keyCode = _keyCode.Key;
			if (currentKey == keyCode && Input.GetKeyUp(keyCode)) {
				currentKey = KeyCode.None;
				animator.SetTrigger("NotMovingTrigger");
			}
		}

		// if (currentKey != KeyCode.None) {
		// 	transform.localPosition += movementSpeed * directionVectors[currentDirection];
		// }
	}

	public void MoveMe(Direction direction) {
		float x = Mathf.Round(rb.position.x);
		float y = Mathf.Round(rb.position.y);

		rb.MovePosition(new Vector2(x, y) + movementSpeed * directionVectors[direction]);

		// transform.localPosition += movementSpeed * directionVectors[direction];
	}
}
