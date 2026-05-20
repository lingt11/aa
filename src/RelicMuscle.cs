using System;
using System.Collections.Generic;

// Token: 0x0200022C RID: 556
public class RelicMuscle : RelicBase
{
	// Token: 0x06000A0C RID: 2572 RVA: 0x000350CC File Offset: 0x000332CC
	public override void Enter()
	{
		List<SkillBase> roleSkillList = this.playerBase.roleSkillList;
		for (int i = roleSkillList.Count - 1; i >= 0; i--)
		{
			SkillBase skillBase = roleSkillList[i];
			if (!(skillBase is PasssiveSkill) && !skillBase.IsHeroSkill())
			{
				Util.RemoveSkill(skillBase);
			}
		}
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui != null)
		{
			ui.RefreshPlayerSkill();
		}
		this.playerBase.UpdateAttackPercent(base.GetValue(0, 0.2f));
		this.playerBase.AddAttackSpeed(base.GetValue(1, 0.35f));
		this.playerBase.AddArmor(base.GetIntValue(2, 50));
		GameHelperClient.CantLearnActiveSkill++;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0003517C File Offset: 0x0003337C
	public override void Exit()
	{
		this.playerBase.UpdateAttackPercent(-base.GetValue(0, 0.2f));
		this.playerBase.AddAttackSpeed(-base.GetValue(1, 0.35f));
		this.playerBase.AddArmor(-base.GetIntValue(2, 50));
		GameHelperClient.CantLearnActiveSkill--;
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x000351DC File Offset: 0x000333DC
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.UpdateAttackPercent(base.GetLevelValueDelta(0, 0.2f, oldLevel, newLevel));
		this.playerBase.AddAttackSpeed(base.GetLevelValueDelta(1, 0.35f, oldLevel, newLevel));
		this.playerBase.AddArmor(base.GetLevelIntValueDelta(2, 50, oldLevel, newLevel));
	}
}
