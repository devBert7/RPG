using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement {
	public class SavingWrapper : MonoBehaviour {
		[SerializeField] float fadeInTime = 1f;

		const string defaultSaveFile = "save";

		SavingSystem saveSys;

		IEnumerator Start() {
			Fader fader = FindObjectOfType<Fader>();
			fader.FadeOutImmediate();
			saveSys = GetComponent<SavingSystem>();
			yield return saveSys.LoadLastScene(defaultSaveFile);
			yield return fader.FadeIn(fadeInTime);
		}

		void Update() {
			if (Input.GetKeyDown(KeyCode.L)) {
				Load();
			}
			
			if (Input.GetKeyDown(KeyCode.S)) {
				Save();
			}
		}

		public void Load() {
			saveSys.Load(defaultSaveFile);
		}

		public void Save() {
			saveSys.Save(defaultSaveFile);
		}
	}
}