using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002BA RID: 698
public class RoleModeBase : MonoBehaviour
{
	// Token: 0x0600106C RID: 4204 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void UpdateEvent()
	{
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void AttackUpdate()
	{
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x0005C618 File Offset: 0x0005A818
	public virtual void OnStartAttack()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.AttackAry[Random.Range(0, this.attackAniNum)], this.roleBase.syncAttackSpeed, 0.1f);
		this.roleBase.isCheckAttack = false;
		if (this.roleBase.hasAuthority)
		{
			RoleBase.OnStartAttackEvent onStartAttackEvent = this.roleBase.onStartAttackEvent;
			if (onStartAttackEvent == null)
			{
				return;
			}
			onStartAttackEvent(this.roleBase.trackRoleBase, this.roleBase.GetRealAttackOffset());
		}
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnExitAttack()
	{
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnExitDead()
	{
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x0005C6A8 File Offset: 0x0005A8A8
	public virtual void OnStartSkill()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill, 1.5f, 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x0005C700 File Offset: 0x0005A900
	public virtual void OnStartSkill2()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill2, 1.5f, 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnStartSkill3()
	{
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnExitSkill2()
	{
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnExitSkill3()
	{
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void UpdateSkill1()
	{
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void UpdateSkill2()
	{
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void UpdateSkill3()
	{
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0005C757 File Offset: 0x0005A957
	public virtual void OnStartIdle()
	{
		this.roleBase.PlayAni(AnimDefine.Idle, 1f, 0.1f);
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnExitSkill()
	{
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0005C773 File Offset: 0x0005A973
	protected virtual void Awake()
	{
		this.myAnim = base.GetComponent<Animator>();
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnClearMode()
	{
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x0005C781 File Offset: 0x0005A981
	public virtual void OnStartDead()
	{
		this.roleBase.PlayAni(AnimDefine.Dead, 1f, 0.1f);
		Game.AudioManager.PlayDeadAudio(this.deadSound, this.roleBase.MyTransform.position);
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnInitMode()
	{
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x0005C7C0 File Offset: 0x0005A9C0
	public virtual void UpdateShowPose()
	{
		float deltaTime = Time.deltaTime;
		this.roleBase.timer += deltaTime;
		Vector3 a = new Vector3(0f, -this.roleBase.animTransform.localScale.y / this.baseModeScale.y * this.headUIHeight, 0f);
		EnemyModeBase enemyModeBase = this as EnemyModeBase;
		if (enemyModeBase != null && enemyModeBase.isPlayShowPoseAnim)
		{
			a.y /= 2f;
		}
		this.roleBase.animTransform.localPosition = Vector3.Lerp(a, Vector3.zero, Mathf.Min(1f, this.roleBase.timer));
		if (this.roleBase.hasAuthority && this.roleBase.timer > 1.2f)
		{
			this.roleBase.UpdateRoleState(RoleState.Idle);
		}
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x0005C8A0 File Offset: 0x0005AAA0
	public virtual void OnStartShowPose()
	{
		this.roleBase.timer = 0f;
		EnemyModeBase enemyModeBase = this as EnemyModeBase;
		bool flag = enemyModeBase != null && enemyModeBase.isPlayShowPoseAnim;
		this.roleBase.PlayAni(flag ? AnimDefine.ShowPose : AnimDefine.Idle, 1f, 0.1f);
		Vector3 localPosition = new Vector3(0f, -this.roleBase.animTransform.localScale.y / this.baseModeScale.y * this.headUIHeight, 0f);
		if (flag)
		{
			localPosition.y /= 2f;
		}
		this.roleBase.animTransform.localPosition = localPosition;
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x00039E1B File Offset: 0x0003801B
	public virtual void OnExitShowPose()
	{
		this.roleBase.animTransform.localPosition = Vector3.zero;
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void UpdateDead()
	{
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0005C953 File Offset: 0x0005AB53
	public float GetAttackDistance()
	{
		return this.attackDistance + this.roleBase.exAttackDistance;
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0005C967 File Offset: 0x0005AB67
	public void UpdateBaseAttackDistance(float value)
	{
		this.attackDistance = value;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnRemove()
	{
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void OnUpdateModeData(int value)
	{
	}

	// Token: 0x04000E67 RID: 3687
	[HideInInspector]
	public bool canAttack = true;

	// Token: 0x04000E68 RID: 3688
	[Header("体型")]
	public float addRange;

	// Token: 0x04000E69 RID: 3689
	public float headUIHeight = 2f;

	// Token: 0x04000E6A RID: 3690
	[Header("材质")]
	public List<Renderer> myRenderers;

	// Token: 0x04000E6B RID: 3691
	public string[] materialList;

	// Token: 0x04000E6C RID: 3692
	[Header("音效")]
	public DeadSound deadSound;

	// Token: 0x04000E6D RID: 3693
	public SkillSoundType skillSoundType;

	// Token: 0x04000E6E RID: 3694
	[Header("攻击参数")]
	public float attackOffset = 1.333f;

	// Token: 0x04000E6F RID: 3695
	[SerializeField]
	private float attackDistance = 2.2f;

	// Token: 0x04000E70 RID: 3696
	public int attackAniNum = 1;

	// Token: 0x04000E71 RID: 3697
	[HideInInspector]
	public Animator myAnim;

	// Token: 0x04000E72 RID: 3698
	[HideInInspector]
	public RoleBase roleBase;

	// Token: 0x04000E73 RID: 3699
	[HideInInspector]
	public Vector3 baseModeScale;
}
