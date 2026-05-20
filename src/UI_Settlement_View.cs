using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A2 RID: 930
public class UI_Settlement_View : UGUIView
{
	// Token: 0x0600153C RID: 5436 RVA: 0x000836FC File Offset: 0x000818FC
	public override void Init(Transform trans)
	{
		this.ltext_info = trans.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
		this.pool_player = trans.GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetComponent<PoolView>();
		this.btn_back = trans.GetChild(3).GetComponent<Button>();
		this.ltext_result = trans.GetChild(4).GetComponent<Text>();
	}

	// Token: 0x040013E9 RID: 5097
	public Text ltext_info;

	// Token: 0x040013EA RID: 5098
	public PoolView pool_player;

	// Token: 0x040013EB RID: 5099
	public Button btn_back;

	// Token: 0x040013EC RID: 5100
	public Text ltext_result;
}
