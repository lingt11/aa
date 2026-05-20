using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003FB RID: 1019
[ExecuteInEditMode]
public class EnchantedMeshApply : MonoBehaviour
{
	// Token: 0x0600176C RID: 5996 RVA: 0x00092424 File Offset: 0x00090624
	private void Start()
	{
		this.rendererMaterials.Clear();
		this.meshRenderers = base.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer meshRenderer in this.meshRenderers)
		{
			bool flag = false;
			foreach (Material material in meshRenderer.sharedMaterials)
			{
				this.rendererMaterials.Add(material);
				if (material == this.EnchantMaterial)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.rendererMaterials.Add(this.EnchantMaterial);
				meshRenderer.sharedMaterials = this.rendererMaterials.ToArray();
			}
		}
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x000924C8 File Offset: 0x000906C8
	private void Update()
	{
		foreach (MeshRenderer meshRenderer in this.meshRenderers)
		{
			Material[] array2 = meshRenderer.sharedMaterials;
			if (Application.isPlaying)
			{
				array2 = meshRenderer.materials;
			}
			foreach (Material material in array2)
			{
				material.SetVector("_EnchantPoint", this.EnchantPoint.position);
				material.SetFloat("_DistanceOffsetScale", this.EnchantPoint.transform.localScale.x);
			}
		}
	}

	// Token: 0x04001631 RID: 5681
	public Transform EnchantPoint;

	// Token: 0x04001632 RID: 5682
	public Material EnchantMaterial;

	// Token: 0x04001633 RID: 5683
	private List<Material> rendererMaterials = new List<Material>();

	// Token: 0x04001634 RID: 5684
	private MeshRenderer[] meshRenderers;
}
