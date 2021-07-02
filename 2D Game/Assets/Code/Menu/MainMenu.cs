using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject fizzObject;
    public GameObject startObject;
    private Animator fizzAnim;
    private Animator beginAnim;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartFizzle", 0.5f);
        fizzAnim = fizzObject.GetComponent<Animator>();
        beginAnim = startObject.GetComponent<Animator>();

        // Initialize or reset all static variable in the game
        PlayerMovement.doubleArrow = false;
        PlayerMovement.tripleShot = false;
        PlayerMovement.arrowNum = 1;

        KnockBack.swordDamage = 2f;
        KnockBack.arrowDamage = 2f;
        KnockBack.lifeSteal = 0f;
        KnockBack.swordThrust = 4f;
        KnockBack.arrowThrust = 2f;

        BossCharging.secondPhase = false;
        BossCharging.charging = false;

        HealthBar.healthMax = 100f;
        StaminaBar.staminaRegenAmount = 35f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartFizzle() {
    	
        if (fizzAnim.enabled == true) {
            float randomTime = Random.Range(3f, 6f);
            StartCoroutine(fizzle());
            Invoke("StartFizzle", randomTime);
        }
        
    }

    IEnumerator fizzle() {
    	fizzObject.SetActive(true);
    	yield return new WaitForSeconds(0.4f);
    	fizzObject.SetActive(false);
    }

    IEnumerator startAnimation() {
        yield return new WaitForSeconds(1.9f);
        SceneManager.LoadScene(2);
    }

    public void StartGame() {
        stopFizz();
        beginAnim.enabled = true;
        StartCoroutine(startAnimation());
    }

    public void Credit() {
    	SceneManager.LoadScene(1);
    }

    public void stopFizz() {
        fizzAnim.enabled = false;
    }

    public void StartFizz() {
        fizzAnim.enabled = true;
        Invoke("StartFizzle", 0.5f);
    }

    public void quitButton() {
        Application.Quit();
    }

}
