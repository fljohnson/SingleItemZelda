using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public enum State {
		OK,
		DYING,
		GAME_OVER,
	}
	
	public Vector3[] possibleOtherDirections = new Vector3[]{
		new Vector3(0,0,1),  //0 degrees 
		new Vector3(0,0,-1), //180 degrees
		new Vector3(-1,0,0), //-90 degrees
		new Vector3(1,0,0) //90 degrees
	};
	public State state = State.OK;
	public float step = 0.1f;
	public int hitPoints = 8;
	public GameObject primaryItem;
	public GameObject secondaryItem;
	private bool canStore = false;
	private GameObject potentialPickup;
	
	private GameObject lastCollidedItem;
	
	private bool moved = false;
	
	private Vector3 lastDirection = new Vector3(0f,0f,1f);
	private Transform modelish;
	
	private float spinTime = -1f;
	public static string[] itemTypes = {"Key","Lantern","Bridgelayer"};
    // Start is called before the first frame update
    void Start()
    {
        modelish=transform.Find("default");
    }

    // Update is called once per frame
    void Update()
    {
		
		switch(state) {
			case State.OK:
				CheckInput();
				break;
			case State.DYING:
				RunDeathSequence();
				break;
			default:
				break;
		}
    }
    
    //Have we moved since the last time anyone asked?
    //this means we won't be recalculating paths every darned frame - see Gremlin.cs for a caller
    public bool DidMove() {
		bool rv = moved;
		moved = false;
		return rv;
	}
	
	public void Zap(int damage = 1) {
		hitPoints -= damage;
		if(hitPoints <= 0) {
			hitPoints = 0;
			StartDeathSequence();
		}
	}
	
	void StartDeathSequence() {
		//spin four times over 3 seconds (a la Ms. PacMan)
		spinTime = 2f;
		GetComponent<Rigidbody>().velocity = Vector3.zero; //STOP
		state = State.DYING;
	}
	
	void RunDeathSequence() {
		//for that, 960f degrees are covered in a second
		float dT = Time.deltaTime;
		Vector3 lAngles = modelish.eulerAngles;
		lAngles.y += dT*960f;
		modelish.eulerAngles = lAngles;
		//aaaand decrement
		spinTime -= dT;
		if(spinTime <= 0f) {
			Destroy(modelish.gameObject); //POOF! Alas, brave knight
			state = State.GAME_OVER;
		}
	}
	
    bool IsItem(string tag) {
		return Array.IndexOf(itemTypes,tag) > -1;
	}
	
    void CheckInput() {
		Vector3 motion=Vector3.zero;
		float yRot = 360f;
		bool changed = false;
		if(Input.GetKeyDown("w")){
			motion.z+=step;
			yRot=0f;
			changed = true;
			moved = true;
		}
		if(Input.GetKeyDown("s")){
			motion.z-=step;
			changed = true;
			yRot =180f;
			moved = true;
		}
		if(Input.GetKeyDown("d")){
			motion.x+=step;
			changed = true;
			yRot = 90f;
			moved = true;
		}
		if(Input.GetKeyDown("a")){
			motion.x-=step;
			yRot = -90f;
			changed = true;
			moved = true;
		}
		if(Input.GetKeyUp("a") || Input.GetKeyUp("d") ) {
			motion.x = 0f;
			changed = true;
		}
		if(Input.GetKeyUp("w") || Input.GetKeyUp("s") ) {
			motion.z = 0f;
			changed = true;
		}
		if(Input.GetKeyDown("q")){
			PickUpItem();
		}
		if(Input.GetKeyDown("e")){
			DropItem();
		}
		if(changed) {
			GetComponent<Rigidbody>().velocity=motion;
			if(motion.x != 0f || motion.z != 0f) {
				lastDirection = motion.normalized;
			}
			if(yRot < 360f) 
			{
				Vector3 properAngles = modelish.transform.eulerAngles;
				properAngles.y=yRot;
				modelish.transform.eulerAngles = properAngles;
			}
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		//prevent ping-ponging between carried and picked-up items
		if(collision.gameObject == lastCollidedItem) {
			return;
		}
		if(IsItem(collision.gameObject.tag)) {
			potentialPickup = collision.gameObject; 
			
			if(primaryItem != null) {
				Vector3 coords = potentialPickup.transform.position;
				primaryItem.transform.position = coords;
				primaryItem.SetActive(true);
				lastCollidedItem = primaryItem;
				 
			}
			primaryItem = potentialPickup;
			potentialPickup.SetActive(false);
			
		}
		if(collision.gameObject.tag == "Chest") {
			canStore = true;
		}
	}
	
	void OnCollisionExit(Collision collision) {
		if(IsItem(collision.gameObject.tag)) {
			potentialPickup = null; 
		}
		lastCollidedItem = null;
		if(collision.gameObject.tag == "Chest") {
			canStore = false;
		}
	}
	
	public bool HasItem(string itemType) {
		if(primaryItem == null)
			return false;
		return primaryItem.CompareTag(itemType);
	}
	void PickUpItem() {
		if(canStore) {
			
			if(secondaryItem != null) {
				GameObject temp = null;
				Vector3 coords = SafeDropPoint();
				if(primaryItem != null) {
					//primaryItem.transform.position = coords;
					//primaryItem.SetActive(true);
					temp = primaryItem;
				}
				lastCollidedItem = primaryItem;
				primaryItem = secondaryItem;
				secondaryItem = temp;
				
			}
			else
			{
				Debug.Log("Chest is empty");
			}
			
		}
	}
	void DropItem() {
		if(primaryItem == null){
			return;
		}
		
		if(!canStore) {
			Vector3 coords = SafeDropPoint();
			DropItemOn(coords);
		}
		if(canStore)
		{
			if(secondaryItem ==  null) {
				secondaryItem = primaryItem;
				primaryItem = null;
			}
			else {
				Debug.Log("Chest is full");
			}
		}
		
		
		
				
	}
	
	public void DropItemOn(Vector3 point) {
		primaryItem.transform.position = point;
		primaryItem.SetActive(true);
		lastCollidedItem = null;
		primaryItem = null;
	} 
	
	Vector3 SafeDropPoint() {
		//this attempts to find a non-trapping place to put the Item down
		Vector3 myPos = transform.position;
		//try 1.5 units directly in front of us
		Vector3 motion = 1.5f*lastDirection;
		motion.y = 0f;
		
		
		
		if(!Physics.Raycast(myPos,lastDirection,1.5f)) { //nothing in the way up front, so
			return myPos+motion;
		}
		
		//try behind us:
		Vector3 otherDir = lastDirection*-1f;
		if(!Physics.Raycast(myPos,otherDir,1.5f)) { //nothing in the way up front, so
			motion = 1.5f*otherDir;
			motion.y = 0f;
			return myPos+motion;
		}
		
		//try the left:
		
		otherDir.x= lastDirection.z;
		otherDir.z= lastDirection.x;
		
		if(!Physics.Raycast(myPos,otherDir,1.5f)) { //nothing in the way up front, so
			motion = 1.5f*otherDir;
			motion.y = 0f;
			return myPos+motion;
		}
		
		//guess that leaves the right
		otherDir = -1f*otherDir;
		motion = 1.5f*otherDir;
		motion.y = 0f;
			
		return myPos+motion;
	}
}
