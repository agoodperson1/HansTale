using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FinalLevel : MonoBehaviour
{
	public GameObject monster1;
	public GameObject trigger;
	public GameObject textBlock;
	public ParticleSystem particle;
	public ParticleSystem particle2;
	public AudioMixer musicMixer;

	private bool endScene = false;

    // Start is called before the first frame update
	void Start()
	{
		musicMixer.SetFloat("MusicVolume", Mathf.Log10(0.5f) * 20);
	}

    // Update is called once per frame
	void Update()
	{
		if (monster1 == null && endScene == false) {
			endScene = true;
			if (trigger.activeSelf) {
				particle.Play();
				particle2.Play();
			}
			textBlock.SetActive(true);
			musicMixer.SetFloat("MusicVolume", Mathf.Log10(0.2f) * 20);

		}
	}
}
