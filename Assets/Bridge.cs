using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
	private Hole _coveredHole;
	private Collider myCollider;
	public bool inUse = false;
	
	public bool inInventory;
	public Hole coveredHole {
		set {
				if(_coveredHole != null)
					_coveredHole.MakePassable(value != null);
				_coveredHole = value;
				
		}
	}
		
	public bool onHole {
		get {
			return (_coveredHole !=null);
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
		
    }
    
	
	
    void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Player") {
			Debug.Log("BRIDGE touched");
			inUse = (onHole);
		}
	}
	
	void OnCollisionExit(Collision collision) {
		if(collision.gameObject.tag == "Player") {
			Debug.Log("BRIDGE left");
			inUse = false;
		}
	}
	
	
}
