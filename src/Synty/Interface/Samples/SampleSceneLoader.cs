using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Synty.Interface.Samples
{
	// Token: 0x0200047B RID: 1147
	public class SampleSceneLoader : MonoBehaviour
	{
		// Token: 0x0600198C RID: 6540 RVA: 0x0009DB08 File Offset: 0x0009BD08
		private void Awake()
		{
			this.contentList = (from screen in this.contentParent.GetComponentsInChildren<RectTransform>(true)
			where screen.parent == this.contentParent
			select screen).ToList<RectTransform>();
			this.contentList.Insert(0, null);
			this.titleScreen.gameObject.SetActive(true);
			this.contentsScreen.gameObject.SetActive(false);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0009DB6C File Offset: 0x0009BD6C
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

		// Token: 0x0600198E RID: 6542 RVA: 0x0009C9B0 File Offset: 0x0009ABB0
		public void QuitApplication()
		{
			Application.Quit();
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0009DBBC File Offset: 0x0009BDBC
		public void ActivateContent(int index)
		{
			base.StartCoroutine(this.C_ActivateContent(index));
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0009DBCC File Offset: 0x0009BDCC
		private IEnumerator C_ActivateContent(int index)
		{
			this.canvasGroup.interactable = false;
			if (this.animator)
			{
				this.animator.gameObject.SetActive(true);
				this.animator.SetBool("Active", true);
				yield return new WaitForSeconds(0.4f);
				this.animator.SetBool("Active", false);
			}
			foreach (RectTransform rectTransform in this.contentList)
			{
				if (rectTransform)
				{
					rectTransform.gameObject.SetActive(false);
				}
			}
			this.currentContent = this.contentList[index];
			this.currentContent.gameObject.SetActive(true);
			this.titleText.text = this.currentContent.name;
			this.titleScreen.gameObject.SetActive(false);
			this.contentsScreen.gameObject.SetActive(true);
			this.canvasGroup.interactable = true;
			yield break;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0009DBE4 File Offset: 0x0009BDE4
		public void ActivateNextContent()
		{
			int num = this.contentList.IndexOf(this.currentContent) + 1;
			if (num >= this.contentList.Count)
			{
				num = 1;
			}
			this.ActivateContent(num);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0009DC1C File Offset: 0x0009BE1C
		public void ActivatePreviousContent()
		{
			int num = this.contentList.IndexOf(this.currentContent) - 1;
			if (num < 1)
			{
				num = this.contentList.Count - 1;
			}
			this.ActivateContent(num);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0009DC56 File Offset: 0x0009BE56
		public void ActivateTitleScreen()
		{
			base.StartCoroutine(this.C_ActivateTitleScreen());
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0009DC65 File Offset: 0x0009BE65
		private IEnumerator C_ActivateTitleScreen()
		{
			this.canvasGroup.interactable = false;
			if (this.animator)
			{
				this.animator.gameObject.SetActive(true);
				this.animator.SetBool("Active", true);
				yield return new WaitForSeconds(0.4f);
				this.animator.SetBool("Active", false);
			}
			this.titleScreen.gameObject.SetActive(true);
			this.contentsScreen.gameObject.SetActive(false);
			this.canvasGroup.interactable = true;
			yield break;
		}

		// Token: 0x040018FA RID: 6394
		[Header("References")]
		public Animator animator;

		// Token: 0x040018FB RID: 6395
		public CanvasGroup canvasGroup;

		// Token: 0x040018FC RID: 6396
		public RectTransform titleScreen;

		// Token: 0x040018FD RID: 6397
		public RectTransform contentsScreen;

		// Token: 0x040018FE RID: 6398
		public TextMeshProUGUI titleText;

		// Token: 0x040018FF RID: 6399
		public RectTransform contentParent;

		// Token: 0x04001900 RID: 6400
		[Header("Parameters")]
		public bool showCursor;

		// Token: 0x04001901 RID: 6401
		private List<RectTransform> contentList = new List<RectTransform>();

		// Token: 0x04001902 RID: 6402
		private RectTransform currentContent;
	}
}
