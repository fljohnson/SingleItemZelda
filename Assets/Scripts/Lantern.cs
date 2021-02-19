using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameAction;

public class Lantern : MonoBehaviour
{
	private bool lit = false;
	private Central central;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetLit(bool isLit) {
		if(!lit) {
			if(isLit) 
				Ignite();
		}
		else {
			if(!isLit)
				Douse();
		}
	}
	
	void Ignite() {
		if(central == null) {
			central = GameObject.Find("Central").GetComponent<Central>();
		}
		central.ActOnEvent(LITLANTERN);
		lit = true;
	}
	
	void Douse() {
		if(central == null) {
			central = GameObject.Find("Central").GetComponent<Central>();
		}
		central.ActOnEvent(OFFLANTERN);
		lit = false;
	}
	
}
