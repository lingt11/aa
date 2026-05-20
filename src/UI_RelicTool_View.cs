using System;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class UI_RelicTool_View : UGUIView
{
	// Token: 0x0600146A RID: 5226 RVA: 0x0007F162 File Offset: 0x0007D362
	public override void Init(Transform trans)
	{
		this.pool_btnSkill = trans.GetChild(0).GetComponent<PoolView>();
		this.pool_content = trans.GetChild(1).GetChild(0).GetChild(0).GetComponent<PoolView>();
	}

	// Token: 0x04001317 RID: 4887
	public PoolView pool_btnSkill;

	// Token: 0x04001318 RID: 4888
	public PoolView pool_content;
}
