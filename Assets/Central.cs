using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameAction;

public class Central : MonoBehaviour
{

	public float volume = 1.0f;
	public AudioClip chestItemGet;
	public AudioClip chestItemStore;
	
	private static GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        if(playerObject == null) {
			playerObject = GameObject.FindWithTag("Player");
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ActOnEvent(GameAction a) {
		switch(a) {
			case CHESTGETITEM:
				AudioSource.PlayClipAtPoint(chestItemGet, playerObject.transform.position,volume);
				break;
			
			case CHESTSTOREITEM:
				AudioSource.PlayClipAtPoint(chestItemStore, playerObject.transform.position,volume);
				break;
			default:
			break;
		}
	}
}
