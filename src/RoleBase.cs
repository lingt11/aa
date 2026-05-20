using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DG.Tweening;
using Mirror;
using Mirror.RemoteCalls;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class RoleBase : NetworkBehaviour
{
	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06000EEF RID: 3823 RVA: 0x000566F1 File Offset: 0x000548F1
	// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x000566F9 File Offset: 0x000548F9
	public float SyncSkillData
	{
		get
		{
			return this.syncSkillData;
		}
		set
		{
			this.NetworksyncSkillData = value;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00056702 File Offset: 0x00054902
	// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x0005670A File Offset: 0x0005490A
	public bool XuanYunImmunity
	{
		get
		{
			return this.xuanYunImmunity;
		}
		set
		{
			this.xuanYunImmunity = value;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00056713 File Offset: 0x00054913
	public float CanXuanYunLastTime
	{
		get
		{
			return this.canXuanYunLastTime;
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0005671B File Offset: 0x0005491B
	public RoleType FatherType
	{
		get
		{
			return this.fatherType;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00056723 File Offset: 0x00054923
	public int FatherId
	{
		get
		{
			return this.fatherId;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0005672B File Offset: 0x0005492B
	public int SkillBookId
	{
		get
		{
			return this.skillBookId;
		}
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x00056733 File Offset: 0x00054933
	public void UpdateSkillData(float value)
	{
		if (Time.time > this.syncSkillDataTime && !Mathf.Approximately(this.syncSkillData, value))
		{
			this.syncSkillDataTime = Time.time + 0.1f;
			this.CmdSyncSkillData(value);
		}
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x00056768 File Offset: 0x00054968
	[Command]
	private void CmdSyncSkillData(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		base.SendCommandInternal(typeof(RoleBase), "CmdSyncSkillData", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x000567A7 File Offset: 0x000549A7
	// (set) Token: 0x06000EFA RID: 3834 RVA: 0x0004A363 File Offset: 0x00048563
	public virtual float moveSpeed
	{
		get
		{
			return this.mMoveSpeed;
		}
		set
		{
			this.mMoveSpeed = value;
		}
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x000567AF File Offset: 0x000549AF
	public void AddMoveSpeed(float num)
	{
		this.moveSpeed = this.mMoveSpeed + num;
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000567BF File Offset: 0x000549BF
	// (set) Token: 0x06000EFD RID: 3837 RVA: 0x000567C7 File Offset: 0x000549C7
	public int reduceInjury { get; set; }

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000567D0 File Offset: 0x000549D0
	public long Shield
	{
		get
		{
			return this.shield;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x06000EFF RID: 3839 RVA: 0x000567D8 File Offset: 0x000549D8
	// (set) Token: 0x06000F00 RID: 3840 RVA: 0x000567E0 File Offset: 0x000549E0
	public float mAttackSpeed { get; set; }

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x06000F01 RID: 3841 RVA: 0x000567E9 File Offset: 0x000549E9
	// (set) Token: 0x06000F02 RID: 3842 RVA: 0x000567F1 File Offset: 0x000549F1
	public float extendAttackSpeed { get; set; }

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x06000F03 RID: 3843 RVA: 0x000567FA File Offset: 0x000549FA
	// (set) Token: 0x06000F04 RID: 3844 RVA: 0x00056802 File Offset: 0x00054A02
	public virtual float attackSpeed
	{
		get
		{
			return this.mAttackSpeed;
		}
		set
		{
			this.mAttackSpeed = value;
		}
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x0005680B File Offset: 0x00054A0B
	public void AddAttackSpeed(float num)
	{
		this.extendAttackSpeed += num;
		if (base.isLocalPlayer)
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.RefreshBaoJi();
		}
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x00056837 File Offset: 0x00054A37
	public float GetAttackSpeed()
	{
		return this.attackSpeed + this.mAttackSpeed * (float)this.AGI * 0.002f + this.extendAttackSpeed;
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0005685B File Offset: 0x00054A5B
	public bool HasAuthority
	{
		get
		{
			return base.hasAuthority;
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00056863 File Offset: 0x00054A63
	public float AniSpeed
	{
		get
		{
			return this.aniSpeed;
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0005686B File Offset: 0x00054A6B
	public Vector3 SyncPos
	{
		get
		{
			return this.syncPos;
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00056873 File Offset: 0x00054A73
	public float SyncEulerY
	{
		get
		{
			return this.syncEulerY;
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0005687B File Offset: 0x00054A7B
	// (set) Token: 0x06000F0C RID: 3852 RVA: 0x00056883 File Offset: 0x00054A83
	public int mAttackPower { get; set; }

	// Token: 0x06000F0D RID: 3853 RVA: 0x0005688C File Offset: 0x00054A8C
	public void AddAttackPower(int num)
	{
		this.mAttackPower += num;
		if (base.isLocalPlayer)
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui != null)
			{
				ui.RefreshPlayerStateUI();
			}
			UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
			if (ui2 == null)
			{
				return;
			}
			ui2.RefreshPlayerStateUI();
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06000F0E RID: 3854 RVA: 0x000568D8 File Offset: 0x00054AD8
	// (set) Token: 0x06000F0F RID: 3855 RVA: 0x000568E0 File Offset: 0x00054AE0
	public int mArmor { get; set; }

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06000F10 RID: 3856 RVA: 0x000568E9 File Offset: 0x00054AE9
	public virtual int armor
	{
		get
		{
			return this.mArmor;
		}
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x000568F4 File Offset: 0x00054AF4
	public void AddArmor(int num)
	{
		int num2 = this.mArmor + num;
		if (num2 != this.mArmor)
		{
			this.mArmor = num2;
			this.CmdUpateMArmor(num2);
			if (base.isLocalPlayer)
			{
				UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
				if (ui != null)
				{
					ui.RefreshPlayerStateUI();
				}
				UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
				if (ui2 == null)
				{
					return;
				}
				ui2.RefreshPlayerStateUI();
			}
		}
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x00056954 File Offset: 0x00054B54
	[Command]
	public void CmdUpateMArmor(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		base.SendCommandInternal(typeof(RoleBase), "CmdUpateMArmor", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x00056994 File Offset: 0x00054B94
	[ClientRpc]
	public void RpcUpateMArmor(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		this.SendRPCInternal(typeof(RoleBase), "RpcUpateMArmor", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000569D3 File Offset: 0x00054BD3
	// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0004A3E3 File Offset: 0x000485E3
	public virtual float critical
	{
		get
		{
			return this.mCritical;
		}
		set
		{
			this.mCritical = value;
		}
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x000569DB File Offset: 0x00054BDB
	public void AddCritical(float num)
	{
		this.critical = this.mCritical + num;
		if (base.isLocalPlayer)
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.RefreshBaoJi();
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00056A07 File Offset: 0x00054C07
	// (set) Token: 0x06000F18 RID: 3864 RVA: 0x0004A402 File Offset: 0x00048602
	public virtual float criticalDamage
	{
		get
		{
			return this.mCriticalDamage;
		}
		set
		{
			this.mCriticalDamage = value;
		}
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x00056A0F File Offset: 0x00054C0F
	public void AddCriticalDamage(float num)
	{
		this.criticalDamage = this.mCriticalDamage + num;
		if (base.isLocalPlayer)
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.RefreshBaoJi();
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00056A3B File Offset: 0x00054C3B
	// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00056A43 File Offset: 0x00054C43
	public bool wudi { get; set; }

	// Token: 0x06000F1C RID: 3868 RVA: 0x00056A4C File Offset: 0x00054C4C
	public void SetWuDi(bool v)
	{
		if (!base.hasAuthority)
		{
			return;
		}
		this.wudi = v;
		this.CmdWuDi(this.wudi);
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00056A6A File Offset: 0x00054C6A
	public virtual int hpAddSec
	{
		get
		{
			return this.mHpAddSec;
		}
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x00056A72 File Offset: 0x00054C72
	public void AddHpAddSec(int num)
	{
		this.mHpAddSec += num;
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00056A82 File Offset: 0x00054C82
	// (set) Token: 0x06000F20 RID: 3872 RVA: 0x00056A8A File Offset: 0x00054C8A
	public int sta { get; set; }

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00056A93 File Offset: 0x00054C93
	public virtual int STA
	{
		get
		{
			return this.sta;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00056A9B File Offset: 0x00054C9B
	// (set) Token: 0x06000F23 RID: 3875 RVA: 0x00056AA3 File Offset: 0x00054CA3
	public int agi { get; set; }

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00056AAC File Offset: 0x00054CAC
	public virtual int AGI
	{
		get
		{
			return this.agi;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00056AB4 File Offset: 0x00054CB4
	// (set) Token: 0x06000F26 RID: 3878 RVA: 0x00056ABC File Offset: 0x00054CBC
	public int mSTR { get; set; }

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00056AC5 File Offset: 0x00054CC5
	public virtual int STR
	{
		get
		{
			return this.mSTR;
		}
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x00056AD0 File Offset: 0x00054CD0
	public void AddSTR(int num)
	{
		GameHelperClient.CheckCoronationGuard();
		this.mSTR += num;
		PlayerBase playerBase = this as PlayerBase;
		if (playerBase != null)
		{
			GameHelperClient.TrackAttributes(playerBase);
		}
		if (base.isLocalPlayer)
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui != null)
			{
				ui.RefreshPlayerStateUI();
			}
			UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
			if (ui2 == null)
			{
				return;
			}
			ui2.RefreshPlayerStateUI();
		}
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x00056B34 File Offset: 0x00054D34
	public void AddSTA(int num)
	{
		GameHelperClient.CheckCoronationGuard();
		int sta = this.STA;
		this.sta += num;
		this.AddPlayerSTAHp(this.STA - sta);
		PlayerBase playerBase = this as PlayerBase;
		if (playerBase != null)
		{
			GameHelperClient.TrackAttributes(playerBase);
		}
		if (base.isLocalPlayer)
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui != null)
			{
				ui.RefreshPlayerStateUI();
			}
			UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
			if (ui2 == null)
			{
				return;
			}
			ui2.RefreshPlayerStateUI();
		}
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x00056BAC File Offset: 0x00054DAC
	public void AddAGI(int num)
	{
		GameHelperClient.CheckCoronationGuard();
		this.agi += num;
		PlayerBase playerBase = this as PlayerBase;
		if (playerBase != null)
		{
			GameHelperClient.TrackAttributes(playerBase);
		}
		if (base.isLocalPlayer)
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui != null)
			{
				ui.RefreshPlayerStateUI();
			}
			UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
			if (ui2 == null)
			{
				return;
			}
			ui2.RefreshPlayerStateUI();
		}
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00056C0E File Offset: 0x00054E0E
	// (set) Token: 0x06000F2C RID: 3884 RVA: 0x00056C16 File Offset: 0x00054E16
	public int STRAdd { get; set; }

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00056C1F File Offset: 0x00054E1F
	// (set) Token: 0x06000F2E RID: 3886 RVA: 0x00056C27 File Offset: 0x00054E27
	public int AGIAdd { get; set; }

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00056C30 File Offset: 0x00054E30
	// (set) Token: 0x06000F30 RID: 3888 RVA: 0x00056C38 File Offset: 0x00054E38
	public int STAAdd { get; set; }

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00056C41 File Offset: 0x00054E41
	public virtual int FinalDoge
	{
		get
		{
			return this.doge;
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00056C49 File Offset: 0x00054E49
	// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0004A5CD File Offset: 0x000487CD
	public virtual float xiXue
	{
		get
		{
			return this.mXiXue;
		}
		set
		{
			this.mXiXue = value;
		}
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x00056C51 File Offset: 0x00054E51
	public void AddXiXue(float num)
	{
		this.xiXue = this.mXiXue + num;
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00056C61 File Offset: 0x00054E61
	public virtual float XiXueLvAll
	{
		get
		{
			return this.xiXueLv;
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00056C69 File Offset: 0x00054E69
	public Transform MyTransform
	{
		get
		{
			return this.myTransform;
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00056C71 File Offset: 0x00054E71
	public RoleState RoleState
	{
		get
		{
			return this.roleState;
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00056C79 File Offset: 0x00054E79
	public virtual long FinalAttackPower
	{
		get
		{
			return (long)this.mAttackPower + (long)this.STR;
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00056C8A File Offset: 0x00054E8A
	public RoleModeBase RoleModeBase
	{
		get
		{
			return this.roleModeBase;
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00056C92 File Offset: 0x00054E92
	// (set) Token: 0x06000F3B RID: 3899 RVA: 0x00056C9A File Offset: 0x00054E9A
	public RoleModeBase OldModeBase
	{
		get
		{
			return this.oldModeBase;
		}
		set
		{
			this.oldModeBase = value;
		}
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x00056CA3 File Offset: 0x00054EA3
	private void Awake()
	{
		this.myTransform = base.transform;
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00056CB4 File Offset: 0x00054EB4
	public virtual void InitRole(RoleType value, RoleAttribute roleAttribute, int attributeLevel = 1, EnemyCreateType enemyCreateType = EnemyCreateType.Normal)
	{
		this.mArmor = roleAttribute.armor;
		this.mMoveSpeed = roleAttribute.moveSpeed;
		string id = roleAttribute.id;
		if (value == RoleType.Player)
		{
			this.roleName = Util.GetHeroName((HeroType)int.Parse(id));
		}
		else if (value != RoleType.King)
		{
			this.roleName = Game.Language.Get(id, "");
		}
		if (value == RoleType.Player)
		{
			this.mAttackPower = roleAttribute.attackPower;
		}
		else if (value == RoleType.Enemy)
		{
			EnemyBase enemyBase = this as EnemyBase;
			if (enemyBase != null)
			{
				if (roleAttribute.type.Equals("boss"))
				{
					enemyBase.isBoss = true;
				}
				else
				{
					enemyBase.isBoss = false;
				}
				if (enemyBase.EnemyEntriesTypes != null)
				{
					for (int i = enemyBase.EnemyEntriesTypes.Length - 1; i > -1; i--)
					{
						this.roleName = PathDefine.Concat(Game.Language.Get(PathDefine.Concat("enemyEntries_", (int)enemyBase.EnemyEntriesTypes[i]), ""), StringDefine.Point, this.roleName);
					}
				}
				if (enemyCreateType == EnemyCreateType.ChallengeAndBOSS)
				{
					this.mAttackPower = Mathf.RoundToInt((float)roleAttribute.attackPower * GameHelperClient.spawnConfig.enemySpawnData[GameHelperClient.WaveNum].bossAttackLevel) * attributeLevel;
				}
				else
				{
					this.mAttackPower = Mathf.RoundToInt((float)roleAttribute.attackPower * GameHelperClient.spawnConfig.enemySpawnData[GameHelperClient.WaveNum].attackLevel) * attributeLevel;
				}
			}
		}
		else if (value == RoleType.Summon)
		{
			this.mAttackPower = roleAttribute.attackPower;
			EnemyBase enemyBase2 = this as EnemyBase;
			if (enemyBase2 != null)
			{
				if (roleAttribute.type.Equals("boss"))
				{
					enemyBase2.isBoss = true;
				}
				else
				{
					enemyBase2.isBoss = false;
				}
			}
		}
		this.roleType = value;
		this.aniName = -1;
		this.mSTR = roleAttribute.STR;
		this.sta = roleAttribute.STA;
		if (value == RoleType.Player)
		{
			if (base.isLocalPlayer)
			{
				UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
				if (ui != null)
				{
					ui.RefreshPlayerStateUI();
				}
				UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
				if (ui2 != null)
				{
					ui2.RefreshPlayerStateUI();
				}
			}
			this.AddPlayerSTAHp(this.STA);
		}
		this.agi = roleAttribute.AGI;
		this.STRAdd = roleAttribute.STRadd;
		this.STAAdd = roleAttribute.STAadd;
		this.AGIAdd = roleAttribute.AGIadd;
		this.mHpAddSec = roleAttribute.hpRecover;
		this.attackSpeed = roleAttribute.attackSpeed;
		this.roleBuffManager = new RoleBuffManager(this);
		this.emitValue = 0f;
		this.isEmit = false;
		this.SetEmit(this.emitValue);
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x00056F25 File Offset: 0x00055125
	private void StartHitEmit()
	{
		this.SetEmit(0.9f);
		this.isEmit = false;
		this.emitValue = 0.1f;
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x00056F44 File Offset: 0x00055144
	private void SetEmit(float value)
	{
		int count = this.roleModeBase.myRenderers.Count;
		for (int i = 0; i < count; i++)
		{
			this.roleModeBase.myRenderers[i].material.SetFloat(ShaderDefine.Property_EmitInt, value);
		}
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x00056F90 File Offset: 0x00055190
	public void UpdateEmit(float updateTime)
	{
		if (this.isEmit)
		{
			this.emitValue -= updateTime * 15f;
			if (this.emitValue <= 0f)
			{
				this.emitValue = 0f;
				this.isEmit = false;
			}
			this.SetEmit(this.emitValue);
			return;
		}
		if (this.emitValue > 0f)
		{
			this.emitValue -= updateTime;
			if (this.emitValue <= 0f)
			{
				this.OnEmitOver();
			}
		}
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x00057013 File Offset: 0x00055213
	private void OnEmitOver()
	{
		this.isEmit = true;
		this.emitValue = 0.9f;
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x00057028 File Offset: 0x00055228
	public virtual void UpdateEvent()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase != null)
		{
			roleModeBase.UpdateEvent();
		}
		if (!base.hasAuthority)
		{
			Vector3 vector = this.myTransform.position;
			Quaternion quaternion = this.myTransform.rotation;
			vector = Vector3.Lerp(vector, this.syncPos, 10f * Time.deltaTime);
			quaternion = Quaternion.Lerp(quaternion, Quaternion.Euler(0f, this.syncEulerY, 0f), 10f * Time.deltaTime);
			this.myTransform.position = vector;
			this.myTransform.rotation = quaternion;
		}
		else if (this.roleState != RoleState.Dead)
		{
			if ((Time.time > this.syncPosTime && !Mathf.Approximately(this.syncPos.x, this.myTransform.position.x)) || !Mathf.Approximately(this.syncPos.z, this.myTransform.position.z) || !Mathf.Approximately(this.syncPos.y, this.myTransform.position.y))
			{
				this.syncPosTime += Time.time + 0.1f;
				this.CmdSyncPos(this.myTransform.position);
			}
			if (Time.time > this.syncAngleTime && !Mathf.Approximately(this.syncEulerY, this.myTransform.eulerAngles.y))
			{
				this.syncAngleTime = Time.time + 0.1f;
				this.CmdSyncEulerY(this.myTransform.eulerAngles.y);
			}
			this.addHpTime += Time.deltaTime;
			if (this.addHpTime >= 1f)
			{
				long num = 0L;
				if (this.hpAddSec != 0 && (this.hpAddSec > 0 || !GameHelperClient.isReady))
				{
					num = (long)this.hpAddSec;
				}
				if (!Mathf.Approximately(this.hpAddSecRate, 0f) && (this.hpAddSecRate > 0f || !GameHelperClient.isReady))
				{
					num += ConstDefine.ClampBattleValue((double)((float)this.maxHp * this.hpAddSecRate));
				}
				if (GameHelperClient.isKingBattle && num > 0L)
				{
					float num2 = (float)num * GameHelperClient.GetKingBattleAddHpLevel();
					this.AddPlayerHp((double)num2);
				}
				else
				{
					this.AddPlayerHp(num);
				}
				this.addHpTime = 0f;
			}
		}
		if (this.roleState == RoleState.Dead)
		{
			this.UpdateDead();
		}
		else if (this.roleState == RoleState.ShowPose)
		{
			this.UpdateShowPose();
		}
		else if (this.roleState == RoleState.XuanYun)
		{
			this.UpdateXuanYun();
		}
		else if (this.roleState == RoleState.Action)
		{
			this.UpdateAction();
		}
		if (this.wudi)
		{
			if (this.wuDiEffect == null)
			{
				this.wuDiEffect = AssetManager.LoadPrefab("Effect/ShieldYellow", null, true);
				this.wuDiEffect.transform.SetParent(base.transform);
				this.wuDiEffect.transform.localPosition = new Vector3(0f, 0.8f + this.roleModeBase.addRange * 2f, 0f);
			}
		}
		else if (this.wuDiEffect != null)
		{
			AssetManager.UnLoadPrefab(this.wuDiEffect, false);
			this.wuDiEffect = null;
		}
		if (this.canXuanYunLastTime > 0f)
		{
			this.canXuanYunLastTime -= Time.deltaTime;
		}
		this.roleBuffManager.Update();
		this.UpdateLocalBuff();
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x0005737B File Offset: 0x0005557B
	protected virtual void UpdateShowPose()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.UpdateShowPose();
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x00057390 File Offset: 0x00055590
	protected virtual void UpdateXuanYun()
	{
		if (base.hasAuthority)
		{
			float deltaTime = Time.deltaTime;
			this.timer -= deltaTime;
			if (this.timer < 0f)
			{
				this.UpdateRoleState(RoleState.Idle);
			}
		}
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected virtual void UpdateAction()
	{
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x000573D0 File Offset: 0x000555D0
	protected virtual void UpdateDead()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase != null)
		{
			roleModeBase.UpdateDead();
		}
		float deltaTime = Time.deltaTime;
		this.timer += deltaTime;
		float num = 10f;
		if (this.roleType == RoleType.Player)
		{
			num = Mathf.Max(5f, GameHelperClient.gameConfig.PlayerRelifeTime + (float)(this as PlayerBase).addRelifeTime);
		}
		if (this.timer > num)
		{
			this.ExitDeadState();
			return;
		}
		if (this.roleType != RoleType.Player && this.timer > (float)this.deadStartMoveTime && this.timer < (float)this.deadEndMoveTime)
		{
			this.animTransform.localPosition += deltaTime * this.deadMoveSpeed * Vector3.down;
		}
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x00057490 File Offset: 0x00055690
	public void AddPlayerSTAHp(int num)
	{
		if (base.hasAuthority)
		{
			this.CmdUpdateMaxHp((long)(num * 10), base.netId);
		}
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x000574AC File Offset: 0x000556AC
	public void AddPlayerHp(double numF)
	{
		if (numF <= 0.0)
		{
			return;
		}
		long num = ConstDefine.ClampBattleValue(numF);
		this.AddPlayerHp(num);
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x000574D4 File Offset: 0x000556D4
	public void AddPlayerHp(long num)
	{
		if (num == 0L || this.roleState == RoleState.Dead)
		{
			return;
		}
		if (base.hasAuthority)
		{
			if (num > 0L)
			{
				PlayerBase playerBase = this as PlayerBase;
				if (playerBase != null && playerBase.hpAddUpgrade > 0f)
				{
					num += ConstDefine.ClampBattleValue((double)((float)num * playerBase.hpAddUpgrade));
				}
			}
			RoleBase.HealthHp healthHp = this.healthHpEvent;
			if (healthHp != null)
			{
				healthHp(num);
			}
		}
		if (num > 0L && this.hp == this.maxHp)
		{
			return;
		}
		this.CmdUpdateHp(num, base.netId, -1);
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x0005755C File Offset: 0x0005575C
	[Command]
	private void CmdSyncPos(Vector3 value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(value);
		base.SendCommandInternal(typeof(RoleBase), "CmdSyncPos", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x0005759C File Offset: 0x0005579C
	[Command]
	private void CmdSyncEulerY(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		base.SendCommandInternal(typeof(RoleBase), "CmdSyncEulerY", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x000575DC File Offset: 0x000557DC
	[Command]
	private void CmdHp(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		base.SendCommandInternal(typeof(RoleBase), "CmdHp", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x0005761C File Offset: 0x0005581C
	[Command]
	public void CmdWuDi(bool v)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteBool(v);
		base.SendCommandInternal(typeof(RoleBase), "CmdWuDi", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x0005765C File Offset: 0x0005585C
	[ClientRpc]
	public void RpcWuDi(bool v)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteBool(v);
		this.SendRPCInternal(typeof(RoleBase), "RpcWuDi", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x0005769B File Offset: 0x0005589B
	public virtual void ExitDeadState()
	{
		this.animTransform.localPosition = Vector3.zero;
		this.timer = 0f;
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x000576B8 File Offset: 0x000558B8
	public void AddHeroSkill(ActiveSkillEnum activeSkill, SkillBase removeSkill)
	{
		ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkill];
		SkillBase skillBase = new SkillBase();
		skillBase.activeSkillEnum = activeSkill;
		skillBase.iconName = activeSkillData.icon;
		if (GameHelperClient.isSaveHero && activeSkillData.isSaveMode)
		{
			skillBase.iconName = PathDefine.Concat(skillBase.iconName, StringDefine.SaveMode);
		}
		skillBase.roleBase = (this as PlayerBase);
		skillBase.cdTime = activeSkillData.cd;
		skillBase.cost = activeSkillData.cost;
		skillBase.skillName = activeSkillData.name;
		skillBase.skillId = activeSkillData.id;
		skillBase.skillBookId = GameHelperClient.GetLocalSkillBookId();
		skillBase.quality = DropDefine.QualityAry.IndexOf(activeSkillData.quality);
		skillBase.languageName = Game.Language.Get(PathDefine.Concat("a_", (int)activeSkill), "");
		skillBase.chargingMax = activeSkillData.chargingNum;
		skillBase.chargingCd = activeSkillData.chargingCd;
		skillBase.skillAttribute = activeSkillData.attribute;
		if (activeSkillData.total)
		{
			skillBase.totalName = Game.Language.Get(PathDefine.Concat("a_", (int)activeSkill, StringDefine.Total), "");
			skillBase.totals = new int[1];
		}
		skillBase.InitActiveSkill();
		Util.AddSkill(skillBase, removeSkill);
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x00057808 File Offset: 0x00055A08
	public void AddHeroPasssiveSkillBook(PasssiveSkillEnum passsiveSkillEnum)
	{
		object dic = ExcelManager.allExcelData["passsiveSkill"];
		int num = (int)passsiveSkillEnum;
		Dictionary<string, object> dictionary = (Dictionary<string, object>)dic.DIC(num.ToString());
		PasssiveSkill passsiveSkill = Util.GetPasssiveSkill(dictionary.DIC("class"));
		if (passsiveSkill != null)
		{
			passsiveSkill.skillBookId = GameHelperClient.GetLocalSkillBookId();
			passsiveSkill.iconName = dictionary.DIC("icon");
			bool flag = dictionary.DIC("saveMode");
			if (GameHelperClient.isSaveHero && flag)
			{
				passsiveSkill.iconName = PathDefine.Concat(passsiveSkill.iconName, StringDefine.SaveMode);
			}
			passsiveSkill.cdTime = dictionary.DIC("cd");
			dictionary.DIC("info");
			passsiveSkill.SetData(dictionary);
			passsiveSkill.skillName = dictionary.DIC("name");
			passsiveSkill.roleBase = GameHelperClient.localPlayer;
			passsiveSkill.isPasssiveSkill = true;
			passsiveSkill.Enter();
			passsiveSkill.languageName = Game.Language.Get(PathDefine.Concat("p_", passsiveSkill.skillId), "");
			passsiveSkill.skillAttribute = GameDataManager.GetSkillAttribute(dictionary.DIC("attribute"));
			passsiveSkill.roleBase = (this as PlayerBase);
			Util.AddSkill(passsiveSkill, null);
		}
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x00057934 File Offset: 0x00055B34
	public void AddActiveSkillBook(ActiveSkillEnum activeSkill, SkillBase removeSkill)
	{
		ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkill];
		SkillBase skillBase = new SkillBase();
		skillBase.activeSkillEnum = activeSkill;
		skillBase.iconName = activeSkillData.icon;
		skillBase.skillName = activeSkillData.name;
		skillBase.skillId = activeSkillData.id;
		skillBase.roleBase = (this as PlayerBase);
		skillBase.cdTime = activeSkillData.cd;
		skillBase.cost = activeSkillData.cost;
		skillBase.quality = DropDefine.QualityAry.IndexOf(activeSkillData.quality);
		skillBase.languageName = Game.Language.Get(PathDefine.Concat("a_", (int)activeSkill), "");
		skillBase.skillAttribute = activeSkillData.attribute;
		skillBase.skillBookId = GameHelperClient.GetLocalSkillBookId();
		skillBase.chargingMax = activeSkillData.chargingNum;
		skillBase.chargingCd = activeSkillData.chargingCd;
		if (activeSkillData.total)
		{
			skillBase.totalName = Game.Language.Get(PathDefine.Concat("a_", (int)activeSkill, StringDefine.Total), "");
			skillBase.totals = new int[1];
		}
		skillBase.InitActiveSkill();
		Util.AddSkill(skillBase, removeSkill);
		(this as PlayerBase).CmdUpdateSyncActiveSkillEnum(activeSkill);
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x00057A68 File Offset: 0x00055C68
	public void AddPasssiveSkillBook(PasssiveSkillEnum passsiveSkillEnum, SkillBase removeSkill)
	{
		object dic = ExcelManager.allExcelData["passsiveSkill"];
		int num = (int)passsiveSkillEnum;
		Dictionary<string, object> dictionary = (Dictionary<string, object>)dic.DIC(num.ToString());
		PasssiveSkill passsiveSkill = Util.GetPasssiveSkill(dictionary.DIC("class"));
		if (passsiveSkill != null)
		{
			passsiveSkill.skillBookId = GameHelperClient.GetLocalSkillBookId();
			passsiveSkill.iconName = dictionary.DIC("icon");
			passsiveSkill.cdTime = dictionary.DIC("cd");
			passsiveSkill.SetData(dictionary);
			passsiveSkill.skillName = dictionary.DIC("name");
			passsiveSkill.roleBase = GameHelperClient.localPlayer;
			passsiveSkill.isPasssiveSkill = true;
			passsiveSkill.Enter();
			passsiveSkill.languageName = Game.Language.Get(PathDefine.Concat("p_", passsiveSkill.skillId), "");
			passsiveSkill.roleBase = (this as PlayerBase);
			Util.AddSkill(passsiveSkill, removeSkill);
		}
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x00057B44 File Offset: 0x00055D44
	public void PlayAni(int nameHash, float speed = 1f)
	{
		if (base.gameObject.activeSelf)
		{
			RoleModeBase roleModeBase = this.roleModeBase;
			if (!(((roleModeBase != null) ? roleModeBase.myAnim : null) == null))
			{
				if (this.aniName == nameHash && Mathf.Approximately(this.aniSpeed, speed))
				{
					return;
				}
				if (this.isOverrideAnim)
				{
					return;
				}
				if (!this.animTransform.gameObject.activeSelf)
				{
					return;
				}
				this.aniName = nameHash;
				this.roleModeBase.myAnim.speed = speed;
				this.aniSpeed = speed;
				this.roleModeBase.myAnim.Play(nameHash);
				return;
			}
		}
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x00057BDD File Offset: 0x00055DDD
	public void ResetAnim()
	{
		this.aniName = -1;
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x00057BE8 File Offset: 0x00055DE8
	public void PlayAni(int nameHash, float speed, float cross)
	{
		if (!base.gameObject.activeSelf || this.roleModeBase.myAnim == null)
		{
			return;
		}
		if (this.aniName == nameHash && Mathf.Approximately(this.aniSpeed, speed))
		{
			return;
		}
		if (this.isOverrideAnim)
		{
			return;
		}
		if (!this.animTransform.gameObject.activeSelf)
		{
			return;
		}
		this.aniName = nameHash;
		this.roleModeBase.myAnim.speed = speed;
		this.aniSpeed = speed;
		this.roleModeBase.myAnim.CrossFadeInFixedTime(nameHash, cross);
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x00057C7C File Offset: 0x00055E7C
	public void PlayAni(int nameHash, float speed, float cross, float normalizedTimeOffset)
	{
		if (!base.gameObject.activeSelf || this.roleModeBase.myAnim == null)
		{
			return;
		}
		if (this.aniName == nameHash && Mathf.Approximately(this.aniSpeed, speed))
		{
			return;
		}
		if (this.isOverrideAnim)
		{
			return;
		}
		if (!this.animTransform.gameObject.activeSelf)
		{
			return;
		}
		this.aniName = nameHash;
		this.roleModeBase.myAnim.speed = speed;
		this.aniSpeed = speed;
		this.roleModeBase.myAnim.CrossFadeInFixedTime(nameHash, cross, 0, normalizedTimeOffset);
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x00057D12 File Offset: 0x00055F12
	public void UpdateAnimSpeed(float speed)
	{
		if (this.roleModeBase.myAnim == null)
		{
			return;
		}
		if (Mathf.Approximately(this.aniSpeed, speed))
		{
			return;
		}
		this.roleModeBase.myAnim.speed = speed;
		this.aniSpeed = speed;
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x00057D50 File Offset: 0x00055F50
	public void ReplayAnim()
	{
		this.timer = 0f;
		this.isOverrideAnim = false;
		if (this.roleState == RoleState.Idle)
		{
			this.aniName = AnimDefine.Idle;
		}
		else if (this.roleState == RoleState.Run)
		{
			this.aniName = AnimDefine.Run;
		}
		else if (this.roleState == RoleState.Dead)
		{
			this.aniName = AnimDefine.Dead;
		}
		this.roleModeBase.myAnim.speed = 1f;
		this.roleModeBase.myAnim.CrossFadeInFixedTime(this.aniName, 0.1f);
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x00057DE0 File Offset: 0x00055FE0
	public float GetDistanceV2(Vector3 pos)
	{
		Vector3 position = this.myTransform.position;
		return Mathf.Sqrt(Mathf.Pow(pos.x - position.x, 2f) + Mathf.Pow(pos.z - position.z, 2f));
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x00057E2D File Offset: 0x0005602D
	public virtual float GetMoveSpeed()
	{
		return Mathf.Min(10f, this.moveSpeed * this.moveSpeedPercent);
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x00057E48 File Offset: 0x00056048
	public void UpdateRoleState(RoleState value)
	{
		if (this.roleState != value && base.hasAuthority)
		{
			if (this.hp <= 0L || this.roleState == RoleState.Dead)
			{
				return;
			}
			this.localSequenceId++;
			this.localRoleState = value;
			this.ExitState(this.roleState);
			this.ClientUpdateState(value);
			this.CmdUpdateRoleState(value, this.localSequenceId);
		}
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x00057EB0 File Offset: 0x000560B0
	private void ExitState(RoleState value)
	{
		if (value == RoleState.Attack)
		{
			this.OnExitAttack();
			return;
		}
		if (value == RoleState.Dead)
		{
			this.OnExitDead();
			return;
		}
		if (value == RoleState.Skill)
		{
			this.OnExitSkill();
			return;
		}
		if (value == RoleState.Skill2)
		{
			this.OnExitSkill2();
			return;
		}
		if (value == RoleState.ShowPose)
		{
			this.OnExitShowPose();
			return;
		}
		if (value == RoleState.XuanYun)
		{
			this.OnExitXuanYun();
			return;
		}
		if (value == RoleState.Skill3)
		{
			this.OnExitSkill3();
			return;
		}
		if (value == RoleState.Action)
		{
			this.OnExitAction();
		}
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x00057F15 File Offset: 0x00056115
	protected virtual void OnExitShowPose()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnExitShowPose();
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x00057F27 File Offset: 0x00056127
	protected virtual void OnExitXuanYun()
	{
		if (this.roleStateEffect != null)
		{
			AssetManager.UnLoadPrefab(this.roleStateEffect, false);
			this.roleStateEffect = null;
		}
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected virtual void OnExitAction()
	{
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x00057F4A File Offset: 0x0005614A
	protected virtual void OnExitSkill()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnExitSkill();
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x00057F5C File Offset: 0x0005615C
	protected virtual void OnExitSkill2()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnExitSkill2();
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x00057F6E File Offset: 0x0005616E
	protected virtual void OnExitSkill3()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnExitSkill3();
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x00057F80 File Offset: 0x00056180
	protected virtual void OnExitDead()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnExitDead();
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x00057F94 File Offset: 0x00056194
	private void ClientUpdateState(RoleState value)
	{
		this.NetworkroleState = value;
		if (this.roleState == RoleState.Idle)
		{
			this.OnStartIdle();
			return;
		}
		if (this.roleState == RoleState.Run)
		{
			this.OnStartRun();
			return;
		}
		if (this.roleState == RoleState.Attack)
		{
			this.OnStartAttack();
			return;
		}
		if (this.roleState == RoleState.Dead)
		{
			this.OnStartDead();
			return;
		}
		if (this.roleState == RoleState.Skill)
		{
			this.OnStartSkill();
			return;
		}
		if (this.roleState == RoleState.Skill2)
		{
			this.OnStartSkill2();
			return;
		}
		if (this.roleState == RoleState.ShowPose)
		{
			this.OnStartShowPose();
			return;
		}
		if (this.roleState == RoleState.XuanYun)
		{
			this.OnStartXuanYun();
			return;
		}
		if (this.roleState == RoleState.Skill3)
		{
			this.OnStartSkill3();
			return;
		}
		if (this.roleState == RoleState.Action)
		{
			this.OnStartAction();
		}
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x00058048 File Offset: 0x00056248
	[Command]
	private void CmdUpdateRoleState(RoleState value, int sequenceIdValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_RoleState(writer, value);
		writer.WriteInt(sequenceIdValue);
		base.SendCommandInternal(typeof(RoleBase), "CmdUpdateRoleState", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x00058094 File Offset: 0x00056294
	private void OnChangeRoleState(RoleState _, RoleState value)
	{
		if (!base.hasAuthority)
		{
			this.ExitState(_);
			this.ClientUpdateState(value);
			return;
		}
		if (value != this.localRoleState && this.syncSequenceId != 0 && this.syncSequenceId < this.localSequenceId)
		{
			this.NetworkroleState = this.localRoleState;
		}
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x000580E3 File Offset: 0x000562E3
	protected virtual void OnStartSkill()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnStartSkill();
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x000580F5 File Offset: 0x000562F5
	protected virtual void OnStartSkill2()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnStartSkill2();
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x00058107 File Offset: 0x00056307
	protected virtual void OnStartSkill3()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnStartSkill3();
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x00058119 File Offset: 0x00056319
	protected virtual void OnExitAttack()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnExitAttack();
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x0005812B File Offset: 0x0005632B
	protected virtual void OnStartShowPose()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnStartShowPose();
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x00058140 File Offset: 0x00056340
	protected virtual void OnStartXuanYun()
	{
		if (this.roleStateEffect != null)
		{
			return;
		}
		this.roleStateEffect = AssetManager.LoadPrefab(EffectDefine.XuanYun, null, true);
		Transform transform = this.roleStateEffect.transform;
		transform.SetParent(this.myTransform);
		transform.localScale = Vector3.one * (1.25f + this.roleModeBase.addRange * 2f);
		Vector3 headUIPos = this.GetHeadUIPos();
		headUIPos.y *= 0.55f;
		transform.position = headUIPos;
		this.PlayAni(AnimDefine.Idle, 0f, 0.1f);
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x000581DE File Offset: 0x000563DE
	protected virtual void OnStartAction()
	{
		this.PlayAni(AnimDefine.Idle, 1f, 0.1f);
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x000581F5 File Offset: 0x000563F5
	protected virtual void OnStartIdle()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnStartIdle();
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x00058207 File Offset: 0x00056407
	protected virtual void OnStartRun()
	{
		this.PlayAni(AnimDefine.Run, 1f, 0.1f);
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x0005821E File Offset: 0x0005641E
	protected virtual void OnStartAttack()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnStartAttack();
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00058230 File Offset: 0x00056430
	protected virtual void OnStartDead()
	{
		if (this.roleStateEffect != null)
		{
			AssetManager.UnLoadPrefab(this.roleStateEffect, false);
			this.roleStateEffect = null;
		}
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase != null)
		{
			roleModeBase.OnStartDead();
		}
		this.timer = 0f;
		if (this.roleType != RoleType.King)
		{
			this.trackRoleBase = null;
		}
		RoleBase.DieEvent dieEvent = this.dieEvent;
		if (dieEvent != null)
		{
			dieEvent(this);
		}
		this.ClearAllBuff(true);
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x000582A4 File Offset: 0x000564A4
	public Vector3 GetHeadUIPos()
	{
		return this.myTransform.position + new Vector3(0f, this.animTransform.localScale.y / this.roleModeBase.baseModeScale.y * this.roleModeBase.headUIHeight, 0f);
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x00058300 File Offset: 0x00056500
	public Vector3 GetAttackPos()
	{
		return this.myTransform.position + new Vector3(0f, this.animTransform.localScale.y / this.roleModeBase.baseModeScale.y * this.roleModeBase.headUIHeight * 0.5f, 0f);
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x00058360 File Offset: 0x00056560
	public long OnHit(RoleBase attackRole, double damage, float attackEulerY, AttackType attackType, bool isAttackWeek)
	{
		if (this.roleState == RoleState.Dead)
		{
			return 0L;
		}
		if (this.roleType == RoleType.Enemy)
		{
			EnemyModeBase enemyModeBase = this.roleModeBase as EnemyModeBase;
			if (enemyModeBase.tmpSpine != null && (this.hitTweener == null || !this.hitTweener.IsActive() || !this.hitTweener.IsPlaying()))
			{
				this.hitTweener = this.myTransform.DOPunchScale(enemyModeBase.punchScale, enemyModeBase.punchTime, 1, 1f).OnComplete(delegate
				{
					this.myTransform.localScale = Vector3.one;
				});
			}
		}
		bool flag = (this.roleType == RoleType.Player || this.roleType == RoleType.King) && !base.hasAuthority;
		if (!flag && attackType == AttackType.Normal && this.FinalDoge > 0)
		{
			float num = Random.Range(0f, 1f);
			float num2 = 1f - Util.GetArmorLevel(this.FinalDoge);
			if (num <= num2)
			{
				if (((base.isLocalPlayer || this.roleType == RoleType.King) && damage > (double)((float)this.maxHp * 0.15f)) || this.roleType == RoleType.Enemy)
				{
					Game.UI.GetUI<UI_PlayerState>().ShowDoge(this.GetAttackPos());
				}
				RoleBase.DogeEvent dogeEvent = this.dogeEvent;
				if (dogeEvent != null)
				{
					dogeEvent();
				}
				return 0L;
			}
		}
		bool flag2 = (this.IsFromRoleType(RoleType.King) && attackRole.IsFromRoleType(RoleType.Player)) || (this.IsFromRoleType(RoleType.Player) && attackRole.IsFromRoleType(RoleType.King));
		if (attackType == AttackType.Normal)
		{
			if (isAttackWeek)
			{
				damage *= (double)attackRole.criticalDamage;
				RoleBase.Critical critical = attackRole.criticalEvent;
				if (critical != null)
				{
					critical(this, ConstDefine.ClampBattleValue(damage));
				}
			}
			if (attackRole.attackEnemyEvent != null)
			{
				float num3 = RoleBase.ToFloatBattleValue(damage);
				damage = (double)attackRole.attackEnemyEvent(attackRole, this, ref num3);
			}
			if (!Mathf.Approximately(attackRole.xiXue, 0f))
			{
				float num4 = attackRole.xiXue;
				if (flag2)
				{
					num4 *= GameHelperClient.GetKingBattleAttackAddHpLevel();
				}
				GameHelperClient.localPlayer.StartHealthHp((double)num4, attackRole);
			}
		}
		else if (attackType == AttackType.Skill)
		{
			if (isAttackWeek)
			{
				damage *= (double)(1f + (attackRole.criticalDamage - 1f) * attackRole.skillCriticalLevel);
			}
			if (attackRole.skillEnemyEvent != null)
			{
				float num5 = RoleBase.ToFloatBattleValue(damage);
				damage = (double)attackRole.skillEnemyEvent(attackRole, this, ref num5);
			}
		}
		else if (attackType == AttackType.Buff)
		{
			if (isAttackWeek)
			{
				damage *= (double)(1f + (attackRole.criticalDamage - 1f) * attackRole.buffCriticalLevel);
			}
			if (attackRole.roleType == RoleType.Player || attackRole.roleType == RoleType.King)
			{
				damage *= (double)(1f + (attackRole as PlayerBase).buffAddDamage);
			}
		}
		else if (attackType == AttackType.AttackEffect && (attackRole.roleType == RoleType.Player || attackRole.roleType == RoleType.King))
		{
			damage *= (double)(1f + (attackRole as PlayerBase).addAttackEffectDamage);
		}
		if (attackRole.roleType == RoleType.Player || attackRole.roleType == RoleType.King)
		{
			PlayerBase playerBase = attackRole as PlayerBase;
			if (attackType != AttackType.AttackEffect && attackType != AttackType.TrueDamage)
			{
				damage += (double)((float)playerBase.extraDamage * (1f + playerBase.addAttackEffectDamage));
			}
			float num6 = playerBase.addDamagePercent;
			if (this.roleType == RoleType.Enemy)
			{
				EnemyBase enemyBase = this as EnemyBase;
				if (enemyBase.isBoss)
				{
					num6 += playerBase.addBossEnemy;
				}
				else if (enemyBase.isElite)
				{
					num6 += playerBase.addEliteEnemy;
				}
				else
				{
					num6 += playerBase.addNormalEnemy;
				}
			}
			if (attackType == AttackType.AttackEffect)
			{
				num6 *= 0.75f;
			}
			if (attackType != AttackType.TrueDamage)
			{
				damage *= (double)(1f + num6);
			}
			if (attackType == AttackType.Normal)
			{
				if (!Mathf.Approximately(attackRole.XiXueLvAll, 0f))
				{
					double num7 = damage * (double)attackRole.XiXueLvAll;
					if (flag2)
					{
						num7 *= (double)(GameHelperClient.GetKingBattleDamageLevel() * GameHelperClient.GetKingBattleAttackPercentAddHpLevel());
					}
					GameHelperClient.localPlayer.StartHealthHp(num7, attackRole);
				}
			}
			else if (attackType == AttackType.Skill)
			{
				float magicXiXue = (attackRole as PlayerBase).magicXiXue;
				if (!Mathf.Approximately(magicXiXue, 0f))
				{
					double num8 = damage * (double)magicXiXue;
					if (flag2)
					{
						num8 *= (double)(GameHelperClient.GetKingBattleDamageLevel() * GameHelperClient.GetKingBattleMagicAddHpLevel());
					}
					GameHelperClient.localPlayer.StartHealthHp(num8, attackRole);
				}
			}
			if (attackRole.finalAttackEvent != null && attackType != AttackType.TrueDamage)
			{
				float num9 = RoleBase.ToFloatBattleValue(damage);
				damage = (double)attackRole.finalAttackEvent(attackRole, this, attackType, ref num9);
			}
		}
		if (!flag && attackType != AttackType.TrueDamage)
		{
			if (this.roleType == RoleType.Player || this.roleType == RoleType.King)
			{
				PlayerBase playerBase2 = this as PlayerBase;
				if (attackType == AttackType.Skill)
				{
					damage *= (double)Util.GetArmorLevel(playerBase2.FinalSkillReduction);
				}
			}
			float armorLevel = Util.GetArmorLevel(this.armor);
			damage *= (double)armorLevel;
			damage -= (double)(flag2 ? ((float)this.reduceInjury * GameHelperClient.GetKingBattleReduceLevel()) : ((float)this.reduceInjury));
			if (damage < 0.0)
			{
				damage = 0.0;
			}
			if (this.damageEvent != null)
			{
				float num10 = RoleBase.ToFloatBattleValue(damage);
				damage = (double)this.damageEvent(attackRole, this, attackType, ref num10);
			}
		}
		if (damage < 0.0)
		{
			damage = 0.0;
		}
		if (flag)
		{
			GameHelperClient.localPlayer.CmdAttackOtherPlayer(damage, attackType, attackRole.netId, attackEulerY, base.netId);
			return ConstDefine.ClampBattleValue(damage);
		}
		long num11 = this.OnHitUpdateHp(damage, attackType, attackRole, attackEulerY, flag2);
		if (attackRole.HasAuthority && (attackRole.roleType == RoleType.Player || attackRole.roleType == RoleType.Summon))
		{
			GameHelperClient.localPlayer.damageStatic += num11;
			if (GameHelperClient.localPlayer.damageStatic > 999999999999999999L)
			{
				GameHelperClient.localPlayer.damageStatic = 999999999999999999L;
			}
		}
		return num11;
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x000588D4 File Offset: 0x00056AD4
	protected long OnHitUpdateHp(double damage, AttackType attackType, RoleBase attackRole, float attackEulerY, bool isKingBattle)
	{
		if (this.wudi)
		{
			damage = 0.0;
		}
		else if (this.roleType == RoleType.Enemy)
		{
			this.StartHitEmit();
		}
		Vector3 attackPos = this.GetAttackPos();
		if (attackType != AttackType.AttackEffect)
		{
			if (damage > 0.0)
			{
				Game.EffectManager.PlayEffect((this.roleType == RoleType.Enemy) ? EffectDefine.HitEffect : EffectDefine.PlayerHitEffect, 2f, attackPos, Vector3.one, new Vector3(0f, attackEulerY - 90f, 0f));
			}
			if (attackType != AttackType.Buff)
			{
				Game.AudioManager.PlayHitAudio(this.roleType, attackPos);
			}
		}
		double num = damage;
		if (this.shield > 0L && num > 0.0)
		{
			if (attackType == AttackType.Normal)
			{
				if (attackRole.normalBreakShield > 0f)
				{
					double num2 = damage * (double)attackRole.normalBreakShield;
					num2 = Math.Min(num2, (double)this.shield - num);
					if (num2 > 0.0)
					{
						num += num2;
					}
				}
			}
			else if (attackType == AttackType.Skill && attackRole.skillBreakShield > 0f)
			{
				double num3 = damage * (double)attackRole.skillBreakShield;
				num3 = Math.Min(num3, (double)this.shield - num);
				if (num3 > 0.0)
				{
					num += num3;
				}
			}
		}
		if (isKingBattle)
		{
			num *= (double)GameHelperClient.GetKingBattleDamageLevel();
		}
		long num4 = ConstDefine.ClampBattleValue(num);
		GameHelperClient.localPlayer.CmdUpdateHp(-num4, base.netId, (int)attackRole.netId);
		return num4;
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00058A37 File Offset: 0x00056C37
	protected static float ToFloatBattleValue(double value)
	{
		if (double.IsNaN(value))
		{
			return 0f;
		}
		if (value > 3.4028234663852886E+38)
		{
			return float.MaxValue;
		}
		if (value < -3.4028234663852886E+38)
		{
			return float.MinValue;
		}
		return (float)value;
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x00058A6D File Offset: 0x00056C6D
	public bool IsFromRoleType(RoleType roleTypeCheck)
	{
		return this.roleType == roleTypeCheck || (roleTypeCheck == RoleType.Player && this.fatherId != -1 && this.fatherType == roleTypeCheck) || (roleTypeCheck == RoleType.King && this.fatherId != -1 && this.fatherType == roleTypeCheck);
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x00058AAC File Offset: 0x00056CAC
	[Command]
	public void CmdUpdateMaxHp(long updateValue, uint updateNetId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteLong(updateValue);
		writer.WriteUInt(updateNetId);
		base.SendCommandInternal(typeof(RoleBase), "CmdUpdateMaxHp", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x00058AF8 File Offset: 0x00056CF8
	[Server]
	public void ServerUpdateMaxHp(long updateValue)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void RoleBase::ServerUpdateMaxHp(System.Int64)' called when server was not active");
			return;
		}
		long num = this.maxHp;
		if (this.roleType == RoleType.Player)
		{
			PlayerBase playerBase = this as PlayerBase;
			playerBase.SetBaseMaxHp(playerBase.baseMaxHp + updateValue);
		}
		else
		{
			this.NetworkmaxHp = ConstDefine.ClampMaxHp(this.maxHp + updateValue);
		}
		if (updateValue > 0L)
		{
			this.Networkhp = this.hp + (this.maxHp - num);
		}
		if (this.hp > this.maxHp)
		{
			this.Networkhp = this.maxHp;
		}
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x00058B88 File Offset: 0x00056D88
	public void StartHealthHp(long updateValue, RoleBase updateRoleBase)
	{
		if (updateValue <= 0L)
		{
			return;
		}
		if (updateRoleBase.hasAuthority && (this.roleType == RoleType.Player || this.roleType == RoleType.King))
		{
			if (updateValue > 0L)
			{
				PlayerBase playerBase = updateRoleBase as PlayerBase;
				if (playerBase != null && playerBase.hpAddUpgrade > 0f)
				{
					updateValue += ConstDefine.ClampBattleValue((double)((float)updateValue * playerBase.hpAddUpgrade));
				}
			}
			RoleBase.HealthHp healthHp = updateRoleBase.healthHpEvent;
			if (healthHp != null)
			{
				healthHp(updateValue);
			}
		}
		this.CmdUpdateHp(updateValue, updateRoleBase.netId, -1);
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x00058C04 File Offset: 0x00056E04
	public void StartHealthHp(double updateValueF, RoleBase updateRoleBase)
	{
		if (updateValueF <= 0.0)
		{
			return;
		}
		long updateValue = ConstDefine.ClampBattleValue(updateValueF);
		this.StartHealthHp(updateValue, updateRoleBase);
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x00058C30 File Offset: 0x00056E30
	public void StartUpdateHealthHp(double updateValueF, RoleBase updateRoleBase)
	{
		if (double.IsNaN(updateValueF))
		{
			return;
		}
		if (updateValueF >= 1E+18)
		{
			updateValueF = 1E+18;
		}
		if (updateValueF <= -1E+18)
		{
			updateValueF = -1E+18;
		}
		this.CmdUpdateHp((long)Math.Round(updateValueF), updateRoleBase.netId, -1);
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00058C88 File Offset: 0x00056E88
	[Command]
	public void CmdUpdateHp(long updateValue, uint updateNetId, int attackRoleId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteLong(updateValue);
		writer.WriteUInt(updateNetId);
		writer.WriteInt(attackRoleId);
		base.SendCommandInternal(typeof(RoleBase), "CmdUpdateHp", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x00058CDC File Offset: 0x00056EDC
	private void CheckServerEnemyDead(EnemyBase enemyBase)
	{
		if (GameHelperClient.isReady)
		{
			return;
		}
		this.TargetRpcEnemyDead(enemyBase.enemyType, enemyBase.netId);
		if (!enemyBase.IsAutoDead)
		{
			enemyBase.ServerCheckEnemyDead(this);
		}
		AnalyticsManager analytics = Game.Analytics;
		if (analytics != null)
		{
			analytics.RecordServerEnemyKill(enemyBase.enemyType, GameHelperClient.WaveNum, enemyBase.isBoss, enemyBase.isElite);
		}
		if (this is PlayerBase)
		{
			PlayerBase playerBase = this as PlayerBase;
			playerBase.NetworkkillEnemyNum = playerBase.killEnemyNum + 1;
		}
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x00058D54 File Offset: 0x00056F54
	[TargetRpc]
	private void TargetRpcEnemyDead(EnemyType enemyType, uint deadId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_EnemyType(writer, enemyType);
		writer.WriteUInt(deadId);
		this.SendTargetRPCInternal(null, typeof(RoleBase), "TargetRpcEnemyDead", writer, 0);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x00058DA0 File Offset: 0x00056FA0
	[TargetRpc]
	private void TargetRpcKillPlayer(uint deadId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(deadId);
		this.SendTargetRPCInternal(null, typeof(RoleBase), "TargetRpcKillPlayer", writer, 0);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x00058DE0 File Offset: 0x00056FE0
	[Server]
	public bool ServerUpdateHp(long updateValue)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Boolean RoleBase::ServerUpdateHp(System.Int64)' called when server was not active");
			return default(bool);
		}
		if (this.hp <= 0L && this.roleState == RoleState.Dead)
		{
			return false;
		}
		this.Networkhp = this.hp + updateValue;
		if (this.hp > this.maxHp)
		{
			this.Networkhp = this.maxHp;
		}
		else if (this.hp <= 0L)
		{
			this.Networkhp = 0L;
			this.timer = 0f;
			this.ServerUpdateState(RoleState.Dead);
			return true;
		}
		return false;
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x00058E74 File Offset: 0x00057074
	[Server]
	public void ServerUpdateState(RoleState value)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void RoleBase::ServerUpdateState(RoleState)' called when server was not active");
			return;
		}
		this.NetworksyncSequenceId = 0;
		if (this.roleState == value)
		{
			return;
		}
		if (base.connectionToClient != null)
		{
			this.TargetRpcUpdateRoleState(this.roleState, value);
		}
		this.NetworkroleState = value;
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x00058EC4 File Offset: 0x000570C4
	[Server]
	public void ServerUpdateStateNoRpc(RoleState value)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void RoleBase::ServerUpdateStateNoRpc(RoleState)' called when server was not active");
			return;
		}
		this.NetworksyncSequenceId = 0;
		if (this.roleState == value)
		{
			return;
		}
		if (base.connectionToClient != null && base.connectionToClient != GameHelperClient.localPlayer.connectionToClient)
		{
			this.TargetRpcUpdateRoleState(this.roleState, value);
		}
		this.NetworkroleState = value;
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x00058F28 File Offset: 0x00057128
	[TargetRpc]
	private void TargetRpcUpdateRoleState(RoleState exitState, RoleState value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_RoleState(writer, exitState);
		Mirror.GeneratedNetworkCode._Write_RoleState(writer, value);
		this.SendTargetRPCInternal(null, typeof(RoleBase), "TargetRpcUpdateRoleState", writer, 0);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x00058F74 File Offset: 0x00057174
	public void TrackRotation(float speedOffset = 1f)
	{
		if (this.trackRoleBase == null)
		{
			return;
		}
		float v2Angle = this.GetV2Angle(this.trackRoleBase.MyTransform.position);
		this.oldRotation = this.myTransform.localEulerAngles.y;
		this.PingHuaZhuanShen(v2Angle, speedOffset);
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x00058FC5 File Offset: 0x000571C5
	public float GetMoveAngle(Vector2 movePos)
	{
		return Mathf.Atan2(movePos.x, movePos.y) * 57.29578f;
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x00058FE0 File Offset: 0x000571E0
	public void PingHuaZhuanShen(float rotation, float speedOffset = 1f)
	{
		float deltaTime = Time.deltaTime;
		if (rotation < 0f)
		{
			rotation += 360f;
		}
		float num = deltaTime * 180f * speedOffset;
		float num2 = rotation - this.oldRotation;
		if (Mathf.Abs(num2) > num)
		{
			if (num2 > 0f)
			{
				if (num2 > 180f)
				{
					rotation = this.oldRotation - num;
				}
				else
				{
					rotation = this.oldRotation + num;
				}
			}
			else if (num2 < -180f)
			{
				rotation = this.oldRotation + num;
			}
			else
			{
				rotation = this.oldRotation - num;
			}
		}
		this.SetRotationY(rotation);
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x0005906C File Offset: 0x0005726C
	public void MyTranslate(float movePos)
	{
		Vector3 forward = this.myTransform.forward;
		forward.x *= movePos;
		forward.z *= movePos;
		this.myTransform.position += forward;
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x000590B4 File Offset: 0x000572B4
	public void SetRotationY(float rotationY)
	{
		this.myTransform.localEulerAngles = new Vector3(0f, rotationY, 0f);
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x000590D1 File Offset: 0x000572D1
	public void SetSyncRotationY(float rotationY)
	{
		this.myTransform.localEulerAngles = new Vector3(0f, rotationY, 0f);
		this.CmdSyncEulerY(rotationY);
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x000590F8 File Offset: 0x000572F8
	public float GetV2Angle(Vector3 trackPos)
	{
		Vector3 position = this.myTransform.position;
		Vector2 vector = new Vector2(trackPos.x - position.x, trackPos.z - position.z);
		return Mathf.Atan2(vector.x, vector.y) * 57.29578f;
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x0005914C File Offset: 0x0005734C
	[Command]
	public void CmdRelifeByState(RoleState newState)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_RoleState(writer, newState);
		base.SendCommandInternal(typeof(RoleBase), "CmdRelifeByState", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x0005918B File Offset: 0x0005738B
	[Server]
	public void ServerRelifeByState(RoleState newState)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void RoleBase::ServerRelifeByState(RoleState)' called when server was not active");
			return;
		}
		this.ServerUpdateState(newState);
		this.Networkhp = this.maxHp;
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x000591B8 File Offset: 0x000573B8
	[Command]
	public void CmdRelife()
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		base.SendCommandInternal(typeof(RoleBase), "CmdRelife", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x000591ED File Offset: 0x000573ED
	[Server]
	public void ServerRelife()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void RoleBase::ServerRelife()' called when server was not active");
			return;
		}
		this.ServerUpdateState(RoleState.Idle);
		this.Networkhp = this.maxHp;
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x00059218 File Offset: 0x00057418
	[Command]
	public void CmdRelifeByHp(long hpValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteLong(hpValue);
		base.SendCommandInternal(typeof(RoleBase), "CmdRelifeByHp", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x00059257 File Offset: 0x00057457
	[Server]
	protected void ServerRelifeByHp(long hpValue)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void RoleBase::ServerRelifeByHp(System.Int64)' called when server was not active");
			return;
		}
		this.ServerUpdateState(RoleState.Idle);
		this.Networkhp = Math.Max(hpValue, this.hp);
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x00059288 File Offset: 0x00057488
	[Command]
	public void CmdCreateSkill(ActiveSkillEnum activeSkillType, Vector3 pos, float attackRotation, int targetRoleId, int skillBookId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_ActiveSkillEnum(writer, activeSkillType);
		writer.WriteVector3(pos);
		writer.WriteFloat(attackRotation);
		writer.WriteInt(targetRoleId);
		writer.WriteInt(skillBookId);
		base.SendCommandInternal(typeof(RoleBase), "CmdCreateSkill", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x000592F0 File Offset: 0x000574F0
	[ClientRpc]
	private void RpcCreateSkill(uint skillId, ActiveSkillEnum activeSkillType, Vector3 pos, float attackRotation, int targetRoleId, int skillBookId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		Mirror.GeneratedNetworkCode._Write_ActiveSkillEnum(writer, activeSkillType);
		writer.WriteVector3(pos);
		writer.WriteFloat(attackRotation);
		writer.WriteInt(targetRoleId);
		writer.WriteInt(skillBookId);
		this.SendRPCInternal(typeof(RoleBase), "RpcCreateSkill", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x00059364 File Offset: 0x00057564
	[Command]
	public void CmdCreateSkillBySyncData(ActiveSkillEnum activeSkillType, Vector3 pos, int syncData, float attackRotation, int targetRoleId, int skillBookId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_ActiveSkillEnum(writer, activeSkillType);
		writer.WriteVector3(pos);
		writer.WriteInt(syncData);
		writer.WriteFloat(attackRotation);
		writer.WriteInt(targetRoleId);
		writer.WriteInt(skillBookId);
		base.SendCommandInternal(typeof(RoleBase), "CmdCreateSkillBySyncData", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x000593D8 File Offset: 0x000575D8
	[ClientRpc]
	private void RpcCreateSkillBySyncData(uint skillId, ActiveSkillEnum activeSkillType, Vector3 pos, int syncData, float attackRotation, int targetRoleId, int skillBookId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		Mirror.GeneratedNetworkCode._Write_ActiveSkillEnum(writer, activeSkillType);
		writer.WriteVector3(pos);
		writer.WriteInt(syncData);
		writer.WriteFloat(attackRotation);
		writer.WriteInt(targetRoleId);
		writer.WriteInt(skillBookId);
		this.SendRPCInternal(typeof(RoleBase), "RpcCreateSkillBySyncData", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x00059454 File Offset: 0x00057654
	public void StartSummon(EnemyType enemyType, Vector3 pos, uint playerId, float newAttackSpeed, long newHp, int newAttackPower, float summonDeadTimeValue, EnemyEntriesType[] enemyEntriesTypesValue = null, long shieldValue = 0L, long curHp = 0L, int skillBookIdValue = -1)
	{
		if (this.IsDead())
		{
			return;
		}
		PlayerBase playerBase = this as PlayerBase;
		if (playerBase != null)
		{
			newHp = ConstDefine.ClampBattleValue((double)newHp * (1.0 + (double)playerBase.addCallMonsterHp));
			newAttackPower = ConstDefine.ClampIntValue((double)newAttackPower * (1.0 + (double)playerBase.addCallMonsterAttack) * (1.0 + (double)playerBase.addDamagePercent));
			summonDeadTimeValue *= 1f + playerBase.addCallMonsterTime;
		}
		this.CmdSummon(enemyType, pos, playerId, newAttackSpeed, newHp, newAttackPower, summonDeadTimeValue, enemyEntriesTypesValue, shieldValue, curHp, skillBookIdValue);
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x000594E8 File Offset: 0x000576E8
	[Command]
	public void CmdSummon(EnemyType enemyType, Vector3 pos, uint playerId, float newAttackSpeed, long newHp, int newAttackPower, float summonDeadTimeValue, EnemyEntriesType[] enemyEntriesTypesValue, long shieldValue, long curHp, int skillBookIdValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_EnemyType(writer, enemyType);
		writer.WriteVector3(pos);
		writer.WriteUInt(playerId);
		writer.WriteFloat(newAttackSpeed);
		writer.WriteLong(newHp);
		writer.WriteInt(newAttackPower);
		writer.WriteFloat(summonDeadTimeValue);
		Mirror.GeneratedNetworkCode._Write_EnemyEntriesType[](writer, enemyEntriesTypesValue);
		writer.WriteLong(shieldValue);
		writer.WriteLong(curHp);
		writer.WriteInt(skillBookIdValue);
		base.SendCommandInternal(typeof(RoleBase), "CmdSummon", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x0005958C File Offset: 0x0005778C
	public void StartSummonByNum(EnemyType enemyType, Vector3 pos, uint playerId, int num, float newAttackSpeed, long newHp, int newAttackPower, float summonDeadTimeValue, EnemyEntriesType[] enemyEntriesTypesValue = null, long shieldValue = 0L, long curHp = 0L, int skillBookIdValue = -1)
	{
		if (this.IsDead())
		{
			return;
		}
		PlayerBase playerBase = this as PlayerBase;
		if (playerBase != null)
		{
			newHp = ConstDefine.ClampBattleValue((double)newHp * (1.0 + (double)playerBase.addCallMonsterHp));
			newAttackPower = ConstDefine.ClampIntValue((double)newAttackPower * (1.0 + (double)playerBase.addCallMonsterAttack) * (1.0 + (double)playerBase.addDamagePercent));
			summonDeadTimeValue *= 1f + playerBase.addCallMonsterTime;
		}
		this.CmdSummonByNum(enemyType, pos, playerId, num, newAttackSpeed, newHp, newAttackPower, summonDeadTimeValue, enemyEntriesTypesValue, shieldValue, curHp, skillBookIdValue);
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x00059624 File Offset: 0x00057824
	[Command]
	public void CmdSummonByNum(EnemyType enemyType, Vector3 pos, uint playerId, int num, float newAttackSpeed, long newHp, int newAttackPower, float summonDeadTimeValue, EnemyEntriesType[] enemyEntriesTypesValue, long shieldValue, long curHp, int skillBookIdValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_EnemyType(writer, enemyType);
		writer.WriteVector3(pos);
		writer.WriteUInt(playerId);
		writer.WriteInt(num);
		writer.WriteFloat(newAttackSpeed);
		writer.WriteLong(newHp);
		writer.WriteInt(newAttackPower);
		writer.WriteFloat(summonDeadTimeValue);
		Mirror.GeneratedNetworkCode._Write_EnemyEntriesType[](writer, enemyEntriesTypesValue);
		writer.WriteLong(shieldValue);
		writer.WriteLong(curHp);
		writer.WriteInt(skillBookIdValue);
		base.SendCommandInternal(typeof(RoleBase), "CmdSummonByNum", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x000596D4 File Offset: 0x000578D4
	private Task AddSummon(EnemyType enemyType, Vector3 pos, uint playerId, float newAttackSpeed, long newHp, int newAttackPower, float summonDeadTimeValue, EnemyEntriesType[] enemyEntriesTypesValue, long shieldValue, long curHp, int fatherIdValue, RoleType fatherTypeValue, int skillBookIdValue)
	{
		RoleBase.<AddSummon>d__320 <AddSummon>d__;
		<AddSummon>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<AddSummon>d__.<>4__this = this;
		<AddSummon>d__.enemyType = enemyType;
		<AddSummon>d__.pos = pos;
		<AddSummon>d__.playerId = playerId;
		<AddSummon>d__.newAttackSpeed = newAttackSpeed;
		<AddSummon>d__.newHp = newHp;
		<AddSummon>d__.newAttackPower = newAttackPower;
		<AddSummon>d__.summonDeadTimeValue = summonDeadTimeValue;
		<AddSummon>d__.enemyEntriesTypesValue = enemyEntriesTypesValue;
		<AddSummon>d__.shieldValue = shieldValue;
		<AddSummon>d__.curHp = curHp;
		<AddSummon>d__.fatherIdValue = fatherIdValue;
		<AddSummon>d__.fatherTypeValue = fatherTypeValue;
		<AddSummon>d__.skillBookIdValue = skillBookIdValue;
		<AddSummon>d__.<>1__state = -1;
		<AddSummon>d__.<>t__builder.Start<RoleBase.<AddSummon>d__320>(ref <AddSummon>d__);
		return <AddSummon>d__.<>t__builder.Task;
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x0005978C File Offset: 0x0005798C
	public void OnLocalStartAttack()
	{
		float num = this.roleModeBase.attackOffset / this.GetRealAttackOffset();
		if (!Mathf.Approximately(this.syncAttackSpeed, num))
		{
			this.NetworksyncAttackSpeed = num;
			this.CmdUpdateAttackSpeed(num);
		}
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x000597C8 File Offset: 0x000579C8
	[Command]
	private void CmdUpdateAttackSpeed(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		base.SendCommandInternal(typeof(RoleBase), "CmdUpdateAttackSpeed", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x00059808 File Offset: 0x00057A08
	[Command]
	public void CmdAttackNum(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		base.SendCommandInternal(typeof(RoleBase), "CmdAttackNum", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x00059848 File Offset: 0x00057A48
	[ClientRpc]
	private void RpcAttackNum(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		this.SendRPCInternal(typeof(RoleBase), "RpcAttackNum", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x00059888 File Offset: 0x00057A88
	[Command]
	public void CmdTeleportBlink(Vector3 pos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		base.SendCommandInternal(typeof(RoleBase), "CmdTeleportBlink", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x000598C7 File Offset: 0x00057AC7
	[Server]
	public void ServerTeleportBlink(Vector3 pos)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void RoleBase::ServerTeleportBlink(UnityEngine.Vector3)' called when server was not active");
			return;
		}
		this.NetworksyncPos = pos;
		this.ClientTeleportBlink(pos);
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x000598EC File Offset: 0x00057AEC
	[ClientRpc]
	private void ClientTeleportBlink(Vector3 pos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		this.SendRPCInternal(typeof(RoleBase), "ClientTeleportBlink", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x0005992C File Offset: 0x00057B2C
	[Command]
	public void CmdDoge(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		base.SendCommandInternal(typeof(RoleBase), "CmdDoge", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x0005996C File Offset: 0x00057B6C
	[ClientRpc]
	public void RpcDoge(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		this.SendRPCInternal(typeof(RoleBase), "RpcDoge", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x000599AB File Offset: 0x00057BAB
	public float GetRealAttackOffset()
	{
		return Mathf.Max(this.roleModeBase.attackOffset / this.GetAttackSpeed(), 0.135f);
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x000599C9 File Offset: 0x00057BC9
	public float GetRealOffsetInAttack()
	{
		return Mathf.Max(this.roleModeBase.attackOffset / this.aniSpeed, 0.135f);
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x000599E7 File Offset: 0x00057BE7
	public void StartOverrideAnim()
	{
		this.isOverrideAnim = true;
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x000599F0 File Offset: 0x00057BF0
	protected void ChangeMaterial(int index)
	{
		if (this.curMaterialIndex == index)
		{
			return;
		}
		this.curMaterialIndex = index;
		Material material = Resources.Load(PathDefine.Concat("Bundles/Material/", this.roleModeBase.materialList[index])) as Material;
		int count = this.roleModeBase.myRenderers.Count;
		for (int i = 0; i < count; i++)
		{
			this.roleModeBase.myRenderers[i].material = material;
			material.SetColor(ShaderDefine.Property_EmitColor, GameHelperClient.gameConfig.HitColor);
		}
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x00059A7E File Offset: 0x00057C7E
	public bool IsDead()
	{
		return this.roleState == RoleState.Dead;
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x00059A8C File Offset: 0x00057C8C
	public void AddShield(long shieldMax)
	{
		if (!base.hasAuthority)
		{
			return;
		}
		this.CmdUpdateShield(shieldMax);
		if (this.shieldEffect == null)
		{
			this.CmdShieldEffect(true);
		}
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x00059AB4 File Offset: 0x00057CB4
	[Command]
	public void CmdUpdateShield(long updateValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteLong(updateValue);
		base.SendCommandInternal(typeof(RoleBase), "CmdUpdateShield", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x00059AF3 File Offset: 0x00057CF3
	[Server]
	public void ServerUpdateShield(long updateValue)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void RoleBase::ServerUpdateShield(System.Int64)' called when server was not active");
			return;
		}
		this.Networkshield = this.shield + updateValue;
		if (this.shield < 0L)
		{
			this.Networkshield = 0L;
		}
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x00059B2A File Offset: 0x00057D2A
	[Server]
	public void ServerSetShield(long setValue)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void RoleBase::ServerSetShield(System.Int64)' called when server was not active");
			return;
		}
		this.Networkshield = setValue;
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x00059B48 File Offset: 0x00057D48
	[Command]
	private void CmdShieldEffect(bool isAdd)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteBool(isAdd);
		base.SendCommandInternal(typeof(RoleBase), "CmdShieldEffect", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x00059B88 File Offset: 0x00057D88
	[ClientRpc]
	private void RpcShieldEffect(bool isAdd)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteBool(isAdd);
		this.SendRPCInternal(typeof(RoleBase), "RpcShieldEffect", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x00059BC7 File Offset: 0x00057DC7
	public void ClearShield(long value)
	{
		if (!base.hasAuthority)
		{
			return;
		}
		if (this.shieldEffect != null && value >= this.shield)
		{
			this.CmdShieldEffect(false);
		}
		if (this.shield > 0L)
		{
			this.CmdUpdateShield(-value);
		}
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x0001562E File Offset: 0x0001382E
	public virtual bool IsShowName()
	{
		return false;
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x00059C02 File Offset: 0x00057E02
	public void HideMode()
	{
		if (this.animTransform.gameObject.activeSelf)
		{
			this.animTransform.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x00059C28 File Offset: 0x00057E28
	public void ShowMode()
	{
		if (!this.animTransform.gameObject.activeSelf)
		{
			this.animTransform.gameObject.SetActive(true);
			this.resetAnim = true;
			return;
		}
		if (this.resetAnim)
		{
			int nameHash = this.aniName;
			this.aniName = -1;
			if (this.roleState == RoleState.Idle)
			{
				this.PlayAni(AnimDefine.Idle, 1f, 0.1f);
			}
			else if (this.roleState == RoleState.Run)
			{
				this.PlayAni(AnimDefine.Run, 1f, 0.1f);
			}
			else if (this.roleState == RoleState.Dead)
			{
				this.PlayAni(AnimDefine.Dead, 1f, 0.1f);
			}
			else
			{
				this.PlayAni(nameHash, this.aniSpeed, 0.1f);
			}
			this.resetAnim = false;
		}
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x00059CF4 File Offset: 0x00057EF4
	public virtual void InitRoleModeBase(RoleModeBase roleModeBaseValue)
	{
		this.roleModeBase = roleModeBaseValue;
		this.animTransform = this.roleModeBase.transform;
		this.animTransform.SetParent(this.myTransform);
		this.animTransform.localPosition = Vector3.zero;
		this.animTransform.localRotation = Quaternion.identity;
		roleModeBaseValue.baseModeScale = this.animTransform.localScale;
		int count = this.roleModeBase.myRenderers.Count;
		for (int i = 0; i < count; i++)
		{
			this.roleModeBase.myRenderers[i].material.SetColor(ShaderDefine.Property_EmitColor, GameHelperClient.gameConfig.HitColor);
		}
		this.roleModeBase.roleBase = this;
		base.gameObject.name = PathDefine.Concat("Temp_", this.animTransform.gameObject.name);
		AssetManagerMirror.CreatePool(base.gameObject.name);
		this.roleModeBase.OnInitMode();
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x00059DF4 File Offset: 0x00057FF4
	private void UpdateLocalBuff()
	{
		float deltaTime = Time.deltaTime;
		int count = this.localRoleBuffDic.Count;
		if (count == 0)
		{
			return;
		}
		for (int i = count - 1; i > -1; i--)
		{
			KeyValuePair<LocalBuffType, RoleBuffBase> keyValuePair = this.localRoleBuffDic.ElementAt(i);
			RoleBuffBase value = keyValuePair.Value;
			value.UpdateBuff();
			value.buffTime -= deltaTime;
			if (value.buffTime <= 0f)
			{
				value.ClearBuff();
				this.localRoleBuffDic.Remove(keyValuePair.Key);
			}
		}
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x00059E79 File Offset: 0x00058079
	public void AddLocalBuff(LocalBuffType localBuffType, RoleBuffBase roleBuffBase)
	{
		roleBuffBase.roleBase = this;
		roleBuffBase.localBuffType = localBuffType;
		roleBuffBase.InitBuff();
		this.localRoleBuffDic.Add(localBuffType, roleBuffBase);
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x00059E9C File Offset: 0x0005809C
	public void ClearAllBuff(bool isDead)
	{
		int count = this.localRoleBuffDic.Count;
		if (count == 0)
		{
			return;
		}
		for (int i = count - 1; i > -1; i--)
		{
			KeyValuePair<LocalBuffType, RoleBuffBase> keyValuePair = this.localRoleBuffDic.ElementAt(i);
			RoleBuffBase value = keyValuePair.Value;
			if (isDead)
			{
				if (!value.deadNoClear)
				{
					value.ClearBuff();
					this.localRoleBuffDic.Remove(keyValuePair.Key);
				}
			}
			else
			{
				value.ClearBuff();
			}
		}
		if (!isDead)
		{
			this.localRoleBuffDic.Clear();
		}
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x00059F18 File Offset: 0x00058118
	[ClientRpc]
	public void RpcRemoveuff(LocalBuffType localBuffType)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_LocalBuffType(writer, localBuffType);
		this.SendRPCInternal(typeof(RoleBase), "RpcRemoveuff", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x00059F58 File Offset: 0x00058158
	[ClientRpc]
	public void RpcAddBuff(uint attackNetId, LocalBuffType localBuffType, float buffValue, float buffTime, int level)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(attackNetId);
		Mirror.GeneratedNetworkCode._Write_LocalBuffType(writer, localBuffType);
		writer.WriteFloat(buffValue);
		writer.WriteFloat(buffTime);
		writer.WriteInt(level);
		this.SendRPCInternal(typeof(RoleBase), "RpcAddBuff", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x00059FBF File Offset: 0x000581BF
	public void XuanYun(float time)
	{
		Util.CmdXuanYun(this, time);
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x00059FC8 File Offset: 0x000581C8
	[TargetRpc]
	public void TargetRpcUpdateXuanYun(float xuanyunTime)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(xuanyunTime);
		this.SendTargetRPCInternal(null, typeof(RoleBase), "TargetRpcUpdateXuanYun", writer, 0);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x0001562E File Offset: 0x0001382E
	public virtual bool GetIsAttackWeek(AttackType attackType)
	{
		return false;
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x0005A008 File Offset: 0x00058208
	[Command]
	public void CmdUpdateOtherAttackSpeed(float updateValue, uint updateNetId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(updateValue);
		writer.WriteUInt(updateNetId);
		base.SendCommandInternal(typeof(RoleBase), "CmdUpdateOtherAttackSpeed", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x0005A054 File Offset: 0x00058254
	[TargetRpc]
	private void RpcUpdateAttackSpeed(float updateValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(updateValue);
		this.SendTargetRPCInternal(null, typeof(RoleBase), "RpcUpdateAttackSpeed", writer, 0);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x0005A093 File Offset: 0x00058293
	public void OnRemove()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnRemove();
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x0005A0A5 File Offset: 0x000582A5
	public List<RoleBase> GetAttackRoles()
	{
		if (this.roleType == RoleType.Enemy || this.roleType == RoleType.King)
		{
			return Game.PlayerManagerClient.clientPlayerList;
		}
		return Game.EnemyManagerClient.clientEnemyList;
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x0005A0CE File Offset: 0x000582CE
	public List<RoleBase> GetFriendRoles()
	{
		if (this.roleType == RoleType.Enemy || this.roleType == RoleType.King)
		{
			return Game.EnemyManagerClient.clientEnemyList;
		}
		return Game.PlayerManagerClient.clientPlayerList;
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x0005A0F8 File Offset: 0x000582F8
	[Command]
	public void CmdUpdateModeData(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		base.SendCommandInternal(typeof(RoleBase), "CmdUpdateModeData", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x0005A138 File Offset: 0x00058338
	[ClientRpc]
	private void RpcUpdateModeData(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		this.SendRPCInternal(typeof(RoleBase), "RpcUpdateModeData", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void MirrorProcessed()
	{
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0005A21C File Offset: 0x0005841C
	// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x0005A230 File Offset: 0x00058430
	public float NetworksyncSkillData
	{
		get
		{
			return this.syncSkillData;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<float>(value, ref this.syncSkillData))
			{
				float num = this.syncSkillData;
				base.SetSyncVar<float>(value, ref this.syncSkillData, 1UL);
			}
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0005A270 File Offset: 0x00058470
	// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0005A284 File Offset: 0x00058484
	public long Networkshield
	{
		get
		{
			return this.shield;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<long>(value, ref this.shield))
			{
				long num = this.shield;
				base.SetSyncVar<long>(value, ref this.shield, 2UL);
			}
		}
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0005A2C4 File Offset: 0x000584C4
	// (set) Token: 0x06000FCC RID: 4044 RVA: 0x0005A2D8 File Offset: 0x000584D8
	public float NetworksyncAttackSpeed
	{
		get
		{
			return this.syncAttackSpeed;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<float>(value, ref this.syncAttackSpeed))
			{
				float num = this.syncAttackSpeed;
				base.SetSyncVar<float>(value, ref this.syncAttackSpeed, 4UL);
			}
		}
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0005A318 File Offset: 0x00058518
	// (set) Token: 0x06000FCE RID: 4046 RVA: 0x0005A32C File Offset: 0x0005852C
	public Vector3 NetworksyncPos
	{
		get
		{
			return this.syncPos;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<Vector3>(value, ref this.syncPos))
			{
				Vector3 vector = this.syncPos;
				base.SetSyncVar<Vector3>(value, ref this.syncPos, 8UL);
			}
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0005A36C File Offset: 0x0005856C
	// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0005A380 File Offset: 0x00058580
	public float NetworksyncEulerY
	{
		get
		{
			return this.syncEulerY;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<float>(value, ref this.syncEulerY))
			{
				float num = this.syncEulerY;
				base.SetSyncVar<float>(value, ref this.syncEulerY, 16UL);
			}
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0005A3C0 File Offset: 0x000585C0
	// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x0005A3D4 File Offset: 0x000585D4
	public int NetworksyncSequenceId
	{
		get
		{
			return this.syncSequenceId;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<int>(value, ref this.syncSequenceId))
			{
				int num = this.syncSequenceId;
				base.SetSyncVar<int>(value, ref this.syncSequenceId, 32UL);
			}
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0005A414 File Offset: 0x00058614
	// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x0005A428 File Offset: 0x00058628
	public RoleState NetworkroleState
	{
		get
		{
			return this.roleState;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<RoleState>(value, ref this.roleState))
			{
				RoleState _ = this.roleState;
				base.SetSyncVar<RoleState>(value, ref this.roleState, 64UL);
				if (NetworkServer.localClientActive && !base.GetSyncVarHookGuard(64UL))
				{
					base.SetSyncVarHookGuard(64UL, true);
					this.OnChangeRoleState(_, value);
					base.SetSyncVarHookGuard(64UL, false);
				}
			}
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0005A4B4 File Offset: 0x000586B4
	// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x0005A4C8 File Offset: 0x000586C8
	public long NetworkmaxHp
	{
		get
		{
			return this.maxHp;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<long>(value, ref this.maxHp))
			{
				long num = this.maxHp;
				base.SetSyncVar<long>(value, ref this.maxHp, 128UL);
			}
		}
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0005A508 File Offset: 0x00058708
	// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x0005A51C File Offset: 0x0005871C
	public long Networkhp
	{
		get
		{
			return this.hp;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<long>(value, ref this.hp))
			{
				long num = this.hp;
				base.SetSyncVar<long>(value, ref this.hp, 256UL);
			}
		}
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x000566F9 File Offset: 0x000548F9
	protected void UserCode_CmdSyncSkillData(float value)
	{
		this.NetworksyncSkillData = value;
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x0005A55B File Offset: 0x0005875B
	protected static void InvokeUserCode_CmdSyncSkillData(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdSyncSkillData called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdSyncSkillData(reader.ReadFloat());
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x0005A585 File Offset: 0x00058785
	protected void UserCode_CmdUpateMArmor(int value)
	{
		this.RpcUpateMArmor(value);
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x0005A58E File Offset: 0x0005878E
	protected static void InvokeUserCode_CmdUpateMArmor(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpateMArmor called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdUpateMArmor(reader.ReadInt());
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x0005A5B7 File Offset: 0x000587B7
	protected void UserCode_RpcUpateMArmor(int value)
	{
		this.mArmor = value;
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x0005A5C0 File Offset: 0x000587C0
	protected static void InvokeUserCode_RpcUpateMArmor(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpateMArmor called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcUpateMArmor(reader.ReadInt());
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x0005A5E9 File Offset: 0x000587E9
	protected void UserCode_CmdSyncPos(Vector3 value)
	{
		this.NetworksyncPos = value;
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x0005A5F2 File Offset: 0x000587F2
	protected static void InvokeUserCode_CmdSyncPos(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdSyncPos called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdSyncPos(reader.ReadVector3());
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x0005A61B File Offset: 0x0005881B
	protected void UserCode_CmdSyncEulerY(float value)
	{
		this.NetworksyncEulerY = value;
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x0005A624 File Offset: 0x00058824
	protected static void InvokeUserCode_CmdSyncEulerY(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdSyncEulerY called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdSyncEulerY(reader.ReadFloat());
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x0005A64E File Offset: 0x0005884E
	protected void UserCode_CmdHp(int value)
	{
		this.Networkhp = (long)value;
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x0005A658 File Offset: 0x00058858
	protected static void InvokeUserCode_CmdHp(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdHp called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdHp(reader.ReadInt());
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x0005A681 File Offset: 0x00058881
	protected void UserCode_CmdWuDi(bool v)
	{
		this.RpcWuDi(v);
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0005A68A File Offset: 0x0005888A
	protected static void InvokeUserCode_CmdWuDi(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdWuDi called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdWuDi(reader.ReadBool());
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0005A6B3 File Offset: 0x000588B3
	protected void UserCode_RpcWuDi(bool v)
	{
		this.wudi = v;
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x0005A6BC File Offset: 0x000588BC
	protected static void InvokeUserCode_RpcWuDi(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcWuDi called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcWuDi(reader.ReadBool());
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0005A6E5 File Offset: 0x000588E5
	protected void UserCode_CmdUpdateRoleState(RoleState value, int sequenceIdValue)
	{
		if (this.hp <= 0L || this.roleState == RoleState.Dead)
		{
			this.NetworksyncSequenceId = 0;
			return;
		}
		this.NetworksyncSequenceId = sequenceIdValue;
		this.NetworkroleState = value;
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0005A710 File Offset: 0x00058910
	protected static void InvokeUserCode_CmdUpdateRoleState(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateRoleState called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdUpdateRoleState(Mirror.GeneratedNetworkCode._Read_RoleState(reader), reader.ReadInt());
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x0005A740 File Offset: 0x00058940
	protected void UserCode_CmdUpdateMaxHp(long updateValue, uint updateNetId)
	{
		NetworkIdentity networkIdentity;
		if (NetworkServer.spawned.TryGetValue(updateNetId, out networkIdentity))
		{
			networkIdentity.GetComponent<RoleBase>().ServerUpdateMaxHp(updateValue);
		}
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x0005A768 File Offset: 0x00058968
	protected static void InvokeUserCode_CmdUpdateMaxHp(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateMaxHp called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdUpdateMaxHp(reader.ReadLong(), reader.ReadUInt());
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0005A798 File Offset: 0x00058998
	protected void UserCode_CmdUpdateHp(long updateValue, uint updateNetId, int attackRoleId)
	{
		NetworkIdentity networkIdentity;
		if (NetworkServer.spawned.TryGetValue(updateNetId, out networkIdentity))
		{
			RoleBase component = networkIdentity.GetComponent<RoleBase>();
			if (component.shield > 0L && updateValue < 0L)
			{
				if (component.shield + updateValue > 0L)
				{
					RoleBase roleBase = component;
					roleBase.Networkshield = roleBase.shield + updateValue;
					return;
				}
				updateValue += component.shield;
				component.Networkshield = 0L;
				if (component.shieldEffect != null)
				{
					component.RpcShieldEffect(false);
				}
			}
			if (component.ServerUpdateHp(updateValue))
			{
				if (component.roleType == RoleType.Enemy)
				{
					this.CheckServerEnemyDead(component as EnemyBase);
					return;
				}
				if (component.roleType == RoleType.King)
				{
					this.TargetRpcKillPlayer(component.netId);
					return;
				}
				NetworkIdentity networkIdentity2;
				if ((component.roleType == RoleType.Player || component.roleType == RoleType.Summon) && attackRoleId != -1 && NetworkServer.spawned.TryGetValue((uint)attackRoleId, out networkIdentity2))
				{
					RoleBase component2 = networkIdentity2.GetComponent<RoleBase>();
					if (component2.roleType == RoleType.King)
					{
						PlayerBase playerBase = component2 as PlayerBase;
						if (playerBase != null)
						{
							playerBase.TargetRpcKillPlayer(component.netId);
							return;
						}
					}
					EnemyBase enemyBase = component2 as EnemyBase;
					NetworkIdentity networkIdentity3;
					if (enemyBase != null && enemyBase.FatherId != -1 && enemyBase.FatherType == RoleType.King && NetworkServer.spawned.TryGetValue((uint)enemyBase.FatherId, out networkIdentity3))
					{
						RoleBase component3 = networkIdentity3.GetComponent<RoleBase>();
						if (component3.roleType == RoleType.King)
						{
							PlayerBase playerBase2 = component3 as PlayerBase;
							if (playerBase2 != null)
							{
								playerBase2.TargetRpcKillPlayer(component.netId);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x0005A900 File Offset: 0x00058B00
	protected static void InvokeUserCode_CmdUpdateHp(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateHp called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdUpdateHp(reader.ReadLong(), reader.ReadUInt(), reader.ReadInt());
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x0005A938 File Offset: 0x00058B38
	protected void UserCode_TargetRpcEnemyDead(EnemyType enemyType, uint deadId)
	{
		NetworkIdentity networkIdentity;
		NetworkClient.spawned.TryGetValue(deadId, out networkIdentity);
		if (networkIdentity != null)
		{
			RoleBase component = networkIdentity.gameObject.GetComponent<RoleBase>();
			if (this.killEnemyEvent != null)
			{
				this.killEnemyEvent(this, component);
			}
			int num = 15;
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			EnemyBase enemyBase = component as EnemyBase;
			if (enemyBase != null)
			{
				if (enemyBase.enemyType >= EnemyType.Goblin_LocalTyrant_0 && enemyBase.enemyType <= EnemyType.Goblin_LocalTyrant_5)
				{
					flag = false;
					PlayerBase playerBase = this as PlayerBase;
					if (playerBase != null)
					{
						playerBase.AddGold(component.GetHeadUIPos(), 350, true);
					}
				}
				if (enemyBase.isBoss)
				{
					flag2 = true;
					int num2 = GameHelperClient.WaveNum + 1;
					UI_Msg ui = Game.UI.GetUI<UI_Msg>();
					if (ui != null)
					{
						ui.ShowMsg(Game.Language.Get("killbossstr", "") + num2.ToString(), false);
					}
					UI_Msg ui2 = Game.UI.GetUI<UI_Msg>();
					if (ui2 != null)
					{
						ui2.ShowMsg(Game.Language.Get("killbossdex", "") + num2.ToString(), false);
					}
					UI_Msg ui3 = Game.UI.GetUI<UI_Msg>();
					if (ui3 != null)
					{
						ui3.ShowMsg(Game.Language.Get("killbosssta", "") + num2.ToString(), false);
					}
					PlayerBase playerBase2 = this as PlayerBase;
					if (playerBase2 != null)
					{
						playerBase2.killBossNum++;
						playerBase2.AddSTA(num2);
						playerBase2.AddAGI(num2);
						playerBase2.AddSTR(num2);
						playerBase2.AddGem(component.GetHeadUIPos(), 1, false);
					}
					num *= 10 * num2;
					EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/击杀BOSS", 1f, 3f);
				}
				else if (enemyBase.isElite)
				{
					num *= 5;
					flag3 = true;
					PlayerBase playerBase3 = this as PlayerBase;
					if (playerBase3 != null)
					{
						playerBase3.AddGem(component.GetHeadUIPos(), 1, false);
					}
				}
			}
			if (flag)
			{
				int num3 = 20;
				if (flag2)
				{
					num3 = 1000;
				}
				else if (flag3)
				{
					num3 = 75;
				}
				PlayerBase playerBase4 = this as PlayerBase;
				if (playerBase4 != null)
				{
					playerBase4.AddGold(component.GetHeadUIPos(), (int)((float)num3 * (0.85f + Random.value * 0.3f)), true);
				}
			}
			PlayerBase playerBase5 = this as PlayerBase;
			if (playerBase5 == null)
			{
				return;
			}
			playerBase5.GainExp(num);
		}
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x0005AB7F File Offset: 0x00058D7F
	protected static void InvokeUserCode_TargetRpcEnemyDead(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("TargetRPC TargetRpcEnemyDead called on server.");
			return;
		}
		((RoleBase)obj).UserCode_TargetRpcEnemyDead(Mirror.GeneratedNetworkCode._Read_EnemyType(reader), reader.ReadUInt());
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x0005ABB0 File Offset: 0x00058DB0
	protected void UserCode_TargetRpcKillPlayer(uint deadId)
	{
		NetworkIdentity networkIdentity;
		NetworkClient.spawned.TryGetValue(deadId, out networkIdentity);
		if (networkIdentity != null)
		{
			RoleBase component = networkIdentity.gameObject.GetComponent<RoleBase>();
			if (this.killEnemyEvent != null)
			{
				this.killEnemyEvent(this, component);
			}
		}
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x0005ABF5 File Offset: 0x00058DF5
	protected static void InvokeUserCode_TargetRpcKillPlayer(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("TargetRPC TargetRpcKillPlayer called on server.");
			return;
		}
		((RoleBase)obj).UserCode_TargetRpcKillPlayer(reader.ReadUInt());
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x0005AC1E File Offset: 0x00058E1E
	protected void UserCode_TargetRpcUpdateRoleState(RoleState exitState, RoleState value)
	{
		this.ExitState(exitState);
		this.ClientUpdateState(value);
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x0005AC2E File Offset: 0x00058E2E
	protected static void InvokeUserCode_TargetRpcUpdateRoleState(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("TargetRPC TargetRpcUpdateRoleState called on server.");
			return;
		}
		((RoleBase)obj).UserCode_TargetRpcUpdateRoleState(Mirror.GeneratedNetworkCode._Read_RoleState(reader), Mirror.GeneratedNetworkCode._Read_RoleState(reader));
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x0005AC5D File Offset: 0x00058E5D
	protected void UserCode_CmdRelifeByState(RoleState newState)
	{
		this.ServerRelifeByState(newState);
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x0005AC66 File Offset: 0x00058E66
	protected static void InvokeUserCode_CmdRelifeByState(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdRelifeByState called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdRelifeByState(Mirror.GeneratedNetworkCode._Read_RoleState(reader));
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x0005AC8F File Offset: 0x00058E8F
	protected void UserCode_CmdRelife()
	{
		this.ServerRelife();
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x0005AC97 File Offset: 0x00058E97
	protected static void InvokeUserCode_CmdRelife(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdRelife called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdRelife();
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x0005ACBA File Offset: 0x00058EBA
	protected void UserCode_CmdRelifeByHp(long hpValue)
	{
		this.ServerRelifeByHp(hpValue);
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x0005ACC3 File Offset: 0x00058EC3
	protected static void InvokeUserCode_CmdRelifeByHp(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdRelifeByHp called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdRelifeByHp(reader.ReadLong());
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x0005ACEC File Offset: 0x00058EEC
	protected void UserCode_CmdCreateSkill(ActiveSkillEnum activeSkillType, Vector3 pos, float attackRotation, int targetRoleId, int skillBookId)
	{
		this.RpcCreateSkill(SkillManager.GetSkillId(), activeSkillType, pos, attackRotation, targetRoleId, skillBookId);
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x0005AD00 File Offset: 0x00058F00
	protected static void InvokeUserCode_CmdCreateSkill(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdCreateSkill called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdCreateSkill(Mirror.GeneratedNetworkCode._Read_ActiveSkillEnum(reader), reader.ReadVector3(), reader.ReadFloat(), reader.ReadInt(), reader.ReadInt());
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x0005AD4D File Offset: 0x00058F4D
	protected void UserCode_RpcCreateSkill(uint skillId, ActiveSkillEnum activeSkillType, Vector3 pos, float attackRotation, int targetRoleId, int skillBookId)
	{
		Util.CreateSkill(skillId, activeSkillType, pos, this, 0, attackRotation, targetRoleId, skillBookId);
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x0005AD60 File Offset: 0x00058F60
	protected static void InvokeUserCode_RpcCreateSkill(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcCreateSkill called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcCreateSkill(reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_ActiveSkillEnum(reader), reader.ReadVector3(), reader.ReadFloat(), reader.ReadInt(), reader.ReadInt());
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x0005ADB3 File Offset: 0x00058FB3
	protected void UserCode_CmdCreateSkillBySyncData(ActiveSkillEnum activeSkillType, Vector3 pos, int syncData, float attackRotation, int targetRoleId, int skillBookId)
	{
		this.RpcCreateSkillBySyncData(SkillManager.GetSkillId(), activeSkillType, pos, syncData, attackRotation, targetRoleId, skillBookId);
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x0005ADCC File Offset: 0x00058FCC
	protected static void InvokeUserCode_CmdCreateSkillBySyncData(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdCreateSkillBySyncData called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdCreateSkillBySyncData(Mirror.GeneratedNetworkCode._Read_ActiveSkillEnum(reader), reader.ReadVector3(), reader.ReadInt(), reader.ReadFloat(), reader.ReadInt(), reader.ReadInt());
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x0005AE1F File Offset: 0x0005901F
	protected void UserCode_RpcCreateSkillBySyncData(uint skillId, ActiveSkillEnum activeSkillType, Vector3 pos, int syncData, float attackRotation, int targetRoleId, int skillBookId)
	{
		Util.CreateSkill(skillId, activeSkillType, pos, this, syncData, attackRotation, targetRoleId, skillBookId);
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x0005AE34 File Offset: 0x00059034
	protected static void InvokeUserCode_RpcCreateSkillBySyncData(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcCreateSkillBySyncData called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcCreateSkillBySyncData(reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_ActiveSkillEnum(reader), reader.ReadVector3(), reader.ReadInt(), reader.ReadFloat(), reader.ReadInt(), reader.ReadInt());
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x0005AE90 File Offset: 0x00059090
	protected void UserCode_CmdSummon(EnemyType enemyType, Vector3 pos, uint playerId, float newAttackSpeed, long newHp, int newAttackPower, float summonDeadTimeValue, EnemyEntriesType[] enemyEntriesTypesValue, long shieldValue, long curHp, int skillBookIdValue)
	{
		this.AddSummon(enemyType, pos, playerId, newAttackSpeed, newHp, newAttackPower, summonDeadTimeValue, enemyEntriesTypesValue, shieldValue, curHp, (int)base.netId, this.roleType, skillBookIdValue);
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x0005AEC4 File Offset: 0x000590C4
	protected static void InvokeUserCode_CmdSummon(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdSummon called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdSummon(Mirror.GeneratedNetworkCode._Read_EnemyType(reader), reader.ReadVector3(), reader.ReadUInt(), reader.ReadFloat(), reader.ReadLong(), reader.ReadInt(), reader.ReadFloat(), Mirror.GeneratedNetworkCode._Read_EnemyEntriesType[](reader), reader.ReadLong(), reader.ReadLong(), reader.ReadInt());
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x0005AF38 File Offset: 0x00059138
	protected void UserCode_CmdSummonByNum(EnemyType enemyType, Vector3 pos, uint playerId, int num, float newAttackSpeed, long newHp, int newAttackPower, float summonDeadTimeValue, EnemyEntriesType[] enemyEntriesTypesValue, long shieldValue, long curHp, int skillBookIdValue)
	{
		for (int i = 0; i < num; i++)
		{
			Vector2 pointByRadian = Util.GetPointByRadian(Random.value * 2f, 0f, Random.value * 360f);
			this.AddSummon(enemyType, new Vector3(pos.x + pointByRadian.x, pos.y, pos.z + pointByRadian.y), playerId, newAttackSpeed, newHp, newAttackPower, summonDeadTimeValue, enemyEntriesTypesValue, shieldValue, curHp, (int)base.netId, this.roleType, skillBookIdValue);
		}
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x0005AFC0 File Offset: 0x000591C0
	protected static void InvokeUserCode_CmdSummonByNum(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdSummonByNum called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdSummonByNum(Mirror.GeneratedNetworkCode._Read_EnemyType(reader), reader.ReadVector3(), reader.ReadUInt(), reader.ReadInt(), reader.ReadFloat(), reader.ReadLong(), reader.ReadInt(), reader.ReadFloat(), Mirror.GeneratedNetworkCode._Read_EnemyEntriesType[](reader), reader.ReadLong(), reader.ReadLong(), reader.ReadInt());
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x0005B038 File Offset: 0x00059238
	protected void UserCode_CmdUpdateAttackSpeed(float value)
	{
		this.NetworksyncAttackSpeed = value;
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x0005B041 File Offset: 0x00059241
	protected static void InvokeUserCode_CmdUpdateAttackSpeed(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateAttackSpeed called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdUpdateAttackSpeed(reader.ReadFloat());
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x0005B06B File Offset: 0x0005926B
	protected void UserCode_CmdAttackNum(int value)
	{
		this.RpcAttackNum(value);
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x0005B074 File Offset: 0x00059274
	protected static void InvokeUserCode_CmdAttackNum(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdAttackNum called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdAttackNum(reader.ReadInt());
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x0005B09D File Offset: 0x0005929D
	protected void UserCode_RpcAttackNum(int value)
	{
		this.attackNum = value;
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x0005B0A6 File Offset: 0x000592A6
	protected static void InvokeUserCode_RpcAttackNum(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcAttackNum called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcAttackNum(reader.ReadInt());
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x0005B0CF File Offset: 0x000592CF
	protected void UserCode_CmdTeleportBlink(Vector3 pos)
	{
		if (this.hp <= 0L || this.roleState == RoleState.Dead)
		{
			return;
		}
		this.ServerTeleportBlink(pos);
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x0005B0EC File Offset: 0x000592EC
	protected static void InvokeUserCode_CmdTeleportBlink(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdTeleportBlink called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdTeleportBlink(reader.ReadVector3());
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0005B115 File Offset: 0x00059315
	protected void UserCode_ClientTeleportBlink(Vector3 pos)
	{
		this.myTransform.position = pos;
		this.NetworksyncPos = pos;
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x0005B12A File Offset: 0x0005932A
	protected static void InvokeUserCode_ClientTeleportBlink(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC ClientTeleportBlink called on server.");
			return;
		}
		((RoleBase)obj).UserCode_ClientTeleportBlink(reader.ReadVector3());
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x0005B153 File Offset: 0x00059353
	protected void UserCode_CmdDoge(int value)
	{
		this.RpcDoge(value);
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x0005B15C File Offset: 0x0005935C
	protected static void InvokeUserCode_CmdDoge(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdDoge called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdDoge(reader.ReadInt());
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x0005B185 File Offset: 0x00059385
	protected void UserCode_RpcDoge(int value)
	{
		this.doge = value;
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x0005B18E File Offset: 0x0005938E
	protected static void InvokeUserCode_RpcDoge(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcDoge called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcDoge(reader.ReadInt());
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x0005B1B7 File Offset: 0x000593B7
	protected void UserCode_CmdUpdateShield(long updateValue)
	{
		this.ServerUpdateShield(updateValue);
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x0005B1C0 File Offset: 0x000593C0
	protected static void InvokeUserCode_CmdUpdateShield(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateShield called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdUpdateShield(reader.ReadLong());
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x0005B1E9 File Offset: 0x000593E9
	protected void UserCode_CmdShieldEffect(bool isAdd)
	{
		this.RpcShieldEffect(isAdd);
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x0005B1F2 File Offset: 0x000593F2
	protected static void InvokeUserCode_CmdShieldEffect(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdShieldEffect called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdShieldEffect(reader.ReadBool());
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x0005B21C File Offset: 0x0005941C
	protected void UserCode_RpcShieldEffect(bool isAdd)
	{
		if (isAdd)
		{
			if (this.shieldEffect == null)
			{
				this.shieldEffect = AssetManager.LoadPrefab(EffectDefine.MagicShield, null, true);
				Transform transform = this.shieldEffect.transform;
				transform.SetParent(this.myTransform);
				transform.localPosition = Vector3.zero;
				transform.localScale = this.roleModeBase.headUIHeight / 2f * Vector3.one;
				return;
			}
		}
		else if (this.shieldEffect != null)
		{
			AssetManager.UnLoadPrefab(this.shieldEffect, false);
			this.shieldEffect = null;
		}
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x0005B2B0 File Offset: 0x000594B0
	protected static void InvokeUserCode_RpcShieldEffect(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcShieldEffect called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcShieldEffect(reader.ReadBool());
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x0005B2DC File Offset: 0x000594DC
	protected void UserCode_RpcRemoveuff(LocalBuffType localBuffType)
	{
		RoleBuffBase roleBuffBase;
		if (this.localRoleBuffDic.TryGetValue(localBuffType, out roleBuffBase))
		{
			roleBuffBase.ClearBuff();
			this.localRoleBuffDic.Remove(localBuffType);
		}
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x0005B30C File Offset: 0x0005950C
	protected static void InvokeUserCode_RpcRemoveuff(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcRemoveuff called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcRemoveuff(Mirror.GeneratedNetworkCode._Read_LocalBuffType(reader));
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0005B338 File Offset: 0x00059538
	protected void UserCode_RpcAddBuff(uint attackNetId, LocalBuffType localBuffType, float buffValue, float buffTime, int level)
	{
		if (this.IsDead())
		{
			return;
		}
		if (this.localRoleBuffDic.ContainsKey(localBuffType))
		{
			RoleBuffBase roleBuffBase;
			if (localBuffType == LocalBuffType.Poison && this.localRoleBuffDic.TryGetValue(localBuffType, out roleBuffBase))
			{
				PoisonRoleBuffBase poisonRoleBuffBase = roleBuffBase as PoisonRoleBuffBase;
				if (poisonRoleBuffBase == null)
				{
					return;
				}
				poisonRoleBuffBase.AddLevel(buffValue, buffTime, level);
			}
			return;
		}
		NetworkIdentity networkIdentity;
		if (NetworkClient.spawned.TryGetValue(attackNetId, out networkIdentity))
		{
			RoleBase component = networkIdentity.GetComponent<RoleBase>();
			RoleBuffBase roleBuffBase2 = null;
			switch (localBuffType)
			{
			case LocalBuffType.Frost:
				roleBuffBase2 = new FrostRoleBuffBase();
				roleBuffBase2.buffObject = AssetManager.LoadPrefab(EffectDefine.FrostBuff, null, true);
				roleBuffBase2.imgPath = "Skill/冰墙";
				break;
			case LocalBuffType.Fire:
				roleBuffBase2 = new FireRoleBuffBase();
				roleBuffBase2.checkOffset = 1f;
				roleBuffBase2.buffObject = AssetManager.LoadPrefab(EffectDefine.RoleOnFire, null, true);
				(roleBuffBase2 as FireRoleBuffBase).checkTime = buffTime - 0.1f;
				roleBuffBase2.imgPath = "Skill/火焰地雷";
				break;
			case LocalBuffType.ReAttack:
				roleBuffBase2 = new ReAttackBuff();
				roleBuffBase2.buffObject = AssetManager.LoadPrefab(EffectDefine.RoleReAttack, null, true);
				roleBuffBase2.imgPath = "Remains/sword_sub";
				break;
			case LocalBuffType.DragonFire:
				roleBuffBase2 = new FireRoleBuffBase();
				roleBuffBase2.checkOffset = 0.15f;
				roleBuffBase2.buffObject = AssetManager.LoadPrefab(EffectDefine.RoleOnFire, null, true);
				(roleBuffBase2 as FireRoleBuffBase).checkTime = buffTime - 0.1f;
				roleBuffBase2.imgPath = "Skill/火焰地雷";
				break;
			case LocalBuffType.SaiYaDark:
				roleBuffBase2 = new SaiYaDarkBuff();
				roleBuffBase2.deadNoClear = true;
				roleBuffBase2.buffObject = AssetManager.LoadPrefab(EffectDefine.RoleReAttack, null, true);
				roleBuffBase2.imgPath = "Skill/Enemy_SaiYaDark";
				break;
			case LocalBuffType.Poison:
				roleBuffBase2 = new PoisonRoleBuffBase();
				roleBuffBase2.checkOffset = 1f;
				roleBuffBase2.buffObject = AssetManager.LoadPrefab(EffectDefine.PoisonBuff, null, true);
				(roleBuffBase2 as PoisonRoleBuffBase).checkTime = buffTime - 0.1f;
				(roleBuffBase2 as PoisonRoleBuffBase).level = level;
				roleBuffBase2.imgPath = "Skill/SpellBook01_86";
				break;
			case LocalBuffType.DemonContract:
				roleBuffBase2 = new RoleSyncEffectBuff();
				roleBuffBase2.deadNoClear = true;
				roleBuffBase2.buffObject = AssetManager.LoadPrefab(EffectDefine.DemonContractBuff, null, true);
				break;
			case LocalBuffType.Guy:
				roleBuffBase2 = new RoleSyncEffectBuff();
				roleBuffBase2.deadNoClear = true;
				roleBuffBase2.buffObject = AssetManager.LoadPrefab(EffectDefine.GuyBuff, null, true);
				break;
			case LocalBuffType.SunFire:
				roleBuffBase2 = new RoleSyncHaloEffect();
				roleBuffBase2.deadNoClear = true;
				(roleBuffBase2 as RoleSyncHaloEffect).haloEffect = EffectDefine.SunFire;
				break;
			case LocalBuffType.SufferingHaloD:
			case LocalBuffType.SufferingHaloC:
			case LocalBuffType.SufferingHaloB:
			case LocalBuffType.SufferingHaloA:
			case LocalBuffType.SufferingHaloS:
				roleBuffBase2 = new RoleSyncHaloEffect();
				roleBuffBase2.deadNoClear = true;
				(roleBuffBase2 as RoleSyncHaloEffect).haloEffect = EffectDefine.SufferingHalo;
				break;
			}
			if (roleBuffBase2 != null)
			{
				roleBuffBase2.attackRoleBase = component;
				roleBuffBase2.buffTime = buffTime;
				roleBuffBase2.buffValue = buffValue;
				this.AddLocalBuff(localBuffType, roleBuffBase2);
			}
		}
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x0005B5E0 File Offset: 0x000597E0
	protected static void InvokeUserCode_RpcAddBuff(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcAddBuff called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcAddBuff(reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_LocalBuffType(reader), reader.ReadFloat(), reader.ReadFloat(), reader.ReadInt());
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x0005B62E File Offset: 0x0005982E
	protected void UserCode_TargetRpcUpdateXuanYun(float xuanyunTime)
	{
		if (!this.IsDead())
		{
			this.UpdateRoleState(RoleState.XuanYun);
			this.timer = xuanyunTime;
		}
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0005B646 File Offset: 0x00059846
	protected static void InvokeUserCode_TargetRpcUpdateXuanYun(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("TargetRPC TargetRpcUpdateXuanYun called on server.");
			return;
		}
		((RoleBase)obj).UserCode_TargetRpcUpdateXuanYun(reader.ReadFloat());
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x0005B670 File Offset: 0x00059870
	protected void UserCode_CmdUpdateOtherAttackSpeed(float updateValue, uint updateNetId)
	{
		NetworkIdentity networkIdentity;
		if (NetworkServer.spawned.TryGetValue(updateNetId, out networkIdentity))
		{
			networkIdentity.GetComponent<RoleBase>().RpcUpdateAttackSpeed(updateValue);
		}
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0005B698 File Offset: 0x00059898
	protected static void InvokeUserCode_CmdUpdateOtherAttackSpeed(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateOtherAttackSpeed called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdUpdateOtherAttackSpeed(reader.ReadFloat(), reader.ReadUInt());
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x0005B6C8 File Offset: 0x000598C8
	protected void UserCode_RpcUpdateAttackSpeed(float updateValue)
	{
		this.AddAttackSpeed(updateValue);
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x0005B6D1 File Offset: 0x000598D1
	protected static void InvokeUserCode_RpcUpdateAttackSpeed(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("TargetRPC RpcUpdateAttackSpeed called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcUpdateAttackSpeed(reader.ReadFloat());
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x0005B6FB File Offset: 0x000598FB
	protected void UserCode_CmdUpdateModeData(int value)
	{
		this.RpcUpdateModeData(value);
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x0005B704 File Offset: 0x00059904
	protected static void InvokeUserCode_CmdUpdateModeData(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateModeData called on client.");
			return;
		}
		((RoleBase)obj).UserCode_CmdUpdateModeData(reader.ReadInt());
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x0005B72D File Offset: 0x0005992D
	protected void UserCode_RpcUpdateModeData(int value)
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.OnUpdateModeData(value);
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x0005B740 File Offset: 0x00059940
	protected static void InvokeUserCode_RpcUpdateModeData(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpdateModeData called on server.");
			return;
		}
		((RoleBase)obj).UserCode_RpcUpdateModeData(reader.ReadInt());
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x0005B76C File Offset: 0x0005996C
	static RoleBase()
	{
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdSyncSkillData", new CmdDelegate(RoleBase.InvokeUserCode_CmdSyncSkillData), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdUpateMArmor", new CmdDelegate(RoleBase.InvokeUserCode_CmdUpateMArmor), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdSyncPos", new CmdDelegate(RoleBase.InvokeUserCode_CmdSyncPos), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdSyncEulerY", new CmdDelegate(RoleBase.InvokeUserCode_CmdSyncEulerY), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdHp", new CmdDelegate(RoleBase.InvokeUserCode_CmdHp), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdWuDi", new CmdDelegate(RoleBase.InvokeUserCode_CmdWuDi), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdUpdateRoleState", new CmdDelegate(RoleBase.InvokeUserCode_CmdUpdateRoleState), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdUpdateMaxHp", new CmdDelegate(RoleBase.InvokeUserCode_CmdUpdateMaxHp), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdUpdateHp", new CmdDelegate(RoleBase.InvokeUserCode_CmdUpdateHp), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdRelifeByState", new CmdDelegate(RoleBase.InvokeUserCode_CmdRelifeByState), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdRelife", new CmdDelegate(RoleBase.InvokeUserCode_CmdRelife), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdRelifeByHp", new CmdDelegate(RoleBase.InvokeUserCode_CmdRelifeByHp), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdCreateSkill", new CmdDelegate(RoleBase.InvokeUserCode_CmdCreateSkill), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdCreateSkillBySyncData", new CmdDelegate(RoleBase.InvokeUserCode_CmdCreateSkillBySyncData), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdSummon", new CmdDelegate(RoleBase.InvokeUserCode_CmdSummon), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdSummonByNum", new CmdDelegate(RoleBase.InvokeUserCode_CmdSummonByNum), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdUpdateAttackSpeed", new CmdDelegate(RoleBase.InvokeUserCode_CmdUpdateAttackSpeed), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdAttackNum", new CmdDelegate(RoleBase.InvokeUserCode_CmdAttackNum), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdTeleportBlink", new CmdDelegate(RoleBase.InvokeUserCode_CmdTeleportBlink), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdDoge", new CmdDelegate(RoleBase.InvokeUserCode_CmdDoge), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdUpdateShield", new CmdDelegate(RoleBase.InvokeUserCode_CmdUpdateShield), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdShieldEffect", new CmdDelegate(RoleBase.InvokeUserCode_CmdShieldEffect), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdUpdateOtherAttackSpeed", new CmdDelegate(RoleBase.InvokeUserCode_CmdUpdateOtherAttackSpeed), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(RoleBase), "CmdUpdateModeData", new CmdDelegate(RoleBase.InvokeUserCode_CmdUpdateModeData), true);
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcUpateMArmor", new CmdDelegate(RoleBase.InvokeUserCode_RpcUpateMArmor));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcWuDi", new CmdDelegate(RoleBase.InvokeUserCode_RpcWuDi));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcCreateSkill", new CmdDelegate(RoleBase.InvokeUserCode_RpcCreateSkill));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcCreateSkillBySyncData", new CmdDelegate(RoleBase.InvokeUserCode_RpcCreateSkillBySyncData));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcAttackNum", new CmdDelegate(RoleBase.InvokeUserCode_RpcAttackNum));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "ClientTeleportBlink", new CmdDelegate(RoleBase.InvokeUserCode_ClientTeleportBlink));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcDoge", new CmdDelegate(RoleBase.InvokeUserCode_RpcDoge));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcShieldEffect", new CmdDelegate(RoleBase.InvokeUserCode_RpcShieldEffect));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcRemoveuff", new CmdDelegate(RoleBase.InvokeUserCode_RpcRemoveuff));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcAddBuff", new CmdDelegate(RoleBase.InvokeUserCode_RpcAddBuff));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcUpdateModeData", new CmdDelegate(RoleBase.InvokeUserCode_RpcUpdateModeData));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "TargetRpcEnemyDead", new CmdDelegate(RoleBase.InvokeUserCode_TargetRpcEnemyDead));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "TargetRpcKillPlayer", new CmdDelegate(RoleBase.InvokeUserCode_TargetRpcKillPlayer));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "TargetRpcUpdateRoleState", new CmdDelegate(RoleBase.InvokeUserCode_TargetRpcUpdateRoleState));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "TargetRpcUpdateXuanYun", new CmdDelegate(RoleBase.InvokeUserCode_TargetRpcUpdateXuanYun));
		RemoteCallHelper.RegisterRpcDelegate(typeof(RoleBase), "RpcUpdateAttackSpeed", new CmdDelegate(RoleBase.InvokeUserCode_RpcUpdateAttackSpeed));
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x0005BC94 File Offset: 0x00059E94
	public override bool SerializeSyncVars(NetworkWriter writer, bool forceAll)
	{
		bool result = base.SerializeSyncVars(writer, forceAll);
		if (forceAll)
		{
			writer.WriteFloat(this.syncSkillData);
			writer.WriteLong(this.shield);
			writer.WriteFloat(this.syncAttackSpeed);
			writer.WriteVector3(this.syncPos);
			writer.WriteFloat(this.syncEulerY);
			writer.WriteInt(this.syncSequenceId);
			Mirror.GeneratedNetworkCode._Write_RoleState(writer, this.roleState);
			writer.WriteLong(this.maxHp);
			writer.WriteLong(this.hp);
			return true;
		}
		writer.WriteULong(base.syncVarDirtyBits);
		if ((base.syncVarDirtyBits & 1UL) != 0UL)
		{
			writer.WriteFloat(this.syncSkillData);
			result = true;
		}
		if ((base.syncVarDirtyBits & 2UL) != 0UL)
		{
			writer.WriteLong(this.shield);
			result = true;
		}
		if ((base.syncVarDirtyBits & 4UL) != 0UL)
		{
			writer.WriteFloat(this.syncAttackSpeed);
			result = true;
		}
		if ((base.syncVarDirtyBits & 8UL) != 0UL)
		{
			writer.WriteVector3(this.syncPos);
			result = true;
		}
		if ((base.syncVarDirtyBits & 16UL) != 0UL)
		{
			writer.WriteFloat(this.syncEulerY);
			result = true;
		}
		if ((base.syncVarDirtyBits & 32UL) != 0UL)
		{
			writer.WriteInt(this.syncSequenceId);
			result = true;
		}
		if ((base.syncVarDirtyBits & 64UL) != 0UL)
		{
			Mirror.GeneratedNetworkCode._Write_RoleState(writer, this.roleState);
			result = true;
		}
		if ((base.syncVarDirtyBits & 128UL) != 0UL)
		{
			writer.WriteLong(this.maxHp);
			result = true;
		}
		if ((base.syncVarDirtyBits & 256UL) != 0UL)
		{
			writer.WriteLong(this.hp);
			result = true;
		}
		return result;
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x0005BE70 File Offset: 0x0005A070
	public override void DeserializeSyncVars(NetworkReader reader, bool initialState)
	{
		base.DeserializeSyncVars(reader, initialState);
		if (initialState)
		{
			float num = this.syncSkillData;
			this.NetworksyncSkillData = reader.ReadFloat();
			long num2 = this.shield;
			this.Networkshield = reader.ReadLong();
			float num3 = this.syncAttackSpeed;
			this.NetworksyncAttackSpeed = reader.ReadFloat();
			Vector3 vector = this.syncPos;
			this.NetworksyncPos = reader.ReadVector3();
			float num4 = this.syncEulerY;
			this.NetworksyncEulerY = reader.ReadFloat();
			int num5 = this.syncSequenceId;
			this.NetworksyncSequenceId = reader.ReadInt();
			RoleState roleState = this.roleState;
			this.NetworkroleState = Mirror.GeneratedNetworkCode._Read_RoleState(reader);
			if (!NetworkBehaviour.SyncVarEqual<RoleState>(roleState, ref this.roleState))
			{
				this.OnChangeRoleState(roleState, this.roleState);
			}
			long num6 = this.maxHp;
			this.NetworkmaxHp = reader.ReadLong();
			long num7 = this.hp;
			this.Networkhp = reader.ReadLong();
			return;
		}
		long num8 = (long)reader.ReadULong();
		if ((num8 & 1L) != 0L)
		{
			float num9 = this.syncSkillData;
			this.NetworksyncSkillData = reader.ReadFloat();
		}
		if ((num8 & 2L) != 0L)
		{
			long num10 = this.shield;
			this.Networkshield = reader.ReadLong();
		}
		if ((num8 & 4L) != 0L)
		{
			float num11 = this.syncAttackSpeed;
			this.NetworksyncAttackSpeed = reader.ReadFloat();
		}
		if ((num8 & 8L) != 0L)
		{
			Vector3 vector2 = this.syncPos;
			this.NetworksyncPos = reader.ReadVector3();
		}
		if ((num8 & 16L) != 0L)
		{
			float num12 = this.syncEulerY;
			this.NetworksyncEulerY = reader.ReadFloat();
		}
		if ((num8 & 32L) != 0L)
		{
			int num13 = this.syncSequenceId;
			this.NetworksyncSequenceId = reader.ReadInt();
		}
		if ((num8 & 64L) != 0L)
		{
			RoleState roleState2 = this.roleState;
			this.NetworkroleState = Mirror.GeneratedNetworkCode._Read_RoleState(reader);
			if (!NetworkBehaviour.SyncVarEqual<RoleState>(roleState2, ref this.roleState))
			{
				this.OnChangeRoleState(roleState2, this.roleState);
			}
		}
		if ((num8 & 128L) != 0L)
		{
			long num14 = this.maxHp;
			this.NetworkmaxHp = reader.ReadLong();
		}
		if ((num8 & 256L) != 0L)
		{
			long num15 = this.hp;
			this.Networkhp = reader.ReadLong();
		}
	}

	// Token: 0x04000DE8 RID: 3560
	[HideInInspector]
	public int henShinSkillId = -1;

	// Token: 0x04000DE9 RID: 3561
	[HideInInspector]
	public int sid = -1;

	// Token: 0x04000DEA RID: 3562
	[SyncVar]
	protected float syncSkillData;

	// Token: 0x04000DEB RID: 3563
	private float syncSkillDataTime;

	// Token: 0x04000DEC RID: 3564
	private float syncPosTime;

	// Token: 0x04000DED RID: 3565
	private float syncAngleTime;

	// Token: 0x04000DEE RID: 3566
	private bool resetAnim;

	// Token: 0x04000DEF RID: 3567
	private bool xuanYunImmunity;

	// Token: 0x04000DF0 RID: 3568
	protected float canXuanYunLastTime;

	// Token: 0x04000DF1 RID: 3569
	protected RoleType fatherType;

	// Token: 0x04000DF2 RID: 3570
	protected int fatherId;

	// Token: 0x04000DF3 RID: 3571
	protected int skillBookId;

	// Token: 0x04000DF4 RID: 3572
	[HideInInspector]
	public float exAttackDistance;

	// Token: 0x04000DF5 RID: 3573
	[HideInInspector]
	public uint authorityId;

	// Token: 0x04000DF6 RID: 3574
	[HideInInspector]
	public string roleName;

	// Token: 0x04000DF7 RID: 3575
	[HideInInspector]
	public RoleType roleType;

	// Token: 0x04000DF8 RID: 3576
	protected float mMoveSpeed = 2f;

	// Token: 0x04000DF9 RID: 3577
	[HideInInspector]
	public float moveSpeedPercent = 1f;

	// Token: 0x04000DFB RID: 3579
	[SyncVar]
	private long shield;

	// Token: 0x04000DFC RID: 3580
	private GameObject shieldEffect;

	// Token: 0x04000DFD RID: 3581
	[HideInInspector]
	public bool isCheckAttack;

	// Token: 0x04000DFE RID: 3582
	[HideInInspector]
	public float oldRotation;

	// Token: 0x04000DFF RID: 3583
	[HideInInspector]
	public RoleBuffManager roleBuffManager;

	// Token: 0x04000E00 RID: 3584
	[HideInInspector]
	public int attackNum = 1;

	// Token: 0x04000E01 RID: 3585
	[HideInInspector]
	public List<string> effectCards = new List<string>();

	// Token: 0x04000E04 RID: 3588
	[HideInInspector]
	public int overrideAnimSkillId;

	// Token: 0x04000E05 RID: 3589
	protected bool isOverrideAnim;

	// Token: 0x04000E06 RID: 3590
	protected int aniName = -1;

	// Token: 0x04000E07 RID: 3591
	protected float aniSpeed;

	// Token: 0x04000E08 RID: 3592
	[HideInInspector]
	public float timer;

	// Token: 0x04000E09 RID: 3593
	[HideInInspector]
	public Transform animTransform;

	// Token: 0x04000E0A RID: 3594
	[HideInInspector]
	public float deadMoveSpeed = 0.25f;

	// Token: 0x04000E0B RID: 3595
	[HideInInspector]
	public int deadStartMoveTime = 4;

	// Token: 0x04000E0C RID: 3596
	[HideInInspector]
	public int deadEndMoveTime = 8;

	// Token: 0x04000E0D RID: 3597
	private int curMaterialIndex;

	// Token: 0x04000E0E RID: 3598
	private bool isEmit;

	// Token: 0x04000E0F RID: 3599
	private float emitValue;

	// Token: 0x04000E10 RID: 3600
	private Tweener hitTweener;

	// Token: 0x04000E11 RID: 3601
	[HideInInspector]
	[SyncVar]
	public float syncAttackSpeed;

	// Token: 0x04000E12 RID: 3602
	protected Transform myTransform;

	// Token: 0x04000E13 RID: 3603
	[SyncVar]
	protected Vector3 syncPos;

	// Token: 0x04000E14 RID: 3604
	[SyncVar]
	protected float syncEulerY;

	// Token: 0x04000E15 RID: 3605
	[SyncVar]
	protected int syncSequenceId;

	// Token: 0x04000E16 RID: 3606
	private int localSequenceId;

	// Token: 0x04000E17 RID: 3607
	private RoleState localRoleState;

	// Token: 0x04000E18 RID: 3608
	[SyncVar(hook = "OnChangeRoleState")]
	protected RoleState roleState;

	// Token: 0x04000E19 RID: 3609
	[SyncVar]
	public long maxHp = 100L;

	// Token: 0x04000E1A RID: 3610
	[SyncVar]
	public long hp = 100L;

	// Token: 0x04000E1B RID: 3611
	[HideInInspector]
	public RoleBase trackRoleBase;

	// Token: 0x04000E1C RID: 3612
	[HideInInspector]
	public float addHatred;

	// Token: 0x04000E1F RID: 3615
	[HideInInspector]
	public float normalBreakShield;

	// Token: 0x04000E20 RID: 3616
	[HideInInspector]
	public float normalBreakShieldBase;

	// Token: 0x04000E21 RID: 3617
	[HideInInspector]
	public float skillBreakShield;

	// Token: 0x04000E22 RID: 3618
	[HideInInspector]
	public float skillBreakShieldBase;

	// Token: 0x04000E23 RID: 3619
	[HideInInspector]
	public bool canSkillCritical;

	// Token: 0x04000E24 RID: 3620
	[HideInInspector]
	public float skillCriticalLevel;

	// Token: 0x04000E25 RID: 3621
	[HideInInspector]
	public bool canBuffCritical;

	// Token: 0x04000E26 RID: 3622
	[HideInInspector]
	public float buffCriticalLevel;

	// Token: 0x04000E27 RID: 3623
	protected float mCritical;

	// Token: 0x04000E28 RID: 3624
	protected float mCriticalDamage = 1.5f;

	// Token: 0x04000E2A RID: 3626
	private GameObject wuDiEffect;

	// Token: 0x04000E2B RID: 3627
	protected int mHpAddSec;

	// Token: 0x04000E2C RID: 3628
	[HideInInspector]
	public float hpAddSecRate;

	// Token: 0x04000E33 RID: 3635
	[HideInInspector]
	public int doge;

	// Token: 0x04000E34 RID: 3636
	protected float mXiXue;

	// Token: 0x04000E35 RID: 3637
	[HideInInspector]
	public float xiXueLv;

	// Token: 0x04000E36 RID: 3638
	public RoleBase.AttackEnemy attackEnemyEvent;

	// Token: 0x04000E37 RID: 3639
	public RoleBase.FinalAttackDamage finalAttackEvent;

	// Token: 0x04000E38 RID: 3640
	public RoleBase.AttackEnemy skillEnemyEvent;

	// Token: 0x04000E39 RID: 3641
	public RoleBase.DamageEnemy damageEvent;

	// Token: 0x04000E3A RID: 3642
	public RoleBase.KillEnemy killEnemyEvent;

	// Token: 0x04000E3B RID: 3643
	public RoleBase.UseSkillEvent useSkillEvent;

	// Token: 0x04000E3C RID: 3644
	public RoleBase.DieEvent dieEvent;

	// Token: 0x04000E3D RID: 3645
	public RoleBase.SkillBookEvent skillBookEvent;

	// Token: 0x04000E3E RID: 3646
	public RoleBase.Critical criticalEvent;

	// Token: 0x04000E3F RID: 3647
	public RoleBase.HealthHp healthHpEvent;

	// Token: 0x04000E40 RID: 3648
	public RoleBase.OnEquipChange onEquipChange;

	// Token: 0x04000E41 RID: 3649
	public RoleBase.DogeEvent dogeEvent;

	// Token: 0x04000E42 RID: 3650
	public RoleBase.OnStartAttackEvent onStartAttackEvent;

	// Token: 0x04000E43 RID: 3651
	protected GameObject roleStateEffect;

	// Token: 0x04000E44 RID: 3652
	protected RoleModeBase roleModeBase;

	// Token: 0x04000E45 RID: 3653
	protected RoleModeBase oldModeBase;

	// Token: 0x04000E46 RID: 3654
	[HideInInspector]
	public Dictionary<LocalBuffType, RoleBuffBase> localRoleBuffDic = new Dictionary<LocalBuffType, RoleBuffBase>();

	// Token: 0x04000E47 RID: 3655
	private float addHpTime;

	// Token: 0x04000E48 RID: 3656
	private RuntimeAnimatorController runtimeAnimatorController;

	// Token: 0x020002AB RID: 683
	// (Invoke) Token: 0x0600102D RID: 4141
	public delegate float AttackEnemy(RoleBase attackRole, RoleBase hurtRole, ref float damage);

	// Token: 0x020002AC RID: 684
	// (Invoke) Token: 0x06001031 RID: 4145
	public delegate float FinalAttackDamage(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float damage);

	// Token: 0x020002AD RID: 685
	// (Invoke) Token: 0x06001035 RID: 4149
	public delegate float DamageEnemy(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float damage);

	// Token: 0x020002AE RID: 686
	// (Invoke) Token: 0x06001039 RID: 4153
	public delegate ActiveSkillEnum UseSkillEvent(ActiveSkillEnum activeSkillEnum);

	// Token: 0x020002AF RID: 687
	// (Invoke) Token: 0x0600103D RID: 4157
	public delegate void HealthHp(long updateHpValue);

	// Token: 0x020002B0 RID: 688
	// (Invoke) Token: 0x06001041 RID: 4161
	public delegate void KillEnemy(RoleBase attackRole, RoleBase hurtRole);

	// Token: 0x020002B1 RID: 689
	// (Invoke) Token: 0x06001045 RID: 4165
	public delegate void DieEvent(RoleBase attackRole);

	// Token: 0x020002B2 RID: 690
	// (Invoke) Token: 0x06001049 RID: 4169
	public delegate void SkillBookEvent(RoleBase useRole);

	// Token: 0x020002B3 RID: 691
	// (Invoke) Token: 0x0600104D RID: 4173
	public delegate void Critical(RoleBase hurtRole, long damage);

	// Token: 0x020002B4 RID: 692
	// (Invoke) Token: 0x06001051 RID: 4177
	public delegate void DogeEvent();

	// Token: 0x020002B5 RID: 693
	// (Invoke) Token: 0x06001055 RID: 4181
	public delegate void OnEquipChange();

	// Token: 0x020002B6 RID: 694
	// (Invoke) Token: 0x06001059 RID: 4185
	public delegate void OnStartAttackEvent(RoleBase hurtRole, float realAttackOffset);
}
