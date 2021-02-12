using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
	[SerializeField] private float minBoostInterval = 2f;
	[SerializeField] private float maxBoostInterval = 4f;
	[SerializeField] private float minBoostSpeed = 5f;
	[SerializeField] private float maxBoostSpeed = 10f;
	[SerializeField] private float moveDirectionDeviation = 0.35f;
	[SerializeField] private int collisionsAllowed = 6;
	[Space]
	[SerializeField] private AnimationClip growAnimationClip;


	private BubbleFactory factoryParent;
	private GameObject bubbleAimDirection;
	private bool isDead = false;
	private Rigidbody2D myRigidbody2D;
	private AudioSource audioSource;

	void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();

		// if new insance, set velocity of 1 toward local y-axis
		if (myRigidbody2D.velocity.magnitude == 0) {
			myRigidbody2D.velocity = bubbleAimDirection.transform.up;
		}
		StartCoroutine(RotateBubble(growAnimationClip.length));
		StartCoroutine(MoveBubble());
	}

	public void InitializeFromFactory(BubbleFactory _factoryParent, GameObject _bubbleAimDirection)
	{
		this.factoryParent = _factoryParent;
		this.bubbleAimDirection = _bubbleAimDirection;
	}

	public IEnumerator RotateBubble(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		this.transform.rotation = Quaternion.identity;
	}

	private IEnumerator MoveBubble() {
		for (;;) {
			// boost only if new speed is faster than current speed
			float newSpeed = (Random.value * (maxBoostSpeed - minBoostSpeed)) + minBoostSpeed;
			if (newSpeed > myRigidbody2D.velocity.magnitude) {
				float directionDeviation = (Random.value * moveDirectionDeviation * 2) - moveDirectionDeviation;
				float newDirection = Mathf.Atan2(myRigidbody2D.velocity.normalized.y, myRigidbody2D.velocity.normalized.x) + directionDeviation;
				myRigidbody2D.velocity = new Vector2(Mathf.Cos(newDirection) * newSpeed, Mathf.Sin(newDirection) * newSpeed);
			}

			float nextBoostTime = (Random.value * (maxBoostInterval - minBoostInterval)) + minBoostInterval;
			yield return new WaitForSeconds(nextBoostTime);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		collisionsAllowed --;
		if (collisionsAllowed == 0) {
			SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
			spriteRenderer.enabled = false;
			circleCollider2D.enabled = false;

			AudioClip pop = SoundManager.instance.BubblePop();
			audioSource.PlayOneShot(pop);

			if (!isDead && factoryParent != null) {
				factoryParent.spawnedBubbles--;	
			}

			isDead = true;
			Destroy(gameObject, pop.length);
		}
	}
}
