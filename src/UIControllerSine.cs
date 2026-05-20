using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003F9 RID: 1017
public class UIControllerSine : MonoBehaviour
{
	// Token: 0x06001764 RID: 5988 RVA: 0x00091D58 File Offset: 0x0008FF58
	private void Start()
	{
		this.lt = new List<Transform>();
		this.prefabs = this.prefabHolder.GetComponentsInChildren<Transform>(true);
		foreach (Transform transform in this.prefabs)
		{
			if (transform.parent == this.prefabHolder)
			{
				this.lt.Add(transform);
			}
		}
		this.prefabs = this.lt.ToArray();
		this.EnableActive();
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x00091DD4 File Offset: 0x0008FFD4
	public void EnableActive()
	{
		for (int i = 0; i < this.prefabs.Length; i++)
		{
			if (i == this.activeNumber)
			{
				this.prefabs[i].gameObject.SetActive(true);
				this.activeGameObject = this.prefabs[i].gameObject;
			}
			else
			{
				this.prefabs[i].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x00091E38 File Offset: 0x00090038
	public void ChangeEffect(bool bo)
	{
		this.activeGameObject.GetComponent<ForceFieldController>().SetOpenCloseValue(0f);
		if (bo)
		{
			this.activeNumber++;
			if (this.activeNumber == this.prefabs.Length)
			{
				this.activeNumber = 0;
			}
		}
		else
		{
			this.activeNumber--;
			if (this.activeNumber == -1)
			{
				this.activeNumber = this.prefabs.Length - 1;
			}
		}
		this.EnableActive();
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x00091EB1 File Offset: 0x000900B1
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.ChangeEffect(true);
		}
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.ChangeEffect(false);
		}
	}

	// Token: 0x04001615 RID: 5653
	public Transform prefabHolder;

	// Token: 0x04001616 RID: 5654
	public float openSpeed = 1f;

	// Token: 0x04001617 RID: 5655
	public bool openAnimation = true;

	// Token: 0x04001618 RID: 5656
	public AnimationCurve openCurve;

	// Token: 0x04001619 RID: 5657
	private Transform[] prefabs;

	// Token: 0x0400161A RID: 5658
	private List<Transform> lt;

	// Token: 0x0400161B RID: 5659
	private int activeNumber;

	// Token: 0x0400161C RID: 5660
	private ForceFieldController ffc;

	// Token: 0x0400161D RID: 5661
	private float openCloseValue;

	// Token: 0x0400161E RID: 5662
	private float openCloseCurve;

	// Token: 0x0400161F RID: 5663
	private GameObject activeGameObject;
}
