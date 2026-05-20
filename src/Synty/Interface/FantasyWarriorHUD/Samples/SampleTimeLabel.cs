using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
	// Token: 0x02000469 RID: 1129
	public class SampleTimeLabel : MonoBehaviour
	{
		// Token: 0x06001932 RID: 6450 RVA: 0x0009CC3F File Offset: 0x0009AE3F
		private void Awake()
		{
			if (this.label == null)
			{
				this.label = base.GetComponent<TMP_Text>();
			}
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0009CC5B File Offset: 0x0009AE5B
		private void OnEnable()
		{
			base.StartCoroutine(this.C_UpdateTime());
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0009CC6A File Offset: 0x0009AE6A
		private void OnDisable()
		{
			base.StopCoroutine(this.C_UpdateTime());
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0009CC78 File Offset: 0x0009AE78
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

		// Token: 0x06001936 RID: 6454 RVA: 0x0009CCCD File Offset: 0x0009AECD
		[ContextMenu("Update Time")]
		public void UpdateTime()
		{
			this.label.SetText(this.GetCurrentTimeString(), true);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0009CCE1 File Offset: 0x0009AEE1
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

		// Token: 0x040018A0 RID: 6304
		[Header("References")]
		public TMP_Text label;

		// Token: 0x040018A1 RID: 6305
		[Header("Parameters")]
		public bool is24Hour = true;

		// Token: 0x040018A2 RID: 6306
		public float timeToRefreshInSeconds = 1f;

		// Token: 0x040018A3 RID: 6307
		private bool beat;
	}
}
