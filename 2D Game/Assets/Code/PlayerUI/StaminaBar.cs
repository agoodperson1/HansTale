using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
	private Image barImage;
	public const int Stam_Max = 100;

	private float staminaAmount;
	public static float staminaRegenAmount = 35f;

	void Start() {
		barImage = transform.Find("Bar").GetComponent<Image>();
		staminaAmount = Stam_Max;
	}

	void Update() {

		staminaAmount += staminaRegenAmount * Time.deltaTime;
		barImage.fillAmount = GetStaminaNormalized();
		staminaAmount = Mathf.Clamp(staminaAmount, 0f, Stam_Max);		
	}

	public void SpendStamina(int amount) {
		staminaAmount -= amount;

	}

	float GetStaminaNormalized() {
		return staminaAmount / Stam_Max;
	}

	public bool CanSpendStamina(int amount) {
		return staminaAmount >= amount;
	}
	
}
