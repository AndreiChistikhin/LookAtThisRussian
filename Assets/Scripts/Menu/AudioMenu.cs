using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    AudioSource menuSong;
    float musicVolume = 1f;

    private void Start()
    {
        PlayerPrefs.SetFloat("MusicIsOn", 1);
        PlayerPrefs.SetFloat("MusicVolume",musicVolume);
        PlayerPrefs.Save();
        menuSong = GameObject.FindObjectOfType<AudioMenu>().GetComponent<AudioSource>();
        menuSong.Play();
    }

    private void Update()
    {
        menuSong.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    //Adjust volume
    public void UpdateVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    //Turn the music off/on
    public void SoundChange()
    {
        AudioSource song = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        if (song.isPlaying)
        {
            song.Pause();
            PlayerPrefs.SetFloat("MusicIsOn", 0);
            Debug.Log(PlayerPrefs.GetFloat("MusciIsOn"));
        }
        else
        {
            song.UnPause();
            PlayerPrefs.SetFloat("MusicIsOn", 1);
        }
            
    }
}
