using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000336 RID: 822
public class UI_GuideSkill_View : UGUIView
{
	// Token: 0x060012D4 RID: 4820 RVA: 0x000706AC File Offset: 0x0006E8AC
	public override void Init(Transform trans)
	{
		this.trans_skillName = trans.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Transform>();
		this.pool_skillList = trans.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<PoolView>();
		this.trans_QuaSort = trans.GetChild(0).GetChild(2).GetComponent<Transform>();
		this.trans_toActive = trans.GetChild(0).GetChild(3).GetChild(0).GetComponent<Transform>();
		this.trans_toPass = trans.GetChild(0).GetChild(3).GetChild(1).GetComponent<Transform>();
		this.btn_back = trans.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
		this.trans_Dec = trans.GetChild(1).GetChild(1).GetComponent<Transform>();
	}

	// Token: 0x0400111F RID: 4383
	public Transform trans_skillName;

	// Token: 0x04001120 RID: 4384
	public PoolView pool_skillList;

	// Token: 0x04001121 RID: 4385
	public Transform trans_QuaSort;

	// Token: 0x04001122 RID: 4386
	public Transform trans_toActive;

	// Token: 0x04001123 RID: 4387
	public Transform trans_toPass;

	// Token: 0x04001124 RID: 4388
	public Button btn_back;

	// Token: 0x04001125 RID: 4389
	public Transform trans_Dec;
}
