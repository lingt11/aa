using System;
using UnityEngine;

// Token: 0x020003EF RID: 1007
[ExecuteInEditMode]
public class Fog : MonoBehaviour
{
	// Token: 0x0600173F RID: 5951 RVA: 0x00090DDC File Offset: 0x0008EFDC
	private void OnValidate()
	{
		Shader.SetGlobalColor(Fog.FogTopColor, this.fogTopColor);
		Shader.SetGlobalColor(Fog.FogColor, this.fogColor);
		Shader.SetGlobalFloat(Fog.FogGlobalDensity, this.fogDensity);
		Shader.SetGlobalFloat(Fog.FogFallOff, this.fogFalloff);
		Shader.SetGlobalFloat(Fog.FogHeight, this.fogHeight);
		Shader.SetGlobalFloat(Fog.FogStartDis, this.fogStartDis);
		Shader.SetGlobalFloat(Fog.FogInscatteringExp, this.fogInscatteringExp);
		Shader.SetGlobalFloat(Fog.FogGradientDis, this.fogGradientDis);
		if (this.enable)
		{
			Shader.EnableKeyword("_CUSTOM_FOG");
			return;
		}
		Shader.DisableKeyword("_CUSTOM_FOG");
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x00090E86 File Offset: 0x0008F086
	private void OnDestroy()
	{
		Shader.DisableKeyword("_CUSTOM_FOG");
	}

	// Token: 0x040015CF RID: 5583
	public bool enable;

	// Token: 0x040015D0 RID: 5584
	public Color fogTopColor;

	// Token: 0x040015D1 RID: 5585
	public Color fogColor;

	// Token: 0x040015D2 RID: 5586
	public float fogHeight;

	// Token: 0x040015D3 RID: 5587
	[Range(0f, 1f)]
	public float fogDensity;

	// Token: 0x040015D4 RID: 5588
	[Min(0f)]
	public float fogFalloff;

	// Token: 0x040015D5 RID: 5589
	public float fogStartDis;

	// Token: 0x040015D6 RID: 5590
	public float fogInscatteringExp;

	// Token: 0x040015D7 RID: 5591
	public float fogGradientDis;

	// Token: 0x040015D8 RID: 5592
	private static readonly int FogTopColor = Shader.PropertyToID("_FogTopColor");

	// Token: 0x040015D9 RID: 5593
	private static readonly int FogColor = Shader.PropertyToID("_FogColor");

	// Token: 0x040015DA RID: 5594
	private static readonly int FogGlobalDensity = Shader.PropertyToID("_FogGlobalDensity");

	// Token: 0x040015DB RID: 5595
	private static readonly int FogFallOff = Shader.PropertyToID("_FogFallOff");

	// Token: 0x040015DC RID: 5596
	private static readonly int FogHeight = Shader.PropertyToID("_FogHeight");

	// Token: 0x040015DD RID: 5597
	private static readonly int FogStartDis = Shader.PropertyToID("_FogStartDis");

	// Token: 0x040015DE RID: 5598
	private static readonly int FogInscatteringExp = Shader.PropertyToID("_FogInscatteringExp");

	// Token: 0x040015DF RID: 5599
	private static readonly int FogGradientDis = Shader.PropertyToID("_FogGradientDis");
}
