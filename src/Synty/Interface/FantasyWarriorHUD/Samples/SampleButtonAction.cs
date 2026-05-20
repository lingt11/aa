using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
	// Token: 0x0200045A RID: 1114
	public class SampleButtonAction : MonoBehaviour
	{
		// Token: 0x060018DA RID: 6362 RVA: 0x0009BE50 File Offset: 0x0009A050
		private void Awake()
		{
			if (this.button == null)
			{
				this.button = base.GetComponent<Button>();
			}
			if (this.button == null)
			{
				return;
			}
			this.button.onClick.AddListener(new UnityAction(this.OnClick));
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0009BEA2 File Offset: 0x0009A0A2
		private void Reset()
		{
			this.button = base.GetComponent<Button>();
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0009BEB0 File Offset: 0x0009A0B0
		private void OnEnable()
		{
			if (this.runOnEnable)
			{
				this.OnClick();
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0009BEC0 File Offset: 0x0009A0C0
		private void OnClick()
		{
			base.StartCoroutine(this.C_ActivateObject());
			foreach (GameObject gameObject in this.toggleObjects)
			{
				gameObject.SetActive(!gameObject.activeSelf);
			}
			foreach (SampleAnimatorActionData sampleAnimatorActionData in this.animatorActions)
			{
				sampleAnimatorActionData.Execute();
			}
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0009BF68 File Offset: 0x0009A168
		private IEnumerator C_ActivateObject()
		{
			if (this.button != null)
			{
				this.button.interactable = false;
			}
			if (this.activateObject != null)
			{
				this.activateObject.SetActive(true);
				if (this.applyRandomRotationToActivateObject)
				{
					this.activateObject.transform.localRotation = Quaternion.Euler(0f, 0f, (float)Random.Range(0, 360));
				}
			}
			yield return new WaitForSeconds(this.activeTime);
			if (this.button != null)
			{
				this.button.interactable = true;
			}
			if (this.activateObject != null)
			{
				this.activateObject.SetActive(false);
			}
			yield break;
		}

		// Token: 0x04001854 RID: 6228
		[Header("References")]
		public Button button;

		// Token: 0x04001855 RID: 6229
		public List<GameObject> toggleObjects;

		// Token: 0x04001856 RID: 6230
		public GameObject activateObject;

		// Token: 0x04001857 RID: 6231
		[Header("Parameters")]
		public List<SampleAnimatorActionData> animatorActions;

		// Token: 0x04001858 RID: 6232
		public float activeTime = 1f;

		// Token: 0x04001859 RID: 6233
		public bool runOnEnable;

		// Token: 0x0400185A RID: 6234
		public bool applyRandomRotationToActivateObject;
	}
}
