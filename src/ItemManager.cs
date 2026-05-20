using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000144 RID: 324
public class ItemManager : IUpdate
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600063F RID: 1599 RVA: 0x00025EA0 File Offset: 0x000240A0
	public ItemStruct CurPickItemStruct
	{
		get
		{
			return this.curPickItemStruct;
		}
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x00025EA8 File Offset: 0x000240A8
	public void AddItem(ItemStruct itemStruct)
	{
		this.itemStructs.Add(itemStruct.id, itemStruct);
		Game.AudioManager.PlayDropAudio(itemStruct.pos);
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00025ECC File Offset: 0x000240CC
	public void AddItemList(ItemStruct[] itemStructs, Vector3 dropPos)
	{
		int i = 0;
		int num = itemStructs.Length;
		while (i < num)
		{
			this.AddItem(itemStructs[i]);
			i++;
		}
		Game.AudioManager.PlayDropAudio(dropPos);
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x00025F00 File Offset: 0x00024100
	public void RemoveItem(uint id)
	{
		ItemStruct itemStruct;
		if (this.itemStructs.TryGetValue(id, out itemStruct))
		{
			if (itemStruct.model != null)
			{
				AssetManager.UnLoadPrefab(itemStruct.model, false);
			}
			if (itemStruct.effect != null)
			{
				AssetManager.UnLoadPrefab(itemStruct.effect, false);
			}
			itemStruct.modelTransform = null;
			itemStruct.model = null;
			itemStruct.effect = null;
			this.itemStructs.Remove(id);
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00025F74 File Offset: 0x00024174
	public void ClearAllItems()
	{
		foreach (ItemStruct itemStruct in this.itemStructs.Values)
		{
			if (itemStruct.model != null)
			{
				AssetManager.UnLoadPrefab(itemStruct.model, false);
			}
			if (itemStruct.effect != null)
			{
				AssetManager.UnLoadPrefab(itemStruct.effect, false);
			}
			itemStruct.modelTransform = null;
			itemStruct.model = null;
		}
		this.itemStructs.Clear();
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00026014 File Offset: 0x00024214
	public void ClearAllTalismans()
	{
		for (int i = this.itemStructs.Count - 1; i > -1; i--)
		{
			ItemStruct value = this.itemStructs.ElementAt(i).Value;
			if (Util.IsTalisman(value.itemType) && value.itemType != ItemType.Talisman_Experience)
			{
				if (ItemManager.CanLocalPlayerPickItem(value) && (value.authorityId == GameHelperClient.localPlayer.netId || GameHelperClient.isHost))
				{
					GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.AllBook, value.pos);
				}
				if (GameHelperClient.localPlayer.GetDistanceV2(value.pos) < 35f)
				{
					Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, value.pos, 1f);
				}
				if (value.model != null)
				{
					AssetManager.UnLoadPrefab(value.model, false);
				}
				if (value.effect != null)
				{
					AssetManager.UnLoadPrefab(value.effect, false);
				}
				value.modelTransform = null;
				value.model = null;
				value.effect = null;
				this.itemStructs.Remove(value.id);
			}
		}
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0002613C File Offset: 0x0002433C
	public void Update()
	{
		ItemStruct itemStruct;
		if (this.curPickItemStruct == null)
		{
			this.minPickDistance = 3f;
		}
		else if (this.itemStructs.TryGetValue(this.curPickItemStruct.id, out itemStruct))
		{
			if (GameHelperClient.localPlayer.GetDistanceV2(itemStruct.pos) > 3f)
			{
				this.curPickItemStruct = null;
				this.minPickDistance = 3f;
			}
		}
		else
		{
			this.curPickItemStruct = null;
			this.minPickDistance = 3f;
		}
		if (GameHelperClient.localPlayer == null)
		{
			return;
		}
		int count = this.itemStructs.Count;
		if (count == 0)
		{
			return;
		}
		if (this.updateIndex >= count)
		{
			this.updateIndex = 0;
		}
		Dictionary<ItemType, ItemData> itemDataDic = Game.GameData.ItemDataDic;
		ItemStruct value = this.itemStructs.ElementAt(this.updateIndex).Value;
		float distanceV = GameHelperClient.localPlayer.GetDistanceV2(value.pos);
		if (distanceV < 22f)
		{
			if (value.model == null)
			{
				ItemData itemData;
				if (itemDataDic.TryGetValue(value.itemType, out itemData))
				{
					value.model = AssetManager.LoadPrefab(PathDefine.Concat("Item/", itemData.model), value.pos);
					value.effect = AssetManager.LoadPrefab(ColorDefine.QuaEffect[itemData.quality], value.pos);
					value.effect.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				value.modelTransform = value.model.transform;
			}
			else if (!value.model.activeSelf)
			{
				value.model.SetActive(true);
			}
			if (distanceV < this.minPickDistance)
			{
				this.minPickDistance = distanceV;
				this.curPickItemStruct = value;
			}
		}
		else if (value.model != null && value.model.activeSelf)
		{
			value.model.SetActive(false);
		}
		this.updateIndex++;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x00026325 File Offset: 0x00024525
	public static uint GetItemId()
	{
		ItemManager.itemId += 1U;
		return ItemManager.itemId;
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x00026338 File Offset: 0x00024538
	public static bool CanLocalPlayerPickItem(ItemStruct itemStruct)
	{
		return ItemManager.CanPlayerPickItem(GameHelperClient.localPlayer, itemStruct);
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00026348 File Offset: 0x00024548
	public static bool CanPlayerPickItem(PlayerBase pickPlayer, ItemStruct itemStruct)
	{
		if (pickPlayer == null || itemStruct == null)
		{
			return false;
		}
		if (itemStruct.authorityId == 0U || itemStruct.authorityId == pickPlayer.netId)
		{
			return true;
		}
		PlayerManagerClient playerManagerClient = Game.PlayerManagerClient;
		PlayerBase playerBase;
		return playerManagerClient == null || !playerManagerClient.clientPlayerDic.TryGetValue(itemStruct.authorityId, out playerBase) || playerBase.isPickShare;
	}

	// Token: 0x04000912 RID: 2322
	public static uint itemId;

	// Token: 0x04000913 RID: 2323
	private int updateIndex;

	// Token: 0x04000914 RID: 2324
	public Dictionary<uint, ItemStruct> itemStructs = new Dictionary<uint, ItemStruct>();

	// Token: 0x04000915 RID: 2325
	private float minPickDistance;

	// Token: 0x04000916 RID: 2326
	private ItemStruct curPickItemStruct;

	// Token: 0x04000917 RID: 2327
	private const float PickDistance = 3f;
}
