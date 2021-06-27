using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnockBack : MonoBehaviour
{
	public HealthBar healthBar;
	public int thrust;

	private float attackTimer = 0.3f;
    private bool attackFlag = true;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackFlag == false) {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0.0f) {
                attackFlag = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player") && attackFlag) {
			attackFlag = false;
			attackTimer = 0.3f;
			Rigidbody2D playerBody = other.GetComponent<Rigidbody2D>();
			Vector2 difference = playerBody.transform.position - transform.position;
			difference = difference.normalized * thrust;
			playerBody.AddForce(difference, ForceMode2D.Impulse);
			healthBar.Damage(5f);	
			
			
			StartCoroutine(knockCoroutine(other));
		}
	}

	private IEnumerator knockCoroutine(Collider2D other) {
		yield return new WaitForSeconds(0.2f);
		if (gameObject.tag == "Arrow") {
			Destroy(gameObject);
		}
	}
}
