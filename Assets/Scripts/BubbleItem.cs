using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleItem : MonoBehaviour
{
	[SerializeField] private float nextBoostTime = 0.5f;
	[SerializeField] private float minBoostInterval = 2f;
	[SerializeField] private float maxBoostInterval = 4f;
	[SerializeField] private float minBoostSpeed = 5f;
	[SerializeField] private float maxBoostSpeed = 10f;
	[SerializeField] private float moveDirectionDeviation = 0.35f;

	Rigidbody2D myRigidbody2D;

	void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (Time.time > nextBoostTime) {
			BubbleMove();
			SetNewMovementTimer();
		}
	}

	private void SetNewMovementTimer() {
		nextBoostTime += (Random.value * (maxBoostInterval - minBoostInterval)) + minBoostInterval;
	}

	private void BubbleMove() {
		float newSpeed = (Random.value * (maxBoostSpeed - minBoostSpeed)) + minBoostSpeed;
		// boost only if new speed is faster than current speed
		if (newSpeed > myRigidbody2D.velocity.magnitude) {
			float directionDeviation = (Random.value * moveDirectionDeviation * 2) - moveDirectionDeviation;
			float newDirection = Mathf.Atan2(myRigidbody2D.velocity.normalized.y, myRigidbody2D.velocity.normalized.x) + directionDeviation;
			myRigidbody2D.velocity = new Vector2(Mathf.Cos(newDirection) * newSpeed, Mathf.Sin(newDirection) * newSpeed);
		}
		// if stuck or stopped, boost in a random direction
		if (myRigidbody2D.velocity.magnitude < 0.1){
			float newDirection = Random.value * 360f;
			myRigidbody2D.velocity = new Vector2(Mathf.Cos(newDirection) * newSpeed, Mathf.Sin(newDirection) * newSpeed);
		}
	}
}
