using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glitch_stop : MonoBehaviour
{
    public GameObject nextTrigger;
    public GameObject nextTrigger2;
    public GameObject nextTrigger3;
    public GameObject nextTrigger4;
    public GameObject nextTrigger5;
    public GameObject nextTrigger6;
    public GameObject nextTrigger7;
    public GameObject nextTrigger8;
    public GameObject music;
    public static int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (count == 1)
        {
            nextTrigger.SetActive(false);
            nextTrigger2.SetActive(false);
            nextTrigger3.SetActive(false);
            nextTrigger4.SetActive(false);
            nextTrigger5.SetActive(false);
            nextTrigger6.SetActive(false);
            nextTrigger7.SetActive(false);
            nextTrigger8.SetActive(false);
            music.SetActive(false);
        }
    }

	// Update is called once per frame
	void Update()
	{
	    StartCoroutine(close());
	}

	IEnumerator close()
    { 
        yield return new WaitForSeconds(.14f);
        Destroy(nextTrigger);
        yield return new WaitForSeconds(.13f);
        Destroy(nextTrigger2);
        yield return new WaitForSeconds(.12f);
        Destroy(nextTrigger3);
        yield return new WaitForSeconds(.15f);
        Destroy(nextTrigger4);
        yield return new WaitForSeconds(.16f);
        nextTrigger5.SetActive(false);
        yield return new WaitForSeconds(.11f);
        nextTrigger6.SetActive(false);
        yield return new WaitForSeconds(.1f);
        nextTrigger7.SetActive(false);
        yield return new WaitForSeconds(.16f);
        nextTrigger8.SetActive(false);
        music.SetActive(false);
        count = 1;
    }
}
