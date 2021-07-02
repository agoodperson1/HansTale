using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

	public float speed;
	private Rigidbody2D playerBody;
	private Vector3 change;						// how much a player's position should change
	private Vector3 lastFacingDirection;

	private Animator playerAnimator;
	public State state;
	private float rollSpeed;
	private bool spamCheck = false;
	private bool swordMode = true;
	private bool playMusic = false;
	private float targetTimer = 0.99f;
	private bool inAttackArea = false;  	// variable for niutouren area attack
	private bool inDeathWater = false;

	public Transform BottomPosition;
	public Transform BottomLeftPosition;
	public Transform BottomRightPosition;
	public Transform LeftPosition;
	public Transform RightPosition;
	public Transform TopPosition;
	public Transform TopRightPosition;
	public Transform TopLeftPosition;

	public GameObject arrowPrefab;
	public GameObject silverArrowPrefab;
	public GameObject iceArrowPrefab;

	// public SpriteRenderer weaponPortrait;
	public Image weaponPortrait;
	public Sprite Sword;
	public Sprite Bow;

	public StaminaBar staminBar;
	public HealthBar healthBar;

	public AudioSource deathMusic;
	public AudioSource sceneMusic;

	public static bool doubleArrow = false;
	public static bool tripleShot = false;
	public static bool dodgeState = false;
	public static int arrowNum = 1;
	public static bool randomArrow = false;

	public enum State {
		Normal,
		Dodge,
		Attack,
		Shoot,
		Death,
		Freeze,
	}

	void Awake() {
		state = State.Normal;
	}

	// Start is called before the first frame update
	void Start()
	{
		state = State.Normal;
		playerAnimator = GetComponent<Animator>();
		playerBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (change.x != 0 || change.y != 0) {
			lastFacingDirection = change;
		}

		if (Input.GetMouseButtonDown(1)) {
			swordMode = !swordMode;					// flip the state of swordMode
			if (!swordMode) {
				weaponPortrait.sprite = Bow;
			} else {
				weaponPortrait.sprite = Sword;
			}
		}


		switch (state) {
			case State.Normal:
			playerAnimator.SetBool("Freeze", false);
			change = Vector3.zero;							// reset variable each tick
			change.x = Input.GetAxisRaw("Horizontal");		// get input axis
			change.y = Input.GetAxisRaw("Vertical");
			dodgeState = false;
			UpdateAnimation();
			Dodge();
			Attack();
			Shoot();
			Death();
			break;

			

			case State.Attack:
			Attacking();
			break;

			case State.Shoot:
			Shooting();
			break;

			case State.Death:
			Die();
			break;

			case State.Dodge:
			
			Rolling();
			
			break;

			case State.Freeze:
			playerAnimator.SetBool("Freeze", true);
			break;
		}

		// if (inAttackArea) {
		// 	targetTimer -= Time.deltaTime;
		// 	if (targetTimer <= 0.0f) {
		// 		checkArea();
		// 	}
		// }

		if (inAttackArea && NiuTouRenCharge.wield == true) {
			healthBar.Damage(30f);
			inAttackArea = false;
			NiuTouRenCharge.wield = false;
		}

		if (inDeathWater) {
			healthBar.Damage(0.1f);
		}
	}

	void FixedUpdate() {
		if (state == State.Dodge) {
			
		}
	}

	// Method that updates animation based on change variable
	void UpdateAnimation() {

		// if change is not reset, that means player is moving
		if (change != Vector3.zero) {
			MoveCharacter();
			playerAnimator.SetFloat("MoveX", change.x);
			playerAnimator.SetFloat("MoveY", change.y);
			playerAnimator.SetBool("Moving", true);
		} else {
			playerAnimator.SetBool("Moving", false);
		}

	}


	// Method that will transform player's position based on input speed and key
	void MoveCharacter() {

		playerBody.MovePosition(
			transform.position + change * speed * Time.fixedDeltaTime

			);
	}



	// method that will determine if the player press the space key and initiate
	// the dodge action. If space is pressed, it will change to Dodge state and
	// execute according animations
	void Dodge() {
		if (Input.GetKeyDown(KeyCode.Space) && spamCheck == false) {
			if (staminBar.CanSpendStamina(30)) {
				state = State.Dodge;
				rollSpeed = 10f;
				staminBar.SpendStamina(30);
				dodgeState = true;
			}
		}
	}

	// Method that calculates how much a player slides
	void Rolling() {
		rollSpeed -= rollSpeed * Time.deltaTime;
		
		playerBody.velocity = change.normalized * rollSpeed;
		if (rollSpeed < 8) {
			playerBody.velocity = Vector2.zero;
			state = State.Normal;
		}
		StartCoroutine(RollCoroutine());

	}
	private IEnumerator RollCoroutine() {
		playerAnimator.SetBool("Roll", true);
		
		yield return null;
		playerAnimator.SetBool("Roll", false);
		spamCheck = true;
		yield return new WaitForSeconds(0.5f);

		
		spamCheck = false;
		
	}





	// method that determine if Key J is pressed for player to attack
	void Attack() {
		if (Input.GetMouseButtonDown(0) && swordMode == true) {
			if (staminBar.CanSpendStamina(25)) {
				playerBody.velocity = Vector2.zero;
				staminBar.SpendStamina(25);
				state = State.Attack;
			}
		}
	}

	void Attacking() {
		StartCoroutine(AttackCoroutine());
	}


	private IEnumerator AttackCoroutine() {
		playerAnimator.SetBool("Attack", true);
		playerAnimator.SetFloat("MoveX", lastFacingDirection.x);
		playerAnimator.SetFloat("MoveY", lastFacingDirection.y);
		yield return null;
		playerAnimator.SetBool("Attack", false);
		yield return new WaitForSeconds(0.2f);
		state = State.Normal;
	}


	// Method that determine if the player is going to shoot arrows
	void Shoot() {
		if (Input.GetMouseButtonDown(0) && swordMode == false) {
			if (staminBar.CanSpendStamina(35)) {
				Vector3 mousePos = Input.mousePosition;
				Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
				
				mousePos.x -= playerPos.x;
				mousePos.y -= playerPos.y;
				
				playerAnimator.SetFloat("MoveX", mousePos.x);
				playerAnimator.SetFloat("MoveY", mousePos.y);
				state = State.Shoot;
				
				
				
				
				if (tripleShot) {
					GameObject arrow = Instantiate (getArrow(), getPosition(GetDirection(new Vector2(mousePos.x, mousePos.y))).position,
					Quaternion.identity);
					// print(GetDirection(new Vector2(mousePos.x, mousePos.y)));
					Rigidbody2D arrowBody = arrow.GetComponent<Rigidbody2D>();
					arrowBody.AddForce(mousePos.normalized * 10f, ForceMode2D.Impulse);
					//arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(change.x, change.y) * 10f;
					arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(mousePos.y,
						mousePos.x) * Mathf.Rad2Deg);
					GameObject arrow1 = Instantiate (getArrow(), getPosition(GetDirection(new Vector2(mousePos.x, mousePos.y))).position,
								Quaternion.identity);
					Rigidbody2D arrowBody1 = arrow1.GetComponent<Rigidbody2D>();
					arrowBody1.AddForce(Quaternion.Euler(0, 0, 25) * mousePos.normalized * 10f, ForceMode2D.Impulse);
					arrow1.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(mousePos.y,
						mousePos.x) * Mathf.Rad2Deg + 25);

					GameObject arrow2 = Instantiate (getArrow(), getPosition(GetDirection(new Vector2(mousePos.x, mousePos.y))).position,
						Quaternion.identity);
					Rigidbody2D arrowBody2 = arrow2.GetComponent<Rigidbody2D>();
					arrowBody2.AddForce(Quaternion.Euler(0, 0, -25) * mousePos.normalized * 10f, ForceMode2D.Impulse);
					arrow2.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(mousePos.y,
						mousePos.x) * Mathf.Rad2Deg - 25);
					
					StartCoroutine(tripleShotTrigger(mousePos));
					
				} else if (doubleArrow) {
					
					GameObject arrow = Instantiate (getArrow(), getPosition(GetDirection(new Vector2(mousePos.x, mousePos.y))).position,
					Quaternion.identity);
					// print(GetDirection(new Vector2(mousePos.x, mousePos.y)));
					Rigidbody2D arrowBody = arrow.GetComponent<Rigidbody2D>();
					arrowBody.AddForce(mousePos.normalized * 10f, ForceMode2D.Impulse);
					//arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(change.x, change.y) * 10f;
					arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(mousePos.y,
						mousePos.x) * Mathf.Rad2Deg);

					StartCoroutine(doubleArrowCoroutine(mousePos));
					
				} else {
					GameObject arrow = Instantiate (getArrow(), getPosition(GetDirection(new Vector2(mousePos.x, mousePos.y))).position,
					Quaternion.identity);
					// print(GetDirection(new Vector2(mousePos.x, mousePos.y)));
					Rigidbody2D arrowBody = arrow.GetComponent<Rigidbody2D>();
					arrowBody.AddForce(mousePos.normalized * 10f, ForceMode2D.Impulse);
					//arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(change.x, change.y) * 10f;
					arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(mousePos.y,
						mousePos.x) * Mathf.Rad2Deg);
				}
				
				staminBar.SpendStamina(35);
				
			}
		}
	}

	void Shooting() {
		StartCoroutine(ShootCoroutine());
	}

	private IEnumerator tripleShotTrigger(Vector3 mousePos) {
		for (int x = 1; x < arrowNum; x++) {
			yield return new WaitForSeconds(0.07f);
			GameObject arrow = Instantiate (getArrow(), getPosition(GetDirection(new Vector2(mousePos.x, mousePos.y))).position,
					Quaternion.identity);
			// print(GetDirection(new Vector2(mousePos.x, mousePos.y)));
			Rigidbody2D arrowBody = arrow.GetComponent<Rigidbody2D>();
			arrowBody.AddForce(mousePos.normalized * 10f, ForceMode2D.Impulse);
			//arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(change.x, change.y) * 10f;
			arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(mousePos.y,
				mousePos.x) * Mathf.Rad2Deg);
			GameObject arrow1 = Instantiate (getArrow(), getPosition(GetDirection(new Vector2(mousePos.x, mousePos.y))).position,
						Quaternion.identity);
			Rigidbody2D arrowBody1 = arrow1.GetComponent<Rigidbody2D>();
			arrowBody1.AddForce(Quaternion.Euler(0, 0, 25) * mousePos.normalized * 10f, ForceMode2D.Impulse);
			arrow1.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(mousePos.y,
				mousePos.x) * Mathf.Rad2Deg + 25);

			GameObject arrow2 = Instantiate (getArrow(), getPosition(GetDirection(new Vector2(mousePos.x, mousePos.y))).position,
				Quaternion.identity);
			Rigidbody2D arrowBody2 = arrow2.GetComponent<Rigidbody2D>();
			arrowBody2.AddForce(Quaternion.Euler(0, 0, -25) * mousePos.normalized * 10f, ForceMode2D.Impulse);
			arrow2.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(mousePos.y,
				mousePos.x) * Mathf.Rad2Deg - 25);
		}
	}

	private IEnumerator doubleArrowCoroutine(Vector3 mousePos) {
		for (int x = 1; x < arrowNum; x++) {
			yield return new WaitForSeconds(0.07f);
			GameObject arrow = Instantiate (getArrow(), getPosition(GetDirection(new Vector2(mousePos.x, mousePos.y))).position,
				Quaternion.identity);
			Rigidbody2D arrowBody = arrow.GetComponent<Rigidbody2D>();
			arrowBody.AddForce(mousePos.normalized * 10f, ForceMode2D.Impulse);
					//arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(change.x, change.y) * 10f;
			arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(mousePos.y,
				mousePos.x) * Mathf.Rad2Deg);
		}
	}

	private IEnumerator ShootCoroutine() {
		playerAnimator.SetBool("Shoot", true);
		yield return null;
		playerAnimator.SetBool("Shoot", false);

		yield return new WaitForSeconds(0.2f);
		state = State.Normal;
	}

	private GameObject getArrow() {
		if (randomArrow) {
			int arrowNum = Random.Range(0, 10);
			if (arrowNum == 0) {
				return iceArrowPrefab;
			} else if (arrowNum == 1 || arrowNum == 2) {
				return silverArrowPrefab;
			} else {
				return arrowPrefab;
			}
		} else {
			return arrowPrefab;			
		}
	}

	void Death() {
		if (healthBar.GetHealthPercent() <= 0f) {
			state = State.Death;
		}
	}

	void Die() {
		sceneMusic.Stop();
		Vector2 target = GetDirection(change);
		playerAnimator.SetFloat("MoveX", target.x);
		playerAnimator.SetFloat("MoveY", target.y);
		if (playMusic == false) {
			deathMusic.Play();
			playMusic = true;
		}
		playerAnimator.SetBool("Dead", true);
	}

	

	// return the position of panel based on change's x and y position
	Transform getPosition() {
		if (lastFacingDirection.x == 0 && lastFacingDirection.y == -1)  {
			return BottomPosition;
		}
		if (lastFacingDirection.x == -1 && lastFacingDirection.y == -1)  {
			return BottomLeftPosition;
		}
		if (lastFacingDirection.x == 1 && lastFacingDirection.y == -1)  {
			return BottomRightPosition;
		}
		if (lastFacingDirection.x == -1 && lastFacingDirection.y == 0)  {
			return LeftPosition;
		}
		if (lastFacingDirection.x == 1 && lastFacingDirection.y == 0)  {
			return RightPosition;
		}
		if (lastFacingDirection.x == 0 && lastFacingDirection.y == 1)  {
			return TopPosition;
		}
		if (lastFacingDirection.x == -1 && lastFacingDirection.y == 1)  {
			return TopLeftPosition;
		}
		if (lastFacingDirection.x == 1 && lastFacingDirection.y == 1)  {
			return TopRightPosition;
		}
		return transform;
	}

	// Vector2 GetDirection(Vector2 target)
	// {
	// 	Vector2 axis = new Vector2(1f, 0f);
	// 	float angle = Vector2.SignedAngle(axis, target);

	// 	if (angle <= 45f && angle >= -45f)
	// 	{
	// 		return new Vector2(1f, 0f);
	// 	}
	// 	else if (angle > 45f && angle <= 135f)
	// 	{
	// 		return new Vector2(0f, 1f);
	// 	}
	// 	else if (angle < -45f && angle >= -135f)
	// 	{
	// 		return new Vector2(0f, -1f);
	// 	}
	// 	else
	// 	{
	// 		return new Vector2(-1f, 0f);
	// 	}
	// }

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

	Transform getPosition(Vector2 target) {
		if (target.x == 0 && target.y == -1)  {
			return BottomPosition;
		}
		if (target.x == -1 && target.y == -1)  {
			return BottomLeftPosition;
		}
		if (target.x == 1 && target.y == -1)  {
			return BottomRightPosition;
		}
		if (target.x == -1 && target.y == 0)  {
			return LeftPosition;
		}
		if (target.x == 1 && target.y == 0)  {
			return RightPosition;
		}
		if (target.x == 0 && target.y == 1)  {
			return TopPosition;
		}
		if (target.x == -1 && target.y == 1)  {
			return TopLeftPosition;
		}
		if (target.x == 1 && target.y == 1)  {
			return TopRightPosition;
		}
		return transform;
	}


	private void OnTriggerStay2D(Collider2D other) {
		

		
	}

	private void checkArea() {
		if (inAttackArea) {
			healthBar.Damage(30f);
			inAttackArea = false;
		}
		targetTimer = 0.99f;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "DeathHole") {
			healthBar.setHealth(0);
		}

		if (other.tag == "areaAttack") {
			inAttackArea = true;
		}

		if (other.tag == "DeathWater") {
			inDeathWater = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "areaAttack") {
			inAttackArea = false;
			NiuTouRenCharge.wield = false;
		}

		if (other.tag == "DeathWater") {
			inDeathWater = false;
		}
	}

	public void setDisableDodge(bool set) {
		spamCheck = set;
	}


}
