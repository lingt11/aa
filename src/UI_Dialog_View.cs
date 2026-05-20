using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000318 RID: 792
public class UI_Dialog_View : UGUIView
{
	// Token: 0x06001247 RID: 4679 RVA: 0x0006D4D8 File Offset: 0x0006B6D8
	public override void Init(Transform trans)
	{
		this.trans_bg = trans.GetChild(0).GetComponent<Transform>();
		this.ltext_info = trans.GetChild(0).GetChild(0).GetComponent<Text>();
		this.img_bg = trans.GetChild(0).GetChild(1).GetComponent<Image>();
		this.btn_confirm = trans.GetChild(0).GetChild(2).GetComponent<Button>();
	}

	// Token: 0x04001089 RID: 4233
	public Transform trans_bg;

	// Token: 0x0400108A RID: 4234
	public Text ltext_info;

	// Token: 0x0400108B RID: 4235
	public Image img_bg;

	// Token: 0x0400108C RID: 4236
	public Button btn_confirm;
}
