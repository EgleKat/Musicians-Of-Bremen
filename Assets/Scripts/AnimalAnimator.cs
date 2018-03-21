using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimator : MonoBehaviour {

	[SerializeField]
	private string spriteSheetName;
	[SerializeField]
 	private uint spriteOffset;
	[SerializeField]
	private uint spritesPerRow;

	public enum Direction {Right = 0, Down = 1, Up = 2, Left = 3};
	private static readonly Dictionary<Direction, Vector2> directionVectors = new Dictionary<Direction, Vector2> {
		{Direction.Up, Vector2.up*6},
		{Direction.Down, Vector2.down*6},
		{Direction.Right, Vector2.right*9},
		{Direction.Left, Vector2.left*9},
	};
	private static readonly Dictionary<KeyCode, Direction> keyDirections = new Dictionary<KeyCode, Direction> {
		{KeyCode.W, Direction.Up},
		{KeyCode.S, Direction.Down},
		{KeyCode.D, Direction.Right},
		{KeyCode.A, Direction.Left},
	};
	private Dictionary<Direction, SpriteAnimation> movingAnimations = new Dictionary<Direction, SpriteAnimation>();
	private Dictionary<Direction, SpriteAnimation> idleAnimations = new Dictionary<Direction, SpriteAnimation>();
	private Direction currentDirection = Direction.Right;
	private KeyCode currentKey = KeyCode.None;
	private Sprite[] allSprites;

	void Start() {
		allSprites = Resources.LoadAll<Sprite>(spriteSheetName);

		movingAnimations[Direction.Down] = gameObject.AddComponent<SpriteAnimation>();
		movingAnimations[Direction.Down].AddFrames(LoadSprites(spriteOffset + 0*spritesPerRow, 3));
		movingAnimations[Direction.Down].SetMovementVector(directionVectors[Direction.Down]);
		movingAnimations[Direction.Left] = gameObject.AddComponent<SpriteAnimation>();
		movingAnimations[Direction.Left].AddFrames(LoadSprites(spriteOffset + 1*spritesPerRow, 3));
		movingAnimations[Direction.Left].SetMovementVector(directionVectors[Direction.Left]);
		movingAnimations[Direction.Right] = gameObject.AddComponent<SpriteAnimation>();
		movingAnimations[Direction.Right].AddFrames(LoadSprites(spriteOffset + 2*spritesPerRow, 3));
		movingAnimations[Direction.Right].SetMovementVector(directionVectors[Direction.Right]);
		movingAnimations[Direction.Up] = gameObject.AddComponent<SpriteAnimation>();
		movingAnimations[Direction.Up].AddFrames(LoadSprites(spriteOffset + 3*spritesPerRow, 3));
		movingAnimations[Direction.Up].SetMovementVector(directionVectors[Direction.Up]);

		idleAnimations[Direction.Down] = gameObject.AddComponent<SpriteAnimation>();
		idleAnimations[Direction.Down].AddFrames(LoadSprites(1+spriteOffset+ 0*spritesPerRow, 1));
		idleAnimations[Direction.Left] = gameObject.AddComponent<SpriteAnimation>();
		idleAnimations[Direction.Left].AddFrames(LoadSprites(1+spriteOffset+ 1*spritesPerRow, 1));
		idleAnimations[Direction.Right] = gameObject.AddComponent<SpriteAnimation>();
		idleAnimations[Direction.Right].AddFrames(LoadSprites(1+spriteOffset+ 2*spritesPerRow, 1));
		idleAnimations[Direction.Up] = gameObject.AddComponent<SpriteAnimation>();
		idleAnimations[Direction.Up].AddFrames(LoadSprites(1+spriteOffset+ 3*spritesPerRow, 1));
	}
	
	// Update is called once per frame
	void Update () {
		foreach (KeyValuePair<KeyCode, Direction> _keyCode in keyDirections) {
			KeyCode keyCode = _keyCode.Key;
			if (currentKey == KeyCode.None && Input.GetKey(keyCode)) {
				currentKey = keyCode;
				currentDirection = keyDirections[keyCode];
				movingAnimations[currentDirection].Play();
			}
		}
		foreach (KeyValuePair<KeyCode, Direction> _keyCode in keyDirections) {
			KeyCode keyCode = _keyCode.Key;
			if (currentKey == keyCode && Input.GetKeyUp(keyCode)) {
				movingAnimations[currentDirection].Stop();
				currentKey = KeyCode.None;
				idleAnimations[currentDirection].PlayOnce();
			}
		}

		// if (currentKey != KeyCode.None) {
		// 	transform.localPosition += movementSpeed * directionVectors[currentDirection];
		// }
	}

	private Sprite[] LoadSprites(uint offset, uint count) {
		Sprite[] sprites = new Sprite[count];
		System.Array.Copy(allSprites, offset, sprites, 0, count);
		return sprites;
	}
}
