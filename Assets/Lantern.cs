using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			Ignite();
		}
	}
	
	void Ignite() {
	}
	
}
