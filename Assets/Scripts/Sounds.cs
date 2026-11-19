using UnityEngine;

public class Sounds : MonoBehaviour
{

    private AudioSource audioSrs => GetComponent<AudioSource>();

    void Start()
    {
        UpdateAudioVolume();
      
        audioSrs.Play();
    }
   public void PlaySound(AudioClip audio)
    {
        audioSrs.PlayOneShot(audio);
    }
    public void UpdateAudioVolume()
    {
        audioSrs.volume = PlayerPrefs.GetFloat("sound_volume", 0.5f);
    }
}
