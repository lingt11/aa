using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200005E RID: 94
public class ShrinkText : Text
{
	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060001AA RID: 426 RVA: 0x0000A1AA File Offset: 0x000083AA
	// (set) Token: 0x060001AB RID: 427 RVA: 0x0000A1B2 File Offset: 0x000083B2
	public int VisibleLines { get; private set; }

	// Token: 0x060001AC RID: 428 RVA: 0x0000A1BC File Offset: 0x000083BC
	private void _UseFitSettings()
	{
		TextGenerationSettings generationSettings = base.GetGenerationSettings(base.rectTransform.rect.size);
		generationSettings.resizeTextForBestFit = false;
		if (!base.resizeTextForBestFit)
		{
			base.cachedTextGenerator.PopulateWithErrors(this.text, generationSettings, base.gameObject);
			return;
		}
		int resizeTextMinSize = base.resizeTextMinSize;
		int length = this.text.Length;
		for (int i = base.resizeTextMaxSize; i >= resizeTextMinSize; i--)
		{
			generationSettings.fontSize = i;
			base.cachedTextGenerator.PopulateWithErrors(this.text, generationSettings, base.gameObject);
			if (base.cachedTextGenerator.characterCountVisible == length)
			{
				break;
			}
		}
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000A268 File Offset: 0x00008468
	protected override void OnPopulateMesh(VertexHelper toFill)
	{
		if (null == base.font)
		{
			return;
		}
		this.m_DisableFontTextureRebuiltCallback = true;
		this._UseFitSettings();
		IList<UIVertex> verts = base.cachedTextGenerator.verts;
		float d = 1f / base.pixelsPerUnit;
		int count = verts.Count;
		if (count <= 0)
		{
			toFill.Clear();
			return;
		}
		Vector2 vector = new Vector2(verts[0].position.x, verts[0].position.y) * d;
		vector = base.PixelAdjustPoint(vector) - vector;
		toFill.Clear();
		if (vector != Vector2.zero)
		{
			for (int i = 0; i < count; i++)
			{
				int num = i & 3;
				this._tmpVerts[num] = verts[i];
				UIVertex[] tmpVerts = this._tmpVerts;
				int num2 = num;
				tmpVerts[num2].position = tmpVerts[num2].position * d;
				UIVertex[] tmpVerts2 = this._tmpVerts;
				int num3 = num;
				tmpVerts2[num3].position.x = tmpVerts2[num3].position.x + vector.x;
				UIVertex[] tmpVerts3 = this._tmpVerts;
				int num4 = num;
				tmpVerts3[num4].position.y = tmpVerts3[num4].position.y + vector.y;
				if (num == 3)
				{
					toFill.AddUIVertexQuad(this._tmpVerts);
				}
			}
		}
		else
		{
			for (int j = 0; j < count; j++)
			{
				int num5 = j & 3;
				this._tmpVerts[num5] = verts[j];
				UIVertex[] tmpVerts4 = this._tmpVerts;
				int num6 = num5;
				tmpVerts4[num6].position = tmpVerts4[num6].position * d;
				if (num5 == 3)
				{
					toFill.AddUIVertexQuad(this._tmpVerts);
				}
			}
		}
		this.m_DisableFontTextureRebuiltCallback = false;
		this.VisibleLines = base.cachedTextGenerator.lineCount;
	}

	// Token: 0x040001FB RID: 507
	private readonly UIVertex[] _tmpVerts = new UIVertex[4];
}
