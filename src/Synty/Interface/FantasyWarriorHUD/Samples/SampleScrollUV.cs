using System;
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
	// Token: 0x02000468 RID: 1128
	public class SampleScrollUV : MonoBehaviour
	{
		// Token: 0x0600192E RID: 6446 RVA: 0x0009CB4D File Offset: 0x0009AD4D
		private void Awake()
		{
			if (this.rawImage == null)
			{
				this.rawImage = base.GetComponent<RawImage>();
			}
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0009CB69 File Offset: 0x0009AD69
		private void Reset()
		{
			this.rawImage = base.GetComponent<RawImage>();
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0009CB78 File Offset: 0x0009AD78
		private void Update()
		{
			Vector2 vector = new Vector2(this.rawImage.rectTransform.rect.width / this.size.x, this.rawImage.rectTransform.rect.height / this.size.y);
			this.rawImage.uvRect = new Rect(this.rawImage.uvRect.position + this.speed * Time.deltaTime, vector);
		}

		// Token: 0x0400189D RID: 6301
		[Header("References")]
		public RawImage rawImage;

		// Token: 0x0400189E RID: 6302
		[Header("Parameters")]
		public Vector2 speed = new Vector2(1f, 0f);

		// Token: 0x0400189F RID: 6303
		public Vector2 size = new Vector2(256f, 256f);
	}
}
