using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
   public Animator transition;

   public float transitionTime = 1f;

   public int SceneNumber;
       // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0)) {

        	LoadNextLevel();
        }
    }

    public void LoadNextLevel() {
    	StartCoroutine(LoadLevel(SceneNumber));
    }

    IEnumerator LoadLevel(int levelIndex) {
    	transition.SetTrigger("Start");

    	yield return new WaitForSeconds(transitionTime);

    	SceneManager.LoadScene(levelIndex);

    }
}
