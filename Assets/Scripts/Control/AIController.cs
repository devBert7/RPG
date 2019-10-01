using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Control {
	public class AIController : MonoBehaviour {
		[SerializeField] float chaseDistance = 5f;

		Fighter fighter;
		GameObject player;

		void Start() {
			fighter = GetComponent<Fighter>();
			player = GameObject.FindWithTag("Player");
		}

		void Update() {
			GameObject player = GameObject.FindWithTag("Player");
			bool inAttackRange = Vector3.Distance(player.transform.position, transform.position) <= chaseDistance;

			if (inAttackRange && fighter.CanAttack(player)) {
				fighter.Attack(player);
			} else {
				fighter.Cancel();
			}
		}	
	}
}