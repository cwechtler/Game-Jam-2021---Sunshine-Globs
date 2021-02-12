using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//[SerializeField] private float horizontalAccelerationSpeed = 5f;
	[SerializeField] private float verticalAccelerationSpeed = 5f;
	[SerializeField] private float boostSpeed = 15f;
	//[SerializeField] private float dropSpeed = 3f;
	[Space]
	[SerializeField] private float maxHorizontalSpeed = 5f;
	[SerializeField] private float maxVerticalSpeed = 5f;
	[SerializeField] private float maxBoostSpeed = 15f;
	//[SerializeField] private float maxDropSpeed = 15f;


	private Rigidbody2D myRigidbody2D;
	private SpriteRenderer spriteRenderer;
	private CircleCollider2D myCircleCollider2D;
	private BoxCollider2D myBoxCollider2D;
	private AudioSource playerAudioSource;

	private float startingGravityScale;
	private bool moveHorizontaly;
	private bool moveVertically;

	private void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		myCircleCollider2D = GetComponent<CircleCollider2D>();
		myBoxCollider2D = GetComponent<BoxCollider2D>();
		playerAudioSource = GetComponent<AudioSource>();

		startingGravityScale = myRigidbody2D.gravityScale;
	}

	private void Update()
	{
		Fly();
		FlipDirection();
	}

	public void Respawn()
	{
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		myRigidbody2D.isKinematic = false;
	}

	private void Fly()
	{
		float flyVertically = Input.GetAxis("Vertical");
		ApplyVerticalVelocity(flyVertically);

		float flyHorizontally = Input.GetAxis("Horizontal");
		ApplyHorizontalVelocity(flyHorizontally);

		BoostUp();
		HoldHorizontal();

		PlayFlightAudio(flyVertically, flyHorizontally);
	}

	private void ApplyHorizontalVelocity(float flyHorizontally)
	{
		Vector2 playerVelocityHorizontal = new Vector2(flyHorizontally * maxHorizontalSpeed, myRigidbody2D.velocity.y);
		myRigidbody2D.velocity = playerVelocityHorizontal;
		moveHorizontaly = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
	}

	private void ApplyVerticalVelocity(float flyUpAndDown)
	{
		if (myRigidbody2D.velocity.y <= maxVerticalSpeed && flyUpAndDown > 0) {
			Vector2 playerVelocity = new Vector2(myRigidbody2D.velocity.x, flyUpAndDown * verticalAccelerationSpeed);
			myRigidbody2D.velocity += playerVelocity * Time.deltaTime;
		}
		else if (myRigidbody2D.velocity.y >= -maxVerticalSpeed && flyUpAndDown < 0) {
			Vector2 playerVelocity = new Vector2(myRigidbody2D.velocity.x, flyUpAndDown * verticalAccelerationSpeed);
			myRigidbody2D.velocity += playerVelocity * Time.deltaTime;
		}
		moveVertically = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;
	}

	private void BoostUp()
	{
		if (Input.GetButtonDown("Jump")) {
			if (myRigidbody2D.velocity.y <= maxBoostSpeed) {
				Vector2 boostVelocity = new Vector2(0f, boostSpeed);

				myRigidbody2D.velocity += boostVelocity;
				SoundManager.instance.PlayBoostClip();
			}
		}
		else if (myRigidbody2D.velocity.y >= 5) {
			myRigidbody2D.AddRelativeForce(-myRigidbody2D.velocity / 5);
		}
	}

	private void HoldHorizontal()
	{
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

	//private void PropelDown() {
	//	if (Input.GetButtonDown("Fire2")) {
	//		if (myRigidbody2D.velocity.y >= -maxDropSpeed) {
	//			Vector2 droptVelocity = new Vector2(0f, dropSpeed);
	//			myRigidbody2D.velocity -= droptVelocity;
	//			SoundManager.instance.PlayDropClip();
	//		}
	//	}
	//}

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

	private void PlayFlightAudio(float flyVertically, float flyHorizontally)
	{
		if (flyHorizontally > 0 || flyHorizontally < 0) {
			if (!playerAudioSource.isPlaying) {
				playerAudioSource.Play();
			}
		}
		else if (flyVertically > 0 || flyVertically < 0) {
			if (!playerAudioSource.isPlaying) {
				playerAudioSource.Play();
			}
		}
		else {
			playerAudioSource.Stop();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{	
		if (collision.gameObject.tag == "Bubble") {
			SoundManager.instance.BubbleImpactClip();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Item") {
			GameController.instance.CollectItems(collision.gameObject.name);
			SoundManager.instance.PlayCollectClip();
			Destroy(collision.gameObject);
		}
	}
}
