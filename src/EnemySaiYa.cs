using System;
using UnityEngine;

// Token: 0x02000272 RID: 626
public class EnemySaiYa : EnemyMeleeMode
{
	// Token: 0x06000B87 RID: 2951 RVA: 0x0003DBA2 File Offset: 0x0003BDA2
	protected override void Awake()
	{
		base.Awake();
		this.startThroneY = this.throne.transform.localPosition.y;
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x0003DBC8 File Offset: 0x0003BDC8
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		this.enemyBase.deadMoveSpeed = 0.75f;
		this.isUseSkill3 = false;
		this.enemyBase.XuanYunImmunity = true;
		if (this.enemyBase.damageEvent == null)
		{
			EnemyBase enemyBase = this.enemyBase;
			enemyBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(enemyBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
		}
		this.enemyBase.reduceInjury = 1000;
		this.enemyBase.SetWuDi(true);
		Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.enemyBase, new string[]
		{
			Game.Language.Get("Talk_SaiYa_Start_0", "")
		}, 5f);
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0003DC88 File Offset: 0x0003BE88
	public override void UpdateSkill1()
	{
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 3.633f / this.enemyBase.AniSpeed)
		{
			this.enemyBase.UpdateRoleState(RoleState.Run);
			return;
		}
		if (!this.enemyBase.isCheckAttack && this.enemyBase.timer > 1.16256f / this.enemyBase.AniSpeed)
		{
			this.enemyBase.isCheckAttack = true;
			this.enemyBase.CmdCreateSkill(ActiveSkillEnum.DrangonFireBoom, this.skillPos, 0f, -1, 0);
		}
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x0003DD3B File Offset: 0x0003BF3B
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		this.CheckUseSkill3();
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0003DD4C File Offset: 0x0003BF4C
	public override void OnStartSkill()
	{
		this.CheckUseSkill3();
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill, 0.7f, 0.1f);
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/SaiYaSkill1", this.roleBase.MyTransform.position, 0.8f);
		this.enemyBase.isCheckAttack = false;
		this.isPlayerEffect = false;
		if (this.enemyBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = true;
			this.skillPos = ((this.enemyBase.trackRoleBase != null) ? this.enemyBase.trackRoleBase.MyTransform.position : (this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.forward * 5f));
			this.skillPos = Util.GetSaveMapPos(this.skillPos);
			float num = Vector3.Distance(this.skillPos, this.enemyBase.MyTransform.position);
			this.rootMoionSpeed = Mathf.Max(1f, num / 3.5f);
			ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[ActiveSkillEnum.DrangonFireBoom];
			if (this.enemyBase.roleType == RoleType.Enemy)
			{
				this.enemyBase.CmdPlayTipSector(this.skillPos, activeSkillData.range, 1.16256f / this.enemyBase.AniSpeed);
			}
		}
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x0003B1F7 File Offset: 0x000393F7
	public override void OnExitSkill()
	{
		base.OnExitSkill();
		if (this.enemyBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = false;
		}
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0003DECC File Offset: 0x0003C0CC
	private void OnAnimatorMove()
	{
		if (this.myAnim.applyRootMotion)
		{
			if (this.enemyBase.RoleState == RoleState.Skill3)
			{
				this.enemyBase.MyTransform.position += this.myAnim.deltaPosition;
				return;
			}
			if (this.enemyBase.timer <= 0.35f)
			{
				float v2Angle = this.enemyBase.GetV2Angle(this.skillPos);
				this.enemyBase.oldRotation = this.enemyBase.MyTransform.localEulerAngles.y;
				this.enemyBase.PingHuaZhuanShen(v2Angle, 3f);
			}
			if (this.enemyBase.timer > 0.35f && this.enemyBase.timer < 1f)
			{
				this.enemyBase.MyTransform.position += this.myAnim.deltaPosition * this.rootMoionSpeed;
				return;
			}
			if (this.enemyBase.timer > 1.5f / this.roleBase.AniSpeed)
			{
				this.enemyBase.MyTransform.position += this.myAnim.deltaPosition;
			}
		}
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0003E010 File Offset: 0x0003C210
	public override void OnStartSkill2()
	{
		this.CheckUseSkill3();
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/SaiYaCast", this.roleBase.MyTransform.position, 1f);
		this.enemyBase.timer = 0f;
		this.enemyBase.PlayAni(AnimDefine.Skill2, 1f, 0.1f);
		this.enemyBase.isCheckAttack = false;
		this.isPlayerEffect = false;
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0003E084 File Offset: 0x0003C284
	public override void UpdateSkill2()
	{
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > this.skill1Time)
		{
			if (this.enemyBase.hasAuthority)
			{
				this.enemyBase.UpdateRoleState(RoleState.Run);
				return;
			}
		}
		else
		{
			if (!this.enemyBase.isCheckAttack && this.enemyBase.timer > this.skill1Time - 1.4f)
			{
				this.enemyBase.isCheckAttack = true;
				if (this.enemyBase.hasAuthority)
				{
					this.enemyBase.CmdCreateSkill(ActiveSkillEnum.FireDaggers, this.enemyBase.MyTransform.position, this.enemyBase.MyTransform.localEulerAngles.y, -1, 0);
				}
			}
			if (this.enemyBase.timer > this.skill1Time - 2.2f)
			{
				if (!this.isPlayerEffect)
				{
					this.isPlayerEffect = true;
					if (this.enemyBase.roleType == RoleType.Enemy)
					{
						Game.EffectManager.PlayTipSector(this.enemyBase.MyTransform.position - this.enemyBase.MyTransform.forward * 2.89f, 39.6f, this.enemyBase.MyTransform.localEulerAngles.y, 30f, 0.8f, 0.045f);
						return;
					}
				}
			}
			else if (this.enemyBase.timer < this.skill1Time - 2.3f && this.enemyBase.hasAuthority)
			{
				this.enemyBase.TrackRotation(3f);
			}
		}
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x0003E22C File Offset: 0x0003C42C
	public override void OnStartShowPose()
	{
		this.roleBase.timer = 0f;
		bool flag = this != null && this.isPlayShowPoseAnim;
		this.roleBase.PlayAni(flag ? AnimDefine.ShowPose : AnimDefine.Idle, 1f, 0.1f);
		Vector3 localPosition = new Vector3(0f, -this.roleBase.animTransform.localScale.y / this.baseModeScale.y * this.headUIHeight, 0f);
		this.roleBase.animTransform.localPosition = localPosition;
		this.throne.SetActive(true);
		this.throne.transform.localPosition = new Vector3(this.throne.transform.localPosition.x, this.startThroneY, this.throne.transform.localPosition.z);
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x0003E318 File Offset: 0x0003C518
	public override void UpdateShowPose()
	{
		float deltaTime = Time.deltaTime;
		this.roleBase.timer += deltaTime;
		Vector3 a = new Vector3(0f, -this.roleBase.animTransform.localScale.y / this.baseModeScale.y * this.headUIHeight, 0f);
		this.roleBase.animTransform.localPosition = Vector3.Lerp(a, Vector3.zero, Mathf.Min(1f, this.roleBase.timer));
		if (this.roleBase.hasAuthority && this.roleBase.timer > 3.25f)
		{
			this.roleBase.UpdateRoleState(RoleState.Idle);
		}
		if (this.roleBase.timer > 2.2f)
		{
			this.throne.transform.localPosition = new Vector3(this.throne.transform.localPosition.x, this.throne.transform.localPosition.y - deltaTime * 2f, this.throne.transform.localPosition.z);
		}
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x0003E441 File Offset: 0x0003C641
	public override void OnExitShowPose()
	{
		base.OnExitShowPose();
		this.enemyBase.SetWuDi(false);
		this.throne.SetActive(false);
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0003E464 File Offset: 0x0003C664
	public override void OnStartDead()
	{
		this.roleBase.SetWuDi(false);
		this.roleBase.PlayAni(GameHelperClient.isReady ? AnimDefine.Idle : AnimDefine.Dead, 1f, 0.1f);
		this.isRelife = !GameHelperClient.isReady;
		if (this.isRelife)
		{
			if (this.roleBase.roleType == RoleType.Enemy)
			{
				Util.ShowTips("塞亚复活");
			}
			Game.AudioManager.PlayDeadAudio(this.deadSound, this.roleBase.MyTransform.position);
		}
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x0003E4F4 File Offset: 0x0003C6F4
	public override void UpdateDead()
	{
		base.UpdateDead();
		if (this.roleBase.hasAuthority && this.roleBase.roleType == RoleType.Enemy && this.isRelife && this.roleBase.timer > 5.5f && !GameHelperClient.isReady)
		{
			this.isRelife = false;
			GameHelperClient.localPlayer.CmdCreateEnemyByPos(EnemyType.SaiYaDark, this.roleBase.MyTransform.position);
		}
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x0003E568 File Offset: 0x0003C768
	public override void UpdateEvent()
	{
		base.UpdateEvent();
		this.CheckUseSkill3();
		if (this.enemyBase.hasAuthority && this.enemyBase.RoleState != RoleState.Skill3 && this.enemyBase.RoleState != RoleState.ShowPose && this.enemyBase.wudi)
		{
			this.enemyBase.SetWuDi(false);
		}
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x0003E5C4 File Offset: 0x0003C7C4
	private void CheckUseSkill3()
	{
		if (this.enemyBase.hasAuthority && !this.isUseSkill3 && (float)this.enemyBase.hp < (float)this.enemyBase.maxHp * 0.95f)
		{
			this.isUseSkill3 = true;
			this.enemyBase.SetAttackCd(0, 0f);
		}
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x0003E620 File Offset: 0x0003C820
	public override void OnStartSkill3()
	{
		this.CheckUseSkill3();
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
		this.enemyBase.timer = 0f;
		this.enemyBase.PlayAni(AnimDefine.Skill3, 1f, 0.1f);
		this.enemyBase.isCheckAttack = false;
		this.isPlayerEffect = false;
		this.isPlayerTip = false;
		this.updateLoadTime = 0f;
		this.attackNum = 0;
		Game.CameraManager.ShakeCameraByPos(this.enemyBase.MyTransform.position, 6.134f, 0.05f, 15, false);
		if (this.enemyBase.hasAuthority)
		{
			this.enemyBase.SetWuDi(true);
			this.myAnim.applyRootMotion = true;
		}
		Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.enemyBase, new string[]
		{
			Game.Language.Get("Talk_Ghost_Skill3_0", "")
		}, 5f);
		Util.ShowTips("塞亚蓄力提示");
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x0003E738 File Offset: 0x0003C938
	public override void UpdateSkill3()
	{
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 8.334f)
		{
			if (this.enemyBase.hasAuthority)
			{
				this.enemyBase.UpdateRoleState(RoleState.Run);
				return;
			}
		}
		else
		{
			if (this.enemyBase.hasAuthority)
			{
				if (this.enemyBase.timer < 0.5f)
				{
					this.enemyBase.TrackRotation(2f);
				}
				if (!this.enemyBase.isCheckAttack && this.enemyBase.timer > 6.1839995f)
				{
					this.enemyBase.isCheckAttack = true;
					this.enemyBase.CmdCreateSkillBySyncData(ActiveSkillEnum.ChargeBoom, this.skillPos, Mathf.RoundToInt(this.enemyBase.SyncSkillData), this.enemyBase.MyTransform.localEulerAngles.y, -1, 0);
				}
			}
			if (this.enemyBase.timer > 0.15f && !this.isPlayerTip)
			{
				this.isPlayerTip = true;
				this.skillPos = (this.enemyBase.hasAuthority ? this.enemyBase.MyTransform.position : this.enemyBase.SyncPos);
				float lifeTime = 6.034f;
				this.tipEffect = Game.EffectManager.PlayTipSector(this.skillPos, 10f, 0f, 360f, lifeTime, 0f);
				this.skill3StartEffect = Game.EffectManager.PlayEffect(EffectDefine.SaiYaSkill3RangeEffect, lifeTime, this.skillPos, 0.5f);
			}
			if (this.isPlayerTip)
			{
				float num = Mathf.Lerp(this.tipEffect.transform.localScale.x, (5f + this.enemyBase.SyncSkillData * 0.25f) * 2f, deltaTime * 10f);
				this.tipEffect.transform.localScale = Vector3.one * num;
				this.skill3StartEffect.localScale = new Vector3(num, 10f, num) / 20f;
			}
			this.updateLoadTime += deltaTime;
			if (this.updateLoadTime > 0.1f)
			{
				this.updateLoadTime = 0f;
				if (this.attackNum > 0)
				{
					GameHelperClient.localPlayer.CmdUpdateSaiYaSkill3(this.enemyBase.netId, this.attackNum);
					this.attackNum = 0;
				}
			}
		}
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x0003E9A4 File Offset: 0x0003CBA4
	public override void OnExitSkill3()
	{
		base.OnExitSkill3();
		this.skill3StartEffect = null;
		if (this.enemyBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = false;
			this.enemyBase.SetWuDi(false);
		}
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x0003E9D8 File Offset: 0x0003CBD8
	private float DamageEvent(RoleBase attackrole, RoleBase hurtrole, AttackType attackType, ref float f)
	{
		if (this.enemyBase.RoleState == RoleState.Skill3)
		{
			this.attackNum++;
		}
		float num = (float)hurtrole.maxHp * 0.04f;
		if (f > num)
		{
			f = num;
		}
		return f;
	}

	// Token: 0x04000C6B RID: 3179
	private bool isPlayerEffect;

	// Token: 0x04000C6C RID: 3180
	private Vector3 skillPos;

	// Token: 0x04000C6D RID: 3181
	private float rootMoionSpeed;

	// Token: 0x04000C6E RID: 3182
	private const float SkillAniTime = 3.633f;

	// Token: 0x04000C6F RID: 3183
	private const float SkillCheckTime = 1.16256f;

	// Token: 0x04000C70 RID: 3184
	private float skill1Time = 3.327273f;

	// Token: 0x04000C71 RID: 3185
	[SerializeField]
	private GameObject throne;

	// Token: 0x04000C72 RID: 3186
	private float startThroneY;

	// Token: 0x04000C73 RID: 3187
	private bool isRelife;

	// Token: 0x04000C74 RID: 3188
	private bool isUseSkill3;

	// Token: 0x04000C75 RID: 3189
	private const float Skill3Time = 8.334f;

	// Token: 0x04000C76 RID: 3190
	private bool isPlayerTip;

	// Token: 0x04000C77 RID: 3191
	public const float Skill3Range = 5f;

	// Token: 0x04000C78 RID: 3192
	public const float Skill3AddLevel = 0.25f;

	// Token: 0x04000C79 RID: 3193
	private float updateLoadTime;

	// Token: 0x04000C7A RID: 3194
	private int attackNum;

	// Token: 0x04000C7B RID: 3195
	private TipEffect tipEffect;

	// Token: 0x04000C7C RID: 3196
	private Transform skill3StartEffect;
}
