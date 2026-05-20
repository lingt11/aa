using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000289 RID: 649
public class ClipController : MonoBehaviour
{
	// Token: 0x06000C21 RID: 3105 RVA: 0x000449C0 File Offset: 0x00042BC0
	private void Start()
	{
		this.material = base.GetComponent<Image>().material;
		for (int i = 0; i < this.holePositions.Count; i++)
		{
			this.holePositions[i] = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
		}
		this.UpdateShaderProperties();
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00044A2C File Offset: 0x00042C2C
	private void UpdateShaderProperties()
	{
		if (this.material == null)
		{
			return;
		}
		float[] array = new float[ClipController.enemyNum];
		float[] array2 = new float[ClipController.enemyNum];
		this.holePositions.Clear();
		foreach (RoleBase roleBase in this.enemyTransList)
		{
			if (!(roleBase == null) && (roleBase.roleType == RoleType.Player || !roleBase.IsDead()))
			{
				Vector3 position = roleBase.transform.position;
				Vector2 item = new Vector2((position.x + this.mapWidth / 2f) / this.mapWidth, (position.z + this.mapWidth / 2f) / this.mapWidth);
				this.holePositions.Add(item);
			}
		}
		for (int i = 0; i < this.holePositions.Count; i++)
		{
			array[i] = this.holePositions[i].x;
			array2[i] = this.holePositions[i].y;
		}
		this.material.SetFloat(ClipController.HoleSizeX, this.holeSize.x);
		this.material.SetFloat(ClipController.HoleSizeY, this.holeSize.y);
		this.material.SetInt(ClipController.HoleCount, this.holePositions.Count);
		this.material.SetFloatArray(ClipController.HolePositionsX, array);
		this.material.SetFloatArray(ClipController.HolePositionsY, array2);
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x00044BD4 File Offset: 0x00042DD4
	private void Update()
	{
		if (this.material != null)
		{
			this.UpdateShaderProperties();
		}
	}

	// Token: 0x04000CE3 RID: 3299
	[Header("Hole Settings")]
	public float mapWidth = 100f;

	// Token: 0x04000CE4 RID: 3300
	public static int enemyNum = 512;

	// Token: 0x04000CE5 RID: 3301
	public Vector2 holeSize = new Vector2(0.05f, 0.05f);

	// Token: 0x04000CE6 RID: 3302
	public List<Vector2> holePositions = new List<Vector2>(ClipController.enemyNum);

	// Token: 0x04000CE7 RID: 3303
	private Material material;

	// Token: 0x04000CE8 RID: 3304
	private static readonly int HoleSizeX = Shader.PropertyToID("_HoleSizeX");

	// Token: 0x04000CE9 RID: 3305
	private static readonly int HoleSizeY = Shader.PropertyToID("_HoleSizeY");

	// Token: 0x04000CEA RID: 3306
	private static readonly int HoleCount = Shader.PropertyToID("_HoleCount");

	// Token: 0x04000CEB RID: 3307
	private static readonly int HolePositionsX = Shader.PropertyToID("_HolePositionsX");

	// Token: 0x04000CEC RID: 3308
	private static readonly int HolePositionsY = Shader.PropertyToID("_HolePositionsY");

	// Token: 0x04000CED RID: 3309
	public List<RoleBase> enemyTransList = new List<RoleBase>(128);
}
