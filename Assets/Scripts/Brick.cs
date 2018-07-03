using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public Sprite[] hitSprites;
	//public AudioSource audioSource;
	public AudioClip Explosion;
	public static int breakableCount = 0;
	public GameObject smoke;
	private bool isBreakable;
	private int timesHit;
	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		//Kep track of breakable bricks
		if (isBreakable){
			breakableCount++;
		}
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter2D (Collision2D collision) {
 		AudioSource.PlayClipAtPoint (Explosion, transform.position, 0.8f);
		if (isBreakable){
		HandleHits();
		}	
	}

	void HandleHits(){
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		if (timesHit >= maxHits){
			breakableCount--;
			levelManager.BrickDestroyed();
			SmokeEffect();
			Destroy(gameObject);
		} else {
			LoadSprites();
		}
	}

	void SmokeEffect(){
		GameObject smokePuff = Instantiate (smoke, gameObject.transform.position, Quaternion.identity) as GameObject;
		smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}

	void LoadSprites(){
		int spriteIndex = timesHit - 1;
		//If a sprite isn't there, render another one
		if (hitSprites[spriteIndex]){
		this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		} else {
			Debug.LogError ("Brick sprite missing.");
		}
	}

	void SimulateWin (){
		levelManager.LoadNextLevel();
	}
}
