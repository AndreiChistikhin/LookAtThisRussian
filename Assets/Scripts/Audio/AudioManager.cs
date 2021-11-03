using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;



public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds;
    [SerializeField]
    Slider musciVolumeSlider;
    [SerializeField]
    Toggle musicSwitcher;

    private bool winIsPlaying;
    private bool soundWasTurnedOff;
    private int songNumber;

    private Timer musicSwitcherTimer;


    void Awake()
    {
        foreach (Sound s in sounds)
        {
          s.source= gameObject.AddComponent<AudioSource>();
          s.source.clip = s.clip;
        }
        //Play random song
        songNumber = UnityEngine.Random.Range(3,12);
        Play("Song"+songNumber);
        musciVolumeSlider.value= PlayerPrefs.GetFloat("MusicVolume");
        
        if (PlayerPrefs.GetFloat("MusicIsOn") == 0)
        {
            soundWasTurnedOff = true;
            musicSwitcher.isOn=false;
        }
        musicSwitcherTimer = gameObject.AddComponent<Timer>();
        musicSwitcherTimer.Duration = 1;
        musicSwitcherTimer.Run();
    }

    public void Play(string name) 
    {
        if (name == "Win")
        {
            foreach (Sound sound in sounds)
            {
                winIsPlaying = true;
                sound.source.Stop();
            }     
        }

        Sound s=Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
     private void Update()
      {
        foreach (Sound s in sounds)
        {
            s.source.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
        //Play next song
        if (!sounds[songNumber].source.isPlaying&& PlayerPrefs.GetFloat("MusicIsOn") == 1&&winIsPlaying==false)
          {
              songNumber++;
              if (songNumber != 11)
              {
                  Play("Song" + songNumber);
              }
              else
              {
                  songNumber = 3;
                  Play("Song" + songNumber);
              }
          }
        if (PlayerPrefs.GetFloat("MusicIsOn") == 0)
        {
            foreach (Sound s in sounds)
            {
                s.source.Stop();
            } 
        }
        if (musicSwitcherTimer.Finished)
        {
            soundWasTurnedOff = false;
        }
      }

    //Turn the music off/on 
    public void MusicTrigger()
    {
        if (PlayerPrefs.GetFloat("MusicIsOn")==1)
        {
            
            PlayerPrefs.SetFloat("MusicIsOn", 0);  
            
        }  
        else if(!soundWasTurnedOff)
        {
            songNumber = UnityEngine.Random.Range(3, 11);
            PlayerPrefs.SetFloat("MusicIsOn", 1);
            Play("Song" + songNumber);
        }
    }

    public void VolumeChange(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume",volume);
        PlayerPrefs.Save();
    }
}
