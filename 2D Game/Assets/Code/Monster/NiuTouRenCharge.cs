using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiuTouRenCharge : MonoBehaviour
{
	public Transform player;
	private Rigidbody2D rb;
	private EnemyAi movement;
	public float chargeDistance;
	private Animator bullAnimator;
	public GameObject shellAttackRange;
	public GameObject rectAttackRange;

	private float coolDownTime = 2f;
	private float nextFireTime = 2f;
	private bool setOnce = true;
	private bool firstTrigger = true; 		// make sure the first ability is not area attack
	public static bool wield = false;
	public static bool niuAbility = false;

    // Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		bullAnimator = GetComponentInChildren<Animator>();
		movement = GetComponent<EnemyAi>();
	}

    // Update is called once per frame
	void Update()
	{
		
		if (Time.time > nextFireTime) {
			// if (Vector2.Distance(player.position, transform.position) > chargeDistance) {
			if (setOnce) {
				int chargeNum = Random.Range(0, 4);
				wield = false;
				// chargeNum = 0;
				if (chargeNum == 0 && firstTrigger == false) {
					GameObject attRange = Instantiate(shellAttackRange, new Vector3(transform.position.x, transform.position.y-1f, 0), Quaternion.identity);
					// attRange.transform.Position = transform.localPosition;
					Vector2 directionVector = player.position - transform.position;
					attRange.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(directionVector.y,
						directionVector.x) * Mathf.Rad2Deg, Space.Self);
					// setOnce = false;
					rb.velocity = Vector2.zero;
					movement.enabled = false;
					rb.mass = 100;
					wieldWeaponAnim(directionVector);
					StartCoroutine(destroy(attRange, 1f));

				} else if (chargeNum == 1 && firstTrigger == false) {
					GameObject attRange = Instantiate(rectAttackRange, new Vector3(transform.position.x, transform.position.y-1f, 0), Quaternion.identity);
					// attRange.transform.Position = transform.localPosition;
					Vector2 directionVector = player.position - transform.position;
					attRange.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(directionVector.y,
						directionVector.x) * Mathf.Rad2Deg, Space.Self);
					// setOnce = false;
					rb.velocity = Vector2.zero;
					movement.enabled = false;
					rb.mass = 100;
					wieldWeaponAnim(directionVector);
					StartCoroutine(destroy(attRange, 0.5f));
				}else {
					firstTrigger = false;
					Vector2 direction = player.position - transform.position;
					rb.AddForce(direction.normalized * 60, ForceMode2D.Impulse);

					StartCoroutine(Charge());
				}
				

			}
		}

	}

	private void wieldWeaponAnim(Vector2 target) {
		niuAbility = true;
		Vector2 axis = new Vector2(1f, 0f);
		float angle = Vector2.SignedAngle(axis, target);
		if (angle <= 90f && angle <= -90f) {
			bullAnimator.SetBool("chargeRight", true);
		} else {
			bullAnimator.SetBool("chargeLeft", true);
		}
	}

	IEnumerator Charge() {
		setOnce = false;
		yield return null;
		setOnce = true;
		nextFireTime = Time.time + coolDownTime;

	}

	IEnumerator destroy(GameObject obj, float wieldTime) {
		setOnce = false;
		yield return new WaitForSeconds(wieldTime);
		wield = true;
		movement.enabled = true;
		niuAbility = false;
		rb.mass = 3;
		bullAnimator.SetBool("chargeRight", false);
		bullAnimator.SetBool("chargeLeft", false);

		yield return null;
		SpriteRenderer area = obj.GetComponentInChildren<SpriteRenderer>();
		area.enabled = false;
		StartCoroutine(destroyGameObject(obj));
		setOnce = true;
		nextFireTime = Time.time + coolDownTime;
	}

	IEnumerator destroyGameObject(GameObject obj) {
		yield return new WaitForSeconds(1f);
		Destroy(obj);
	}

}
