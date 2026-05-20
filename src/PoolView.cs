using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class PoolView : MonoBehaviour
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x0600019E RID: 414 RVA: 0x00009D0C File Offset: 0x00007F0C
	public GameObject Pool
	{
		get
		{
			if (this._pool == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.transform.SetParent(base.transform);
				gameObject.SetZero();
				gameObject.name = "_pool";
				this._pool = gameObject;
			}
			return this._pool;
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00009D60 File Offset: 0x00007F60
	private void Start()
	{
		if (this.target != null)
		{
			this.target.transform.SetParent(this.Pool.transform);
			this.target.SetActive(false);
			this.poolList.Add(this.target);
		}
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00009DB4 File Offset: 0x00007FB4
	public GameObject AddView()
	{
		GameObject gameObject;
		if (this.poolList.Count > 1)
		{
			gameObject = this.poolList[0];
			this.poolList.Remove(gameObject);
			gameObject.SetActive(true);
			gameObject.transform.SetParent(base.transform);
		}
		else
		{
			gameObject = Object.Instantiate<GameObject>(this.target, base.transform, false);
		}
		gameObject.SetActive(true);
		gameObject.transform.SetSiblingIndex(gameObject.transform.parent.childCount - 1);
		this.viewList.Add(gameObject);
		gameObject.SetZero();
		return gameObject;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00009E50 File Offset: 0x00008050
	public Task<GameObject> AddViewAsync()
	{
		PoolView.<AddViewAsync>d__8 <AddViewAsync>d__;
		<AddViewAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GameObject>.Create();
		<AddViewAsync>d__.<>4__this = this;
		<AddViewAsync>d__.<>1__state = -1;
		<AddViewAsync>d__.<>t__builder.Start<PoolView.<AddViewAsync>d__8>(ref <AddViewAsync>d__);
		return <AddViewAsync>d__.<>t__builder.Task;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00009E94 File Offset: 0x00008094
	public Task<GameObject> InstantiateAsync(GameObject target, Transform transform)
	{
		PoolView.<InstantiateAsync>d__9 <InstantiateAsync>d__;
		<InstantiateAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GameObject>.Create();
		<InstantiateAsync>d__.target = target;
		<InstantiateAsync>d__.transform = transform;
		<InstantiateAsync>d__.<>1__state = -1;
		<InstantiateAsync>d__.<>t__builder.Start<PoolView.<InstantiateAsync>d__9>(ref <InstantiateAsync>d__);
		return <InstantiateAsync>d__.<>t__builder.Task;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00009EDF File Offset: 0x000080DF
	public void RemoveView(GameObject go)
	{
		go.SetActive(false);
		this.poolList.Add(go);
		this.viewList.Remove(go);
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00009F04 File Offset: 0x00008104
	public void RemoveAllView()
	{
		for (int i = this.viewList.Count; i > 0; i--)
		{
			PoolView[] componentsInChildren = this.viewList[0].GetComponentsInChildren<PoolView>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].RemoveAllView();
			}
			this.RemoveView(this.viewList[0]);
		}
	}

	// Token: 0x040001ED RID: 493
	public GameObject target;

	// Token: 0x040001EE RID: 494
	[HideInInspector]
	public List<GameObject> viewList = new List<GameObject>();

	// Token: 0x040001EF RID: 495
	private List<GameObject> poolList = new List<GameObject>();

	// Token: 0x040001F0 RID: 496
	private GameObject _pool;
}
