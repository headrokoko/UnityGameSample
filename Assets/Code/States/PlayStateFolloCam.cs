﻿using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.States;

namespace Assets.Code.States{
	public class PlayStateFollowCam : IState {
		private GameStateManager gamemanager;
		private GameData gamedata;
		private SpwanController spwancontroller;
		
		public PlayStateFollowCam(GameStateManager gamestateManager){
			gamemanager = gamestateManager;
			Debug.Log("Follor com state");
			gamedata = GameObject.Find("GameManager").GetComponent<GameData>();
		}
		
		public void StateUpdata(){
			//Debug.Log("play state stateup");
			spwancontroller = GameObject.Find("EnemySpwanManager").GetComponent<SpwanController>();

			if(Input.GetKeyDown(KeyCode.Return)){
				Time.timeScale = 0;
				gamemanager.SwichState(new ResultState(gamemanager));
			}

			//Baseの耐久値が０
			if(gamedata.BaseHP <= 0){
				Time.timeScale = 0;
				gamemanager.SwichState(new ResultState(gamemanager));			
			}

			//既定のWave数をこなす
			if(spwancontroller.StageClear == true){
				Time.timeScale = 0;
				Debug.Log("All Wave Clear");
				gamemanager.SwichState(new ResultState(gamemanager));	
			}
			
		}

		public void Render(){
			if(GUI.Button(new Rect(50,450,50,50),"Gun")){
				Debug.Log("Gun shot mode");
			}
			
			else if(GUI.Button(new Rect(100,450,50,50),"Floor")){
				Debug.Log("Floor trap mode");
			}
			
			else if(GUI.Button(new Rect(150,450,50,50),"Wall")){
				Debug.Log("Wall trap mode");
			}
			
			else if(GUI.Button(new Rect(200,450,50,50),"Loof")){
				Debug.Log("Loof trap mode");
			}
		}

		
		void CameraChange(){
			foreach(GameObject camera in gamemanager.gameData.cameras){
				if(camera.name != "FollowCamera"){
					camera.SetActive(true);
				}
				else{
					camera.SetActive(false);
				}
			}
			
		}
		
	}
}