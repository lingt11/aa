using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200038E RID: 910
public class UI_RookieMoveKey_View : UGUIView
{
	// Token: 0x060014B3 RID: 5299 RVA: 0x00080159 File Offset: 0x0007E359
	public override void Init(Transform trans)
	{
		this.trans_bg = trans.GetChild(0).GetComponent<Transform>();
		this.ltext_dec = trans.GetChild(0).GetChild(2).GetComponent<Text>();
	}

	// Token: 0x0400134B RID: 4939
	public Transform trans_bg;

	// Token: 0x0400134C RID: 4940
	public Text ltext_dec;
}
