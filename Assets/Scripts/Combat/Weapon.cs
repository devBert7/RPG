using UnityEngine;
using RPG.Core;

namespace RPG.Combat {
	[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
	public class Weapon : ScriptableObject {
		[SerializeField] AnimatorOverrideController animatorOverride = null;
		[SerializeField] GameObject equippedPrefab = null;
		[SerializeField] float weaponRange = 1f;
		[SerializeField] float weaponDamage = 10f;
		[SerializeField] bool isRightHanded = true;
		[SerializeField] Projectile projectile = null;

		public void Spawn(Transform rightHand, Transform leftHand, Animator animator) {
			if (equippedPrefab != null) {				
				Instantiate(equippedPrefab, GetTransform(rightHand, leftHand));
			}

			if (animatorOverride != null) {
				animator.runtimeAnimatorController = animatorOverride;
			}
		}

		Transform GetTransform(Transform rightHand, Transform leftHand) {
			Transform handTransform;

			if (isRightHanded) {
				handTransform = rightHand;
			} else {
				handTransform = leftHand;
			}

			return handTransform;
		}

		public bool HasProjectile() {
			return projectile != null;
		}

		public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target) {
			if (projectile == null) {
				return;
			}
			
			Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
			projectileInstance.SetTarget(target);
		}

		public float WeaponRange() {
			return weaponRange;
		}

		public float WeaponDamage() {
			return weaponDamage;
		}
	}
}