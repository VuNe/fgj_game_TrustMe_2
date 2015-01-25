﻿using UnityEngine;
using System.Collections;

//******************************************************************************************//
//class that controls enemy movement
//******************************************************************************************//
public class GhostMovement : MonoBehaviour {
	
	NavMeshAgent navAgent;
	Transform player;
	bool bPlayerVisible = false;
	bool bPlayerIsAlive = true;
			
	public Transform patrolTarget;
	public float FOW = 110f;

//******************************************************************************************//
//get our nav agent and our player object
//******************************************************************************************//
	void Awake()
	{
		navAgent = GetComponent <NavMeshAgent> ();
		player   = GameObject.FindGameObjectWithTag ("Player").transform;

	}	
//******************************************************************************************//
//Define the target
//******************************************************************************************//
	void Update () 
	{
		IsPlayerAhead (player);
		ChooseTarget ();
	}
//******************************************************************************************//
//make the ghost move/rotate towards the target
//******************************************************************************************//
	void ChooseTarget ()
	{
		if (bPlayerVisible)
		{
			float distance     = Vector3.Distance(navAgent.transform.position,player.transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, player.transform.rotation, Time.deltaTime);

			if(distance >= 1.5f)
			{
					navAgent.SetDestination (player.position);
			}
			else{
					navAgent.Stop();
			}

		} else {
					navAgent.Stop();
		}
	}

//******************************************************************************************//
//triggers to check does the ghost see the player or not
//******************************************************************************************//
	void OnTriggerEnter( Collider target)
	{
		if (target.tag == "Player") 
		{
			bPlayerVisible = true;
		}
	}
//******************************************************************************************//
//check is the player front of ghost's field of view
//******************************************************************************************//

	void IsPlayerAhead (Transform target)
	{
		Vector3 direction = target.transform.position - transform.position;
		float angle = Vector3.Angle (direction, transform.forward);

		if (angle < FOW * 0.5f) 
		{
				RaycastHit hit;
				if (Physics.Raycast (transform.position, direction.normalized, out hit, 100)) 
				{
					if (hit.collider.gameObject.tag == "Player") 
				 	{
								bPlayerVisible = true;
					} else {
								print ("i don't see you");
								bPlayerVisible = false;
					}
				}
		}
	}
}
