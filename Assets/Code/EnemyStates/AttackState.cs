﻿using UnityEngine;
using Assets.Code.EnemyStates;
using Assets.Code.Interfaces;
using System.Collections;

namespace Assets.Code.EnemyStates{
public class AttackState :  MonoBehaviour,EnemyState{

	public Transform Player;
	public float Range = 5f;
	public float Speed = 0.01f;
	private EnemyStateManager Emanager;

	public AttackState(EnemyStateManager Estatemanager){
		Emanager = Estatemanager;
		
	}

	// Use this for initialization
	void Start () {
	
	}
		
	public void EStateUpdata(){
			Debug.Log("MarchState");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 enemy = transform.position;
		Vector3 playr = Player.transform.position;
		float dis = Vector3.Distance(enemy,playr);
		if(dis > Range){
			Debug.Log("Player Lost");
			Emanager.SwichState(new MarchState(Emanager));
		}
	
	}
}
}
