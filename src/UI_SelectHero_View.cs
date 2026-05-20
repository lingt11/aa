using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000390 RID: 912
public class UI_SelectHero_View : UGUIView
{
	// Token: 0x060014C7 RID: 5319 RVA: 0x00080690 File Offset: 0x0007E890
	public override void Init(Transform trans)
	{
		this.btn_card = trans.GetChild(0).GetComponent<Button>();
		this.btn_backMenu = trans.GetChild(1).GetComponent<Button>();
		this.trans_info = trans.GetChild(2).GetComponent<Transform>();
		this.ltext_heroInfo = trans.GetChild(2).GetChild(0).GetComponent<Text>();
		this.dd_selectHero = trans.GetChild(3).GetComponent<Dropdown>();
	}

	// Token: 0x04001350 RID: 4944
	public Button btn_card;

	// Token: 0x04001351 RID: 4945
	public Button btn_backMenu;

	// Token: 0x04001352 RID: 4946
	public Transform trans_info;

	// Token: 0x04001353 RID: 4947
	public Text ltext_heroInfo;

	// Token: 0x04001354 RID: 4948
	public Dropdown dd_selectHero;
}
