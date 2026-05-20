using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200031C RID: 796
public class UI_DropGold_View : UGUIView
{
	// Token: 0x06001261 RID: 4705 RVA: 0x0006DA9C File Offset: 0x0006BC9C
	public override void Init(Transform trans)
	{
		this.trans_equipStreng = trans.GetChild(0).GetComponent<Transform>();
		this.btn_drop = trans.GetChild(0).GetChild(1).GetComponent<Button>();
		this.trans_inputGold = trans.GetChild(0).GetChild(2).GetChild(1).GetComponent<Transform>();
		this.btn_goldAdd = trans.GetChild(0).GetChild(2).GetChild(2).GetComponent<Button>();
		this.btn_goldRed = trans.GetChild(0).GetChild(2).GetChild(3).GetComponent<Button>();
		this.trans_inputGem = trans.GetChild(0).GetChild(3).GetChild(1).GetComponent<Transform>();
		this.btn_gemAdd = trans.GetChild(0).GetChild(3).GetChild(2).GetComponent<Button>();
		this.btn_gemRed = trans.GetChild(0).GetChild(3).GetChild(3).GetComponent<Button>();
		this.btn_close = trans.GetChild(0).GetChild(5).GetComponent<Button>();
	}

	// Token: 0x0400109D RID: 4253
	public Transform trans_equipStreng;

	// Token: 0x0400109E RID: 4254
	public Button btn_drop;

	// Token: 0x0400109F RID: 4255
	public Transform trans_inputGold;

	// Token: 0x040010A0 RID: 4256
	public Button btn_goldAdd;

	// Token: 0x040010A1 RID: 4257
	public Button btn_goldRed;

	// Token: 0x040010A2 RID: 4258
	public Transform trans_inputGem;

	// Token: 0x040010A3 RID: 4259
	public Button btn_gemAdd;

	// Token: 0x040010A4 RID: 4260
	public Button btn_gemRed;

	// Token: 0x040010A5 RID: 4261
	public Button btn_close;
}
