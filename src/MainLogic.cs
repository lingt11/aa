using System;
using Mirror;
using Unity.Mathematics;
using UnityEngine;

// Token: 0x02000288 RID: 648
public class MainLogic
{
	// Token: 0x06000C1E RID: 3102 RVA: 0x000446D4 File Offset: 0x000428D4
	public void Init()
	{
		UI_SelectLanguage.ApplySavedFrameRateSetting();
		Debug.Log("框架成功运行");
		Game.UI.OpenUIPanel<UI_StartGame>(null);
		int remainsNum = 0;
		foreach (object obj in Enum.GetValues(typeof(ItemType)))
		{
			ItemType itemType = (ItemType)obj;
			if (itemType >= ItemType.STRBook)
			{
				GameHelperClient.RemainsNum = remainsNum;
				break;
			}
			remainsNum = (int)itemType;
		}
		int num = 0;
		foreach (object obj2 in Enum.GetValues(typeof(ItemType)))
		{
			ItemType itemType2 = (ItemType)obj2;
			if (itemType2 >= ItemType.Talisman_Roar && itemType2 < ItemType.Pick_Sun)
			{
				num++;
			}
		}
		GameHelperClient.TalismanNum = num;
		GameHelperClient.spawnConfig = Resources.Load<SOSpawnConfig>("Bundles/SO/SOSpawnConfig_" + GameHelperClient.MapLevel.ToString());
		GameHelperClient.gameConfig = Resources.Load<SOGameConfig>("Bundles/SO/SOGameConfig");
		Transform myTransform = EntityStatic.Get<CameraManager>().MyTransform;
		myTransform.position = new Vector3(2.704f, -3.976f, 68.78f);
		myTransform.rotation = quaternion.identity;
		EntityStatic.Get<LobbyManager>().LoadLobbyObject();
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00044838 File Offset: 0x00042A38
	public void GameStart(bool isServer, int level)
	{
		if (isServer)
		{
			NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
			{
				serverNetOperation = ServerNetOperation.EnterDungeon,
				datas = new int[]
				{
					level
				}
			}, 0);
			return;
		}
		Debug.Log("游戏开始,难度 " + level.ToString());
		GameHelperClient.MapLevel = level;
		GameHelperClient.spawnConfig = Resources.Load<SOSpawnConfig>("Bundles/SO/SOSpawnConfig_" + GameHelperClient.MapLevel.ToString());
		GameHelperClient.gameConfig = Resources.Load<SOGameConfig>("Bundles/SO/SOGameConfig");
		Game.GameData.InitEnemySpawn();
		Game.GameData.InitEnemy();
		Game.UI.BackUIPanel();
		Game.UI.BackUIPanel();
		EntityStatic.Get<LobbyManager>().UnLoadLobbyObject();
		Transform myTransform = EntityStatic.Get<CameraManager>().MyTransform;
		GameHelperClient.PlayerCameraEuler = new Vector3(55f, 0f, 0f);
		myTransform.position = new Vector3(100f, 10f, 94f);
		myTransform.eulerAngles = new Vector3(55f, 0f, 0f);
		Game.UI.CloseUI<UI_MyCard>();
		Game.UI.OpenUI<UI_SelectHero>(null);
		GameHelperClient.ScenePath = GameHelperClient.spawnConfig.ScenePath;
		GameHelperClient.CanSpellArea = GameHelperClient.spawnConfig.CanSpellArea;
		GameHelperClient.NoSpellArea = GameHelperClient.spawnConfig.NoSpellArea;
		AssetManager.LoadPrefab(GameHelperClient.ScenePath, null, true);
		AssetManager.LoadPrefab("Scenes/SelectHero", null, true);
		NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
		{
			serverNetOperation = ServerNetOperation.CreatePlayer
		}, 0);
	}
}
