using System;

// Token: 0x0200021D RID: 541
public class RelicGuy : RelicBase
{
	// Token: 0x060009D0 RID: 2512 RVA: 0x000346D4 File Offset: 0x000328D4
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddSTR(base.GetIntValue(0, 200));
		GameHelperClient.localPlayer.AddAGI(base.GetIntValue(0, 200));
		GameHelperClient.localPlayer.AddSTA(base.GetIntValue(0, 200));
		GameHelperClient.localPlayer.CmdAddBuff(GameHelperClient.localPlayer.netId, GameHelperClient.localPlayer.netId, LocalBuffType.Guy, 0f, 99999f, 0);
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x00034750 File Offset: 0x00032950
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddSTR(-base.GetIntValue(0, 200));
		GameHelperClient.localPlayer.AddAGI(-base.GetIntValue(0, 200));
		GameHelperClient.localPlayer.AddSTA(-base.GetIntValue(0, 200));
		bool flag = true;
		foreach (RelicBase relicBase in GameHelperClient.localPlayer.playerAttribute.relicList)
		{
			if (relicBase != this && relicBase is RelicGuy)
			{
				flag = false;
			}
		}
		if (flag)
		{
			GameHelperClient.localPlayer.CmdRemoveuff(GameHelperClient.localPlayer.netId, LocalBuffType.Guy);
		}
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x00034814 File Offset: 0x00032A14
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddSTR(base.GetLevelIntValueDelta(0, 200, oldLevel, newLevel));
		GameHelperClient.localPlayer.AddAGI(base.GetLevelIntValueDelta(0, 200, oldLevel, newLevel));
		GameHelperClient.localPlayer.AddSTA(base.GetLevelIntValueDelta(0, 200, oldLevel, newLevel));
	}
}
