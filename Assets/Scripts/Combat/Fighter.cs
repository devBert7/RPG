﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
	public class Fighter : MonoBehaviour, IAction {
		[SerializeField] float weaponRange = 2f;

		Transform target;
		Mover mover;

		void Start() {
			mover = GetComponent<Mover>();
		}

		void Update() {
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
			GetComponent<Animator>().SetTrigger("Attack");
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