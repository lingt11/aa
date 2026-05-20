using System;
using System.IO;
using UnityEngine;

// Token: 0x02000406 RID: 1030
public class RampGeneratorTDE : MonoBehaviour
{
	// Token: 0x06001796 RID: 6038 RVA: 0x000933EC File Offset: 0x000915EC
	private void Start()
	{
		switch (this.mode)
		{
		case RampGeneratorTDE.Mode.CreateAtStart:
			this.UpdateRampTexture();
			return;
		case RampGeneratorTDE.Mode.UpdateEveryFrame:
			this.UpdateRampTexture();
			break;
		case RampGeneratorTDE.Mode.BakeAndSaveAsTexture:
			break;
		default:
			return;
		}
	}

	// Token: 0x06001797 RID: 6039 RVA: 0x00093420 File Offset: 0x00091620
	private void Update()
	{
		switch (this.mode)
		{
		case RampGeneratorTDE.Mode.CreateAtStart:
		case RampGeneratorTDE.Mode.BakeAndSaveAsTexture:
			break;
		case RampGeneratorTDE.Mode.UpdateEveryFrame:
			this.UpdateRampTexture();
			break;
		default:
			return;
		}
	}

	// Token: 0x06001798 RID: 6040 RVA: 0x00093450 File Offset: 0x00091650
	private Texture2D GenerateTextureFromGradient(Gradient grad, float textureheight)
	{
		if (this.tempTexture == null)
		{
			this.tempTexture = new Texture2D((int)this.width, (int)textureheight);
		}
		int num = 0;
		while ((float)num < this.width)
		{
			int num2 = 0;
			while ((float)num2 < textureheight)
			{
				Color color = grad.Evaluate(0f + (float)num / this.width);
				this.tempTexture.SetPixel(num, num2, color);
				num2++;
			}
			num++;
		}
		this.tempTexture.wrapMode = TextureWrapMode.Clamp;
		this.tempTexture.Apply();
		return this.tempTexture;
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x000934E0 File Offset: 0x000916E0
	public void UpdateRampTexture()
	{
		this.rampTexture = this.GenerateTextureFromGradient(this.procedrualGradientRamp, this.height);
		Renderer[] array = this.renderers;
		for (int i = 0; i < array.Length; i++)
		{
			Material[] materials = array[i].materials;
			for (int j = 0; j < materials.Length; j++)
			{
				materials[j].SetTexture("_Ramp", this.rampTexture);
			}
		}
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x00093544 File Offset: 0x00091744
	public void BakeGradient()
	{
		this.rampTexture = this.GenerateTextureFromGradient(this.procedrualGradientRamp, 64f);
		byte[] bytes = this.rampTexture.EncodeToPNG();
		File.WriteAllBytes(string.Concat(new string[]
		{
			Application.dataPath,
			this.pathForPNG,
			"GeneratedRamp_",
			Random.Range(0, 99999).ToString(),
			".png"
		}), bytes);
	}

	// Token: 0x0400167B RID: 5755
	public Gradient procedrualGradientRamp;

	// Token: 0x0400167C RID: 5756
	public string pathForPNG = "/SineVFX/TopDownEffects/Recources/Textures/ForVFX/RampsGenerated/";

	// Token: 0x0400167D RID: 5757
	public Renderer[] renderers;

	// Token: 0x0400167E RID: 5758
	public RampGeneratorTDE.Mode mode;

	// Token: 0x0400167F RID: 5759
	private Texture2D rampTexture;

	// Token: 0x04001680 RID: 5760
	private Texture2D tempTexture;

	// Token: 0x04001681 RID: 5761
	private float width = 256f;

	// Token: 0x04001682 RID: 5762
	private float height = 64f;

	// Token: 0x02000407 RID: 1031
	public enum Mode
	{
		// Token: 0x04001684 RID: 5764
		CreateAtStart,
		// Token: 0x04001685 RID: 5765
		UpdateEveryFrame,
		// Token: 0x04001686 RID: 5766
		BakeAndSaveAsTexture
	}
}
