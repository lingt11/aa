using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A2 RID: 674
public class PlayerSwordMasterMode : MeleePlayerMode
{
	// Token: 0x06000EC9 RID: 3785 RVA: 0x00054854 File Offset: 0x00052A54
	protected override void Awake()
	{
		base.Awake();
		if (this.skillWeaponTransform != null)
		{
			this.weapon = this.attackEffect.transform.parent;
			this.oldLocalPosition = this.weapon.localPosition;
			this.oldlocalRotation = this.weapon.localRotation;
			this.weaponParent = this.weapon.parent;
		}
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x000548C0 File Offset: 0x00052AC0
	public override void OnExitSkill2()
	{
		this.isSkillCheck = false;
		if (this.castEffect.activeSelf)
		{
			this.castEffect.SetActive(false);
			if (this.newTipEffect != null)
			{
				this.newTipEffect.go.SetActive(false);
			}
		}
		if (this.skillWeaponTransform != null)
		{
			this.weapon.SetParent(this.weaponParent);
			this.weapon.localPosition = this.oldLocalPosition;
			this.weapon.localRotation = this.oldlocalRotation;
		}
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x00054948 File Offset: 0x00052B48
	public override void OnStartSkill2()
	{
		if (this.playerBase.hasAuthority)
		{
			this.playerBase.UpdateSkillData(-1f);
			if (this.newTipEffect == null)
			{
				GameObject gameObject = AssetManager.LoadPrefab(EffectDefine.TipYuanHero, null, true);
				this.newTipEffect = new TipEffect();
				this.newTipEffect.go = gameObject;
				this.newTipEffect.transform = gameObject.transform;
				this.newTipEffect.materialBlock = new MaterialPropertyBlock();
				this.newTipEffect.renderer = gameObject.GetComponent<Renderer>();
				this.newTipEffect.transform.SetParent(this.playerBase.MyTransform);
				this.newTipEffect.transform.localPosition = new Vector3(0f, 0.35f, 0f);
				this.newTipEffect.transform.localScale = Vector3.one * (15f * (1f + this.playerBase.skillRange));
				this.newTipEffect.materialBlock.SetFloat(ShaderDefine.Property_Progress, 0.001f);
				this.newTipEffect.renderer.SetPropertyBlock(this.newTipEffect.materialBlock);
			}
			else
			{
				this.newTipEffect.timer = 0f;
			}
			this.newTipEffect.lifeTime = 2f / (1f + this.playerBase.castSpeed);
		}
		if (!this.castEffect.activeSelf)
		{
			this.castEffect.SetActive(true);
			if (this.newTipEffect != null)
			{
				this.newTipEffect.go.SetActive(true);
			}
		}
		this.myAnim.SetBool(AnimDefine.IsCheck, false);
		this.playerBase.PlayAni(AnimDefine.Skill2, 1f, 0.1f);
		this.playerBase.timer = 0f;
		this.isSkillCheck = false;
		if (this.skillWeaponTransform != null)
		{
			this.weapon.SetParent(this.skillWeaponTransform);
		}
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x00054B44 File Offset: 0x00052D44
	public override void UpdateSkill2()
	{
		float deltaTime = Time.deltaTime;
		if (this.skillWeaponTransform != null)
		{
			if (this.isSkillCheck)
			{
				this.weapon.localPosition = Vector3.Lerp(this.weapon.localPosition, this.oldLocalPosition, deltaTime * 10f);
				this.weapon.localRotation = Quaternion.Lerp(this.weapon.localRotation, this.oldlocalRotation, deltaTime * 10f);
			}
			else if (this.skillWeaponTransform != null)
			{
				this.weapon.localPosition = Vector3.Lerp(this.weapon.localPosition, Vector3.zero, deltaTime * 15f);
				this.weapon.localRotation = Quaternion.Lerp(this.weapon.localRotation, Quaternion.identity, deltaTime * 15f);
			}
		}
		if (this.playerBase.hasAuthority)
		{
			if (this.isSkillCheck)
			{
				this.playerBase.timer += deltaTime;
				if (this.playerBase.timer > 0.733f)
				{
					this.playerBase.UpdateRoleState(RoleState.Idle);
					return;
				}
			}
			else
			{
				if (this.newTipEffect != null)
				{
					this.newTipEffect.timer += deltaTime;
					this.newTipEffect.materialBlock.SetFloat(ShaderDefine.Property_Progress, Mathf.Min(1f, this.newTipEffect.timer / this.newTipEffect.lifeTime));
					this.newTipEffect.renderer.SetPropertyBlock(this.newTipEffect.materialBlock);
				}
				if (this.newTipEffect != null && this.roleBase.roleType == RoleType.King && this.newTipEffect.timer / this.newTipEffect.lifeTime >= 1f)
				{
					this.playerBase.UpdateSkillData(this.newTipEffect.timer);
					this.OnSkillAttack();
					return;
				}
			}
		}
		else if (!this.isSkillCheck && this.playerBase.SyncSkillData > 0f)
		{
			this.OnSkillAttack();
		}
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x00054D48 File Offset: 0x00052F48
	public override void OnSkillKeyUp(int index)
	{
		if (this.playerBase.RoleState == RoleState.Skill2 && !this.isSkillCheck)
		{
			SkillBase activeSkillByKeyIndex = Util.GetActiveSkillByKeyIndex(index);
			if (activeSkillByKeyIndex != null && activeSkillByKeyIndex.activeSkillEnum == ActiveSkillEnum.Hero_DrawKnife)
			{
				this.playerBase.UpdateSkillData(this.newTipEffect.timer);
				this.OnSkillAttack();
			}
		}
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x00054DA0 File Offset: 0x00052FA0
	private void OnSkillAttack()
	{
		this.isSkillCheck = true;
		this.myAnim.SetBool(AnimDefine.IsCheck, true);
		if (this.castEffect.activeSelf)
		{
			this.castEffect.SetActive(false);
			if (this.newTipEffect != null)
			{
				this.newTipEffect.go.SetActive(false);
			}
		}
		if (this.playerBase.hasAuthority && this.newTipEffect != null)
		{
			float num = Mathf.Min(1f, this.newTipEffect.timer / this.newTipEffect.lifeTime) * 7.5f * (1f + this.playerBase.skillRange);
			Game.EffectManager.PlayEffect(EffectDefine.SwordSlash7, 1.5f, this.playerBase.MyTransform.position, num * 0.4f);
			List<RoleBase> attackRoles = this.playerBase.GetAttackRoles();
			int count = attackRoles.Count;
			bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
			int num2 = (int)((float)Util.GetSkillDamage(Game.GameData.ActiveSkillDataDic[ActiveSkillEnum.Hero_DrawKnife], this.playerBase) * (1f + this.playerBase.normalAttackAddDamage));
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.playerBase.MyTransform.position, roleBase.MyTransform.position, num + roleBase.RoleModeBase.addRange, false))
				{
					Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)num2, Util.GetV2Angle(roleBase.MyTransform.position, this.playerBase.MyTransform.position), AttackType.Normal, isAttackWeek);
				}
			}
			if (!this.playerBase.wudi)
			{
				this.playerBase.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", 0.5f);
				this.secretAttackTime = Time.time;
				this.lastRange = num;
				this.isCanSecret = true;
			}
		}
		else
		{
			Game.EffectManager.PlayEffect(EffectDefine.SwordSlash7, 1.5f, this.playerBase.MyTransform.position, 7.5f * Mathf.Min(1f, this.playerBase.SyncSkillData / (2f / (1f + this.playerBase.castSpeed))) * 0.4f * (1f + this.playerBase.skillRange));
		}
		if (this.skillWeaponTransform != null)
		{
			this.weapon.SetParent(this.weaponParent);
		}
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x00055049 File Offset: 0x00053249
	public void OpenSecret(string cardNameValue)
	{
		this.cardName = cardNameValue;
		RoleBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x00055079 File Offset: 0x00053279
	public void CloseSecret()
	{
		RoleBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x000550A4 File Offset: 0x000532A4
	private float DamageEvent(RoleBase attackrole, RoleBase hurtrole, AttackType attackType, ref float f)
	{
		if (this.roleBase.wudi && Time.time < this.secretAttackTime + 0.5f && attackType == AttackType.Skill && this.isCanSecret)
		{
			this.isCanSecret = false;
			this.OnSecretAttack(this.lastRange);
			PlayerSwordMasterMode.SecretAttackClass secretAttackClass = new PlayerSwordMasterMode.SecretAttackClass();
			secretAttackClass.playerSwordMasterMode = this;
			secretAttackClass.lastRange = this.lastRange;
			Game.TimerManager.AddTimer(0.25f, new Action(secretAttackClass.OnCallBack));
			new PlayerSwordMasterMode.SecretAttackClass().playerSwordMasterMode = this;
			secretAttackClass.lastRange = this.lastRange;
			Game.TimerManager.AddTimer(0.5f, new Action(secretAttackClass.OnCallBack));
			if (this.playerBase.isLocalPlayer)
			{
				Util.ShowTipsNoLanguage(string.Format(ColorDefine.NormalColor, this.cardName));
			}
		}
		return f;
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x0005518C File Offset: 0x0005338C
	private void OnSecretAttack(float attackRange)
	{
		if (this.playerBase == null || this.playerBase.MyTransform == null || base.gameObject == null)
		{
			return;
		}
		this.playerBase.CmdPlayEffect(EffectDefine.SwordSlash7, 1.5f, this.playerBase.MyTransform.position, attackRange * 0.4f);
		List<RoleBase> attackRoles = this.playerBase.GetAttackRoles();
		int count = attackRoles.Count;
		bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
		int num = (int)((float)Util.GetSkillDamage(Game.GameData.ActiveSkillDataDic[ActiveSkillEnum.Hero_DrawKnife], this.playerBase) * (1f + this.playerBase.normalAttackAddDamage));
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.playerBase.MyTransform.position, roleBase.MyTransform.position, attackRange + roleBase.RoleModeBase.addRange, false))
			{
				Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)num, Util.GetV2Angle(roleBase.MyTransform.position, this.playerBase.MyTransform.position), AttackType.Normal, isAttackWeek);
			}
		}
	}

	// Token: 0x04000DC2 RID: 3522
	[SerializeField]
	private GameObject castEffect;

	// Token: 0x04000DC3 RID: 3523
	[SerializeField]
	private Transform skillWeaponTransform;

	// Token: 0x04000DC4 RID: 3524
	private TipEffect newTipEffect;

	// Token: 0x04000DC5 RID: 3525
	private const float AttackRange = 7.5f;

	// Token: 0x04000DC6 RID: 3526
	private const float CastTime = 2f;

	// Token: 0x04000DC7 RID: 3527
	private const float WudiTime = 0.5f;

	// Token: 0x04000DC8 RID: 3528
	private Vector3 oldLocalPosition;

	// Token: 0x04000DC9 RID: 3529
	private Quaternion oldlocalRotation;

	// Token: 0x04000DCA RID: 3530
	private Transform weaponParent;

	// Token: 0x04000DCB RID: 3531
	private Transform weapon;

	// Token: 0x04000DCC RID: 3532
	private float secretAttackTime;

	// Token: 0x04000DCD RID: 3533
	private float lastRange;

	// Token: 0x04000DCE RID: 3534
	private bool isCanSecret;

	// Token: 0x04000DCF RID: 3535
	private string cardName;

	// Token: 0x04000DD0 RID: 3536
	private bool isSkillCheck;

	// Token: 0x020002A3 RID: 675
	private class SecretAttackClass
	{
		// Token: 0x06000ED4 RID: 3796 RVA: 0x000552EF File Offset: 0x000534EF
		public void OnCallBack()
		{
			PlayerSwordMasterMode playerSwordMasterMode = this.playerSwordMasterMode;
			if (playerSwordMasterMode == null)
			{
				return;
			}
			playerSwordMasterMode.OnSecretAttack(this.lastRange);
		}

		// Token: 0x04000DD1 RID: 3537
		public PlayerSwordMasterMode playerSwordMasterMode;

		// Token: 0x04000DD2 RID: 3538
		public float lastRange;
	}
}
