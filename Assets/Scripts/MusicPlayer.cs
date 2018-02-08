using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;

	private AudioSource music;

	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
		} 
		else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);

			// Always play music at start
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}
	}

 	// Following methods used to control the music via SceneManagement
	private void OnEnable () {
   		SceneManager.sceneLoaded += OnSceneLoaded;  // subscribe
	}
 
	private void OnDisable () {
    	SceneManager.sceneLoaded -= OnSceneLoaded;  //unsubscribe
	}
 
	private void OnSceneLoaded (Scene level, LoadSceneMode loadSceneMode) {
		music = GetComponent<AudioSource>();
		// Stop current music
    	music.Stop ();
   
    	int lvl = level.buildIndex;
    	switch (lvl) {
        	case 0:
	            music.clip = startClip;
	            break;
	        case 1:
	            music.clip = gameClip;
	            break;
	        case 2:
	            music.clip = endClip;
	            break;
	    }
 
	    //Play new music
	    music.loop = true;
	    music.Play ();
	}
}
