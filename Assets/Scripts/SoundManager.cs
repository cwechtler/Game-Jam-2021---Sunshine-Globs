using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance = null;
    [Range(.01f, .5f)] [SerializeField] private float fadeInTime = .05f;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource MusicAudioSource;
    [SerializeField] private AudioSource SFXAudioSource;

    [SerializeField] private AudioClip[] music;
    [SerializeField] private AudioClip[] jumpClips;
    [Space]
    [SerializeField] private AudioClip collectClip;
    [SerializeField] private AudioClip buttonClick;
   
    private float audioVolume = 1f;

    void Awake(){
        if (instance != null){
            Destroy(gameObject);
        } else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update(){
        //MusicSelect();
        //VolumeFadeIn(MusicAudioSource);   
    }

    void VolumeFadeIn(AudioSource audioSource) {
        if (audioVolume <= 1f){
            audioVolume += fadeInTime * Time.deltaTime;
            audioSource.volume = audioVolume;
        } else{
            audioVolume = 1f;
        }

        if (audioSource.clip != null){
            if (!audioSource.isPlaying){
                audioSource.Play();
                audioSource.volume = 0f;
                audioVolume = 0f;
            }
        }
    }

    void VolumeFadeOut(AudioSource audioSource) {
        if (audioVolume >= 1f){
            audioVolume -= fadeInTime * Time.deltaTime;
            audioSource.volume = audioVolume;
        } else{
            audioVolume = 0f;
        }

        if (audioSource.volume <= 0f){
            audioSource.Stop();
        }
    }

    //public void MusicSelect() {
    //    switch (LevelManager.Instance.currentScene){
    //        case "Start Menu":
    //            MusicAudioSource.clip = music[0];
    //            break;

    //        case "Level 1":
    //            MusicAudioSource.clip = music[1];
    //            break;

    //        case "Win":
    //            MusicAudioSource.clip = music[2];
    //            break;

    //        case "Lose":
    //            MusicAudioSource.clip = music[3];
    //            break;

    //        default:
    //            break;
    //    }
    //} 

    public void StartAudio(){
        MusicAudioSource.Play();
    }

    public void SetButtonClip(){
        SFXAudioSource.PlayOneShot(buttonClick, .6f);
    }

    public void PlayJump() {
        int clip = Random.Range(0, jumpClips.Length);
        SFXAudioSource.PlayOneShot(jumpClips[clip], .2f);
    }

    public void PlayCollectClip() {
        SFXAudioSource.PlayOneShot(collectClip, .3f);
    }

    public void ChangeMasterVolume(float volume) {
        audioMixer.SetFloat("MasterVolume", volume);
        if (volume == -40f){
            audioMixer.SetFloat("MasterVolume", -80f);
        }
    }

    public void ChangeMusicVolume(float volume){
        audioMixer.SetFloat("MusicVolume", volume);
        if (volume == -40f){
            audioMixer.SetFloat("MusicVolume", -80f);
        }
    }

    public void ChangeSFXVolume(float volume){
        audioMixer.SetFloat("SFXVolume", volume);
        if (volume == -40f){
            audioMixer.SetFloat("SFXVolume", -80f);
        }
    }
}
