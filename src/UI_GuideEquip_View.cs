using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000325 RID: 805
public class UI_GuideEquip_View : UGUIView
{
	// Token: 0x06001283 RID: 4739 RVA: 0x0006E464 File Offset: 0x0006C664
	public override void Init(Transform trans)
	{
		this.trans_skillName = trans.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Transform>();
		this.pool_skillList = trans.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<PoolView>();
		this.trans_Normal = trans.GetChild(0).GetChild(2).GetChild(0).GetComponent<Transform>();
		this.trans_Myth = trans.GetChild(0).GetChild(2).GetChild(1).GetComponent<Transform>();
		this.btn_back = trans.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
		this.trans_Dec = trans.GetChild(1).GetChild(1).GetComponent<Transform>();
	}

	// Token: 0x040010BF RID: 4287
	public Transform trans_skillName;

	// Token: 0x040010C0 RID: 4288
	public PoolView pool_skillList;

	// Token: 0x040010C1 RID: 4289
	public Transform trans_Normal;

	// Token: 0x040010C2 RID: 4290
	public Transform trans_Myth;

	// Token: 0x040010C3 RID: 4291
	public Button btn_back;

	// Token: 0x040010C4 RID: 4292
	public Transform trans_Dec;
}
