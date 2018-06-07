using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterControllerScript : MonoBehaviour {

	Rigidbody2D rigidBody2D;

	public float maxSpeed = 7f;
	public float jumpForce = 450f;
	public bool colliding = false;

	bool facingRight = true;
	bool grounded = false;
	float groundRadius = 0.2f;
	float interactRadius = 1.0f;

	public GameObject interactable;

	public Transform groundCheck;
	public LayerMask whatIsGround;
	public Transform interactCheck;
	public LayerMask whatIsCritter;

	void Start() {
		rigidBody2D = GetComponent<Rigidbody2D>();
	}

	void Update() {
		if (grounded && Input.GetKeyDown(KeyCode.Space))
			rigidBody2D.AddForce(new Vector2(0, jumpForce));
		interactWithObject();
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		float move = Input.GetAxis("Horizontal");
		rigidBody2D.velocity = new Vector2(move * maxSpeed, rigidBody2D.velocity.y);

		if ((move > 0 && !facingRight) || (move < 0 && facingRight))
			flip();
		Collider2D critterCollision = Physics2D.OverlapCircle(interactCheck.position, interactRadius, whatIsCritter);
		if (critterCollision)
			interactable = critterCollision.gameObject;
		else
			interactable = null;
	}

	void flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void interactWithObject() {
		if (interactable) {
			CritterControllerScript controller = interactable.GetComponent(typeof(CritterControllerScript)) as CritterControllerScript;
			if (Input.GetKeyDown(KeyCode.F))
				controller.interact(KeyCode.F);
			if (Input.GetKeyDown(KeyCode.W))
				controller.interact(KeyCode.W);
		}
	}
}
