using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameAction;
using UnityEngine.Assertions;

public class Central : MonoBehaviour
{

	public float volume = 1.0f;
	public AudioClip chestItemGet;
	public AudioClip chestItemStore;
	public AudioClip lanternIgnite;
	public AudioClip lanternContinue;
	
	private float timeToBurn = 0f;
	
	private static GameObject whereToPlay;
	private static AudioSource player;
    // Start is called before the first frame update
    void Start()
    {
        if(whereToPlay == null) {
			whereToPlay = GameObject.FindWithTag("MainCamera");
			player = whereToPlay.GetComponent<AudioSource>();
			Assert.IsNotNull(player,"No AudioSource attched to Camera");
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToBurn > 0f) {
			if(Time.time >= timeToBurn) {
				timeToBurn = 0f;
				player.loop = true;
				player.clip = lanternContinue;
				player.volume = volume;
				player.Play();
			}
		}	
    }
    
    public void ActOnEvent(GameAction a) {
		switch(a) {
			case CHESTGETITEM:
				player.PlayOneShot(chestItemGet,volume);
				break;
			
			case CHESTSTOREITEM:
				player.PlayOneShot(chestItemStore, volume);
				break;
			case LITLANTERN:
				timeToBurn = lanternIgnite.length +Time.time;
				player.PlayOneShot(lanternIgnite, volume);
				break;
			case OFFLANTERN:	
				timeToBurn = -1f;
				player.Stop();
				player.loop = false;
				break;
			default:
			break;
		}
	}
}
