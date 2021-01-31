using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFactory : MonoBehaviour
{
	[SerializeField] private GameObject bubbleParent;
	[Space]
	[SerializeField] private int numberOfBubbles = 3;
	[SerializeField] private float spawnInterval = 2f;
	[SerializeField] private GameObject bubblePrefab;

	public int spawnedBubbles { get; set; } = 0;

	void Start()
	{
		StartCoroutine(GenerateBubbles());
	}

	private IEnumerator GenerateBubbles() {
		for (;;) {
			if (spawnedBubbles < numberOfBubbles) {
				GameObject bubbleInstance = GameObject.Instantiate(bubblePrefab, this.transform.position, this.transform.rotation);
				bubbleInstance.transform.SetParent(bubbleParent.transform);

				Bubble bubbleReference = bubbleInstance.GetComponent<Bubble>();
				bubbleReference.InitializeFromFactory(this);

				spawnedBubbles++;
			}
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
