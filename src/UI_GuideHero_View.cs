using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000328 RID: 808
public class UI_GuideHero_View : UGUIView
{
	// Token: 0x0600128F RID: 4751 RVA: 0x0006E7C0 File Offset: 0x0006C9C0
	public override void Init(Transform trans)
	{
		this.pool_skillList = trans.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<PoolView>();
		this.btn_back = trans.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
		this.trans_Dec = trans.GetChild(1).GetChild(1).GetComponent<Transform>();
	}

	// Token: 0x040010CC RID: 4300
	public PoolView pool_skillList;

	// Token: 0x040010CD RID: 4301
	public Button btn_back;

	// Token: 0x040010CE RID: 4302
	public Transform trans_Dec;
}
