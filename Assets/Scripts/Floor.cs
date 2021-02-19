using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
	public Room owner;
   
    void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Player") {
			owner.MoveCamera();
		}
	}
	
	
}
