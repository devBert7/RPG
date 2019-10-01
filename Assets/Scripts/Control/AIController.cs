using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control {
	public class AIController : MonoBehaviour {
		[SerializeField] float chaseDistance = 5f;

		void Update() {
			GameObject player = GameObject.FindWithTag("Player");
			bool isInRange = Vector3.Distance(player.transform.position, transform.position) <= chaseDistance;

			if (isInRange) {
				print(gameObject.name + " Should Chase Now");
			}
		}	
	}
}