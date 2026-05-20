using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200030C RID: 780
public class UI_BattleSetting_View : UGUIView
{
	// Token: 0x06001219 RID: 4633 RVA: 0x0006B58C File Offset: 0x0006978C
	public override void Init(Transform trans)
	{
		this.trans_bg = trans.GetChild(0).GetComponent<Transform>();
		this.trans_main = trans.GetChild(0).GetChild(1).GetComponent<Transform>();
		this.btn_set = trans.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
		this.btn_card = trans.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>();
		this.btn_battle = trans.GetChild(0).GetChild(1).GetChild(2).GetComponent<Button>();
		this.btn_return = trans.GetChild(0).GetChild(1).GetChild(3).GetComponent<Button>();
		this.btn_playerInfo = trans.GetChild(0).GetChild(1).GetChild(4).GetComponent<Button>();
		this.btn_quit = trans.GetChild(0).GetChild(1).GetChild(5).GetComponent<Button>();
	}

	// Token: 0x0400103E RID: 4158
	public Transform trans_bg;

	// Token: 0x0400103F RID: 4159
	public Transform trans_main;

	// Token: 0x04001040 RID: 4160
	public Button btn_set;

	// Token: 0x04001041 RID: 4161
	public Button btn_card;

	// Token: 0x04001042 RID: 4162
	public Button btn_battle;

	// Token: 0x04001043 RID: 4163
	public Button btn_return;

	// Token: 0x04001044 RID: 4164
	public Button btn_playerInfo;

	// Token: 0x04001045 RID: 4165
	public Button btn_quit;
}
