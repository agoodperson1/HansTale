using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
	public float speed;
	private bool rolling;

    // Start is called before the first frame update
	void Start()
	{
		StartCoroutine(roll());
	}

    // Update is called once per frame
	void Update()
	{
		if (rolling) {
			transform.Translate(Vector2.up * Time.deltaTime * speed);
			if (transform.position.y > 105) {
				SceneManager.LoadScene(0);
			}
		}
	}


	IEnumerator roll() {
		yield return new WaitForSeconds(4f);
		rolling = true;
	}
}
