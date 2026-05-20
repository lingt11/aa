using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class EntityStatic
{
	// Token: 0x060000D1 RID: 209 RVA: 0x000060E4 File Offset: 0x000042E4
	public static void Clear()
	{
		EntityStatic.compDic.Clear();
		EntityStatic.updateList.Clear();
		EntityStatic.fixedUpdateList.Clear();
		EntityStatic.lateUpdateList.Clear();
		EntityStatic.applicationList.Clear();
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00006118 File Offset: 0x00004318
	protected static void AddComp(Type type)
	{
		object t = Activator.CreateInstance(type);
		EntityStatic.AddToComp(type, t);
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00006134 File Offset: 0x00004334
	private static void AddToComp(Type type, object t)
	{
		if (!EntityStatic.compDic.ContainsKey(type))
		{
			EntityStatic.compDic.Add(type, t);
			IUpdate update = t as IUpdate;
			if (update != null)
			{
				EntityStatic.updateList.Add(update);
			}
			IFixedUpdate fixedUpdate = t as IFixedUpdate;
			if (fixedUpdate != null)
			{
				EntityStatic.fixedUpdateList.Add(fixedUpdate);
			}
			ILateUpdate lateUpdate = t as ILateUpdate;
			if (lateUpdate != null)
			{
				EntityStatic.lateUpdateList.Add(lateUpdate);
			}
			IApplicationQuit applicationQuit = t as IApplicationQuit;
			if (applicationQuit != null)
			{
				EntityStatic.applicationList.Add(applicationQuit);
			}
			Entity entity = t as Entity;
			if (entity == null)
			{
				return;
			}
			foreach (IUpdate item in entity.updateList)
			{
				EntityStatic.updateList.Add(item);
			}
			foreach (IFixedUpdate item2 in entity.fixedUpdateList)
			{
				EntityStatic.fixedUpdateList.Add(item2);
			}
			foreach (ILateUpdate item3 in entity.lateUpdateList)
			{
				EntityStatic.lateUpdateList.Add(item3);
			}
			using (List<IApplicationQuit>.Enumerator enumerator4 = entity.applicationList.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					IApplicationQuit item4 = enumerator4.Current;
					EntityStatic.applicationList.Add(item4);
				}
				return;
			}
		}
		Debug.LogError("不能重复添加组件" + ((type != null) ? type.ToString() : null));
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00006308 File Offset: 0x00004508
	public static T AddComp<T>() where T : new()
	{
		Type typeFromHandle = typeof(T);
		T t = (T)((object)Activator.CreateInstance(typeFromHandle));
		EntityStatic.AddToComp(typeFromHandle, t);
		return t;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00006338 File Offset: 0x00004538
	public static T AddNewComp<T>() where T : new()
	{
		Type typeFromHandle = typeof(T);
		T t = (T)((object)Activator.CreateInstance(typeFromHandle));
		if (!EntityStatic.compDic.ContainsKey(typeFromHandle))
		{
			EntityStatic.compDic.Add(typeFromHandle, t);
		}
		else
		{
			EntityStatic.compDic[typeFromHandle] = t;
		}
		IUpdate update = t as IUpdate;
		if (update != null)
		{
			EntityStatic.updateList.Add(update);
		}
		IFixedUpdate fixedUpdate = t as IFixedUpdate;
		if (fixedUpdate != null)
		{
			EntityStatic.fixedUpdateList.Add(fixedUpdate);
		}
		ILateUpdate lateUpdate = t as ILateUpdate;
		if (lateUpdate != null)
		{
			EntityStatic.lateUpdateList.Add(lateUpdate);
		}
		IApplicationQuit applicationQuit = t as IApplicationQuit;
		if (applicationQuit != null)
		{
			EntityStatic.applicationList.Add(applicationQuit);
		}
		return t;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x000063FC File Offset: 0x000045FC
	public static void RemoveComp<T>()
	{
		Type typeFromHandle = typeof(T);
		object obj = EntityStatic.compDic[typeFromHandle];
		if (obj != null)
		{
			IUpdate update = obj as IUpdate;
			if (update != null)
			{
				EntityStatic.updateList.Remove(update);
			}
			IFixedUpdate fixedUpdate = obj as IFixedUpdate;
			if (fixedUpdate != null)
			{
				EntityStatic.fixedUpdateList.Remove(fixedUpdate);
			}
			ILateUpdate lateUpdate = obj as ILateUpdate;
			if (lateUpdate != null)
			{
				EntityStatic.lateUpdateList.Remove(lateUpdate);
			}
			IApplicationQuit applicationQuit = obj as IApplicationQuit;
			if (applicationQuit != null)
			{
				EntityStatic.applicationList.Remove(applicationQuit);
			}
			IDispose dispose = obj as IDispose;
			if (dispose != null)
			{
				dispose.Dispose();
			}
			Entity entity = obj as Entity;
			if (entity != null)
			{
				foreach (IUpdate item in entity.updateList)
				{
					EntityStatic.updateList.Remove(item);
				}
				foreach (IFixedUpdate item2 in entity.fixedUpdateList)
				{
					EntityStatic.fixedUpdateList.Remove(item2);
				}
				foreach (ILateUpdate item3 in entity.lateUpdateList)
				{
					EntityStatic.lateUpdateList.Remove(item3);
				}
				foreach (IApplicationQuit item4 in entity.applicationList)
				{
					EntityStatic.applicationList.Remove(item4);
				}
				entity.Dispose();
			}
		}
		EntityStatic.compDic.Remove(typeFromHandle);
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x000065EC File Offset: 0x000047EC
	public static T Get<T>()
	{
		Type typeFromHandle = typeof(T);
		if (EntityStatic.compDic.ContainsKey(typeFromHandle))
		{
			return (T)((object)EntityStatic.compDic[typeFromHandle]);
		}
		return default(T);
	}

	// Token: 0x04000102 RID: 258
	private static Dictionary<Type, object> compDic = new Dictionary<Type, object>(16);

	// Token: 0x04000103 RID: 259
	protected static List<IUpdate> updateList = new List<IUpdate>(16);

	// Token: 0x04000104 RID: 260
	protected static List<IFixedUpdate> fixedUpdateList = new List<IFixedUpdate>(16);

	// Token: 0x04000105 RID: 261
	protected static List<ILateUpdate> lateUpdateList = new List<ILateUpdate>(16);

	// Token: 0x04000106 RID: 262
	protected static List<IApplicationQuit> applicationList = new List<IApplicationQuit>(16);
}
