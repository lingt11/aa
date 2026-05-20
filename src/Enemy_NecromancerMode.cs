using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x0200027C RID: 636
public class Enemy_NecromancerMode : EnemyMeleeMode
{
	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00040ECC File Offset: 0x0003F0CC
	public BrotatoWeaponController BrotatoWeaponController
	{
		get
		{
			return this.brotatoWeaponController;
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00040ED4 File Offset: 0x0003F0D4
	public Transform SkillSwordTran
	{
		get
		{
			return this.skillSwordTran;
		}
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00040EDC File Offset: 0x0003F0DC
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		this.brotatoWeaponController.ClearBrotatoWeapons();
		if (this.skillSwordTran != null)
		{
			this.skillSwordTran.gameObject.SetActive(false);
		}
		this.skillEndTime = -1f;
		if (this.roleBase.hasAuthority)
		{
			if (this.roleBase.attackEnemyEvent == null)
			{
				RoleBase roleBase = this.roleBase;
				roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
				return;
			}
		}
		else
		{
			this.roleBase.attackEnemyEvent = null;
		}
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00040F72 File Offset: 0x0003F172
	public override void OnClearMode()
	{
		base.OnClearMode();
		this.KillSwordTweens();
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00040F80 File Offset: 0x0003F180
	public override void UpdateEvent()
	{
		base.UpdateEvent();
		if (this.skillEndTime > 0f && this.skillSwordTran != null && this.skillSwordTran.gameObject.activeSelf && this.enemyBase.hasAuthority && Time.time > this.skillEndTime)
		{
			this.skillEndTime = -1f;
			List<RoleBase> friendRoles = this.roleBase.GetFriendRoles();
			this.createSwordPos.Clear();
			for (int i = friendRoles.Count - 1; i > -1; i--)
			{
				RoleBase roleBase = friendRoles[i];
				if (roleBase != null && (long)roleBase.FatherId == (long)((ulong)this.roleBase.netId) && !roleBase.IsDead())
				{
					EnemyBase enemyBase = roleBase as EnemyBase;
					if (enemyBase != null)
					{
						enemyBase.CmdAutoDead();
						this.createSwordPos.Add(enemyBase.MyTransform.position);
					}
				}
			}
			this.roleBase.UpdateRoleState(RoleState.Skill2);
		}
		this.brotatoWeaponController.UpdateEvent();
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x0004108C File Offset: 0x0003F28C
	public override void OnStartSkill()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill, 1f, 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/SaiYaDarkCast", this.roleBase.MyTransform.position, 1f);
		this.enemyBase.isCheckAttack = false;
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x00041114 File Offset: 0x0003F314
	public override void UpdateSkill1()
	{
		base.UpdateSkill1();
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 1.2f / this.enemyBase.AniSpeed)
		{
			this.enemyBase.UpdateRoleState(RoleState.Idle);
			return;
		}
		if (this.enemyBase.timer > 1.2f / this.enemyBase.AniSpeed * 0.38f && !this.enemyBase.isCheckAttack)
		{
			this.enemyBase.isCheckAttack = true;
			this.enemyBase.CmdCreateSkill(ActiveSkillEnum.NecromancerCall, this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.forward * 1.5f, 0f, -1, 0);
		}
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x000411FC File Offset: 0x0003F3FC
	public void CreateSword(Vector3 groundPosition, float necromancerCallTime)
	{
		if (this.sword == null)
		{
			Debug.LogError("Enemy_NecromancerMode missing sword reference.", this);
			return;
		}
		this.skillEndTime = Time.time + necromancerCallTime;
		if (this.skillSwordTran == null)
		{
			this.skillSwordTran = AssetManager.LoadPrefab(EffectDefine.NecromancerSword, null, true).transform;
		}
		if (!this.skillSwordTran.gameObject.activeSelf)
		{
			this.skillSwordTran.gameObject.SetActive(true);
		}
		this.skillSwordTran.position = this.sword.position;
		this.skillSwordTran.rotation = this.sword.rotation;
		groundPosition.y = 2f;
		Sequence sequence = DOTween.Sequence().SetLink(this.skillSwordTran.gameObject);
		sequence.Append(this.skillSwordTran.DOMoveY(groundPosition.y + 3.5f, 0.5f, false).SetEase(Ease.OutCubic));
		Vector3 endValue = new Vector3(0f, 1440f, 0f);
		sequence.Join(this.skillSwordTran.DORotate(endValue, 0.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.InOutSine));
		sequence.Append(this.skillSwordTran.DOMoveY(groundPosition.y, 0.5f, false).SetEase(Ease.InCubic));
		sequence.Join(this.skillSwordTran.DORotate(new Vector3(0f, 0f, 180f), 0.25f, RotateMode.Fast).SetEase(Ease.OutQuad));
		sequence.onComplete = delegate()
		{
			if (this.skillSwordTran == null)
			{
				return;
			}
			this.skillSwordTran.DOPunchPosition(Vector3.up * 0.2f, 0.2f, 10, 1f, false);
			Vector3 vector = new Vector3(groundPosition.x, 0.5f, groundPosition.z);
			Game.EffectManager.PlayEffect(EffectDefine.SmokeEffect, 3f, vector, 2f);
			Game.CameraManager.ShakeCameraByPos(vector, 0.1f, 0.75f, 15, false);
		};
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x000413B4 File Offset: 0x0003F5B4
	public override void OnStartSkill2()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill, 1f, 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/SaiYaDarkCast", this.roleBase.MyTransform.position, 1f);
		this.enemyBase.isCheckAttack = false;
		if (this.skillSwordTran != null)
		{
			this.skillSwordTran.DOKill(false);
			Vector3 endValue = new Vector3(0f, 0f, 1080f);
			float num = 0.8f;
			this.skillSwordTran.DORotate(endValue, num, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.OutQuad).SetLink(this.skillSwordTran.gameObject);
			float progress = 0f;
			Vector3 startPos = this.skillSwordTran.position;
			DOTween.To(() => progress, delegate(float x)
			{
				progress = x;
			}, 1f, num).SetEase(Ease.InQuad).SetTarget(this.skillSwordTran).SetLink(this.skillSwordTran.gameObject).OnUpdate(delegate
			{
				if (this.skillSwordTran == null || this.sword == null)
				{
					return;
				}
				Vector3 a = Vector3.Lerp(startPos, this.sword.position, progress);
				float num2 = 2f;
				float d = Mathf.Sin(progress * 3.1415927f) * num2;
				this.skillSwordTran.position = a + Vector3.up * d;
			}).OnComplete(delegate
			{
				if (this.skillSwordTran == null)
				{
					return;
				}
				this.skillSwordTran.gameObject.SetActive(false);
				string text = ((Dictionary<string, object>)ExcelManager.allExcelData["passsiveSkill"])[34.ToString()].DIC("value");
				for (int j = 0; j < this.createSwordPos.Count; j++)
				{
					uint syncPassSkillIndex = SkillManager.GetSyncPassSkillIndex();
					if (!string.IsNullOrEmpty(text))
					{
						float[] skillValues = Array.ConvertAll<string, float>(text.Split('|', StringSplitOptions.None), new Converter<string, float>(float.Parse));
						this.enemyBase.CmdAddBrotatoWeapon(BrotatoWeaponType.NecromancerSword, syncPassSkillIndex, skillValues, 0);
					}
				}
			});
			BrotatoWeaponData getBrotatoWeaponData = Util.GetSOBrotatoWeaponConfig().GetBrotatoWeaponData(BrotatoWeaponType.NecromancerSword);
			for (int i = 0; i < this.createSwordPos.Count; i++)
			{
				Vector3 createtPos = this.createSwordPos[i];
				createtPos.y = 2f;
				Transform createEffect = Game.EffectManager.PlayEffect(EffectDefine.NecromancerSword, num, createtPos, 1f);
				if (!(createEffect == null))
				{
					createEffect.DOKill(false);
					int index = i;
					float effectProgress = 0f;
					DOTween.To(() => effectProgress, delegate(float x)
					{
						effectProgress = x;
					}, 1f, num).SetEase(Ease.InQuad).SetTarget(createEffect).SetLink(createEffect.gameObject).OnUpdate(delegate
					{
						if (createEffect == null || this.enemyBase == null)
						{
							return;
						}
						Vector3 weaponPos = BrotatoWeapon.GetWeaponPos(getBrotatoWeaponData, this.createSwordPos.Count, index, BrotatoShootType.Melee, this.enemyBase, null);
						Vector3 a = Vector3.Lerp(createtPos, weaponPos, effectProgress);
						float num2 = 2f;
						float d = Mathf.Sin(effectProgress * 3.1415927f) * num2;
						createEffect.position = a + Vector3.up * d;
					});
				}
			}
		}
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00041640 File Offset: 0x0003F840
	public override void UpdateSkill2()
	{
		base.UpdateSkill2();
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 1.2f / this.enemyBase.AniSpeed)
		{
			this.enemyBase.UpdateRoleState(RoleState.Idle);
			return;
		}
		if (this.enemyBase.timer > 1.2f / this.enemyBase.AniSpeed * 0.38f && !this.enemyBase.isCheckAttack)
		{
			this.enemyBase.isCheckAttack = true;
		}
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x000416E4 File Offset: 0x0003F8E4
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
		int count = attackRoles.Count;
		Vector3 position = hurtrole.MyTransform.position;
		bool isAttackWeek = this.roleBase.GetIsAttackWeek(AttackType.AttackEffect);
		float num = 1f;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && roleBase != hurtrole && Util.NewCheckYuanXing(position, roleBase.MyTransform.position, num * 2.5f + roleBase.RoleModeBase.addRange, false))
			{
				roleBase.OnHit(attackrole, (double)damage, Util.GetV2Angle(roleBase.MyTransform.position, this.roleBase.MyTransform.position), AttackType.AttackEffect, isAttackWeek);
			}
		}
		GameHelperClient.localPlayer.CmdPlayEffect(EffectDefine.SwordImpactEpicGold, 1f, position, num * 0.833f);
		return damage;
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x000417E8 File Offset: 0x0003F9E8
	public override void OnStartDead()
	{
		base.OnStartDead();
		this.brotatoWeaponController.ClearBrotatoWeapons();
		this.KillSwordTweens();
		if (this.skillSwordTran != null)
		{
			this.skillSwordTran.gameObject.SetActive(false);
		}
		List<RoleBase> friendRoles = this.roleBase.GetFriendRoles();
		this.createSwordPos.Clear();
		if (this.enemyBase.hasAuthority)
		{
			for (int i = friendRoles.Count - 1; i > -1; i--)
			{
				RoleBase roleBase = friendRoles[i];
				if (roleBase != null && (long)roleBase.FatherId == (long)((ulong)this.roleBase.netId))
				{
					EnemyBase enemyBase = roleBase as EnemyBase;
					if (enemyBase != null)
					{
						enemyBase.CmdAutoDead();
					}
				}
			}
		}
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00041898 File Offset: 0x0003FA98
	private void KillSwordTweens()
	{
		DOTween.Kill(this, false);
		if (this.skillSwordTran != null)
		{
			this.skillSwordTran.DOKill(false);
		}
	}

	// Token: 0x04000CB3 RID: 3251
	[SerializeField]
	private Transform sword;

	// Token: 0x04000CB4 RID: 3252
	private Transform skillSwordTran;

	// Token: 0x04000CB5 RID: 3253
	private float skillEndTime;

	// Token: 0x04000CB6 RID: 3254
	private BrotatoWeaponController brotatoWeaponController = new BrotatoWeaponController();

	// Token: 0x04000CB7 RID: 3255
	private List<Vector3> createSwordPos = new List<Vector3>();
}
