using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000320 RID: 800
public class UI_Fps_View : UGUIView
{
	// Token: 0x0600126F RID: 4719 RVA: 0x0006DE45 File Offset: 0x0006C045
	public override void Init(Transform trans)
	{
		this.ltext_content = trans.GetChild(0).GetComponent<Text>();
	}

	// Token: 0x040010AD RID: 4269
	public Text ltext_content;
}
