using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
[CreateAssetMenu(menuName = "ScriptableObject/SOLevelStageData")]
public class SOLevelStageData : ScriptableObject
{
	// Token: 0x040002F3 RID: 755
	public SOLevelStageData.StageCreate[] stages;

	// Token: 0x02000099 RID: 153
	[Serializable]
	public struct StageCreate
	{
		// Token: 0x040002F4 RID: 756
		public StageCompCondition stageCompCondition;

		// Token: 0x040002F5 RID: 757
		public SOLevelStageData.CreateEnemyData[] createEnemyDatas;

		// Token: 0x040002F6 RID: 758
		public SOLevelStageData.CreateSkillData[] createSkillDatas;
	}

	// Token: 0x0200009A RID: 154
	[Serializable]
	public struct CreateEnemyData
	{
		// Token: 0x040002F7 RID: 759
		public EnemyType enemyType;

		// Token: 0x040002F8 RID: 760
		public Vector3 enemyPos;
	}

	// Token: 0x0200009B RID: 155
	[Serializable]
	public struct CreateSkillData
	{
		// Token: 0x040002F9 RID: 761
		public ActiveSkillData activeSkillData;

		// Token: 0x040002FA RID: 762
		public Vector3 skillPos;
	}
}
