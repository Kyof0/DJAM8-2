using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
   public Slider musicSlider, SFXSlider;
   public void ToggleMusic()
   {
    AudioManager.Instance.ToggleMusic();
   }

   public void ToggleSFX()
   {
    AudioManager.Instance.ToggleSFX();
   }

   public void musicVolume()
   {
        AudioManager.Instance.musicVolume(musicSlider.value);
   }

   public void SFXVolume()
   {
        AudioManager.Instance.SFXVolume(SFXSlider.value);
   }
}
