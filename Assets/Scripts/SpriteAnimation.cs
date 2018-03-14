using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour {
	private Sprite[] frames;
	private uint slowFactor = 4;
	private uint currentFrame = 0;
	private uint currentFrameRepeat = 0;
	private bool playing = false;
	private bool forward = true;
	private Vector2 movementVector;

	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rb;
	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	public void AddFrames(Sprite[] frames) {
		this.frames = frames;
	}

	public void SetMovementVector(Vector2 movementVector) {
		this.movementVector = movementVector;
	}

	public void Play() {
		currentFrame = 0;
		currentFrameRepeat = 0;
		forward = true;
		playing = true;
	}
	public void PlayOnce() {
		spriteRenderer.sprite = frames[0];
	}
	public void Stop() {
		playing = false;
	}

	void FixedUpdate() {
		rb.velocity = Vector2.zero;
		if (playing) {
			spriteRenderer.sprite = frames[currentFrame];
			if (currentFrameRepeat == 0) {
				float x = Mathf.Round(rb.position.x);
				float y = Mathf.Round(rb.position.y);

				rb.MovePosition(new Vector2(x, y) + movementVector);
			}

			currentFrameRepeat++;

			if (currentFrameRepeat == slowFactor) {
				currentFrameRepeat = 0;
				if (forward) {
					currentFrame++;
				} else {
					currentFrame--;
				}
			}
			if (currentFrame == frames.Length - 1) {
				forward = false;
			}
			if (currentFrame == 0) {
				forward = true;
			}
		}
	}
}
