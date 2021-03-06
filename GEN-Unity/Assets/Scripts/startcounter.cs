﻿using UnityEngine;
using System.Collections;

public class startcounter : MonoBehaviour {
	//Label text
	public UILabel countertext;
	//playermovement
	public GameObject playermovement;
	//enemymovement
	public GameObject[] enemymovement;
	//ref to tween
	private TweenScale scaler;
	//timerobj
	public GameObject timerobject;
	//audio for 1 2 3
	public AudioClip audionum;
	//audio go
	public AudioClip audiogo;
	//turn off UI widget
	public UIWidget UIcontroller;

	//will turn on playmaker parts
//	public bool addyes = false;
//	public GameObject adbot;
//	public GameObject adcon;


	public string Countertext
	{
		get
		{
			return countertext.text;
		}
		set
		{
			countertext.text = value;
		}

	}

	// Use this for initialization
	void Start () {

		//set addyes by editor
//		if(addyes)
//		{
//			adbot.gameObject.SetActive(true);
//			adcon.gameObject.SetActive(true);
//		}


	//	scaler = gameObject.GetComponent<TweenScale>();
		StartCoroutine(startgame());

		enemymovement = GameObject.FindGameObjectsWithTag("Enemy");
		playermovement.GetComponent<Playercontrol>().enabled = false;
		playermovement.GetComponent<pbullets>().enabled = false;

		timerobject.GetComponent<Timer>().enabled = false;

		for(int i = 0;i<enemymovement.Length;i++){
			enemymovement[i].transform.FindChild("Nose shooter").gameObject.SetActive(false);
			if(enemymovement[i].gameObject.name == "bouy2")
			{
				enemymovement[i].transform.FindChild("Nose shooter2").gameObject.SetActive(false);
			}
			enemymovement[i].GetComponent<EnemyAI>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator startgame()
	{
		for(int i =3; i>0;i--){
			audio.PlayOneShot(audionum,1f);
			countertext.text = i.ToString();
			yield return new WaitForSeconds(1f);

		}
		countertext.text = "GO!";
		audio.PlayOneShot(audiogo,1f);
		yield return new WaitForSeconds(.5f);
		countertext.text = "";

		UIcontroller.gameObject.SetActive(true);

		playermovement.GetComponent<Playercontrol>().enabled = true;
		playermovement.GetComponent<pbullets>().enabled = true;

		for(int i = 0;i<enemymovement.Length;i++){
			enemymovement[i].transform.FindChild("Nose shooter").gameObject.SetActive(true);
			if(enemymovement[i].gameObject.name == "bouy2")
			{
				enemymovement[i].transform.FindChild("Nose shooter2").gameObject.SetActive(true);
			}
			enemymovement[i].GetComponent<EnemyAI>().enabled = true;
		}

		timerobject.GetComponent<Timer>().enabled = true;
	
	}
	

}
