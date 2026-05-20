using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using MemoryPack;
using Mirror;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200007E RID: 126
public static class Util
{
	// Token: 0x06000278 RID: 632 RVA: 0x0000CCE2 File Offset: 0x0000AEE2
	public static SOBrotatoWeaponConfig GetSOBrotatoWeaponConfig()
	{
		if (Util.soBrotatoWeaponConfig == null)
		{
			Util.soBrotatoWeaponConfig = Resources.Load<SOBrotatoWeaponConfig>("Bundles/SO/SOBrotatoWeaponConfig");
		}
		return Util.soBrotatoWeaponConfig;
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000CD05 File Offset: 0x0000AF05
	public static string GetLevelStarName(string nameStr, int level)
	{
		if (level <= 0)
		{
			return nameStr;
		}
		return PathDefine.Concat(nameStr, "(+", level, ")");
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000CD24 File Offset: 0x0000AF24
	public static float GetSymmetricValue(int totalCount, int index, float step)
	{
		if (totalCount <= 0)
		{
			return 0f;
		}
		float num = (float)(totalCount - 1) / 2f;
		return ((float)index - num) * step;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000CD4C File Offset: 0x0000AF4C
	public static Vector3 GetSaveMapPos(Vector3 pos)
	{
		float num = 1f;
		Vector2 canSpellArea = GameHelperClient.CanSpellArea;
		if (canSpellArea.x > 0f || canSpellArea.y > 0f)
		{
			pos.x = Mathf.Clamp(pos.x, -canSpellArea.x + num, canSpellArea.x - num);
			pos.z = Mathf.Clamp(pos.z, -canSpellArea.y + num, canSpellArea.y - num);
		}
		Vector2 noSpellArea = GameHelperClient.NoSpellArea;
		if (noSpellArea.x > 0f && noSpellArea.y > 0f)
		{
			num = 0.5f;
			float num2 = noSpellArea.x + num - Mathf.Abs(pos.x);
			float num3 = noSpellArea.y + num - Mathf.Abs(pos.z);
			if (num2 > 0f && num3 > 0f)
			{
				if (num2 < num3)
				{
					if (pos.x > 0f)
					{
						pos.x = noSpellArea.x + num;
					}
					else
					{
						pos.x = -noSpellArea.x - num;
					}
				}
				else if (pos.z > 0f)
				{
					pos.z = noSpellArea.y + num;
				}
				else
				{
					pos.z = -noSpellArea.y - num;
				}
			}
		}
		return pos;
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0000CE94 File Offset: 0x0000B094
	public static string GetHeroModePath(HeroType heroTypeValue)
	{
		Dictionary<string, RoleAttribute> heroAttributeDic = Game.GameData.HeroAttributeDic;
		int num = (int)heroTypeValue;
		RoleAttribute roleAttribute = heroAttributeDic[num.ToString()];
		string str = "Prefabs/Player_";
		num = (int)heroTypeValue;
		string text = str + num.ToString();
		if (GameHelperClient.isSaveHero && roleAttribute.isSaveMode)
		{
			text = PathDefine.Concat(text, StringDefine.SaveMode);
		}
		return text;
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0000CEEC File Offset: 0x0000B0EC
	public static string GetHeroName(HeroType heroTypeValue)
	{
		string result;
		if (LocalHeroModelService.TryGetEnabledHeroName(heroTypeValue, out result))
		{
			return result;
		}
		Dictionary<string, RoleAttribute> heroAttributeDic = Game.GameData.HeroAttributeDic;
		int num = (int)heroTypeValue;
		RoleAttribute roleAttribute = heroAttributeDic[num.ToString()];
		if (GameHelperClient.isSaveHero && roleAttribute.isSaveMode)
		{
			LanguageManager language = Game.Language;
			string str = "hero_";
			num = (int)heroTypeValue;
			return language.Get(str + num.ToString() + StringDefine.SaveMode, "");
		}
		LanguageManager language2 = Game.Language;
		string str2 = "hero_";
		num = (int)heroTypeValue;
		return language2.Get(str2 + num.ToString(), "");
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0000CF78 File Offset: 0x0000B178
	public static Sprite GetHeroIcon(HeroType heroTypeValue)
	{
		Sprite result;
		if (LocalHeroModelService.TryGetEnabledHeroIcon(heroTypeValue, out result))
		{
			return result;
		}
		object a = "Bundles/UI/Icon/HeadIcon/Player_";
		string[] array = heroTypeValue.ToString().Split("_", StringSplitOptions.None);
		string text = PathDefine.Concat(a, array[array.Length - 1]);
		Dictionary<string, RoleAttribute> heroAttributeDic = Game.GameData.HeroAttributeDic;
		int num = (int)heroTypeValue;
		RoleAttribute roleAttribute = heroAttributeDic[num.ToString()];
		if (GameHelperClient.isSaveHero && roleAttribute.isSaveMode)
		{
			text = PathDefine.Concat(text, StringDefine.SaveMode);
		}
		return Resources.Load<Sprite>(text);
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
	public static string GetItemName(ItemType itemType)
	{
		ItemData itemData;
		if (Game.GameData.ItemDataDic.TryGetValue(itemType, out itemData))
		{
			return Game.Language.Get(itemData.name, "");
		}
		return "";
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000D030 File Offset: 0x0000B230
	public static Transform SetZero(this Transform go)
	{
		go.localPosition = Vector3.zero;
		go.localScale = Vector3.one;
		go.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		return go;
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000D068 File Offset: 0x0000B268
	public static GameObject SetZero(this GameObject go)
	{
		return go.transform.SetZero().SetZero().gameObject;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0000D07F File Offset: 0x0000B27F
	public static GameObject SetPos(this GameObject go, float x, float y, float z)
	{
		go.transform.localPosition = new Vector3(x, y, z);
		go.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		return go;
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
	public static GameObject SetPos(this GameObject go, Vector3 v3)
	{
		go.transform.localPosition = v3;
		go.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		return go;
	}

	// Token: 0x06000284 RID: 644 RVA: 0x0000D0EC File Offset: 0x0000B2EC
	public static void DelayAction(Action ac, int time)
	{
		Util.<DelayAction>d__13 <DelayAction>d__;
		<DelayAction>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<DelayAction>d__.ac = ac;
		<DelayAction>d__.time = time;
		<DelayAction>d__.<>1__state = -1;
		<DelayAction>d__.<>t__builder.Start<Util.<DelayAction>d__13>(ref <DelayAction>d__);
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0000D12B File Offset: 0x0000B32B
	public static float GetLuckAddValue(int lucky)
	{
		return GameHelperClient.gameConfig.LuckCurve.Evaluate((float)lucky / 500f);
	}

	// Token: 0x06000286 RID: 646 RVA: 0x0000D144 File Offset: 0x0000B344
	public static bool IsTouchUI()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0000D158 File Offset: 0x0000B358
	public static byte[] Object2Bytes(this object obj)
	{
		byte[] buffer;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			((IFormatter)new BinaryFormatter()).Serialize(memoryStream, obj);
			buffer = memoryStream.GetBuffer();
		}
		return buffer;
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0000D19C File Offset: 0x0000B39C
	public static object Bytes2Object(this byte[] buff)
	{
		object result;
		using (MemoryStream memoryStream = new MemoryStream(buff))
		{
			result = ((IFormatter)new BinaryFormatter()).Deserialize(memoryStream);
		}
		return result;
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0000D1DC File Offset: 0x0000B3DC
	public static IPEndPoint String2IP(this string ipStr)
	{
		string[] array = ipStr.Split(':', StringSplitOptions.None);
		return new IPEndPoint(IPAddress.Parse(array[0]), int.Parse(array[1]));
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0000D208 File Offset: 0x0000B408
	public static object DIC(this object dic, string key)
	{
		if (((Dictionary<string, object>)dic).ContainsKey(key))
		{
			return ((Dictionary<string, object>)dic)[key];
		}
		return null;
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0000D228 File Offset: 0x0000B428
	public static T DIC<T>(this object dic, string key)
	{
		if (((Dictionary<string, object>)dic).ContainsKey(key))
		{
			object obj = ((Dictionary<string, object>)dic)[key];
			object obj2 = obj;
			string a = typeof(T).ToString();
			if (!(a == "System.Int32"))
			{
				if (!(a == "System.String"))
				{
					if (!(a == "System.Boolean"))
					{
						if (!(a == "System.Single"))
						{
							if (a == "System.Double")
							{
								obj2 = Convert.ToDouble(obj);
							}
						}
						else
						{
							obj2 = Convert.ToSingle(obj);
						}
					}
					else
					{
						obj2 = Convert.ToBoolean(obj);
					}
				}
				else
				{
					obj2 = obj.ToString();
				}
			}
			else
			{
				obj2 = obj.ToInt32();
			}
			return (T)((object)obj2);
		}
		Debug.LogError("没有这个key" + key);
		return default(T);
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0000D307 File Offset: 0x0000B507
	public static Dictionary<string, object> DIC(this object dic)
	{
		return (Dictionary<string, object>)dic;
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0000D310 File Offset: 0x0000B510
	public static T Clone<T>(this T RealObject)
	{
		T result;
		using (Stream stream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			((IFormatter)binaryFormatter).Serialize(stream, RealObject);
			stream.Seek(0L, SeekOrigin.Begin);
			result = (T)((object)((IFormatter)binaryFormatter).Deserialize(stream));
		}
		return result;
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000D368 File Offset: 0x0000B568
	public static int ToInt32(this object data)
	{
		if (data == null)
		{
			return 0;
		}
		if (string.IsNullOrEmpty(data.ToString()))
		{
			return 0;
		}
		return Convert.ToInt32(data);
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0000D384 File Offset: 0x0000B584
	public static object[] ToObjectArray(this object dataArray)
	{
		return (object[])dataArray;
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0000D38C File Offset: 0x0000B58C
	public static bool IsEmptyOrNull(this string data)
	{
		return string.IsNullOrEmpty(data);
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0000D394 File Offset: 0x0000B594
	public static string ToCheckZero(this int num)
	{
		if (num == 1)
		{
			return "";
		}
		return num.ToString();
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0000D3A7 File Offset: 0x0000B5A7
	public static T GetComponentAutoAdd<T>(this GameObject go) where T : Component
	{
		if (go.GetComponent<T>() == null)
		{
			return go.AddComponent<T>();
		}
		return go.GetComponent<T>();
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0000D3C9 File Offset: 0x0000B5C9
	public static T GetComponentAutoAdd<T>(this Transform go) where T : Component
	{
		if (go.GetComponent<T>() == null)
		{
			return go.gameObject.AddComponent<T>();
		}
		return go.GetComponent<T>();
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
	public static int ToBool2Int(this bool b)
	{
		if (b)
		{
			return 1;
		}
		return -1;
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
	public static void AddEventOnlyOne(this AnimationClip clip, AnimationEvent newEvent)
	{
		bool flag = false;
		for (int i = 0; i < clip.events.Length; i++)
		{
			if (clip.events[i].functionName.Equals(newEvent.functionName))
			{
				Debug.LogWarning("动画事件添加过了，或者添加了相同名字的事件");
				clip.events[i] = newEvent;
				flag = true;
			}
		}
		if (!flag)
		{
			clip.AddEvent(newEvent);
		}
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0000D453 File Offset: 0x0000B653
	public static Vector2 GetCenterToLB(this Vector3 v2, float width, float height)
	{
		return new Vector2(v2.x - width / 2f, v2.y - height / 2f);
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0000D478 File Offset: 0x0000B678
	public static void LookAt2D(this Transform transform, Transform target, int flipDir)
	{
		Vector3 vector = (target.position - transform.position) * (float)flipDir;
		float angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime);
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
	public static void AddToggleEvent(this Toggle toggle, Action ac)
	{
		toggle.onValueChanged.RemoveAllListeners();
		toggle.onValueChanged.AddListener(delegate(bool isOn)
		{
			if (isOn)
			{
				ac();
			}
		});
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0000D514 File Offset: 0x0000B714
	public static int GetNum(this Text _text)
	{
		return _text.text.ToInt32();
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0000D521 File Offset: 0x0000B721
	public static void SetNum(this Text _text, int _num)
	{
		_text.text = _num.ToString();
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000D530 File Offset: 0x0000B730
	public static void AddNum(this Text _text, int _num)
	{
		int num = _text.text.ToInt32();
		_text.text = (num + _num).ToString();
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0000D55A File Offset: 0x0000B75A
	public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
	{
		if (color == null)
		{
			color = new Color?(Color.white);
		}
		return Util.CreateWorldText(parent, text, localPosition, fontSize, color.Value, textAnchor, textAlignment, sortingOrder);
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000D588 File Offset: 0x0000B788
	public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
	{
		GameObject gameObject = new GameObject("World_Text", new Type[]
		{
			typeof(TextMesh)
		});
		Transform transform = gameObject.transform;
		transform.SetParent(parent, false);
		transform.localPosition = localPosition;
		TextMesh component = gameObject.GetComponent<TextMesh>();
		component.anchor = textAnchor;
		component.alignment = textAlignment;
		component.text = text;
		component.fontSize = fontSize;
		component.color = color;
		component.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
		return component;
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000D5FE File Offset: 0x0000B7FE
	public static int GetAbsoluteDistance(int x1, int y1, int x2, int y2)
	{
		return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0000D614 File Offset: 0x0000B814
	public static List<T> Shuffle<T>(this List<T> original)
	{
		RandomData randomNum = new RandomData();
		return original.Shuffle(randomNum);
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000D630 File Offset: 0x0000B830
	public static List<T> Shuffle<T>(this List<T> original, RandomData randomNum)
	{
		for (int i = 0; i < original.Count; i++)
		{
			int num = randomNum.Next(0, original.Count - 1);
			if (num != i)
			{
				T value = original[i];
				original[i] = original[num];
				original[num] = value;
			}
		}
		return original;
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000D684 File Offset: 0x0000B884
	public static List<T> GetRandomNumList<T>(this List<T> original, RandomData randomData, int num)
	{
		if (num >= original.Count)
		{
			return original;
		}
		List<int> list = new List<int>();
		List<T> list2 = new List<T>();
		for (int i = 0; i < original.Count; i++)
		{
			list.Add(i);
		}
		int num2 = 0;
		while (num2 < num && list.Count > 0)
		{
			int index = randomData.Next(0, list.Count);
			T item = original[list[index]];
			if (!list2.Contains(item))
			{
				list2.Add(item);
				list.RemoveAt(index);
				num2++;
			}
		}
		return list2;
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0000D710 File Offset: 0x0000B910
	public static void CopyFolder(string sourcePath, string destPath)
	{
		if (Directory.Exists(sourcePath))
		{
			if (!Directory.Exists(destPath))
			{
				try
				{
					Directory.CreateDirectory(destPath);
				}
				catch (Exception ex)
				{
					Debug.LogError("创建失败" + ex.Message);
				}
			}
			new List<string>(Directory.GetFiles(sourcePath)).ForEach(delegate(string c)
			{
				if (!c.EndsWith(".meta"))
				{
					string destFileName = Path.Combine(destPath, Path.GetFileName(c));
					File.Copy(c, destFileName, true);
				}
			});
			new List<string>(Directory.GetDirectories(sourcePath)).ForEach(delegate(string c)
			{
				string destPath2 = Path.Combine(destPath, Path.GetFileName(c));
				Util.CopyFolder(c, destPath2);
			});
			return;
		}
		Debug.LogError("源目录不存在");
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
	public static string GetMD5HashFromFile(byte[] file)
	{
		string result;
		try
		{
			byte[] array = new MD5CryptoServiceProvider().ComputeHash(file);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			result = stringBuilder.ToString();
		}
		catch (Exception ex)
		{
			throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
		}
		return result;
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000D830 File Offset: 0x0000BA30
	public static bool CheckInputPositionIsLarge(Vector2 v2)
	{
		return Camera.main.pixelRect.Contains(v2);
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000D855 File Offset: 0x0000BA55
	public static bool CheckArray2Out<T>(this T[,] t, int x, int y)
	{
		return x < 0 || y < 0 || (x >= t.GetLength(0) || y >= t.GetLength(1));
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000D878 File Offset: 0x0000BA78
	public static int GetWeightIndex(string[] array)
	{
		int[] array2 = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i].ToInt32();
		}
		return Util.GetWeightIndex(array2);
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
	public static int GetWeightIndex(int[] array)
	{
		int num = 0;
		foreach (int num2 in array)
		{
			num += num2;
		}
		int num3 = Random.Range(0, num) + 1;
		int num4 = 0;
		for (int j = 0; j < array.Length; j++)
		{
			num4 += array[j];
			if (num3 <= num4)
			{
				return j;
			}
		}
		return 0;
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000D90C File Offset: 0x0000BB0C
	public static long GetNowTimeTick()
	{
		return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000000L;
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000D940 File Offset: 0x0000BB40
	public static DateTime GetTime(this long tick)
	{
		string text = tick.ToString();
		if (text.Length > 10)
		{
			text = text.Substring(0, 10);
		}
		DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
		long ticks = long.Parse(text + "0000000");
		TimeSpan value = new TimeSpan(ticks);
		return dateTime.Add(value);
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000D9A1 File Offset: 0x0000BBA1
	public static byte[] DataToBytes<T>(this T v)
	{
		return MemoryPackSerializer.Serialize<T>(v, null);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000D9AB File Offset: 0x0000BBAB
	public static T BytesToData<T>(this byte[] v)
	{
		return MemoryPackSerializer.Deserialize<T>(v, null);
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000D9BC File Offset: 0x0000BBBC
	public static Vector2 GetPointByRadian(float x, float y, float angle)
	{
		Vector2 result = default(Vector2);
		float f = 0.017453292f * angle;
		result.x = x * Mathf.Cos(f) - y * Mathf.Sin(f);
		result.y = x * Mathf.Sin(f) + y * Mathf.Cos(f);
		return result;
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000DA0A File Offset: 0x0000BC0A
	public static bool NewCheckYuanXing(Vector3 checkPos, Vector3 targetPos, float checkDistance, bool isShowTip = false)
	{
		return Mathf.Pow(targetPos.x - checkPos.x, 2f) + Mathf.Pow(targetPos.z - checkPos.z, 2f) < checkDistance * checkDistance;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000DA44 File Offset: 0x0000BC44
	public static bool NewCheckShanXing(Vector3 startPos, Vector3 targetPos, float checkRotation, float checkDistance, float localRotationEulerY, bool isShowTip = false)
	{
		checkRotation /= 2f;
		float num = targetPos.x - startPos.x;
		float num2 = targetPos.z - startPos.z;
		if (Mathf.Pow(num, 2f) + Mathf.Pow(num2, 2f) < checkDistance * checkDistance)
		{
			float num3 = Mathf.Atan2(num, num2) * 180f / 3.1415927f;
			if (Mathf.Abs(num3 - localRotationEulerY) < checkRotation || Mathf.Abs(num3 - localRotationEulerY) > 360f - checkRotation)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
	public static bool NewCheckJuXing(Vector3 startPos, float rotation, float width, float height, Vector3 targetPos, float targetrange, bool isCenter = false, bool isShowTip = false)
	{
		float x = targetPos.x - startPos.x;
		float y = targetPos.z - startPos.z;
		Vector2 pointByRadian = Util.GetPointByRadian(x, y, rotation);
		float num = width / 2f;
		float num2 = height / 2f;
		float num3;
		float num4;
		float num5;
		float num6;
		if (isCenter)
		{
			num3 = -num;
			num4 = num;
			num5 = -num2;
			num6 = num2;
		}
		else
		{
			num3 = -num;
			num4 = num;
			num5 = 0f;
			num6 = height;
		}
		float x2 = pointByRadian.x;
		float y2 = pointByRadian.y;
		float num7 = Mathf.Min(Mathf.Abs(num3 - x2), Mathf.Abs(num4 - x2));
		float num8 = Mathf.Min(Mathf.Abs(num5 - y2), Mathf.Abs(num6 - y2));
		if (num7 * num7 + num8 * num8 < targetrange * targetrange)
		{
			return true;
		}
		float num9 = (num3 + num4) / 2f;
		float num10 = (num5 + num6) / 2f;
		return (Mathf.Abs(num9 - x2) < Mathf.Abs(num4 - num3) / 2f + targetrange && Mathf.Abs(y2 - num10) < Mathf.Abs(num6 - num5) / 2f) || (Mathf.Abs(num10 - y2) < Mathf.Abs(num6 - num5) / 2f + targetrange && Mathf.Abs(x2 - num9) < Mathf.Abs(num4 - num3) / 2f);
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0000DC14 File Offset: 0x0000BE14
	public static long OnLocalPlayerHit(RoleBase attackRole, RoleBase hitRole, double damage, float hitEulerAngleY, AttackType attackType, bool isAttackWeek)
	{
		if (hitRole == null || attackRole == null)
		{
			return 0L;
		}
		long num = hitRole.OnHit(attackRole, damage, hitEulerAngleY, attackType, isAttackWeek);
		if (num > 0L)
		{
			Game.UI.GetUI<UI_PlayerState>().ShowDamageNum(num, hitRole.GetAttackPos(), isAttackWeek, attackType);
		}
		PlayerBase playerBase = attackRole as PlayerBase;
		if (playerBase != null && attackType == AttackType.Normal && playerBase.attackEffectTime > 0)
		{
			for (int i = 0; i < playerBase.attackEffectTime; i++)
			{
				long num2 = hitRole.OnHit(attackRole, 0.0, hitEulerAngleY, attackType, isAttackWeek);
				if (num2 > 0L && attackRole.roleType != RoleType.King)
				{
					Game.UI.GetUI<UI_PlayerState>().ShowDamageNum(num2, hitRole.GetAttackPos(), isAttackWeek, attackType);
				}
			}
		}
		return num;
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0000DCCC File Offset: 0x0000BECC
	public static float GetV2Angle(Vector3 trackPos, Vector3 myPos)
	{
		Vector2 vector = new Vector2(trackPos.x - myPos.x, trackPos.z - myPos.z);
		return Mathf.Atan2(vector.x, vector.y) * 57.29578f;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0000DD11 File Offset: 0x0000BF11
	public static float GetV2Distance(Vector3 trackPos, Vector3 myPos)
	{
		return Mathf.Sqrt(Mathf.Pow(trackPos.x - myPos.x, 2f) + Mathf.Pow(trackPos.z - myPos.z, 2f));
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0000DD48 File Offset: 0x0000BF48
	public static void OnLocalStartUseSkill(ActiveSkillEnum activeSkillName, PlayerBase attackPlayer, int skillBookId)
	{
		if (!attackPlayer.gameObject.activeSelf || attackPlayer.hp <= 0L)
		{
			return;
		}
		if (attackPlayer.RoleState != RoleState.Idle && attackPlayer.RoleState != RoleState.Run && attackPlayer.RoleState != RoleState.Attack)
		{
			return;
		}
		ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillName];
		int realCost = Util.GetRealCost(attackPlayer, activeSkillData.cost);
		if (GameHelperClient.localPlayer.mp < realCost)
		{
			Util.ShowTips("tip_noMp");
			return;
		}
		if (activeSkillData.indicator.Equals(IndicatorDefine.None))
		{
			Game.EffectManager.HideSpellGroundTip();
			Util.OnLocalCmdSkill(attackPlayer, activeSkillName, Vector3.zero, activeSkillData, 0f, -1, skillBookId);
			return;
		}
		if (activeSkillData.indicator.Equals(IndicatorDefine.Range))
		{
			Game.EffectManager.ShowSpellGroundTip(activeSkillName, activeSkillData.range, 0, skillBookId);
			return;
		}
		if (activeSkillData.indicator.Equals(IndicatorDefine.Vector))
		{
			Game.EffectManager.HideSpellGroundTip();
			Vector3 mouseGroundPos = Game.EffectManager.GetMouseGroundPos();
			Vector2 movePos = new Vector2(mouseGroundPos.x - attackPlayer.MyTransform.position.x, mouseGroundPos.z - attackPlayer.MyTransform.position.z);
			float moveAngle = attackPlayer.GetMoveAngle(movePos);
			attackPlayer.SetSyncRotationY(moveAngle);
			Util.OnLocalCmdSkill(attackPlayer, activeSkillName, attackPlayer.MyTransform.position, activeSkillData, moveAngle, -1, skillBookId);
			return;
		}
		if (activeSkillData.indicator.Equals(IndicatorDefine.Switch))
		{
			Game.EffectManager.HideSpellGroundTip();
			Util.OnLocalCmdSkill(attackPlayer, activeSkillName, Vector3.zero, activeSkillData, 0f, -1, skillBookId);
			return;
		}
		if (activeSkillData.indicator.Equals(IndicatorDefine.Target))
		{
			Game.EffectManager.ShowSpellGroundTip(activeSkillName, activeSkillData.range, 1, skillBookId);
		}
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
	public static bool OnLocalCmdSkill(PlayerBase attackPlayer, ActiveSkillEnum activeSkillEnum, Vector3 targetPos, ActiveSkillData activeSkillData, float attackRotation, int targetRoleId, int skillBookId)
	{
		if (!attackPlayer.gameObject.activeSelf || attackPlayer.hp <= 0L)
		{
			return false;
		}
		if (attackPlayer.RoleState != RoleState.Idle && attackPlayer.RoleState != RoleState.Run && attackPlayer.RoleState != RoleState.Attack)
		{
			return false;
		}
		if (attackPlayer.mp >= Util.GetRealCost(attackPlayer, activeSkillData.cost))
		{
			if (activeSkillEnum - ActiveSkillEnum.Hero_DrawKnife > 1 && activeSkillEnum != ActiveSkillEnum.Hero_Roll)
			{
				switch (activeSkillEnum)
				{
				case ActiveSkillEnum.ChickenDance:
					goto IL_83;
				case ActiveSkillEnum.SoulDevourer:
				{
					if (attackPlayer.henShinSkillId > 0)
					{
						if (attackPlayer.isLocalPlayer)
						{
							Util.ShowTips("该状态无法使用！");
						}
						return false;
					}
					PlayerKoboldMode playerKoboldMode = attackPlayer.RoleModeBase as PlayerKoboldMode;
					if (playerKoboldMode != null && playerKoboldMode.IsUseSkillAttack)
					{
						return false;
					}
					if (attackPlayer.useSkillEvent != null)
					{
						activeSkillEnum = attackPlayer.useSkillEvent(activeSkillEnum);
					}
					attackPlayer.CmdCreateSkill(activeSkillEnum, targetPos, attackRotation, targetRoleId, skillBookId);
					goto IL_1A5;
				}
				case ActiveSkillEnum.PlantBomb:
					if (attackPlayer.useSkillEvent != null)
					{
						activeSkillEnum = attackPlayer.useSkillEvent(activeSkillEnum);
					}
					Game.AudioManager.PlaySkillAudio(attackPlayer.RoleModeBase.skillSoundType, attackPlayer.MyTransform.position);
					attackPlayer.CmdCreateSkillBySyncData(activeSkillEnum, attackPlayer.MyTransform.position, attackPlayer.STA, attackRotation, targetRoleId, skillBookId);
					goto IL_1A5;
				}
				if (attackPlayer.useSkillEvent != null)
				{
					activeSkillEnum = attackPlayer.useSkillEvent(activeSkillEnum);
				}
				attackPlayer.UpdateRoleState(RoleState.Skill);
				attackPlayer.CmdCreateSkill(activeSkillEnum, targetPos, attackRotation, targetRoleId, skillBookId);
				goto IL_1A5;
			}
			IL_83:
			if (attackPlayer.henShinSkillId > 0)
			{
				if (attackPlayer.isLocalPlayer)
				{
					Util.ShowTips("该状态无法使用！");
				}
				return false;
			}
			attackPlayer.UpdateRoleState(RoleState.Skill2);
			if (attackPlayer.useSkillEvent != null)
			{
				activeSkillEnum = attackPlayer.useSkillEvent(activeSkillEnum);
			}
			IL_1A5:
			attackPlayer.AddMp(-Util.GetRealCost(attackPlayer, activeSkillData.cost));
			if (attackPlayer.isLocalPlayer)
			{
				Game.UI.GetUI<UI_PlayerState>().SetCDTime();
			}
			return true;
		}
		return false;
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0000E0D3 File Offset: 0x0000C2D3
	public static int GetRealCost(PlayerBase playerBase, int cost)
	{
		if (!Mathf.Approximately(playerBase.skillMpUsed, 0f))
		{
			return Mathf.RoundToInt((float)cost * (1f + playerBase.skillMpUsed));
		}
		return cost;
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0000E100 File Offset: 0x0000C300
	public static long GetSkillDamage(ActiveSkillData activeSkillData, RoleBase roleBase)
	{
		double num = 0.0;
		if (activeSkillData.damageType == 0)
		{
			num = (double)((float)activeSkillData.damageBase + (float)roleBase.FinalAttackPower * activeSkillData.damageValue);
		}
		else if (activeSkillData.damageType == 1)
		{
			num = (double)((float)activeSkillData.damageBase + (float)roleBase.STR * activeSkillData.damageValue);
		}
		else if (activeSkillData.damageType == 2)
		{
			num = (double)((float)activeSkillData.damageBase + (float)roleBase.AGI * activeSkillData.damageValue);
		}
		else if (activeSkillData.damageType == 3)
		{
			num = (double)((float)activeSkillData.damageBase + (float)roleBase.STA * activeSkillData.damageValue);
		}
		else if (activeSkillData.damageType == 4)
		{
			num = (double)((float)activeSkillData.damageBase + (float)roleBase.maxHp * activeSkillData.damageValue);
		}
		if (roleBase.roleType == RoleType.Player || roleBase.roleType == RoleType.King)
		{
			PlayerBase playerBase = roleBase as PlayerBase;
			if (playerBase != null)
			{
				float num2 = playerBase.SkillExDamageAll;
				if (activeSkillData.attribute == SkillAttribute.None)
				{
					num2 += playerBase.skillNoneAdd;
				}
				else if (activeSkillData.attribute == SkillAttribute.Fire)
				{
					num2 += playerBase.skillFireAdd;
				}
				else if (activeSkillData.attribute == SkillAttribute.Ice)
				{
					num2 += playerBase.skillIceAdd;
				}
				else if (activeSkillData.attribute == SkillAttribute.Lighting)
				{
					num2 += playerBase.skillLightingAdd;
				}
				num += num * (double)num2;
			}
		}
		return ConstDefine.ClampBattleValue(num);
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000E248 File Offset: 0x0000C448
	public static Vector2 GetScreenPosition(Vector3 pos)
	{
		Vector3 vector = Game.CameraManager.camera.WorldToViewportPoint(pos);
		Vector2 screenSize = Util.GetScreenSize();
		return new Vector2((vector.x - 0.5f) * screenSize.x, (vector.y - 0.5f) * screenSize.y + 20f);
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
	private static Vector2 GetScreenSize()
	{
		float num = 1.7777778f;
		if ((float)Screen.width / (float)Screen.height > num)
		{
			return new Vector2(1080f * (float)Screen.width / (float)Screen.height, 1080f);
		}
		return new Vector2(1920f, 1920f * (float)Screen.height / (float)Screen.width);
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0000E300 File Offset: 0x0000C500
	public static long GetPassSkillDamage(RoleBase roleBase, SkillAttribute skillAttribute, double finalDamage, bool isBuff = false)
	{
		PlayerBase playerBase = roleBase as PlayerBase;
		if (playerBase != null)
		{
			float num = playerBase.SkillExDamageAll;
			if (skillAttribute == SkillAttribute.None)
			{
				num += playerBase.skillNoneAdd;
			}
			else if (skillAttribute == SkillAttribute.Fire)
			{
				num += playerBase.skillFireAdd;
			}
			else if (skillAttribute == SkillAttribute.Ice)
			{
				num += playerBase.skillIceAdd;
			}
			else if (skillAttribute == SkillAttribute.Lighting)
			{
				num += playerBase.skillLightingAdd;
			}
			if (!isBuff)
			{
				finalDamage += finalDamage * (double)num;
			}
		}
		return ConstDefine.ClampBattleValue(finalDamage);
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0000E368 File Offset: 0x0000C568
	public static void OnLocalPlayerPickItem(ItemType itemType, int itemNum = 0)
	{
		GameHelperClient.localPlayer.PickItem(itemType, itemNum);
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0000E378 File Offset: 0x0000C578
	public static void ShowTips(string str)
	{
		string data = Game.Language.Get(str, "");
		Game.UI.OpenUI<UI_Tips>(data);
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
	public static bool CheckCanRoguelike()
	{
		UI_Roguelike ui = Game.UI.GetUI<UI_Roguelike>();
		UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
		if ((ui != null && ui.isOpen) || (ui2 != null && ui2.IsSwitchSkill))
		{
			Util.ShowTips("请先选择一个技能！");
			return false;
		}
		return true;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0000E3EA File Offset: 0x0000C5EA
	public static void ShowTipsNoLanguage(string str)
	{
		Game.UI.OpenUI<UI_Tips>(str);
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
	public static void CreateSkill(uint skillId, ActiveSkillEnum activeSkillType, Vector3 pos, RoleBase attackRole, int syncData, float attackRotation, int targetRoleId, int skillBookId)
	{
		Dictionary<uint, ActiveSkillBase> skills = Game.SkillManager.skills;
		ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		float num = activeSkillData.range;
		if (attackRole.roleType == RoleType.Player)
		{
			num += num * (attackRole as PlayerBase).skillRange;
		}
		MySystemEvent.Instance.DispatchMessage<RoleBase>(45, attackRole);
		if (activeSkillType <= ActiveSkillEnum.S_PoisionAoe)
		{
			switch (activeSkillType)
			{
			case ActiveSkillEnum.D_SpellThunder:
			case ActiveSkillEnum.C_SpellThunder:
				break;
			case ActiveSkillEnum.D_SwordMove:
			case ActiveSkillEnum.C_SwordMove:
				goto IL_AFA;
			case ActiveSkillEnum.D_Revive:
			{
				ReviveActiveSkill reviveActiveSkill = new ReviveActiveSkill();
				reviveActiveSkill.skillId = skillId;
				reviveActiveSkill.InitSkill(activeSkillType, attackRole, EffectDefine.HealBuff, activeSkillData);
				skills.Add(skillId, reviveActiveSkill);
				return;
			}
			case ActiveSkillEnum.D_SummonKight:
				if (attackRole.HasAuthority)
				{
					attackRole.StartSummon(EnemyType.Summon_Knight_D, pos, GameHelperClient.localPlayer.netId, 1f + (float)attackRole.AGI * activeSkillData.damageExValue[0], (long)Mathf.RoundToInt(activeSkillData.damageExValue[1] + (float)((int)((float)attackRole.STA * activeSkillData.damageExValue[2]))), (int)(activeSkillData.damageExValue[4] + (float)attackRole.STR * activeSkillData.damageExValue[3]), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
					return;
				}
				return;
			case ActiveSkillEnum.D_BlackHole:
			case ActiveSkillEnum.C_BlackHole:
				goto IL_9DB;
			case ActiveSkillEnum.D_ContinuousLight:
			case ActiveSkillEnum.C_ContinuousLight:
				goto IL_BEC;
			case ActiveSkillEnum.D_Flameshrower:
			case ActiveSkillEnum.C_Flameshrower:
				goto IL_C46;
			case ActiveSkillEnum.D_SurroundingFire:
			case ActiveSkillEnum.C_SurroundingFire:
				goto IL_C9F;
			case ActiveSkillEnum.D_KingsTreasure:
			case ActiveSkillEnum.C_KingsTreasure:
				goto IL_CD4;
			case ActiveSkillEnum.D_Whirlwind:
			case ActiveSkillEnum.C_Whirlwind:
				goto IL_D0A;
			case ActiveSkillEnum.D_FireTornado:
			case ActiveSkillEnum.C_FireTornado:
				goto IL_DB9;
			case ActiveSkillEnum.D_WindBreakSlash:
			case ActiveSkillEnum.C_WindBreakSlash:
				goto IL_1039;
			case (ActiveSkillEnum)12:
			case (ActiveSkillEnum)36:
			case (ActiveSkillEnum)37:
			case (ActiveSkillEnum)38:
			case (ActiveSkillEnum)39:
			case (ActiveSkillEnum)40:
			case (ActiveSkillEnum)41:
			case (ActiveSkillEnum)42:
			case (ActiveSkillEnum)43:
			case (ActiveSkillEnum)44:
			case (ActiveSkillEnum)45:
			case (ActiveSkillEnum)46:
			case (ActiveSkillEnum)47:
			case (ActiveSkillEnum)48:
			case (ActiveSkillEnum)49:
			case (ActiveSkillEnum)50:
			case (ActiveSkillEnum)51:
			case (ActiveSkillEnum)52:
			case (ActiveSkillEnum)53:
			case (ActiveSkillEnum)54:
			case (ActiveSkillEnum)55:
			case (ActiveSkillEnum)56:
			case (ActiveSkillEnum)57:
			case (ActiveSkillEnum)58:
			case (ActiveSkillEnum)59:
			case (ActiveSkillEnum)60:
			case (ActiveSkillEnum)61:
			case (ActiveSkillEnum)62:
			case (ActiveSkillEnum)63:
			case (ActiveSkillEnum)64:
			case (ActiveSkillEnum)65:
			case (ActiveSkillEnum)66:
			case (ActiveSkillEnum)67:
			case (ActiveSkillEnum)68:
			case (ActiveSkillEnum)69:
			case (ActiveSkillEnum)70:
			case (ActiveSkillEnum)71:
			case (ActiveSkillEnum)72:
			case (ActiveSkillEnum)73:
			case (ActiveSkillEnum)74:
			case (ActiveSkillEnum)75:
			case (ActiveSkillEnum)76:
			case (ActiveSkillEnum)77:
			case (ActiveSkillEnum)78:
			case (ActiveSkillEnum)79:
			case (ActiveSkillEnum)80:
			case (ActiveSkillEnum)81:
			case (ActiveSkillEnum)82:
			case (ActiveSkillEnum)83:
			case (ActiveSkillEnum)84:
			case (ActiveSkillEnum)85:
			case (ActiveSkillEnum)86:
			case (ActiveSkillEnum)87:
			case (ActiveSkillEnum)88:
			case (ActiveSkillEnum)89:
			case (ActiveSkillEnum)90:
			case (ActiveSkillEnum)91:
			case (ActiveSkillEnum)92:
			case (ActiveSkillEnum)93:
			case (ActiveSkillEnum)94:
			case (ActiveSkillEnum)95:
			case (ActiveSkillEnum)96:
			case (ActiveSkillEnum)97:
			case (ActiveSkillEnum)98:
			case (ActiveSkillEnum)99:
			case (ActiveSkillEnum)102:
			case (ActiveSkillEnum)112:
				return;
			case ActiveSkillEnum.D_Sacrifice:
			case ActiveSkillEnum.C_Sacrifice:
				goto IL_1091;
			case ActiveSkillEnum.D_SuperSaiyan:
			case ActiveSkillEnum.C_SuperSaiyan:
				goto IL_AC3;
			case ActiveSkillEnum.D_KamehamehaWave:
			case ActiveSkillEnum.C_KamehamehaWave:
				goto IL_C7B;
			case ActiveSkillEnum.D_Henshin:
			case ActiveSkillEnum.C_Henshin:
				goto IL_10C8;
			case ActiveSkillEnum.D_Rachel:
			case ActiveSkillEnum.C_Rachel:
				goto IL_10F0;
			case ActiveSkillEnum.D_Rasengan:
			case ActiveSkillEnum.C_Rasengan:
				goto IL_1126;
			case ActiveSkillEnum.D_IceWall:
			case ActiveSkillEnum.C_IceWall:
				goto IL_115C;
			case ActiveSkillEnum.D_BlackCoffin:
			case ActiveSkillEnum.C_BlackCoffin:
				goto IL_1180;
			case ActiveSkillEnum.D_FireStep:
			case ActiveSkillEnum.C_FireStep:
				goto IL_11A4;
			case ActiveSkillEnum.D_SwordOfLight:
			case ActiveSkillEnum.C_SwordOfLight:
				goto IL_11D9;
			case ActiveSkillEnum.D_LightningChain:
			case ActiveSkillEnum.C_LightningChain:
				goto IL_120F;
			case ActiveSkillEnum.D_LandMine:
			case ActiveSkillEnum.C_LandMine:
				goto IL_1245;
			case ActiveSkillEnum.D_DarkMonster:
			case ActiveSkillEnum.C_DarkMonster:
				goto IL_12A9;
			case ActiveSkillEnum.D_HenshinLight:
			case ActiveSkillEnum.C_HenshinLight:
				goto IL_12CE;
			case ActiveSkillEnum.D_Peashooter:
			case ActiveSkillEnum.C_Peashooter:
				goto IL_1346;
			case ActiveSkillEnum.D_BloodSacrifice:
			case ActiveSkillEnum.C_BloodSacrifice:
				goto IL_13CB;
			case ActiveSkillEnum.D_Alchemy:
			case ActiveSkillEnum.C_Alchemy:
				goto IL_13EF;
			case ActiveSkillEnum.D_Execution:
			case ActiveSkillEnum.C_Execution:
				goto IL_1415;
			case ActiveSkillEnum.D_Sunflower:
			case ActiveSkillEnum.C_Sunflower:
				goto IL_143D;
			case ActiveSkillEnum.D_SummomTree:
				if (attackRole.HasAuthority)
				{
					attackRole.StartSummon(EnemyType.Summon_Tree_D, pos, GameHelperClient.localPlayer.netId, 1f + (float)attackRole.AGI * 0.001f, (long)Mathf.RoundToInt(activeSkillData.damageExValue[1] + (float)((int)((float)attackRole.STA * activeSkillData.damageExValue[2]))), (int)((float)attackRole.STR * 5f), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
					return;
				}
				return;
			case ActiveSkillEnum.D_AvatarDodge:
			case ActiveSkillEnum.C_AvatarDodge:
				goto IL_16E3;
			case ActiveSkillEnum.D_DeathCyclone:
			case ActiveSkillEnum.C_DeathCyclone:
				goto IL_D3F;
			case ActiveSkillEnum.D_PoisionAoe:
			case ActiveSkillEnum.C_PoisionAoe:
				goto IL_A1B;
			case ActiveSkillEnum.C_SummonKight:
				if (attackRole.HasAuthority)
				{
					attackRole.StartSummon(EnemyType.Summon_Knight_C, pos, GameHelperClient.localPlayer.netId, 1f + (float)attackRole.AGI * activeSkillData.damageExValue[0], (long)Mathf.RoundToInt(activeSkillData.damageExValue[1] + (float)((int)((float)attackRole.STA * activeSkillData.damageExValue[2]))), (int)(activeSkillData.damageExValue[4] + (float)attackRole.STR * activeSkillData.damageExValue[3]), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
					return;
				}
				return;
			case ActiveSkillEnum.C_SummomTree:
				if (attackRole.HasAuthority)
				{
					attackRole.StartSummon(EnemyType.Summon_Tree_C, pos, GameHelperClient.localPlayer.netId, 1f + (float)attackRole.AGI * 0.001f, (long)Mathf.RoundToInt(activeSkillData.damageExValue[1] + (float)((int)((float)attackRole.STA * activeSkillData.damageExValue[2]))), (int)((float)attackRole.STR * 8f), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
					return;
				}
				return;
			default:
				switch (activeSkillType)
				{
				case ActiveSkillEnum.B_SpellThunder:
					goto IL_5EA;
				case ActiveSkillEnum.B_SwordMove:
					goto IL_AFA;
				case (ActiveSkillEnum)202:
				case (ActiveSkillEnum)212:
					return;
				case ActiveSkillEnum.B_SummonKight:
					if (attackRole.HasAuthority)
					{
						attackRole.StartSummon(EnemyType.Summon_Knight_B, pos, GameHelperClient.localPlayer.netId, 1f + (float)attackRole.AGI * activeSkillData.damageExValue[0], (long)Mathf.RoundToInt(activeSkillData.damageExValue[1] + (float)((int)((float)attackRole.STA * activeSkillData.damageExValue[2]))), (int)(activeSkillData.damageExValue[4] + (float)attackRole.STR * activeSkillData.damageExValue[3]), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
						return;
					}
					return;
				case ActiveSkillEnum.B_BlackHole:
					goto IL_9DB;
				case ActiveSkillEnum.B_ContinuousLight:
					goto IL_BEC;
				case ActiveSkillEnum.B_Flameshrower:
					goto IL_C46;
				case ActiveSkillEnum.B_SurroundingFire:
					goto IL_C9F;
				case ActiveSkillEnum.B_KingsTreasure:
					goto IL_CD4;
				case ActiveSkillEnum.B_Whirlwind:
					goto IL_D0A;
				case ActiveSkillEnum.B_FireTornado:
					goto IL_DB9;
				case ActiveSkillEnum.B_WindBreakSlash:
					goto IL_1039;
				case ActiveSkillEnum.B_Sacrifice:
					goto IL_1091;
				case ActiveSkillEnum.B_SuperSaiyan:
					goto IL_AC3;
				case ActiveSkillEnum.B_KamehamehaWave:
					goto IL_C7B;
				case ActiveSkillEnum.B_Henshin:
					goto IL_10C8;
				case ActiveSkillEnum.B_Rachel:
					goto IL_10F0;
				case ActiveSkillEnum.B_Rasengan:
					goto IL_1126;
				case ActiveSkillEnum.B_IceWall:
					goto IL_115C;
				case ActiveSkillEnum.B_BlackCoffin:
					goto IL_1180;
				case ActiveSkillEnum.B_FireStep:
					goto IL_11A4;
				case ActiveSkillEnum.B_SwordOfLight:
					goto IL_11D9;
				case ActiveSkillEnum.B_LightningChain:
					goto IL_120F;
				case ActiveSkillEnum.B_LandMine:
					goto IL_1245;
				case ActiveSkillEnum.B_DarkMonster:
					goto IL_12A9;
				case ActiveSkillEnum.B_HenshinLight:
					goto IL_12CE;
				case ActiveSkillEnum.B_Peashooter:
					goto IL_1346;
				case ActiveSkillEnum.B_BloodSacrifice:
					goto IL_13CB;
				case ActiveSkillEnum.B_Alchemy:
					goto IL_13EF;
				case ActiveSkillEnum.B_Execution:
					goto IL_1415;
				case ActiveSkillEnum.B_Sunflower:
					goto IL_143D;
				case ActiveSkillEnum.B_SummomTree:
					break;
				case ActiveSkillEnum.B_AvatarDodge:
					goto IL_16E3;
				case ActiveSkillEnum.B_DeathCyclone:
					goto IL_D3F;
				case ActiveSkillEnum.B_PoisionAoe:
					goto IL_A1B;
				default:
					switch (activeSkillType)
					{
					case ActiveSkillEnum.A_SpellThunder:
					case ActiveSkillEnum.S_SpellThunder:
						goto IL_5EA;
					case ActiveSkillEnum.A_SwordMove:
					case ActiveSkillEnum.S_SwordMove:
						goto IL_AFA;
					case (ActiveSkillEnum)302:
					case (ActiveSkillEnum)312:
					case (ActiveSkillEnum)336:
					case (ActiveSkillEnum)337:
					case (ActiveSkillEnum)338:
					case (ActiveSkillEnum)339:
					case (ActiveSkillEnum)340:
					case (ActiveSkillEnum)341:
					case (ActiveSkillEnum)342:
					case (ActiveSkillEnum)343:
					case (ActiveSkillEnum)344:
					case (ActiveSkillEnum)345:
					case (ActiveSkillEnum)346:
					case (ActiveSkillEnum)347:
					case (ActiveSkillEnum)348:
					case (ActiveSkillEnum)349:
					case (ActiveSkillEnum)350:
					case (ActiveSkillEnum)351:
					case (ActiveSkillEnum)352:
					case (ActiveSkillEnum)353:
					case (ActiveSkillEnum)354:
					case (ActiveSkillEnum)355:
					case (ActiveSkillEnum)356:
					case (ActiveSkillEnum)357:
					case (ActiveSkillEnum)358:
					case (ActiveSkillEnum)359:
					case (ActiveSkillEnum)360:
					case (ActiveSkillEnum)361:
					case (ActiveSkillEnum)362:
					case (ActiveSkillEnum)363:
					case (ActiveSkillEnum)364:
					case (ActiveSkillEnum)365:
					case (ActiveSkillEnum)366:
					case (ActiveSkillEnum)367:
					case (ActiveSkillEnum)368:
					case (ActiveSkillEnum)369:
					case (ActiveSkillEnum)370:
					case (ActiveSkillEnum)371:
					case (ActiveSkillEnum)372:
					case (ActiveSkillEnum)373:
					case (ActiveSkillEnum)374:
					case (ActiveSkillEnum)375:
					case (ActiveSkillEnum)376:
					case (ActiveSkillEnum)377:
					case (ActiveSkillEnum)378:
					case (ActiveSkillEnum)379:
					case (ActiveSkillEnum)380:
					case (ActiveSkillEnum)381:
					case (ActiveSkillEnum)382:
					case (ActiveSkillEnum)383:
					case (ActiveSkillEnum)384:
					case (ActiveSkillEnum)385:
					case (ActiveSkillEnum)386:
					case (ActiveSkillEnum)387:
					case (ActiveSkillEnum)388:
					case (ActiveSkillEnum)389:
					case (ActiveSkillEnum)390:
					case (ActiveSkillEnum)391:
					case (ActiveSkillEnum)392:
					case (ActiveSkillEnum)393:
					case (ActiveSkillEnum)394:
					case (ActiveSkillEnum)395:
					case (ActiveSkillEnum)396:
					case (ActiveSkillEnum)397:
					case (ActiveSkillEnum)398:
					case (ActiveSkillEnum)399:
					case (ActiveSkillEnum)402:
						return;
					case ActiveSkillEnum.A_SummonKight:
						if (attackRole.HasAuthority)
						{
							attackRole.StartSummon(EnemyType.Summon_Knight_A, pos, GameHelperClient.localPlayer.netId, 1f + (float)attackRole.AGI * activeSkillData.damageExValue[0], (long)Mathf.RoundToInt(activeSkillData.damageExValue[1] + (float)((int)((float)attackRole.STA * activeSkillData.damageExValue[2]))), (int)(activeSkillData.damageExValue[4] + (float)attackRole.STR * activeSkillData.damageExValue[3]), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
							return;
						}
						return;
					case ActiveSkillEnum.A_BlackHole:
					case ActiveSkillEnum.S_BlackHole:
						goto IL_9DB;
					case ActiveSkillEnum.A_ContinuousLight:
					case ActiveSkillEnum.S_ContinuousLight:
						goto IL_BEC;
					case ActiveSkillEnum.A_Flameshrower:
					case ActiveSkillEnum.S_Flameshrower:
						goto IL_C46;
					case ActiveSkillEnum.A_SurroundingFire:
					case ActiveSkillEnum.S_SurroundingFire:
						goto IL_C9F;
					case ActiveSkillEnum.A_KingsTreasure:
					case ActiveSkillEnum.S_KingsTreasure:
						goto IL_CD4;
					case ActiveSkillEnum.A_Whirlwind:
					case ActiveSkillEnum.S_Whirlwind:
						goto IL_D0A;
					case ActiveSkillEnum.A_FireTornado:
					case ActiveSkillEnum.S_FireTornado:
						goto IL_DB9;
					case ActiveSkillEnum.A_WindBreakSlash:
					case ActiveSkillEnum.S_WindBreakSlash:
						goto IL_1039;
					case ActiveSkillEnum.A_Sacrifice:
					case ActiveSkillEnum.S_Sacrifice:
						goto IL_1091;
					case ActiveSkillEnum.A_SuperSaiyan:
					case ActiveSkillEnum.S_SuperSaiyan:
						goto IL_AC3;
					case ActiveSkillEnum.A_KamehamehaWave:
					case ActiveSkillEnum.S_KamehamehaWave:
						goto IL_C7B;
					case ActiveSkillEnum.A_Henshin:
					case ActiveSkillEnum.S_Henshin:
						goto IL_10C8;
					case ActiveSkillEnum.A_Rachel:
					case ActiveSkillEnum.S_Rachel:
						goto IL_10F0;
					case ActiveSkillEnum.A_Rasengan:
					case ActiveSkillEnum.S_Rasengan:
						goto IL_1126;
					case ActiveSkillEnum.A_IceWall:
					case ActiveSkillEnum.S_IceWall:
						goto IL_115C;
					case ActiveSkillEnum.A_BlackCoffin:
					case ActiveSkillEnum.S_BlackCoffin:
						goto IL_1180;
					case ActiveSkillEnum.A_FireStep:
					case ActiveSkillEnum.S_FireStep:
						goto IL_11A4;
					case ActiveSkillEnum.A_SwordOfLight:
					case ActiveSkillEnum.S_SwordOfLight:
						goto IL_11D9;
					case ActiveSkillEnum.A_LightningChain:
					case ActiveSkillEnum.S_LightningChain:
						goto IL_120F;
					case ActiveSkillEnum.A_LandMine:
					case ActiveSkillEnum.S_LandMine:
						goto IL_1245;
					case ActiveSkillEnum.A_DarkMonster:
					case ActiveSkillEnum.S_DarkMonster:
						goto IL_12A9;
					case ActiveSkillEnum.A_HenshinLight:
					case ActiveSkillEnum.S_HenshinLight:
						goto IL_12CE;
					case ActiveSkillEnum.A_Peashooter:
					case ActiveSkillEnum.S_Peashooter:
						goto IL_1346;
					case ActiveSkillEnum.A_BloodSacrifice:
					case ActiveSkillEnum.S_BloodSacrifice:
						goto IL_13CB;
					case ActiveSkillEnum.A_Alchemy:
					case ActiveSkillEnum.S_Alchemy:
						goto IL_13EF;
					case ActiveSkillEnum.A_Execution:
					case ActiveSkillEnum.S_Execution:
						goto IL_1415;
					case ActiveSkillEnum.A_Sunflower:
					case ActiveSkillEnum.S_Sunflower:
						goto IL_143D;
					case ActiveSkillEnum.A_SummomTree:
					case ActiveSkillEnum.S_SummomTree:
						break;
					case ActiveSkillEnum.A_AvatarDodge:
					case ActiveSkillEnum.S_AvatarDodge:
						goto IL_16E3;
					case ActiveSkillEnum.A_DeathCyclone:
					case ActiveSkillEnum.S_DeathCyclone:
						goto IL_D3F;
					case ActiveSkillEnum.A_PoisionAoe:
					case ActiveSkillEnum.S_PoisionAoe:
						goto IL_A1B;
					case ActiveSkillEnum.S_SummonKight:
						if (attackRole.HasAuthority)
						{
							attackRole.StartSummon(EnemyType.Summon_Knight_S, pos, GameHelperClient.localPlayer.netId, 1f + (float)attackRole.AGI * activeSkillData.damageExValue[0], (long)Mathf.RoundToInt(activeSkillData.damageExValue[1] + (float)((int)((float)attackRole.STA * activeSkillData.damageExValue[2]))), (int)(activeSkillData.damageExValue[4] + (float)attackRole.STR * activeSkillData.damageExValue[3]), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
							return;
						}
						return;
					case ActiveSkillEnum.S_StarBurstStream:
					{
						StarBurstStream starBurstStream = new StarBurstStream();
						starBurstStream.skillId = skillId;
						starBurstStream.InitSkill(activeSkillType, attackRole, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
						skills.Add(skillId, starBurstStream);
						return;
					}
					default:
						return;
					}
					break;
				}
				if (attackRole.HasAuthority)
				{
					attackRole.StartSummon(EnemyType.Summon_Tree_B, pos, GameHelperClient.localPlayer.netId, 1f + (float)attackRole.AGI * 0.001f, (long)Mathf.RoundToInt(activeSkillData.damageExValue[1] + (float)((int)((float)attackRole.STA * activeSkillData.damageExValue[2]))), (int)((float)attackRole.STR * 10f), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
					return;
				}
				return;
			}
			IL_5EA:
			AoeActiveSkill aoeActiveSkill = new AoeActiveSkill();
			aoeActiveSkill.skillId = skillId;
			aoeActiveSkill.InitSkill(activeSkillType, attackRole, pos, num);
			skills.Add(skillId, aoeActiveSkill);
			return;
			IL_9DB:
			BlackHoleActiveSkill blackHoleActiveSkill = new BlackHoleActiveSkill();
			blackHoleActiveSkill.skillId = skillId;
			blackHoleActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, EffectDefine.BlackHoleEffect, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration), 0.6667f);
			skills.Add(skillId, blackHoleActiveSkill);
			return;
			IL_A1B:
			PoisionAoeActiveSkill poisionAoeActiveSkill = new PoisionAoeActiveSkill();
			poisionAoeActiveSkill.skillId = skillId;
			poisionAoeActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, EffectDefine.PoisonAoe, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration), 0.25f);
			skills.Add(skillId, poisionAoeActiveSkill);
			return;
			IL_AC3:
			SuperSaiyanActiveSkill superSaiyanActiveSkill = new SuperSaiyanActiveSkill();
			superSaiyanActiveSkill.skillId = skillId;
			superSaiyanActiveSkill.InitSkill(activeSkillType, attackRole, (activeSkillType > ActiveSkillEnum.B_SuperSaiyan) ? EffectDefine.SuperSaiyanBlue : EffectDefine.SuperSaiyanYellow, activeSkillData);
			skills.Add(skillId, superSaiyanActiveSkill);
			return;
			IL_AFA:
			MoveAoeActiveSkill moveAoeActiveSkill = new MoveAoeActiveSkill();
			moveAoeActiveSkill.skillId = skillId;
			moveAoeActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, EffectDefine.SlashWaveDark, attackRole.MyTransform.rotation);
			skills.Add(skillId, moveAoeActiveSkill);
			return;
			IL_BEC:
			ContinuousAoeActiveSkill continuousAoeActiveSkill = new ContinuousAoeActiveSkill();
			continuousAoeActiveSkill.skillId = skillId;
			continuousAoeActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, continuousAoeActiveSkill);
			return;
			IL_C46:
			FlameshrowerActiveSkill flameshrowerActiveSkill = new FlameshrowerActiveSkill();
			flameshrowerActiveSkill.skillId = skillId;
			flameshrowerActiveSkill.InitSkill(activeSkillType, attackRole, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, flameshrowerActiveSkill);
			return;
			IL_C7B:
			KamehamehaWaveActiveSkill kamehamehaWaveActiveSkill = new KamehamehaWaveActiveSkill();
			kamehamehaWaveActiveSkill.skillId = skillId;
			kamehamehaWaveActiveSkill.InitSkill(activeSkillType, attackRole, activeSkillData, num);
			skills.Add(skillId, kamehamehaWaveActiveSkill);
			return;
			IL_C9F:
			SurroundingFireActiveSkill surroundingFireActiveSkill = new SurroundingFireActiveSkill();
			surroundingFireActiveSkill.skillId = skillId;
			surroundingFireActiveSkill.InitSkill(activeSkillType, attackRole, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, surroundingFireActiveSkill);
			return;
			IL_CD4:
			KingsTreasureActiveSkill kingsTreasureActiveSkill = new KingsTreasureActiveSkill();
			kingsTreasureActiveSkill.skillId = skillId;
			kingsTreasureActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, kingsTreasureActiveSkill);
			return;
			IL_D0A:
			WhirlwindActiveSkill whirlwindActiveSkill = new WhirlwindActiveSkill();
			whirlwindActiveSkill.skillId = skillId;
			whirlwindActiveSkill.InitSkill(activeSkillType, attackRole, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, whirlwindActiveSkill);
			return;
			IL_D3F:
			DeathCycloneActiveSkill deathCycloneActiveSkill = new DeathCycloneActiveSkill();
			deathCycloneActiveSkill.skillId = skillId;
			deathCycloneActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, deathCycloneActiveSkill);
			return;
			IL_DB9:
			FireTornadoActiveSkill fireTornadoActiveSkill = new FireTornadoActiveSkill();
			fireTornadoActiveSkill.skillId = skillId;
			fireTornadoActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, fireTornadoActiveSkill);
			return;
			IL_1039:
			WindBreakSlashActiveSkill windBreakSlashActiveSkill = new WindBreakSlashActiveSkill();
			windBreakSlashActiveSkill.skillId = skillId;
			windBreakSlashActiveSkill.InitSkill(activeSkillType, attackRole, num);
			skills.Add(skillId, windBreakSlashActiveSkill);
			return;
			IL_1091:
			SacrificeActiveSkill sacrificeActiveSkill = new SacrificeActiveSkill();
			sacrificeActiveSkill.skillId = skillId;
			sacrificeActiveSkill.InitSkill(activeSkillType, attackRole, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration), skillBookId);
			skills.Add(skillId, sacrificeActiveSkill);
			return;
			IL_10C8:
			HenshinActiveSkill henshinActiveSkill = new HenshinActiveSkill();
			henshinActiveSkill.skillId = skillId;
			henshinActiveSkill.InitSkill(activeSkillType, attackRole, "Henshin/Demon", activeSkillData);
			skills.Add(skillId, henshinActiveSkill);
			return;
			IL_10F0:
			RachelActiveSkill rachelActiveSkill = new RachelActiveSkill();
			rachelActiveSkill.skillId = skillId;
			rachelActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, rachelActiveSkill);
			return;
			IL_1126:
			RasenganActiveSkill rasenganActiveSkill = new RasenganActiveSkill();
			rasenganActiveSkill.skillId = skillId;
			rasenganActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, rasenganActiveSkill);
			return;
			IL_115C:
			IceWallActiveSkill iceWallActiveSkill = new IceWallActiveSkill();
			iceWallActiveSkill.skillId = skillId;
			iceWallActiveSkill.InitSkill(activeSkillType, attackRole, pos, num);
			skills.Add(skillId, iceWallActiveSkill);
			return;
			IL_1180:
			BlackCoffinActiveSkill blackCoffinActiveSkill = new BlackCoffinActiveSkill();
			blackCoffinActiveSkill.skillId = skillId;
			blackCoffinActiveSkill.InitSkill(activeSkillType, attackRole, pos, num);
			skills.Add(skillId, blackCoffinActiveSkill);
			return;
			IL_11A4:
			FireStepActiveSkill fireStepActiveSkill = new FireStepActiveSkill();
			fireStepActiveSkill.skillId = skillId;
			fireStepActiveSkill.InitSkill(activeSkillType, attackRole, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, fireStepActiveSkill);
			return;
			IL_11D9:
			SwordOfLightActiveSkill swordOfLightActiveSkill = new SwordOfLightActiveSkill();
			swordOfLightActiveSkill.skillId = skillId;
			swordOfLightActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, swordOfLightActiveSkill);
			return;
			IL_120F:
			LightningChainActiveSkill lightningChainActiveSkill = new LightningChainActiveSkill();
			lightningChainActiveSkill.skillId = skillId;
			lightningChainActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, lightningChainActiveSkill);
			return;
			IL_1245:
			LandMineActiveSkill landMineActiveSkill = new LandMineActiveSkill();
			landMineActiveSkill.skillId = skillId;
			landMineActiveSkill.InitSkill(activeSkillType, attackRole, pos, activeSkillData, num, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, landMineActiveSkill);
			return;
			IL_12A9:
			DarkMonsterActiveSkill darkMonsterActiveSkill = new DarkMonsterActiveSkill();
			darkMonsterActiveSkill.skillId = skillId;
			darkMonsterActiveSkill.InitSkill(activeSkillType, attackRole, pos, activeSkillData, num);
			skills.Add(skillId, darkMonsterActiveSkill);
			return;
			IL_12CE:
			HenshinActiveSkill henshinActiveSkill2 = new HenshinActiveSkill();
			henshinActiveSkill2.skillId = skillId;
			henshinActiveSkill2.InitSkill(activeSkillType, attackRole, "Henshin/HenshinLight", activeSkillData);
			skills.Add(skillId, henshinActiveSkill2);
			return;
			IL_1346:
			if (attackRole.HasAuthority)
			{
				attackRole.StartSummon(EnemyType.Summon_Peashooter, pos, GameHelperClient.localPlayer.netId, 1f + (float)attackRole.AGI * activeSkillData.damageExValue[0], (long)Mathf.RoundToInt(activeSkillData.damageExValue[1] + (float)((int)((float)attackRole.STA * activeSkillData.damageExValue[2]))), (int)(activeSkillData.damageExValue[4] + (float)attackRole.STR * activeSkillData.damageExValue[3]), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
				return;
			}
			return;
			IL_13CB:
			BloodSacrificeActiveSkill bloodSacrificeActiveSkill = new BloodSacrificeActiveSkill();
			bloodSacrificeActiveSkill.skillId = skillId;
			bloodSacrificeActiveSkill.InitSkill(activeSkillType, attackRole, pos, num);
			skills.Add(skillId, bloodSacrificeActiveSkill);
			return;
			IL_13EF:
			AlchemyActiveSkill alchemyActiveSkill = new AlchemyActiveSkill();
			alchemyActiveSkill.skillId = skillId;
			alchemyActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, skillBookId);
			skills.Add(skillId, alchemyActiveSkill);
			return;
			IL_1415:
			ExecutionActiveSkill executionActiveSkill = new ExecutionActiveSkill();
			executionActiveSkill.skillId = skillId;
			executionActiveSkill.InitSkill(activeSkillType, attackRole, pos, activeSkillData, targetRoleId, skillBookId);
			skills.Add(skillId, executionActiveSkill);
			return;
			IL_143D:
			if (attackRole.HasAuthority)
			{
				attackRole.StartSummon(EnemyType.Summon_Sunflower, pos, GameHelperClient.localPlayer.netId, 1f + (float)attackRole.AGI * 0.001f, (long)Mathf.RoundToInt(activeSkillData.damageExValue[1] + (float)((int)((float)attackRole.STA * activeSkillData.damageExValue[2]))), (int)((float)attackRole.STR * 5f), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, skillBookId);
				return;
			}
			return;
			IL_16E3:
			AvatarDodgeActiveSkill avatarDodgeActiveSkill = new AvatarDodgeActiveSkill();
			avatarDodgeActiveSkill.skillId = skillId;
			avatarDodgeActiveSkill.InitSkill(activeSkillType, attackRole, pos, activeSkillData);
			skills.Add(skillId, avatarDodgeActiveSkill);
			return;
		}
		switch (activeSkillType)
		{
		case ActiveSkillEnum.Hero_Blink:
		{
			BlinkActiveSkill blinkActiveSkill = new BlinkActiveSkill();
			blinkActiveSkill.skillId = skillId;
			blinkActiveSkill.InitSkill(activeSkillType, attackRole, pos, num);
			skills.Add(skillId, blinkActiveSkill);
			return;
		}
		case ActiveSkillEnum.Hero_DrawKnife:
		case ActiveSkillEnum.Hero_PlayMusic:
		case ActiveSkillEnum.Hero_Roll:
		case ActiveSkillEnum.ChickenDance:
			break;
		case ActiveSkillEnum.Hero_FieldExpansion:
		{
			FieldExpansionActiveSkill fieldExpansionActiveSkill = new FieldExpansionActiveSkill();
			fieldExpansionActiveSkill.skillId = skillId;
			fieldExpansionActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, EffectDefine.FieldExpansionEffect, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
			skills.Add(skillId, fieldExpansionActiveSkill);
			return;
		}
		case ActiveSkillEnum.Hero_Titan:
		{
			HenshinActiveSkill henshinActiveSkill3 = new HenshinActiveSkill();
			henshinActiveSkill3.skillId = skillId;
			henshinActiveSkill3.InitSkill(activeSkillType, attackRole, "Henshin/Titan", activeSkillData);
			skills.Add(skillId, henshinActiveSkill3);
			return;
		}
		case ActiveSkillEnum.XieHuangBao:
			if (attackRole.hasAuthority)
			{
				PlayerBase playerBase = attackRole as PlayerBase;
				if (playerBase != null)
				{
					playerBase.MakeXieHuangBao();
					return;
				}
			}
			break;
		case ActiveSkillEnum.SummonNezuko:
			if (attackRole.HasAuthority)
			{
				attackRole.StartSummon(EnemyType.Summon_Nezuko, pos, GameHelperClient.localPlayer.netId, 2f + (float)attackRole.AGI * 0.001f, (long)(2000 + (int)((float)attackRole.STA * 30f)), (int)((float)attackRole.STR * 10f), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
				return;
			}
			break;
		case ActiveSkillEnum.CopyNinja:
		{
			CopyNinjaActiveSkill copyNinjaActiveSkill = new CopyNinjaActiveSkill();
			copyNinjaActiveSkill.skillId = skillId;
			copyNinjaActiveSkill.InitSkill(activeSkillType, attackRole);
			skills.Add(skillId, copyNinjaActiveSkill);
			return;
		}
		case ActiveSkillEnum.HollofiedIchigo:
		{
			HenshinActiveSkill henshinActiveSkill4 = new HenshinActiveSkill();
			henshinActiveSkill4.skillId = skillId;
			henshinActiveSkill4.InitSkill(activeSkillType, attackRole, "Henshin/HollofiedIchigo", activeSkillData);
			skills.Add(skillId, henshinActiveSkill4);
			return;
		}
		case ActiveSkillEnum.ShadowCloneTechnique:
			if (attackRole.HasAuthority)
			{
				for (int i = 0; i < 5; i++)
				{
					Vector2 pointByRadian = Util.GetPointByRadian(2f, 0f, (float)(i * 72));
					attackRole.StartSummon(EnemyType.Summon_Naruto, new Vector3(attackRole.MyTransform.position.x + pointByRadian.x, 0f, attackRole.MyTransform.position.z + pointByRadian.y), GameHelperClient.localPlayer.netId, attackRole.GetAttackSpeed(), attackRole.maxHp, ConstDefine.ClampIntValue((double)attackRole.FinalAttackPower), Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
				}
				return;
			}
			break;
		case ActiveSkillEnum.SoulDevourer:
		{
			PlayerKoboldMode playerKoboldMode = attackRole.RoleModeBase as PlayerKoboldMode;
			if (playerKoboldMode != null)
			{
				playerKoboldMode.StartSkill();
				return;
			}
			break;
		}
		case ActiveSkillEnum.PlantBomb:
		{
			PlantBombActiveSkill plantBombActiveSkill = new PlantBombActiveSkill();
			plantBombActiveSkill.skillId = skillId;
			plantBombActiveSkill.InitSkill(activeSkillType, attackRole, pos, activeSkillData, syncData, Util.GetRealSkillDuration(attackRole, activeSkillData.duration), num);
			skills.Add(skillId, plantBombActiveSkill);
			return;
		}
		default:
			switch (activeSkillType)
			{
			case ActiveSkillEnum.Boss_SwordMove:
			{
				MoveAoeActiveSkill moveAoeActiveSkill2 = new MoveAoeActiveSkill();
				moveAoeActiveSkill2.skillId = skillId;
				moveAoeActiveSkill2.InitSkill(activeSkillType, attackRole, pos, num, EffectDefine.SwordMoveAoe, attackRole.MyTransform.rotation);
				skills.Add(skillId, moveAoeActiveSkill2);
				return;
			}
			case ActiveSkillEnum.HellFire_Call:
			{
				HellFireCallActiveSkill hellFireCallActiveSkill = new HellFireCallActiveSkill();
				hellFireCallActiveSkill.skillId = skillId;
				hellFireCallActiveSkill.InitSkill(activeSkillType, attackRole, pos, num);
				skills.Add(skillId, hellFireCallActiveSkill);
				return;
			}
			case ActiveSkillEnum.D_Kight_Sword:
			case ActiveSkillEnum.C_Kight_Sword:
			case ActiveSkillEnum.B_Kight_Sword:
			case ActiveSkillEnum.A_Kight_Sword:
			case ActiveSkillEnum.S_Kight_Sword:
			case ActiveSkillEnum.Elder_Wave:
			{
				KightSwordActiveSkill kightSwordActiveSkill = new KightSwordActiveSkill();
				kightSwordActiveSkill.skillId = skillId;
				kightSwordActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, syncData, attackRotation);
				skills.Add(skillId, kightSwordActiveSkill);
				return;
			}
			case ActiveSkillEnum.MoonlightGreatsword:
			{
				MoveAoeActiveSkill moveAoeActiveSkill = new MoveAoeActiveSkill();
				moveAoeActiveSkill.skillId = skillId;
				moveAoeActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, EffectDefine.MoonlightGreatsword, Quaternion.Euler(0f, attackRotation, 0f));
				skills.Add(skillId, moveAoeActiveSkill);
				return;
			}
			case ActiveSkillEnum.DrangonFireBoom:
			{
				DragonFireBoomActiveSkill dragonFireBoomActiveSkill = new DragonFireBoomActiveSkill();
				dragonFireBoomActiveSkill.skillId = skillId;
				dragonFireBoomActiveSkill.InitSkill(activeSkillType, attackRole, pos, num);
				skills.Add(skillId, dragonFireBoomActiveSkill);
				return;
			}
			case ActiveSkillEnum.FireDaggers:
			{
				FireDaggersActiveSkill fireDaggersActiveSkill = new FireDaggersActiveSkill();
				fireDaggersActiveSkill.skillId = skillId;
				fireDaggersActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, attackRotation);
				skills.Add(skillId, fireDaggersActiveSkill);
				return;
			}
			case ActiveSkillEnum.ChargeBoom:
			{
				ChargeBoomActiveSkill chargeBoomActiveSkill = new ChargeBoomActiveSkill();
				chargeBoomActiveSkill.skillId = skillId;
				chargeBoomActiveSkill.InitSkill(activeSkillType, attackRole, pos, (float)syncData, EffectDefine.DragonFireAfterBoom, activeSkillData.interval, activeSkillData.duration, 0.2857143f);
				skills.Add(skillId, chargeBoomActiveSkill);
				return;
			}
			case ActiveSkillEnum.SaiyaCall:
				if (attackRole.HasAuthority)
				{
					for (int j = 0; j < 5; j++)
					{
						Vector2 pointByRadian2 = Util.GetPointByRadian(-attackRole.MyTransform.forward.x * 3.5f, -attackRole.MyTransform.forward.z * 3.5f, (float)(j * 45 - 90));
						attackRole.StartSummon(EnemyType.SkeletonCrossbow, new Vector3(attackRole.MyTransform.position.x + pointByRadian2.x, 0f, attackRole.MyTransform.position.z + pointByRadian2.y), GameHelperClient.localPlayer.netId, 1f, ConstDefine.ClampBattleValue((double)attackRole.maxHp * 0.2), ConstDefine.ClampIntValue((double)attackRole.FinalAttackPower * 0.35), (attackRole.roleType == RoleType.Enemy) ? 0f : Util.GetRealSkillDuration(attackRole, activeSkillData.duration), null, 0L, 0L, -1);
					}
					return;
				}
				break;
			case ActiveSkillEnum.PlayerDrangonFireBoom:
			{
				PlayerDragonFireBoomActiveSkill playerDragonFireBoomActiveSkill = new PlayerDragonFireBoomActiveSkill();
				playerDragonFireBoomActiveSkill.skillId = skillId;
				playerDragonFireBoomActiveSkill.InitSkill(activeSkillType, attackRole, pos, activeSkillData);
				skills.Add(skillId, playerDragonFireBoomActiveSkill);
				return;
			}
			case ActiveSkillEnum.PlayerDrangonFireBoomEnd:
			{
				PlayerDragonFireBoomEndActiveSkill playerDragonFireBoomEndActiveSkill = new PlayerDragonFireBoomEndActiveSkill();
				playerDragonFireBoomEndActiveSkill.skillId = skillId;
				playerDragonFireBoomEndActiveSkill.InitSkill(activeSkillType, attackRole, pos, num);
				skills.Add(skillId, playerDragonFireBoomEndActiveSkill);
				return;
			}
			case ActiveSkillEnum.RockFall:
			{
				RockFallActiveSkill rockFallActiveSkill = new RockFallActiveSkill();
				rockFallActiveSkill.skillId = skillId;
				rockFallActiveSkill.InitSkill(activeSkillType, attackRole, pos, num);
				skills.Add(skillId, rockFallActiveSkill);
				return;
			}
			case ActiveSkillEnum.NecromancerCall:
			{
				float realSkillDuration = Util.GetRealSkillDuration(attackRole, activeSkillData.duration);
				Enemy_NecromancerMode enemy_NecromancerMode = attackRole.RoleModeBase as Enemy_NecromancerMode;
				if (enemy_NecromancerMode != null)
				{
					enemy_NecromancerMode.CreateSword(pos, realSkillDuration);
				}
				if (attackRole.HasAuthority)
				{
					for (int k = 0; k < 8; k++)
					{
						Vector2 pointByRadian3 = Util.GetPointByRadian(0f, 8.5f, (float)(k * 45 - 90));
						attackRole.StartSummon(EnemyType.NecromancerStone, new Vector3(pos.x + pointByRadian3.x, 0f, pos.z + pointByRadian3.y), GameHelperClient.localPlayer.netId, 1f, ConstDefine.ClampBattleValue((double)attackRole.maxHp * 0.2), ConstDefine.ClampIntValue((double)attackRole.FinalAttackPower * 0.35), realSkillDuration + 10f, null, 0L, 0L, -1);
					}
					return;
				}
				break;
			}
			case ActiveSkillEnum.IceGround:
			{
				IceGroundActiveSkill iceGroundActiveSkill = new IceGroundActiveSkill();
				iceGroundActiveSkill.skillId = skillId;
				iceGroundActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, EffectDefine.IceGround, activeSkillData.interval, Util.GetRealSkillDuration(attackRole, activeSkillData.duration), 0.23f);
				skills.Add(skillId, iceGroundActiveSkill);
				return;
			}
			case ActiveSkillEnum.GuardianBullet:
			{
				MoveBulletActiveSkill moveBulletActiveSkill = new MoveBulletActiveSkill();
				moveBulletActiveSkill.skillId = skillId;
				moveBulletActiveSkill.InitSkill(activeSkillType, attackRole, pos, num, EffectDefine.CrossbowArrow, EffectDefine.CrossbowArrowHit, 12f, Quaternion.Euler(0f, attackRotation, 0f), Util.GetRealSkillDuration(attackRole, activeSkillData.duration));
				skills.Add(skillId, moveBulletActiveSkill);
				return;
			}
			default:
			{
				if (activeSkillType != ActiveSkillEnum.TrapSpears)
				{
					return;
				}
				TrapSpearsActiveSkill trapSpearsActiveSkill = new TrapSpearsActiveSkill();
				trapSpearsActiveSkill.skillId = skillId;
				trapSpearsActiveSkill.InitSkill(activeSkillType, attackRole, pos);
				skills.Add(skillId, trapSpearsActiveSkill);
				break;
			}
			}
			break;
		}
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0000FB6C File Offset: 0x0000DD6C
	public static ItemType GetRandomSkillBook(float[] skillProbability, float bookAdd)
	{
		if (skillProbability == null || skillProbability.Length == 0)
		{
			return ItemType.None;
		}
		int num = skillProbability.Length;
		float num2 = 0f;
		int num3 = 0;
		float num4 = 0f;
		float[] bookDropLevel = GameHelperClient.gameConfig.BookDropLevel;
		for (int i = 0; i < num; i++)
		{
			float num5 = (bookDropLevel != null && i < bookDropLevel.Length) ? bookDropLevel[i] : 0f;
			num4 += skillProbability[i] * (1f + bookAdd * num5);
		}
		if (num4 <= 0f)
		{
			return ItemType.None;
		}
		float num6 = Random.value * num4;
		for (int j = 0; j < num; j++)
		{
			float num7 = (bookDropLevel != null && j < bookDropLevel.Length) ? bookDropLevel[j] : 0f;
			num2 += skillProbability[j] * (1f + bookAdd * num7);
			if (num6 < num2)
			{
				num3 = j;
				break;
			}
		}
		if (Random.value > 0.5f)
		{
			return ItemType.Active_Book_D + num3;
		}
		return ItemType.Passsive_Book_D + num3;
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0000FC50 File Offset: 0x0000DE50
	public static ItemType GetRandomAttributeBook()
	{
		return ItemType.STRBook + Random.Range(0, 3);
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0000FC5F File Offset: 0x0000DE5F
	public static ItemType GetRandomTalisman()
	{
		return ItemType.Talisman_Roar + Random.Range(0, GameHelperClient.TalismanNum);
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0000FC74 File Offset: 0x0000DE74
	public static int[] GetRandomList(int length, int randomNum)
	{
		int[] array = new int[randomNum];
		int[] array2 = new int[length];
		for (int i = 0; i < length; i++)
		{
			array2[i] = i;
		}
		for (int j = 0; j < length; j++)
		{
			int num = Random.Range(0, length);
			ref int ptr = ref array2[j];
			int[] array3 = array2;
			int num2 = num;
			int num3 = array2[num];
			int num4 = array2[j];
			ptr = num3;
			array3[num2] = num4;
		}
		for (int k = 0; k < randomNum; k++)
		{
			array[k] = array2[k];
		}
		return array;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0000FCF4 File Offset: 0x0000DEF4
	public static long InitServerEnemyEntries(long baseHp, EnemyEntriesType[] enemyEntriesTypes, bool isBoss)
	{
		if (enemyEntriesTypes == null)
		{
			return baseHp;
		}
		long num = baseHp;
		Dictionary<EnemyEntriesType, EnemyEntriesData> enemyEntriesDic = Game.GameData.EnemyEntriesDic;
		int num2 = enemyEntriesTypes.Length;
		for (int i = 0; i < num2; i++)
		{
			EnemyEntriesData enemyEntriesData = enemyEntriesDic[enemyEntriesTypes[i]];
			if (enemyEntriesData.enemyEntriesType == EnemyEntriesType.Stamina)
			{
				num += ConstDefine.ClampBattleValue((double)((float)baseHp * (isBoss ? enemyEntriesData.bossLevel : enemyEntriesData.level)));
			}
		}
		return ConstDefine.ClampMaxHp(num);
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0000FD60 File Offset: 0x0000DF60
	public static void InitClientEnemyEntries(EnemyBase enemyBase)
	{
		EnemyEntriesType[] enemyEntriesTypes = enemyBase.EnemyEntriesTypes;
		if (enemyEntriesTypes == null)
		{
			return;
		}
		int num = enemyEntriesTypes.Length;
		int mAttackPower = enemyBase.mAttackPower;
		float attackSpeed = enemyBase.attackSpeed;
		float moveSpeed = enemyBase.moveSpeed;
		Dictionary<EnemyEntriesType, EnemyEntriesData> enemyEntriesDic = Game.GameData.EnemyEntriesDic;
		for (int i = 0; i < num; i++)
		{
			EnemyEntriesData enemyEntriesData = enemyEntriesDic[enemyEntriesTypes[i]];
			EnemyEntriesType enemyEntriesType = enemyEntriesData.enemyEntriesType;
			if (enemyEntriesType != EnemyEntriesType.Strength)
			{
				if (enemyEntriesType == EnemyEntriesType.Agility)
				{
					enemyBase.AddMoveSpeed(moveSpeed * (enemyBase.isBoss ? enemyEntriesData.bossLevel : enemyEntriesData.level) * 0.35f);
					enemyBase.attackSpeed += attackSpeed * (enemyBase.isBoss ? enemyEntriesData.bossLevel : enemyEntriesData.level);
				}
			}
			else
			{
				enemyBase.mAttackPower += (int)((float)mAttackPower * (enemyBase.isBoss ? enemyEntriesData.bossLevel : enemyEntriesData.level));
			}
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0000FE4E File Offset: 0x0000E04E
	public static Vector3 GetMousePos()
	{
		if (GameHelperClient.IsJoyStick)
		{
			return Util.visualMouse;
		}
		return Input.mousePosition;
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0000FE64 File Offset: 0x0000E064
	public static void CmdXuanYun(RoleBase roleBase, float timer)
	{
		if (roleBase.CanXuanYunLastTime > 0f || roleBase.XuanYunImmunity)
		{
			if (roleBase.RoleState != RoleState.XuanYun)
			{
				UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
				if (ui == null)
				{
					return;
				}
				ui.ShowXuanYunImmunity(roleBase.GetAttackPos());
			}
			return;
		}
		GameHelperClient.localPlayer.CmdXuanYun(roleBase.netId, timer);
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0000FEBC File Offset: 0x0000E0BC
	public static int RemoveSkill(SkillBase removeSkill)
	{
		List<SkillBase> roleSkillList = GameHelperClient.localPlayer.roleSkillList;
		PasssiveSkill passsiveSkill = removeSkill as PasssiveSkill;
		if (passsiveSkill != null)
		{
			passsiveSkill.Exit();
		}
		int num = roleSkillList.IndexOf(removeSkill);
		roleSkillList.RemoveAt(num);
		return num;
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0000FEF4 File Offset: 0x0000E0F4
	public static void AddSkill(SkillBase skill, SkillBase removeSkill)
	{
		if (GameHelperClient.localPlayer.roleSkillList.Count > GameHelperClient.MaxSkillNum - 1 && removeSkill == null)
		{
			removeSkill = GameHelperClient.localPlayer.roleSkillList[1];
		}
		List<SkillBase> roleSkillList = GameHelperClient.localPlayer.roleSkillList;
		int num = -1;
		if (removeSkill != null)
		{
			num = Util.RemoveSkill(removeSkill);
		}
		if (num != -1)
		{
			roleSkillList.Insert(num, skill);
		}
		else
		{
			roleSkillList.Add(skill);
		}
		int count = roleSkillList.Count;
		if (num == -1)
		{
			num = count - 1;
		}
		SkillUI skillUI = null;
		if (Game.UI.GetUI<UI_PlayerState>() != null && Game.UI.GetUI<UI_PlayerState>().skillList != null && Game.UI.GetUI<UI_PlayerState>().skillList.Count > 0)
		{
			skillUI = Game.UI.GetUI<UI_PlayerState>().skillList[num];
		}
		if (count > 1)
		{
			for (int i = 1; i < count; i++)
			{
				SkillBase skillBase = roleSkillList[i];
				if (skillBase is PasssiveSkill)
				{
					int j = i + 1;
					while (j < count)
					{
						SkillBase skillBase2 = roleSkillList[j];
						if (!(skillBase2 is PasssiveSkill))
						{
							List<SkillBase> list = roleSkillList;
							int index = i;
							List<SkillBase> list2 = roleSkillList;
							int index2 = j;
							SkillBase value = skillBase2;
							SkillBase value2 = skillBase;
							list[index] = value;
							list2[index2] = value2;
							if (num == i)
							{
								num = j;
								break;
							}
							break;
						}
						else
						{
							j++;
						}
					}
				}
			}
		}
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui != null)
		{
			ui.RefreshPlayerSkill();
		}
		if (skillUI != null)
		{
			skillUI.ClearCD();
		}
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0001005C File Offset: 0x0000E25C
	public static SkillBase GetActiveSkillByKeyIndex(int index)
	{
		int num = 0;
		List<SkillBase> roleSkillList = GameHelperClient.localPlayer.roleSkillList;
		int count = roleSkillList.Count;
		for (int i = 0; i < count; i++)
		{
			if (!(roleSkillList[i] is PasssiveSkill))
			{
				if (num == index)
				{
					return roleSkillList[i];
				}
				num++;
			}
		}
		return null;
	}

	// Token: 0x060002CA RID: 714 RVA: 0x000100A8 File Offset: 0x0000E2A8
	public static int GetActiveIndexByKeyIndex(int index)
	{
		int num = 0;
		List<SkillBase> roleSkillList = GameHelperClient.localPlayer.roleSkillList;
		int count = roleSkillList.Count;
		for (int i = 0; i < count; i++)
		{
			if (!(roleSkillList[i] is PasssiveSkill))
			{
				if (num == index)
				{
					return i;
				}
				num++;
			}
		}
		return -1;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x000100F0 File Offset: 0x0000E2F0
	public static void ShowRemainsRoguelike(Action closeAction, float delayShowTime)
	{
		Dictionary<ItemType, RemainsData> remainsDataDic = Game.GameData.RemainsDataDic;
		List<ItemType> randomRelicDrop = Util.GetRandomRelicDrop();
		RoguelikeUIData[] array = new RoguelikeUIData[3];
		for (int i = 0; i < 3; i++)
		{
			ItemType itemType = randomRelicDrop[i];
			RemainsData remainsData = remainsDataDic[itemType];
			RoguelikeUIData roguelikeUIData = default(RoguelikeUIData);
			object dic = ExcelManager.allExcelData["remains"];
			int num = (int)itemType;
			Dictionary<string, object> dictionary = (Dictionary<string, object>)dic.DIC(num.ToString());
			LanguageManager language = Game.Language;
			string str = "pickitem_";
			num = (int)itemType;
			roguelikeUIData.name = language.Get(str + num.ToString(), "");
			roguelikeUIData.icon = "Bundles/UI/Icon/Remains/" + dictionary.DIC("icon");
			LanguageManager language2 = Game.Language;
			string str2 = "pickitem_";
			num = (int)itemType;
			roguelikeUIData.dec = RelicBase.GetFormatDec(language2.Get(str2 + num.ToString() + "_m", ""), dictionary);
			num = (int)itemType;
			roguelikeUIData.data = num.ToString();
			roguelikeUIData.quality = remainsData.grade;
			array[i] = roguelikeUIData;
		}
		Util.roguelikeIndex = 3;
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习主动技能", 1f, 3f);
		ui_Roguelike.ShowRoguelike(array, new Action<RoguelikeUIData>(Util.OnRemainsRoguelike), Game.Language.Get("遗物选择", ""), new UI_Roguelike.RefreshActionEvent(Util.OnRefreshActionEvent), closeAction, delayShowTime, null, "relic");
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0001027C File Offset: 0x0000E47C
	public static void OnRemainsRoguelike(RoguelikeUIData roguelikeData)
	{
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/拾取物品", 1f, 3f);
		ItemType itemType = (ItemType)int.Parse(roguelikeData.data);
		if (itemType <= (ItemType)GameHelperClient.RemainsNum)
		{
			object dic = ExcelManager.allExcelData["remains"];
			int num = (int)itemType;
			if (((Dictionary<string, object>)dic.DIC(num.ToString())).DIC("isTeam") == 1)
			{
				GameHelperClient.localPlayer.CmdChat(Game.Language.Get("选择遗物提示", "") + string.Format(ColorDefine.NormalColor, roguelikeData.name));
				GameHelperClient.localPlayer.CmdAddAllPlayerItem(itemType);
				return;
			}
		}
		GameHelperClient.localPlayer.AddRelic(int.Parse(roguelikeData.data), 0);
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0001033C File Offset: 0x0000E53C
	public static void SelectKingChallenge()
	{
		List<SaveLoadManager.TeamBuildData> teamBuildDataList = SaveLoadManager.teamBuildDataList;
		if (teamBuildDataList == null || teamBuildDataList.Count == 0)
		{
			return;
		}
		int count = teamBuildDataList.Count;
		int num = -1;
		ulong steamID = SteamUser.GetSteamID().m_SteamID;
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < count; i++)
		{
			SaveLoadManager.TeamBuildData teamBuildData = teamBuildDataList[i];
			if (SteamLeaderboardRankOrder.HasCompleteBuildData(teamBuildData))
			{
				list.Add(i);
				if (teamBuildData.steamGlobalRank > 50)
				{
					list2.Add(i);
				}
				if (teamBuildData.members[0].steamID == steamID)
				{
					num = list.Count - 1;
				}
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		int num2 = 0;
		int num3 = 0;
		while (num3 < list.Count && list[num3] < 3)
		{
			num2++;
			num3++;
		}
		if (num2 == 0)
		{
			num2 = Mathf.Min(3, list.Count);
		}
		int maxExclusive = (num == -1) ? num2 : Mathf.Min(num2, Mathf.Max(1, num));
		int num4 = Random.Range(0, maxExclusive);
		RoguelikeUIData[] array = new RoguelikeUIData[3];
		List<int> list3 = list;
		List<int> list4 = list;
		int index = num4;
		int value = list[num4];
		int num5 = list[0];
		list3[0] = value;
		list4[index] = num5;
		int num6 = Util.MoveRandomLowRankCandidateToSecond(list, list2) ? 2 : 1;
		for (int j = num6; j < list.Count; j++)
		{
			int num7 = Random.Range(num6, list.Count);
			List<int> list5 = list;
			index = j;
			list4 = list;
			num5 = num7;
			value = list[num7];
			int value2 = list[j];
			list5[index] = value;
			list4[num5] = value2;
		}
		int num8 = Mathf.Min(3, list.Count);
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		List<SaveLoadManager.TeamBuildData> list6 = new List<SaveLoadManager.TeamBuildData>();
		for (int k = 0; k < num8; k++)
		{
			RoguelikeUIData roguelikeUIData = default(RoguelikeUIData);
			int num9 = list[k];
			SaveLoadManager.TeamBuildData teamBuildData2 = teamBuildDataList[num9];
			roguelikeUIData.name = teamBuildData2.members[0].kingName;
			roguelikeUIData.icon = teamBuildData2.members[0].steamID.ToString();
			roguelikeUIData.dec = teamBuildData2.teamMessage;
			roguelikeUIData.quality = 3;
			roguelikeUIData.data = num9.ToString();
			roguelikeUIData.displayRank = ((teamBuildData2.steamGlobalRank > 0 && teamBuildData2.steamGlobalRank > 10) ? teamBuildData2.steamGlobalRank : (num9 + 1));
			array[k] = roguelikeUIData;
			list6.Add(teamBuildData2);
		}
		if (num8 < 3)
		{
			for (int l = num8; l < 3; l++)
			{
				list6.Add(list6[0]);
				array[l] = array[0];
			}
		}
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买怪物", 1f, 3f);
		ui_Roguelike.ShowRoguelike(array, new Action<RoguelikeUIData>(Util.OnSelectKingChallenge), Game.Language.Get("挑战选择", ""), null, null, 0f, list6, "king_challenge");
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0001067C File Offset: 0x0000E87C
	private static bool MoveRandomLowRankCandidateToSecond(List<int> candidateIndexes, List<int> lowRankCandidateIndexes)
	{
		if (candidateIndexes == null || candidateIndexes.Count <= 1 || lowRankCandidateIndexes == null || lowRankCandidateIndexes.Count == 0)
		{
			return false;
		}
		List<int> list = new List<int>();
		for (int i = 0; i < lowRankCandidateIndexes.Count; i++)
		{
			int num = lowRankCandidateIndexes[i];
			if (num != candidateIndexes[0])
			{
				list.Add(num);
			}
		}
		if (list.Count == 0)
		{
			return false;
		}
		int item = list[Random.Range(0, list.Count)];
		int num2 = candidateIndexes.IndexOf(item);
		if (num2 > 0)
		{
			int index = num2;
			int value = candidateIndexes[num2];
			int value2 = candidateIndexes[1];
			candidateIndexes[1] = value;
			candidateIndexes[index] = value2;
			return true;
		}
		return false;
	}

	// Token: 0x060002CF RID: 719 RVA: 0x00010734 File Offset: 0x0000E934
	private static void OnSelectKingChallenge(RoguelikeUIData roguelikeData)
	{
		int num = int.Parse(roguelikeData.data);
		if (SaveLoadManager.teamBuildDataList != null && num >= 0 && num < SaveLoadManager.teamBuildDataList.Count)
		{
			SaveLoadManager.TeamBuildData teamBuildData = SaveLoadManager.teamBuildDataList[num];
			if (SteamLeaderboardRankOrder.HasCompleteBuildData(teamBuildData))
			{
				AnalyticsManager analytics = Game.Analytics;
				if (analytics != null)
				{
					analytics.RecordKingChallengeSelection(num, teamBuildData.members[0].heroType);
				}
				NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
				{
					serverNetOperation = ServerNetOperation.KingChallenge,
					datas = new int[]
					{
						num
					}
				}, 0);
			}
		}
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x000107C8 File Offset: 0x0000E9C8
	public static List<ItemType> GetRandomRelicDrop()
	{
		float luckyAdd = Util.GetLuckAddValue(GameHelperClient.localPlayer.lucky) + GameHelperClient.localPlayer.RelicAdd;
		Util.relicRoguelikeList = new List<ItemType>();
		for (int i = 0; i < 35; i++)
		{
			List<ItemType> list = new List<ItemType>();
			Dictionary<ItemType, RemainsData> remainsDataDic = Game.GameData.RemainsDataDic;
			int relicDropGrade = Util.GetRelicDropGrade(luckyAdd);
			int count = remainsDataDic.Count;
			for (int j = 0; j < count; j++)
			{
				KeyValuePair<ItemType, RemainsData> keyValuePair = remainsDataDic.ElementAt(j);
				if (keyValuePair.Value.grade == relicDropGrade && !Util.relicRoguelikeList.Contains(keyValuePair.Key) && (keyValuePair.Value.conditions == EntryConditions.None || (GameHelperClient.localPlayer.entryConditions != null && GameHelperClient.localPlayer.entryConditions.Contains(keyValuePair.Value.conditions))))
				{
					list.Add(keyValuePair.Key);
				}
			}
			if (list.Count > 0)
			{
				Util.relicRoguelikeList.Add(list[Random.Range(0, list.Count)]);
			}
		}
		return Util.relicRoguelikeList;
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x000108DC File Offset: 0x0000EADC
	public static int GetRelicDropGrade(float luckyAdd)
	{
		float[] remainDrop = GameHelperClient.gameConfig.RemainDrop;
		float[] remainLucky = GameHelperClient.gameConfig.RemainLucky;
		int num = remainDrop.Length;
		float num2 = 0f;
		float num3 = 0f;
		for (int i = 0; i < num; i++)
		{
			num3 += remainDrop[i] * (1f + luckyAdd * remainLucky[i]);
		}
		float num4 = Random.value * num3;
		for (int j = 0; j < num; j++)
		{
			num2 += remainDrop[j] * (1f + luckyAdd * remainLucky[j]);
			if (num4 < num2)
			{
				return j;
			}
		}
		return 0;
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x00010970 File Offset: 0x0000EB70
	private static RoguelikeUIData OnRefreshActionEvent()
	{
		Util.roguelikeIndex = Mathf.Min(Util.roguelikeIndex, Util.relicRoguelikeList.Count - 1);
		ItemType itemType = Util.relicRoguelikeList[Util.roguelikeIndex];
		RemainsData remainsData = Game.GameData.RemainsDataDic[itemType];
		RoguelikeUIData result = default(RoguelikeUIData);
		object dic = ExcelManager.allExcelData["remains"];
		int num = (int)itemType;
		Dictionary<string, object> dictionary = (Dictionary<string, object>)dic.DIC(num.ToString());
		LanguageManager language = Game.Language;
		string str = "pickitem_";
		num = (int)itemType;
		result.name = language.Get(str + num.ToString(), "");
		result.icon = "Bundles/UI/Icon/Remains/" + dictionary.DIC("icon");
		LanguageManager language2 = Game.Language;
		string str2 = "pickitem_";
		num = (int)itemType;
		result.dec = RelicBase.GetFormatDec(language2.Get(str2 + num.ToString() + "_m", ""), dictionary);
		num = (int)itemType;
		result.data = num.ToString();
		result.quality = remainsData.grade;
		Util.roguelikeIndex++;
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/学习主动技能", 1f, 3f);
		return result;
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00010AA5 File Offset: 0x0000ECA5
	public static float GetRealSkillDuration(RoleBase role, float duration)
	{
		if (role.roleType != RoleType.Player)
		{
			return duration;
		}
		return duration * (1f + (role as PlayerBase).skillAddTime);
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00010AC4 File Offset: 0x0000ECC4
	public static float GetArmorLevel(int armor)
	{
		if (armor == 0)
		{
			return 1f;
		}
		int num = Mathf.Abs(armor);
		if (armor <= 0)
		{
			return 1f + (float)num / (100f + (float)num);
		}
		return 1f - (float)num / (100f + (float)num);
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00010B08 File Offset: 0x0000ED08
	public static float GetCdReduce(int skillCd)
	{
		return 1f / (1f + (float)skillCd / 100f);
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00010B1E File Offset: 0x0000ED1E
	public static string FormatForgingValue(float value)
	{
		return value.ToString("0.##", CultureInfo.InvariantCulture);
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00010B34 File Offset: 0x0000ED34
	public static string GetForgingDec(ForgingData forgingData, float value, float minValue, float maxValue)
	{
		string text = "";
		string type = forgingData.type;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(type);
		if (num <= 2000437136U)
		{
			if (num <= 853522520U)
			{
				if (num <= 591723698U)
				{
					if (num <= 399922361U)
					{
						if (num != 326073063U)
						{
							if (num == 399922361U)
							{
								if (type == "Sta")
								{
									text = Game.Language.Get("sta", "");
								}
							}
						}
						else if (type == "ArmedAdd")
						{
							text = Game.Language.Get("武装伤害", "");
						}
					}
					else if (num != 521850149U)
					{
						if (num == 591723698U)
						{
							if (type == "HPSec")
							{
								text = Game.Language.Get("hpAddSec", "");
							}
						}
					}
					else if (type == "SkillAdd")
					{
						text = Game.Language.Get("法术伤害加成", "");
					}
				}
				else if (num <= 776144995U)
				{
					if (num != 618031408U)
					{
						if (num == 776144995U)
						{
							if (type == "Three")
							{
								text = Game.Language.Get("全属性", "");
							}
						}
					}
					else if (type == "Str")
					{
						text = Game.Language.Get("str", "");
					}
				}
				else if (num != 785119009U)
				{
					if (num == 853522520U)
					{
						if (type == "MP")
						{
							text = Game.Language.Get("法力值", "");
						}
					}
				}
				else if (type == "SummonAdd")
				{
					text = Game.Language.Get("召唤物强度", "");
				}
			}
			else if (num <= 1287009716U)
			{
				if (num <= 1041612137U)
				{
					if (num != 895613287U)
					{
						if (num == 1041612137U)
						{
							if (type == "CriticalDamage")
							{
								text = Game.Language.Get("baojiDamage", "");
							}
						}
					}
					else if (type == "SkillHit")
					{
						text = Game.Language.Get("技能抵抗", "");
					}
				}
				else if (num != 1182039611U)
				{
					if (num == 1287009716U)
					{
						if (type == "XiXue")
						{
							text = Game.Language.Get("xixue", "");
						}
					}
				}
				else if (type == "ExpAdd")
				{
					text = Game.Language.Get("经验获取", "");
				}
			}
			else if (num <= 1680846624U)
			{
				if (num != 1483691181U)
				{
					if (num == 1680846624U)
					{
						if (type == "HpSecRate")
						{
							text = Game.Language.Get("hpAddSec", "");
						}
					}
				}
				else if (type == "SkillBreak")
				{
					text = Game.Language.Get("法术破盾伤害", "");
				}
			}
			else if (num != 1806474955U)
			{
				if (num != 1894470373U)
				{
					if (num == 2000437136U)
					{
						if (type == "Luck")
						{
							text = Game.Language.Get("幸运值", "");
						}
					}
				}
				else if (type == "HP")
				{
					text = Game.Language.Get("生命值", "");
				}
			}
			else if (type == "HenshinAdd")
			{
				text = Game.Language.Get("变身强度", "");
			}
		}
		else if (num <= 2752153801U)
		{
			if (num <= 2278596520U)
			{
				if (num <= 2107383588U)
				{
					if (num != 2076688730U)
					{
						if (num == 2107383588U)
						{
							if (type == "ExtraDamage")
							{
								text = Game.Language.Get("exs", "");
							}
						}
					}
					else if (type == "HaloAdd")
					{
						text = Game.Language.Get("BUFF伤害加成", "");
					}
				}
				else if (num != 2226667892U)
				{
					if (num == 2278596520U)
					{
						if (type == "Critical")
						{
							text = Game.Language.Get("baoji", "");
						}
					}
				}
				else if (type == "Armor")
				{
					text = Game.Language.Get("armor", "");
				}
			}
			else if (num <= 2343121693U)
			{
				if (num != 2311178276U)
				{
					if (num == 2343121693U)
					{
						if (type == "Attack")
						{
							text = Game.Language.Get("attack", "");
						}
					}
				}
				else if (type == "Doge")
				{
					text = Game.Language.Get("闪避值", "");
				}
			}
			else if (num != 2374343684U)
			{
				if (num != 2462836616U)
				{
					if (num == 2752153801U)
					{
						if (type == "NormalAdd")
						{
							text = Game.Language.Get("物理伤害加成", "");
						}
					}
				}
				else if (type == "Agi")
				{
					text = Game.Language.Get("dex", "");
				}
			}
			else if (type == "ReduceInjury")
			{
				text = Game.Language.Get("gdj", "");
			}
		}
		else if (num <= 3371345849U)
		{
			if (num <= 3354445110U)
			{
				if (num != 3135110330U)
				{
					if (num == 3354445110U)
					{
						if (type == "GoldAdd")
						{
							text = Game.Language.Get("金币获取", "");
						}
					}
				}
				else if (type == "HpPercent")
				{
					text = Game.Language.Get("最大生命值提升", "");
				}
			}
			else if (num != 3365943077U)
			{
				if (num == 3371345849U)
				{
					if (type == "NormalBreak")
					{
						text = Game.Language.Get("物理破盾加成", "");
					}
				}
			}
			else if (type == "AddDamage")
			{
				text = Game.Language.Get("总伤害加成", "");
			}
		}
		else if (num <= 3581048735U)
		{
			if (num != 3439798920U)
			{
				if (num == 3581048735U)
				{
					if (type == "MoveSpeed")
					{
						text = Game.Language.Get("moveSpeed", "");
					}
				}
			}
			else if (type == "AttackSpeed")
			{
				text = Game.Language.Get("attackSpeed", "");
			}
		}
		else if (num != 3990130861U)
		{
			if (num != 4235090694U)
			{
				if (num == 4292234280U)
				{
					if (type == "CoolDown")
					{
						text = Game.Language.Get("技能急速", "");
					}
				}
			}
			else if (type == "XiXueRate")
			{
				text = Game.Language.Get("攻击生命偷取", "");
			}
		}
		else if (type == "MPSec")
		{
			text = Game.Language.Get("mpAddSec", "");
		}
		string text2 = Util.FormatForgingValue(value);
		string text3 = Util.FormatForgingValue(minValue);
		string text4 = Util.FormatForgingValue(maxValue);
		string text5;
		if (forgingData.isPercent)
		{
			if (Mathf.Approximately(minValue, maxValue))
			{
				text5 = string.Format(StringDefine.ForgingDecPercentConst, text2, text);
			}
			else
			{
				text5 = string.Format(StringDefine.ForgingDecPercent, new object[]
				{
					text2,
					text,
					text3,
					text4
				});
			}
		}
		else if (Mathf.Approximately(minValue, maxValue))
		{
			text5 = string.Format(StringDefine.ForgingDecConst, text2, text);
		}
		else
		{
			text5 = string.Format(StringDefine.ForgingDec, new object[]
			{
				text2,
				text,
				text3,
				text4
			});
		}
		if (value >= 0f)
		{
			text5 = PathDefine.Concat(StringDefine.AddColor, text5);
		}
		return text5;
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x000114E5 File Offset: 0x0000F6E5
	public static string GetPercentData(float value)
	{
		return PathDefine.Concat(Mathf.RoundToInt(value * 100f), StringDefine.Percent);
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x00011504 File Offset: 0x0000F704
	public static PasssiveSkill GetPasssiveSkill(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2081316917U)
		{
			if (num <= 908205375U)
			{
				if (num <= 432329434U)
				{
					if (num <= 158751187U)
					{
						if (num <= 71654165U)
						{
							if (num != 31741680U)
							{
								if (num != 34314116U)
								{
									if (num == 71654165U)
									{
										if (name == "C闪避")
										{
											return new C闪避();
										}
									}
								}
								else if (name == "H量子阅读")
								{
									return new H量子阅读();
								}
							}
							else if (name == "B高级活性护甲")
							{
								return new B高级活性护甲();
							}
						}
						else if (num != 76764151U)
						{
							if (num != 147500456U)
							{
								if (num == 158751187U)
								{
									if (name == "C属性加强")
									{
										return new C属性加强();
									}
								}
							}
							else if (name == "H战斗龙卷风")
							{
								return new H战斗龙卷风();
							}
						}
						else if (name == "D长臂猿")
						{
							return new D长臂猿();
						}
					}
					else if (num <= 243932087U)
					{
						if (num != 175814084U)
						{
							if (num != 241534840U)
							{
								if (num == 243932087U)
								{
									if (name == "力量加成")
									{
										return new 力量加成();
									}
								}
							}
							else if (name == "暴击加成")
							{
								return new 暴击加成();
							}
						}
						else if (name == "HAK4鸡")
						{
							return new HAK4鸡();
						}
					}
					else if (num <= 290744945U)
					{
						if (num != 288456915U)
						{
							if (num == 290744945U)
							{
								if (name == "苦难光环")
								{
									return new 苦难光环();
								}
							}
						}
						else if (name == "C大荆棘皮肤")
						{
							return new C大荆棘皮肤();
						}
					}
					else if (num != 347644119U)
					{
						if (num == 432329434U)
						{
							if (name == "H橡胶果实")
							{
								return new H橡胶果实();
							}
						}
					}
					else if (name == "A攻击加超级巨多钱")
					{
						return new A攻击加超级巨多钱();
					}
				}
				else if (num <= 690809332U)
				{
					if (num <= 511780443U)
					{
						if (num != 447793777U)
						{
							if (num != 468180055U)
							{
								if (num == 511780443U)
								{
									if (name == "杀人书")
									{
										return new 杀人书();
									}
								}
							}
							else if (name == "A闪避")
							{
								return new A闪避();
							}
						}
						else if (name == "C超长臂猿")
						{
							return new C超长臂猿();
						}
					}
					else if (num <= 633993305U)
					{
						if (num != 592518372U)
						{
							if (num == 633993305U)
							{
								if (name == "C杀怪加很多钱")
								{
									return new C杀怪加很多钱();
								}
							}
						}
						else if (name == "B属性加强")
						{
							return new B属性加强();
						}
					}
					else if (num != 660488918U)
					{
						if (num == 690809332U)
						{
							if (name == "应急护盾")
							{
								return new 应急护盾();
							}
						}
					}
					else if (name == "B大火锅")
					{
						return new B大火锅();
					}
				}
				else if (num <= 819090665U)
				{
					if (num != 709138213U)
					{
						if (num != 748947525U)
						{
							if (num == 819090665U)
							{
								if (name == "C小分裂攻击")
								{
									return new C小分裂攻击();
								}
							}
						}
						else if (name == "D小斩杀")
						{
							return new D小斩杀();
						}
					}
					else if (name == "B杀怪加超多钱")
					{
						return new B杀怪加超多钱();
					}
				}
				else if (num <= 829433566U)
				{
					if (num != 828837241U)
					{
						if (num == 829433566U)
						{
							if (name == "C大树祝福")
							{
								return new C大树祝福();
							}
						}
					}
					else if (name == "猎物标记破")
					{
						return new 猎物标记破();
					}
				}
				else if (num != 847690695U)
				{
					if (num == 908205375U)
					{
						if (name == "H土豆兄弟")
						{
							return new H土豆兄弟();
						}
					}
				}
				else if (name == "移速加成")
				{
					return new 移速加成();
				}
			}
			else if (num <= 1421732223U)
			{
				if (num <= 1115124312U)
				{
					if (num <= 1043336595U)
					{
						if (num != 941955532U)
						{
							if (num != 986646172U)
							{
								if (num == 1043336595U)
								{
									if (name == "C斩杀")
									{
										return new C斩杀();
									}
								}
							}
							else if (name == "B超荆棘皮肤")
							{
								return new B超荆棘皮肤();
							}
						}
						else if (name == "C大先发制人")
						{
							return new C大先发制人();
						}
					}
					else if (num != 1043521240U)
					{
						if (num != 1089609779U)
						{
							if (num == 1115124312U)
							{
								if (name == "D先发制人")
								{
									return new D先发制人();
								}
							}
						}
						else if (name == "H死亡笔记")
						{
							return new H死亡笔记();
						}
					}
					else if (name == "枪灵")
					{
						return new 枪灵();
					}
				}
				else if (num <= 1212479759U)
				{
					if (num != 1142707130U)
					{
						if (num != 1150538745U)
						{
							if (num == 1212479759U)
							{
								if (name == "B分裂攻击")
								{
									return new B分裂攻击();
								}
							}
						}
						else if (name == "A属性加强")
						{
							return new A属性加强();
						}
					}
					else if (name == "H起洞")
					{
						return new H起洞();
					}
				}
				else if (num <= 1355872997U)
				{
					if (num != 1216681018U)
					{
						if (num == 1355872997U)
						{
							if (name == "S闪避")
							{
								return new S闪避();
							}
						}
					}
					else if (name == "D没用")
					{
						return new D没用();
					}
				}
				else if (num != 1416827400U)
				{
					if (num == 1421732223U)
					{
						if (name == "H内部折扣")
						{
							return new H内部折扣();
						}
					}
				}
				else if (name == "A超级火锅")
				{
					return new A超级火锅();
				}
			}
			else if (num <= 1867715427U)
			{
				if (num <= 1681306444U)
				{
					if (num != 1582100499U)
					{
						if (num != 1621624396U)
						{
							if (num == 1681306444U)
							{
								if (name == "耐力加成")
								{
									return new 耐力加成();
								}
							}
						}
						else if (name == "H大魔法使")
						{
							return new H大魔法使();
						}
					}
					else if (name == "D重击")
					{
						return new D重击();
					}
				}
				else if (num <= 1708267377U)
				{
					if (num != 1706819923U)
					{
						if (num == 1708267377U)
						{
							if (name == "C小吸血鬼")
							{
								return new C小吸血鬼();
							}
						}
					}
					else if (name == "C小火锅")
					{
						return new C小火锅();
					}
				}
				else if (num != 1833644674U)
				{
					if (num == 1867715427U)
					{
						if (name == "A大分裂攻击")
						{
							return new A大分裂攻击();
						}
					}
				}
				else if (name == "A几乎无限重生")
				{
					return new A几乎无限重生();
				}
			}
			else if (num <= 1993534869U)
			{
				if (num != 1900841318U)
				{
					if (num != 1987213731U)
					{
						if (num == 1993534869U)
						{
							if (name == "暴击伤害")
							{
								return new 暴击伤害();
							}
						}
					}
					else if (name == "C群众效应")
					{
						return new C群众效应();
					}
				}
				else if (name == "B闪避")
				{
					return new B闪避();
				}
			}
			else if (num <= 2078146616U)
			{
				if (num != 2074426603U)
				{
					if (num == 2078146616U)
					{
						if (name == "A吸血鬼")
						{
							return new A吸血鬼();
						}
					}
				}
				else if (name == "A多重攻击")
				{
					return new A多重攻击();
				}
			}
			else if (num != 2079021781U)
			{
				if (num == 2081316917U)
				{
					if (name == "涂毒")
					{
						return new 涂毒();
					}
				}
			}
			else if (name == "S多重攻击")
			{
				return new S多重攻击();
			}
		}
		else if (num <= 3156890210U)
		{
			if (num <= 2457350405U)
			{
				if (num <= 2239214139U)
				{
					if (num <= 2141709623U)
					{
						if (num != 2116341474U)
						{
							if (num != 2121878965U)
							{
								if (num == 2141709623U)
								{
									if (name == "A杀怪加超级巨多钱")
									{
										return new A杀怪加超级巨多钱();
									}
								}
							}
							else if (name == "C凋零打击")
							{
								return new C凋零打击();
							}
						}
						else if (name == "C重击")
						{
							return new C重击();
						}
					}
					else if (num != 2203930974U)
					{
						if (num != 2231602111U)
						{
							if (num == 2239214139U)
							{
								if (name == "攻击加成")
								{
									return new 攻击加成();
								}
							}
						}
						else if (name == "D小闪避")
						{
							return new D小闪避();
						}
					}
					else if (name == "D攻击加钱")
					{
						return new D攻击加钱();
					}
				}
				else if (num <= 2277752261U)
				{
					if (num != 2248514567U)
					{
						if (num != 2259556689U)
						{
							if (num == 2277752261U)
							{
								if (name == "B重生")
								{
									return new B重生();
								}
							}
						}
						else if (name == "B攻击加超多钱")
						{
							return new B攻击加超多钱();
						}
					}
					else if (name == "B大斩杀")
					{
						return new B大斩杀();
					}
				}
				else if (num <= 2416175267U)
				{
					if (num != 2338770228U)
					{
						if (num == 2416175267U)
						{
							if (name == "H三级除草证")
							{
								return new H三级除草证();
							}
						}
					}
					else if (name == "H重金悬赏")
					{
						return new H重金悬赏();
					}
				}
				else if (num != 2429372854U)
				{
					if (num == 2457350405U)
					{
						if (name == "A究超长臂猿")
						{
							return new A究超长臂猿();
						}
					}
				}
				else if (name == "H二刀流")
				{
					return new H二刀流();
				}
			}
			else if (num <= 2922192046U)
			{
				if (num <= 2651546179U)
				{
					if (num != 2604715017U)
					{
						if (num != 2626671343U)
						{
							if (num == 2651546179U)
							{
								if (name == "H赛亚人之血")
								{
									return new H赛亚人之血();
								}
							}
						}
						else if (name == "S超分裂攻击")
						{
							return new S超分裂攻击();
						}
					}
					else if (name == "H电锯恶魔")
					{
						return new H电锯恶魔();
					}
				}
				else if (num <= 2777764326U)
				{
					if (num != 2726903549U)
					{
						if (num == 2777764326U)
						{
							if (name == "无极剑道")
							{
								return new 无极剑道();
							}
						}
					}
					else if (name == "H禁忌魔法")
					{
						return new H禁忌魔法();
					}
				}
				else if (num != 2891910621U)
				{
					if (num == 2922192046U)
					{
						if (name == "A龟壳")
						{
							return new A龟壳();
						}
					}
				}
				else if (name == "喷火器")
				{
					return new 喷火器();
				}
			}
			else if (num <= 3018047877U)
			{
				if (num != 2964383355U)
				{
					if (num != 2990843541U)
					{
						if (num == 3018047877U)
						{
							if (name == "S财富自由群员")
							{
								return new S财富自由群员();
							}
						}
					}
					else if (name == "B大树祝福")
					{
						return new B大树祝福();
					}
				}
				else if (name == "B吸血鬼")
				{
					return new B吸血鬼();
				}
			}
			else if (num <= 3108231136U)
			{
				if (num != 3045660037U)
				{
					if (num == 3108231136U)
					{
						if (name == "C刀扇装甲")
						{
							return new C刀扇装甲();
						}
					}
				}
				else if (name == "C攻击加很多钱")
				{
					return new C攻击加很多钱();
				}
			}
			else if (num != 3112821425U)
			{
				if (num == 3156890210U)
				{
					if (name == "D微小吸收")
					{
						return new D微小吸收();
					}
				}
			}
			else if (name == "S究极护甲")
			{
				return new S究极护甲();
			}
		}
		else if (num <= 3585115383U)
		{
			if (num <= 3244135876U)
			{
				if (num <= 3200926903U)
				{
					if (num != 3168868538U)
					{
						if (num != 3183723411U)
						{
							if (num == 3200926903U)
							{
								if (name == "攻速光环")
								{
									return new 攻速光环();
								}
							}
						}
						else if (name == "敏捷加成")
						{
							return new 敏捷加成();
						}
					}
					else if (name == "D小刀扇装甲")
					{
						return new D小刀扇装甲();
					}
				}
				else if (num != 3213135326U)
				{
					if (num != 3237460452U)
					{
						if (num == 3244135876U)
						{
							if (name == "B大刀扇装甲")
							{
								return new B大刀扇装甲();
							}
						}
					}
					else if (name == "H凝视黑暗")
					{
						return new H凝视黑暗();
					}
				}
				else if (name == "D杀怪加钱")
				{
					return new D杀怪加钱();
				}
			}
			else if (num <= 3390159859U)
			{
				if (num != 3319556031U)
				{
					if (num != 3320494339U)
					{
						if (num == 3390159859U)
						{
							if (name == "C小重生")
							{
								return new C小重生();
							}
						}
					}
					else if (name == "S属性加强")
					{
						return new S属性加强();
					}
				}
				else if (name == "火之呼吸")
				{
					return new 火之呼吸();
				}
			}
			else if (num <= 3442299502U)
			{
				if (num != 3429884026U)
				{
					if (num == 3442299502U)
					{
						if (name == "B多重攻击")
						{
							return new B多重攻击();
						}
					}
				}
				else if (name == "猎物标记斩")
				{
					return new 猎物标记斩();
				}
			}
			else if (num != 3553347243U)
			{
				if (num == 3585115383U)
				{
					if (name == "D小凋零打击")
					{
						return new D小凋零打击();
					}
				}
			}
			else if (name == "剑灵")
			{
				return new 剑灵();
			}
		}
		else if (num <= 3986905974U)
		{
			if (num <= 3818404505U)
			{
				if (num != 3602072616U)
				{
					if (num != 3813426336U)
					{
						if (num == 3818404505U)
						{
							if (name == "B重击")
							{
								return new B重击();
							}
						}
					}
					else if (name == "H忍者旋风")
					{
						return new H忍者旋风();
					}
				}
				else if (name == "RPG")
				{
					return new RPG();
				}
			}
			else if (num <= 3950564396U)
			{
				if (num != 3899538639U)
				{
					if (num == 3950564396U)
					{
						if (name == "C附魔武器")
						{
							return new C附魔武器();
						}
					}
				}
				else if (name == "H复仇电锯")
				{
					return new H复仇电锯();
				}
			}
			else if (num != 3984489253U)
			{
				if (num == 3986905974U)
				{
					if (name == "H对死者的供奉")
					{
						return new H对死者的供奉();
					}
				}
			}
			else if (name == "B真超长臂猿")
			{
				return new B真超长臂猿();
			}
		}
		else if (num <= 4094113267U)
		{
			if (num != 3993231422U)
			{
				if (num != 4003992076U)
				{
					if (num == 4094113267U)
					{
						if (name == "蓝条加成")
						{
							return new 蓝条加成();
						}
					}
				}
				else if (name == "召唤物加成")
				{
					return new 召唤物加成();
				}
			}
			else if (name == "D属性加强")
			{
				return new D属性加强();
			}
		}
		else if (num <= 4164262023U)
		{
			if (num != 4142020514U)
			{
				if (num == 4164262023U)
				{
					if (name == "攻速加成")
					{
						return new 攻速加成();
					}
				}
			}
			else if (name == "D小附魔武器")
			{
				return new D小附魔武器();
			}
		}
		else if (num != 4200677823U)
		{
			if (num == 4203527939U)
			{
				if (name == "D大树祝福")
				{
					return new D大树祝福();
				}
			}
		}
		else if (name == "D荆棘皮肤")
		{
			return new D荆棘皮肤();
		}
		return null;
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0001276C File Offset: 0x0001096C
	public static RoleBuff GetRoleBuff(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2008592230U)
		{
			if (num <= 1389651382U)
			{
				if (num <= 433940088U)
				{
					if (num != 165306690U)
					{
						if (num == 433940088U)
						{
							if (name == "Buff无敌")
							{
								return new Buff无敌();
							}
						}
					}
					else if (name == "Buff回血")
					{
						return new Buff回血();
					}
				}
				else if (num != 452605310U)
				{
					if (num == 1389651382U)
					{
						if (name == "Buff回血固定")
						{
							return new Buff回血固定();
						}
					}
				}
				else if (name == "Buff眩晕")
				{
					return new Buff眩晕();
				}
			}
			else if (num <= 1783708735U)
			{
				if (num != 1546764370U)
				{
					if (num == 1783708735U)
					{
						if (name == "Buff经验神符")
						{
							return new Buff经验神符();
						}
					}
				}
				else if (name == "Buff护甲神符")
				{
					return new Buff护甲神符();
				}
			}
			else if (num != 1867270404U)
			{
				if (num == 2008592230U)
				{
					if (name == "Buff音乐鼓舞")
					{
						return new Buff音乐鼓舞();
					}
				}
			}
			else if (name == "Buff咆哮神符")
			{
				return new Buff咆哮神符();
			}
		}
		else if (num <= 2678184294U)
		{
			if (num <= 2300769662U)
			{
				if (num != 2127772273U)
				{
					if (num == 2300769662U)
					{
						if (name == "Buff攻速加成")
						{
							return new Buff攻速加成();
						}
					}
				}
				else if (name == "Buff回蓝")
				{
					return new Buff回蓝();
				}
			}
			else if (num != 2642407790U)
			{
				if (num == 2678184294U)
				{
					if (name == "Buff护盾神符")
					{
						return new Buff护盾神符();
					}
				}
			}
			else if (name == "Buff通用显示")
			{
				return new Buff通用显示();
			}
		}
		else if (num <= 3645429698U)
		{
			if (num != 3552511177U)
			{
				if (num == 3645429698U)
				{
					if (name == "Buff痊愈神符")
					{
						return new Buff痊愈神符();
					}
				}
			}
			else if (name == "Buff好运神符")
			{
				return new Buff好运神符();
			}
		}
		else if (num != 4050687917U)
		{
			if (num != 4175039047U)
			{
				if (num == 4279229502U)
				{
					if (name == "Buff狂暴神符")
					{
						return new Buff狂暴神符();
					}
				}
			}
			else if (name == "Buff魔法神符")
			{
				return new Buff魔法神符();
			}
		}
		else if (name == "Buff速度神符")
		{
			return new Buff速度神符();
		}
		return null;
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00012A5B File Offset: 0x00010C5B
	public static bool ItemIsAddBag(ItemType itemType)
	{
		return !Util.IsTalisman(itemType) && (itemType < ItemType.STRBook || itemType > ItemType.STABook) && itemType != ItemType.Pick_Sun && itemType != ItemType.Gold && itemType != ItemType.Gem;
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00012A96 File Offset: 0x00010C96
	public static bool IsMedicineItem(ItemType itemType)
	{
		return itemType >= ItemType.Medicine_0 && itemType <= ItemType.Medicine_7;
	}

	// Token: 0x060002DD RID: 733 RVA: 0x00012AAD File Offset: 0x00010CAD
	public static string GetMedicineShopId(ItemType itemType)
	{
		return itemType.ToString();
	}

	// Token: 0x060002DE RID: 734 RVA: 0x00012ABC File Offset: 0x00010CBC
	public static bool IsTalisman(ItemType itemType)
	{
		return itemType >= ItemType.Talisman_Roar && itemType < ItemType.Talisman_Roar + GameHelperClient.TalismanNum;
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00012AD8 File Offset: 0x00010CD8
	public static SaveLoadManager.PlayerKingData GetLocalPlayerKingData()
	{
		SaveLoadManager.PlayerKingData playerKingData = default(SaveLoadManager.PlayerKingData);
		playerKingData.kingName = SteamFriends.GetPersonaName();
		playerKingData.steamID = SteamUser.GetSteamID().m_SteamID;
		playerKingData.heroType = GameHelperClient.localPlayer.heroType;
		playerKingData.level = GameHelperClient.localPlayer.Level;
		int count = GameHelperClient.localPlayer.roleSkillList.Count;
		if (count > 0)
		{
			playerKingData.skill = new SaveLoadManager.PlayerKingSkillData[count];
			for (int i = 0; i < count; i++)
			{
				SkillBase skillBase = GameHelperClient.localPlayer.roleSkillList[i];
				SaveLoadManager.PlayerKingSkillData playerKingSkillData = default(SaveLoadManager.PlayerKingSkillData);
				if (skillBase is PasssiveSkill)
				{
					playerKingSkillData.skillName = PathDefine.Concat("p_", skillBase.skillId);
				}
				else
				{
					playerKingSkillData.skillName = PathDefine.Concat("a_", skillBase.skillId);
				}
				playerKingSkillData.skillData = skillBase.GetSaveSkillData();
				playerKingData.skill[i] = playerKingSkillData;
			}
		}
		int count2 = GameHelperClient.localPlayer.playerAttribute.equipList.Count;
		if (count2 > 0)
		{
			playerKingData.equip = new SaveLoadManager.PlayerKingEquipData[count2];
			for (int j = 0; j < count2; j++)
			{
				EquipBase equipBase = GameHelperClient.localPlayer.playerAttribute.equipList[j];
				SaveLoadManager.PlayerKingEquipData playerKingEquipData = new SaveLoadManager.PlayerKingEquipData
				{
					equip = equipBase.equipIndex,
					equipData = equipBase.level
				};
				if (equipBase.evolutionEntryList != null && equipBase.evolutionEntryList.Count > 0)
				{
					List<string> list = new List<string>();
					for (int k = 0; k < equipBase.evolutionEntryList.Count; k++)
					{
						EquipEvolutionEntryData equipEvolutionEntryData = equipBase.evolutionEntryList[k];
						if (equipEvolutionEntryData != null && equipEvolutionEntryData.IsSkill && equipEvolutionEntryData.equipSkill != EquipSkillType.None)
						{
							list.Add(equipEvolutionEntryData.equipSkill.ToString());
						}
					}
					if (list.Count > 0)
					{
						playerKingEquipData.equipEvolutionSkill = list.ToArray();
					}
				}
				playerKingData.equip[j] = playerKingEquipData;
			}
		}
		int count3 = GameHelperClient.localPlayer.playerAttribute.relicList.Count;
		if (count3 > 0)
		{
			playerKingData.relic = new SaveLoadManager.PlayerKingRelicData[count3];
			for (int l = 0; l < count3; l++)
			{
				RelicBase relicBase = GameHelperClient.localPlayer.playerAttribute.relicList[l];
				SaveLoadManager.PlayerKingRelicData playerKingRelicData = new SaveLoadManager.PlayerKingRelicData
				{
					relicName = relicBase.keyIndex,
					relicLevel = relicBase.level
				};
				playerKingData.relic[l] = playerKingRelicData;
			}
		}
		if (SaveLoadManager.gameSaveData.equipCards != null && SaveLoadManager.gameSaveData.equipCards.Count > 0)
		{
			playerKingData.card = SaveLoadManager.gameSaveData.equipCards.ToArray();
		}
		playerKingData.critical = GameHelperClient.localPlayer.critical;
		playerKingData.criticalDamage = GameHelperClient.localPlayer.criticalDamage;
		playerKingData.attack = ConstDefine.ClampIntValue((double)GameHelperClient.localPlayer.FinalAttackPower);
		playerKingData.attackSpeed = GameHelperClient.localPlayer.GetAttackSpeed();
		playerKingData.attackAddHp = GameHelperClient.localPlayer.xiXue;
		playerKingData.moveSpeed = GameHelperClient.localPlayer.GetMoveSpeed();
		playerKingData.sta = GameHelperClient.localPlayer.STA;
		playerKingData.agi = GameHelperClient.localPlayer.AGI;
		playerKingData.str = GameHelperClient.localPlayer.STR;
		playerKingData.armor = GameHelperClient.localPlayer.armor;
		playerKingData.hpAdd = GameHelperClient.localPlayer.hpAddSec;
		playerKingData.mpAdd = GameHelperClient.localPlayer.mpAddSecRate;
		playerKingData.lucky = GameHelperClient.localPlayer.lucky;
		playerKingData.skillDamage = GameHelperClient.localPlayer.SkillExDamageAll;
		playerKingData.skillRange = GameHelperClient.localPlayer.skillRange;
		playerKingData.skillTime = GameHelperClient.localPlayer.skillAddTime;
		playerKingData.skillExpend = GameHelperClient.localPlayer.skillMpUsed;
		playerKingData.skillCd = GameHelperClient.localPlayer.AllSkillCd;
		playerKingData.normalDamage = GameHelperClient.localPlayer.normalAttackAddDamage;
		playerKingData.maxHp = GameHelperClient.localPlayer.maxHp;
		playerKingData.maxMp = GameHelperClient.localPlayer.maxMp;
		playerKingData.normalBreak = GameHelperClient.localPlayer.normalBreakShield;
		playerKingData.skillBreak = GameHelperClient.localPlayer.skillBreakShield;
		playerKingData.allDamage = GameHelperClient.localPlayer.addDamagePercent;
		playerKingData.lifeStealing = GameHelperClient.localPlayer.XiXueLvAll;
		playerKingData.reduceInjury = GameHelperClient.localPlayer.reduceInjury;
		playerKingData.extraDamage = GameHelperClient.localPlayer.extraDamage;
		playerKingData.dodge = GameHelperClient.localPlayer.FinalDoge;
		playerKingData.hpSecRate = GameHelperClient.localPlayer.hpAddSecRate;
		playerKingData.skillReduction = GameHelperClient.localPlayer.FinalSkillReduction;
		playerKingData.attackDistance = GameHelperClient.localPlayer.exAttackDistance;
		playerKingData.fireDamage = GameHelperClient.localPlayer.skillFireAdd;
		playerKingData.iceDamage = GameHelperClient.localPlayer.skillIceAdd;
		playerKingData.lightDamage = GameHelperClient.localPlayer.skillLightingAdd;
		playerKingData.skillNoneDamage = GameHelperClient.localPlayer.skillNoneAdd;
		playerKingData.effectDamage = GameHelperClient.localPlayer.addAttackEffectDamage;
		playerKingData.buffDamage = GameHelperClient.localPlayer.buffAddDamage;
		playerKingData.relifeTime = GameHelperClient.localPlayer.addRelifeTime;
		playerKingData.addCallMonsterAttack = GameHelperClient.localPlayer.addCallMonsterAttack;
		playerKingData.addCallMonsterHp = GameHelperClient.localPlayer.addCallMonsterHp;
		playerKingData.addHenshin = GameHelperClient.localPlayer.addHenshin;
		playerKingData.haloRangeAdd = GameHelperClient.localPlayer.haloRangeAdd;
		playerKingData.castSpeed = GameHelperClient.localPlayer.castSpeed;
		playerKingData.magicXiXue = GameHelperClient.localPlayer.magicXiXue;
		playerKingData.hpAddUpgrade = GameHelperClient.localPlayer.hpAddUpgrade;
		playerKingData.addHatred = GameHelperClient.localPlayer.addHatred;
		playerKingData.addHenshinTime = GameHelperClient.localPlayer.addHenshinTime;
		playerKingData.addCallMonsterSize = GameHelperClient.localPlayer.addCallMonsterSize;
		playerKingData.addCallMonsterTime = GameHelperClient.localPlayer.addCallMonsterTime;
		playerKingData.armedAdd = GameHelperClient.localPlayer.armedAdd;
		playerKingData.equipAdd = GameHelperClient.localPlayer.equipAddValue;
		playerKingData.level = GameHelperClient.localPlayer.Level;
		playerKingData.allMoney = GameHelperClient.localPlayer.getGoldNum;
		playerKingData.allGem = GameHelperClient.localPlayer.getGemNum;
		ShopManager shopManager = EntityStatic.Get<ShopManager>();
		if (shopManager != null)
		{
			playerKingData.forgeAdd = shopManager.forgingManager.forgingAdd;
		}
		return playerKingData;
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0001316C File Offset: 0x0001136C
	public static List<ItemType> GetDropItem(string dropKey, float luckyAdd)
	{
		DropData dropData;
		if (!Game.GameData.DropDataDic.TryGetValue(dropKey, out dropData))
		{
			return null;
		}
		int num = Random.Range(dropData.dropNum.min, dropData.dropNum.max + 1);
		if (num == 0)
		{
			return null;
		}
		Util.DropItemList.Clear();
		Util.DropItemTemp_0.Clear();
		Util.DropItemTemp_1.Clear();
		Util.DropItemTemp_2.Clear();
		Util.DropItemTemp_3.Clear();
		Util.DropItemTemp_4.Clear();
		foreach (DropItemData dropItemData in dropData.dropItems)
		{
			for (int j = dropItemData.startItem; j <= dropItemData.endItem; j++)
			{
				ItemData itemData;
				if (Game.GameData.ItemDataDic.TryGetValue((ItemType)j, out itemData))
				{
					Util.DropItemTemp[itemData.quality].Add(j);
				}
			}
		}
		for (int k = 0; k < num; k++)
		{
			int dropGrade = Util.GetDropGrade(dropData.dropWeight, luckyAdd);
			List<int> list = Util.DropItemTemp[dropGrade];
			int index = Random.Range(0, list.Count);
			Util.DropItemList.Add((ItemType)list[index]);
			list.RemoveAt(index);
		}
		return Util.DropItemList;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x000132AC File Offset: 0x000114AC
	private static int GetDropGrade(float[] dropWeight, float luckyAdd)
	{
		int num = dropWeight.Length;
		float num2 = 0f;
		float num3 = 0f;
		for (int i = 0; i < num; i++)
		{
			if (Util.DropItemTemp[i].Count != 0)
			{
				num3 += dropWeight[i] * (1f + luckyAdd * Util.DropLuckAdd[i]);
			}
		}
		float num4 = Random.value * num3;
		for (int j = 0; j < num; j++)
		{
			if (Util.DropItemTemp[j].Count != 0)
			{
				num2 += dropWeight[j] * (1f + luckyAdd * Util.DropLuckAdd[j]);
				if (num4 < num2)
				{
					return j;
				}
			}
		}
		return 0;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00013348 File Offset: 0x00011548
	private static List<int> GetContractRandomList(Dictionary<int, ContractData> contractDataDic, int quality)
	{
		List<int> list = new List<int>();
		foreach (ContractData contractData in contractDataDic.Values)
		{
			if (contractData.quality == quality)
			{
				list.Add(contractData.id);
			}
		}
		return list;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x000133B0 File Offset: 0x000115B0
	private static int GetContractWaveUpValue(int baseValue, float waveUpLevel)
	{
		int num = Mathf.Max(0, GameHelperClient.WaveNum);
		return Mathf.RoundToInt((float)baseValue * waveUpLevel * (float)num);
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x000133D5 File Offset: 0x000115D5
	private static void ApplyContractLimit(ContractData contractData, ref int minValue, ref int maxValue)
	{
		if (!contractData.hasLimit)
		{
			return;
		}
		minValue = Mathf.Min(minValue, contractData.limit);
		maxValue = Mathf.Min(maxValue, contractData.limit);
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00013400 File Offset: 0x00011600
	private static RoguelikeUIData CreateDemonContractRoguelikeData(ContractData positiveContractData, ContractData negativeContractData)
	{
		int num = positiveContractData.minValue + Util.GetContractWaveUpValue(positiveContractData.minValue, positiveContractData.waveUpLevel);
		int num2 = positiveContractData.maxValue + Util.GetContractWaveUpValue(positiveContractData.maxValue, positiveContractData.waveUpLevel);
		Util.ApplyContractLimit(positiveContractData, ref num, ref num2);
		int num3 = (num == num2) ? num : Random.Range(num, num2 + 1);
		int num4 = negativeContractData.minValue + Util.GetContractWaveUpValue(negativeContractData.minValue, negativeContractData.waveUpLevel);
		int num5 = negativeContractData.maxValue + Util.GetContractWaveUpValue(negativeContractData.maxValue, negativeContractData.waveUpLevel);
		Util.ApplyContractLimit(negativeContractData, ref num4, ref num5);
		int num6 = -num5;
		int num7 = -num4;
		int num8 = (num6 == num7) ? num6 : Random.Range(num6, num7 + 1);
		return new RoguelikeUIData
		{
			name = Game.Language.Get("forging_" + positiveContractData.type, "") + "&" + string.Format(ColorDefine.RedForColor, Game.Language.Get("forging_" + negativeContractData.type, "")),
			icon = "Bundles/UI/Icon/Remains/" + positiveContractData.icon,
			dec = Util.GetContractDec(positiveContractData, num3, num, num2) + StringDefine.Wrap + Util.GetContractDec(negativeContractData, num8, num6, num7),
			data = PathDefine.Concat(positiveContractData.type, StringDefine.Underline, num3) + "|" + PathDefine.Concat(negativeContractData.type, StringDefine.Underline, num8),
			quality = -1
		};
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x000135A0 File Offset: 0x000117A0
	public static void OnDemonContract(Action closeAction)
	{
		Dictionary<int, ContractData> contractDataDic = Game.GameData.ContractDataDic;
		List<int> contractRandomList = Util.GetContractRandomList(contractDataDic, 0);
		List<int> contractRandomList2 = Util.GetContractRandomList(contractDataDic, 1);
		RoguelikeUIData[] array = new RoguelikeUIData[3];
		int count = contractRandomList.Count;
		int count2 = contractRandomList2.Count;
		if (count == 0 || count2 == 0)
		{
			return;
		}
		for (int i = 0; i < count; i++)
		{
			int num = Random.Range(0, count);
			List<int> list = contractRandomList;
			int index = i;
			List<int> list2 = contractRandomList;
			int index2 = num;
			int value = contractRandomList[num];
			int value2 = contractRandomList[i];
			list[index] = value;
			list2[index2] = value2;
		}
		for (int j = 0; j < count2; j++)
		{
			int num2 = Random.Range(0, count2);
			List<int> list3 = contractRandomList2;
			int index2 = j;
			List<int> list2 = contractRandomList2;
			int index = num2;
			int value2 = contractRandomList2[num2];
			int value = contractRandomList2[j];
			list3[index2] = value2;
			list2[index] = value;
		}
		for (int k = 0; k < 3; k++)
		{
			ContractData positiveContractData = contractDataDic[contractRandomList[k % count]];
			ContractData negativeContractData = contractDataDic[contractRandomList2[k % count2]];
			array[k] = Util.CreateDemonContractRoguelikeData(positiveContractData, negativeContractData);
		}
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		Game.AudioManager.PlayAudio("Audio/Battle_Audio/NPC/npc_ghost_appear", 1f, 3f);
		ui_Roguelike.ShowRoguelike(array, new Action<RoguelikeUIData>(Util.OnDemonContractSelect), Game.Language.Get("契约选择", ""), null, closeAction, 0.5f, null, "demon_contract");
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00013738 File Offset: 0x00011938
	private static void OnDemonContractSelect(RoguelikeUIData roguelikeUIData)
	{
		GameHelperClient.localPlayer.CmdDemonContract();
		Game.AudioManager.PlayAudio("Audio/Battle_Audio/NPC/npc_ghost_laugh", 1f, 3f);
		string[] array = roguelikeUIData.data.Split('|', StringSplitOptions.None);
		for (int i = 0; i < array.Length; i++)
		{
			Util.ApplyDemonContractData(array[i]);
		}
		(GameHelperClient.localPlayer.AddRelic(101, 0) as RelicDemonContract).exDec = roguelikeUIData.dec;
		UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshBaoJi();
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x000137C0 File Offset: 0x000119C0
	private static void ApplyDemonContractData(string contractData)
	{
		string[] array = contractData.Split(StringDefine.Underline, StringSplitOptions.None);
		if (array.Length < 2)
		{
			return;
		}
		Util.ApplyDemonContractData(array[0], int.Parse(array[1]));
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x000137F4 File Offset: 0x000119F4
	private static void ApplyDemonContractData(string dataType, int dataValue)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(dataType);
		if (num <= 2000437136U)
		{
			if (num <= 853522520U)
			{
				if (num <= 591723698U)
				{
					if (num <= 399922361U)
					{
						if (num != 326073063U)
						{
							if (num != 399922361U)
							{
								return;
							}
							if (!(dataType == "Sta"))
							{
								return;
							}
							GameHelperClient.localPlayer.AddSTA(dataValue);
							return;
						}
						else
						{
							if (!(dataType == "ArmedAdd"))
							{
								return;
							}
							GameHelperClient.localPlayer.armedAdd += (float)dataValue * 0.01f;
							return;
						}
					}
					else if (num != 521850149U)
					{
						if (num != 591723698U)
						{
							return;
						}
						if (!(dataType == "HPSec"))
						{
							return;
						}
						GameHelperClient.localPlayer.AddHpAddSec(dataValue);
						return;
					}
					else
					{
						if (!(dataType == "SkillAdd"))
						{
							return;
						}
						GameHelperClient.localPlayer.skillExDamage += (float)dataValue * 0.01f;
						return;
					}
				}
				else if (num <= 776144995U)
				{
					if (num != 618031408U)
					{
						if (num != 776144995U)
						{
							return;
						}
						if (!(dataType == "Three"))
						{
							return;
						}
						GameHelperClient.localPlayer.AddSTR(dataValue);
						GameHelperClient.localPlayer.AddSTA(dataValue);
						GameHelperClient.localPlayer.AddAGI(dataValue);
						return;
					}
					else
					{
						if (!(dataType == "Str"))
						{
							return;
						}
						GameHelperClient.localPlayer.AddSTR(dataValue);
						return;
					}
				}
				else if (num != 785119009U)
				{
					if (num != 853522520U)
					{
						return;
					}
					if (!(dataType == "MP"))
					{
						return;
					}
					GameHelperClient.localPlayer.AddMaxMp(dataValue);
					return;
				}
				else
				{
					if (!(dataType == "SummonAdd"))
					{
						return;
					}
					GameHelperClient.localPlayer.addCallMonsterAttack += (float)dataValue * 0.01f;
					GameHelperClient.localPlayer.addCallMonsterHp += (float)dataValue * 0.01f;
					return;
				}
			}
			else if (num <= 1287009716U)
			{
				if (num <= 1041612137U)
				{
					if (num != 895613287U)
					{
						if (num != 1041612137U)
						{
							return;
						}
						if (!(dataType == "CriticalDamage"))
						{
							return;
						}
						GameHelperClient.localPlayer.AddCriticalDamage((float)dataValue * 0.01f);
						return;
					}
					else
					{
						if (!(dataType == "SkillHit"))
						{
							return;
						}
						GameHelperClient.localPlayer.UpdateSkillHitDamage(dataValue);
						return;
					}
				}
				else if (num != 1182039611U)
				{
					if (num != 1287009716U)
					{
						return;
					}
					if (!(dataType == "XiXue"))
					{
						return;
					}
					GameHelperClient.localPlayer.AddXiXue((float)dataValue);
					return;
				}
				else
				{
					if (!(dataType == "ExpAdd"))
					{
						return;
					}
					GameHelperClient.localPlayer.addExp += (float)dataValue * 0.01f;
					return;
				}
			}
			else if (num <= 1680846624U)
			{
				if (num != 1483691181U)
				{
					if (num != 1680846624U)
					{
						return;
					}
					if (!(dataType == "HpSecRate"))
					{
						return;
					}
					GameHelperClient.localPlayer.hpAddSecRate += (float)dataValue * 0.01f;
					return;
				}
				else
				{
					if (!(dataType == "SkillBreak"))
					{
						return;
					}
					GameHelperClient.localPlayer.skillBreakShieldBase += (float)dataValue * 0.01f;
					GameHelperClient.localPlayer.UpdateBreakShield();
					return;
				}
			}
			else if (num != 1806474955U)
			{
				if (num != 1894470373U)
				{
					if (num != 2000437136U)
					{
						return;
					}
					if (!(dataType == "Luck"))
					{
						return;
					}
					GameHelperClient.localPlayer.CmdUpdateLucky(dataValue);
					return;
				}
				else
				{
					if (!(dataType == "HP"))
					{
						return;
					}
					GameHelperClient.localPlayer.CmdUpdateMaxHp((long)dataValue, GameHelperClient.localPlayer.netId);
					return;
				}
			}
			else
			{
				if (!(dataType == "HenshinAdd"))
				{
					return;
				}
				GameHelperClient.localPlayer.addHenshin += (float)dataValue * 0.01f;
				return;
			}
		}
		else if (num <= 2752153801U)
		{
			if (num <= 2278596520U)
			{
				if (num <= 2107383588U)
				{
					if (num != 2076688730U)
					{
						if (num != 2107383588U)
						{
							return;
						}
						if (!(dataType == "ExtraDamage"))
						{
							return;
						}
						GameHelperClient.localPlayer.extraDamage += dataValue;
						return;
					}
					else
					{
						if (!(dataType == "HaloAdd"))
						{
							return;
						}
						GameHelperClient.localPlayer.buffAddDamage += (float)dataValue * 0.01f;
						return;
					}
				}
				else if (num != 2226667892U)
				{
					if (num != 2278596520U)
					{
						return;
					}
					if (!(dataType == "Critical"))
					{
						return;
					}
					GameHelperClient.localPlayer.AddCritical((float)dataValue * 0.01f);
					return;
				}
				else
				{
					if (!(dataType == "Armor"))
					{
						return;
					}
					GameHelperClient.localPlayer.AddArmor(dataValue);
					return;
				}
			}
			else if (num <= 2343121693U)
			{
				if (num != 2311178276U)
				{
					if (num != 2343121693U)
					{
						return;
					}
					if (!(dataType == "Attack"))
					{
						return;
					}
					GameHelperClient.localPlayer.AddAttackPower(dataValue);
					return;
				}
				else
				{
					if (!(dataType == "Doge"))
					{
						return;
					}
					GameHelperClient.localPlayer.doge += dataValue;
					GameHelperClient.localPlayer.CmdDoge(GameHelperClient.localPlayer.doge);
					return;
				}
			}
			else if (num != 2374343684U)
			{
				if (num != 2462836616U)
				{
					if (num != 2752153801U)
					{
						return;
					}
					if (!(dataType == "NormalAdd"))
					{
						return;
					}
					GameHelperClient.localPlayer.normalAttackAddDamage += (float)dataValue * 0.01f;
					return;
				}
				else
				{
					if (!(dataType == "Agi"))
					{
						return;
					}
					GameHelperClient.localPlayer.AddAGI(dataValue);
					return;
				}
			}
			else
			{
				if (!(dataType == "ReduceInjury"))
				{
					return;
				}
				GameHelperClient.localPlayer.UpdateReduce(dataValue);
				return;
			}
		}
		else if (num <= 3371345849U)
		{
			if (num <= 3354445110U)
			{
				if (num != 3135110330U)
				{
					if (num != 3354445110U)
					{
						return;
					}
					if (!(dataType == "GoldAdd"))
					{
						return;
					}
					GameHelperClient.localPlayer.addGoldPercent += (float)dataValue * 0.01f;
					return;
				}
				else
				{
					if (!(dataType == "HpPercent"))
					{
						return;
					}
					GameHelperClient.localPlayer.CmdUpdateMaxHpAddPercent((float)dataValue * 0.01f);
					return;
				}
			}
			else if (num != 3365943077U)
			{
				if (num != 3371345849U)
				{
					return;
				}
				if (!(dataType == "NormalBreak"))
				{
					return;
				}
				GameHelperClient.localPlayer.normalBreakShieldBase += (float)dataValue * 0.01f;
				GameHelperClient.localPlayer.UpdateBreakShield();
				return;
			}
			else
			{
				if (!(dataType == "AddDamage"))
				{
					return;
				}
				GameHelperClient.localPlayer.addDamagePercent += (float)dataValue * 0.01f;
				return;
			}
		}
		else if (num <= 3581048735U)
		{
			if (num != 3439798920U)
			{
				if (num != 3581048735U)
				{
					return;
				}
				if (!(dataType == "MoveSpeed"))
				{
					return;
				}
				GameHelperClient.localPlayer.AddMoveSpeed((float)dataValue);
				return;
			}
			else
			{
				if (!(dataType == "AttackSpeed"))
				{
					return;
				}
				GameHelperClient.localPlayer.AddAttackSpeed((float)dataValue * 0.01f);
				return;
			}
		}
		else if (num != 3990130861U)
		{
			if (num != 4235090694U)
			{
				if (num != 4292234280U)
				{
					return;
				}
				if (!(dataType == "CoolDown"))
				{
					return;
				}
				GameHelperClient.localPlayer.skillCdReduce += dataValue;
				return;
			}
			else
			{
				if (!(dataType == "XiXueRate"))
				{
					return;
				}
				GameHelperClient.localPlayer.xiXueLv += (float)dataValue * 0.01f;
				return;
			}
		}
		else
		{
			if (!(dataType == "MPSec"))
			{
				return;
			}
			GameHelperClient.localPlayer.AddMpAddSec(dataValue);
			return;
		}
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00013F58 File Offset: 0x00012158
	public static string GetContractDec(ContractData contractData, int value, int minValue, int maxValue)
	{
		string text = "";
		string type = contractData.type;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(type);
		if (num <= 2000437136U)
		{
			if (num <= 853522520U)
			{
				if (num <= 591723698U)
				{
					if (num <= 399922361U)
					{
						if (num != 326073063U)
						{
							if (num == 399922361U)
							{
								if (type == "Sta")
								{
									text = Game.Language.Get("sta", "");
								}
							}
						}
						else if (type == "ArmedAdd")
						{
							text = Game.Language.Get("武装伤害", "");
						}
					}
					else if (num != 521850149U)
					{
						if (num == 591723698U)
						{
							if (type == "HPSec")
							{
								text = Game.Language.Get("hpAddSec", "");
							}
						}
					}
					else if (type == "SkillAdd")
					{
						text = Game.Language.Get("法术伤害加成", "");
					}
				}
				else if (num <= 776144995U)
				{
					if (num != 618031408U)
					{
						if (num == 776144995U)
						{
							if (type == "Three")
							{
								text = Game.Language.Get("全属性", "");
							}
						}
					}
					else if (type == "Str")
					{
						text = Game.Language.Get("str", "");
					}
				}
				else if (num != 785119009U)
				{
					if (num == 853522520U)
					{
						if (type == "MP")
						{
							text = Game.Language.Get("法力值", "");
						}
					}
				}
				else if (type == "SummonAdd")
				{
					text = Game.Language.Get("召唤物强度", "");
				}
			}
			else if (num <= 1287009716U)
			{
				if (num <= 1041612137U)
				{
					if (num != 895613287U)
					{
						if (num == 1041612137U)
						{
							if (type == "CriticalDamage")
							{
								text = Game.Language.Get("baojiDamage", "");
							}
						}
					}
					else if (type == "SkillHit")
					{
						text = Game.Language.Get("技能抵抗", "");
					}
				}
				else if (num != 1182039611U)
				{
					if (num == 1287009716U)
					{
						if (type == "XiXue")
						{
							text = Game.Language.Get("xixue", "");
						}
					}
				}
				else if (type == "ExpAdd")
				{
					text = Game.Language.Get("经验获取", "");
				}
			}
			else if (num <= 1680846624U)
			{
				if (num != 1483691181U)
				{
					if (num == 1680846624U)
					{
						if (type == "HpSecRate")
						{
							text = Game.Language.Get("hpAddSec", "");
						}
					}
				}
				else if (type == "SkillBreak")
				{
					text = Game.Language.Get("法术破盾伤害", "");
				}
			}
			else if (num != 1806474955U)
			{
				if (num != 1894470373U)
				{
					if (num == 2000437136U)
					{
						if (type == "Luck")
						{
							text = Game.Language.Get("幸运值", "");
						}
					}
				}
				else if (type == "HP")
				{
					text = Game.Language.Get("生命值", "");
				}
			}
			else if (type == "HenshinAdd")
			{
				text = Game.Language.Get("变身强度", "");
			}
		}
		else if (num <= 2752153801U)
		{
			if (num <= 2278596520U)
			{
				if (num <= 2107383588U)
				{
					if (num != 2076688730U)
					{
						if (num == 2107383588U)
						{
							if (type == "ExtraDamage")
							{
								text = Game.Language.Get("exs", "");
							}
						}
					}
					else if (type == "HaloAdd")
					{
						text = Game.Language.Get("BUFF伤害加成", "");
					}
				}
				else if (num != 2226667892U)
				{
					if (num == 2278596520U)
					{
						if (type == "Critical")
						{
							text = Game.Language.Get("baoji", "");
						}
					}
				}
				else if (type == "Armor")
				{
					text = Game.Language.Get("armor", "");
				}
			}
			else if (num <= 2343121693U)
			{
				if (num != 2311178276U)
				{
					if (num == 2343121693U)
					{
						if (type == "Attack")
						{
							text = Game.Language.Get("attack", "");
						}
					}
				}
				else if (type == "Doge")
				{
					text = Game.Language.Get("闪避值", "");
				}
			}
			else if (num != 2374343684U)
			{
				if (num != 2462836616U)
				{
					if (num == 2752153801U)
					{
						if (type == "NormalAdd")
						{
							text = Game.Language.Get("物理伤害加成", "");
						}
					}
				}
				else if (type == "Agi")
				{
					text = Game.Language.Get("dex", "");
				}
			}
			else if (type == "ReduceInjury")
			{
				text = Game.Language.Get("gdj", "");
			}
		}
		else if (num <= 3371345849U)
		{
			if (num <= 3354445110U)
			{
				if (num != 3135110330U)
				{
					if (num == 3354445110U)
					{
						if (type == "GoldAdd")
						{
							text = Game.Language.Get("金币获取", "");
						}
					}
				}
				else if (type == "HpPercent")
				{
					text = Game.Language.Get("最大生命值提升", "");
				}
			}
			else if (num != 3365943077U)
			{
				if (num == 3371345849U)
				{
					if (type == "NormalBreak")
					{
						text = Game.Language.Get("物理破盾加成", "");
					}
				}
			}
			else if (type == "AddDamage")
			{
				text = Game.Language.Get("总伤害加成", "");
			}
		}
		else if (num <= 3581048735U)
		{
			if (num != 3439798920U)
			{
				if (num == 3581048735U)
				{
					if (type == "MoveSpeed")
					{
						text = Game.Language.Get("moveSpeed", "");
					}
				}
			}
			else if (type == "AttackSpeed")
			{
				text = Game.Language.Get("attackSpeed", "");
			}
		}
		else if (num != 3990130861U)
		{
			if (num != 4235090694U)
			{
				if (num == 4292234280U)
				{
					if (type == "CoolDown")
					{
						text = Game.Language.Get("技能急速", "");
					}
				}
			}
			else if (type == "XiXueRate")
			{
				text = Game.Language.Get("攻击生命偷取", "");
			}
		}
		else if (type == "MPSec")
		{
			text = Game.Language.Get("mpAddSec", "");
		}
		bool flag = value < 0;
		string text2;
		if (contractData.isPercent)
		{
			if (Mathf.Approximately((float)minValue, (float)maxValue))
			{
				text2 = (flag ? string.Format(StringDefine.ContractDecPercentConst, value, text) : string.Format(StringDefine.ForgingDecPercentConst, value, text));
			}
			else
			{
				text2 = (flag ? string.Format(StringDefine.ContractDecPercent, new object[]
				{
					value,
					text,
					minValue,
					maxValue
				}) : string.Format(StringDefine.ForgingDecPercent, new object[]
				{
					value,
					text,
					minValue,
					maxValue
				}));
			}
		}
		else if (Mathf.Approximately((float)minValue, (float)maxValue))
		{
			text2 = (flag ? string.Format(StringDefine.ContractDecConst, value, text) : string.Format(StringDefine.ForgingDecConst, value, text));
		}
		else
		{
			text2 = (flag ? string.Format(StringDefine.ContractDec, new object[]
			{
				value,
				text,
				minValue,
				maxValue
			}) : string.Format(StringDefine.ForgingDec, new object[]
			{
				value,
				text,
				minValue,
				maxValue
			}));
		}
		if (!flag)
		{
			text2 = PathDefine.Concat(StringDefine.AddColor, text2);
		}
		return text2;
	}

	// Token: 0x060002EB RID: 747 RVA: 0x000149B7 File Offset: 0x00012BB7
	public static string FormatFloat(float value)
	{
		return value.ToString("0.#");
	}

	// Token: 0x0400026E RID: 622
	private static SOBrotatoWeaponConfig soBrotatoWeaponConfig;

	// Token: 0x0400026F RID: 623
	public const int sortingOrderDefault = 5000;

	// Token: 0x04000270 RID: 624
	public static Vector3 visualMouse = new Vector3(500f, 500f, 0f);

	// Token: 0x04000271 RID: 625
	private static List<ItemType> relicRoguelikeList;

	// Token: 0x04000272 RID: 626
	private static int roguelikeIndex;

	// Token: 0x04000273 RID: 627
	private static List<int> DropItemTemp_0 = new List<int>();

	// Token: 0x04000274 RID: 628
	private static List<int> DropItemTemp_1 = new List<int>();

	// Token: 0x04000275 RID: 629
	private static List<int> DropItemTemp_2 = new List<int>();

	// Token: 0x04000276 RID: 630
	private static List<int> DropItemTemp_3 = new List<int>();

	// Token: 0x04000277 RID: 631
	private static List<int> DropItemTemp_4 = new List<int>();

	// Token: 0x04000278 RID: 632
	private static List<int>[] DropItemTemp = new List<int>[]
	{
		Util.DropItemTemp_0,
		Util.DropItemTemp_1,
		Util.DropItemTemp_2,
		Util.DropItemTemp_3,
		Util.DropItemTemp_4
	};

	// Token: 0x04000279 RID: 633
	private static float[] DropLuckAdd = new float[]
	{
		0f,
		1f,
		2f,
		4f,
		6f
	};

	// Token: 0x0400027A RID: 634
	private static List<ItemType> DropItemList = new List<ItemType>();
}
