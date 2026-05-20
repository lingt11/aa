using System;
using UnityEngine;

// Token: 0x02000405 RID: 1029
public class PrefabSpawner : MonoBehaviour
{
	// Token: 0x06001791 RID: 6033 RVA: 0x00093328 File Offset: 0x00091528
	private void Start()
	{
		this.nameOfThePrefab = this.prefabs[this.index].name;
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x00093328 File Offset: 0x00091528
	private void Update()
	{
		this.nameOfThePrefab = this.prefabs[this.index].name;
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x00093344 File Offset: 0x00091544
	public void SpawnPrefab()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(this.sceneCamera.ScreenPointToRay(Input.mousePosition), out raycastHit))
		{
			Object.Instantiate<GameObject>(this.prefabs[this.index], raycastHit.point, Quaternion.identity);
		}
	}

	// Token: 0x06001794 RID: 6036 RVA: 0x0009338C File Offset: 0x0009158C
	public void ChangePrefabIntex(bool bo)
	{
		if (bo)
		{
			this.index++;
			if (this.index == this.prefabs.Length)
			{
				this.index = 0;
				return;
			}
		}
		else
		{
			this.index--;
			if (this.index == -1)
			{
				this.index = this.prefabs.Length - 1;
			}
		}
	}

	// Token: 0x04001677 RID: 5751
	public GameObject[] prefabs;

	// Token: 0x04001678 RID: 5752
	public Camera sceneCamera;

	// Token: 0x04001679 RID: 5753
	public string nameOfThePrefab;

	// Token: 0x0400167A RID: 5754
	private int index;
}
