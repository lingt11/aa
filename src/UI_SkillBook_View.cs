using System;
using UnityEngine;

// Token: 0x020003B4 RID: 948
public class UI_SkillBook_View : UGUIView
{
	// Token: 0x060015A3 RID: 5539 RVA: 0x000869D4 File Offset: 0x00084BD4
	public override void Init(Transform trans)
	{
		this.pool_btnSkill = trans.GetChild(0).GetComponent<PoolView>();
		this.pool_content = trans.GetChild(1).GetChild(0).GetChild(0).GetComponent<PoolView>();
	}

	// Token: 0x0400145C RID: 5212
	public PoolView pool_btnSkill;

	// Token: 0x0400145D RID: 5213
	public PoolView pool_content;
}
