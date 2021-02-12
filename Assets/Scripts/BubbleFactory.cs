using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFactory : MonoBehaviour
{
	[SerializeField] private GameObject bubbleAimDirection;
	[SerializeField] private GameObject growStartLocation;
	[Space]
	[SerializeField] private int numberOfBubbles = 3;
	[SerializeField] private float spawnInterval = 2f;
	[SerializeField] private GameObject bubblePrefab;

	public int spawnedBubbles { get; set; } = 0;

	private void Awake()
	{
		growStartLocation.SetActive(false);
	}

	void Start()
	{
		StartCoroutine(GenerateBubbles());
	}

	private IEnumerator GenerateBubbles() {
		for (;;) {
			if (spawnedBubbles < numberOfBubbles) {
				GameObject bubbleInstance = GameObject.Instantiate(bubblePrefab, this.transform.position, bubbleAimDirection.transform.rotation);
				bubbleInstance.transform.SetParent(this.transform);

				Bubble bubbleReference = bubbleInstance.GetComponent<Bubble>();
				bubbleReference.InitializeFromFactory(this, bubbleAimDirection);

				spawnedBubbles++;
			}
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
