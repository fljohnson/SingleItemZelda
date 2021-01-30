using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
	private Hole _coveredHole;
	private Collider myCollider;
	
	public bool inInventory;
	public Hole coveredHole {
		set {
				ArmCollider(value == null);
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
		if(!inInventory && onHole)
		{
			ArmCollider(!PlayerOn());
		}
    }
    
    void ArmCollider(bool enabled) {
		if(myCollider == null)
		{
			 myCollider = GetComponent<Collider>();
		}
		myCollider.enabled = enabled;
	}
	
	bool PlayerOn() {
		float saneDistance = 2f;
		Vector3 myPos = transform.position;
		Vector3 rayDir = new Vector3(0,1,0);
		if(Physics.Raycast(myPos,rayDir,saneDistance)) { 
			//something directly above
			return true;
		}
		//north?
		rayDir.z=1f;
		if(Physics.Raycast(myPos,rayDir.normalized,saneDistance)) { 
			return true;
		}
		
		//south?
		
		rayDir.z=-1f;
		if(Physics.Raycast(myPos,rayDir.normalized,saneDistance)) { 
			return true;
		}
		
		//west?
		rayDir.z =0;
		rayDir.x = -1f;
		if(Physics.Raycast(myPos,rayDir.normalized,saneDistance)) { 
			return true;
		}
		
		//east?
		rayDir.x = 1f;
		if(Physics.Raycast(myPos,rayDir.normalized,saneDistance)) { 
			return true;
		}
		Debug.Log("NOT HERE:ARMING collider");
		return false;
		
	}
    
}
