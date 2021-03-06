﻿using UnityEngine;
using System.Collections;

public class extraabilities : MonoBehaviour {
	
	//enemy counter from global var
	private int exabenemycounter;
	
	//ui label to show ability
	public UISprite exablabel;
	
	//side cannons
	public GameObject[] exabcannons;
	
	//black holes
	public GameObject[] exabblackholes;
	
	//constant count
	public int exabrequirementholder = 0;
	
	//variable time count
	public int exabtimecount = 0;
	
	//variable first time
	public bool exabfirsttime = true;
	
	//random ability chosen
	public int exabchosen;
	
	//random ability amount
	public int exababilityamount;
	
	//bombs
	public GameObject exabbombs;

	// player location
	public Transform playerlocation;

	//new variable exab use counter
	public int exabusecounter = 0;
	
	
	void Start()
	{
		exabusecounter = 0;

		StartCoroutine(Quick10());
	}

	void Update()
	{

	}
	
	IEnumerator Quick10()
	{
		
		if(exabfirsttime)
		{
			yield return new WaitForSeconds(13f);
			exabfirsttime = false;
		}
		else
		{
			yield return new WaitForSeconds(exabtimecount);	
		}

		print(DeadEnemyCounter.enemiesdead);
		
		if((DeadEnemyCounter.enemiesdead - (1*exabusecounter))>= exabrequirementholder)
		{
			exabchosen = Random.Range(1,exababilityamount);
			exablabel.gameObject.SetActive(true);
			
			exabusecounter = exabusecounter + exabrequirementholder;
			print ("did meet requirement");
		}
		else
		{
			print ("didn't meet requirement");
			StartCoroutine(Quick10());
		}
		
	}
	
	public void exabclick()
	{
		exablabel.gameObject.SetActive(false);

		switch (exabchosen)
		{
		case 1:

			exabcannons[0].gameObject.SetActive(true);
			exabcannons[1].gameObject.SetActive(true);

			break;
		case 2:

			for(int a=0;a<4;a++)
			{
				exabblackholes[a].gameObject.SetActive(true);
			}
			StartCoroutine (Turnoff());

			break;
		case 3:
			StartCoroutine(exabbombing());
			break;
		}

		StartCoroutine(Quick10());
	}

	IEnumerator Turnoff()
	{

		yield return new WaitForSeconds(8f);

		for(int a=0;a<4;a++)
		{
			exabblackholes[a].gameObject.SetActive(false);
		}

	}

	IEnumerator exabbombing()
	{
		for(int i = 1;i<=10;i++)
		{
			Instantiate(exabbombs, playerlocation.position, playerlocation.rotation);
			yield return new WaitForSeconds(.7f);
		}

	}
	
}