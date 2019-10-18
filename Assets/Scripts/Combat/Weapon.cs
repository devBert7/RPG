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

		const string weaponName = "Weapon";

		public void Spawn(Transform rightHand, Transform leftHand, Animator animator) {
			DestroyOldWeapon(rightHand, leftHand);

			if (equippedPrefab != null) {				
				GameObject weapon = Instantiate(equippedPrefab, GetTransform(rightHand, leftHand));
				weapon.name = weaponName;
			}
			
			var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

			if (animatorOverride != null) {
				animator.runtimeAnimatorController = animatorOverride;
			} else if (overrideController != null) {
				animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
			}
		}

		void DestroyOldWeapon(Transform rightHand, Transform leftHand) {
			Transform oldWeapon = rightHand.Find(weaponName);

			if (oldWeapon == null) {
				oldWeapon = leftHand.Find(weaponName);
			}

			if (oldWeapon == null) {
				return;
			}

			oldWeapon.name = "Destroy";
			Destroy(oldWeapon.gameObject);
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
			projectileInstance.SetTarget(target, weaponDamage);
		}

		public float WeaponRange() {
			return weaponRange;
		}

		public float WeaponDamage() {
			return weaponDamage;
		}
	}
}