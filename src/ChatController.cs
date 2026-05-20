using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000410 RID: 1040
public class ChatController : MonoBehaviour
{
	// Token: 0x060017B2 RID: 6066 RVA: 0x000940B8 File Offset: 0x000922B8
	private void OnEnable()
	{
		this.ChatInputField.onSubmit.AddListener(new UnityAction<string>(this.AddToChatOutput));
	}

	// Token: 0x060017B3 RID: 6067 RVA: 0x000940D6 File Offset: 0x000922D6
	private void OnDisable()
	{
		this.ChatInputField.onSubmit.RemoveListener(new UnityAction<string>(this.AddToChatOutput));
	}

	// Token: 0x060017B4 RID: 6068 RVA: 0x000940F4 File Offset: 0x000922F4
	private void AddToChatOutput(string newText)
	{
		this.ChatInputField.text = string.Empty;
		DateTime now = DateTime.Now;
		string text = string.Concat(new string[]
		{
			"[<#FFFF80>",
			now.Hour.ToString("d2"),
			":",
			now.Minute.ToString("d2"),
			":",
			now.Second.ToString("d2"),
			"</color>] ",
			newText
		});
		if (this.ChatDisplayOutput != null)
		{
			if (this.ChatDisplayOutput.text == string.Empty)
			{
				this.ChatDisplayOutput.text = text;
			}
			else
			{
				TMP_Text chatDisplayOutput = this.ChatDisplayOutput;
				chatDisplayOutput.text = chatDisplayOutput.text + "\n" + text;
			}
		}
		this.ChatInputField.ActivateInputField();
		this.ChatScrollbar.value = 0f;
	}

	// Token: 0x040016C0 RID: 5824
	public TMP_InputField ChatInputField;

	// Token: 0x040016C1 RID: 5825
	public TMP_Text ChatDisplayOutput;

	// Token: 0x040016C2 RID: 5826
	public Scrollbar ChatScrollbar;
}
