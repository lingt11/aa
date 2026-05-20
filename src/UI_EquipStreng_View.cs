using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200031E RID: 798
public class UI_EquipStreng_View : UGUIView
{
	// Token: 0x06001269 RID: 4713 RVA: 0x0006DD54 File Offset: 0x0006BF54
	public override void Init(Transform trans)
	{
		this.trans_equipStreng = trans.GetChild(0).GetComponent<Transform>();
		this.ltext_title = trans.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
		this.btn_close = trans.GetChild(0).GetChild(3).GetComponent<Button>();
	}

	// Token: 0x040010A8 RID: 4264
	public Transform trans_equipStreng;

	// Token: 0x040010A9 RID: 4265
	public Text ltext_title;

	// Token: 0x040010AA RID: 4266
	public Button btn_close;
}
