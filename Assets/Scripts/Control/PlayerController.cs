using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.control {
	public class PlayerController : MonoBehaviour {
		void Update() {
			if (CombatInteraction()) {
				return;
			}
			
			if (MovementInteraction()) {
				return;
			}			
		}

		bool CombatInteraction() {
			RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
			foreach(RaycastHit hit in hits) {
				CombatTarget target = hit.transform.GetComponent<CombatTarget>();

				if (target == null) {
					continue;
				}

				if (!GetComponent<Fighter>().CanAttack(target.gameObject)) {
					continue;
				}

				if (Input.GetMouseButtonDown(0)) {
					GetComponent<Fighter>().Attack(target.gameObject);
				}
				
				return true;
			}

			return false;
		}

		bool MovementInteraction() {
			RaycastHit hit;
			bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

			if (hasHit) {
				if (Input.GetMouseButton(0)) {
					GetComponent<Mover>().StartMovementAction(hit.point);
				}

				return true;
			}

			return false;
		}

		static Ray GetMouseRay() {
			return Camera.main.ScreenPointToRay(Input.mousePosition);
		}
	}
}