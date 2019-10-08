using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics {
	public class CinematicsControlRemover : MonoBehaviour {
		PlayableDirector director;
		GameObject player;

		void Start() {
			director = GetComponent<PlayableDirector>();
			director.played += DisableControl;
			director.stopped += EnableControl;
			player = GameObject.FindWithTag("Player");
		}

		void DisableControl(PlayableDirector director) {
			player.GetComponent<ActionScheduler>().CancelCurrentAction();
			player.GetComponent<PlayerController>().enabled = false;
		}

		void EnableControl(PlayableDirector director) {
			player.GetComponent<PlayerController>().enabled = true;
		}
	}
}