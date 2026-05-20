using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
	// Token: 0x0200046C RID: 1132
	public class SampleXPBar : MonoBehaviour
	{
		// Token: 0x06001941 RID: 6465 RVA: 0x0009CD8E File Offset: 0x0009AF8E
		private void Awake()
		{
			this.currentLevel = Random.Range(1, 69);
			this.currentXPNormalized = 0f;
			this.secondsPerLevelUp = Random.Range(4f, 20f);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0009CDC0 File Offset: 0x0009AFC0
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

		// Token: 0x06001943 RID: 6467 RVA: 0x0009CEB8 File Offset: 0x0009B0B8
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

		// Token: 0x040018A7 RID: 6311
		[Header("References")]
		public Animator animator;

		// Token: 0x040018A8 RID: 6312
		public Slider xpSlider;

		// Token: 0x040018A9 RID: 6313
		public TMP_Text xpText;

		// Token: 0x040018AA RID: 6314
		public TMP_Text levelText;

		// Token: 0x040018AB RID: 6315
		[Header("Parameters")]
		public int xpPerLevelUp = 1000;

		// Token: 0x040018AC RID: 6316
		private int currentLevel;

		// Token: 0x040018AD RID: 6317
		private float currentXPNormalized;

		// Token: 0x040018AE RID: 6318
		private float secondsPerLevelUp;
	}
}
