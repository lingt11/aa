using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TMPro
{
	// Token: 0x0200041E RID: 1054
	public class TMP_TextEventHandler : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00094EA4 File Offset: 0x000930A4
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x00094EAC File Offset: 0x000930AC
		public TMP_TextEventHandler.CharacterSelectionEvent onCharacterSelection
		{
			get
			{
				return this.m_OnCharacterSelection;
			}
			set
			{
				this.m_OnCharacterSelection = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00094EB5 File Offset: 0x000930B5
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x00094EBD File Offset: 0x000930BD
		public TMP_TextEventHandler.SpriteSelectionEvent onSpriteSelection
		{
			get
			{
				return this.m_OnSpriteSelection;
			}
			set
			{
				this.m_OnSpriteSelection = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00094EC6 File Offset: 0x000930C6
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x00094ECE File Offset: 0x000930CE
		public TMP_TextEventHandler.WordSelectionEvent onWordSelection
		{
			get
			{
				return this.m_OnWordSelection;
			}
			set
			{
				this.m_OnWordSelection = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x00094ED7 File Offset: 0x000930D7
		// (set) Token: 0x060017E9 RID: 6121 RVA: 0x00094EDF File Offset: 0x000930DF
		public TMP_TextEventHandler.LineSelectionEvent onLineSelection
		{
			get
			{
				return this.m_OnLineSelection;
			}
			set
			{
				this.m_OnLineSelection = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x00094EE8 File Offset: 0x000930E8
		// (set) Token: 0x060017EB RID: 6123 RVA: 0x00094EF0 File Offset: 0x000930F0
		public TMP_TextEventHandler.LinkSelectionEvent onLinkSelection
		{
			get
			{
				return this.m_OnLinkSelection;
			}
			set
			{
				this.m_OnLinkSelection = value;
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00094EFC File Offset: 0x000930FC
		private void Awake()
		{
			this.m_TextComponent = base.gameObject.GetComponent<TMP_Text>();
			if (this.m_TextComponent.GetType() == typeof(TextMeshProUGUI))
			{
				this.m_Canvas = base.gameObject.GetComponentInParent<Canvas>();
				if (this.m_Canvas != null)
				{
					if (this.m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
					{
						this.m_Camera = null;
						return;
					}
					this.m_Camera = this.m_Canvas.worldCamera;
					return;
				}
			}
			else
			{
				this.m_Camera = Camera.main;
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00094F88 File Offset: 0x00093188
		private void LateUpdate()
		{
			if (TMP_TextUtilities.IsIntersectingRectTransform(this.m_TextComponent.rectTransform, Input.mousePosition, this.m_Camera))
			{
				int num = TMP_TextUtilities.FindIntersectingCharacter(this.m_TextComponent, Input.mousePosition, this.m_Camera, true);
				if (num != -1 && num != this.m_lastCharIndex)
				{
					this.m_lastCharIndex = num;
					TMP_TextElementType elementType = this.m_TextComponent.textInfo.characterInfo[num].elementType;
					if (elementType == TMP_TextElementType.Character)
					{
						this.SendOnCharacterSelection(this.m_TextComponent.textInfo.characterInfo[num].character, num);
					}
					else if (elementType == TMP_TextElementType.Sprite)
					{
						this.SendOnSpriteSelection(this.m_TextComponent.textInfo.characterInfo[num].character, num);
					}
				}
				int num2 = TMP_TextUtilities.FindIntersectingWord(this.m_TextComponent, Input.mousePosition, this.m_Camera);
				if (num2 != -1 && num2 != this.m_lastWordIndex)
				{
					this.m_lastWordIndex = num2;
					TMP_WordInfo tmp_WordInfo = this.m_TextComponent.textInfo.wordInfo[num2];
					this.SendOnWordSelection(tmp_WordInfo.GetWord(), tmp_WordInfo.firstCharacterIndex, tmp_WordInfo.characterCount);
				}
				int num3 = TMP_TextUtilities.FindIntersectingLine(this.m_TextComponent, Input.mousePosition, this.m_Camera);
				if (num3 != -1 && num3 != this.m_lastLineIndex)
				{
					this.m_lastLineIndex = num3;
					TMP_LineInfo tmp_LineInfo = this.m_TextComponent.textInfo.lineInfo[num3];
					char[] array = new char[tmp_LineInfo.characterCount];
					int num4 = 0;
					while (num4 < tmp_LineInfo.characterCount && num4 < this.m_TextComponent.textInfo.characterInfo.Length)
					{
						array[num4] = this.m_TextComponent.textInfo.characterInfo[num4 + tmp_LineInfo.firstCharacterIndex].character;
						num4++;
					}
					string line = new string(array);
					this.SendOnLineSelection(line, tmp_LineInfo.firstCharacterIndex, tmp_LineInfo.characterCount);
				}
				int num5 = TMP_TextUtilities.FindIntersectingLink(this.m_TextComponent, Input.mousePosition, this.m_Camera);
				if (num5 != -1 && num5 != this.m_selectedLink)
				{
					this.m_selectedLink = num5;
					TMP_LinkInfo tmp_LinkInfo = this.m_TextComponent.textInfo.linkInfo[num5];
					this.SendOnLinkSelection(tmp_LinkInfo.GetLinkID(), tmp_LinkInfo.GetLinkText(), num5);
					return;
				}
			}
			else
			{
				this.m_selectedLink = -1;
				this.m_lastCharIndex = -1;
				this.m_lastWordIndex = -1;
				this.m_lastLineIndex = -1;
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00002D1D File Offset: 0x00000F1D
		public void OnPointerEnter(PointerEventData eventData)
		{
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00002D1D File Offset: 0x00000F1D
		public void OnPointerExit(PointerEventData eventData)
		{
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000951EB File Offset: 0x000933EB
		private void SendOnCharacterSelection(char character, int characterIndex)
		{
			if (this.onCharacterSelection != null)
			{
				this.onCharacterSelection.Invoke(character, characterIndex);
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00095202 File Offset: 0x00093402
		private void SendOnSpriteSelection(char character, int characterIndex)
		{
			if (this.onSpriteSelection != null)
			{
				this.onSpriteSelection.Invoke(character, characterIndex);
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00095219 File Offset: 0x00093419
		private void SendOnWordSelection(string word, int charIndex, int length)
		{
			if (this.onWordSelection != null)
			{
				this.onWordSelection.Invoke(word, charIndex, length);
			}
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00095231 File Offset: 0x00093431
		private void SendOnLineSelection(string line, int charIndex, int length)
		{
			if (this.onLineSelection != null)
			{
				this.onLineSelection.Invoke(line, charIndex, length);
			}
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00095249 File Offset: 0x00093449
		private void SendOnLinkSelection(string linkID, string linkText, int linkIndex)
		{
			if (this.onLinkSelection != null)
			{
				this.onLinkSelection.Invoke(linkID, linkText, linkIndex);
			}
		}

		// Token: 0x0400170A RID: 5898
		[SerializeField]
		private TMP_TextEventHandler.CharacterSelectionEvent m_OnCharacterSelection = new TMP_TextEventHandler.CharacterSelectionEvent();

		// Token: 0x0400170B RID: 5899
		[SerializeField]
		private TMP_TextEventHandler.SpriteSelectionEvent m_OnSpriteSelection = new TMP_TextEventHandler.SpriteSelectionEvent();

		// Token: 0x0400170C RID: 5900
		[SerializeField]
		private TMP_TextEventHandler.WordSelectionEvent m_OnWordSelection = new TMP_TextEventHandler.WordSelectionEvent();

		// Token: 0x0400170D RID: 5901
		[SerializeField]
		private TMP_TextEventHandler.LineSelectionEvent m_OnLineSelection = new TMP_TextEventHandler.LineSelectionEvent();

		// Token: 0x0400170E RID: 5902
		[SerializeField]
		private TMP_TextEventHandler.LinkSelectionEvent m_OnLinkSelection = new TMP_TextEventHandler.LinkSelectionEvent();

		// Token: 0x0400170F RID: 5903
		private TMP_Text m_TextComponent;

		// Token: 0x04001710 RID: 5904
		private Camera m_Camera;

		// Token: 0x04001711 RID: 5905
		private Canvas m_Canvas;

		// Token: 0x04001712 RID: 5906
		private int m_selectedLink = -1;

		// Token: 0x04001713 RID: 5907
		private int m_lastCharIndex = -1;

		// Token: 0x04001714 RID: 5908
		private int m_lastWordIndex = -1;

		// Token: 0x04001715 RID: 5909
		private int m_lastLineIndex = -1;

		// Token: 0x0200041F RID: 1055
		[Serializable]
		public class CharacterSelectionEvent : UnityEvent<char, int>
		{
		}

		// Token: 0x02000420 RID: 1056
		[Serializable]
		public class SpriteSelectionEvent : UnityEvent<char, int>
		{
		}

		// Token: 0x02000421 RID: 1057
		[Serializable]
		public class WordSelectionEvent : UnityEvent<string, int, int>
		{
		}

		// Token: 0x02000422 RID: 1058
		[Serializable]
		public class LineSelectionEvent : UnityEvent<string, int, int>
		{
		}

		// Token: 0x02000423 RID: 1059
		[Serializable]
		public class LinkSelectionEvent : UnityEvent<string, string, int>
		{
		}
	}
}
