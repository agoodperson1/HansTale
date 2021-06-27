using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	private float health;
	public static float healthMax = 100;
	private Image barImage;

	// Change Material When Damaged
	public GameObject playerObj;
	public Material damagedMat;
	private Material defaultMat;
	private SpriteRenderer playerSprite;

	public void Start() {
		barImage = transform.Find("Bar").GetComponent<Image>();
		health = healthMax;
		playerSprite = playerObj.GetComponent<SpriteRenderer>();
		defaultMat = playerSprite.material;
	}

	void Update() {
		barImage.fillAmount = GetHealthPercent();
		if (healthMax > 100) {
			Animator extend = GetComponent<Animator>();
			extend.SetBool("Expand", true);
		}
	}

	public float GetHealth() {
		return health;
	}

	public float GetHealthPercent() {
		return health / healthMax;
	}

	public void Damage(float damageAmount) {
		health -= damageAmount;
		playerSprite.material = damagedMat;
		Invoke("resetMaterial", 0.05f);
	}

	public void Heal(float healAmount) {
		health += healAmount;
		if (health > healthMax) {
			health = healthMax;
		}
	}

	public void setHealth(float healthAmount) {
		health = healthAmount;
	}

	private void resetMaterial() {
		playerSprite.material = defaultMat;
	}
}
