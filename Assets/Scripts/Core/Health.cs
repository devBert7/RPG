﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core {
	public class Health : MonoBehaviour, ISaveable {
		[SerializeField] float health = 100f;

		bool isDead = false;

		public bool IsDead() {
			return isDead;
		}

		public void TakeDamage(float damage) {
			health = Mathf.Max(health - damage, 0);

			if (health == 0 && !isDead) {
				Die();
			}
		}

		void Die() {
			isDead = true;
			GetComponent<Animator>().SetTrigger("Die");
			GetComponent<ActionScheduler>().CancelCurrentAction();
		}

		public object CaptureState() {
			return health;
		}

		public void RestoreState(object state) {
			health = (float)state;

			if (health == 0 && !isDead) {
				Die();
			}
		}
	}
}