using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
	// Token: 0x0200045C RID: 1116
	public class SampleCountdownLabel : MonoBehaviour
	{
		// Token: 0x060018E6 RID: 6374 RVA: 0x0009C08F File Offset: 0x0009A28F
		private void Reset()
		{
			this.text = base.GetComponentInChildren<TMP_Text>();
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0009C09D File Offset: 0x0009A29D
		private IEnumerator Start()
		{
			yield return new WaitForSeconds(this.initialDelay);
			this.BeginTimer();
			yield break;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0009C0AC File Offset: 0x0009A2AC
		private void BeginTimer()
		{
			this.currentTime = this.countdownTime;
			this.RefreshUI();
			base.StartCoroutine(this.C_TickDown());
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0009C0CD File Offset: 0x0009A2CD
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
			if (this.setAnimatorActive)
			{
				Animator animator = this.animator;
				if (animator != null)
				{
					animator.gameObject.SetActive(true);
				}
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
			if (this.setAnimatorActive)
			{
				Animator animator4 = this.animator;
				if (animator4 != null)
				{
					animator4.gameObject.SetActive(false);
				}
			}
			UnityEvent unityEvent = this.onCountdownComplete;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			this.BeginTimer();
			yield break;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0009C0DC File Offset: 0x0009A2DC
		private void RefreshUI()
		{
			if (this.text)
			{
				this.text.SetText(this.currentTime.ToString("F1"), true);
			}
		}

		// Token: 0x0400185E RID: 6238
		[Header("References")]
		public Animator animator;

		// Token: 0x0400185F RID: 6239
		public TMP_Text text;

		// Token: 0x04001860 RID: 6240
		[Header("Parameters")]
		public bool setAnimatorActive = true;

		// Token: 0x04001861 RID: 6241
		public float initialDelay;

		// Token: 0x04001862 RID: 6242
		public float countdownTime = 30f;

		// Token: 0x04001863 RID: 6243
		public float updateInterval = 0.1f;

		// Token: 0x04001864 RID: 6244
		public float timeUpDuration = 2.5f;

		// Token: 0x04001865 RID: 6245
		public UnityEvent onCountdownComplete;

		// Token: 0x04001866 RID: 6246
		private float currentTime;
	}
}
