using System;

// Token: 0x020000C6 RID: 198
public class CardSkillReverse : CardSkillBase
{
	// Token: 0x06000387 RID: 903 RVA: 0x000170D4 File Offset: 0x000152D4
	public override void Enter()
	{
		MySystemEvent.Instance.RegisterMessage(30, new Action<Body>(this.OnRoguelikeRefresh));
		MySystemEvent.Instance.RegisterMessage<bool>(31, new Action<Body, bool>(this.OnOpenRoguelike));
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00017106 File Offset: 0x00015306
	public override void Exit()
	{
		MySystemEvent.Instance.UnregisterMessage(30, new Action<Body>(this.OnRoguelikeRefresh));
		MySystemEvent.Instance.UnregisterMessage<bool>(31, new Action<Body, bool>(this.OnOpenRoguelike));
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00017138 File Offset: 0x00015338
	private void OnRoguelikeRefresh(Body body)
	{
		if (this.canUse && GameHelperClient.RefreshNum == 0)
		{
			this.canUse = false;
			GameHelperClient.AddRefreshNum(3);
			Util.ShowTipsNoLanguage(Game.Language.Get("card_" + this.cardId.ToString(), ""));
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x0001718C File Offset: 0x0001538C
	private void OnOpenRoguelike(Body body, bool isCanRefresh)
	{
		if (!isCanRefresh)
		{
			return;
		}
		this.canUse = true;
		if (this.canUse && GameHelperClient.RefreshNum == 0)
		{
			this.canUse = false;
			GameHelperClient.AddRefreshNum(3);
			Util.ShowTipsNoLanguage(Game.Language.Get("card_" + this.cardId.ToString(), ""));
		}
	}

	// Token: 0x04000384 RID: 900
	private bool canUse;
}
