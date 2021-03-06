﻿using UnityEngine;
using System;

namespace Limone{
	
	[Serializable]
	public class GameDataController{

		public Texture2D startTex;
		public Texture2D menuTex;
		public Texture2D stageselectTex;
		public Texture2D resultTex;

		public int PlayerHP;

		public IGameDataController IgamedataController;
		public GameData GData;

		public GameDataController(){
			GData = new GameData();
		}
		
		void Start() {
		}

		public void SetGameDataController(IGameDataController igamedatacontroller){
			this.IgamedataController = igamedatacontroller;
		}


		public int GetPlayerHP(){
			return GData.playerHP;
		}
		public int GetInitPlayerHP(){
			return GData.initPlayerHP;
		}
		public int GetBaseHP(){
			return GData.BaseHP;
		}
		public int GetScore(){
			return GData.score;
		}
		public int Getmonery(){
			return GData.Money;
		}
	}
}
