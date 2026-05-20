using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.Samples
{
	// Token: 0x02000478 RID: 1144
	public class SampleRadialFillBar : MonoBehaviour
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x0009D7F8 File Offset: 0x0009B9F8
		public string LabelText
		{
			get
			{
				return string.Format(this.labelText, (this.image.fillAmount / this.fillAmountFull * 100f).ToString("0"));
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0009D835 File Offset: 0x0009BA35
		private void Awake()
		{
			if (this.image == null)
			{
				this.image = base.GetComponentInChildren<Image>();
			}
			if (this.text == null)
			{
				this.text = base.GetComponentInChildren<TMP_Text>();
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0009D86B File Offset: 0x0009BA6B
		private void Reset()
		{
			this.image = base.GetComponentInChildren<Image>();
			this.text = base.GetComponentInChildren<TMP_Text>();
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0009D885 File Offset: 0x0009BA85
		private void Start()
		{
			base.StartCoroutine(this.C_TweenBackAndForth());
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0009D894 File Offset: 0x0009BA94
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

		// Token: 0x0600197E RID: 6526 RVA: 0x0009D8A3 File Offset: 0x0009BAA3
		private IEnumerator C_TweenFloat(float startValue, float endValue, float duration)
		{
			float time = 0f;
			while (time < 1f)
			{
				time += Time.deltaTime / duration;
				float num = Mathf.Lerp(startValue, endValue, time);
				this.image.fillAmount = num * this.fillAmountFull;
				TMP_Text tmp_Text = this.text;
				if (tmp_Text != null)
				{
					tmp_Text.SetText(this.LabelText, true);
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x040018E7 RID: 6375
		[Header("References")]
		public Image image;

		// Token: 0x040018E8 RID: 6376
		public TMP_Text text;

		// Token: 0x040018E9 RID: 6377
		[Header("Parameters")]
		public float fillAmountFull = 1f;

		// Token: 0x040018EA RID: 6378
		public float inSpeed = 5f;

		// Token: 0x040018EB RID: 6379
		public float outSpeed = 5f;

		// Token: 0x040018EC RID: 6380
		public float startDelay;

		// Token: 0x040018ED RID: 6381
		public float inDelay = 2.5f;

		// Token: 0x040018EE RID: 6382
		public float outDelay = 2.5f;

		// Token: 0x040018EF RID: 6383
		public string labelText = "{0}%";
	}
}
