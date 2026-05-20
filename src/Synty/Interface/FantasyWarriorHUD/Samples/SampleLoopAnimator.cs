using System;
using System.Collections;
using UnityEngine;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
	// Token: 0x0200045F RID: 1119
	public class SampleLoopAnimator : MonoBehaviour
	{
		// Token: 0x060018F8 RID: 6392 RVA: 0x0009C31B File Offset: 0x0009A51B
		private void Awake()
		{
			if (this.animator == null)
			{
				this.animator = base.GetComponent<Animator>();
			}
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0009C337 File Offset: 0x0009A537
		private void Reset()
		{
			this.animator = base.GetComponent<Animator>();
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0009C345 File Offset: 0x0009A545
		private void OnEnable()
		{
			if (this.animator == null)
			{
				return;
			}
			base.StartCoroutine(this.C_TweenBackAndForth());
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0009C363 File Offset: 0x0009A563
		private IEnumerator C_TweenBackAndForth()
		{
			yield return new WaitForSeconds(this.startDelay);
			for (;;)
			{
				yield return this.C_TweenFloat(0f, 1f, this.inSpeed);
				yield return new WaitForSeconds(this.outDelay);
				yield return this.C_TweenFloat(1f, 0f, this.outSpeed);
				yield return new WaitForSeconds(this.inDelay);
			}
			yield break;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0009C372 File Offset: 0x0009A572
		private IEnumerator C_TweenFloat(float startValue, float endValue, float duration)
		{
			float time = 0f;
			while (time < 1f)
			{
				time += Time.deltaTime / duration;
				float value = Mathf.Lerp(startValue, endValue, time);
				this.animator.SetFloat(this.parameterName, value);
				yield return null;
			}
			yield break;
		}

		// Token: 0x0400186D RID: 6253
		[Header("References")]
		public Animator animator;

		// Token: 0x0400186E RID: 6254
		[Header("Parameters")]
		public string parameterName = "Health";

		// Token: 0x0400186F RID: 6255
		public float inSpeed = 5f;

		// Token: 0x04001870 RID: 6256
		public float outSpeed = 5f;

		// Token: 0x04001871 RID: 6257
		public float startDelay;

		// Token: 0x04001872 RID: 6258
		public float inDelay = 2.5f;

		// Token: 0x04001873 RID: 6259
		public float outDelay = 2.5f;
	}
}
