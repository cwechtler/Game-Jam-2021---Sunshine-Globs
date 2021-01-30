using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 5f;
	[SerializeField] private float boostSpeed = 15f;
	[SerializeField] private float dropSpeed = .5f;

	Rigidbody2D myRigidbody2D;
	SpriteRenderer spriteRenderer;
	CircleCollider2D myCircleCollider2D;
	BoxCollider2D myBoxCollider2D;

	float startingGravityScale;
	bool moveHorizontaly;
	bool moveVertically;

	//public List<string> collectedItems;
	//public Dictionary<string, GameObject> collectedItems;


	public static bool IsDead { get; set; } = false;

	void Start()
	{
		IsDead = false;

		myRigidbody2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		myCircleCollider2D = GetComponent<CircleCollider2D>();
		myBoxCollider2D = GetComponent<BoxCollider2D>();

		startingGravityScale = myRigidbody2D.gravityScale;
	}

	void Update()
	{
		if (!IsDead) {
			Fly();
			PropelUp();
			PropelDown();
			FlipDirection();
		}
	}

	public void Respawn()
	{
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		myRigidbody2D.isKinematic = false;
		IsDead = false;
	}

	private void Playerdeath()
	{
		if (!IsDead) {
			//if (!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Water"))) {
			//	myRigidbody2D.velocity = new Vector2(0, 0);
			//	myRigidbody2D.isKinematic = true;
			//}
			GameController.instance.TakeLife();
		}
		IsDead = true;
	}

	private void Fly()
	{
		float running = Input.GetAxis("Horizontal");
		Vector2 playerVelocity = new Vector2(running * movementSpeed, myRigidbody2D.velocity.y);
		myRigidbody2D.velocity = playerVelocity;
		moveHorizontaly = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
	}

	private void PropelUp()
	{
		if (Input.GetButtonDown("Jump")) {
			if (myRigidbody2D.velocity.y <= 15) {
				Vector2 boostVelocity = new Vector2(0f, boostSpeed);
				myRigidbody2D.velocity += boostVelocity;
			}
		}
		else if(myRigidbody2D.velocity.y >= 5) {
			myRigidbody2D.AddRelativeForce(-myRigidbody2D.velocity/5);
		}

		if (Input.GetButton("Fire1")) {
			myRigidbody2D.gravityScale = 0;

			if (myRigidbody2D.velocity.y == 0) {
				myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0f);
			}
			else {
				myRigidbody2D.AddRelativeForce(-myRigidbody2D.velocity);
			}
		}
		else {
			myRigidbody2D.gravityScale = startingGravityScale;
		}
	}

	private void PropelDown() {
		if (Input.GetButtonDown("Fire2")) {
			if (myRigidbody2D.velocity.y >= -10) {
				Vector2 droptVelocity = new Vector2(0f, dropSpeed);
				myRigidbody2D.velocity -= droptVelocity;
			}
		}
	}

	private void FlipDirection()
	{
		if (moveHorizontaly) {
			float Direction = Mathf.Sign(myRigidbody2D.velocity.x);
			transform.localScale = (Direction == 1) ? new Vector2(1f, 1f) : new Vector2(-1f, 1f);

			if (Direction == 1) {
				transform.localScale = new Vector2(1f, 1f);
			}
			if (Direction == -1) {
				transform.localScale = new Vector2(-1f, 1f);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy" || myCircleCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazzard"))) {
			Playerdeath();
		}


	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Hazzard") {
			Playerdeath();
		}

		if (collision.gameObject.tag == "Item") {
			print("Item " + collision.gameObject.name);
			//collectedItems.Add(collision.gameObject.name, collision.gameObject);
			GameController.instance.CollectItems(collision.gameObject.name);
			Destroy(collision.gameObject);
		}
	}

	private void SetGameObjectInActive()  //Called from event in Die animation
	{
		gameObject.SetActive(false);
	}
}
