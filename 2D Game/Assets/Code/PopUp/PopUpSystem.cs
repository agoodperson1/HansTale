using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PopUpSystem : MonoBehaviour
{
	public GameObject popUpBox;
	public Animator popUpAnim;
	public TMP_Text popUpText;

	public GameObject nextTrigger;
	public GameObject nextTrigger2;
	public ParticleSystem BoxFly;

	private int nextScene;

	private void Start()
	{
		nextScene = SceneManager.GetActiveScene().buildIndex + 1;
	}

	public void PopUp(string text) {
		popUpBox.SetActive(true);
		popUpText.text = text;
		popUpAnim.SetBool("pop", true);
		BoxFly.Stop();
	}

	public void Close() {
		popUpAnim.SetBool("pop", false);
		StartCoroutine(close());
	}

	IEnumerator close() {
		popUpAnim.SetBool("close", true);
		yield return new WaitForSeconds(0.5f);
		popUpAnim.SetBool("close", false);
	}

}
