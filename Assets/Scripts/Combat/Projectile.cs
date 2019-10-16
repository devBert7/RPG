using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat {
	public class Projectile : MonoBehaviour{
		[SerializeField] float arrowSpeed = 10f;
		
		float damage = 0;
		Health target = null;
		
		void Update() {
			if (target == null) {
				return;
			}

			transform.LookAt(GetAimLocation());
			transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
		}

		public void SetTarget(Health target, float damage) {
			this.target = target;
			this.damage = damage;
		}

		Vector3 GetAimLocation() {
			CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

			if (targetCapsule == null) {
				return target.transform.position;
			}
			
			return target.transform.position + Vector3.up * targetCapsule.height / 2;
		}

		void OnTriggerEnter(Collider other) {
			if (other.GetComponent<Health>() != target) {
				return;
			}

			target.TakeDamage(damage);

			Destroy(gameObject);
		}
	}
}