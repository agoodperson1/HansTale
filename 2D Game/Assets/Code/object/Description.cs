using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Description : MonoBehaviour
{
	public static int mirrorBool = 1;
	public static string item = "apple";
	public string content;
	public GameObject nextTrigger;
	public GameObject fizz;
	public GameObject boxFly;
	private BeginScene sceneController;
	private bool pop = false;
	private bool open = false;
	public GameObject ending;


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
					if (mirrorBool == 1 && item == "mirro") {
		ending.SetActive(true);
		boxFly.SetActive(true);
		fizz.SetActive(true);
		mirrorBool = 0;
		item = "stop";
	}
				} else {
					open = true;
					PopUpSystem popSystem = GameObject.FindGameObjectWithTag("Controller").GetComponent<PopUpSystem>();
					popSystem.PopUp(content);

				}
			}
		}
	}


    // Update is called once per frame
	private void OnTriggerEnter2D(Collider2D collision) {
		//house 2
		if (collision.name == "NPC") {
			content  = "Once there’s a legend that says every 3000 years, there will be a person chosen by fate to sacrifice for the people." + 
			"He is permitted to enter the sacred cave, and nobody will see them again. How frightful!";
		}
		if (collision.name == "NPC Ending") {
			content  = "I heard another Hero will show up soon, the long peace century will come again";
		}
		if (collision.name == "Table") {
			content  = "A table made from a wood of a crashed ship long time ago";
		} else if (collision.name == "Bow") {
			content = "The imitation of the bow from the only person who came back from the cave alive. The original bow can turn arrow into many elements";
		} else if (collision.name == "Swords") {
			content = "The imitation of the Swords from the only person who came back from the cave alive. " +
				"In the history, the left sword has no special effect but can cut through everthing and the right sword is dull but has element slash";
		} else if (collision.name == "Spear") {
			content = "There's no information on the spears, some say it's the third weapon of the hero who left the cave alive";
		} else if (collision.name == "Chairs") {
			content = "Sturdy chair that's made from the remains of a broken ship";
		} else if (collision.name == "Angel") {
			content = "The God who set the rule of sacrifice";
		} else if (collision.name == "Devil") {
			content = "Was defeated by the Gods in the past";
		} else if (collision.name == "Evil_Plate") {
			content = "The plate of a organization who worthship the devil, since devil disappeared, this plate is rare to find";
		} else if (collision.name == "Holy_Plate") {
			content = "The plate of a organization who worthship the Gods, the main theme of the organization is to find and protect the chosen one " +
				"before he or she enter the holy cave";
		} else if (collision.name == "Blind") {
			content = "Unknown material, but can block strong lights. Say to be made from devil's fur";
		} else if (collision.name == "Evil_Banner") {
			content = "The banner of a organization who worthship the devil, since devil disappeared, this banner is rare to find";
		} else if (collision.name == "Holy_Banner") {
			content = "The banner of a organization who worthship the Gods, the holy banner is say to be able to summon angel";
		} else if (collision.name == "Pot") {
			content = "A regular pot with weird yellow liquid in it. Sometime it creates a yellow and green glow at night";
		}

		//house 1
		if (collision.name == "NPC2") {
			content  = "The sacred cave is sealed off by a mystical power that nobody can breakthrough. Only the chosen one is allowed. But who will want to be selected?";
		}
		if (collision.name == "NPC2 Ending") {
			content  = "I have been thinking about drinking the red potion but I don't know what will happen to me. The merchant who sold the potion to me said it came from the devil but didn't devil disappeared long time ago? " + 
			"I'm gonna leave a note here in case anything happened to me";
		}
		if (collision.name == "Table1")
		{
			content = "A table made from the tree house, if the table is near the tree house then it will combine with the tree again";
		}
		else if (collision.name == "Chair1")
		{
			content = "The imitation of the bow from the only person who came back from the cave alive. The original bow can turn arrow into many elements";
		}
		else if (collision.name == "Drawer")
		{
			content = "This cabinet is found at a holy pond that a fairy lives. The intention of the " +
				"first owner who threw the cabinet down the pond is unknown";
		}
		else if (collision.name == "Plant")
		{
			content = "Special plant that glows green light. It use to grow in desert area but because it's scorpian's favor food, it went extinct";
		}
		else if (collision.name == "Piano")
		{
			content = "The piano appeared from a bottomless pit that disappeared long time ago. " +
				"Before the piano appears, people use to hear music and bounce sound from the pit";
		}
		else if (collision.name == "Cabinet")
		{
			content = "This drawer contains all the document of the adventure the owner went through in the past";
		}
		else if (collision.name == "Candle")
		{
			content = "The light is not produced by fire but the fireflies that eats candle";
		}
		else if (collision.name == "Cooking_Range")
		{
			content = "A fire slime live down there and will only produce heat when they eat coat";
		}
		else if (collision.name == "Fountain")
		{
			content = "A magical sink that will produce water by collecting water from the air then the water will disappear in the bottomless pit";
		}
		else if (collision.name == "Map")
		{
			content = "The map shows the world map, the world use to only have one land but after the war everything changed";
		}
		else if (collision.name == "Single_Plant")
		{
			content = "A normal plant, when the firefly finish it's job, they will sleep in the plant ball";
		}
		else if (collision.name == "Potion")
		{
			content = "The red potion gives the person life changing truth and unsettling life. " +
				"The blue potion doesn't do anything to the person. You're not going to drink it because it's probably not real.";
		}

		//house 2 second floor
		if (collision.name == "Bed")
		{
			content = "A nice and soft bed, remember to wake up or else you might never be able to";
		}
		else if (collision.name == "Mirror")
		{
			content = "?!&%牛%hs头@a！）##人」「";
		}
		else if (collision.name == "Pot1")
		{
			content = "Smell like wine in there";
		}
		else if (collision.name == "Pot2")
		{
			content = "Smell like something is rotten in there, like rotten flesh?";
		}
		else if (collision.name == "Pot3")
		{
			content = "This pot is empty now, what's in there before?";
		}
		else if (collision.name == "Blind2")
		{
			content = "This curtain will never get dirty, some say it's made out of angel's feather";
		}
		else if (collision.name == "Mirror2")
		{
			if (mirrorBool == 1) {
				content = "When a higher dimensional being controls a puppet in the lower dimension, death is the only choice to freedom. The death of the puppet is at the mercy of his controller";
			}
			if (mirrorBool == 0) {
				content = "Seems like the indescribable !*/帽&人% will want to play with you for a bit longer";
			}
			item = "mirro";
		}
		else if (collision.name == "NPC Outside")
		{
			content = "Another marionette of the Gods, even the Gods is just the mere playthings of someone in another dimension. After drinking the red potion, long ago I understand the world more, but there's no escaspe";
		}

		else if (collision.name == "NPC Outside1")
		{
			content = "Use 'Left Click' to attack... can you just read it yourself? I'm too lazy to explain, go to control in the main menu again. I hope I did a good job as a NPC";
		}

		//Hero
		else if (collision.name == "Hero")
		{
			content = "The only person who survive from the fate of sacrifice. With the powerful power the Gods gave him, " +
				"he create a country that everybody can live peacefully. Before hero pass away in his bed, he told people that " +
				"he is not a hero but a coward that's afraid of the truth";
		} 
		else if (collision.name == "Hero Ending")
		{
			content = "With the newly selected Hero, the statue's color changed to green. Everyone knows this is not the end of the sacrifice. One day the statue will be red again, and the Gods will pick someone again";
		}
		else if (collision.name == "Post Ending")
		{
			content = "You can keep beating the levels. All the upgrade will be kepted.";
		}

		//Bonus
		if (collision.name == "Message")
		{
			content = "FOR THE PERSON WHO SEES THIS, I LEFT ONE OF MY BOW ABILITIES IN THE DARK SLAB. " + 
			"I CAN ONLY LEAVE ONE ABILITY BECAUSE THE ANGELS ARE WATCHING ME. I MIGHT BE DEAD BY THE TIME YOU SAW THIS, BUT IN THE END, NOTHING IN THIS WORLD MATTERS. EVERY IN THIS WORLD IS FAKE...";
		}
		pop = true;
	}

	private void OnTriggerExit2D(Collider2D collision) {
		
			pop = true;
			open = false;
			Animator popAnim = GameObject.FindGameObjectWithTag("PopUp").GetComponent<Animator>();
			popAnim.SetBool("pop", false);
			StartCoroutine(close(popAnim));

	} 

	IEnumerator close(Animator popAnim) {
		popAnim.SetBool("close", true);
		yield return new WaitForSeconds(.5f);
		popAnim.SetBool("close", false);
		nextTrigger.SetActive(true);
	}
}
