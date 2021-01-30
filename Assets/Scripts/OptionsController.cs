using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	[SerializeField] private Slider masterVolumeSlider;
	[SerializeField] private Slider musicVolumeSlider;
	[SerializeField] private Slider sfxVolumeSlider;

	private LevelManager levelManager;
	private SoundManager soundManager;
	
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		soundManager = GameObject.FindObjectOfType<SoundManager>();

		if (PlayerPrefs.HasKey ("master_volume")){
			masterVolumeSlider.value = PlayerPrefsManager.GetMasterVolume ();
		}else {
			masterVolumeSlider.value = -20f;
		}

		if (PlayerPrefs.HasKey("music_volume")) {
			musicVolumeSlider.value = PlayerPrefsManager.GetMusicVolume();
		}
		else {
			musicVolumeSlider.value = 0f;
		}

		if (PlayerPrefs.HasKey("sfx_volume")) {
			sfxVolumeSlider.value = PlayerPrefsManager.GetSFXVolume();
		}
		else {
			sfxVolumeSlider.value = 0f;
		}
	}
	
	void Update () {
		soundManager.ChangeMasterVolume (masterVolumeSlider.value);
		soundManager.ChangeMusicVolume(musicVolumeSlider.value);
		soundManager.ChangeSFXVolume(sfxVolumeSlider.value);
	}
	
	public void SaveAndExit(){
		PlayerPrefsManager.SetMasterVolume (masterVolumeSlider.value);
		PlayerPrefsManager.SetMusicVolume (musicVolumeSlider.value);
		PlayerPrefsManager.SetSFXVolume(sfxVolumeSlider.value);
		levelManager.LoadLevel ("Main Menu");
	}
	
	public void SetDefaults(){
		masterVolumeSlider.value = -20f;
		musicVolumeSlider.value = 0f;
		sfxVolumeSlider.value = 0f;
	}
}
