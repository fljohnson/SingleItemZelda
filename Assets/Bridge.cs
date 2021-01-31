using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Bridge : MonoBehaviour
{
	private Hole _coveredHole;
	private Collider myCollider;
	public bool inUse = false;
	
	private bool primed = false;
	
	private GameObject playerHit;
	private float prior_y = -10f;
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
		if(primed) {
			
			playerHit.GetComponent<Rigidbody>().constraints &= ~(RigidbodyConstraints.FreezePositionZ| RigidbodyConstraints.FreezePositionX);
			playerHit.GetComponent<Collider>().enabled = true;
			primed = false;
		}
    }
    
	
	
    void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Player") {
			Debug.Log("BRIDGE touched "+ primed.ToString());
			inUse = (onHole);
			if(inUse) {
				/*
					BoxCollider yeCollider = GetComponent<BoxCollider>();
					Vector3 aha = (yeCollider.size*.5f);
					aha.z*= -1f;
					prior_y = collision.transform.position.y;
					Debug.Log(transform.TransformPoint(yeCollider.center+aha).ToString("F2"));
					Debug.Log(collision.GetContact(0).point.ToString("F2"));
					collision.transform.position = collision.GetContact(0).point + new Vector3(0f,.1f,0f);
					//collision.transform.position += new Vector3(0f,.35f,0f);
					*/
				/*	
				if(!primed)
				{
					//prime it by lifting
					prior_y = collision.transform.position.y;
					aha.x=0f;
					aha.y+= .11f;
					aha.z=0f;
					
					Debug.Log(aha.ToString("F2")+" "+prior_y);
					collision.transform.position += aha; //new Vector3(0f,0.025f,0f);
					primed=true;
				}
				else
				{
					primed = false;
					Debug.Log("BRIDGE cocked");
				}
				*/
			}
		}
	}
	
	
	
	void OnCollisionExit(Collision collision) {
		if(collision.gameObject.tag == "Player") {
			Debug.Log("BRIDGE left "+ primed.ToString());
	
			/*
			if(prior_y > -10f) {
				
				Debug.Log("BRIDGE fired");
				Vector3 nu = collision.transform.position;
				Debug.Log(nu.ToString("F2")+" " +prior_y);
				nu.y = prior_y;
				collision.transform.position = nu;
				prior_y = -10f;
			}
			*/
			/*
			//collision.collider.enabled = false;
			collision.rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ| RigidbodyConstraints.FreezePositionX;
			Debug.Log("out2:"+transform.position.ToString("F2")+" "+collision.transform.position.ToString("F2"));
			Vector3 delta = 0.5f*(collision.transform.position - transform.position);
			collision.transform.position+=delta;
			delta = collision.transform.position;
			delta.y = prior_y;
			Debug.Log("ptui: "+delta.ToString("F2"));
			collision.transform.position = delta;
			
			playerHit = collision.gameObject;
			*/		
			//Assert.IsTrue(false);
			inUse = false;
			
			//primed = true;
			
		}
	}
	
	
}
