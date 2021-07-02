using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
	public static float swordThrust = 4f;
	public static float arrowThrust = 3f;
	private Rigidbody2D enemy;
	private bool knock;
	private MonsterHealthBar enemyHealth;
	private HealthBar healthBar;
	private PlayerMovement playerMovementSpeed;
	private EnemyAi enemySpeed;
	private bool setOnce = true;

	public static float swordDamage = 2f;
	public static float arrowDamage = 2f;
	public static float lifeSteal = 0f;

	void Update() {
		// Debug.Log(swordDamage);
		// Debug.Log(arrowDamage);
	} 

	void Start() {
		healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
		playerMovementSpeed = GameObject.Find("Player").GetComponent<PlayerMovement>();
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log(gameObject.transform.parent.gameObject.tag);
		if (other.gameObject.CompareTag("Monster") && (gameObject.tag == "Arrow" || 
			gameObject.tag == "Weapon" || gameObject.tag == "silverArrow" || gameObject.tag == "iceArrow")) {
			
			if (gameObject.tag == "Arrow" || gameObject.tag == "silverArrow" || gameObject.tag == "iceArrow") {
				gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
				
			}

			enemyHealth = other.transform.Find("MonsterHealthBar").GetComponent<MonsterHealthBar>();
			AudioSource enemySound = other.GetComponent<AudioSource>();
			enemySpeed = other.transform.GetComponent<EnemyAi>();

			if (enemyHealth != null) {
				enemySound.Play();
				if (gameObject.tag == "Arrow" || gameObject.tag == "silverArrow" || gameObject.tag == "iceArrow") {
					if (gameObject.tag == "Arrow") {
						enemyHealth.Damage(arrowDamage);
					} else if (gameObject.tag == "silverArrow") {
						enemyHealth.Damage(arrowDamage * 2);
					} else {
						enemyHealth.Damage(arrowDamage / 2);
						float enemyCurrentSpeed = enemySpeed.speed;
						Debug.Log(enemyCurrentSpeed);	
						StartCoroutine(resetEnemyMovementSpeed(enemyCurrentSpeed));
					}
					
					Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
					if (enemy != null) {
						Vector2 difference = enemy.transform.position - transform.position;
						difference = difference.normalized * arrowThrust;
						//other.gameObject.GetComponent<Pathfinding.AIPath>().enabled = false;

						enemy.AddForce(difference, ForceMode2D.Impulse);
						StartCoroutine(knockCoroutine(other));
					}		
				}

				if (gameObject.tag == "Weapon") {
					enemyHealth.Damage(swordDamage);
					
					healthBar.Heal(lifeSteal * swordDamage);
					
					Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
					if (enemy != null) {
						Vector2 difference = enemy.transform.position - transform.position;
						difference = difference.normalized * swordThrust;
						//other.gameObject.GetComponent<Pathfinding.AIPath>().enabled = false;

						enemy.AddForce(difference, ForceMode2D.Impulse);
						StartCoroutine(knockCoroutine(other));
					}		
				}
			}
			

			// Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
			// if (enemy != null) {
			// 	Vector2 difference = enemy.transform.position - transform.position;
			// 	difference = difference.normalized * thrust;
			// 	//other.gameObject.GetComponent<Pathfinding.AIPath>().enabled = false;

			// 	enemy.AddForce(difference, ForceMode2D.Impulse);
			// 	StartCoroutine(knockCoroutine(other));
			// }		

		} 

		IEnumerator resetEnemyMovementSpeed(float enemyCurrentSpeed) {
			if (setOnce) {
				setOnce = false;
				enemySpeed.speed = enemySpeed.speed / 2;
				yield return new WaitForSeconds(1.5f);
				enemySpeed.speed = enemyCurrentSpeed;
				setOnce = true;
			}
		}

		if (other.gameObject.CompareTag("Map") && (gameObject.tag == "Arrow" || gameObject.tag == "silverArrow" || gameObject.tag == "iceArrow")) {
			Destroy(gameObject);
		} 

		if (other.gameObject.CompareTag("Player") && (gameObject.tag == "Arrow" || gameObject.tag == "silverArrow" || gameObject.tag == "iceArrow")) {
			if (gameObject.tag == "Arrow") {
				healthBar.Damage(5f);
				Destroy(gameObject);
			} else if (gameObject.tag == "silverArrow") {
				healthBar.Damage(10f);
				Destroy(gameObject);
			} else {
				healthBar.Damage(4f);
				if (BossCharging.secondPhase == false) {
					playerMovementSpeed.setDisableDodge(true);
				}
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
				StartCoroutine(slowDownCoroutine());
			}
		

		}

		IEnumerator slowDownCoroutine() {
			playerMovementSpeed.speed = 1f;
			yield return new WaitForSeconds(2f);
			playerMovementSpeed.setDisableDodge(false);
			playerMovementSpeed.speed = 2.5f;
			yield return new WaitForSeconds(1f);
			Destroy(gameObject);
		}

		if (other.gameObject.CompareTag("Boss") && (gameObject.tag == "Arrow" || gameObject.tag == "Weapon"
					 || gameObject.tag == "silverArrow" || gameObject.tag == "iceArrow")) {

			
			if (gameObject.tag == "Arrow" || gameObject.tag == "silverArrow" || gameObject.tag == "iceArrow") {
				gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
			}

			enemyHealth = GameObject.Find("MonsterHealthBar").GetComponent<MonsterHealthBar>();
			AudioSource enemySound = other.GetComponent<AudioSource>();

			if (enemyHealth != null) {
				enemySound.Play();

				if (gameObject.tag == "Arrow" || gameObject.tag == "silverArrow" || gameObject.tag == "iceArrow") {
					
					if (gameObject.tag == "Arrow") {
						enemyHealth.Damage(arrowDamage);
					} else if (gameObject.tag == "silverArrow") {
						enemyHealth.Damage(arrowDamage * 2);
					} else {
						enemyHealth.Damage(arrowDamage / 2);
						float enemyCurrentSpeed = enemySpeed.speed;
						StartCoroutine(resetEnemyMovementSpeed(enemyCurrentSpeed));
					}

					Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
					if (enemy != null) {
						Vector2 difference = enemy.transform.position - transform.position;
						difference = difference.normalized * arrowThrust;
					//other.gameObject.GetComponent<Pathfinding.AIPath>().enabled = false;

						enemy.AddForce(difference, ForceMode2D.Impulse);
						StartCoroutine(knockCoroutine(other));
					}		
				}

				if (gameObject.tag == "Weapon") {
					enemyHealth.Damage(swordDamage);
					
					healthBar.Heal(swordDamage * lifeSteal);
					
					Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
					if (enemy != null) {
						Vector2 difference = enemy.transform.position - transform.position;
						difference = difference.normalized * swordThrust;
					//other.gameObject.GetComponent<Pathfinding.AIPath>().enabled = false;

						enemy.AddForce(difference, ForceMode2D.Impulse);
						StartCoroutine(knockCoroutine(other));
					}		
				}
			}


			// Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
			// if (enemy != null) {
			// 	Vector2 difference = enemy.transform.position - transform.position;
			// 	difference = difference.normalized * thrust;
			// 	//other.gameObject.GetComponent<Pathfinding.AIPath>().enabled = false;

			// 	enemy.AddForce(difference, ForceMode2D.Impulse);
			// 	StartCoroutine(knockCoroutine(other));
			// }		
			
		}

		
	}

	private IEnumerator knockCoroutine(Collider2D other) {
		yield return new WaitForSeconds(2f);
		if (other != null) {
			//other.gameObject.GetComponent<Pathfinding.AIPath>().enabled = true; 
		}
		if (gameObject.tag == "Arrow") {
			Destroy(gameObject);
		}
	}


}
