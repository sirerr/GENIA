﻿using UnityEngine;
using System.Collections;

public class DeadenemyAI : MonoBehaviour {
	
	private GameObject collector;
	private Transform etarget;
	private int edead;


	// Use this for initialization
	void Start () {
		collector = GameObject.Find ("Playerbody");
		etarget= collector.transform;
		edead = 1;
	}


	void OnTriggerEnter(Collider sucker ){

		if(sucker.gameObject.name == "sucker"){
		GameMaster.deadcount += edead;
		Destroy (this.gameObject);
		}
	}
}
