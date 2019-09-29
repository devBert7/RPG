﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat {
	public class Fighter : MonoBehaviour {
		[SerializeField] float weaponRange = 2f;

		Transform target;
		Mover mover;

		void Start() {
			mover = GetComponent<Mover>();
		}

		void Update() {
			bool isInRange = Vector3.Distance(target.position, transform.position) < weaponRange;
			if (target && !isInRange) {
				mover.MoveTo(target.position);
			} else {
				mover.Stop();
			}
		}

		public void Attack(CombatTarget combatTarget) {
			target = combatTarget.transform;
		}

		public void Cancel() {
			target = null;
		}
	}
}