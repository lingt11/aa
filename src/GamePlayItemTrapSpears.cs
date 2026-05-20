using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000280 RID: 640
public class GamePlayItemTrapSpears : MonoBehaviour
{
	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000BED RID: 3053 RVA: 0x00041B9B File Offset: 0x0003FD9B
	public bool IsOpen
	{
		get
		{
			return this.isOpen;
		}
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x00041BA3 File Offset: 0x0003FDA3
	private void Awake()
	{
		this.myTransform = base.transform;
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x00041BB1 File Offset: 0x0003FDB1
	public void Init(uint skillIdValue)
	{
		this.skillId = skillIdValue;
		this.modelTransform.localPosition = this.startPos;
		this.isMove = false;
		this.isOpen = false;
		this.StartAttack();
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00041BE0 File Offset: 0x0003FDE0
	private void CheckCanAction()
	{
		if (this.CheckRoleListCanAction(Game.PlayerManagerClient.clientPlayerList))
		{
			return;
		}
		if (this.CheckRoleListCanAction(Game.EnemyManagerClient.clientEnemyList))
		{
			return;
		}
		if (Time.time > this.checkCloseTime && this.isOpen)
		{
			GameHelperClient.localPlayer.CmdEndSkillAciton(this.skillId);
		}
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00041C38 File Offset: 0x0003FE38
	private bool CheckRoleListCanAction(List<RoleBase> roleList)
	{
		int count = roleList.Count;
		int i = 0;
		while (i < count)
		{
			RoleBase roleBase = roleList[i];
			Vector3 position = roleBase.MyTransform.position;
			float num = position.y - this.myTransform.position.y;
			if (!roleBase.IsDead() && num > -0.5f && num < 2f && Util.NewCheckJuXing(this.myTransform.position, this.myTransform.localEulerAngles.y, this.attackSize.x, this.attackSize.z, position, 0f, true, false))
			{
				this.checkCloseTime = Time.time + 1f;
				if (!this.isOpen && !this.isMove && Time.time > this.checkCdTime)
				{
					this.checkCdTime = Time.time + 0.5f;
					GameHelperClient.localPlayer.CmdStartSkillAciton(this.skillId);
					return true;
				}
				return true;
			}
			else
			{
				i++;
			}
		}
		return false;
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x00041D3D File Offset: 0x0003FF3D
	public void OnStartTrigger()
	{
		this.isOpen = true;
		this.isMove = true;
		this.openTime = 0f;
		this.checkAttackTime = 1f;
		this.StartAttack();
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x00041D69 File Offset: 0x0003FF69
	public void LocalUpdateEvent()
	{
		this.CheckCanAction();
		if (this.isOpen)
		{
			this.CheckAttack();
		}
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x00041D80 File Offset: 0x0003FF80
	public void UpdateEvent()
	{
		if (this.isOpen)
		{
			float deltaTime = Time.deltaTime;
			if (this.isMove)
			{
				float num = this.modelTransform.localPosition.y;
				this.openTime += deltaTime * 5f;
				num += 2.5f * deltaTime * this.openTime;
				if (num > 0f)
				{
					this.OnMoveEnd();
					return;
				}
				this.modelTransform.localPosition = new Vector3(0f, num, 0f);
				return;
			}
		}
		else if (this.isMove)
		{
			float deltaTime2 = Time.deltaTime;
			float num2 = this.modelTransform.localPosition.y;
			num2 -= 10f * deltaTime2;
			if (num2 < this.startPos.y)
			{
				this.isMove = false;
				this.modelTransform.localPosition = this.startPos;
				return;
			}
			this.modelTransform.localPosition = new Vector3(0f, num2, 0f);
		}
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x00041E72 File Offset: 0x00040072
	private void OnMoveEnd()
	{
		this.isMove = false;
		this.modelTransform.localPosition = Vector3.zero;
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x00041E8B File Offset: 0x0004008B
	private void StartAttack()
	{
		this.checkRoles.Clear();
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x00041E98 File Offset: 0x00040098
	private void CheckAttack()
	{
		float deltaTime = Time.deltaTime;
		this.checkAttackTime -= deltaTime;
		if (this.checkAttackTime < 0f)
		{
			this.StartAttack();
			this.checkAttackTime = 1f;
		}
		this.CheckRoleListAttack(Game.PlayerManagerClient.clientPlayerList);
		this.CheckRoleListAttack(Game.EnemyManagerClient.clientEnemyList);
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00041EF8 File Offset: 0x000400F8
	private void CheckRoleListAttack(List<RoleBase> roleList)
	{
		int count = roleList.Count;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = roleList[i];
			if (!this.checkRoles.Contains(roleBase))
			{
				Vector3 position = roleBase.MyTransform.position;
				float num = position.y - this.myTransform.position.y;
				if (!roleBase.IsDead() && num > -0.5f && num < 2f && Util.NewCheckJuXing(this.myTransform.position, this.myTransform.localEulerAngles.y, this.attackSize.x, this.attackSize.z, position, 0f, true, false))
				{
					roleBase.OnHit(roleBase, (double)((float)roleBase.maxHp * 0.2f), 0f, AttackType.TrueDamage, false);
					this.checkRoles.Add(roleBase);
				}
			}
		}
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x00041FDF File Offset: 0x000401DF
	public void OnCloseUse()
	{
		this.isOpen = false;
		this.isMove = true;
	}

	// Token: 0x04000CC3 RID: 3267
	[SerializeField]
	private Transform modelTransform;

	// Token: 0x04000CC4 RID: 3268
	private Transform myTransform;

	// Token: 0x04000CC5 RID: 3269
	private readonly Vector3 attackSize = new Vector3(2f, 2.2f, 2f);

	// Token: 0x04000CC6 RID: 3270
	private readonly Vector3 attackCenter = new Vector3(0f, 1.1f, 0f);

	// Token: 0x04000CC7 RID: 3271
	private readonly Vector3 startPos = new Vector3(0f, -2f, 0f);

	// Token: 0x04000CC8 RID: 3272
	private bool isMove;

	// Token: 0x04000CC9 RID: 3273
	private bool isOpen;

	// Token: 0x04000CCA RID: 3274
	private float openTime;

	// Token: 0x04000CCB RID: 3275
	private float checkAttackTime;

	// Token: 0x04000CCC RID: 3276
	private List<RoleBase> checkRoles = new List<RoleBase>();

	// Token: 0x04000CCD RID: 3277
	private float checkCdTime;

	// Token: 0x04000CCE RID: 3278
	private float checkCloseTime;

	// Token: 0x04000CCF RID: 3279
	private uint skillId;
}
