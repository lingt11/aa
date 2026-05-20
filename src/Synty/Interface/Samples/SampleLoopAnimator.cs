using System;
using System.Collections;
using UnityEngine;

namespace Synty.Interface.Samples
{
	// Token: 0x02000474 RID: 1140
	public class SampleLoopAnimator : MonoBehaviour
	{
		// Token: 0x06001963 RID: 6499 RVA: 0x0009D4C0 File Offset: 0x0009B6C0
		private void Awake()
		{
			if (this.animator == null)
			{
				this.animator = base.GetComponent<Animator>();
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0009D4DC File Offset: 0x0009B6DC
		private void Reset()
		{
			this.animator = base.GetComponent<Animator>();
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0009D4EA File Offset: 0x0009B6EA
		private void OnEnable()
		{
			if (this.animator == null)
			{
				return;
			}
			base.StartCoroutine(this.C_TweenBackAndForth());
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0009D508 File Offset: 0x0009B708
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

		// Token: 0x06001967 RID: 6503 RVA: 0x0009D517 File Offset: 0x0009B717
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

		// Token: 0x040018D2 RID: 6354
		[Header("References")]
		public Animator animator;

		// Token: 0x040018D3 RID: 6355
		[Header("Parameters")]
		public string parameterName = "Health";

		// Token: 0x040018D4 RID: 6356
		public float inSpeed = 5f;

		// Token: 0x040018D5 RID: 6357
		public float outSpeed = 5f;

		// Token: 0x040018D6 RID: 6358
		public float startDelay;

		// Token: 0x040018D7 RID: 6359
		public float inDelay = 2.5f;

		// Token: 0x040018D8 RID: 6360
		public float outDelay = 2.5f;
	}
}
