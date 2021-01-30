using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //It seemed simple to do the key check in the Door
    void OnCollisionEnter(Collision collision) {
		CheckForPlayerKey(collision.gameObject);
		
	}
	
	void CheckForPlayerKey(GameObject mightBe) {
		Player player = mightBe.GetComponent<Player>();
		if(player == null)
			return;
		if(player.HasItem("Key")) {
			//for now, ditch this door
			Destroy(gameObject);
			//GetComponent<Collider>().enabled = false;
		}
	}
}
