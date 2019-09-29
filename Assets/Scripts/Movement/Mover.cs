using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement {
	public class Mover : MonoBehaviour {
		 NavMeshAgent navMeshAgent;

		 void Start() {
		 	navMeshAgent = GetComponent<NavMeshAgent>();
		 }

		void Update() {
			UpdateAnimator();
		}

		public void MoveTo(Vector3 destination) {
			navMeshAgent.destination = destination;
			navMeshAgent.isStopped = false;
		}

		public void Stop() {
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