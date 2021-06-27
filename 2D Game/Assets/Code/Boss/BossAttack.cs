using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
	
	public GameObject arrowPrefab;
	public GameObject silverArrowPrefab;
	public GameObject iceArrowPrefab;

	public Transform BottomPosition;
	public Transform BottomLeftPosition;
	public Transform BottomRightPosition;
	public Transform LeftPosition;
	public Transform RightPosition;
	public Transform TopPosition;
	public Transform TopRightPosition;
	public Transform TopLeftPosition;

	public Transform player;
	private Animator anim;
	public MonsterHealthBar monsterHealthBar;
	public HealthBar healthBar;

	private bool setOnce = true;

    // Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
	}

    // Update is called once per frame
	void Update()
	{
		Vector2 positionVector = player.transform.position - transform.position;
		if (anim.GetBool("Shoot")) {
			GameObject arrow;
			if (setOnce == true) {
				int arrowNum = Random.Range(0, 3);
				if (arrowNum == 0) {
					arrow = Instantiate (arrowPrefab, getPosition(GetDirection(positionVector)).transform.position, Quaternion.identity);
				} else if (arrowNum == 1) {
					arrow = Instantiate (silverArrowPrefab, getPosition(GetDirection(positionVector)).transform.position, Quaternion.identity);
				} else {
					arrow = Instantiate (iceArrowPrefab, getPosition(GetDirection(positionVector)).transform.position, Quaternion.identity);
				}
			} else {
				int mode = Random.Range(0, 2);
				if (mode == 1) { 			// triple arrow
					arrow = Instantiate (getRandomArrow(), getPosition(GetDirection(positionVector)).transform.position, Quaternion.identity);

					GameObject arrow1 = Instantiate (getRandomArrow(), getPosition(GetDirection(positionVector)).position,
						Quaternion.identity);
					Rigidbody2D arrowBody1 = arrow1.GetComponent<Rigidbody2D>();
					arrowBody1.AddForce(Quaternion.Euler(0, 0, 25) * positionVector.normalized * 10f, ForceMode2D.Impulse);
					arrow1.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(positionVector.y,
						positionVector.x) * Mathf.Rad2Deg + 25);

					GameObject arrow2 = Instantiate (getRandomArrow(), getPosition(GetDirection(positionVector)).position,
						Quaternion.identity);
					Rigidbody2D arrowBody2 = arrow2.GetComponent<Rigidbody2D>();
					arrowBody2.AddForce(Quaternion.Euler(0, 0, -25) * positionVector.normalized * 10f, ForceMode2D.Impulse);
					arrow2.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(positionVector.y,
						positionVector.x) * Mathf.Rad2Deg - 25);
				} else {
					// double shot
					arrow = Instantiate (getRandomArrow(), getPosition(GetDirection(positionVector)).transform.position, Quaternion.identity);
					StartCoroutine(doubleArrowCoroutine(positionVector));
				}
				
			} 
			Rigidbody2D arrowBody = arrow.GetComponent<Rigidbody2D>();
			arrowBody.AddForce(positionVector.normalized * 10f, ForceMode2D.Impulse);
			arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(positionVector.y,
				positionVector.x) * Mathf.Rad2Deg);
			
		}

		if (monsterHealthBar.GetHealthPercent() < 0.8 && setOnce == true) {
			setOnce = false;
			anim.SetBool("ChangePhase", true);
		}

		if (monsterHealthBar.GetHealthPercent() <= 0) {
			Destroy(gameObject);
		}
	}

	private IEnumerator doubleArrowCoroutine(Vector2 positionVector) {
		yield return new WaitForSeconds(0.1f);
		GameObject arrow = Instantiate (getRandomArrow(), getPosition(GetDirection(positionVector)).position,
										 Quaternion.identity);
		Rigidbody2D arrowBody = arrow.GetComponent<Rigidbody2D>();
		arrowBody.AddForce(positionVector.normalized * 10f, ForceMode2D.Impulse);
				//arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(change.x, change.y) * 10f;
		arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(positionVector.y,
			positionVector.x) * Mathf.Rad2Deg);
	}

	
	private GameObject getRandomArrow() {
		int num = Random.Range(0, 2);
		if (num == 0) {
			return silverArrowPrefab;
		} else {
			return iceArrowPrefab;
		}
	}

	void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject.tag == "Player" && anim.GetBool("ChangePhase")) {
			healthBar.Damage(15f);
		}
	}

	// return the position of panel based on change's x and y position
	Transform getPosition(Vector2 direction) {
		if (direction.x == 0 && direction.y == -1)  {
			return BottomPosition;
		}
		if (direction.x == -1 && direction.y == -1)  {
			return BottomLeftPosition;
		}
		if (direction.x == 1 && direction.y == -1)  {
			return BottomRightPosition;
		}
		if (direction.x == -1 && direction.y == 0)  {
			return LeftPosition;
		}
		if (direction.x == 1 && direction.y == 0)  {
			return RightPosition;
		}
		if (direction.x == 0 && direction.y == 1)  {
			return TopPosition;
		}
		if (direction.x == -1 && direction.y == 1)  {
			return TopLeftPosition;
		}
		if (direction.x == 1 && direction.y == 1)  {
			return TopRightPosition;
		}
		return transform;
	}

	Vector2 GetDirection(Vector2 target)
	{
		Vector2 axis = new Vector2(1f, 0f);
		float angle = Vector2.SignedAngle(axis, target);
            //float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

		if (angle <= 30f && angle >= -30f) {
			return new Vector2(1f, 0f);
		} else if (angle > 30f && angle < 60f) {
			return new Vector2(1f, 1f);
		} else if (angle >= 60f && angle <= 120f) {
			return new Vector2(0f, 1f);
		} else if (angle > 120f && angle < 150f) {
			return new Vector2 (-1f, 1f);
		} else if (angle <-30f && angle > -60f) {
			return new Vector2(1f, -1f);
		} else if (angle <= -60f && angle >= -120f) {
			return new Vector2(0f, -1f);
		} else if (angle < -120f && angle > -150f) {
			return new Vector2(-1f, -1f);
		} else {
			return new Vector2(-1f, 0f);
		} 
	}
}
