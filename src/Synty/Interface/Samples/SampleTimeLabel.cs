using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Synty.Interface.Samples
{
	// Token: 0x0200047F RID: 1151
	public class SampleTimeLabel : MonoBehaviour
	{
		// Token: 0x060019A7 RID: 6567 RVA: 0x0009DFDB File Offset: 0x0009C1DB
		private void Awake()
		{
			if (this.label == null)
			{
				this.label = base.GetComponent<TMP_Text>();
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0009DFF7 File Offset: 0x0009C1F7
		private void OnEnable()
		{
			base.StartCoroutine(this.C_UpdateTime());
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0009E006 File Offset: 0x0009C206
		private void OnDisable()
		{
			base.StopCoroutine(this.C_UpdateTime());
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0009E014 File Offset: 0x0009C214
		public string GetCurrentTimeString()
		{
			if (!this.is24Hour)
			{
				return DateTime.Now.ToString("hh:mm tt");
			}
			if (this.beat)
			{
				return DateTime.Now.ToString("HH<color=#AAAAAA>:</color>mm");
			}
			return DateTime.Now.ToString("HH:mm");
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0009E069 File Offset: 0x0009C269
		[ContextMenu("Update Time")]
		public void UpdateTime()
		{
			this.label.SetText(this.GetCurrentTimeString(), true);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0009E07D File Offset: 0x0009C27D
		private IEnumerator C_UpdateTime()
		{
			for (;;)
			{
				this.UpdateTime();
				this.beat = !this.beat;
				yield return new WaitForSecondsRealtime(this.timeToRefreshInSeconds);
			}
			yield break;
		}

		// Token: 0x0400190D RID: 6413
		[Header("References")]
		public TMP_Text label;

		// Token: 0x0400190E RID: 6414
		[Header("Parameters")]
		public bool is24Hour = true;

		// Token: 0x0400190F RID: 6415
		public float timeToRefreshInSeconds = 1f;

		// Token: 0x04001910 RID: 6416
		private bool beat;
	}
}
