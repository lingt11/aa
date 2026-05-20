using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003BE RID: 958
public class UI_Tips_View : UGUIView
{
	// Token: 0x060015E6 RID: 5606 RVA: 0x00088093 File Offset: 0x00086293
	public override void Init(Transform trans)
	{
		this.trans_bg = trans.GetChild(0).GetComponent<Transform>();
		this.ltext_info = trans.GetChild(0).GetChild(0).GetComponent<Text>();
	}

	// Token: 0x040014A2 RID: 5282
	public Transform trans_bg;

	// Token: 0x040014A3 RID: 5283
	public Text ltext_info;
}
