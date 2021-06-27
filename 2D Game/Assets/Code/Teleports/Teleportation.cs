using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
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
            sad.SetActive(true);
        }
    }

    IEnumerator Teleport() {
        yield return new WaitForSeconds (1);
        happy.SetActive(false);
        Player.transform.position = new Vector2 (Portal.transform.position.x, Portal.transform.position.y - 1);
        sad.SetActive(false);
    }
}