using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
	public class Fighter : MonoBehaviour, IAction {
		[SerializeField] float weaponRange = 2f;
		[SerializeField] float timeBetweenAttacks = 1f;
		[SerializeField] float weaponDamage = 10f;

		Health target;
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

			if (target.IsDead()) {
				return;
			}

			if (!GetIsInRange()) {
				mover.MoveTo(target.transform.position);
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

		// Animation Event
		void Hit() {
			target.TakeDamage(weaponDamage);
		}

		bool GetIsInRange() {
			return Vector3.Distance(target.transform.position, transform.position) < weaponRange;
		}

		public void Attack(CombatTarget combatTarget) {
			GetComponent<ActionScheduler>().StartAction(this);
			target = combatTarget.GetComponent<Health>();
		}

		public void Cancel() {
			GetComponent<Animator>().SetTrigger("StopAttack");
			target = null;
		}
	}
}