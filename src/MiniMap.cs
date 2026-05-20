using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000164 RID: 356
public class MiniMap : MonoBehaviour
{
	// Token: 0x060006F4 RID: 1780 RVA: 0x0002A8C3 File Offset: 0x00028AC3
	private void Start()
	{
		this.playerTransList = EntityStatic.Get<PlayerManagerClient>().clientPlayerList;
		this.enemyTransList = Game.EnemyManagerClient.clientEnemyList;
		this.texture = (this.rawImage.texture as Texture2D);
		this.AlphaImage();
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0002A904 File Offset: 0x00028B04
	private void Update()
	{
		this.time += Time.deltaTime;
		if (this.time <= 0.3f)
		{
			return;
		}
		this.time = 0f;
		this.UpdateRolePositions(this.playerTransList, this.lastPlayerPositions, this.playerColor);
		this.UpdateRolePositions(this.enemyTransList, this.lastEnemyPositions, this.enemyColor);
		this.texture.Apply();
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0002A978 File Offset: 0x00028B78
	private void UpdateRolePositions(List<RoleBase> roleList, Dictionary<RoleBase, Vector2Int> lastPositions, Color color)
	{
		foreach (RoleBase roleBase in roleList)
		{
			Vector2Int vector2Int = new Vector2Int((int)(roleBase.transform.position.x / this.mapWidth * 512f), (int)(roleBase.transform.position.z / this.mapWidth * 512f));
			Vector2Int vector2Int2;
			if (lastPositions.TryGetValue(roleBase, out vector2Int2) && vector2Int2 != vector2Int)
			{
				this.ClearRectangle(this.texture, vector2Int2, this.rectangleWidth, this.rectangleHeight);
			}
			this.DrawRectangle(this.texture, vector2Int, this.rectangleWidth, this.rectangleHeight, color);
			lastPositions[roleBase] = vector2Int;
		}
		List<RoleBase> list = new List<RoleBase>();
		foreach (KeyValuePair<RoleBase, Vector2Int> keyValuePair in lastPositions)
		{
			if (!roleList.Contains(keyValuePair.Key))
			{
				this.ClearRectangle(this.texture, keyValuePair.Value, this.rectangleWidth, this.rectangleHeight);
				list.Add(keyValuePair.Key);
			}
		}
		foreach (RoleBase key in list)
		{
			lastPositions.Remove(key);
		}
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0002AB14 File Offset: 0x00028D14
	private void ClearRectangle(Texture2D texture, Vector2Int startPixel, int width, int height)
	{
		Color color = new Color(1f, 1f, 1f, 0f);
		for (int i = startPixel.y; i < startPixel.y + height; i++)
		{
			for (int j = startPixel.x; j < startPixel.x + width; j++)
			{
				if (j >= 0 && j < texture.width && i >= 0 && i < texture.height)
				{
					texture.SetPixel(j, i, color);
				}
			}
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0002AB94 File Offset: 0x00028D94
	private void DrawRectangle(Texture2D texture, Vector2Int startPixel, int width, int height, Color color)
	{
		for (int i = startPixel.y; i < startPixel.y + height; i++)
		{
			for (int j = startPixel.x; j < startPixel.x + width; j++)
			{
				if (j >= 0 && j < texture.width && i >= 0 && i < texture.height)
				{
					texture.SetPixel(j, i, color);
				}
			}
		}
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0002ABFC File Offset: 0x00028DFC
	private void AlphaImage()
	{
		Color[] array = new Color[262144];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new Color(1f, 1f, 1f, 0f);
		}
		this.texture.SetPixels(array);
	}

	// Token: 0x04000B05 RID: 2821
	public RawImage rawImage;

	// Token: 0x04000B06 RID: 2822
	public Vector2Int rolePos;

	// Token: 0x04000B07 RID: 2823
	public int rectangleWidth = 20;

	// Token: 0x04000B08 RID: 2824
	public int rectangleHeight = 20;

	// Token: 0x04000B09 RID: 2825
	public Color playerColor = Color.blue;

	// Token: 0x04000B0A RID: 2826
	public Color enemyColor = Color.red;

	// Token: 0x04000B0B RID: 2827
	public float mapWidth = 100f;

	// Token: 0x04000B0C RID: 2828
	private Texture2D texture;

	// Token: 0x04000B0D RID: 2829
	public List<RoleBase> playerTransList = new List<RoleBase>(4);

	// Token: 0x04000B0E RID: 2830
	public List<RoleBase> enemyTransList = new List<RoleBase>(128);

	// Token: 0x04000B0F RID: 2831
	private Dictionary<RoleBase, Vector2Int> lastPlayerPositions = new Dictionary<RoleBase, Vector2Int>();

	// Token: 0x04000B10 RID: 2832
	private Dictionary<RoleBase, Vector2Int> lastEnemyPositions = new Dictionary<RoleBase, Vector2Int>();

	// Token: 0x04000B11 RID: 2833
	private float time;
}
