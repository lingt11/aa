using System;
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.Samples
{
	// Token: 0x0200047E RID: 1150
	public class SampleScrollUV : MonoBehaviour
	{
		// Token: 0x060019A3 RID: 6563 RVA: 0x0009DEEA File Offset: 0x0009C0EA
		private void Awake()
		{
			if (this.rawImage == null)
			{
				this.rawImage = base.GetComponent<RawImage>();
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0009DF06 File Offset: 0x0009C106
		private void Reset()
		{
			this.rawImage = base.GetComponent<RawImage>();
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0009DF14 File Offset: 0x0009C114
		private void Update()
		{
			Vector2 vector = new Vector2(this.rawImage.rectTransform.rect.width / this.size.x, this.rawImage.rectTransform.rect.height / this.size.y);
			this.rawImage.uvRect = new Rect(this.rawImage.uvRect.position + this.speed * Time.deltaTime, vector);
		}

		// Token: 0x0400190A RID: 6410
		[Header("References")]
		public RawImage rawImage;

		// Token: 0x0400190B RID: 6411
		[Header("Parameters")]
		public Vector2 speed = new Vector2(1f, 0f);

		// Token: 0x0400190C RID: 6412
		public Vector2 size = new Vector2(256f, 256f);
	}
}
