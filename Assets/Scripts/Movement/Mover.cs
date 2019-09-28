using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour {

	void Update() {
		UpdateAnimator();
	}

	public void MoveTo(Vector3 destination) {
		GetComponent<NavMeshAgent>().destination = destination;
	}

	void UpdateAnimator() {
		Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
		Vector3 localVelocity = transform.InverseTransformDirection(velocity);
		float speed = localVelocity.z;
		GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
	}
}