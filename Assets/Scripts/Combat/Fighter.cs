using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
	public class Fighter : MonoBehaviour, IAction {
		[SerializeField] float weaponRange = 2f;
		[SerializeField] float timeBetweenAttacks = 1f;

		Transform target;
		float timeSinceLastAttack;
		Mover mover;

		void Start() {
			mover = GetComponent<Mover>();
		}

		void Update() {
			timeSinceLastAttack += Time.deltaTime;
			if (target == null) {
				return;
			}

			if (!GetIsInRange()) {
				mover.MoveTo(target.position);
			} else {
				mover.Cancel();
				AttackBehaviour();
			}
		}

		void AttackBehaviour() {
			if (timeSinceLastAttack > timeBetweenAttacks) {
				GetComponent<Animator>().SetTrigger("Attack");
				timeSinceLastAttack = 0;
			}
		}

		bool GetIsInRange() {
			return Vector3.Distance(target.position, transform.position) < weaponRange;
		}

		public void Attack(CombatTarget combatTarget) {
			GetComponent<ActionScheduler>().StartAction(this);
			target = combatTarget.transform;
		}

		public void Cancel() {
			target = null;
		}

		// Animation Event
		void Hit() {
			
		}
	}
}