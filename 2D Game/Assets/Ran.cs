using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ran : MonoBehaviour
{
    public GameObject Player;
    //public GameObject happy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" ) {
        	//happy.SetActive(true);
            StartCoroutine (Teleport ());
        }
    }

    IEnumerator Teleport() {
        yield return new WaitForSeconds (1);
        //happy.SetActive(false);
        Player.transform.position = new Vector2 (Random.Range(-5.0f, 5f), Random.Range(-5f, 5f));
    }
}
