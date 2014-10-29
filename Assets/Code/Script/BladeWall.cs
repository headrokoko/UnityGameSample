﻿using UnityEngine;
using System.Collections;

namespace Limone{
	public class BladeWall : MonoBehaviour,IBladeWallController {

		public int damage = 50;
		public float ReloadTime = 3.0f;
		private bool trap = true;
		private bool TrapBool = false;

		public BladeWallController bladewallcontroller;

		
		public void OnEnable(){
			bladewallcontroller.SetBladeWallController(this);
		}

		void OnTriggerStay(Collider collider){
			if((collider.gameObject.tag == "Enemy") && trap){
				IntegrationTest.Pass(gameObject);
				Debug.Log("BladeWall ON");
				trap = false;
				TrapBool = true;
				GetComponent<Animator>().SetBool("OnTrap",TrapBool);
				//collider.GetComponent<EnemyController>().health -= damage;
				StartCoroutine("Reload");
			}
		}

		IEnumerator Reload(){
			//Debug.Log ("Reload");
			yield return new WaitForSeconds(ReloadTime);
			trap = true;
			TrapBool = false;
			GetComponent<Animator>().SetBool("OnTrap",TrapBool);
			Debug.Log("Walltrapready");
		}
		public void BladeWallInit(){
		
		}
	}
}
