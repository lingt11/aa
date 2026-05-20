using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
	// Token: 0x02000466 RID: 1126
	public class SampleSceneLoader : MonoBehaviour
	{
		// Token: 0x06001921 RID: 6433 RVA: 0x0009C960 File Offset: 0x0009AB60
		private void OnEnable()
		{
			if (this.animator)
			{
				this.animator.gameObject.SetActive(true);
				this.animator.SetBool("Active", false);
			}
			if (this.showCursor)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0009C9B0 File Offset: 0x0009ABB0
		public void QuitApplication()
		{
			Application.Quit();
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0009C9B8 File Offset: 0x0009ABB8
		public void NextScene()
		{
			string name = SceneManager.GetActiveScene().name;
			this.SwitchScene(this._sceneNames[(this._sceneNames.IndexOf(name) + 1) % this._sceneNames.Count]);
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0009CA00 File Offset: 0x0009AC00
		public void PreviousScene()
		{
			string name = SceneManager.GetActiveScene().name;
			this.SwitchScene(this._sceneNames[(this._sceneNames.IndexOf(name) - 1 + this._sceneNames.Count) % this._sceneNames.Count]);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0009CA52 File Offset: 0x0009AC52
		public void SwitchScene(string sceneName)
		{
			base.StartCoroutine(this.C_SwitchScene(sceneName));
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0009CA62 File Offset: 0x0009AC62
		private IEnumerator C_SwitchScene(string sceneName)
		{
			if (this.animator)
			{
				this.animator.gameObject.SetActive(true);
				this.animator.SetBool("Active", true);
				yield return new WaitForSeconds(0.5f);
			}
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
			while (!asyncLoad.isDone)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x04001895 RID: 6293
		[Header("References")]
		public Animator animator;

		// Token: 0x04001896 RID: 6294
		[Header("Parameters")]
		public bool showCursor;

		// Token: 0x04001897 RID: 6295
		[SerializeField]
		private List<string> _sceneNames;
	}
}
