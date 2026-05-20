using System;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class UI_DevTool_View : UGUIView
{
	// Token: 0x06001240 RID: 4672 RVA: 0x0006D424 File Offset: 0x0006B624
	public override void Init(Transform trans)
	{
		this.trans_point = trans.GetChild(1).GetComponent<Transform>();
		this.pool_frame = trans.GetChild(2).GetComponent<PoolView>();
	}

	// Token: 0x04001085 RID: 4229
	public Transform trans_point;

	// Token: 0x04001086 RID: 4230
	public PoolView pool_frame;
}
