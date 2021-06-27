using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdEnding : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public Animator deathTextAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "ThirdEndingTrigger") {
           mainMenuCanvas.SetActive(true);
           deathTextAnimator.SetBool("Word", true);
       }
   }


}
