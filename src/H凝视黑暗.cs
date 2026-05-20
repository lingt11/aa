using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class H凝视黑暗 : PasssiveSkill
{
	// Token: 0x0600083E RID: 2110 RVA: 0x0002F45C File Offset: 0x0002D65C
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Combine(roleBase.dieEvent, new RoleBase.DieEvent(this.OnDie));
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0002F4C6 File Offset: 0x0002D6C6
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Remove(roleBase.dieEvent, new RoleBase.DieEvent(this.OnDie));
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x0002F4EF File Offset: 0x0002D6EF
	private void OnDie(RoleBase attackrole)
	{
		this.checkTime = Time.time + Mathf.Min(GameHelperClient.CountDownTime, 1f);
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x0002F50C File Offset: 0x0002D70C
	public override void Update()
	{
		if (this.checkTime > 0f && Time.time > this.checkTime)
		{
			this.checkTime = -1f;
			if (this.roleBase.IsDead())
			{
				Util.ShowTips("圣剑掉落了");
				this.roleBase.AddSTR(-this.addSanWei);
				this.roleBase.AddSTA(-this.addSanWei);
				this.roleBase.AddAGI(-this.addSanWei);
				Util.ShowTipsNoLanguage(PathDefine.Concat(Game.Language.Get("p_" + this.skillId, ""), StringDefine.Colon, Game.Language.Get("全属性减少", ""), string.Format(ColorDefine.NormalColor, this.addSanWei)));
				this.addSanWei = 0;
				this.time = 0f;
				this.totals[0] = 0;
			}
		}
		if (this.roleBase.RoleState == RoleState.Dead || GameHelperClient.isReady)
		{
			return;
		}
		this.time += Time.deltaTime;
		if (this.time >= 60f)
		{
			this.time = 0f;
			int num = Mathf.RoundToInt(this.skillValues[0]) * (GameHelperClient.WaveNum + 1);
			this.roleBase.AddSTR(num);
			this.roleBase.AddSTA(num);
			this.roleBase.AddAGI(num);
			this.addSanWei += num;
			this.totals[0] = this.addSanWei;
			Util.ShowTipsNoLanguage(PathDefine.Concat(Game.Language.Get("p_" + this.skillId, ""), StringDefine.Colon, Game.Language.Get("全属性增加", ""), string.Format(ColorDefine.NormalColor, num)));
		}
	}

	// Token: 0x04000B7A RID: 2938
	public int addSanWei;

	// Token: 0x04000B7B RID: 2939
	public float time;

	// Token: 0x04000B7C RID: 2940
	private float checkTime;
}
