using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000331 RID: 817
public class UI_GuideRelic_View : UGUIView
{
	// Token: 0x060012BE RID: 4798 RVA: 0x0006FD44 File Offset: 0x0006DF44
	public override void Init(Transform trans)
	{
		this.btn_back = trans.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
		this.trans_skillName = trans.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Transform>();
		this.pool_skillList = trans.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<PoolView>();
		this.trans_tipInfo = trans.GetChild(1).GetChild(1).GetComponent<Transform>();
		this.ltext_tipTitle = trans.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
		this.ltext_tipInfo = trans.GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>();
	}

	// Token: 0x04001105 RID: 4357
	public Button btn_back;

	// Token: 0x04001106 RID: 4358
	public Transform trans_skillName;

	// Token: 0x04001107 RID: 4359
	public PoolView pool_skillList;

	// Token: 0x04001108 RID: 4360
	public Transform trans_tipInfo;

	// Token: 0x04001109 RID: 4361
	public Text ltext_tipTitle;

	// Token: 0x0400110A RID: 4362
	public Text ltext_tipInfo;
}
