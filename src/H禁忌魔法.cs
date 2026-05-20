using System;
using UnityEngine;

// Token: 0x020001C8 RID: 456
public class H禁忌魔法 : PasssiveSkill
{
	// Token: 0x06000862 RID: 2146 RVA: 0x0002FEA9 File Offset: 0x0002E0A9
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.useSkillEvent = (RoleBase.UseSkillEvent)Delegate.Combine(roleBase.useSkillEvent, new RoleBase.UseSkillEvent(this.UseSkillEvent));
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00002D1D File Offset: 0x00000F1D
	public override void Exit()
	{
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0002FED4 File Offset: 0x0002E0D4
	private ActiveSkillEnum UseSkillEvent(ActiveSkillEnum activeSkillEnum)
	{
		if (Random.value < this.skillValues[0] * 0.01f && activeSkillEnum < ActiveSkillEnum.S_SpellThunder && Game.GameData.ActiveSkillDataDic.ContainsKey(activeSkillEnum + 100))
		{
			if (this.roleBase.isLocalPlayer)
			{
				Util.ShowTipsNoLanguage(string.Format(ColorDefine.NormalColor, Game.Language.Get("禁忌魔法！", "")));
			}
			return activeSkillEnum + 100;
		}
		return activeSkillEnum;
	}
}
