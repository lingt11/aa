using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000389 RID: 905
public class UI_Roguelike_View : UGUIView
{
	// Token: 0x060014A3 RID: 5283 RVA: 0x0007FEF8 File Offset: 0x0007E0F8
	public override void Init(Transform trans)
	{
		this.pool_roguelike = trans.GetChild(0).GetComponent<PoolView>();
		this.trans_switchSkill = trans.GetChild(1).GetComponent<Transform>();
		this.ltext_Title = trans.GetChild(1).GetChild(0).GetComponent<Text>();
		this.trans_heroTip = trans.GetChild(2).GetComponent<Transform>();
	}

	// Token: 0x04001341 RID: 4929
	public PoolView pool_roguelike;

	// Token: 0x04001342 RID: 4930
	public Transform trans_switchSkill;

	// Token: 0x04001343 RID: 4931
	public Text ltext_Title;

	// Token: 0x04001344 RID: 4932
	public Transform trans_heroTip;
}
