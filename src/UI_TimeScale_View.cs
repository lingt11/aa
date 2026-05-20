using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003BC RID: 956
public class UI_TimeScale_View : UGUIView
{
	// Token: 0x060015E0 RID: 5600 RVA: 0x00087F4A File Offset: 0x0008614A
	public override void Init(Transform trans)
	{
		this.slider_time = trans.GetChild(0).GetComponent<Slider>();
	}

	// Token: 0x0400149E RID: 5278
	public Slider slider_time;
}
