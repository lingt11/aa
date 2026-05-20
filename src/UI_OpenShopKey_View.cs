using System;
using UnityEngine;

// Token: 0x0200035F RID: 863
public class UI_OpenShopKey_View : UGUIView
{
	// Token: 0x060013B7 RID: 5047 RVA: 0x00079DFD File Offset: 0x00077FFD
	public override void Init(Transform trans)
	{
		this.trans_bg = trans.GetChild(0).GetComponent<Transform>();
	}

	// Token: 0x04001250 RID: 4688
	public Transform trans_bg;
}
