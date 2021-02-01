using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCanvasController : MonoBehaviour
{
	[SerializeField] private GameObject pausePanel;
	[Space]
	[SerializeField] private TextMeshProUGUI collectionTextPanel;
	[SerializeField] private GameObject collectedPanel;
	[SerializeField] private GameObject[] itemCollectedPrefabs;

	private GameController gameController;
	private LevelManager levelManager;
	private SoundManager soundManager;
	private TextMeshProUGUI collectionText;

	private void Start()
	{
		gameController = GameObject.FindObjectOfType<GameController>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		soundManager = GameObject.FindObjectOfType<SoundManager>();
		collectionText = collectionTextPanel.GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		PauseGame();
	}

	public void AddCollectedItem(string itemName) {

		collectionText.text = GameController.instance.collectedItems.Count + " of 4";

		switch (itemName) {
			case "Red":
				print ("Red");
				GameObject redItemCollected = GameObject.Instantiate(itemCollectedPrefabs[0]);
				redItemCollected.transform.SetParent(collectedPanel.transform, true);
				redItemCollected.transform.localScale = Vector3.one;
				break;
			case "Purple":
				GameObject purpleItemCollected = GameObject.Instantiate(itemCollectedPrefabs[1]);
				purpleItemCollected.transform.SetParent(collectedPanel.transform, true);
				purpleItemCollected.transform.localScale = Vector3.one;
				break;
			case "Orange":
				GameObject orangeItemCollected = GameObject.Instantiate(itemCollectedPrefabs[2]);
				orangeItemCollected.transform.SetParent(collectedPanel.transform, true);
				orangeItemCollected.transform.localScale = Vector3.one;
				break;
			case "Green":
				GameObject greenItemCollected = GameObject.Instantiate(itemCollectedPrefabs[3]);
				greenItemCollected.transform.SetParent(collectedPanel.transform, true);
				greenItemCollected.transform.localScale = Vector3.one;
				break;
			default:
				break;
		}
		print(GameController.instance.collectedItems.Count);
	}

	public void PauseGame()
	{
		if (Input.GetButtonDown("Cancel")) {
			Time.timeScale = 0;
			pausePanel.SetActive(true);
		}
	}

	public void ResumeGame()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1;
	}

	public void Options()
	{
		GameController.instance.SavePlayerInfo();
		levelManager.LoadOptions();
	}

	public void QuitGame() {
		GameController.instance.SavePlayerInfo();
		levelManager.LoadLevel("Main Menu");
	}

}
