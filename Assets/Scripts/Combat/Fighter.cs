using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
	public class Fighter : MonoBehaviour, IAction {
		[SerializeField] float timeBetweenAttacks = 1f;
		[SerializeField] Transform rightHandTransform = null;
		[SerializeField] Transform leftHandTransform = null;
		[SerializeField] Weapon defaultWeapon = null;

		Health target;
		float timeSinceLastAttack = Mathf.Infinity;
		Mover mover;
		Weapon currentWeapon = null;

		void Start() {
			mover = GetComponent<Mover>();
			EquipWeapon(defaultWeapon);
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

		public void EquipWeapon(Weapon weapon) {
			currentWeapon = weapon;
			Animator animator = GetComponent<Animator>();
			weapon.Spawn(rightHandTransform, leftHandTransform, animator);
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

			if (currentWeapon.HasProjectile()) {
				currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
			} else {
				target.TakeDamage(currentWeapon.WeaponDamage());
			}			
		}

		// Animation Event
		void Shoot() {
			Hit();
		}

		bool GetIsInRange() {
			return Vector3.Distance(target.transform.position, transform.position) < currentWeapon.WeaponRange();
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
			GetComponent<Mover>().Cancel();
		}
	}
}