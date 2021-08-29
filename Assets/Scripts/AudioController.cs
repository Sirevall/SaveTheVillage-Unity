using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixerGroup mixer;

    public AudioSource CreateWarrior;
    public AudioSource CreatePeasant;
    public AudioSource WaveSound;
    public AudioSource Consume;
    public AudioSource CreateWheat;
    public AudioSource BackgroundMusic;
    public AudioSource UISound;

    public AudioClip StartTheme;
    public AudioClip MainTheme;
    public AudioClip Fight;
    public AudioClip StartAttack;
    public AudioClip Click;
    public AudioClip VictorySound;
    public AudioClip FailureSound;

    public void PlayStartTheme()
    {
        BackgroundMusic.clip = StartTheme;
        BackgroundMusic.Play();
        BackgroundMusic.loop = true;
    }
    public void PlayMainTheme()
    {
        BackgroundMusic.clip = MainTheme;
        BackgroundMusic.Play();
        BackgroundMusic.loop = true;
    }
    public void PlayVictorySound()
    {
        BackgroundMusic.clip = VictorySound;
        BackgroundMusic.Play();
        BackgroundMusic.loop = false;
    }
    public void PlayFailureSound()
    {
        BackgroundMusic.clip = FailureSound;
        BackgroundMusic.Play();
        BackgroundMusic.loop = false;
    }
    public void PlayAttackSound()
    {
        WaveSound.clip = StartAttack;
        WaveSound.Play();
    }
    public void PlayFightSound()
    {
        WaveSound.clip = Fight;
        WaveSound.Play();
    }
    public void PlayClickSound()
    {
        UISound.clip = Click;
        UISound.Play();
    }
    public void SwitchSound(bool musicIsPlaying)
    {
        if (musicIsPlaying)
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            mixer.audioMixer.SetFloat("MasterVolume", 0);
        }
    }
}
