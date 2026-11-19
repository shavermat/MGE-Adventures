using UnityEngine;
using UnityEngine.UI;

public class Soundvolume : MonoBehaviour
{

   private Scrollbar  soundVolume;
   public Sounds sounds;
    void Start()
    {
        soundVolume = GetComponent<Scrollbar>();

        soundVolume.value = PlayerPrefs.GetFloat("sound_volume", 0.5f);
        
        soundVolume.onValueChanged.AddListener(SaveScrollbarValue);
    }

    // Update is called once per frame
    void SaveScrollbarValue(float value)
    {
        PlayerPrefs.SetFloat("sound_volume", value);
        PlayerPrefs.SetInt("sound_changed", 1);
        PlayerPrefs.Save();
         sounds.UpdateAudioVolume();

    }
    void OnDestroy()
    {     
    if (soundVolume != null)
    {
        soundVolume.onValueChanged.RemoveListener(SaveScrollbarValue);
    }
    }  
}
