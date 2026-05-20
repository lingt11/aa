using System;
using UnityEngine;

// Token: 0x02000217 RID: 535
public class RelicFitness : RelicBase
{
	// Token: 0x060009B8 RID: 2488 RVA: 0x000341DE File Offset: 0x000323DE
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
		this.totals = new int[3];
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x00034214 File Offset: 0x00032414
	private void KillEvent(RoleBase attackrole, RoleBase hurtrole)
	{
		if (hurtrole is EnemyBase && Random.value < base.GetValue(0, 0.12f))
		{
			int num = Random.Range(0, 3);
			string b = this.relicData.DIC("id");
			int intValue = base.GetIntValue(1, 1);
			if (num == 0)
			{
				UI_Msg ui = Game.UI.GetUI<UI_Msg>();
				if (ui != null)
				{
					ui.ShowMsg(PathDefine.Concat(Game.Language.Get(PathDefine.Concat("pickitem_", b), ""), StringDefine.Colon, Game.Language.Get("addsta", "")), false);
				}
				this.playerBase.AddSTA(intValue);
				this.totals[2] += intValue;
				return;
			}
			if (num == 1)
			{
				UI_Msg ui2 = Game.UI.GetUI<UI_Msg>();
				if (ui2 != null)
				{
					ui2.ShowMsg(PathDefine.Concat(Game.Language.Get(PathDefine.Concat("pickitem_", b), ""), StringDefine.Colon, Game.Language.Get("addstr", "")), false);
				}
				this.playerBase.AddSTR(intValue);
				this.totals[0] += intValue;
				return;
			}
			if (num == 2)
			{
				UI_Msg ui3 = Game.UI.GetUI<UI_Msg>();
				if (ui3 != null)
				{
					ui3.ShowMsg(PathDefine.Concat(Game.Language.Get(PathDefine.Concat("pickitem_", b), ""), StringDefine.Colon, Game.Language.Get("adddex", "")), false);
				}
				this.playerBase.AddAGI(intValue);
				this.totals[1] += intValue;
			}
		}
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x000343B2 File Offset: 0x000325B2
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}
}
