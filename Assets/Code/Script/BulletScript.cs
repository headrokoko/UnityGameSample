using UnityEngine;
using System.Collections;

namespace Limone{
	public class BulletScript : MonoBehaviour,IBulletController {

		public float DestroyTime = 3.0f;

		void OnTriggerEnter(Collider collider){
			if(collider.gameObject.tag == "Enemy"){
				Destroy(this.gameObject);
			}
		}
		public void BulletInit(){
			Destroy(gameObject,DestroyTime);
		}
	}
}
