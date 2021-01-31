using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public static LevelManager Instance;
	public string currentScene;

	private bool button;

	private void Awake(){
		if (Instance == null){
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else if (Instance != this){
			Destroy(gameObject);
		}
	}

	void Start(){
		currentScene = SceneManager.GetActiveScene().name;
	}

	private void Update(){
		if (SceneManager.GetActiveScene().name != currentScene) {
			currentScene = SceneManager.GetActiveScene().name;
		}

		if (Input.GetButtonDown("Submit") && currentScene != "Level 1")
		{
			button = true;
			LoadlevelbyCurrentScene();
		}
	}

	public void LoadlevelbyCurrentScene(){
		switch (currentScene){
			case "Start Menu":
				LoadLevel("Level 1");
				break;
			case "Lose":
				LoadLevel("Start Menu");
				break;
			case "Win":
				LoadLevel("Start Menu");
				break;
			default:
				break;
				
		}
	}

	public void LoadOptions() {
		LoadLevel("Options");
	}

	public void LoadLevel (string name){
		Debug.Log("Level load requested for: " + name);
		SceneManager.LoadScene(name);
		if (button)
		{
			SoundManager.instance.SetButtonClip();
			button = false;
		}

	}
	
	public void QuitRequest () {
		Debug.Log("Level Quit Request");
		
		Application.Quit();
	}
	
	public void LoadNextLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		SoundManager.instance.SetButtonClip();
		currentScene = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name;
	}
}
