using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000346 RID: 838
public class UI_Msg_View : UGUIView
{
	// Token: 0x0600131B RID: 4891 RVA: 0x000739C0 File Offset: 0x00071BC0
	public override void Init(Transform trans)
	{
		this.sr_content = trans.GetChild(0).GetComponent<ScrollRect>();
		this.ltext_content = trans.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
		this.linput_msg = trans.GetChild(1).GetComponent<InputField>();
	}

	// Token: 0x040011A6 RID: 4518
	public ScrollRect sr_content;

	// Token: 0x040011A7 RID: 4519
	public Text ltext_content;

	// Token: 0x040011A8 RID: 4520
	public InputField linput_msg;
}
