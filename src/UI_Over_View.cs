using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000361 RID: 865
public class UI_Over_View : UGUIView
{
	// Token: 0x060013BD RID: 5053 RVA: 0x00079EF8 File Offset: 0x000780F8
	public override void Init(Transform trans)
	{
		this.ltext_info = trans.GetChild(0).GetChild(0).GetComponent<Text>();
		this.btn_backmenu = trans.GetChild(1).GetComponent<Button>();
	}

	// Token: 0x04001252 RID: 4690
	public Text ltext_info;

	// Token: 0x04001253 RID: 4691
	public Button btn_backmenu;
}
