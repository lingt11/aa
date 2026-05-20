using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class SaiYaDarkBuff : RoleBuffBase
{
	// Token: 0x06000356 RID: 854 RVA: 0x00015E04 File Offset: 0x00014004
	public override void InitBuff()
	{
		base.InitBuff();
		this.buffObject.transform.localScale = this.buffObject.transform.localScale.x * new Vector3(1.15f, 1.15f, 1.15f);
	}

	// Token: 0x06000357 RID: 855 RVA: 0x00015E58 File Offset: 0x00014058
	public override void UpdateBuff()
	{
		base.UpdateBuff();
		if (this.attackRoleBase == null || this.attackRoleBase.IsDead() || GameHelperClient.isReady)
		{
			this.buffTime = -1f;
			return;
		}
		if (this.roleBase.isLocalPlayer)
		{
			this.checkTimer += Time.deltaTime;
			if (this.checkTimer > 0.2f)
			{
				this.checkTimer = 0f;
				PlayerBase localPlayer = GameHelperClient.localPlayer;
				if (this.checkIndex == 0)
				{
					this.reData = default(SaiYaDarkBuff.ReData);
					this.reData.reStr = Mathf.RoundToInt((float)localPlayer.STR * 0.5f);
					this.reData.reSta = Mathf.RoundToInt((float)localPlayer.STA * 0.5f);
					this.reData.reAgi = Mathf.RoundToInt((float)localPlayer.AGI * 0.5f);
					this.reData.reMaxHp = ConstDefine.ClampIntValue((double)((float)(localPlayer.maxHp - (long)(localPlayer.STA * 10)) * 0.5f));
					this.reData.reMaxMp = Mathf.RoundToInt((float)localPlayer.maxMp * 0.5f);
					this.reData.reArmor = Mathf.Max(0, Mathf.RoundToInt((float)localPlayer.armor * 0.5f));
					this.reData.reAttackSpeed = (localPlayer.GetAttackSpeed() - (float)localPlayer.agi * 0.2f * 0.01f) * 0.5f;
					this.reData.reMoveSpeed = localPlayer.GetMoveSpeed() * 0.3f;
					this.reData.reAttack = ConstDefine.ClampIntValue((double)((float)(localPlayer.FinalAttackPower - (long)localPlayer.STR) * 0.5f));
					this.reData.dodge = Mathf.RoundToInt((float)localPlayer.FinalDoge * 0.5f);
					this.playerReData = default(SaiYaDarkBuff.PlayerReData);
					this.playerReData.reStrAdd = (1f + localPlayer.StrAllAdd) * 0.5f;
					this.playerReData.reStaAdd = (1f + localPlayer.StaAllAdd) * 0.5f;
					this.playerReData.reAgiAdd = (1f + localPlayer.AgiAllAdd) * 0.5f;
					this.playerReData.reMaxHpAdd = (1f + localPlayer.maxHpAddPercent - (float)(localPlayer.STA * 10) / (float)localPlayer.maxHp) * 0.5f;
					localPlayer.StrAllAdd -= this.playerReData.reStrAdd;
					localPlayer.StaAllAdd -= this.playerReData.reStaAdd;
					localPlayer.AgiAllAdd -= this.playerReData.reAgiAdd;
					localPlayer.CmdUpdateMaxHpAddPercent(-this.playerReData.reMaxHpAdd);
					localPlayer.AddMaxMp(-this.reData.reMaxMp);
					localPlayer.AddArmor(-this.reData.reArmor);
					localPlayer.AddAttackSpeed(-this.reData.reAttackSpeed);
					localPlayer.moveSpeedPercent -= 0.3f;
					localPlayer.AddAttackPower(-this.reData.reAttack);
					localPlayer.doge += -this.reData.dodge;
					localPlayer.CmdDoge(this.roleBase.doge);
					GameHelperClient.localPlayer.CmdUpdateSaiYaDarkBuff(this.reData, this.attackRoleBase.netId);
					Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("属性降低", ""), Game.Language.Get("str", "")));
				}
				else if (this.checkIndex == 1)
				{
					Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("属性降低", ""), Game.Language.Get("sta", "")));
				}
				else if (this.checkIndex == 2)
				{
					Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("属性降低", ""), Game.Language.Get("dex", "")));
				}
				else if (this.checkIndex == 3)
				{
					Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("属性降低", ""), Game.Language.Get("生命值", "")));
				}
				else if (this.checkIndex == 4)
				{
					Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("属性降低", ""), Game.Language.Get("法力值", "")));
				}
				else if (this.checkIndex == 5)
				{
					Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("属性降低", ""), Game.Language.Get("armor", "")));
				}
				else if (this.checkIndex == 6)
				{
					Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("属性降低", ""), Game.Language.Get("attackSpeed", "")));
				}
				else if (this.checkIndex == 7)
				{
					Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("属性降低", ""), Game.Language.Get("moveSpeed", "")));
				}
				else if (this.checkIndex == 8)
				{
					Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("属性降低", ""), Game.Language.Get("attack", "")));
				}
				else if (this.checkIndex == 9)
				{
					Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("属性降低", ""), Game.Language.Get("闪避值", "")));
				}
				this.checkIndex++;
			}
		}
	}

	// Token: 0x06000358 RID: 856 RVA: 0x00016440 File Offset: 0x00014640
	public override void ClearBuff()
	{
		if (this.roleBase.isLocalPlayer)
		{
			PlayerBase localPlayer = GameHelperClient.localPlayer;
			if (this.checkIndex > 1)
			{
				localPlayer.CmdUpdateMaxHpAddPercent(this.playerReData.reMaxHpAdd);
				localPlayer.AddMaxMp(this.reData.reMaxMp);
				localPlayer.AddArmor(this.reData.reArmor);
				localPlayer.AddAttackSpeed(this.reData.reAttackSpeed);
				localPlayer.moveSpeedPercent += 0.3f;
				localPlayer.AddAttackPower(this.reData.reAttack);
				localPlayer.doge += this.reData.dodge;
				localPlayer.CmdDoge(this.roleBase.doge);
				localPlayer.StrAllAdd += this.playerReData.reStrAdd;
				localPlayer.StaAllAdd += this.playerReData.reStaAdd;
				localPlayer.AgiAllAdd += this.playerReData.reAgiAdd;
			}
		}
		base.ClearBuff();
	}

	// Token: 0x0400034C RID: 844
	private SaiYaDarkBuff.ReData reData;

	// Token: 0x0400034D RID: 845
	private SaiYaDarkBuff.PlayerReData playerReData;

	// Token: 0x0400034E RID: 846
	private float checkTimer;

	// Token: 0x0400034F RID: 847
	private int checkIndex;

	// Token: 0x04000350 RID: 848
	private const float AddValueLevel = 0.5f;

	// Token: 0x04000351 RID: 849
	private const float AddMoveSpeedLevel = 0.3f;

	// Token: 0x020000B3 RID: 179
	public struct ReData
	{
		// Token: 0x04000352 RID: 850
		public int reSta;

		// Token: 0x04000353 RID: 851
		public int reStr;

		// Token: 0x04000354 RID: 852
		public int reAgi;

		// Token: 0x04000355 RID: 853
		public int reMaxHp;

		// Token: 0x04000356 RID: 854
		public int reMaxMp;

		// Token: 0x04000357 RID: 855
		public int reArmor;

		// Token: 0x04000358 RID: 856
		public float reAttackSpeed;

		// Token: 0x04000359 RID: 857
		public float reMoveSpeed;

		// Token: 0x0400035A RID: 858
		public int reAttack;

		// Token: 0x0400035B RID: 859
		public int dodge;
	}

	// Token: 0x020000B4 RID: 180
	public struct PlayerReData
	{
		// Token: 0x0400035C RID: 860
		public float reStaAdd;

		// Token: 0x0400035D RID: 861
		public float reStrAdd;

		// Token: 0x0400035E RID: 862
		public float reAgiAdd;

		// Token: 0x0400035F RID: 863
		public float reMaxHpAdd;
	}
}
