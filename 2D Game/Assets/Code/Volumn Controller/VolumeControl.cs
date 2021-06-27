using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{

	public AudioMixer musicMixer;
	public AudioMixer effectMixer;
	public Slider musicSlider;
	public Slider effectSlider;

	private static float musicValue = 0.5f;
	private static float effectValue = 0.5f;

	void Start() {
		musicMixer.SetFloat("MusicVolume", Mathf.Log10(musicValue) * 20);
		effectMixer.SetFloat("EffectVolumn", Mathf.Log10(effectValue) * 20);
		musicSlider.value = musicValue;
		effectSlider.value = effectValue;
	}

	void Update() {

	}

	public void SetMusicLevel(float sliderValue) {
		musicValue = sliderValue;
		musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
	}

	public void SetEffectLevel(float sliderValue) {
		effectValue = sliderValue;
		effectMixer.SetFloat("EffectVolumn", Mathf.Log10(sliderValue) * 20);
	}
}
