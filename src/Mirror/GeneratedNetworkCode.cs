using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Mirror
{
	// Token: 0x020004CA RID: 1226
	[StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
	public static class GeneratedNetworkCode
	{
		// Token: 0x06001B00 RID: 6912 RVA: 0x000A72F0 File Offset: 0x000A54F0
		public static ReadyMessage ReadyMessage(NetworkReader reader)
		{
			return default(ReadyMessage);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000A7308 File Offset: 0x000A5508
		public static void ReadyMessage(NetworkWriter writer, ReadyMessage value)
		{
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000A7318 File Offset: 0x000A5518
		public static NotReadyMessage NotReadyMessage(NetworkReader reader)
		{
			return default(NotReadyMessage);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x000A7330 File Offset: 0x000A5530
		public static void NotReadyMessage(NetworkWriter writer, NotReadyMessage value)
		{
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x000A7340 File Offset: 0x000A5540
		public static AddPlayerMessage AddPlayerMessage(NetworkReader reader)
		{
			return default(AddPlayerMessage);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x000A7358 File Offset: 0x000A5558
		public static void AddPlayerMessage(NetworkWriter writer, AddPlayerMessage value)
		{
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000A7368 File Offset: 0x000A5568
		public static SceneMessage SceneMessage(NetworkReader reader)
		{
			return new SceneMessage
			{
				sceneName = reader.ReadString(),
				sceneOperation = GeneratedNetworkCode._Read_Mirror.SceneOperation(reader),
				customHandling = reader.ReadBool()
			};
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000A73B0 File Offset: 0x000A55B0
		public static SceneOperation SceneOperation(NetworkReader reader)
		{
			return (SceneOperation)reader.ReadByte();
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x000A73C4 File Offset: 0x000A55C4
		public static void SceneMessage(NetworkWriter writer, SceneMessage value)
		{
			writer.WriteString(value.sceneName);
			GeneratedNetworkCode._Write_Mirror.SceneOperation(writer, value.sceneOperation);
			writer.WriteBool(value.customHandling);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000A73F8 File Offset: 0x000A55F8
		public static void SceneOperation(NetworkWriter writer, SceneOperation value)
		{
			writer.WriteByte((byte)value);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000A740C File Offset: 0x000A560C
		public static CommandMessage CommandMessage(NetworkReader reader)
		{
			return new CommandMessage
			{
				netId = reader.ReadUInt(),
				componentIndex = reader.ReadByte(),
				functionHash = reader.ReadInt(),
				payload = reader.ReadBytesAndSizeSegment()
			};
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000A7460 File Offset: 0x000A5660
		public static void CommandMessage(NetworkWriter writer, CommandMessage value)
		{
			writer.WriteUInt(value.netId);
			writer.WriteByte(value.componentIndex);
			writer.WriteInt(value.functionHash);
			writer.WriteBytesAndSizeSegment(value.payload);
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x000A74A0 File Offset: 0x000A56A0
		public static RpcMessage RpcMessage(NetworkReader reader)
		{
			return new RpcMessage
			{
				netId = reader.ReadUInt(),
				componentIndex = reader.ReadByte(),
				functionHash = reader.ReadInt(),
				payload = reader.ReadBytesAndSizeSegment()
			};
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x000A74F4 File Offset: 0x000A56F4
		public static void RpcMessage(NetworkWriter writer, RpcMessage value)
		{
			writer.WriteUInt(value.netId);
			writer.WriteByte(value.componentIndex);
			writer.WriteInt(value.functionHash);
			writer.WriteBytesAndSizeSegment(value.payload);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x000A7534 File Offset: 0x000A5734
		public static SpawnMessage SpawnMessage(NetworkReader reader)
		{
			return new SpawnMessage
			{
				netId = reader.ReadUInt(),
				isLocalPlayer = reader.ReadBool(),
				isOwner = reader.ReadBool(),
				sceneId = reader.ReadULong(),
				assetId = reader.ReadGuid(),
				position = reader.ReadVector3(),
				rotation = reader.ReadQuaternion(),
				scale = reader.ReadVector3(),
				payload = reader.ReadBytesAndSizeSegment(),
				prefabName = reader.ReadString()
			};
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x000A75E4 File Offset: 0x000A57E4
		public static void SpawnMessage(NetworkWriter writer, SpawnMessage value)
		{
			writer.WriteUInt(value.netId);
			writer.WriteBool(value.isLocalPlayer);
			writer.WriteBool(value.isOwner);
			writer.WriteULong(value.sceneId);
			writer.WriteGuid(value.assetId);
			writer.WriteVector3(value.position);
			writer.WriteQuaternion(value.rotation);
			writer.WriteVector3(value.scale);
			writer.WriteBytesAndSizeSegment(value.payload);
			writer.WriteString(value.prefabName);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000A766C File Offset: 0x000A586C
		public static ChangeOwnerMessage ChangeOwnerMessage(NetworkReader reader)
		{
			return new ChangeOwnerMessage
			{
				netId = reader.ReadUInt(),
				isOwner = reader.ReadBool()
			};
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000A76A4 File Offset: 0x000A58A4
		public static void ChangeOwnerMessage(NetworkWriter writer, ChangeOwnerMessage value)
		{
			writer.WriteUInt(value.netId);
			writer.WriteBool(value.isOwner);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000A76CC File Offset: 0x000A58CC
		public static ObjectSpawnStartedMessage ObjectSpawnStartedMessage(NetworkReader reader)
		{
			return default(ObjectSpawnStartedMessage);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000A76E4 File Offset: 0x000A58E4
		public static void ObjectSpawnStartedMessage(NetworkWriter writer, ObjectSpawnStartedMessage value)
		{
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000A76F4 File Offset: 0x000A58F4
		public static ObjectSpawnFinishedMessage ObjectSpawnFinishedMessage(NetworkReader reader)
		{
			return default(ObjectSpawnFinishedMessage);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000A770C File Offset: 0x000A590C
		public static void ObjectSpawnFinishedMessage(NetworkWriter writer, ObjectSpawnFinishedMessage value)
		{
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000A771C File Offset: 0x000A591C
		public static ObjectDestroyMessage ObjectDestroyMessage(NetworkReader reader)
		{
			return new ObjectDestroyMessage
			{
				netId = reader.ReadUInt()
			};
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000A7744 File Offset: 0x000A5944
		public static void ObjectDestroyMessage(NetworkWriter writer, ObjectDestroyMessage value)
		{
			writer.WriteUInt(value.netId);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000A7760 File Offset: 0x000A5960
		public static ObjectHideMessage ObjectHideMessage(NetworkReader reader)
		{
			return new ObjectHideMessage
			{
				netId = reader.ReadUInt()
			};
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000A7788 File Offset: 0x000A5988
		public static void ObjectHideMessage(NetworkWriter writer, ObjectHideMessage value)
		{
			writer.WriteUInt(value.netId);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x000A77A4 File Offset: 0x000A59A4
		public static EntityStateMessage EntityStateMessage(NetworkReader reader)
		{
			return new EntityStateMessage
			{
				netId = reader.ReadUInt(),
				payload = reader.ReadBytesAndSizeSegment()
			};
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000A77DC File Offset: 0x000A59DC
		public static void EntityStateMessage(NetworkWriter writer, EntityStateMessage value)
		{
			writer.WriteUInt(value.netId);
			writer.WriteBytesAndSizeSegment(value.payload);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000A7804 File Offset: 0x000A5A04
		public static NetworkPingMessage NetworkPingMessage(NetworkReader reader)
		{
			return new NetworkPingMessage
			{
				clientTime = reader.ReadDouble()
			};
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000A782C File Offset: 0x000A5A2C
		public static void NetworkPingMessage(NetworkWriter writer, NetworkPingMessage value)
		{
			writer.WriteDouble(value.clientTime);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000A7848 File Offset: 0x000A5A48
		public static NetworkPongMessage NetworkPongMessage(NetworkReader reader)
		{
			return new NetworkPongMessage
			{
				clientTime = reader.ReadDouble(),
				serverTime = reader.ReadDouble()
			};
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000A7880 File Offset: 0x000A5A80
		public static void NetworkPongMessage(NetworkWriter writer, NetworkPongMessage value)
		{
			writer.WriteDouble(value.clientTime);
			writer.WriteDouble(value.serverTime);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000A78A8 File Offset: 0x000A5AA8
		public static ServerNetMessage _Read_ServerNetMessage(NetworkReader reader)
		{
			return new ServerNetMessage
			{
				serverNetOperation = GeneratedNetworkCode._Read_ServerNetOperation(reader),
				datas = GeneratedNetworkCode._Read_System.Int32[](reader),
				strData = reader.ReadString()
			};
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000A78F0 File Offset: 0x000A5AF0
		public static ServerNetOperation _Read_ServerNetOperation(NetworkReader reader)
		{
			return (ServerNetOperation)reader.ReadByte();
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000A7904 File Offset: 0x000A5B04
		public static int[] Int32[](NetworkReader reader)
		{
			return reader.ReadArray<int>();
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000A7918 File Offset: 0x000A5B18
		public static void _Write_ServerNetMessage(NetworkWriter writer, ServerNetMessage value)
		{
			GeneratedNetworkCode._Write_ServerNetOperation(writer, value.serverNetOperation);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.datas);
			writer.WriteString(value.strData);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000A794C File Offset: 0x000A5B4C
		public static void _Write_ServerNetOperation(NetworkWriter writer, ServerNetOperation value)
		{
			writer.WriteByte((byte)value);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000A7960 File Offset: 0x000A5B60
		public static void Int32[](NetworkWriter writer, int[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000A7974 File Offset: 0x000A5B74
		public static ClientNetMessage _Read_ClientNetMessage(NetworkReader reader)
		{
			return new ClientNetMessage
			{
				clientNetOperation = GeneratedNetworkCode._Read_ClientNetOperation(reader),
				datas = GeneratedNetworkCode._Read_System.Int32[](reader),
				data = reader.ReadInt(),
				strs = GeneratedNetworkCode._Read_System.String[](reader)
			};
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000A79C8 File Offset: 0x000A5BC8
		public static ClientNetOperation _Read_ClientNetOperation(NetworkReader reader)
		{
			return (ClientNetOperation)reader.ReadByte();
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000A79DC File Offset: 0x000A5BDC
		public static string[] String[](NetworkReader reader)
		{
			return reader.ReadArray<string>();
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000A79F0 File Offset: 0x000A5BF0
		public static void _Write_ClientNetMessage(NetworkWriter writer, ClientNetMessage value)
		{
			GeneratedNetworkCode._Write_ClientNetOperation(writer, value.clientNetOperation);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.datas);
			writer.WriteInt(value.data);
			GeneratedNetworkCode._Write_System.String[](writer, value.strs);
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000A7A30 File Offset: 0x000A5C30
		public static void _Write_ClientNetOperation(NetworkWriter writer, ClientNetOperation value)
		{
			writer.WriteByte((byte)value);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000A7A44 File Offset: 0x000A5C44
		public static void String[](NetworkWriter writer, string[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000A7A58 File Offset: 0x000A5C58
		public static void _Write_RoleState(NetworkWriter writer, RoleState value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000A7A6C File Offset: 0x000A5C6C
		public static RoleState _Read_RoleState(NetworkReader reader)
		{
			return (RoleState)reader.ReadInt();
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000A7A80 File Offset: 0x000A5C80
		public static void _Write_EnemyType(NetworkWriter writer, EnemyType value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000A7A94 File Offset: 0x000A5C94
		public static EnemyType _Read_EnemyType(NetworkReader reader)
		{
			return (EnemyType)reader.ReadInt();
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x000A7AA8 File Offset: 0x000A5CA8
		public static void _Write_ActiveSkillEnum(NetworkWriter writer, ActiveSkillEnum value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000A7ABC File Offset: 0x000A5CBC
		public static ActiveSkillEnum _Read_ActiveSkillEnum(NetworkReader reader)
		{
			return (ActiveSkillEnum)reader.ReadInt();
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000A7AD0 File Offset: 0x000A5CD0
		public static void _Write_EnemyEntriesType[](NetworkWriter writer, EnemyEntriesType[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000A7AE4 File Offset: 0x000A5CE4
		public static void _Write_EnemyEntriesType(NetworkWriter writer, EnemyEntriesType value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000A7AF8 File Offset: 0x000A5CF8
		public static EnemyEntriesType[] _Read_EnemyEntriesType[](NetworkReader reader)
		{
			return reader.ReadArray<EnemyEntriesType>();
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000A7B0C File Offset: 0x000A5D0C
		public static EnemyEntriesType _Read_EnemyEntriesType(NetworkReader reader)
		{
			return (EnemyEntriesType)reader.ReadInt();
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000A7B20 File Offset: 0x000A5D20
		public static void _Write_LocalBuffType(NetworkWriter writer, LocalBuffType value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000A7B34 File Offset: 0x000A5D34
		public static LocalBuffType _Read_LocalBuffType(NetworkReader reader)
		{
			return (LocalBuffType)reader.ReadInt();
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000A7B48 File Offset: 0x000A5D48
		public static void _Write_RoleType(NetworkWriter writer, RoleType value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000A7B5C File Offset: 0x000A5D5C
		public static RoleType _Read_RoleType(NetworkReader reader)
		{
			return (RoleType)reader.ReadInt();
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000A7B70 File Offset: 0x000A5D70
		public static void _Write_EnemyCreateType(NetworkWriter writer, EnemyCreateType value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000A7B84 File Offset: 0x000A5D84
		public static EnemyCreateType _Read_EnemyCreateType(NetworkReader reader)
		{
			return (EnemyCreateType)reader.ReadInt();
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x000A7B98 File Offset: 0x000A5D98
		public static void _Write_ItemStruct[](NetworkWriter writer, ItemStruct[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000A7BAC File Offset: 0x000A5DAC
		public static void _Write_ItemStruct(NetworkWriter writer, ItemStruct value)
		{
			if (value == null)
			{
				writer.WriteBool(false);
				return;
			}
			writer.WriteBool(true);
			writer.WriteUInt(value.id);
			writer.WriteUInt(value.authorityId);
			GeneratedNetworkCode._Write_ItemType(writer, value.itemType);
			writer.WriteInt(value.itemNum);
			writer.WriteVector3(value.pos);
			writer.WriteGameObject(value.model);
			writer.WriteGameObject(value.effect);
			writer.WriteTransform(value.modelTransform);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000A7C30 File Offset: 0x000A5E30
		public static void _Write_ItemType(NetworkWriter writer, ItemType value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000A7C44 File Offset: 0x000A5E44
		public static ItemStruct[] _Read_ItemStruct[](NetworkReader reader)
		{
			return reader.ReadArray<ItemStruct>();
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x000A7C58 File Offset: 0x000A5E58
		public static ItemStruct _Read_ItemStruct(NetworkReader reader)
		{
			if (!reader.ReadBool())
			{
				return null;
			}
			return new ItemStruct
			{
				id = reader.ReadUInt(),
				authorityId = reader.ReadUInt(),
				itemType = GeneratedNetworkCode._Read_ItemType(reader),
				itemNum = reader.ReadInt(),
				pos = reader.ReadVector3(),
				model = reader.ReadGameObject(),
				effect = reader.ReadGameObject(),
				modelTransform = reader.ReadTransform()
			};
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000A7CF4 File Offset: 0x000A5EF4
		public static ItemType _Read_ItemType(NetworkReader reader)
		{
			return (ItemType)reader.ReadInt();
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000A7D08 File Offset: 0x000A5F08
		public static void _Write_BrotatoWeaponType(NetworkWriter writer, BrotatoWeaponType value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000A7D1C File Offset: 0x000A5F1C
		public static void Single[](NetworkWriter writer, float[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000A7D30 File Offset: 0x000A5F30
		public static BrotatoWeaponType _Read_BrotatoWeaponType(NetworkReader reader)
		{
			return (BrotatoWeaponType)reader.ReadInt();
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x000A7D44 File Offset: 0x000A5F44
		public static float[] Single[](NetworkReader reader)
		{
			return reader.ReadArray<float>();
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000A7D58 File Offset: 0x000A5F58
		public static void _Write_HeroType(NetworkWriter writer, HeroType value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000A7D6C File Offset: 0x000A5F6C
		public static HeroType _Read_HeroType(NetworkReader reader)
		{
			return (HeroType)reader.ReadInt();
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000A7D80 File Offset: 0x000A5F80
		public static void _Write_BagItem(NetworkWriter writer, BagItem value)
		{
			if (value == null)
			{
				writer.WriteBool(false);
				return;
			}
			writer.WriteBool(true);
			GeneratedNetworkCode._Write_BagItemType(writer, value.bagItemType);
			GeneratedNetworkCode._Write_ItemType(writer, value.bookType);
			writer.WriteString(value.id);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000A7DC8 File Offset: 0x000A5FC8
		public static void _Write_BagItemType(NetworkWriter writer, BagItemType value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000A7DDC File Offset: 0x000A5FDC
		public static BagItem _Read_BagItem(NetworkReader reader)
		{
			if (!reader.ReadBool())
			{
				return null;
			}
			return new BagItem
			{
				bagItemType = GeneratedNetworkCode._Read_BagItemType(reader),
				bookType = GeneratedNetworkCode._Read_ItemType(reader),
				id = reader.ReadString()
			};
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000A7E2C File Offset: 0x000A602C
		public static BagItemType _Read_BagItemType(NetworkReader reader)
		{
			return (BagItemType)reader.ReadInt();
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x000A7E40 File Offset: 0x000A6040
		public static void List(NetworkWriter writer, List<uint> value)
		{
			writer.WriteList(value);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x000A7E54 File Offset: 0x000A6054
		public static List<uint> List(NetworkReader reader)
		{
			return reader.ReadList<uint>();
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x000A7E68 File Offset: 0x000A6068
		public static void _Write_AttackType(NetworkWriter writer, AttackType value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000A7E7C File Offset: 0x000A607C
		public static AttackType _Read_AttackType(NetworkReader reader)
		{
			return (AttackType)reader.ReadInt();
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000A7E90 File Offset: 0x000A6090
		public static void _Write_SaiYaDarkBuff/ReData(NetworkWriter writer, SaiYaDarkBuff.ReData value)
		{
			writer.WriteInt(value.reSta);
			writer.WriteInt(value.reStr);
			writer.WriteInt(value.reAgi);
			writer.WriteInt(value.reMaxHp);
			writer.WriteInt(value.reMaxMp);
			writer.WriteInt(value.reArmor);
			writer.WriteFloat(value.reAttackSpeed);
			writer.WriteFloat(value.reMoveSpeed);
			writer.WriteInt(value.reAttack);
			writer.WriteInt(value.dodge);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x000A7F18 File Offset: 0x000A6118
		public static SaiYaDarkBuff.ReData _Read_SaiYaDarkBuff/ReData(NetworkReader reader)
		{
			return new SaiYaDarkBuff.ReData
			{
				reSta = reader.ReadInt(),
				reStr = reader.ReadInt(),
				reAgi = reader.ReadInt(),
				reMaxHp = reader.ReadInt(),
				reMaxMp = reader.ReadInt(),
				reArmor = reader.ReadInt(),
				reAttackSpeed = reader.ReadFloat(),
				reMoveSpeed = reader.ReadFloat(),
				reAttack = reader.ReadInt(),
				dodge = reader.ReadInt()
			};
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000A7FC8 File Offset: 0x000A61C8
		public static void _Write_SaveLoadManager/PlayerKingData(NetworkWriter writer, SaveLoadManager.PlayerKingData value)
		{
			writer.WriteString(value.kingName);
			writer.WriteULong(value.steamID);
			GeneratedNetworkCode._Write_HeroType(writer, value.heroType);
			GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingSkillData[](writer, value.skill);
			GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingEquipData[](writer, value.equip);
			GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingRelicData[](writer, value.relic);
			GeneratedNetworkCode._Write_System.Int32[](writer, value.card);
			writer.WriteInt(value.level);
			writer.WriteFloat(value.allDamage);
			writer.WriteInt(value.allMoney);
			writer.WriteInt(value.allGem);
			writer.WriteLong(value.maxHp);
			writer.WriteInt(value.maxMp);
			writer.WriteInt(value.str);
			writer.WriteInt(value.agi);
			writer.WriteInt(value.sta);
			writer.WriteInt(value.armor);
			writer.WriteInt(value.dodge);
			writer.WriteInt(value.skillReduction);
			writer.WriteFloat(value.moveSpeed);
			writer.WriteInt(value.lucky);
			writer.WriteInt(value.hpAdd);
			writer.WriteInt(value.mpAdd);
			writer.WriteFloat(value.hpSecRate);
			writer.WriteFloat(value.attackAddHp);
			writer.WriteFloat(value.lifeStealing);
			writer.WriteFloat(value.magicXiXue);
			writer.WriteInt(value.attack);
			writer.WriteFloat(value.attackSpeed);
			writer.WriteFloat(value.critical);
			writer.WriteFloat(value.criticalDamage);
			writer.WriteFloat(value.normalDamage);
			writer.WriteFloat(value.normalBreak);
			writer.WriteFloat(value.skillDamage);
			writer.WriteFloat(value.skillBreak);
			writer.WriteInt(value.skillCd);
			writer.WriteFloat(value.skillRange);
			writer.WriteFloat(value.skillTime);
			writer.WriteFloat(value.skillExpend);
			writer.WriteInt(value.reduceInjury);
			writer.WriteInt(value.extraDamage);
			writer.WriteFloat(value.attackDistance);
			writer.WriteFloat(value.castSpeed);
			writer.WriteFloat(value.skillNoneDamage);
			writer.WriteFloat(value.fireDamage);
			writer.WriteFloat(value.iceDamage);
			writer.WriteFloat(value.lightDamage);
			writer.WriteFloat(value.effectDamage);
			writer.WriteFloat(value.hpAddUpgrade);
			writer.WriteFloat(value.buffDamage);
			writer.WriteFloat(value.haloRangeAdd);
			writer.WriteFloat(value.addCallMonsterAttack);
			writer.WriteFloat(value.addCallMonsterHp);
			writer.WriteFloat(value.addCallMonsterSize);
			writer.WriteFloat(value.addCallMonsterTime);
			writer.WriteFloat(value.addHenshin);
			writer.WriteFloat(value.addHenshinTime);
			writer.WriteFloat(value.armedAdd);
			writer.WriteFloat(value.equipAdd);
			writer.WriteInt(value.relifeTime);
			writer.WriteFloat(value.addHatred);
			writer.WriteFloat(value.forgeAdd);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000A82C0 File Offset: 0x000A64C0
		public static void _Write_SaveLoadManager/PlayerKingSkillData[](NetworkWriter writer, SaveLoadManager.PlayerKingSkillData[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000A82D4 File Offset: 0x000A64D4
		public static void _Write_SaveLoadManager/PlayerKingSkillData(NetworkWriter writer, SaveLoadManager.PlayerKingSkillData value)
		{
			writer.WriteString(value.skillName);
			writer.WriteInt(value.skillData);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x000A82FC File Offset: 0x000A64FC
		public static void _Write_SaveLoadManager/PlayerKingEquipData[](NetworkWriter writer, SaveLoadManager.PlayerKingEquipData[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000A8310 File Offset: 0x000A6510
		public static void _Write_SaveLoadManager/PlayerKingEquipData(NetworkWriter writer, SaveLoadManager.PlayerKingEquipData value)
		{
			writer.WriteString(value.equip);
			writer.WriteInt(value.equipData);
			GeneratedNetworkCode._Write_System.String[](writer, value.equipEvolutionSkill);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x000A8344 File Offset: 0x000A6544
		public static void _Write_SaveLoadManager/PlayerKingRelicData[](NetworkWriter writer, SaveLoadManager.PlayerKingRelicData[] value)
		{
			writer.WriteArray(value);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000A8358 File Offset: 0x000A6558
		public static void _Write_SaveLoadManager/PlayerKingRelicData(NetworkWriter writer, SaveLoadManager.PlayerKingRelicData value)
		{
			writer.WriteString(value.relicName);
			writer.WriteInt(value.relicLevel);
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000A8380 File Offset: 0x000A6580
		public static SaveLoadManager.PlayerKingData _Read_SaveLoadManager/PlayerKingData(NetworkReader reader)
		{
			return new SaveLoadManager.PlayerKingData
			{
				kingName = reader.ReadString(),
				steamID = reader.ReadULong(),
				heroType = GeneratedNetworkCode._Read_HeroType(reader),
				skill = GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingSkillData[](reader),
				equip = GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingEquipData[](reader),
				relic = GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingRelicData[](reader),
				card = GeneratedNetworkCode._Read_System.Int32[](reader),
				level = reader.ReadInt(),
				allDamage = reader.ReadFloat(),
				allMoney = reader.ReadInt(),
				allGem = reader.ReadInt(),
				maxHp = reader.ReadLong(),
				maxMp = reader.ReadInt(),
				str = reader.ReadInt(),
				agi = reader.ReadInt(),
				sta = reader.ReadInt(),
				armor = reader.ReadInt(),
				dodge = reader.ReadInt(),
				skillReduction = reader.ReadInt(),
				moveSpeed = reader.ReadFloat(),
				lucky = reader.ReadInt(),
				hpAdd = reader.ReadInt(),
				mpAdd = reader.ReadInt(),
				hpSecRate = reader.ReadFloat(),
				attackAddHp = reader.ReadFloat(),
				lifeStealing = reader.ReadFloat(),
				magicXiXue = reader.ReadFloat(),
				attack = reader.ReadInt(),
				attackSpeed = reader.ReadFloat(),
				critical = reader.ReadFloat(),
				criticalDamage = reader.ReadFloat(),
				normalDamage = reader.ReadFloat(),
				normalBreak = reader.ReadFloat(),
				skillDamage = reader.ReadFloat(),
				skillBreak = reader.ReadFloat(),
				skillCd = reader.ReadInt(),
				skillRange = reader.ReadFloat(),
				skillTime = reader.ReadFloat(),
				skillExpend = reader.ReadFloat(),
				reduceInjury = reader.ReadInt(),
				extraDamage = reader.ReadInt(),
				attackDistance = reader.ReadFloat(),
				castSpeed = reader.ReadFloat(),
				skillNoneDamage = reader.ReadFloat(),
				fireDamage = reader.ReadFloat(),
				iceDamage = reader.ReadFloat(),
				lightDamage = reader.ReadFloat(),
				effectDamage = reader.ReadFloat(),
				hpAddUpgrade = reader.ReadFloat(),
				buffDamage = reader.ReadFloat(),
				haloRangeAdd = reader.ReadFloat(),
				addCallMonsterAttack = reader.ReadFloat(),
				addCallMonsterHp = reader.ReadFloat(),
				addCallMonsterSize = reader.ReadFloat(),
				addCallMonsterTime = reader.ReadFloat(),
				addHenshin = reader.ReadFloat(),
				addHenshinTime = reader.ReadFloat(),
				armedAdd = reader.ReadFloat(),
				equipAdd = reader.ReadFloat(),
				relifeTime = reader.ReadInt(),
				addHatred = reader.ReadFloat(),
				forgeAdd = reader.ReadFloat()
			};
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000A873C File Offset: 0x000A693C
		public static SaveLoadManager.PlayerKingSkillData[] _Read_SaveLoadManager/PlayerKingSkillData[](NetworkReader reader)
		{
			return reader.ReadArray<SaveLoadManager.PlayerKingSkillData>();
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000A8750 File Offset: 0x000A6950
		public static SaveLoadManager.PlayerKingSkillData _Read_SaveLoadManager/PlayerKingSkillData(NetworkReader reader)
		{
			return new SaveLoadManager.PlayerKingSkillData
			{
				skillName = reader.ReadString(),
				skillData = reader.ReadInt()
			};
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x000A8788 File Offset: 0x000A6988
		public static SaveLoadManager.PlayerKingEquipData[] _Read_SaveLoadManager/PlayerKingEquipData[](NetworkReader reader)
		{
			return reader.ReadArray<SaveLoadManager.PlayerKingEquipData>();
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000A879C File Offset: 0x000A699C
		public static SaveLoadManager.PlayerKingEquipData _Read_SaveLoadManager/PlayerKingEquipData(NetworkReader reader)
		{
			return new SaveLoadManager.PlayerKingEquipData
			{
				equip = reader.ReadString(),
				equipData = reader.ReadInt(),
				equipEvolutionSkill = GeneratedNetworkCode._Read_System.String[](reader)
			};
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x000A87E4 File Offset: 0x000A69E4
		public static SaveLoadManager.PlayerKingRelicData[] _Read_SaveLoadManager/PlayerKingRelicData[](NetworkReader reader)
		{
			return reader.ReadArray<SaveLoadManager.PlayerKingRelicData>();
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x000A87F8 File Offset: 0x000A69F8
		public static SaveLoadManager.PlayerKingRelicData _Read_SaveLoadManager/PlayerKingRelicData(NetworkReader reader)
		{
			return new SaveLoadManager.PlayerKingRelicData
			{
				relicName = reader.ReadString(),
				relicLevel = reader.ReadInt()
			};
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000A8830 File Offset: 0x000A6A30
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void InitReadWriters()
		{
			Writer<byte>.write = new Action<NetworkWriter, byte>(NetworkWriterExtensions.WriteByte);
			Writer<sbyte>.write = new Action<NetworkWriter, sbyte>(NetworkWriterExtensions.WriteSByte);
			Writer<char>.write = new Action<NetworkWriter, char>(NetworkWriterExtensions.WriteChar);
			Writer<bool>.write = new Action<NetworkWriter, bool>(NetworkWriterExtensions.WriteBool);
			Writer<ushort>.write = new Action<NetworkWriter, ushort>(NetworkWriterExtensions.WriteUShort);
			Writer<short>.write = new Action<NetworkWriter, short>(NetworkWriterExtensions.WriteShort);
			Writer<uint>.write = new Action<NetworkWriter, uint>(NetworkWriterExtensions.WriteUInt);
			Writer<int>.write = new Action<NetworkWriter, int>(NetworkWriterExtensions.WriteInt);
			Writer<ulong>.write = new Action<NetworkWriter, ulong>(NetworkWriterExtensions.WriteULong);
			Writer<long>.write = new Action<NetworkWriter, long>(NetworkWriterExtensions.WriteLong);
			Writer<float>.write = new Action<NetworkWriter, float>(NetworkWriterExtensions.WriteFloat);
			Writer<double>.write = new Action<NetworkWriter, double>(NetworkWriterExtensions.WriteDouble);
			Writer<decimal>.write = new Action<NetworkWriter, decimal>(NetworkWriterExtensions.WriteDecimal);
			Writer<string>.write = new Action<NetworkWriter, string>(NetworkWriterExtensions.WriteString);
			Writer<byte[]>.write = new Action<NetworkWriter, byte[]>(NetworkWriterExtensions.WriteBytesAndSize);
			Writer<ArraySegment<byte>>.write = new Action<NetworkWriter, ArraySegment<byte>>(NetworkWriterExtensions.WriteBytesAndSizeSegment);
			Writer<Vector2>.write = new Action<NetworkWriter, Vector2>(NetworkWriterExtensions.WriteVector2);
			Writer<Vector3>.write = new Action<NetworkWriter, Vector3>(NetworkWriterExtensions.WriteVector3);
			Writer<Vector3?>.write = new Action<NetworkWriter, Vector3?>(NetworkWriterExtensions.WriteVector3Nullable);
			Writer<Vector4>.write = new Action<NetworkWriter, Vector4>(NetworkWriterExtensions.WriteVector4);
			Writer<Vector2Int>.write = new Action<NetworkWriter, Vector2Int>(NetworkWriterExtensions.WriteVector2Int);
			Writer<Vector3Int>.write = new Action<NetworkWriter, Vector3Int>(NetworkWriterExtensions.WriteVector3Int);
			Writer<Color>.write = new Action<NetworkWriter, Color>(NetworkWriterExtensions.WriteColor);
			Writer<Color?>.write = new Action<NetworkWriter, Color?>(NetworkWriterExtensions.WriteColorNullable);
			Writer<Color32>.write = new Action<NetworkWriter, Color32>(NetworkWriterExtensions.WriteColor32);
			Writer<Color32?>.write = new Action<NetworkWriter, Color32?>(NetworkWriterExtensions.WriteColor32Nullable);
			Writer<Quaternion>.write = new Action<NetworkWriter, Quaternion>(NetworkWriterExtensions.WriteQuaternion);
			Writer<Quaternion?>.write = new Action<NetworkWriter, Quaternion?>(NetworkWriterExtensions.WriteQuaternionNullable);
			Writer<Rect>.write = new Action<NetworkWriter, Rect>(NetworkWriterExtensions.WriteRect);
			Writer<Plane>.write = new Action<NetworkWriter, Plane>(NetworkWriterExtensions.WritePlane);
			Writer<Ray>.write = new Action<NetworkWriter, Ray>(NetworkWriterExtensions.WriteRay);
			Writer<Matrix4x4>.write = new Action<NetworkWriter, Matrix4x4>(NetworkWriterExtensions.WriteMatrix4x4);
			Writer<Guid>.write = new Action<NetworkWriter, Guid>(NetworkWriterExtensions.WriteGuid);
			Writer<NetworkIdentity>.write = new Action<NetworkWriter, NetworkIdentity>(NetworkWriterExtensions.WriteNetworkIdentity);
			Writer<NetworkBehaviour>.write = new Action<NetworkWriter, NetworkBehaviour>(NetworkWriterExtensions.WriteNetworkBehaviour);
			Writer<Transform>.write = new Action<NetworkWriter, Transform>(NetworkWriterExtensions.WriteTransform);
			Writer<GameObject>.write = new Action<NetworkWriter, GameObject>(NetworkWriterExtensions.WriteGameObject);
			Writer<Uri>.write = new Action<NetworkWriter, Uri>(NetworkWriterExtensions.WriteUri);
			Writer<ReadyMessage>.write = new Action<NetworkWriter, ReadyMessage>(GeneratedNetworkCode._Write_Mirror.ReadyMessage);
			Writer<NotReadyMessage>.write = new Action<NetworkWriter, NotReadyMessage>(GeneratedNetworkCode._Write_Mirror.NotReadyMessage);
			Writer<AddPlayerMessage>.write = new Action<NetworkWriter, AddPlayerMessage>(GeneratedNetworkCode._Write_Mirror.AddPlayerMessage);
			Writer<SceneMessage>.write = new Action<NetworkWriter, SceneMessage>(GeneratedNetworkCode._Write_Mirror.SceneMessage);
			Writer<SceneOperation>.write = new Action<NetworkWriter, SceneOperation>(GeneratedNetworkCode._Write_Mirror.SceneOperation);
			Writer<CommandMessage>.write = new Action<NetworkWriter, CommandMessage>(GeneratedNetworkCode._Write_Mirror.CommandMessage);
			Writer<RpcMessage>.write = new Action<NetworkWriter, RpcMessage>(GeneratedNetworkCode._Write_Mirror.RpcMessage);
			Writer<SpawnMessage>.write = new Action<NetworkWriter, SpawnMessage>(GeneratedNetworkCode._Write_Mirror.SpawnMessage);
			Writer<ChangeOwnerMessage>.write = new Action<NetworkWriter, ChangeOwnerMessage>(GeneratedNetworkCode._Write_Mirror.ChangeOwnerMessage);
			Writer<ObjectSpawnStartedMessage>.write = new Action<NetworkWriter, ObjectSpawnStartedMessage>(GeneratedNetworkCode._Write_Mirror.ObjectSpawnStartedMessage);
			Writer<ObjectSpawnFinishedMessage>.write = new Action<NetworkWriter, ObjectSpawnFinishedMessage>(GeneratedNetworkCode._Write_Mirror.ObjectSpawnFinishedMessage);
			Writer<ObjectDestroyMessage>.write = new Action<NetworkWriter, ObjectDestroyMessage>(GeneratedNetworkCode._Write_Mirror.ObjectDestroyMessage);
			Writer<ObjectHideMessage>.write = new Action<NetworkWriter, ObjectHideMessage>(GeneratedNetworkCode._Write_Mirror.ObjectHideMessage);
			Writer<EntityStateMessage>.write = new Action<NetworkWriter, EntityStateMessage>(GeneratedNetworkCode._Write_Mirror.EntityStateMessage);
			Writer<NetworkPingMessage>.write = new Action<NetworkWriter, NetworkPingMessage>(GeneratedNetworkCode._Write_Mirror.NetworkPingMessage);
			Writer<NetworkPongMessage>.write = new Action<NetworkWriter, NetworkPongMessage>(GeneratedNetworkCode._Write_Mirror.NetworkPongMessage);
			Writer<ServerNetMessage>.write = new Action<NetworkWriter, ServerNetMessage>(GeneratedNetworkCode._Write_ServerNetMessage);
			Writer<ServerNetOperation>.write = new Action<NetworkWriter, ServerNetOperation>(GeneratedNetworkCode._Write_ServerNetOperation);
			Writer<int[]>.write = new Action<NetworkWriter, int[]>(GeneratedNetworkCode._Write_System.Int32[]);
			Writer<ClientNetMessage>.write = new Action<NetworkWriter, ClientNetMessage>(GeneratedNetworkCode._Write_ClientNetMessage);
			Writer<ClientNetOperation>.write = new Action<NetworkWriter, ClientNetOperation>(GeneratedNetworkCode._Write_ClientNetOperation);
			Writer<string[]>.write = new Action<NetworkWriter, string[]>(GeneratedNetworkCode._Write_System.String[]);
			Writer<RoleState>.write = new Action<NetworkWriter, RoleState>(GeneratedNetworkCode._Write_RoleState);
			Writer<EnemyType>.write = new Action<NetworkWriter, EnemyType>(GeneratedNetworkCode._Write_EnemyType);
			Writer<ActiveSkillEnum>.write = new Action<NetworkWriter, ActiveSkillEnum>(GeneratedNetworkCode._Write_ActiveSkillEnum);
			Writer<EnemyEntriesType[]>.write = new Action<NetworkWriter, EnemyEntriesType[]>(GeneratedNetworkCode._Write_EnemyEntriesType[]);
			Writer<EnemyEntriesType>.write = new Action<NetworkWriter, EnemyEntriesType>(GeneratedNetworkCode._Write_EnemyEntriesType);
			Writer<LocalBuffType>.write = new Action<NetworkWriter, LocalBuffType>(GeneratedNetworkCode._Write_LocalBuffType);
			Writer<RoleType>.write = new Action<NetworkWriter, RoleType>(GeneratedNetworkCode._Write_RoleType);
			Writer<EnemyCreateType>.write = new Action<NetworkWriter, EnemyCreateType>(GeneratedNetworkCode._Write_EnemyCreateType);
			Writer<ItemStruct[]>.write = new Action<NetworkWriter, ItemStruct[]>(GeneratedNetworkCode._Write_ItemStruct[]);
			Writer<ItemStruct>.write = new Action<NetworkWriter, ItemStruct>(GeneratedNetworkCode._Write_ItemStruct);
			Writer<ItemType>.write = new Action<NetworkWriter, ItemType>(GeneratedNetworkCode._Write_ItemType);
			Writer<BrotatoWeaponType>.write = new Action<NetworkWriter, BrotatoWeaponType>(GeneratedNetworkCode._Write_BrotatoWeaponType);
			Writer<float[]>.write = new Action<NetworkWriter, float[]>(GeneratedNetworkCode._Write_System.Single[]);
			Writer<HeroType>.write = new Action<NetworkWriter, HeroType>(GeneratedNetworkCode._Write_HeroType);
			Writer<BagItem>.write = new Action<NetworkWriter, BagItem>(GeneratedNetworkCode._Write_BagItem);
			Writer<BagItemType>.write = new Action<NetworkWriter, BagItemType>(GeneratedNetworkCode._Write_BagItemType);
			Writer<List<uint>>.write = new Action<NetworkWriter, List<uint>>(GeneratedNetworkCode._Write_System.Collections.Generic.List`1<System.UInt32>);
			Writer<AttackType>.write = new Action<NetworkWriter, AttackType>(GeneratedNetworkCode._Write_AttackType);
			Writer<SaiYaDarkBuff.ReData>.write = new Action<NetworkWriter, SaiYaDarkBuff.ReData>(GeneratedNetworkCode._Write_SaiYaDarkBuff/ReData);
			Writer<SaveLoadManager.PlayerKingData>.write = new Action<NetworkWriter, SaveLoadManager.PlayerKingData>(GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingData);
			Writer<SaveLoadManager.PlayerKingSkillData[]>.write = new Action<NetworkWriter, SaveLoadManager.PlayerKingSkillData[]>(GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingSkillData[]);
			Writer<SaveLoadManager.PlayerKingSkillData>.write = new Action<NetworkWriter, SaveLoadManager.PlayerKingSkillData>(GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingSkillData);
			Writer<SaveLoadManager.PlayerKingEquipData[]>.write = new Action<NetworkWriter, SaveLoadManager.PlayerKingEquipData[]>(GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingEquipData[]);
			Writer<SaveLoadManager.PlayerKingEquipData>.write = new Action<NetworkWriter, SaveLoadManager.PlayerKingEquipData>(GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingEquipData);
			Writer<SaveLoadManager.PlayerKingRelicData[]>.write = new Action<NetworkWriter, SaveLoadManager.PlayerKingRelicData[]>(GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingRelicData[]);
			Writer<SaveLoadManager.PlayerKingRelicData>.write = new Action<NetworkWriter, SaveLoadManager.PlayerKingRelicData>(GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingRelicData);
			Reader<byte>.read = new Func<NetworkReader, byte>(NetworkReaderExtensions.ReadByte);
			Reader<sbyte>.read = new Func<NetworkReader, sbyte>(NetworkReaderExtensions.ReadSByte);
			Reader<char>.read = new Func<NetworkReader, char>(NetworkReaderExtensions.ReadChar);
			Reader<bool>.read = new Func<NetworkReader, bool>(NetworkReaderExtensions.ReadBool);
			Reader<short>.read = new Func<NetworkReader, short>(NetworkReaderExtensions.ReadShort);
			Reader<ushort>.read = new Func<NetworkReader, ushort>(NetworkReaderExtensions.ReadUShort);
			Reader<int>.read = new Func<NetworkReader, int>(NetworkReaderExtensions.ReadInt);
			Reader<uint>.read = new Func<NetworkReader, uint>(NetworkReaderExtensions.ReadUInt);
			Reader<long>.read = new Func<NetworkReader, long>(NetworkReaderExtensions.ReadLong);
			Reader<ulong>.read = new Func<NetworkReader, ulong>(NetworkReaderExtensions.ReadULong);
			Reader<float>.read = new Func<NetworkReader, float>(NetworkReaderExtensions.ReadFloat);
			Reader<double>.read = new Func<NetworkReader, double>(NetworkReaderExtensions.ReadDouble);
			Reader<decimal>.read = new Func<NetworkReader, decimal>(NetworkReaderExtensions.ReadDecimal);
			Reader<string>.read = new Func<NetworkReader, string>(NetworkReaderExtensions.ReadString);
			Reader<byte[]>.read = new Func<NetworkReader, byte[]>(NetworkReaderExtensions.ReadBytesAndSize);
			Reader<ArraySegment<byte>>.read = new Func<NetworkReader, ArraySegment<byte>>(NetworkReaderExtensions.ReadBytesAndSizeSegment);
			Reader<Vector2>.read = new Func<NetworkReader, Vector2>(NetworkReaderExtensions.ReadVector2);
			Reader<Vector3>.read = new Func<NetworkReader, Vector3>(NetworkReaderExtensions.ReadVector3);
			Reader<Vector3?>.read = new Func<NetworkReader, Vector3?>(NetworkReaderExtensions.ReadVector3Nullable);
			Reader<Vector4>.read = new Func<NetworkReader, Vector4>(NetworkReaderExtensions.ReadVector4);
			Reader<Vector2Int>.read = new Func<NetworkReader, Vector2Int>(NetworkReaderExtensions.ReadVector2Int);
			Reader<Vector3Int>.read = new Func<NetworkReader, Vector3Int>(NetworkReaderExtensions.ReadVector3Int);
			Reader<Color>.read = new Func<NetworkReader, Color>(NetworkReaderExtensions.ReadColor);
			Reader<Color?>.read = new Func<NetworkReader, Color?>(NetworkReaderExtensions.ReadColorNullable);
			Reader<Color32>.read = new Func<NetworkReader, Color32>(NetworkReaderExtensions.ReadColor32);
			Reader<Color32?>.read = new Func<NetworkReader, Color32?>(NetworkReaderExtensions.ReadColor32Nullable);
			Reader<Quaternion>.read = new Func<NetworkReader, Quaternion>(NetworkReaderExtensions.ReadQuaternion);
			Reader<Quaternion?>.read = new Func<NetworkReader, Quaternion?>(NetworkReaderExtensions.ReadQuaternionNullable);
			Reader<Rect>.read = new Func<NetworkReader, Rect>(NetworkReaderExtensions.ReadRect);
			Reader<Plane>.read = new Func<NetworkReader, Plane>(NetworkReaderExtensions.ReadPlane);
			Reader<Ray>.read = new Func<NetworkReader, Ray>(NetworkReaderExtensions.ReadRay);
			Reader<Matrix4x4>.read = new Func<NetworkReader, Matrix4x4>(NetworkReaderExtensions.ReadMatrix4x4);
			Reader<Guid>.read = new Func<NetworkReader, Guid>(NetworkReaderExtensions.ReadGuid);
			Reader<Transform>.read = new Func<NetworkReader, Transform>(NetworkReaderExtensions.ReadTransform);
			Reader<GameObject>.read = new Func<NetworkReader, GameObject>(NetworkReaderExtensions.ReadGameObject);
			Reader<NetworkIdentity>.read = new Func<NetworkReader, NetworkIdentity>(NetworkReaderExtensions.ReadNetworkIdentity);
			Reader<NetworkBehaviour>.read = new Func<NetworkReader, NetworkBehaviour>(NetworkReaderExtensions.ReadNetworkBehaviour);
			Reader<NetworkBehaviour.NetworkBehaviourSyncVar>.read = new Func<NetworkReader, NetworkBehaviour.NetworkBehaviourSyncVar>(NetworkReaderExtensions.ReadNetworkBehaviourSyncVar);
			Reader<Uri>.read = new Func<NetworkReader, Uri>(NetworkReaderExtensions.ReadUri);
			Reader<ReadyMessage>.read = new Func<NetworkReader, ReadyMessage>(GeneratedNetworkCode._Read_Mirror.ReadyMessage);
			Reader<NotReadyMessage>.read = new Func<NetworkReader, NotReadyMessage>(GeneratedNetworkCode._Read_Mirror.NotReadyMessage);
			Reader<AddPlayerMessage>.read = new Func<NetworkReader, AddPlayerMessage>(GeneratedNetworkCode._Read_Mirror.AddPlayerMessage);
			Reader<SceneMessage>.read = new Func<NetworkReader, SceneMessage>(GeneratedNetworkCode._Read_Mirror.SceneMessage);
			Reader<SceneOperation>.read = new Func<NetworkReader, SceneOperation>(GeneratedNetworkCode._Read_Mirror.SceneOperation);
			Reader<CommandMessage>.read = new Func<NetworkReader, CommandMessage>(GeneratedNetworkCode._Read_Mirror.CommandMessage);
			Reader<RpcMessage>.read = new Func<NetworkReader, RpcMessage>(GeneratedNetworkCode._Read_Mirror.RpcMessage);
			Reader<SpawnMessage>.read = new Func<NetworkReader, SpawnMessage>(GeneratedNetworkCode._Read_Mirror.SpawnMessage);
			Reader<ChangeOwnerMessage>.read = new Func<NetworkReader, ChangeOwnerMessage>(GeneratedNetworkCode._Read_Mirror.ChangeOwnerMessage);
			Reader<ObjectSpawnStartedMessage>.read = new Func<NetworkReader, ObjectSpawnStartedMessage>(GeneratedNetworkCode._Read_Mirror.ObjectSpawnStartedMessage);
			Reader<ObjectSpawnFinishedMessage>.read = new Func<NetworkReader, ObjectSpawnFinishedMessage>(GeneratedNetworkCode._Read_Mirror.ObjectSpawnFinishedMessage);
			Reader<ObjectDestroyMessage>.read = new Func<NetworkReader, ObjectDestroyMessage>(GeneratedNetworkCode._Read_Mirror.ObjectDestroyMessage);
			Reader<ObjectHideMessage>.read = new Func<NetworkReader, ObjectHideMessage>(GeneratedNetworkCode._Read_Mirror.ObjectHideMessage);
			Reader<EntityStateMessage>.read = new Func<NetworkReader, EntityStateMessage>(GeneratedNetworkCode._Read_Mirror.EntityStateMessage);
			Reader<NetworkPingMessage>.read = new Func<NetworkReader, NetworkPingMessage>(GeneratedNetworkCode._Read_Mirror.NetworkPingMessage);
			Reader<NetworkPongMessage>.read = new Func<NetworkReader, NetworkPongMessage>(GeneratedNetworkCode._Read_Mirror.NetworkPongMessage);
			Reader<ServerNetMessage>.read = new Func<NetworkReader, ServerNetMessage>(GeneratedNetworkCode._Read_ServerNetMessage);
			Reader<ServerNetOperation>.read = new Func<NetworkReader, ServerNetOperation>(GeneratedNetworkCode._Read_ServerNetOperation);
			Reader<int[]>.read = new Func<NetworkReader, int[]>(GeneratedNetworkCode._Read_System.Int32[]);
			Reader<ClientNetMessage>.read = new Func<NetworkReader, ClientNetMessage>(GeneratedNetworkCode._Read_ClientNetMessage);
			Reader<ClientNetOperation>.read = new Func<NetworkReader, ClientNetOperation>(GeneratedNetworkCode._Read_ClientNetOperation);
			Reader<string[]>.read = new Func<NetworkReader, string[]>(GeneratedNetworkCode._Read_System.String[]);
			Reader<RoleState>.read = new Func<NetworkReader, RoleState>(GeneratedNetworkCode._Read_RoleState);
			Reader<EnemyType>.read = new Func<NetworkReader, EnemyType>(GeneratedNetworkCode._Read_EnemyType);
			Reader<ActiveSkillEnum>.read = new Func<NetworkReader, ActiveSkillEnum>(GeneratedNetworkCode._Read_ActiveSkillEnum);
			Reader<EnemyEntriesType[]>.read = new Func<NetworkReader, EnemyEntriesType[]>(GeneratedNetworkCode._Read_EnemyEntriesType[]);
			Reader<EnemyEntriesType>.read = new Func<NetworkReader, EnemyEntriesType>(GeneratedNetworkCode._Read_EnemyEntriesType);
			Reader<LocalBuffType>.read = new Func<NetworkReader, LocalBuffType>(GeneratedNetworkCode._Read_LocalBuffType);
			Reader<RoleType>.read = new Func<NetworkReader, RoleType>(GeneratedNetworkCode._Read_RoleType);
			Reader<EnemyCreateType>.read = new Func<NetworkReader, EnemyCreateType>(GeneratedNetworkCode._Read_EnemyCreateType);
			Reader<ItemStruct[]>.read = new Func<NetworkReader, ItemStruct[]>(GeneratedNetworkCode._Read_ItemStruct[]);
			Reader<ItemStruct>.read = new Func<NetworkReader, ItemStruct>(GeneratedNetworkCode._Read_ItemStruct);
			Reader<ItemType>.read = new Func<NetworkReader, ItemType>(GeneratedNetworkCode._Read_ItemType);
			Reader<BrotatoWeaponType>.read = new Func<NetworkReader, BrotatoWeaponType>(GeneratedNetworkCode._Read_BrotatoWeaponType);
			Reader<float[]>.read = new Func<NetworkReader, float[]>(GeneratedNetworkCode._Read_System.Single[]);
			Reader<HeroType>.read = new Func<NetworkReader, HeroType>(GeneratedNetworkCode._Read_HeroType);
			Reader<BagItem>.read = new Func<NetworkReader, BagItem>(GeneratedNetworkCode._Read_BagItem);
			Reader<BagItemType>.read = new Func<NetworkReader, BagItemType>(GeneratedNetworkCode._Read_BagItemType);
			Reader<List<uint>>.read = new Func<NetworkReader, List<uint>>(GeneratedNetworkCode._Read_System.Collections.Generic.List`1<System.UInt32>);
			Reader<AttackType>.read = new Func<NetworkReader, AttackType>(GeneratedNetworkCode._Read_AttackType);
			Reader<SaiYaDarkBuff.ReData>.read = new Func<NetworkReader, SaiYaDarkBuff.ReData>(GeneratedNetworkCode._Read_SaiYaDarkBuff/ReData);
			Reader<SaveLoadManager.PlayerKingData>.read = new Func<NetworkReader, SaveLoadManager.PlayerKingData>(GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingData);
			Reader<SaveLoadManager.PlayerKingSkillData[]>.read = new Func<NetworkReader, SaveLoadManager.PlayerKingSkillData[]>(GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingSkillData[]);
			Reader<SaveLoadManager.PlayerKingSkillData>.read = new Func<NetworkReader, SaveLoadManager.PlayerKingSkillData>(GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingSkillData);
			Reader<SaveLoadManager.PlayerKingEquipData[]>.read = new Func<NetworkReader, SaveLoadManager.PlayerKingEquipData[]>(GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingEquipData[]);
			Reader<SaveLoadManager.PlayerKingEquipData>.read = new Func<NetworkReader, SaveLoadManager.PlayerKingEquipData>(GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingEquipData);
			Reader<SaveLoadManager.PlayerKingRelicData[]>.read = new Func<NetworkReader, SaveLoadManager.PlayerKingRelicData[]>(GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingRelicData[]);
			Reader<SaveLoadManager.PlayerKingRelicData>.read = new Func<NetworkReader, SaveLoadManager.PlayerKingRelicData>(GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingRelicData);
		}
	}
}
