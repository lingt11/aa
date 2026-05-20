using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000291 RID: 657
public class PlayerBardMode : MeleePlayerMode
{
	// Token: 0x06000C9F RID: 3231 RVA: 0x00049CD4 File Offset: 0x00047ED4
	protected override void Awake()
	{
		base.Awake();
		if (this.skillWeaponTransform != null)
		{
			this.oldLocalPosition = this.weapon.localPosition;
			this.oldlocalRotation = this.weapon.localRotation;
			this.weaponParent = this.weapon.parent;
		}
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x00049D28 File Offset: 0x00047F28
	public override void OnExitSkill2()
	{
		if (this.castEffect.activeSelf)
		{
			this.castEffect.SetActive(false);
		}
		if (this.playerBase.isLocalPlayer)
		{
			UI_QTEMusic ui = Game.UI.GetUI<UI_QTEMusic>();
			if (ui.IsOpen())
			{
				if (ui.IsSussess())
				{
					Util.ShowTips("tip_goodMusic");
					this.OnSkillAttack();
				}
				else
				{
					Util.ShowTips("tip_badMusic");
				}
				Game.UI.CloseUI<UI_QTEMusic>();
			}
		}
		else if (this.playerBase.roleType == RoleType.King)
		{
			this.OnSkillAttack();
		}
		if (this.skillWeaponTransform != null)
		{
			this.weapon.SetParent(this.weaponParent);
			this.weapon.localPosition = this.oldLocalPosition;
			this.weapon.localRotation = this.oldlocalRotation;
		}
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x00049DF4 File Offset: 0x00047FF4
	public override void OnStartSkill2()
	{
		if (this.playerBase.isLocalPlayer)
		{
			Game.UI.OpenUI<UI_QTEMusic>(null);
		}
		if (!this.castEffect.activeSelf)
		{
			this.castEffect.SetActive(true);
		}
		this.playerBase.timer = 0f;
		this.playerBase.isCheckAttack = false;
		this.playerBase.PlayAni(AnimDefine.Skill2, 1f, 0.1f);
		if (this.skillWeaponTransform != null)
		{
			this.weapon.SetParent(this.skillWeaponTransform);
		}
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00049E88 File Offset: 0x00048088
	public override void UpdateSkill2()
	{
		float deltaTime = Time.deltaTime;
		if (this.skillWeaponTransform != null)
		{
			this.weapon.localPosition = Vector3.Lerp(this.weapon.localPosition, Vector3.zero, deltaTime * 35f);
			this.weapon.localRotation = Quaternion.Lerp(this.weapon.localRotation, Quaternion.identity, deltaTime * 35f);
		}
		if (this.playerBase.hasAuthority)
		{
			this.playerBase.timer += deltaTime;
			if (this.playerBase.timer > 3.711f)
			{
				this.playerBase.UpdateRoleState(RoleState.Idle);
			}
		}
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x00049F38 File Offset: 0x00048138
	private void OnSkillAttack()
	{
		this.playerBase.isCheckAttack = true;
		if (this.castEffect.activeSelf)
		{
			this.castEffect.SetActive(false);
		}
		if (this.playerBase.hasAuthority)
		{
			this.playerBase.CmdAddMusicBuff(this.playerBase.MyTransform.position);
			this.playerBase.CmdPlayEffect(EffectDefine.YinYueBaoFa, 1.5f, this.playerBase.MyTransform.position + new Vector3(0f, 1.5f, 0f), 2f);
			List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
			int count = attackRoles.Count;
			bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Skill);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.playerBase.MyTransform.position, roleBase.MyTransform.position, 5f + roleBase.RoleModeBase.addRange, false))
				{
					long skillDamage = Util.GetSkillDamage(Game.GameData.ActiveSkillDataDic[ActiveSkillEnum.Hero_PlayMusic], this.playerBase);
					Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.playerBase.MyTransform.position), AttackType.Skill, isAttackWeek);
				}
			}
		}
	}

	// Token: 0x04000D20 RID: 3360
	[SerializeField]
	private GameObject castEffect;

	// Token: 0x04000D21 RID: 3361
	private const float AttackRange = 5f;

	// Token: 0x04000D22 RID: 3362
	[SerializeField]
	private Transform skillWeaponTransform;

	// Token: 0x04000D23 RID: 3363
	[SerializeField]
	private Transform weapon;

	// Token: 0x04000D24 RID: 3364
	private Vector3 oldLocalPosition;

	// Token: 0x04000D25 RID: 3365
	private Quaternion oldlocalRotation;

	// Token: 0x04000D26 RID: 3366
	private Transform weaponParent;
}
