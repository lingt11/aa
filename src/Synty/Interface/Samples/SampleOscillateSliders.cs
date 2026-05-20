using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.Samples
{
	// Token: 0x02000477 RID: 1143
	public class SampleOscillateSliders : MonoBehaviour
	{
		// Token: 0x06001975 RID: 6517 RVA: 0x0009D74C File Offset: 0x0009B94C
		private void Reset()
		{
			this.sliders = Object.FindObjectsOfType<Slider>().ToList<Slider>();
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0009D75E File Offset: 0x0009B95E
		private void Start()
		{
			if (this.autoGetSliders)
			{
				this.sliders = Object.FindObjectsOfType<Slider>().ToList<Slider>();
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0009D778 File Offset: 0x0009B978
		private void Update()
		{
			for (int i = 0; i < this.sliders.Count; i++)
			{
				this.sliders[i].value = Mathf.Sin(Time.time * this.speed + (float)i * this.offset) * 0.5f + 0.5f;
			}
		}

		// Token: 0x040018E3 RID: 6371
		[Header("References")]
		public List<Slider> sliders;

		// Token: 0x040018E4 RID: 6372
		[Header("Parameters")]
		public bool autoGetSliders = true;

		// Token: 0x040018E5 RID: 6373
		public float speed = 1f;

		// Token: 0x040018E6 RID: 6374
		public float offset = 0.5f;
	}
}
