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


	private BubbleFactory factoryParent;
	private bool isDead = false;

	Rigidbody2D myRigidbody2D;

	void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();

		// if new insance, set velocity of 1 toward local y-axis
		if (myRigidbody2D.velocity.magnitude == 0) {
			myRigidbody2D.velocity = this.transform.up;
		}

		StartCoroutine(MoveBubble());
	}

	public void InitializeFromFactory(BubbleFactory factoryParent) {
		this.factoryParent = factoryParent;
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
		collisionsAllowed -= 1;
		if (collisionsAllowed <= 0) {
			Destroy(gameObject, 0.5f);
			if (!isDead && factoryParent != null) {
				factoryParent.spawnedBubbles--;
			}
			isDead = true;
		}
	}
}
