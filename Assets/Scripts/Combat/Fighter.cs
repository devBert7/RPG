using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
	public class Fighter : MonoBehaviour, IAction {
		[SerializeField] float weaponRange = 1f;
		[SerializeField] float timeBetweenAttacks = 1f;
		[SerializeField] float weaponDamage = 10f;

		Health target;
		float timeSinceLastAttack = Mathf.Infinity;
		Mover mover;

		void Start() {
			mover = GetComponent<Mover>();
		}

		void Update() {
			timeSinceLastAttack += Time.deltaTime;

			if (target == null || target.IsDead()) {
				return;
			}

			transform.LookAt(target.transform);

			if (!GetIsInRange()) {
				mover.MoveTo(target.transform.position, 1f);
			} else {
				mover.Cancel();
				AttackBehaviour();
			}
		}

		void AttackBehaviour() {
			if (timeSinceLastAttack > timeBetweenAttacks) {
				GetComponent<Animator>().ResetTrigger("StopAttack");
				GetComponent<Animator>().SetTrigger("Attack");
				timeSinceLastAttack = 0;
			}
		}

		// Animation Event
		void Hit() {
			if (target == null) {
				return;
			}
			
			target.TakeDamage(weaponDamage);
		}

		bool GetIsInRange() {
			return Vector3.Distance(target.transform.position, transform.position) < weaponRange;
		}

		public bool CanAttack(GameObject combatTarget) {
			Health testTarget = combatTarget.GetComponent<Health>();

			return testTarget != null && !testTarget.IsDead();
		}

		public void Attack(GameObject combatTarget) {
			GetComponent<ActionScheduler>().StartAction(this);
			target = combatTarget.GetComponent<Health>();
		}

		public void Cancel() {
			GetComponent<Animator>().ResetTrigger("Attack");
			GetComponent<Animator>().SetTrigger("StopAttack");
			target = null;
		}
	}
}