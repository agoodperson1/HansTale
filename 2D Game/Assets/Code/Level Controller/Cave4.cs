using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Cave4 : MonoBehaviour
{
	private bool endScene = false;

	public GameObject monster1;
	
	public GameObject textBlock;
	public GameObject door;
	public GameObject trigger;
	public AudioMixer musicMixer;

	public GameObject skillCard;
	public GameObject particle;

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
			Invoke("activeSkillCard", 1f);
			textBlock.SetActive(true);
			particle.SetActive(true);
			musicMixer.SetFloat("MusicVolume", Mathf.Log10(0.2f) * 20);

			StartCoroutine(attack());

		}

		if (trigger.activeSelf) {
			door.SetActive(true);
		}

	}

	IEnumerator attack() {
		yield return new WaitForSeconds(2f);
		Time.timeScale = 0f;
	}

	public void SwordDamageIncrease() {
		KnockBack.swordDamage += 1.5f;
		skillCard.SetActive(false);
		Time.timeScale = 1f;
	}

	public void ArrowDamageIncrease() {
		KnockBack.arrowDamage += 1.5f;
		skillCard.SetActive(false);
		Time.timeScale = 1f;
	}

	private void activeSkillCard() {
		skillCard.SetActive(true);
	}
}
