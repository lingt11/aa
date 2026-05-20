using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020003BB RID: 955
public class UI_TimeScale : UGUICtrl
{
	// Token: 0x060015DC RID: 5596 RVA: 0x00087ED7 File Offset: 0x000860D7
	public UI_TimeScale()
	{
		this.selfView = new UI_TimeScale_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_timeScale", base.GetType());
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x00087F01 File Offset: 0x00086101
	protected override void ButtonAddClick()
	{
		this.selfView.slider_time.onValueChanged.AddListener(new UnityAction<float>(this.OnSliderValueChanged));
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x00087F24 File Offset: 0x00086124
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.selfView.slider_time.value = 1f;
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x00087F42 File Offset: 0x00086142
	private void OnSliderValueChanged(float value)
	{
		Time.timeScale = value;
	}

	// Token: 0x0400149D RID: 5277
	public UI_TimeScale_View selfView;
}
