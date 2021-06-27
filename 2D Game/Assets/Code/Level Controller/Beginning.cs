using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginning : MonoBehaviour
{
	public GameObject skillCard;
	public GameObject questionMark;
	public ParticleSystem BoxFly;

	private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
    	if (other.tag == "Player" && flag == false) {
    		flag = true;
    		skillCard.SetActive(true);
    		Destroy(questionMark);
    		StartCoroutine(freeze());
    	}
    }

    IEnumerator freeze() {
		yield return new WaitForSeconds(1f);
		BoxFly.Stop();
		Time.timeScale = 0f;
	}

	public void SwordDamageIncrease() {
		KnockBack.swordDamage += 0.5f;
		skillCard.SetActive(false);
		Time.timeScale = 1f;
	}

	public void ArrowDamageIncrease() {
		KnockBack.arrowDamage += 0.5f;
		skillCard.SetActive(false);
		Time.timeScale = 1f;
	}
}
