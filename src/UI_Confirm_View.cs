using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200030E RID: 782
public class UI_Confirm_View : UGUIView
{
	// Token: 0x06001221 RID: 4641 RVA: 0x0006B818 File Offset: 0x00069A18
	public override void Init(Transform trans)
	{
		this.trans_bg = trans.GetChild(0).GetComponent<Transform>();
		this.trans_main = trans.GetChild(0).GetChild(1).GetComponent<Transform>();
		this.ltext_dec = trans.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
		this.btn_confirm = trans.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>();
		this.btn_cancel = trans.GetChild(0).GetChild(1).GetChild(2).GetComponent<Button>();
		this.trans_inputMessage = trans.GetChild(0).GetChild(1).GetChild(3).GetComponent<Transform>();
	}

	// Token: 0x0400104B RID: 4171
	public Transform trans_bg;

	// Token: 0x0400104C RID: 4172
	public Transform trans_main;

	// Token: 0x0400104D RID: 4173
	public Text ltext_dec;

	// Token: 0x0400104E RID: 4174
	public Button btn_confirm;

	// Token: 0x0400104F RID: 4175
	public Button btn_cancel;

	// Token: 0x04001050 RID: 4176
	public Transform trans_inputMessage;
}
