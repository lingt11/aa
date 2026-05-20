using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000259 RID: 601
public class UIPointerEventPenetrate : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x06000AC3 RID: 2755 RVA: 0x00037393 File Offset: 0x00035593
	public void OnPointerClick(PointerEventData eventData)
	{
		this.Psss<IPointerClickHandler>(eventData, ExecuteEvents.pointerClickHandler);
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x000373A4 File Offset: 0x000355A4
	public void Psss<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function) where T : IEventSystemHandler
	{
		this.results.Clear();
		EventSystem.current.RaycastAll(data, this.results);
		GameObject gameObject = data.pointerCurrentRaycast.gameObject;
		for (int i = 0; i < this.results.Count; i++)
		{
			string name = this.results[i].gameObject.name;
			if (name.Equals("btnsell"))
			{
				this.sellEvent();
				EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/出售物品", 1f, 3f);
			}
			else if (name.Equals("btndiscord"))
			{
				this.discordEvent();
				EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/丢弃物品", 1f, 3f);
			}
			else if (name.Equals("btnuse"))
			{
				this.useEvent();
			}
		}
	}

	// Token: 0x04000BF3 RID: 3059
	private List<RaycastResult> results = new List<RaycastResult>();

	// Token: 0x04000BF4 RID: 3060
	public Action sellEvent;

	// Token: 0x04000BF5 RID: 3061
	public Action useEvent;

	// Token: 0x04000BF6 RID: 3062
	public Action discordEvent;
}
