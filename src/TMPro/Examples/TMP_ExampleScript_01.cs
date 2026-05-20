using System;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x02000440 RID: 1088
	public class TMP_ExampleScript_01 : MonoBehaviour
	{
		// Token: 0x06001867 RID: 6247 RVA: 0x000981B0 File Offset: 0x000963B0
		private void Awake()
		{
			if (this.ObjectType == TMP_ExampleScript_01.objectType.TextMeshPro)
			{
				this.m_text = (base.GetComponent<TextMeshPro>() ?? base.gameObject.AddComponent<TextMeshPro>());
			}
			else
			{
				this.m_text = (base.GetComponent<TextMeshProUGUI>() ?? base.gameObject.AddComponent<TextMeshProUGUI>());
			}
			this.m_text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Anton SDF");
			this.m_text.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/Anton SDF - Drop Shadow");
			this.m_text.fontSize = 120f;
			this.m_text.text = "A <#0080ff>simple</color> line of text.";
			Vector2 preferredValues = this.m_text.GetPreferredValues(float.PositiveInfinity, float.PositiveInfinity);
			this.m_text.rectTransform.sizeDelta = new Vector2(preferredValues.x, preferredValues.y);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0009827E File Offset: 0x0009647E
		private void Update()
		{
			if (!this.isStatic)
			{
				this.m_text.SetText("The count is <#0080ff>{0}</color>", (float)(this.count % 1000));
				this.count++;
			}
		}

		// Token: 0x040017CB RID: 6091
		public TMP_ExampleScript_01.objectType ObjectType;

		// Token: 0x040017CC RID: 6092
		public bool isStatic;

		// Token: 0x040017CD RID: 6093
		private TMP_Text m_text;

		// Token: 0x040017CE RID: 6094
		private const string k_label = "The count is <#0080ff>{0}</color>";

		// Token: 0x040017CF RID: 6095
		private int count;

		// Token: 0x02000441 RID: 1089
		public enum objectType
		{
			// Token: 0x040017D1 RID: 6097
			TextMeshPro,
			// Token: 0x040017D2 RID: 6098
			TextMeshProUGUI
		}
	}
}
