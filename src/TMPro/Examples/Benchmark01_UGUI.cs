using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro.Examples
{
	// Token: 0x02000426 RID: 1062
	public class Benchmark01_UGUI : MonoBehaviour
	{
		// Token: 0x06001803 RID: 6147 RVA: 0x000955BD File Offset: 0x000937BD
		private IEnumerator Start()
		{
			if (this.BenchmarkType == 0)
			{
				this.m_textMeshPro = base.gameObject.AddComponent<TextMeshProUGUI>();
				if (this.TMProFont != null)
				{
					this.m_textMeshPro.font = this.TMProFont;
				}
				this.m_textMeshPro.fontSize = 48f;
				this.m_textMeshPro.alignment = TextAlignmentOptions.Center;
				this.m_textMeshPro.extraPadding = true;
				this.m_material01 = this.m_textMeshPro.font.material;
				this.m_material02 = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - BEVEL");
			}
			else if (this.BenchmarkType == 1)
			{
				this.m_textMesh = base.gameObject.AddComponent<Text>();
				if (this.TextMeshFont != null)
				{
					this.m_textMesh.font = this.TextMeshFont;
				}
				this.m_textMesh.fontSize = 48;
				this.m_textMesh.alignment = TextAnchor.MiddleCenter;
			}
			int num;
			for (int i = 0; i <= 1000000; i = num + 1)
			{
				if (this.BenchmarkType == 0)
				{
					this.m_textMeshPro.text = "The <#0050FF>count is: </color>" + (i % 1000).ToString();
					if (i % 1000 == 999)
					{
						this.m_textMeshPro.fontSharedMaterial = ((this.m_textMeshPro.fontSharedMaterial == this.m_material01) ? (this.m_textMeshPro.fontSharedMaterial = this.m_material02) : (this.m_textMeshPro.fontSharedMaterial = this.m_material01));
					}
				}
				else if (this.BenchmarkType == 1)
				{
					this.m_textMesh.text = "The <color=#0050FF>count is: </color>" + (i % 1000).ToString();
				}
				yield return null;
				num = i;
			}
			yield return null;
			yield break;
		}

		// Token: 0x04001724 RID: 5924
		public int BenchmarkType;

		// Token: 0x04001725 RID: 5925
		public Canvas canvas;

		// Token: 0x04001726 RID: 5926
		public TMP_FontAsset TMProFont;

		// Token: 0x04001727 RID: 5927
		public Font TextMeshFont;

		// Token: 0x04001728 RID: 5928
		private TextMeshProUGUI m_textMeshPro;

		// Token: 0x04001729 RID: 5929
		private Text m_textMesh;

		// Token: 0x0400172A RID: 5930
		private const string label01 = "The <#0050FF>count is: </color>";

		// Token: 0x0400172B RID: 5931
		private const string label02 = "The <color=#0050FF>count is: </color>";

		// Token: 0x0400172C RID: 5932
		private Material m_material01;

		// Token: 0x0400172D RID: 5933
		private Material m_material02;
	}
}
