using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
	[SerializeField] GameObject continueButton;
	[SerializeField] GameObject fadePanel;

	private Button button;
	private TextMeshProUGUI buttonText;
	private Animator animator;

	private void Start()
	{
		animator = fadePanel.GetComponent<Animator>();

		if (continueButton != null) {
			button = continueButton.GetComponent<Button>();
			buttonText = continueButton.GetComponentInChildren<TextMeshProUGUI>();

			if (PlayerPrefsManager.CanContinue()) {
				button.interactable = true;
				buttonText.color = new Color32(0, 0, 0, 255);
			}
			else {
				button.interactable = false;
				buttonText.color = new Color32(0, 0, 0, 91);
			}
		}
	}

	public void MainMenu() {
		animator.SetBool("FadeOut", true);
		LevelManager.instance.LoadLevel(0, .9f);
	}

	public void StartNewGame() {
		animator.SetBool("FadeOut", true);
		LevelManager.instance.StartNewGame();
	}

	public void ContinueGame() {
		animator.SetBool("FadeOut", true);
		LevelManager.instance.Continue();
	}

	public void Options() {
		animator.SetBool("FadeOut", true);
		LevelManager.instance.LoadLevel(1, .9f);
	}

	public void QuitGame() {
		animator.SetBool("FadeOut", true);
		LevelManager.instance.QuitRequest();
	}
}
