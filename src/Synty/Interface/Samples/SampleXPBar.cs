using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.Samples
{
	// Token: 0x02000482 RID: 1154
	public class SampleXPBar : MonoBehaviour
	{
		// Token: 0x060019B6 RID: 6582 RVA: 0x0009E122 File Offset: 0x0009C322
		private void Awake()
		{
			this.currentLevel = Random.Range(1, 69);
			this.currentXPNormalized = 0f;
			this.secondsPerLevelUp = Random.Range(4f, 20f);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0009E154 File Offset: 0x0009C354
		private void Reset()
		{
			List<RectTransform> list = new List<RectTransform>();
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				if (transform is RectTransform)
				{
					list.Add(transform as RectTransform);
				}
			}
			RectTransform rectTransform = list.SingleOrDefault((RectTransform c) => c.name.ToLower().Contains("xp"));
			if (rectTransform)
			{
				this.xpSlider = rectTransform.GetComponentInChildren<Slider>();
				this.xpText = rectTransform.transform.GetComponentInChildren<TMP_Text>();
			}
			RectTransform rectTransform2 = list.SingleOrDefault((RectTransform c) => c.name.ToLower().Contains("level"));
			if (rectTransform2)
			{
				this.levelText = rectTransform2.GetComponentInChildren<TMP_Text>();
			}
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0009E24C File Offset: 0x0009C44C
		private void Update()
		{
			if (this.xpSlider)
			{
				this.xpSlider.value = this.currentXPNormalized;
			}
			if (this.xpText)
			{
				this.xpText.text = string.Format("{0}/{1}", Mathf.RoundToInt(this.currentXPNormalized * (float)this.xpPerLevelUp), this.xpPerLevelUp);
			}
			if (this.levelText)
			{
				this.levelText.text = string.Format("{0}", this.currentLevel);
			}
			if (this.currentXPNormalized >= 1f)
			{
				this.currentLevel++;
				this.currentXPNormalized = 0f;
				if (this.animator)
				{
					this.animator.SetTrigger("LevelUp");
				}
			}
			this.currentXPNormalized += Time.deltaTime / this.secondsPerLevelUp;
		}

		// Token: 0x04001914 RID: 6420
		[Header("References")]
		public Animator animator;

		// Token: 0x04001915 RID: 6421
		public Slider xpSlider;

		// Token: 0x04001916 RID: 6422
		public TMP_Text xpText;

		// Token: 0x04001917 RID: 6423
		public TMP_Text levelText;

		// Token: 0x04001918 RID: 6424
		[Header("Parameters")]
		public int xpPerLevelUp = 1000;

		// Token: 0x04001919 RID: 6425
		private int currentLevel;

		// Token: 0x0400191A RID: 6426
		private float currentXPNormalized;

		// Token: 0x0400191B RID: 6427
		private float secondsPerLevelUp;
	}
}
