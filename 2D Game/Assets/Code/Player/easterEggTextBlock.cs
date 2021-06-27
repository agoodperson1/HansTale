using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easterEggTextBlock : MonoBehaviour
{
    public GameObject skillCard;
    public GameObject questionMark;
    public GameObject boxFly;
    private static bool triggerOnce = true;

    // Start is called before the first frame update
    

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "EasterEggTextBlock") {
            if (triggerOnce) {
                triggerOnce = false;
                Destroy(questionMark);
                Destroy(boxFly);
                skillCard.SetActive(true);

                Invoke("freeze", 1f);
            }
        }
    }

    public void triggerElementalArrow() {
        PlayerMovement.randomArrow = true;
        Time.timeScale = 1f;
        skillCard.SetActive(false);
    }

    private void freeze() {
        Time.timeScale = 0f;
    }
}
