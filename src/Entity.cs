using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class Entity
{
	// Token: 0x060000C6 RID: 198 RVA: 0x00005C78 File Offset: 0x00003E78
	public T AddComp<T>(params object[] datas)
	{
		Type typeFromHandle = typeof(T);
		T t = (T)((object)Activator.CreateInstance(typeFromHandle, datas));
		if (!this.compDic.ContainsKey(typeFromHandle))
		{
			this.compDic.Add(typeFromHandle, t);
			IUpdate update = t as IUpdate;
			if (update != null)
			{
				this.updateList.Add(update);
			}
			IFixedUpdate fixedUpdate = t as IFixedUpdate;
			if (fixedUpdate != null)
			{
				this.fixedUpdateList.Add(fixedUpdate);
			}
			ILateUpdate lateUpdate = t as ILateUpdate;
			if (lateUpdate != null)
			{
				this.lateUpdateList.Add(lateUpdate);
			}
			IApplicationQuit applicationQuit = t as IApplicationQuit;
			if (applicationQuit != null)
			{
				this.applicationList.Add(applicationQuit);
			}
		}
		else
		{
			Debug.LogError("不能重复添加组件");
		}
		return t;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00005D40 File Offset: 0x00003F40
	public T GetComp<T>()
	{
		Type typeFromHandle = typeof(T);
		if (this.compDic.ContainsKey(typeFromHandle))
		{
			return (T)((object)this.compDic[typeFromHandle]);
		}
		return default(T);
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00005D84 File Offset: 0x00003F84
	public void RemoveComp<T>(T t)
	{
		Type typeFromHandle = typeof(T);
		if (this.compDic.ContainsKey(typeFromHandle))
		{
			Entity entity = this.compDic[typeFromHandle] as Entity;
			if (entity != null)
			{
				if (entity.compDic.Count > 0)
				{
					using (Dictionary<Type, object>.Enumerator enumerator = entity.compDic.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<Type, object> keyValuePair = enumerator.Current;
							Type key = keyValuePair.Key;
							this.RemoveComp<Type>(key);
						}
						goto IL_119;
					}
				}
				IUpdate update = this.compDic[typeFromHandle] as IUpdate;
				if (update != null)
				{
					this.updateList.Remove(update);
				}
				IFixedUpdate fixedUpdate = this.compDic[typeFromHandle] as IFixedUpdate;
				if (fixedUpdate != null)
				{
					this.fixedUpdateList.Remove(fixedUpdate);
				}
				ILateUpdate lateUpdate = this.compDic[typeFromHandle] as ILateUpdate;
				if (lateUpdate != null)
				{
					this.lateUpdateList.Remove(lateUpdate);
				}
				IApplicationQuit applicationQuit = this.compDic[typeFromHandle] as IApplicationQuit;
				if (applicationQuit != null)
				{
					this.applicationList.Remove(applicationQuit);
				}
			}
			IL_119:
			this.compDic.Remove(typeFromHandle);
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00005EC8 File Offset: 0x000040C8
	public virtual void Dispose()
	{
		foreach (KeyValuePair<Type, object> keyValuePair in this.compDic)
		{
			Entity entity = keyValuePair.Value as Entity;
			if (entity != null)
			{
				entity.Dispose();
			}
		}
	}

	// Token: 0x040000FC RID: 252
	public Dictionary<Type, object> compDic = new Dictionary<Type, object>();

	// Token: 0x040000FD RID: 253
	public List<IUpdate> updateList = new List<IUpdate>();

	// Token: 0x040000FE RID: 254
	public List<IFixedUpdate> fixedUpdateList = new List<IFixedUpdate>();

	// Token: 0x040000FF RID: 255
	public List<ILateUpdate> lateUpdateList = new List<ILateUpdate>();

	// Token: 0x04000100 RID: 256
	public List<IApplicationQuit> applicationList = new List<IApplicationQuit>();
}
