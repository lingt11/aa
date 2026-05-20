using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Token: 0x0200006E RID: 110
public class GlobalVolumeController : MonoBehaviour
{
	// Token: 0x06000229 RID: 553 RVA: 0x0000C0CC File Offset: 0x0000A2CC
	private void Awake()
	{
		GlobalVolumeController.Instance = this;
		this.globalVolume.profile.TryGet<ColorAdjustments>(out this.colorAdjustments);
		this.globalVolume.profile.TryGet<Bloom>(out this.bloom);
		float @float = PlayerPrefs.GetFloat("settings.viewlight", 1f);
		float float2 = PlayerPrefs.GetFloat("settings.effectlight", 1f);
		this.SetBrightness(@float);
		this.SetBloom(float2);
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0000C13B File Offset: 0x0000A33B
	public void SetBrightness(float value)
	{
		if (this.colorAdjustments != null)
		{
			this.colorAdjustments.postExposure.value = Mathf.Lerp(-2f, 0f, value);
		}
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0000C16B File Offset: 0x0000A36B
	public void SetBloom(float value)
	{
		if (this.colorAdjustments != null)
		{
			this.bloom.intensity.value = Mathf.Lerp(0f, 5f, value);
		}
	}

	// Token: 0x04000242 RID: 578
	public static GlobalVolumeController Instance;

	// Token: 0x04000243 RID: 579
	public Volume globalVolume;

	// Token: 0x04000244 RID: 580
	private ColorAdjustments colorAdjustments;

	// Token: 0x04000245 RID: 581
	private Bloom bloom;
}
