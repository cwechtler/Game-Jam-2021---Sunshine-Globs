using UnityEngine;

public class OptionsUnloader : MonoBehaviour
{
	public void Unload() {
		StartCoroutine(LevelManager.instance.UnloadScene(1, "Options Additive Load"));
	}
}
