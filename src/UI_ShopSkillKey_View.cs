using System;
using UnityEngine;

// Token: 0x020003AF RID: 943
public class UI_ShopSkillKey_View : UGUIView
{
	// Token: 0x06001596 RID: 5526 RVA: 0x000865EA File Offset: 0x000847EA
	public override void Init(Transform trans)
	{
		this.trans_bg = trans.GetChild(0).GetComponent<Transform>();
	}

	// Token: 0x04001455 RID: 5205
	public Transform trans_bg;
}
