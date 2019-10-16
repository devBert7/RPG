using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat {
	public class Projectile : MonoBehaviour{
		[SerializeField] float arrowSpeed = 10f;

		Health target = null;
		
		void Update() {
			if (target == null) {
				return;
			}

			transform.LookAt(GetAimLocation());
			transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
		}

		public void SetTarget(Health target) {
			this.target = target;
		}

		Vector3 GetAimLocation() {
			CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

			if (targetCapsule == null) {
				return target.transform.position;
			}
			
			return target.transform.position + Vector3.up * targetCapsule.height / 2;
		}
	}
}