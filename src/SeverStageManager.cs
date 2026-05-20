using System;
using System.Collections.Generic;
using Mirror;

// Token: 0x020002C3 RID: 707
public class SeverStageManager
{
	// Token: 0x060010A1 RID: 4257 RVA: 0x0005D724 File Offset: 0x0005B924
	public void Init()
	{
		MySystemEvent.Instance.RegisterMessage(44, new Action<Body>(this.OnNextStage));
		SOLevelStageData.StageCreate[] array = GameHelperClient.spawnConfig.soLevelStageData.stages;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			SOLevelStageData.StageCreate stageCreate = array[i];
			ServerStageBase serverStageBase = null;
			if (stageCreate.stageCompCondition == StageCompCondition.Clear)
			{
				serverStageBase = new ServerStageClear();
				((ServerStageClear)serverStageBase).stageCreate = stageCreate;
			}
			this.stages.Add(serverStageBase);
		}
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0005D79D File Offset: 0x0005B99D
	private void Clear()
	{
		MySystemEvent.Instance.UnregisterMessage(44, new Action<Body>(this.OnNextStage));
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0005D7B7 File Offset: 0x0005B9B7
	public void OnUpdate()
	{
		if (this.stages.Count == 0)
		{
			return;
		}
		this.stages[0].OnUpdate();
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0005D7D8 File Offset: 0x0005B9D8
	public void OnStart()
	{
		if (this.stages.Count == 0)
		{
			return;
		}
		this.stages[0].OnEnter();
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x0005D7FC File Offset: 0x0005B9FC
	private void OnNextStage(Body body)
	{
		this.stages[0].OnExit();
		this.stages.RemoveAt(0);
		if (this.stages.Count == 0)
		{
			this.OnGameOver();
			return;
		}
		(NetworkManager.singleton as MyServerNetworkManager).ServerSendAllPlayer(new ClientNetMessage
		{
			clientNetOperation = ClientNetOperation.OnStartRest,
			datas = new int[]
			{
				GameHelperClient.WaveNum,
				1
			}
		});
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x0005D874 File Offset: 0x0005BA74
	private void OnGameOver()
	{
		this.Clear();
	}

	// Token: 0x04000EA1 RID: 3745
	private List<ServerStageBase> stages = new List<ServerStageBase>();
}
