using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
	// Token: 0x02000462 RID: 1122
	public class SampleOscillateSliders : MonoBehaviour
	{
		// Token: 0x0600190A RID: 6410 RVA: 0x0009C5A4 File Offset: 0x0009A7A4
		private void Reset()
		{
			this.sliders = Object.FindObjectsOfType<Slider>().ToList<Slider>();
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0009C5B6 File Offset: 0x0009A7B6
		private void Start()
		{
			if (this.autoGetSliders)
			{
				this.sliders = Object.FindObjectsOfType<Slider>().ToList<Slider>();
			}
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0009C5D0 File Offset: 0x0009A7D0
		private void Update()
		{
			for (int i = 0; i < this.sliders.Count; i++)
			{
				this.sliders[i].value = Mathf.Sin(Time.time * this.speed + (float)i * this.offset) * 0.5f + 0.5f;
			}
		}

		// Token: 0x0400187E RID: 6270
		[Header("References")]
		public List<Slider> sliders;

		// Token: 0x0400187F RID: 6271
		[Header("Parameters")]
		public bool autoGetSliders = true;

		// Token: 0x04001880 RID: 6272
		public float speed = 1f;

		// Token: 0x04001881 RID: 6273
		public float offset = 0.5f;
	}
}
