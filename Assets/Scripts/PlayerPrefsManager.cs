using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string MUSIC_VOLUME_KEY = "music_volume";
	const string SFX_VOLUME_KEY = "sfx_volume";
	const string SCORE_KEY = "score";
	const string PLAYER_LIVES_KEY = "player_lives";
	const string PLAYER_SPAWNPOINTX_KEY = "player_spawnpointX";
	const string PLAYER_SPAWNPOINTY_KEY = "player_spawnpointY";


	public static void SetMasterVolume(float volume) {
		if (volume >= -40f && volume <= 1.000001f)
		{
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError("Master volume out of range");
		}
	}

	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}

	public static void SetMusicVolume(float volume){
		if (volume >= -40f && volume <= 1.000001f)
		{
			PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
		} else{
			Debug.LogError("Music volume out of range");
		}
	}

	public static float GetMusicVolume(){
		return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
	}

	public static void SetSFXVolume(float volume){
		if (volume >= -40f && volume <= 1.000001f){
			PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
		} else{
			Debug.LogError("SFX volume out of range");
		}
	}

	public static float GetSFXVolume(){
		return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
	}

	public static void SetScore(int score)
	{
		PlayerPrefs.SetInt(SCORE_KEY, score);
	}

	public static int GetScore() {
		return PlayerPrefs.GetInt(SCORE_KEY);
	}

	public static void SetPlayerLives(int playerLives){
		PlayerPrefs.SetInt(PLAYER_LIVES_KEY, playerLives);
	}

	public static int GetPlayerLives(){
		return PlayerPrefs.GetInt(PLAYER_LIVES_KEY);
	}

	public static void SetPlayerSpawnpointX(int playerSpawnpointX){
		PlayerPrefs.SetInt(PLAYER_SPAWNPOINTX_KEY, playerSpawnpointX);
	}

	public static int GetPlayerSpawnpointX()
	{
		return PlayerPrefs.GetInt(PLAYER_SPAWNPOINTX_KEY);
	}

	public static void SetPlayerSpawnpointY(int playerSpawnpointY)
	{
		PlayerPrefs.SetInt(PLAYER_SPAWNPOINTY_KEY, playerSpawnpointY);
	}

	public static int GetPlayerSpawnpointY()
	{
		return PlayerPrefs.GetInt(PLAYER_SPAWNPOINTY_KEY);
	}

	public void DeleteAllPlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}

	public void DeletePlayerPrefsMusicKey(){
		PlayerPrefs.DeleteKey("music_choice");
	}
}
