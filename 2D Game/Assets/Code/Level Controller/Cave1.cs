using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Cave1 : MonoBehaviour
{
	private bool endScene = false;

	public GameObject monster1;
	public GameObject monster2;
	public GameObject monster3;
	public GameObject textBlock;
	public GameObject particle;
	public AudioMixer musicMixer;

	public GameObject skillCard;

    // Start is called before the first frame update
	void Start()
	{
		musicMixer.SetFloat("MusicVolume", Mathf.Log10(0.5f) * 20);
	}

    // Update is called once per frame
	void Update()
	{
		if (monster2 == null && monster1 == null) {
			if (monster3 != null) {
				monster3.SetActive(true);
			}
		}

		if (monster1 == null && monster3 == null && monster2 == null && endScene == false) {
			endScene = true;
			Invoke("activeSkillCard", 1f);
			textBlock.SetActive(true);
			particle.SetActive(true);
			musicMixer.SetFloat("MusicVolume", Mathf.Log10(0.2f) * 20);
			StartCoroutine(attack());
		}  
		//Debug.Log(playerState.state);
	}

	IEnumerator attack() {
		yield return new WaitForSeconds(2f);
		Time.timeScale = 0f;
	}


	public void StaminaRegen() {
		StaminaBar.staminaRegenAmount = 47f;
		skillCard.SetActive(false);
		Time.timeScale = 1f;
	}

	
	public void MaxHealthIncrease() {
		HealthBar.healthMax += 50f;
		skillCard.SetActive(false);
		Time.timeScale = 1f;
	}

	private void activeSkillCard() {
		skillCard.SetActive(true);
	}
}
