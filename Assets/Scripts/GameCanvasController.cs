using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class GameCanvasController : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera VCinemachineCamera;
	[SerializeField] private GameObject pausePanel;
	[SerializeField] private GameObject pauseButtonsPanel;
	[Space]
	[SerializeField] private TextMeshProUGUI collectionTextPanel;
	[SerializeField] private GameObject collectedPanel;
	[SerializeField] private GameObject[] itemCollectedPrefabs;

	private TextMeshProUGUI collectionText;
	private CanvasGroup canvasGroup;
	private Animator pausePanelAnimator;

	private void Awake()
	{
		GameController.instance.LoadSceneObjects();
		VCinemachineCamera.Follow = GameController.instance.playerGO.transform;
	}

	private void Start()
	{	
		collectionText = collectionTextPanel.GetComponent<TextMeshProUGUI>();
		canvasGroup = pauseButtonsPanel.GetComponent<CanvasGroup>();
		pausePanelAnimator = pauseButtonsPanel.GetComponent<Animator>();

		if (GameController.instance.collectedItems.Count > 0) {
			GameObject[] Items = GameObject.FindGameObjectsWithTag("Item");

			foreach (var collectedItem in GameController.instance.collectedItems) {
				AddCollectedItem(collectedItem);
				foreach (var item in Items) {
					if (item.name == collectedItem) {
						Destroy(item);
					}
				}
			}
		}
	}

	private void Update()
	{
		PauseGame();
	}

	private void InstantiateItem(GameObject prefab) {
		GameObject itemCollected = GameObject.Instantiate(prefab);
		itemCollected.transform.SetParent(collectedPanel.transform, true);
		itemCollected.transform.localScale = Vector3.one;
	}

	private GameObject SelectPrefab(string itemName)
	{
		foreach (var item in itemCollectedPrefabs) {
			if (item.name == itemName + " Item Collected") {
				return item;
			}
		}
		return null;
	}

	public void AddCollectedItem(string itemName) {

		collectionText.text = GameController.instance.collectedItems.Count + " of 4";

		switch (itemName) {
			case "Red":
				InstantiateItem(SelectPrefab(itemName));
				break;
			case "Purple":
				InstantiateItem(SelectPrefab(itemName));
				break;
			case "Orange":
				InstantiateItem(SelectPrefab(itemName));
				break;
			case "Green":
				InstantiateItem(SelectPrefab(itemName));
				break;
			default:
				break;
		}
	}

	public void PauseGame()
	{
		string buttonDown = "Cancel";

		#if UNITY_WEBGL
				buttonDown = "Pause";
		#endif

		if (Input.GetButtonDown(buttonDown) && !GameController.instance.OptionsOverlayOpen) {
			GameController.instance.PauseGame();
			pausePanel.SetActive(true);
			pausePanelAnimator.SetBool("FadeIn", true);

		}
	}

	public void ResumeGame()
	{
		pausePanel.SetActive(false);
		GameController.instance.ResumeGame();
	}

	public void Options()
	{
		GameController.instance.OptionsOverlayOpen = true;
		LevelManager.instance.LoadLevelAdditive("Options Additive Load");
		pausePanelAnimator.SetBool("FadeIn", false);
	}

	public void FadeCanvas() {
		pausePanelAnimator.SetBool("FadeIn", true);
	}

	public void QuitGame() {
		GameController.instance.SavePlayerInfo();
		LevelManager.instance.LoadLevel(0, .9f);
		GameController.instance.ResumeGame();
	}

	public void ExitApplication()
	{
		LevelManager.instance.QuitRequest();
	}
}
