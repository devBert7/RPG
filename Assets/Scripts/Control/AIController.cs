using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control {
	public class AIController : MonoBehaviour {
		[SerializeField] float chaseDistance = 5f;
		[SerializeField] float suspicionTime = 5f;

		Fighter fighter;
		GameObject player;
		Health health;
		Vector3 guardPosition;
		Mover mover;
		float timeSinceChase = Mathf.Infinity;

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
				timeSinceChase = 0;
				fighter.Attack(player);
			} else if (timeSinceChase < suspicionTime) {
				GetComponent<ActionScheduler>().CancelCurrentAction();
			} else {
				mover.StartMovementAction(guardPosition);
			}

			timeSinceChase += Time.deltaTime;
		}

		void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, chaseDistance);
		}
	}
}