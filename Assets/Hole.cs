using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
	Vector3 goosing = new Vector3(0f,0.16f,0f);
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
			MakePassable(true);
			player.DropItemOn(transform.position+goosing,this);//goose the y
		}
	}
	
	public void MakePassable(bool passable) {		
		GetComponent<Collider>().enabled = !passable;
	}
}
