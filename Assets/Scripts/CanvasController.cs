using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
	[SerializeField] private GameObject continueButton;
	[SerializeField] private GameObject fadePanel;
	[SerializeField] private Button buttonToStartAsSelected;
	[SerializeField] private TextMeshProUGUI versionNumberText;

	private Button button;
	private TextMeshProUGUI buttonText;
	private Animator animator;

	private void Start()
	{
		buttonToStartAsSelected.Select();
		animator = fadePanel.GetComponent<Animator>();

		if (versionNumberText != null) {
			versionNumberText.text = "V " + Application.version;
		}


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
		LevelManager.instance.QuitRequest();
	}
}
