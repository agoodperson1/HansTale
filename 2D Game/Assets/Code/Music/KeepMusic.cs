using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMusic : MonoBehaviour
{
    void Awake () {
    	GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
    	print(objs.Length);
    	if (objs.Length > 1 && objs.Length < 3) {
    		Destroy(objs[objs.Length - 1].gameObject);
    	} else {
    		DontDestroyOnLoad(this.gameObject);
    	}
    }
}
