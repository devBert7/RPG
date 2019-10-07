using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics {
	public class CinematicsControlRemover : MonoBehaviour {
		PlayableDirector director;
		void Start() {
			director = GetComponent<PlayableDirector>();
			director.played += DisableControl;
			director.stopped += EnableControl;
		}

		void DisableControl(PlayableDirector disable) {
			print("Disable Control");
		}

		void EnableControl(PlayableDirector enable) {
			print("Enable Control");
		}
	}
}