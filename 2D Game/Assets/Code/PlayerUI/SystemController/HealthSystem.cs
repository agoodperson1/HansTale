using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int health;
    private int healthMax;

    public HealthSystem(int health) {
    	this.healthMax = health;
    	health = healthMax;
    }

    public int GetHealth() {
    	return health;
    }

    public float GetHealthPercent() {
    	return (float) health / healthMax;
    }

    public void Damage(int damageAmount) {
    	health -= damageAmount;
    }

    public void Heal(int healAmount) {
    	health += healAmount;
    	if (health > healthMax) {
    		health = healthMax;
    	}
    }
}
