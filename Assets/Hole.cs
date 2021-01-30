using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
	void OnCollisionEnter(Collision collision) {
		CheckForPlayerItem(collision.gameObject);
		
	}
	
	void CheckForPlayerItem(GameObject mightBe) {
		Player player = mightBe.GetComponent<Player>();
		if(player == null)
			return;
		if(player.HasItem("Bridge")) {
			GetComponent<Collider>().enabled = false;
			player.DropItemOn(transform.position);
		}
	}
}
