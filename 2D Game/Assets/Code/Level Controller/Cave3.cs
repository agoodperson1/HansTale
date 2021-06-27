using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Cave3 : MonoBehaviour
{
    private bool endScene = false;

    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    public GameObject monster4;
    public GameObject textBlock;
    public GameObject player;
    public AudioMixer musicMixer;

    public GameObject skillCard;
    public GameObject particle;

    private Vector3 slimePosition;
    private bool spawn1 = false;
    // Start is called before the first frame update
    void Start()
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(0.5f) * 20);
    }

    // Update is called once per frame
    void Update()
    {   
        if (monster1 != null) {
            slimePosition = monster1.transform.position;
        }

        if (monster1 == null) {
            if (spawn1 == false && monster3 != null && monster2 != null) {
                spawn1 = true;
                monster2.transform.position = slimePosition;
                monster3.transform.position = slimePosition;
                monster4.transform.position = slimePosition;
                monster2.SetActive(true);
                monster3.SetActive(true);
                monster4.SetActive(true);
            }   
        }

        if (monster1 == null && monster3 == null && monster2 == null && monster4 == null && endScene == false) {
            endScene = true;
            Invoke("activeSkillCard", 1f);
            textBlock.SetActive(true);
            particle.SetActive(true);
            musicMixer.SetFloat("MusicVolume", Mathf.Log10(0.2f) * 20);
            StartCoroutine(attack());
        }  
        //Debug.Log(playerState.state);
    }

    IEnumerator attack() {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
    }

    public void DoubleArrow() {
        KnockBack.arrowDamage *= 0.55f;
        PlayerMovement.doubleArrow = true;
        PlayerMovement.arrowNum += 1;
        Time.timeScale = 1f;
        skillCard.SetActive(false);
    }

    public void TripleShot() {
        KnockBack.arrowDamage *= 0.4f;
        PlayerMovement.tripleShot = true;
        Time.timeScale = 1f;
        skillCard.SetActive(false);
    }

    private void activeSkillCard() {
        skillCard.SetActive(true);
    }
}
