using UnityEngine;

public class OptionsUnloader : MonoBehaviour
{
	public void Unload() {
		GameController.instance.OptionsOverlayOpen = false;
		StartCoroutine(LevelManager.instance.UnloadScene(1, "Options Additive Load"));
	}
}
