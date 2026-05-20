using System;
using System.Collections.Generic;
using System.Linq;
using RVO;
using UnityEngine;

// Token: 0x02000131 RID: 305
public class EnemyManagerClient : IUpdate
{
	// Token: 0x060005C9 RID: 1481 RVA: 0x0002250C File Offset: 0x0002070C
	public EnemyManagerClient()
	{
		Simulator.Instance.setTimeStep(0.1f);
		Simulator.Instance.setAgentDefaults(1f, 10, 5f, 5f, 0.5f, 5f, new RVO.Vector2(0f, 0f));
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0002258E File Offset: 0x0002078E
	public void AddEnemy(RoleBase role)
	{
		if (!this.clientEnemyList.Contains(role))
		{
			this.clientEnemyList.Add(role);
			this.CreatAgent(role);
		}
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x000225B1 File Offset: 0x000207B1
	public void AddEnemyNoAgent(RoleBase role)
	{
		if (!this.clientEnemyList.Contains(role))
		{
			this.clientEnemyList.Add(role);
		}
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x000225CD File Offset: 0x000207CD
	public void RemoveEnemy(RoleBase role)
	{
		this.DeleteAgent(role);
		role.OnRemove();
		this.clientEnemyList.Remove(role);
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x000225EC File Offset: 0x000207EC
	public List<RoleBase> GetRangeEnemy(float distance, Vector3 pos)
	{
		List<RoleBase> list = new List<RoleBase>();
		foreach (RoleBase roleBase in this.clientEnemyList)
		{
			if (roleBase != null && roleBase.MyTransform != null)
			{
				float num = Vector3.Distance(roleBase.MyTransform.position, pos);
				RoleModeBase roleModeBase = roleBase.RoleModeBase;
				float? num2 = distance + ((roleModeBase != null) ? new float?(roleModeBase.addRange) : null);
				if (num <= num2.GetValueOrDefault() & num2 != null)
				{
					list.Add(roleBase);
				}
			}
		}
		return list;
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x000226D8 File Offset: 0x000208D8
	public void Update()
	{
		if (GameHelperClient.isGameOver || GameHelperClient.localPlayer == null)
		{
			return;
		}
		Vector3 position = GameHelperClient.localPlayer.MyTransform.position;
		float deltaTime = Time.deltaTime;
		for (int i = this.clientEnemyList.Count - 1; i > -1; i--)
		{
			RoleBase roleBase = this.clientEnemyList[i];
			if (roleBase == null)
			{
				this.clientEnemyList.RemoveAt(i);
			}
			else
			{
				if (roleBase.hasAuthority && GameHelperClient.isReady && GameHelperClient.isWin)
				{
					roleBase.UpdateRoleState(RoleState.Dead);
				}
				roleBase.UpdateEvent();
				roleBase.UpdateEmit(deltaTime);
				EnemyBase enemyBase = roleBase as EnemyBase;
				if (enemyBase != null && !enemyBase.isBoss)
				{
					if (Util.GetV2Distance(position, roleBase.MyTransform.position) > 22f)
					{
						roleBase.HideMode();
					}
					else
					{
						roleBase.ShowMode();
					}
				}
			}
		}
		for (int j = this.m_agentMap.Count - 1; j > -1; j--)
		{
			KeyValuePair<int, RoleBase> keyValuePair = this.m_agentMap.ElementAt(j);
			if (keyValuePair.Value == null)
			{
				this.m_agentMap.Remove(keyValuePair.Key);
			}
		}
		Simulator.Instance.doStep();
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00022810 File Offset: 0x00020A10
	public void DeleteAgent(RoleBase delRole)
	{
		int sid = delRole.sid;
		RoleBase x;
		if (this.m_agentMap.TryGetValue(sid, out x) && x == delRole)
		{
			Simulator.Instance.delAgent(sid);
			this.m_agentMap.Remove(sid);
			delRole.sid = -1;
		}
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0002285C File Offset: 0x00020A5C
	public void CreatAgent(RoleBase role)
	{
		Vector3 position = role.MyTransform.position;
		int num = Simulator.Instance.addAgent(new RVO.Vector2(position.x, position.z));
		if (num >= 0)
		{
			role.sid = num;
			this.m_agentMap.Add(num, role);
		}
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x000228AC File Offset: 0x00020AAC
	public void OnGameOver(bool isWin)
	{
		this.demonContractCheckTemp.Clear();
		for (int i = this.clientEnemyList.Count - 1; i > -1; i--)
		{
			RoleBase roleBase = this.clientEnemyList[i];
			if (roleBase == null)
			{
				this.clientEnemyList.RemoveAt(i);
			}
			else if (GameHelperClient.isHost)
			{
				if (isWin)
				{
					if (!GameHelperClient.isGameOver && !roleBase.IsDead())
					{
						EnemyBase enemyBase = roleBase as EnemyBase;
						if (enemyBase != null && enemyBase.isBoss && !this.playerDemonContract.Contains(enemyBase.authorityId) && !this.demonContractCheckTemp.Contains(enemyBase.authorityId))
						{
							this.demonContractCheckTemp.Add(enemyBase.authorityId);
						}
					}
					roleBase.ServerUpdateHp(-roleBase.hp);
					roleBase.ServerUpdateState(RoleState.Dead);
				}
				else
				{
					roleBase.ServerUpdateState(RoleState.Idle);
				}
			}
		}
		Game.TimerManager.AddTimer(1f, delegate()
		{
			foreach (uint num in this.demonContractCheckTemp)
			{
				PlayerBase playerBase;
				if (Game.PlayerManagerClient.clientPlayerDic.TryGetValue(num, out playerBase))
				{
					Vector3 pos = GameHelperClient.spawnConfig.playerSpawnPoint[(int)(num - 1U)];
					Vector3 vector = Quaternion.Euler(0f, (float)EnemyManagerClient.DemonContractLookEuler[(int)(num - 1U)], 0f) * Vector3.back * 2f;
					pos.x += vector.x;
					pos.z += vector.z;
					playerBase.TargetCreateDemonContract(pos);
				}
			}
		});
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x000229A8 File Offset: 0x00020BA8
	public void StartKingBattle()
	{
		for (int i = this.clientEnemyList.Count - 1; i > -1; i--)
		{
			RoleBase roleBase = this.clientEnemyList[i];
			if (roleBase == null)
			{
				this.clientEnemyList.RemoveAt(i);
			}
			else if (roleBase.hasAuthority)
			{
				roleBase.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", 2f);
			}
		}
	}

	// Token: 0x04000837 RID: 2103
	private Vector3 spawnPos;

	// Token: 0x04000838 RID: 2104
	public List<RoleBase> clientEnemyList = new List<RoleBase>();

	// Token: 0x04000839 RID: 2105
	private Dictionary<int, RoleBase> m_agentMap = new Dictionary<int, RoleBase>();

	// Token: 0x0400083A RID: 2106
	private List<uint> demonContractCheckTemp = new List<uint>();

	// Token: 0x0400083B RID: 2107
	public static int[] DemonContractLookEuler = new int[]
	{
		135,
		225,
		45,
		315
	};

	// Token: 0x0400083C RID: 2108
	public List<uint> playerDemonContract = new List<uint>();
}
