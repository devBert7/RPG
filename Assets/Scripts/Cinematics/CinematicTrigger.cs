using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics {
	public class CinematicTrigger : MonoBehaviour{
		bool hasTriggered = false;
		void OnTriggerEnter(Collider other) {
			if (GameObject.FindWithTag("Player") && !hasTriggered) {
				GetComponent<PlayableDirector>().Play();
				hasTriggered = true;
			}
		}
	}
}