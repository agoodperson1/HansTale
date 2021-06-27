using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
	public HealthBar healthBar;
	private Animator deathTextAnimator;
	public GameObject deathCanvas;

    // Start is called before the first frame update
	void Start()
	{
		
	}

    // Update is called once per frame
	void Update()
	{
		if (healthBar.GetHealthPercent() <= 0f) {
			StartCoroutine(DeathCoroutine());
		}
	}

	private IEnumerator DeathCoroutine() {
		yield return new WaitForSeconds(0.5f); 
		//Time.timeScale = 0;
		deathCanvas.SetActive(true);
		deathTextAnimator = GetComponentInChildren<Animator>();
		deathTextAnimator.SetBool("Word", true);
	}

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
