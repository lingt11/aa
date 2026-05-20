using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200038B RID: 907
public class UI_RoleInfoDebug_View : UGUIView
{
	// Token: 0x060014A9 RID: 5289 RVA: 0x00080092 File Offset: 0x0007E292
	public override void Init(Transform trans)
	{
		this.ltext_info = trans.GetChild(1).GetComponent<Text>();
		this.btn_refresh = trans.GetChild(2).GetComponent<Button>();
	}

	// Token: 0x04001346 RID: 4934
	public Text ltext_info;

	// Token: 0x04001347 RID: 4935
	public Button btn_refresh;
}
