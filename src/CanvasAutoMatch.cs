using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000E RID: 14
public class CanvasAutoMatch : MonoBehaviour
{
	// Token: 0x0600002C RID: 44 RVA: 0x00002D36 File Offset: 0x00000F36
	private void Start()
	{
		if (this.scaler != null)
		{
			this.Match();
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002D4C File Offset: 0x00000F4C
	private void Match()
	{
		this.scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		this.scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
		Vector2 referenceResolution = this.scaler.referenceResolution;
		float num = referenceResolution.x / referenceResolution.y;
		if ((float)Screen.width / (float)Screen.height >= num)
		{
			this.scaler.matchWidthOrHeight = 1f;
			return;
		}
		this.scaler.matchWidthOrHeight = 0f;
	}

	// Token: 0x0400003D RID: 61
	public CanvasScaler scaler;
}
