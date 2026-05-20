using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Synty.Interface.Samples
{
	// Token: 0x02000470 RID: 1136
	public class SampleButtonAction : MonoBehaviour
	{
		// Token: 0x0600194B RID: 6475 RVA: 0x0009D0C4 File Offset: 0x0009B2C4
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

		// Token: 0x0600194C RID: 6476 RVA: 0x0009D116 File Offset: 0x0009B316
		private void Reset()
		{
			this.button = base.GetComponent<Button>();
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0009D124 File Offset: 0x0009B324
		private void OnEnable()
		{
			if (this.runOnEnable)
			{
				this.OnClick();
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0009D134 File Offset: 0x0009B334
		private void OnClick()
		{
			if (this.activateObject)
			{
				base.StartCoroutine(this.C_ActivateObject());
			}
			foreach (GameObject gameObject in this.toggleObjects)
			{
				gameObject.SetActive(!gameObject.activeSelf);
			}
			foreach (AnimatorActionData animatorActionData in this.animatorActions)
			{
				animatorActionData.Execute();
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0009D1E8 File Offset: 0x0009B3E8
		private IEnumerator C_ActivateObject()
		{
			if (this.activateObject == null)
			{
				yield break;
			}
			this.activateObject.SetActive(true);
			if (this.applyRandomRotationToActivateObject)
			{
				this.activateObject.transform.localRotation = Quaternion.Euler(0f, 0f, (float)Random.Range(0, 360));
			}
			yield return new WaitForSeconds(this.activeTime);
			this.activateObject.SetActive(false);
			yield break;
		}

		// Token: 0x040018BE RID: 6334
		[Header("References")]
		public Button button;

		// Token: 0x040018BF RID: 6335
		public List<GameObject> toggleObjects;

		// Token: 0x040018C0 RID: 6336
		public GameObject activateObject;

		// Token: 0x040018C1 RID: 6337
		[Header("Parameters")]
		public List<AnimatorActionData> animatorActions;

		// Token: 0x040018C2 RID: 6338
		public float activeTime = 1f;

		// Token: 0x040018C3 RID: 6339
		public bool runOnEnable;

		// Token: 0x040018C4 RID: 6340
		public bool applyRandomRotationToActivateObject;
	}
}
