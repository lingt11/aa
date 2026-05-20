using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Synty.Interface.Samples
{
	// Token: 0x02000472 RID: 1138
	public class SampleCountdownLabel : MonoBehaviour
	{
		// Token: 0x06001957 RID: 6487 RVA: 0x0009D2CC File Offset: 0x0009B4CC
		private void Reset()
		{
			this.text = base.GetComponentInChildren<TMP_Text>();
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0009D2DA File Offset: 0x0009B4DA
		private void OnEnable()
		{
			this.BeginTimer();
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0009D2E2 File Offset: 0x0009B4E2
		private void BeginTimer()
		{
			this.currentTime = this.countdownTime;
			this.RefreshUI();
			base.StartCoroutine(this.C_TickDown());
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0009D303 File Offset: 0x0009B503
		private IEnumerator C_TickDown()
		{
			while (this.currentTime > 0f)
			{
				yield return new WaitForSeconds(this.updateInterval);
				this.currentTime -= this.updateInterval;
				if (this.currentTime <= 0f)
				{
					this.currentTime = 0f;
				}
				this.RefreshUI();
			}
			Animator animator = this.animator;
			if (animator != null)
			{
				animator.gameObject.SetActive(true);
			}
			Animator animator2 = this.animator;
			if (animator2 != null)
			{
				animator2.SetBool("Active", true);
			}
			yield return new WaitForSeconds(this.timeUpDuration);
			Animator animator3 = this.animator;
			if (animator3 != null)
			{
				animator3.SetBool("Active", false);
			}
			yield return new WaitForSeconds(1f);
			Animator animator4 = this.animator;
			if (animator4 != null)
			{
				animator4.gameObject.SetActive(false);
			}
			UnityEvent unityEvent = this.onCountdownComplete;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			this.BeginTimer();
			yield break;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0009D312 File Offset: 0x0009B512
		private void RefreshUI()
		{
			this.text.SetText(this.currentTime.ToString("F1"), true);
		}

		// Token: 0x040018C8 RID: 6344
		[Header("References")]
		public Animator animator;

		// Token: 0x040018C9 RID: 6345
		public TMP_Text text;

		// Token: 0x040018CA RID: 6346
		[Header("Parameters")]
		public float countdownTime = 30f;

		// Token: 0x040018CB RID: 6347
		public float updateInterval = 0.1f;

		// Token: 0x040018CC RID: 6348
		public float timeUpDuration = 2.5f;

		// Token: 0x040018CD RID: 6349
		public UnityEvent onCountdownComplete;

		// Token: 0x040018CE RID: 6350
		private float currentTime;
	}
}
