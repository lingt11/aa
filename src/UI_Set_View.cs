using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200039E RID: 926
public class UI_Set_View : UGUIView
{
	// Token: 0x06001531 RID: 5425 RVA: 0x00083180 File Offset: 0x00081380
	public override void Init(Transform trans)
	{
		this.btn_close = trans.GetChild(0).GetComponent<Button>();
		this.btn_quitGame = trans.GetChild(1).GetChild(0).GetComponent<Button>();
		this.slider_music = trans.GetChild(1).GetChild(1).GetComponent<Slider>();
		this.trans_language = trans.GetChild(1).GetChild(2).GetComponent<Transform>();
	}

	// Token: 0x040013DA RID: 5082
	public Button btn_close;

	// Token: 0x040013DB RID: 5083
	public Button btn_quitGame;

	// Token: 0x040013DC RID: 5084
	public Slider slider_music;

	// Token: 0x040013DD RID: 5085
	public Transform trans_language;
}
