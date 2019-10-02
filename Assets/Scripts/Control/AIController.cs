using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control {
	public class AIController : MonoBehaviour {
		[SerializeField] float chaseDistance = 5f;

		Fighter fighter;
		GameObject player;
		Health health;
		Vector3 guardPosition;
		Mover mover;

		void Start() {
			fighter = GetComponent<Fighter>();
			player = GameObject.FindWithTag("Player");
			health = GetComponent<Health>();
			guardPosition = transform.position;
			mover = GetComponent<Mover>();
		}

		void Update() {
			if (health.IsDead()) {
				return;
			}

			bool inAttackRange = Vector3.Distance(player.transform.position, transform.position) <= chaseDistance;

			if (inAttackRange && fighter.CanAttack(player)) {
				fighter.Attack(player);
			} else {
				mover.StartMovementAction(guardPosition);
			}
		}

		void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, chaseDistance);
		}
	}
}