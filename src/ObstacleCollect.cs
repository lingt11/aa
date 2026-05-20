using System;
using System.Collections.Generic;
using RVO;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public class ObstacleCollect : MonoBehaviour
{
	// Token: 0x060016FC RID: 5884 RVA: 0x0008F6D8 File Offset: 0x0008D8D8
	private void Awake()
	{
		BoxCollider[] componentsInChildren = base.GetComponentsInChildren<BoxCollider>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			float x = componentsInChildren[i].transform.position.x - componentsInChildren[i].size.x * componentsInChildren[i].transform.lossyScale.x * 0.5f;
			float y = componentsInChildren[i].transform.position.z - componentsInChildren[i].size.z * componentsInChildren[i].transform.lossyScale.z * 0.5f;
			float x2 = componentsInChildren[i].transform.position.x + componentsInChildren[i].size.x * componentsInChildren[i].transform.lossyScale.x * 0.5f;
			float y2 = componentsInChildren[i].transform.position.z + componentsInChildren[i].size.z * componentsInChildren[i].transform.lossyScale.z * 0.5f;
			IList<RVO.Vector2> list = new List<RVO.Vector2>();
			list.Add(new RVO.Vector2(x2, y2));
			list.Add(new RVO.Vector2(x, y2));
			list.Add(new RVO.Vector2(x, y));
			list.Add(new RVO.Vector2(x2, y));
			Simulator.Instance.addObstacle(list);
			Simulator.Instance.processObstacles();
		}
		Object.Destroy(base.gameObject);
	}
}
