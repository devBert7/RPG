using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat {
	public class Projectile : MonoBehaviour{
		[SerializeField] float arrowSpeed = 10f;
		[SerializeField] bool isHoming = true;
		
		float damage = 0;
		Health target = null;

		void Start() {
			transform.LookAt(GetAimLocation());
		}
		
		void Update() {
			if (target == null) {
				return;
			}

			if (isHoming && !target.IsDead()) {
				transform.LookAt(GetAimLocation());
			}

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
				Invoke("RemoveObject", 5f);
				return;
			}

			if (target.IsDead()) {
				return;
			}

			target.TakeDamage(damage);

			Destroy(gameObject);
		}

		void RemoveObject() {
			Destroy(gameObject);
		}
	}
}