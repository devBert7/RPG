using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
	public class WeaponPickup : MonoBehaviour {
		[SerializeField] Weapon weapon = null;

		void OnTriggerEnter(Collider other) {
			if (other.tag == "Player") {
				print("Player Colliding With Sword");
				other.GetComponent<Fighter>().EquipWeapon(weapon);
				Destroy(gameObject);
			}
		}	
	}
}