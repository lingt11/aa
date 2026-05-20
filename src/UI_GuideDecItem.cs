using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000332 RID: 818
public class UI_GuideDecItem : MonoBehaviour
{
	// Token: 0x060012C0 RID: 4800 RVA: 0x0006FE1D File Offset: 0x0006E01D
	public void SetSkill(string skillName, string skillInfo, Sprite sprite)
	{
		this.skillIcon.sprite = sprite;
		this.skillNameText.text = skillName;
		this.skillDec.text = skillInfo;
	}

	// Token: 0x0400110B RID: 4363
	public Image skillIcon;

	// Token: 0x0400110C RID: 4364
	public Text skillNameText;

	// Token: 0x0400110D RID: 4365
	public Text skillDec;
}
