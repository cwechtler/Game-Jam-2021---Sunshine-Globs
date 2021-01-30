using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
	private GameController gameController;
	private LevelManager levelManager;
	private SoundManager soundManager;

	void Start()
	{
		gameController = GameObject.FindObjectOfType<GameController>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		soundManager = GameObject.FindObjectOfType<SoundManager>();
	}

	public void StartGame() {
		gameController.StartGame();
	}

	public void ContinueGame() {
		gameController.Continue();
	}
}
