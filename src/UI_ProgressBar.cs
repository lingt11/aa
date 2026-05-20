using System;
using UnityEngine;

// Token: 0x02000376 RID: 886
public class UI_ProgressBar : UGUICtrl
{
	// Token: 0x06001451 RID: 5201 RVA: 0x0007E957 File Offset: 0x0007CB57
	public UI_ProgressBar()
	{
		this.selfView = new UI_ProgressBar_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_progressBar", base.GetType());
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x0006DDD3 File Offset: 0x0006BFD3
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x0007E981 File Offset: 0x0007CB81
	public override void Update()
	{
		this.timer += Time.deltaTime;
		this.selfView.img_pro.fillAmount = this.timer / this.allTime;
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x0007E9B2 File Offset: 0x0007CBB2
	public void ShowProgress(float allTimeValue, string text)
	{
		this.selfView.ltext_name.text = text;
		this.allTime = allTimeValue;
		this.timer = 0f;
	}

	// Token: 0x040012FC RID: 4860
	public UI_ProgressBar_View selfView;

	// Token: 0x040012FD RID: 4861
	private float timer;

	// Token: 0x040012FE RID: 4862
	private float allTime;
}
