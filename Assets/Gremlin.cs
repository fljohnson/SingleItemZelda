using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Gremlin : MonoBehaviour
{
	const float QUARTER_PI = Mathf.PI/4f;
	public enum State {
		TARGETING,
		ADVANCING,
		ATTACKING,
		RELOADING,
		RETREATING,
		COVERED
	}
	
	public static Player player;
	public float pursuitRange = 10f; 
	public float secondsBetweenAttacks = 3f;
	private State state = State.TARGETING;
	private bool processingHit = false;
	private float nextAttackTime = 0f;
	
	private UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent=GetComponent<UnityEngine.AI.NavMeshAgent>();
        if(player == null) {
			player = GameObject.FindWithTag("Player").GetComponent<Player>();
		}
    }

    // Update is called once per frame
    void Update()
    {
        Assert.IsNotNull(agent, "Missed arming agent");
        Assert.IsNotNull(player, "Missed finding player");
        if(player.hitPoints <= 0) {
			return;
		}
		//At any time, if the player is in the vicinity (so it matters) and has the lantern, RUN!
		if(PlayerHasLantern()) {
			if(state != State.RETREATING) {
				state = State.RETREATING;
			}
		}
        switch(state) {
			case State.TARGETING:
				FindPlayer();
				break;
			case State.ADVANCING:
				Pursue();
				break;
			case State.ATTACKING:
				AttackInterval();
				break;
			case State.RETREATING:
				Flee();
				break;
			default:
				break;
		}
    }
    
    bool PlayerHasLantern() {
		//if the player's out of range, continue with whatever we were doing
		Vector3 playerPos = player.transform.position;
		Vector3 myPos = transform.position;
		//stored the state to guard against mid-frame changes in data
		Vector3 deltaPos = (playerPos - myPos);
		if(deltaPos.magnitude > pursuitRange)
		{
			return false;
		}
		return (player.HasItem("Lantern"));
		
	}
    void FindPlayer() {
		Vector3 playerPos = player.transform.position;
		Vector3 myPos = transform.position;
		//stored the state to guard against mid-frame changes in data
		Vector3 deltaPos = (playerPos - myPos);
		//if the player isn't within range, don't bother
		if(deltaPos.magnitude > pursuitRange)
		{
			return;
		}
		//O-kay, are there any walls between us and the player?
		int wallLayerMask = 1 << 8; //bit shift layer 8
		if(Physics.Raycast(myPos,deltaPos.normalized,pursuitRange, wallLayerMask)) { //we hit a wall
			return;
		}
		//time to go to work
		if(!agent.enabled) {
			agent.enabled = true;
		}
		agent.SetDestination(playerPos);
		state = State.ADVANCING;
	}
	
	
	void Pursue() {
		if(player.DidMove()) {
			if(!agent.enabled && Time.time >= nextAttackTime) {
				//break's over
				agent.enabled = true;
			}
			agent.SetDestination(player.transform.position);
		}
		
	}
	
	void Flee() {
		//get away from the player until out of range or the player drops the Lantern
		//figure out the direction we want to oppose (playerDir)
		//pick a random angle between playerDir - 135 and playerDir + 135 (degrees)
		//scale the resulting vector by range, and set the agent to go to that offset from our positon 
		
		
		Vector3 playerPos = player.transform.position;
		Vector3 myPos = transform.position;
		//stored the state to guard against mid-frame changes in data
		Vector3 deltaPos = (playerPos - myPos);
		if(!PlayerHasLantern()) //it's over now, so back to pursuing after a break
		{
			nextAttackTime = Time.time + 5f;
			state = State.TARGETING;
			return;
		}
		//simple trick to find 180 deg (PI radians) from the direction to player
		float theta = Mathf.Atan2(-deltaPos.z,-deltaPos.x);
		
		//pick a number between -45 and 45 (inclusive), and add it to that angle
		
		theta += Random.Range(-QUARTER_PI, QUARTER_PI);
		//now form the retreat vector and add it to myPos. this is the agent's destination
		Vector3 retreat = new Vector3(Mathf.Cos(theta),0f,Mathf.Sin(theta)); //makes debugging easier
		agent.SetDestination(myPos+retreat);
	}
	
	void AttackInterval() {
		if(Time.time >= nextAttackTime) {
			//let 'im have it
			player.Zap();
			if(player.hitPoints > 0f) {
				//take a breather
				agent.enabled = false; 
				nextAttackTime = Time.time + secondsBetweenAttacks;
			}
		}
	}
	
	
	void OnCollisionEnter(Collision collision) {
		//prevents multi-processing of one collision
		if(processingHit) {
			return;
		}
		processingHit = true;
		if(collision.gameObject.tag == "Player") {
			state = State.ATTACKING;
		}
		processingHit = false;
	}
	
	void OnCollisionExit(Collision collision) {
		
		if(collision.gameObject.tag == "Player") {
			state = State.ADVANCING;
		}
		processingHit = false;
	}
}
