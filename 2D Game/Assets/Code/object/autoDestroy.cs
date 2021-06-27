using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestroy : MonoBehaviour
{
    public float destroyTimer;          // Timer before the object auto destroys itself
    private float targetDestroyTime = -15f;
    
    // Update is called once per frame
    void Update()
    {
        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0f && destroyTimer > targetDestroyTime) {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        } else if (destroyTimer <= targetDestroyTime) {
            Destroy(gameObject);
        }
    }
}
