using System;
using UnityEngine;

// Token: 0x020001CC RID: 460
public class H量子阅读 : PasssiveSkill
{
	// Token: 0x06000872 RID: 2162 RVA: 0x00030548 File Offset: 0x0002E748
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.skillBookEvent = (RoleBase.SkillBookEvent)Delegate.Combine(roleBase.skillBookEvent, new RoleBase.SkillBookEvent(this.SkillBook));
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x000305B2 File Offset: 0x0002E7B2
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.skillBookEvent = (RoleBase.SkillBookEvent)Delegate.Remove(roleBase.skillBookEvent, new RoleBase.SkillBookEvent(this.SkillBook));
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x000305DC File Offset: 0x0002E7DC
	private void SkillBook(RoleBase player)
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		player.AddSTR(num);
		player.AddAGI(num);
		player.AddSTA(num);
		this.totals[0] += num;
	}
}
