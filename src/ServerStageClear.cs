using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x020002C1 RID: 705
public class ServerStageClear : ServerStageBase
{
	// Token: 0x0600109B RID: 4251 RVA: 0x0005D40C File Offset: 0x0005B60C
	public override void OnEnter()
	{
		base.OnEnter();
		SOLevelStageData.CreateEnemyData[] createEnemyDatas = this.stageCreate.createEnemyDatas;
		if (createEnemyDatas != null)
		{
			this.stateRoles = new List<RoleBase>();
			int num = createEnemyDatas.Length;
			uint netId = GameHelperClient.localPlayer.netId;
			for (int i = 0; i < num; i++)
			{
				SOLevelStageData.CreateEnemyData createEnemyData = createEnemyDatas[i];
				this.AddEnemy(createEnemyData.enemyType, netId, false, EnemyCreateType.ChallengeAndBOSS, createEnemyData.enemyPos);
			}
		}
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x0005D478 File Offset: 0x0005B678
	private Task AddEnemy(EnemyType enemyType, uint playerId, bool isElite, EnemyCreateType enemyCreateType, Vector3 spawnPos)
	{
		ServerStageClear.<AddEnemy>d__3 <AddEnemy>d__;
		<AddEnemy>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<AddEnemy>d__.<>4__this = this;
		<AddEnemy>d__.enemyType = enemyType;
		<AddEnemy>d__.playerId = playerId;
		<AddEnemy>d__.isElite = isElite;
		<AddEnemy>d__.enemyCreateType = enemyCreateType;
		<AddEnemy>d__.spawnPos = spawnPos;
		<AddEnemy>d__.<>1__state = -1;
		<AddEnemy>d__.<>t__builder.Start<ServerStageClear.<AddEnemy>d__3>(ref <AddEnemy>d__);
		return <AddEnemy>d__.<>t__builder.Task;
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0005D4E8 File Offset: 0x0005B6E8
	private void DieEvent(RoleBase role)
	{
		role.dieEvent = (RoleBase.DieEvent)Delegate.Remove(role.dieEvent, new RoleBase.DieEvent(this.DieEvent));
		this.stateRoles.Remove(role);
		if (this.stateRoles.Count == 0)
		{
			MySystemEvent.Instance.DispatchMessage(44);
		}
	}

	// Token: 0x04000E96 RID: 3734
	public SOLevelStageData.StageCreate stageCreate;

	// Token: 0x04000E97 RID: 3735
	private List<RoleBase> stateRoles;
}
