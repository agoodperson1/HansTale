using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAnim : MonoBehaviour
{
	private Animator animator;

    // Start is called before the first frame update
	void Start()
	{
		animator = GetComponentInChildren<Animator>();   
	}

    // Update is called once per frame
	void Update()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			animator.SetTrigger("Attack");
		}
	}
}
