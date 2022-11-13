using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource _audio;
    [SerializeField] AudioClip ShootSound,KillSound,SmallBlast,fallingSound,GameOver;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        _audio = GetComponent<AudioSource>();
    }

   public void PlayShotSound()
    {
        _audio.PlayOneShot(ShootSound);
        _audio.volume = 0.3f;
    }

    public void PlayKillSound()
    {
        _audio.PlayOneShot(KillSound);
        _audio.volume = 1f;
    }

    public void PlaySmallBlastSound()
    {
        _audio.PlayOneShot(SmallBlast);
    }

    public void PlayFallingSound()
    {
        _audio.PlayOneShot(fallingSound);
    }
   
    public void PlayGameOverSound()
    {
        _audio.PlayOneShot(GameOver);
    }
}
