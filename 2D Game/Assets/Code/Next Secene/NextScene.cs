using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;


public class NextScene : MonoBehaviour
{
	public string content;
	public int SceneNumber;
	public int ending;
	public int bools = 0;
	public GameObject nextTrigger;
	public GameObject trigger2;
	private BeginScene sceneController;
	public GameObject questionMark;
	private bool pop = false;
	private bool open = false;


	void Start() {
		sceneController = GameObject.FindGameObjectWithTag("Controller").GetComponent<BeginScene>();
	}

	void Update() {
		if (pop) {
			if (Input.GetKeyDown("e")) {
				if (open) {
					pop = false;
					open = false;
					Animator popAnim = GameObject.FindGameObjectWithTag("PopUp").GetComponent<Animator>();
					popAnim.SetBool("pop", false);
					StartCoroutine(close(popAnim));
					bools = 1;
				} else {
					Destroy(questionMark);
					open = true;
					PopUpSystem popSystem = GameObject.FindGameObjectWithTag("Controller").GetComponent<PopUpSystem>();
					popSystem.PopUp(content);
				}
			}
		} 
	}


    // Update is called once per frame
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "TextBlock" || collision.tag == "NPC") {
			pop = true;
		}
		if (collision.tag == "NextScene") {
			SceneManager.LoadScene(SceneNumber);
		}
		if (collision.tag == "House1") {
			sceneController.LoadScene(3);
		}
		if (collision.tag == "House2") {
			sceneController.LoadScene(4);
		}
		
		if (collision.tag == "House2Level2") {
			sceneController.LoadScene(5);
		}
		if (collision.tag == "Beginning") {
			sceneController.LoadScene(2);
		}
		if (collision.tag == "CaveEntrence") {
			SceneManager.LoadScene(12);
		}
		if (collision.tag == "Second ending")
		{
			SceneManager.LoadScene(ending);
		}

		if (collision.name == "left") {
			sceneController.LoadScene(20);
		}
		if (collision.name == "right") {
			sceneController.LoadScene(21);
		}
		
		if (collision.name == "2nd") {
			sceneController.LoadScene(19);
		}

		if (collision.tag == "EndBeginning") {
			sceneController.LoadScene(18);
		}

		if (collision.name == "EasterEgg trigger") {
			sceneController.LoadScene(22);
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "TextBlock" || collision.tag == "NPC") {
			pop = true;
			open = false;
			Animator popAnim = GameObject.FindGameObjectWithTag("PopUp").GetComponent<Animator>();
			popAnim.SetBool("pop", false);
			StartCoroutine(close(popAnim));
		}

		// if (collision.tag == "NPC") {
		// 	Animator popAnim = GameObject.FindGameObjectWithTag("PopUp").GetComponent<Animator>();
		// 	//popAnim.ResetTrigger("pop");
		// 	popAnim.SetBool("pop", false);
		// 	StartCoroutine(close(popAnim));
		// }
	} 

	IEnumerator close(Animator popAnim) {
		popAnim.SetBool("close", true);
		yield return new WaitForSeconds(0.5f);
		popAnim.SetBool("close", false);
		nextTrigger.SetActive(true);
		trigger2.SetActive(true);
	}

	public void MainMenu() {
		sceneController.LoadScene(0);
	}
}
