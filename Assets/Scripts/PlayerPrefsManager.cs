using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string MUSIC_VOLUME_KEY = "music_volume";
	const string SFX_VOLUME_KEY = "sfx_volume";
	const string ITEMS_KEY = "items";
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

	public static void SetItems(string items)
	{
		PlayerPrefs.SetString(ITEMS_KEY, items);
	}

	public static string GetItems() {
		return PlayerPrefs.GetString(ITEMS_KEY);
	}

	public static void SetPlayerSpawnpointX(float playerSpawnpointX){
		PlayerPrefs.SetFloat(PLAYER_SPAWNPOINTX_KEY, playerSpawnpointX);
	}

	public static float GetPlayerSpawnpointX()
	{
		return PlayerPrefs.GetFloat(PLAYER_SPAWNPOINTX_KEY);
	}

	public static void SetPlayerSpawnpointY(float playerSpawnpointY)
	{
		PlayerPrefs.SetFloat(PLAYER_SPAWNPOINTY_KEY, playerSpawnpointY);
	}

	public static float GetPlayerSpawnpointY()
	{
		return PlayerPrefs.GetFloat(PLAYER_SPAWNPOINTY_KEY);
	}

	public static void DeleteAllPlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}

	public static void DeletePlayerPrefsPlayerInfo(){
		PlayerPrefs.DeleteKey(ITEMS_KEY);
		PlayerPrefs.DeleteKey(PLAYER_SPAWNPOINTX_KEY);
		PlayerPrefs.DeleteKey(PLAYER_SPAWNPOINTY_KEY);
	}
}
