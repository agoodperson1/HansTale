using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealthBar : MonoBehaviour
{
	private float health;
	public float healthMax;
	public GameObject rain;
	public GameObject rainMusic;

	// Change Material When Damaged
	public GameObject monsterObj;
	public Material damagedMat;
	private Material defaultMat;
	private SpriteRenderer monsterSprite;
	

	public void Start() {
		health = healthMax;
		monsterSprite = monsterObj.GetComponent<SpriteRenderer>();
		defaultMat = monsterSprite.material;
	}

	void Update() {
		transform.Find("Bar").localScale = new Vector3(GetHealthPercent(), 1);
		if (GetHealthPercent() < 0.5f) {
			rain.SetActive(true);
			rainMusic.SetActive(true);
		}
	}

	public float GetHealth() {
		return health;
	}

	public float GetHealthPercent() {
		return health / healthMax;
	}

	public void Damage(float damageAmount) {
		if (health - damageAmount >= 0) {
			health -= damageAmount;
			monsterSprite.material = damagedMat;
			Invoke("resetMaterial", 0.05f);
		} else {
			health = 0;
		}
		
	}

	private void resetMaterial() {
		monsterSprite.material = defaultMat;
	}
}
