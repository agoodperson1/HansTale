using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharging : MonoBehaviour
{
	public Transform player;
	Rigidbody2D rb;
	Animator bossAnimator;
	bool exeOnce = true;
	public static bool secondPhase = false;
	public static bool charging = false;
	private bool setOnce = false;

	BossAttack bossAttack;
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

    // Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		bossAnimator = GetComponent<Animator>();
		bossAttack = new BossAttack();
		charging = false;
		secondPhase = false;
	}

    // Update is called once per frame
	void Update()
	{
		if (bossAnimator.GetBool("ChangePhase") == true && exeOnce == true) {
			exeOnce = false;
			StartCoroutine(charge());

		}

		if (secondPhase && setOnce == false) {
			setOnce = true;
			changeSecondPhaseMode();
		}
	}

	private void changeSecondPhaseMode() {
		int mode = Random.Range(0, 3);
		bool chargeOnce = true;
		if (mode == 0) {
			charging = true;
			if (chargeOnce) {
				chargeOnce = false;
				ChargePlayer();
			}
		} else {
			charging = false;
		}
		Invoke("changeSecondPhaseMode", 3f);
	}

	IEnumerator charge() {
		ChargePlayer();
		rb.mass = 100;
		yield return new WaitForSeconds(1.5f);
		ultimate();
		yield return new WaitForSeconds(1f);
		ultimate1();
		rb.mass = 10;
		yield return new WaitForSeconds(2f);

		ChargePlayer();
		rb.mass = 100;
		yield return new WaitForSeconds(1.5f);
		ultimate();
		yield return new WaitForSeconds(1f);
		ultimate1();
		rb.mass = 10;
		yield return new WaitForSeconds(2f);

		ChargePlayer();
		rb.mass = 100;
		yield return new WaitForSeconds(1.5f);
		ultimate();
		yield return new WaitForSeconds(1f);
		ultimate1();
		rb.mass = 10;
		yield return new WaitForSeconds(2f);


		bossAnimator.SetBool("ChangePhase", false);
		secondPhase = true;

	}

	private void ChargePlayer() {
		Vector2 target = new Vector2(player.position.x, player.position.y);
		Vector2 ownPosition = new Vector2(transform.position.x, transform.position.y);
		Vector2 direction = target - ownPosition;
		rb.AddForce(direction * 100, ForceMode2D.Impulse);
	}

	private GameObject getrandomArrow() {
		int arrowNum = Random.Range(0, 2);
			GameObject arrow;
			if (arrowNum == 0) {
				return arrowPrefab;
			} else {
				return silverArrowPrefab;	
			}
	}

	public void ultimate() {
		// bottom position
		Vector2 position = new Vector2(0, -1);
		GameObject arrow1 = Instantiate (getrandomArrow(), new Vector3(BottomPosition.transform.position.x, BottomPosition.transform.position.y, 0), Quaternion.identity);
		Rigidbody2D arrowBody1 = arrow1.GetComponent<Rigidbody2D>();
		arrowBody1.AddForce(Quaternion.Euler(0, 0, 0) * position * 5f, ForceMode2D.Impulse);
		arrow1.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 0);
		GameObject arrow2 = Instantiate (getrandomArrow(), new Vector3(BottomPosition.transform.position.x + 0.5f, BottomPosition.transform.position.y, 0), Quaternion.identity);
		Rigidbody2D arrowBody2 = arrow2.GetComponent<Rigidbody2D>();
		arrowBody2.AddForce(Quaternion.Euler(0, 0, 25) * position * 5f, ForceMode2D.Impulse);
		arrow2.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 25);
		GameObject arrow3 = Instantiate (getrandomArrow(), new Vector3(BottomPosition.transform.position.x - 0.5f, BottomPosition.transform.position.y, 0), Quaternion.identity);
		Rigidbody2D arrowBody3 = arrow3.GetComponent<Rigidbody2D>();
		arrowBody3.AddForce(Quaternion.Euler(0, 0, -25) * position * 5f, ForceMode2D.Impulse);
		arrow3.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg - 25);

		//left position
		position = new Vector2(-1, 0);
		GameObject arrow4 = Instantiate (getrandomArrow(), new Vector3(LeftPosition.transform.position.x, LeftPosition.transform.position.y, 0), Quaternion.identity);
		Rigidbody2D arrowBody4 = arrow4.GetComponent<Rigidbody2D>();
		arrowBody4.AddForce(Quaternion.Euler(0, 0, 0) * position * 5f, ForceMode2D.Impulse);
		arrow4.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 0);
		GameObject arrow5 = Instantiate (getrandomArrow(), new Vector3(LeftPosition.transform.position.x, LeftPosition.transform.position.y - 0.5f, 0), Quaternion.identity);
		Rigidbody2D arrowBody5 = arrow5.GetComponent<Rigidbody2D>();
		arrowBody5.AddForce(Quaternion.Euler(0, 0, 25) * position * 5f, ForceMode2D.Impulse);
		arrow5.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 25);
		GameObject arrow6 = Instantiate (getrandomArrow(), new Vector3(LeftPosition.transform.position.x, LeftPosition.transform.position.y + 0.5f, 0), Quaternion.identity);
		Rigidbody2D arrowBody6 = arrow6.GetComponent<Rigidbody2D>();
		arrowBody6.AddForce(Quaternion.Euler(0, 0, -25) * position * 5f, ForceMode2D.Impulse);
		arrow6.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg - 25);

		// top position
		position = new Vector2(0, 1);
		arrow4 = Instantiate (getrandomArrow(), new Vector3(TopPosition.transform.position.x, TopPosition.transform.position.y, 0), Quaternion.identity);
		arrowBody4 = arrow4.GetComponent<Rigidbody2D>();
		arrowBody4.AddForce(Quaternion.Euler(0, 0, 0) * position * 5f, ForceMode2D.Impulse);
		arrow4.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 0);
		arrow5 = Instantiate (getrandomArrow(), new Vector3(TopPosition.transform.position.x - 0.5f, TopPosition.transform.position.y, 0), Quaternion.identity);
		arrowBody5 = arrow5.GetComponent<Rigidbody2D>();
		arrowBody5.AddForce(Quaternion.Euler(0, 0, 25) * position * 5f, ForceMode2D.Impulse);
		arrow5.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 25);
		arrow6 = Instantiate (getrandomArrow(), new Vector3(TopPosition.transform.position.x + 0.5f, TopPosition.transform.position.y, 0), Quaternion.identity);
		arrowBody6 = arrow6.GetComponent<Rigidbody2D>();
		arrowBody6.AddForce(Quaternion.Euler(0, 0, -25) * position * 5f, ForceMode2D.Impulse);
		arrow6.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg - 25);

		// right position
		position = new Vector2(1, 0);
		arrow4 = Instantiate (getrandomArrow(), new Vector3(RightPosition.transform.position.x, RightPosition.transform.position.y, 0), Quaternion.identity);
		arrowBody4 = arrow4.GetComponent<Rigidbody2D>();
		arrowBody4.AddForce(Quaternion.Euler(0, 0, 0) * position * 5f, ForceMode2D.Impulse);
		arrow4.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 0);
		arrow5 = Instantiate (getrandomArrow(), new Vector3(RightPosition.transform.position.x, RightPosition.transform.position.y + 0.5f, 0), Quaternion.identity);
		arrowBody5 = arrow5.GetComponent<Rigidbody2D>();
		arrowBody5.AddForce(Quaternion.Euler(0, 0, 25) * position * 5f, ForceMode2D.Impulse);
		arrow5.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 25);
		arrow6 = Instantiate (getrandomArrow(), new Vector3(RightPosition.transform.position.x, RightPosition.transform.position.y - 0.5f, 0), Quaternion.identity);
		arrowBody6 = arrow6.GetComponent<Rigidbody2D>();
		arrowBody6.AddForce(Quaternion.Euler(0, 0, -25) * position * 5f, ForceMode2D.Impulse);
		arrow6.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg - 25);
	}

	public void ultimate1() {
		// bottom left position
		Vector2 position = new Vector2(-1, -1);
		GameObject arrow1 = Instantiate (getrandomArrow(), new Vector3(BottomLeftPosition.transform.position.x, BottomLeftPosition.transform.position.y, 0), Quaternion.identity);
		Rigidbody2D arrowBody1 = arrow1.GetComponent<Rigidbody2D>();
		arrowBody1.AddForce(Quaternion.Euler(0, 0, 0) * position * 7f, ForceMode2D.Impulse);
		arrow1.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 0);
		GameObject arrow2 = Instantiate (getrandomArrow(), new Vector3(BottomLeftPosition.transform.position.x + 0.5f, BottomLeftPosition.transform.position.y - 0.5f, 0), Quaternion.identity);
		Rigidbody2D arrowBody2 = arrow2.GetComponent<Rigidbody2D>();
		arrowBody2.AddForce(Quaternion.Euler(0, 0, 25) * position * 7f, ForceMode2D.Impulse);
		arrow2.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 25);
		GameObject arrow3 = Instantiate (getrandomArrow(), new Vector3(BottomLeftPosition.transform.position.x - 0.5f, BottomLeftPosition.transform.position.y + 0.5f, 0), Quaternion.identity);
		Rigidbody2D arrowBody3 = arrow3.GetComponent<Rigidbody2D>();
		arrowBody3.AddForce(Quaternion.Euler(0, 0, -25) * position * 7f, ForceMode2D.Impulse);
		arrow3.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg - 25);

		// bottom right position
		position = new Vector2(1, -1);
		GameObject arrow4 = Instantiate (getrandomArrow(), new Vector3(BottomRightPosition.transform.position.x, BottomRightPosition.transform.position.y, 0), Quaternion.identity);
		Rigidbody2D arrowBody4 = arrow4.GetComponent<Rigidbody2D>();
		arrowBody4.AddForce(Quaternion.Euler(0, 0, 0) * position * 7f, ForceMode2D.Impulse);
		arrow4.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 0);
		GameObject arrow5 = Instantiate (getrandomArrow(), new Vector3(BottomRightPosition.transform.position.x + 0.5f, BottomRightPosition.transform.position.y + 0.5f, 0), Quaternion.identity);
		Rigidbody2D arrowBody5 = arrow5.GetComponent<Rigidbody2D>();
		arrowBody5.AddForce(Quaternion.Euler(0, 0, 25) * position * 7f, ForceMode2D.Impulse);
		arrow5.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 25);
		GameObject arrow6 = Instantiate (getrandomArrow(), new Vector3(BottomRightPosition.transform.position.x - 0.5f, BottomRightPosition.transform.position.y - 0.5f, 0), Quaternion.identity);
		Rigidbody2D arrowBody6 = arrow6.GetComponent<Rigidbody2D>();
		arrowBody6.AddForce(Quaternion.Euler(0, 0, -25) * position * 7f, ForceMode2D.Impulse);
		arrow6.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg - 25);

		// top left position
		position = new Vector2(-1f, 1);
		arrow4 = Instantiate (getrandomArrow(), new Vector3(TopLeftPosition.transform.position.x, TopLeftPosition.transform.position.y, 0), Quaternion.identity);
		arrowBody4 = arrow4.GetComponent<Rigidbody2D>();
		arrowBody4.AddForce(Quaternion.Euler(0, 0, 0) * position * 7f, ForceMode2D.Impulse);
		arrow4.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 0);
		arrow5 = Instantiate (getrandomArrow(), new Vector3(TopLeftPosition.transform.position.x - 0.5f, TopLeftPosition.transform.position.y - 0.5f, 0), Quaternion.identity);
		arrowBody5 = arrow5.GetComponent<Rigidbody2D>();
		arrowBody5.AddForce(Quaternion.Euler(0, 0, 25) * position * 7f, ForceMode2D.Impulse);
		arrow5.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 25);
		arrow6 = Instantiate (getrandomArrow(), new Vector3(TopLeftPosition.transform.position.x + 0.5f, TopLeftPosition.transform.position.y + 0.5f, 0), Quaternion.identity);
		arrowBody6 = arrow6.GetComponent<Rigidbody2D>();
		arrowBody6.AddForce(Quaternion.Euler(0, 0, -25) * position * 7f, ForceMode2D.Impulse);
		arrow6.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg - 25);

		// top right position
		position = new Vector2(1, 1);
		arrow4 = Instantiate (getrandomArrow(), new Vector3(TopRightPosition.transform.position.x, TopRightPosition.transform.position.y, 0), Quaternion.identity);
		arrowBody4 = arrow4.GetComponent<Rigidbody2D>();
		arrowBody4.AddForce(Quaternion.Euler(0, 0, 0) * position * 7f, ForceMode2D.Impulse);
		arrow4.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 0);
		arrow5 = Instantiate (getrandomArrow(), new Vector3(TopRightPosition.transform.position.x - 0.5f, TopRightPosition.transform.position.y + 0.5f, 0), Quaternion.identity);
		arrowBody5 = arrow5.GetComponent<Rigidbody2D>();
		arrowBody5.AddForce(Quaternion.Euler(0, 0, 25) * position * 7f, ForceMode2D.Impulse);
		arrow5.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg + 25);
		arrow6 = Instantiate (getrandomArrow(), new Vector3(TopRightPosition.transform.position.x + 0.5f, TopRightPosition.transform.position.y - 0.5f, 0), Quaternion.identity);
		arrowBody6 = arrow6.GetComponent<Rigidbody2D>();
		arrowBody6.AddForce(Quaternion.Euler(0, 0, -25) * position * 7f, ForceMode2D.Impulse);
		arrow6.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(position.y,
			position.x) * Mathf.Rad2Deg - 25);
	}
}
