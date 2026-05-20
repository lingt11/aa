using System;
using UnityEngine;

// Token: 0x0200031F RID: 799
public class UI_Fps : UGUICtrl
{
	// Token: 0x0600126B RID: 4715 RVA: 0x0006DDA9 File Offset: 0x0006BFA9
	public UI_Fps()
	{
		this.selfView = new UI_Fps_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_fps", base.GetType());
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x0006DDD3 File Offset: 0x0006BFD3
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x0006DDDC File Offset: 0x0006BFDC
	public override void Update()
	{
		this.time += Time.deltaTime;
		if (this.time > 0.2f)
		{
			this.time = 0f;
			this.selfView.ltext_content.text = "FPS: " + (1f / Time.deltaTime).ToString("0");
		}
	}

	// Token: 0x040010AB RID: 4267
	public UI_Fps_View selfView;

	// Token: 0x040010AC RID: 4268
	private float time;
}
