using System;

// Token: 0x02000308 RID: 776
public class UI_Battle_Joy
{
	// Token: 0x06001204 RID: 4612 RVA: 0x0006ADC6 File Offset: 0x00068FC6
	public UI_Battle_Joy(UI_Battle ui)
	{
		this.uiBattle = ui;
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x0006ADD5 File Offset: 0x00068FD5
	public void Open()
	{
		MySystemEvent.Instance.RegisterMessage(1, new Action<Body>(this.JoyA));
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x0006ADEE File Offset: 0x00068FEE
	public void Close()
	{
		MySystemEvent.Instance.UnregisterMessage(1, new Action<Body>(this.JoyA));
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x0006AE07 File Offset: 0x00069007
	private void JoyA(Body body)
	{
		this.uiBattle.OnKeyPick();
	}

	// Token: 0x04001011 RID: 4113
	private UI_Battle uiBattle;
}
