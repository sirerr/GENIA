﻿using UnityEngine;
using System.Collections;

public class makebigger : MonoBehaviour {

	private Vector3 playerscale;
	public float scalesize;
	public static bool makelarger = false;
	
	//make larger scale 15

	void Start()

	{
		playerscale = transform.lossyScale;
		print(playerscale);

	}
	
	void Update()
		
	{
		if(makelarger){

			transform.localScale = new Vector3(scalesize,1,scalesize);

			makelarger = false;

			StartCoroutine(GetSmall());

		}    

	}

	IEnumerator GetSmall()
	{
	yield return new WaitForSeconds(5f);
		transform.localScale = playerscale;
	}


}