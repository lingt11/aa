using System;
using UnityEngine;

// Token: 0x020003C1 RID: 961
public class UI_UseSkillBookKey_View : UGUIView
{
	// Token: 0x060015F0 RID: 5616 RVA: 0x00088156 File Offset: 0x00086356
	public override void Init(Transform trans)
	{
		this.trans_bg = trans.GetChild(0).GetComponent<Transform>();
	}

	// Token: 0x040014A7 RID: 5287
	public Transform trans_bg;
}
