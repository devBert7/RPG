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
		[SerializeField] float dwellTime = 3f;
		[SerializeField] PatrolPath patrolPath;
		[SerializeField] float waypointTolerance = 1f;
		[Range(0,1)]
		[SerializeField] float patrolSpeedFraction = .2f;

		Fighter fighter;
		GameObject player;
		Health health;
		Mover mover;
		
		Vector3 guardPosition;
		float timeSinceChase = Mathf.Infinity;
		float timeSinceDwell = Mathf.Infinity;
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
				AttackBehavior();
			} else if (timeSinceChase < suspicionTime) {
				SuspicionBehavior();
			} else {
				PatrolBehavior();
			}

			UpdateTimers();
		}

		void AttackBehavior() {
			timeSinceChase = 0;
			fighter.Attack(player);
		}

		void SuspicionBehavior() {
			GetComponent<ActionScheduler>().CancelCurrentAction();
		}

		void PatrolBehavior() {
			Vector3 nextPosition = guardPosition;

			if (patrolPath != null) {
				if (AtWaypoint()) {
					timeSinceDwell = 0f;
					CycleWaypoint();
				}

				nextPosition = GetCurrentWaypoint();
			}

			if (timeSinceDwell > dwellTime) {
				mover.StartMovementAction(nextPosition, patrolSpeedFraction);
			}
		}

		void UpdateTimers() {
			timeSinceChase += Time.deltaTime;
			timeSinceDwell += Time.deltaTime;
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