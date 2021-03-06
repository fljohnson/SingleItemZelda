﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject explosion;

    //It seemed simple to do the key check in the Door
    void OnCollisionEnter(Collision collision)
    {
		CheckForPlayerKey(collision.gameObject);		
	}
	
	void CheckForPlayerKey(GameObject mightBe) {
		Player player = mightBe.GetComponent<Player>();
		if(player == null)
			return;
		if(player.HasItem("Key")) {
            //for now, ditch this door'
            player.ThrowOutItem();
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
			//GetComponent<Collider>().enabled = false;
		}
	}
}
