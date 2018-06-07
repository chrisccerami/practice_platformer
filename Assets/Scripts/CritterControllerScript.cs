using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterControllerScript : MonoBehaviour, Interactable {
	Rigidbody2D rigidBody2D;
	public Rigidbody2D playerRigidBody;

	public float maxSpeed = 1f;
	public bool following = false;

	bool facingRight = true;

	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		float move = moveSpeed();
		rigidBody2D.velocity = new Vector2(move * maxSpeed, rigidBody2D.velocity.y);

		if ((move > 0 && !facingRight) || (move < 0 && facingRight))
			Flip();
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	float moveSpeed() {
		if (following) {
			if (playerRigidBody.position.x + 1 > rigidBody2D.position.x) {
				return 0.2f;
			} else if (playerRigidBody.position.x - 1 < rigidBody2D.position.x) {
				return -0.2f;
			} else {
				return 0;
			}
		} else return 0;
	}

	public void interact(KeyCode keyCode) {
		if (keyCode == KeyCode.F) {
			following = true;
		} else if (keyCode == KeyCode.W) {
			following = false;
		}
	}
}
