using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class EntityMono : MonoBehaviour
{
	// Token: 0x060000CB RID: 203 RVA: 0x00005F6C File Offset: 0x0000416C
	public T AddComp<T>() where T : new()
	{
		Type typeFromHandle = typeof(T);
		T t = (T)((object)Activator.CreateInstance(typeFromHandle));
		if (!this.compDic.ContainsKey(typeFromHandle))
		{
			this.compDic.Add(typeFromHandle, t);
		}
		else
		{
			Debug.LogError("不能重复添加组件");
		}
		return t;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00005FC0 File Offset: 0x000041C0
	public T AddMonoComp<T>() where T : MonoBehaviour
	{
		Type typeFromHandle = typeof(T);
		T t = Object.FindObjectOfType<T>();
		if (t == null)
		{
			t = new GameObject(typeof(T).ToString()).AddComponent<T>();
		}
		if (!this.compDic.ContainsKey(typeFromHandle))
		{
			this.compDic.Add(typeFromHandle, t);
		}
		else
		{
			Debug.LogError("不能重复添加组件");
		}
		return t;
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00006034 File Offset: 0x00004234
	public T AddMonoComp<T>(GameObject go) where T : MonoBehaviour
	{
		Type typeFromHandle = typeof(T);
		T component = go.GetComponent<T>();
		if (!this.compDic.ContainsKey(typeFromHandle))
		{
			this.compDic.Add(typeFromHandle, component);
		}
		else
		{
			Debug.LogError("不能重复添加组件");
		}
		return component;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00006080 File Offset: 0x00004280
	public void ClearMonoComp()
	{
		this.compDic.Clear();
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00006090 File Offset: 0x00004290
	public T GetComp<T>()
	{
		Type typeFromHandle = typeof(T);
		if (this.compDic.ContainsKey(typeFromHandle))
		{
			return (T)((object)this.compDic[typeFromHandle]);
		}
		return default(T);
	}

	// Token: 0x04000101 RID: 257
	public Dictionary<Type, object> compDic = new Dictionary<Type, object>();
}
