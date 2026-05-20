using System;
using UnityEngine;

// Token: 0x02000379 RID: 889
public class UI_QTEMusic_View : UGUIView
{
	// Token: 0x0600145D RID: 5213 RVA: 0x0007ED54 File Offset: 0x0007CF54
	public override void Init(Transform trans)
	{
		this.trans_bg = trans.GetChild(1).GetComponent<Transform>();
		this.trans_trigger = trans.GetChild(1).GetChild(0).GetComponent<Transform>();
		this.trans_arrow = trans.GetChild(1).GetChild(1).GetComponent<Transform>();
	}

	// Token: 0x0400130D RID: 4877
	public Transform trans_bg;

	// Token: 0x0400130E RID: 4878
	public Transform trans_trigger;

	// Token: 0x0400130F RID: 4879
	public Transform trans_arrow;
}
