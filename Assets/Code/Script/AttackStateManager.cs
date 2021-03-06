﻿using UnityEngine;
using System.Collections;

namespace Limone{
	public class AttackStateManager : MonoBehaviour,IAttackStateController {
		private AttackStateManager actAttackState;
		private GameData gamedata;
		public Rigidbody Bullet;
		public GameObject Floor;
		public GameObject Wall;
		public GameObject Loof;
		private AudioClip GunSE;
		private AudioClip TrapSE;
		private GameObject Weapon;
		public int weaponNum = 0;
		private GunShotState gun;
		public int FireRate = 30;
		private PutTrapState PutTrap;

		[HideInInspector]
		public Vector3 mousepos;
		[HideInInspector]
		public Vector3 screenpos;
		public Vector3 PutPos;
		private GameObject touchobj;
		private RaycastHit cameraRayHit;

		public AttackStateController attackstatecontroller;
		private SECheck secheck;
		
		public void OnEnable(){
			attackstatecontroller.SetAttackStateManagerController(this);
		}
		// Use this for initialization
		void Start () {
			AttackStateManagerInit();
		}
		// Update is called once per frame
		public void Update () {
			if(weaponNum == 0){
				//Debug.Log("gun shot");
				gun.GunAction(transform);
			}
			//FloorTrap
			else if(weaponNum == 1){
				FloorTrap();
			}
			//WallTrap
			else if(weaponNum == 2){
				WallTrap();
			}
			//LoofTrap
			else if(weaponNum == 3){
				LoofTrap();
			}
		}

		public void FloorTrap(){
			var CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			GetClickPos();
			ClickPosOffset();
			Floor.transform.position = screenpos;
			if(Input.GetMouseButtonDown(0) && Physics.Raycast(CameraRay,out cameraRayHit,500.0f) && gamedata.Money >= 100){
				TrapPosOffset(0.0f);
				touchobj = cameraRayHit.collider.gameObject;
				if(touchobj.tag == "Floor"){
					gamedata.Money -= 100;
					PutTrap.PutFloorTrap(PutPos);
				}
			}
		}
		public void WallTrap(){
			var CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			GetClickPos();
			ClickPosOffset();
			Wall.transform.position = screenpos;
			if(Input.GetMouseButtonDown(0) && Physics.Raycast(CameraRay,out cameraRayHit,500.0f) && gamedata.Money >= 100){
				TrapPosOffset(2.3f);
				touchobj = cameraRayHit.collider.gameObject;
				if(touchobj.tag == "Wall"){
					gamedata.Money -= 100;
					PutTrap.PutWallTrap(PutPos);
				}
			}
		}
		public void LoofTrap(){
			var CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			GetClickPos();
			ClickPosOffset();
			Loof.transform.position = screenpos;
			if(Input.GetMouseButtonDown(0) && Physics.Raycast(CameraRay,out cameraRayHit,500.0f) && gamedata.Money >= 100){
				TrapPosOffset(0.0f);
				touchobj = cameraRayHit.collider.gameObject;
				if(touchobj.tag == "Loof"){
					gamedata.Money -= 100;
					PutTrap.PutLoofTrap(PutPos);
				}
			}
		}

		public string FormatClickPos(){
			return mousepos.ToString();
		}
		public string FormatClickPosOffset(){
			return screenpos.ToString();
		}

		public string GetClickPos(){
			mousepos = Input.mousePosition;
			return mousepos.ToString();
		}

		public string ClickPosOffset(){
			screenpos = Camera.main.ScreenToWorldPoint(mousepos);
			return screenpos.ToString();
		}

		public float TrapPosOffset(float offset){
			PutPos = cameraRayHit.point;
			PutPos.z = offset;
			return PutPos.z;
		}

		public void AttackChange(int newWeaponNum){
			weaponNum = newWeaponNum;
			Debug.Log("Atack mode ;" + weaponNum);
		}
		public void AttackStateManagerInit(){
			gamedata = GameObject.Find("GameManager").GetComponent<GameData>();
			secheck = gameObject.GetComponentInParent<SECheck>();
			GunSE = secheck.GunShotSE;
			TrapSE = secheck.PutTrapSE;
			gun = new GunShotState(Bullet,FireRate,GunSE);
			PutTrap = new PutTrapState(Floor,Wall,Loof,TrapSE);
		}
	}
}