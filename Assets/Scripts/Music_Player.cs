using UnityEngine;
using System.Collections;

public class Music_Player : MonoBehaviour {

	static Music_Player instance = null;

	void Awake (){
		Debug.Log ("Music player Awake " + GetInstanceID());
		// Makes sure only ONE music player is active
		if (instance != null){
			Destroy (gameObject);
			print ("Duplicate Music Player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Music player Start " + GetInstanceID());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
