using System;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x02000432 RID: 1074
	public class SimpleScript : MonoBehaviour
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x000968EC File Offset: 0x00094AEC
		private void Start()
		{
			this.m_textMeshPro = base.gameObject.AddComponent<TextMeshPro>();
			this.m_textMeshPro.autoSizeTextContainer = true;
			this.m_textMeshPro.fontSize = 48f;
			this.m_textMeshPro.alignment = TextAlignmentOptions.Center;
			this.m_textMeshPro.enableWordWrapping = false;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00096942 File Offset: 0x00094B42
		private void Update()
		{
			this.m_textMeshPro.SetText("The <#0050FF>count is: </color>{0:2}", this.m_frame % 1000f);
			this.m_frame += 1f * Time.deltaTime;
		}

		// Token: 0x04001776 RID: 6006
		private TextMeshPro m_textMeshPro;

		// Token: 0x04001777 RID: 6007
		private const string label = "The <#0050FF>count is: </color>{0:2}";

		// Token: 0x04001778 RID: 6008
		private float m_frame;
	}
}
