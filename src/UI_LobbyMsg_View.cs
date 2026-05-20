using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000342 RID: 834
public class UI_LobbyMsg_View : UGUIView
{
	// Token: 0x06001308 RID: 4872 RVA: 0x0007341C File Offset: 0x0007161C
	public override void Init(Transform trans)
	{
		this.sr_content = trans.GetChild(0).GetComponent<ScrollRect>();
		this.ltext_content = trans.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
		this.linput_msg = trans.GetChild(1).GetComponent<InputField>();
	}

	// Token: 0x04001198 RID: 4504
	public ScrollRect sr_content;

	// Token: 0x04001199 RID: 4505
	public Text ltext_content;

	// Token: 0x0400119A RID: 4506
	public InputField linput_msg;
}
