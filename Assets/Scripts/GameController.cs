using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
	public static GameController instance = null;

	[SerializeField] private GameObject itemPanel;
	[SerializeField] private GameObject[] items = new GameObject[4];

	public List<string> collectedItems;

	public GameObject playerGO;
	//public GameObject spawnPoint;

	

	private Vector3 spawnPointLocation;
	private bool continueGame = false;

	private void Awake()
	{
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		playerGO = GameObject.FindGameObjectWithTag("Player");
		//spawnPoint = GameObject.Find("Spawn Point");
	}

	public GameObject GetItem(string name) {
		foreach (var item in items) {
			if (item.name == name) {
				return item;
			}
		}
		return null;
	}

	public void CollectItems(string itemName) {
		print("Item collected " + itemName);
		collectedItems.Add(itemName);
		GameCanvasController gameCanvasController = FindObjectOfType<GameCanvasController>();

		gameCanvasController.AddCollectedItem(itemName);

		if (collectedItems.Count == 4) {
			LevelManager.Instance.LoadNextLevel();
			collectedItems.Clear();
		}
		print("List # " + collectedItems.Count);

	}

	public void StartGame()
	{
		PlayerPrefsManager.DeletePlayerPrefsPlayerInfo();
		StartCoroutine(LoadScene(2));
	}

	public void Continue()
	{
		print("Continue");
		continueGame = true;

		//List<string> itemsString = PlayerPrefsManager.GetItems();

		//collectedItems = itemsString;
		print("items " + PlayerPrefsManager.GetItems().Count);

		Vector3 spawnPointLocation = new Vector3(PlayerPrefsManager.GetPlayerSpawnpointX(), PlayerPrefsManager.GetPlayerSpawnpointY(), 0);

		LevelManager.Instance.LoadLevel("Game Level 1");
		//StartCoroutine(LoadScene(2));
	}

	public void SavePlayerInfo() {
		PlayerPrefsManager.SetItems(collectedItems);
		PlayerPrefsManager.SetPlayerSpawnpointX(playerGO.transform.position.x);
		PlayerPrefsManager.SetPlayerSpawnpointY(playerGO.transform.position.y);
	}

	public void NextLevel(int level) {
		StartCoroutine(LoadScene(level));
	}

	private IEnumerator LoadScene(int sceneToLoad) {
		print("Load " + sceneToLoad);
		yield return new WaitForSeconds(.3f);
		SceneManager.LoadScene(sceneToLoad);

		yield return new WaitForSeconds(1);
		playerGO = GameObject.FindGameObjectWithTag("Player");
		if (continueGame) {
			playerGO.transform.position = spawnPointLocation;
			continueGame = false;
		}
		//spawnPoint = GameObject.Find("Spawn Point");
	}

	//private IEnumerator RespawnPlayer(int waitToSpawn)
	//{
	//	yield return new WaitForSeconds(waitToSpawn);
	//	playerGO.transform.position = spawnPoint.transform.position;
	//	playerGO.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
	//	playerGO.gameObject.SetActive(true);
	//	playerGO.GetComponent<Player>().Respawn();
	//	yield return new WaitForSeconds(1);
	//}
}
