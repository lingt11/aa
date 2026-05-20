using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003F4 RID: 1012
public class ForceFieldController : MonoBehaviour
{
	// Token: 0x0600174F RID: 5967 RVA: 0x00091499 File Offset: 0x0008F699
	private void Start()
	{
		this.psmain = this.controlParticleSystem.main;
		this.GetRenderers();
		this.GetNumberOfSpheres();
		this.GetSphereArrays();
		this.ApplyMaterials();
		if (this.procedrualGradientEnabled)
		{
			this.UpdateRampTexture();
		}
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x000914D4 File Offset: 0x0008F6D4
	private void OnEnable()
	{
		this.psmain = this.controlParticleSystem.main;
		this.GetRenderers();
		this.GetNumberOfSpheres();
		this.GetSphereArrays();
		this.controlParticles = new ParticleSystem.Particle[this.affectorCount];
		this.controlParticlesPositions = new Vector4[this.affectorCount];
		this.controlParticlesSizes = new float[this.affectorCount];
		this.psmain.maxParticles = this.affectorCount;
		this.controlParticleSystem.GetParticles(this.controlParticles);
		for (int i = 0; i < this.affectorCount; i++)
		{
			this.controlParticlesPositions[i] = this.controlParticles[i].position;
			this.controlParticlesSizes[i] = this.controlParticles[i].GetCurrentSize(this.controlParticleSystem) * this.controlParticleSystem.transform.lossyScale.x;
		}
		this.OpenCloseProgress();
		this.UpdateHitWaves();
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x000915D0 File Offset: 0x0008F7D0
	private void Update()
	{
		this.GetNumberOfSpheres();
		if (this.numberOfSpheres != this.numberOfSpheresOld)
		{
			this.GetRenderers();
			this.ApplyMaterials();
		}
		this.numberOfSpheresOld = this.numberOfSpheres;
		this.GetSphereArrays();
		if (this.procedrualGradientEnabled && this.procedrualGradientUpdate)
		{
			this.UpdateRampTexture();
		}
		this.controlParticles = new ParticleSystem.Particle[this.affectorCount];
		this.controlParticlesPositions = new Vector4[this.affectorCount];
		this.controlParticlesSizes = new float[this.affectorCount];
		this.psmain.maxParticles = this.affectorCount;
		this.controlParticleSystem.GetParticles(this.controlParticles);
		for (int i = 0; i < this.affectorCount; i++)
		{
			this.controlParticlesPositions[i] = this.controlParticles[i].position;
			this.controlParticlesSizes[i] = this.controlParticles[i].GetCurrentSize(this.controlParticleSystem) * this.controlParticleSystem.transform.lossyScale.x;
		}
		this.UpdateHitWaves();
		if (this.openAutoAnimation)
		{
			this.OpenCloseProgress();
		}
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x000916F7 File Offset: 0x0008F8F7
	private void GetNumberOfSpheres()
	{
		if (this.getRenderersCustom.Length != 0)
		{
			this.numberOfSpheres = this.getRenderersCustom.Length;
			return;
		}
		this.numberOfSpheres = this.getRenderersInChildren.transform.childCount;
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x00091728 File Offset: 0x0008F928
	private void GetSphereArrays()
	{
		this.spherePositions = new Vector4[this.numberOfSpheres];
		this.sphereSizes = new float[this.numberOfSpheres];
		for (int i = 0; i < this.numberOfSpheres; i++)
		{
			this.spherePositions[i] = this.renderers[i].gameObject.transform.position;
			this.sphereSizes[i] = this.renderers[i].gameObject.transform.lossyScale.x;
		}
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x000917B4 File Offset: 0x0008F9B4
	private void OpenCloseProgress()
	{
		if (this.openCloseValue < 1f)
		{
			this.openCloseValue += Time.deltaTime * this.openSpeed;
		}
		else
		{
			this.openCloseValue = 1f;
		}
		this.openCloseCurve = this.openCurve.Evaluate(this.openCloseValue);
		this.openCloseProgress = this.openCloseCurve;
	}

	// Token: 0x06001755 RID: 5973 RVA: 0x00091817 File Offset: 0x0008FA17
	public void SetOpenCloseValue(float val)
	{
		if (this.openAutoAnimation)
		{
			this.openCloseValue = val;
		}
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x00091828 File Offset: 0x0008FA28
	private Texture2D GenerateTextureFromGradient(Gradient grad)
	{
		float num = 256f;
		float num2 = 1f;
		Texture2D texture2D = new Texture2D((int)num, (int)num2);
		int num3 = 0;
		while ((float)num3 < num)
		{
			int num4 = 0;
			while ((float)num4 < num2)
			{
				Color color = grad.Evaluate(0f + (float)num3 / num);
				texture2D.SetPixel(num3, num4, color);
				num4++;
			}
			num3++;
		}
		texture2D.wrapMode = TextureWrapMode.Clamp;
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x00091894 File Offset: 0x0008FA94
	public void ApplyMaterials()
	{
		for (int i = 0; i < this.materialLayers.Length; i++)
		{
			this.materialLayers[i] = new Material(this.materialLayers[i]);
		}
		foreach (Renderer renderer in this.renderers)
		{
			this.rendererMaterials.Clear();
			foreach (Material material in renderer.sharedMaterials)
			{
				bool flag = false;
				for (int l = 0; l < this.materialLayers.Length; l++)
				{
					if (this.materialLayers[l].name == material.name)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					this.rendererMaterials.Add(material);
				}
			}
			foreach (Material item in this.materialLayers)
			{
				this.rendererMaterials.Add(item);
			}
			renderer.materials = this.rendererMaterials.ToArray();
		}
	}

	// Token: 0x06001758 RID: 5976 RVA: 0x000919A0 File Offset: 0x0008FBA0
	public void UpdateRampTexture()
	{
		this.rampTexture = this.GenerateTextureFromGradient(this.procedrualGradientRamp);
		this.GetRenderers();
		foreach (Renderer renderer in this.renderers)
		{
			foreach (Material material in this.materialLayers)
			{
				material.SetTexture("_Ramp", this.rampTexture);
				material.SetColor("_RampColorTint", this.procedrualRampColorTint);
			}
		}
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x00091A17 File Offset: 0x0008FC17
	public void GetRenderers()
	{
		if (this.getRenderersCustom.Length != 0)
		{
			this.renderers = this.getRenderersCustom;
			return;
		}
		this.renderers = this.getRenderersInChildren.GetComponentsInChildren<Renderer>();
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x00091A40 File Offset: 0x0008FC40
	public void UpdateHitWaves()
	{
		foreach (Renderer renderer in this.renderers)
		{
			ForceFieldController.FFstate ffstate = this.forceFieldMode;
			if (ffstate != ForceFieldController.FFstate.SingleSpheres)
			{
				if (ffstate == ForceFieldController.FFstate.MultipleSpheres)
				{
					foreach (Material material in this.materialLayers)
					{
						material.SetVectorArray("_ControlParticlePosition", this.controlParticlesPositions);
						material.SetFloatArray("_ControlParticleSize", this.controlParticlesSizes);
						material.SetInt("_AffectorCount", this.affectorCount);
						material.SetFloat("_PSLossyScale", this.controlParticleSystem.transform.lossyScale.x);
						material.SetFloat("_MaskAppearProgress", this.openCloseProgress);
						material.SetVectorArray("_FFSpherePositions", this.spherePositions);
						material.SetFloatArray("_FFSphereSizes", this.sphereSizes);
						material.SetFloat("_FFSphereCount", (float)this.numberOfSpheres);
					}
				}
			}
			else
			{
				foreach (Material material2 in this.materialLayers)
				{
					material2.SetVectorArray("_ControlParticlePosition", this.controlParticlesPositions);
					material2.SetFloatArray("_ControlParticleSize", this.controlParticlesSizes);
					material2.SetInt("_AffectorCount", this.affectorCount);
					material2.SetFloat("_PSLossyScale", this.controlParticleSystem.transform.lossyScale.x);
					material2.SetFloat("_MaskAppearProgress", this.openCloseProgress);
				}
			}
		}
	}

	// Token: 0x040015F1 RID: 5617
	public ForceFieldController.FFstate forceFieldMode;

	// Token: 0x040015F2 RID: 5618
	public int affectorCount = 20;

	// Token: 0x040015F3 RID: 5619
	[Range(-2f, 2f)]
	public float openCloseProgress = 2f;

	// Token: 0x040015F4 RID: 5620
	public bool openAutoAnimation = true;

	// Token: 0x040015F5 RID: 5621
	public float openSpeed = 0.6f;

	// Token: 0x040015F6 RID: 5622
	public AnimationCurve openCurve;

	// Token: 0x040015F7 RID: 5623
	public Material[] materialLayers;

	// Token: 0x040015F8 RID: 5624
	public bool procedrualGradientEnabled = true;

	// Token: 0x040015F9 RID: 5625
	public bool procedrualGradientUpdate = true;

	// Token: 0x040015FA RID: 5626
	public Gradient procedrualGradientRamp;

	// Token: 0x040015FB RID: 5627
	public Color procedrualRampColorTint = Color.white;

	// Token: 0x040015FC RID: 5628
	public ParticleSystem controlParticleSystem;

	// Token: 0x040015FD RID: 5629
	public GameObject getRenderersInChildren;

	// Token: 0x040015FE RID: 5630
	public Renderer[] getRenderersCustom;

	// Token: 0x040015FF RID: 5631
	private Renderer[] renderers;

	// Token: 0x04001600 RID: 5632
	private Texture2D rampTexture;

	// Token: 0x04001601 RID: 5633
	private Vector4[] spherePositions;

	// Token: 0x04001602 RID: 5634
	private float[] sphereSizes;

	// Token: 0x04001603 RID: 5635
	private int numberOfSpheres;

	// Token: 0x04001604 RID: 5636
	private int numberOfSpheresOld;

	// Token: 0x04001605 RID: 5637
	private ParticleSystem.Particle[] controlParticles;

	// Token: 0x04001606 RID: 5638
	private Vector4[] controlParticlesPositions;

	// Token: 0x04001607 RID: 5639
	private float[] controlParticlesSizes;

	// Token: 0x04001608 RID: 5640
	private List<Material> rendererMaterials = new List<Material>();

	// Token: 0x04001609 RID: 5641
	private ParticleSystem.MainModule psmain;

	// Token: 0x0400160A RID: 5642
	private float openCloseValue;

	// Token: 0x0400160B RID: 5643
	private float openCloseCurve;

	// Token: 0x020003F5 RID: 1013
	public enum FFstate
	{
		// Token: 0x0400160D RID: 5645
		SingleSpheres,
		// Token: 0x0400160E RID: 5646
		MultipleSpheres
	}
}
