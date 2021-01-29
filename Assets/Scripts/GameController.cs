using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
	public static GameController instance = null;

	[SerializeField] private int lives = 3;
	[SerializeField] private int score = 0;
	[SerializeField] private TextMeshProUGUI livesText = null;
	[SerializeField] private TextMeshProUGUI scoreText = null;
	[Space]
	[SerializeField] private GameObject itemPanel;
	[SerializeField] private GameObject[] items = new GameObject[4];

	public GameObject playerGO;
	public GameObject spawnPoint;

	

	private Vector3 spawnPointLocation;
	private bool continueGame;

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
		spawnPoint = GameObject.Find("Spawn Point");
		//livesText.text = lives.ToString();
		//scoreText.text = score.ToString();
	}

	public GameObject GetItem(string name) {
		foreach (var item in items) {
			if (item.name == name) {
				return item;
			}
		}
		return null;
	}

	public void TakeLife()
	{
		if (lives > 1) {
			lives--;
			livesText.text = lives.ToString();
			StartCoroutine(RespawnPlayer(5));		
		}
		else {
			lives = 0;
			livesText.text = lives.ToString();
			StartCoroutine(LoadScene(0));
		}
	}

	public void AddScore(int addScore)
	{
		score += addScore;
		scoreText.text = score.ToString();
	}

	public void StartGame()
	{
		lives = 3;
		//livesText.text = lives.ToString();

		score = 0;
		//scoreText.text = score.ToString();

		StartCoroutine(LoadScene(1));

	}

	public void Continue()
	{
		continueGame = true;
		lives = PlayerPrefsManager.GetPlayerLives();
		score = PlayerPrefsManager.GetScore();

		Vector3 spawnPointLocation = new Vector3(PlayerPrefsManager.GetPlayerSpawnpointX(), PlayerPrefsManager.GetPlayerSpawnpointY(), 0);
		//spawnPoint.transform.position = spawnPointLocation;

		StartCoroutine(LoadScene(1));

		//livesText.text = lives.ToString();
		//scoreText.text = score.ToString();
	}

	public void NextLevel(int level) {
		StartCoroutine(LoadScene(level));
	}

	private IEnumerator LoadScene(int sceneToLoad) {
		yield return new WaitForSeconds(.3f);
		SceneManager.LoadScene(sceneToLoad);
		yield return new WaitForSeconds(1);
		playerGO = GameObject.FindGameObjectWithTag("Player");
		if (continueGame) {
			playerGO.transform.position = spawnPointLocation;
			continueGame = false;
		}
		spawnPoint = GameObject.Find("Spawn Point");
	}

	private IEnumerator RespawnPlayer(int waitToSpawn)
	{
		yield return new WaitForSeconds(waitToSpawn);
		playerGO.transform.position = spawnPoint.transform.position;
		playerGO.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
		playerGO.gameObject.SetActive(true);
		playerGO.GetComponent<Player>().Respawn();
		yield return new WaitForSeconds(1);
	}
}
