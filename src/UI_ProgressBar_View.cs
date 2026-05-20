using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000377 RID: 887
public class UI_ProgressBar_View : UGUIView
{
	// Token: 0x06001456 RID: 5206 RVA: 0x0007E9D8 File Offset: 0x0007CBD8
	public override void Init(Transform trans)
	{
		this.ltext_name = trans.GetChild(0).GetChild(0).GetComponent<Text>();
		this.trans_bg = trans.GetChild(1).GetComponent<Transform>();
		this.img_pro = trans.GetChild(1).GetChild(0).GetComponent<Image>();
	}

	// Token: 0x040012FF RID: 4863
	public Text ltext_name;

	// Token: 0x04001300 RID: 4864
	public Transform trans_bg;

	// Token: 0x04001301 RID: 4865
	public Image img_pro;
}
