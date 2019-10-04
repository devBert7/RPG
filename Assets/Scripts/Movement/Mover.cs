using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement {
	public class Mover : MonoBehaviour, IAction {
		[SerializeField] float maxSpeed = 6f;

		NavMeshAgent navMeshAgent;
		Health health;

		void Start() {
			health = GetComponent<Health>();
			navMeshAgent = GetComponent<NavMeshAgent>();
		}

		void Update() {
			navMeshAgent.enabled = !health.IsDead();

			UpdateAnimator();
		}

		public void StartMovementAction(Vector3 destination, float speedFraction) {
			GetComponent<ActionScheduler>().StartAction(this);
			MoveTo(destination, speedFraction);
		}

		public void MoveTo(Vector3 destination, float speedFraction) {
			navMeshAgent.destination = destination;
			navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
			navMeshAgent.isStopped = false;
		}

		public void Cancel() {
			navMeshAgent.isStopped = true;
		}

		void UpdateAnimator() {
			Vector3 velocity = navMeshAgent.velocity;
			Vector3 localVelocity = transform.InverseTransformDirection(velocity);
			float speed = localVelocity.z;
			GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
		}
	}
}