using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro.Examples
{
	// Token: 0x02000429 RID: 1065
	public class Benchmark03 : MonoBehaviour
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x00002D1D File Offset: 0x00000F1D
		private void Awake()
		{
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00095AFC File Offset: 0x00093CFC
		private void Start()
		{
			TMP_FontAsset tmp_FontAsset = null;
			switch (this.Benchmark)
			{
			case Benchmark03.BenchmarkType.TMP_SDF_MOBILE:
				tmp_FontAsset = TMP_FontAsset.CreateFontAsset(this.SourceFont, 90, 9, GlyphRenderMode.SDFAA, 256, 256, AtlasPopulationMode.Dynamic, true);
				break;
			case Benchmark03.BenchmarkType.TMP_SDF__MOBILE_SSD:
				tmp_FontAsset = TMP_FontAsset.CreateFontAsset(this.SourceFont, 90, 9, GlyphRenderMode.SDFAA, 256, 256, AtlasPopulationMode.Dynamic, true);
				tmp_FontAsset.material.shader = Shader.Find("TextMeshPro/Mobile/Distance Field SSD");
				break;
			case Benchmark03.BenchmarkType.TMP_SDF:
				tmp_FontAsset = TMP_FontAsset.CreateFontAsset(this.SourceFont, 90, 9, GlyphRenderMode.SDFAA, 256, 256, AtlasPopulationMode.Dynamic, true);
				tmp_FontAsset.material.shader = Shader.Find("TextMeshPro/Distance Field");
				break;
			case Benchmark03.BenchmarkType.TMP_BITMAP_MOBILE:
				tmp_FontAsset = TMP_FontAsset.CreateFontAsset(this.SourceFont, 90, 9, GlyphRenderMode.SMOOTH, 256, 256, AtlasPopulationMode.Dynamic, true);
				break;
			}
			for (int i = 0; i < this.NumberOfSamples; i++)
			{
				Benchmark03.BenchmarkType benchmark = this.Benchmark;
				if (benchmark > Benchmark03.BenchmarkType.TMP_BITMAP_MOBILE)
				{
					if (benchmark == Benchmark03.BenchmarkType.TEXTMESH_BITMAP)
					{
						TextMesh textMesh = new GameObject
						{
							transform = 
							{
								position = new Vector3(0f, 1.2f, 0f)
							}
						}.AddComponent<TextMesh>();
						textMesh.GetComponent<Renderer>().sharedMaterial = this.SourceFont.material;
						textMesh.font = this.SourceFont;
						textMesh.anchor = TextAnchor.MiddleCenter;
						textMesh.fontSize = 130;
						textMesh.color = new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);
						textMesh.text = "@";
					}
				}
				else
				{
					TextMeshPro textMeshPro = new GameObject
					{
						transform = 
						{
							position = new Vector3(0f, 1.2f, 0f)
						}
					}.AddComponent<TextMeshPro>();
					textMeshPro.font = tmp_FontAsset;
					textMeshPro.fontSize = 128f;
					textMeshPro.text = "@";
					textMeshPro.alignment = TextAlignmentOptions.Center;
					textMeshPro.color = new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);
					if (this.Benchmark == Benchmark03.BenchmarkType.TMP_BITMAP_MOBILE)
					{
						textMeshPro.fontSize = 132f;
					}
				}
			}
		}

		// Token: 0x04001736 RID: 5942
		public int NumberOfSamples = 100;

		// Token: 0x04001737 RID: 5943
		public Benchmark03.BenchmarkType Benchmark;

		// Token: 0x04001738 RID: 5944
		public Font SourceFont;

		// Token: 0x0200042A RID: 1066
		public enum BenchmarkType
		{
			// Token: 0x0400173A RID: 5946
			TMP_SDF_MOBILE,
			// Token: 0x0400173B RID: 5947
			TMP_SDF__MOBILE_SSD,
			// Token: 0x0400173C RID: 5948
			TMP_SDF,
			// Token: 0x0400173D RID: 5949
			TMP_BITMAP_MOBILE,
			// Token: 0x0400173E RID: 5950
			TEXTMESH_BITMAP
		}
	}
}
