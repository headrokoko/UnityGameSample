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
		public AudioClip GunSE;
		public AudioClip TrapSE;
		private GameObject Weapon;
		public int weaponNum = 0;
		private GunShotState gun;
		public int FireRate = 30;
		private FloorTrapState floor;
		private WallTrapState wall;
		private LoofTrapState loof;
		private Vector3 mousepos;
		private Vector3 screenpos;
		private Vector3 PutPos;
		private GameObject touchobj;

		public AttackStateController attackstatecontroller;
		
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
				RaycastHit cameraRayHit;
				var CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				mousepos = Input.mousePosition;
				screenpos = Camera.main.ScreenToWorldPoint(mousepos);
				Floor.transform.position = screenpos;
				if(Input.GetMouseButtonDown(0) && Physics.Raycast(CameraRay,out cameraRayHit,500.0f) && gamedata.Money >= 100){
					PutPos = cameraRayHit.point;
					PutPos.z = 0.0f;
					touchobj = cameraRayHit.collider.gameObject;
					if(touchobj.tag == "Floor"){
						gamedata.Money -= 100;
						floor.PutTrap(PutPos);
					}
				}
			}
			//WallTrap
			else if(weaponNum == 2){
				RaycastHit cameraRayHit;
				var CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				mousepos = Input.mousePosition;
				screenpos = Camera.main.ScreenToWorldPoint(mousepos);
				Wall.transform.position = screenpos;
				if(Input.GetMouseButtonDown(0) && Physics.Raycast(CameraRay,out cameraRayHit,500.0f) && gamedata.Money >= 100){
					PutPos = cameraRayHit.point;
					PutPos.z = 2.3f;
					touchobj = cameraRayHit.collider.gameObject;
					if(touchobj.tag == "Wall"){
						gamedata.Money -= 100;
						wall.PutTrap(PutPos);
					}
				}
			}
			//LoofTrap
			else if(weaponNum == 3){
				RaycastHit cameraRayHit;
				var CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				mousepos = Input.mousePosition;
				screenpos = Camera.main.ScreenToWorldPoint(mousepos);
				Loof.transform.position = screenpos;
				if(Input.GetMouseButtonDown(0) && Physics.Raycast(CameraRay,out cameraRayHit,500.0f) && gamedata.Money >= 100){
					PutPos = cameraRayHit.point;
					PutPos.z = 0.0f;
					touchobj = cameraRayHit.collider.gameObject;
					if(touchobj.tag == "Loof"){
						gamedata.Money -= 100;
						loof.PutTrap(PutPos);
					}
				}
			}
		}
		public void AttackChange(int newWeaponNum){
			weaponNum = newWeaponNum;
			Debug.Log("Atack mode ;" + weaponNum);
		}
		public void AttackStateManagerInit(){
			gamedata = GameObject.Find("GameManager").GetComponent<GameData>();
			gun = new GunShotState(Bullet,FireRate,GunSE);
			floor = new FloorTrapState(Floor,TrapSE);
			wall = new WallTrapState(Wall,TrapSE);
			loof = new LoofTrapState(Loof,TrapSE);
		}
	}
}