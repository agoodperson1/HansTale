using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rand : MonoBehaviour
{
	public GameObject Portal;
    public GameObject Player;
    public GameObject happy;
    public GameObject sad;

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
        	happy.SetActive(true);
            StartCoroutine (Teleport ());
        }
    }

    IEnumerator Teleport() {
        yield return new WaitForSeconds (1);
        happy.SetActive(false);
        var position = new Vector2 (Random.Range(-5f, 7f), Random.Range(-3f, 3f));
        Player.transform.position = position;
        position.y = position.y - .8f;
        //position.x = position.x + .1f;
        sad.transform.position = position;
        sad.SetActive(true);
        yield return new WaitForSeconds (.9f);
        sad.SetActive(false);
    }
}