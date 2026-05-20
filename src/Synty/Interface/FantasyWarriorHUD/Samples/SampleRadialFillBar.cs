using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
	// Token: 0x02000463 RID: 1123
	public class SampleRadialFillBar : MonoBehaviour
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x0009C650 File Offset: 0x0009A850
		public string LabelText
		{
			get
			{
				return string.Format(this.labelText, (this.image.fillAmount / this.fillAmountFull * 100f).ToString("0"));
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0009C68D File Offset: 0x0009A88D
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

		// Token: 0x06001910 RID: 6416 RVA: 0x0009C6C3 File Offset: 0x0009A8C3
		private void Reset()
		{
			this.image = base.GetComponentInChildren<Image>();
			this.text = base.GetComponentInChildren<TMP_Text>();
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0009C6DD File Offset: 0x0009A8DD
		private void Start()
		{
			base.StartCoroutine(this.C_TweenBackAndForth());
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0009C6EC File Offset: 0x0009A8EC
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

		// Token: 0x06001913 RID: 6419 RVA: 0x0009C6FB File Offset: 0x0009A8FB
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

		// Token: 0x04001882 RID: 6274
		[Header("References")]
		public Image image;

		// Token: 0x04001883 RID: 6275
		public TMP_Text text;

		// Token: 0x04001884 RID: 6276
		[Header("Parameters")]
		public float fillAmountFull = 1f;

		// Token: 0x04001885 RID: 6277
		public float inSpeed = 5f;

		// Token: 0x04001886 RID: 6278
		public float outSpeed = 5f;

		// Token: 0x04001887 RID: 6279
		public float startDelay;

		// Token: 0x04001888 RID: 6280
		public float inDelay = 2.5f;

		// Token: 0x04001889 RID: 6281
		public float outDelay = 2.5f;

		// Token: 0x0400188A RID: 6282
		public string labelText = "{0}%";
	}
}
