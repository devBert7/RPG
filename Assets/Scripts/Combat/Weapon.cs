﻿using UnityEngine;

namespace RPG.Combat {
	[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
	public class Weapon : ScriptableObject {
		[SerializeField] AnimatorOverrideController animatorOverride = null;
		[SerializeField] GameObject equippedPrefab = null;
		[SerializeField] float weaponRange = 1f;
		[SerializeField] float weaponDamage = 10f;

		public void Spawn(Transform handTransform, Animator animator) {
			if (equippedPrefab != null) {
				Instantiate(equippedPrefab, handTransform);
			}
			if (animatorOverride != null) {
				animator.runtimeAnimatorController = animatorOverride;
			}
		}

		public float WeaponRange() {
			return weaponRange;
		}

		public float WeaponDamage() {
			return weaponDamage;
		}
	}
}