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
		[SerializeField] PatrolPath patrolPath;
		[SerializeField] float waypointTolerance = 1f;

		Fighter fighter;
		GameObject player;
		Health health;
		Mover mover;
		
		Vector3 guardPosition;
		float timeSinceChase = Mathf.Infinity;
		int currentWaypointIndex = 0;

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
				AttackBehavior();
			} else if (timeSinceChase < suspicionTime) {
				SuspicionBehavior();
			} else {
				PatrolBehavior();
			}

			timeSinceChase += Time.deltaTime;
		}

		void AttackBehavior() {
			fighter.Attack(player);
		}

		void SuspicionBehavior() {
			GetComponent<ActionScheduler>().CancelCurrentAction();
		}

		void PatrolBehavior() {
			Vector3 nextPosition = guardPosition;

			if (patrolPath != null) {
				if (AtWaypoint()) {
					CycleWaypoint();
				}

				nextPosition = GetCurrentWaypoint();
			}

			mover.StartMovementAction(nextPosition);
		}

		bool AtWaypoint() {
			float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
			return distanceToWaypoint < waypointTolerance;
		}

		void CycleWaypoint() {
			currentWaypointIndex = patrolPath.GetNextWaypoint(currentWaypointIndex);
		}

		Vector3 GetCurrentWaypoint() {
			return patrolPath.GetWaypoint(currentWaypointIndex);
		}

		void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, chaseDistance);
		}
	}
}