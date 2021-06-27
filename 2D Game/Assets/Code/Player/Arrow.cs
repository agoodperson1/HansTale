using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	void Update() {
      //Destroy(gameObject, 2f);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		/*if (collision.gameObject.tag == "Map" || collision.gameObject.tag == "Monster"){
			Destroy(gameObject);
		} */
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Map") || other.gameObject.CompareTag("Monster")) {
			Destroy(gameObject);
		} 
	}
}
