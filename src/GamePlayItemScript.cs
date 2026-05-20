using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class GamePlayItemScript : MonoBehaviour
{
	// Token: 0x0600059E RID: 1438 RVA: 0x00020A54 File Offset: 0x0001EC54
	private void Awake()
	{
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			GameHelperClient.AddGamePlayItem(new GamePlayItemData
			{
				gamePlayItemType = GamePlayItemType.Help,
				pos = child.transform.position
			});
		}
		Object.Destroy(this);
	}
}
