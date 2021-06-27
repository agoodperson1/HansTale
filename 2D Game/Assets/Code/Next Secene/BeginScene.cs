using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginScene : MonoBehaviour
{
	public static int prevScene = 0;
	public static int currentScene = 0;

	public virtual void Start() {
		currentScene = SceneManager.GetActiveScene().buildIndex;
	}

	public void LoadScene(int index) {
		prevScene = currentScene;
		SceneManager.LoadScene(index);
	}
}
