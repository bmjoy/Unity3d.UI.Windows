using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI.Windows.Components.Events;
using UnityEngine.UI.Windows.Plugins.Localization.UI;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Windows.Components {

	public class InputFieldComponent : WindowComponentNavigation, IInteractableComponent, IEventSystemHandler {

		public bool convertToUppercase = false;

		[SerializeField]
		protected Text text;
		
		[SerializeField]
		protected Extensions.InputField inputField;
		
		[SerializeField]
		protected Text placeholder;
		
		[SerializeField]
		protected bool selectByDefault;

		private ComponentEvent<string> onChange = new ComponentEvent<string>();
		private ComponentEvent<string> onEditEnd = new ComponentEvent<string>();
		private ComponentEvent<bool> onFocus = new ComponentEvent<bool>();
		
		private bool lastFocusValue = false;

		public override bool IsNavigationPreventEvents(NavigationSide side) {

			var inputField = this.inputField;
			if (inputField != null) {

				return inputField.HasKeyboard();

			}

			return base.IsNavigationPreventChildEvents(side);

		}

		#region macros UI.Windows.ButtonComponent.States 
	/*
	 * This code is auto-generated by Macros Module
	 * Do not change anything
	 */
	[SerializeField]
			private bool hoverOnAnyButtonState = false;
	
			[SerializeField]
			private bool hoverCursorDefaultOnInactive = false;
	
			public bool IsHoverCursorDefaultOnInactive() {
	
				return this.IsInteractable() == false && this.hoverCursorDefaultOnInactive == true;
	
			}
	
			public void Select() {
	
				this.GetSelectable().Select();
	
			}
	
			public virtual IInteractableComponent SetEnabledState(bool state) {
	
				if (state == true) {
	
					this.SetEnabled();
	
				} else {
	
					this.SetDisabled();
	
				}
	
				return this;
	
			}
	
			public virtual IInteractableComponent SetDisabled() {
	
				var sel = this.GetSelectable();
				if (sel != null) {
	
					if (sel.interactable != false) {
	
						sel.interactable = false;
						this.OnInteractableChanged();
	
					}
	
				}
	
				return this;
	
			}
	
			public virtual IInteractableComponent SetEnabled() {
	
				var sel = this.GetSelectable();
				if (sel != null) {
	
					if (sel.interactable != true) {
	
						sel.interactable = true;
						this.OnInteractableChanged();
	
					}
	
				}
	
				return this;
	
			}
	
			public IInteractableComponent SetHoverOnAnyButtonState(bool state) {
	
				this.hoverOnAnyButtonState = state;
	
				return this;
	
			}
	
			protected override bool ValidateHoverPointer() {
	
				if (this.hoverOnAnyButtonState == false && this.IsInteractable() == false) return false;
				return base.ValidateHoverPointer();
	
			}
	
			public bool IsInteractable() {
	
				var sel = this.GetSelectable();
				return (sel != null ? sel.IsInteractable() : false);
	
			}
	
			public virtual void OnInteractableChanged() {
	
			}
	#endregion

		public bool IsInteractableAndHasEvents() {

			return this.IsInteractable() == true /*&&
				(
					this.callback.GetPersistentEventCount() > 0 ||
					this.callbackButton.GetPersistentEventCount() > 0 ||
					this.button.onClick.GetPersistentEventCount() > 0
				)*/;

		}

		public virtual Selectable GetSelectable() {
			
			return this.inputField;
			
		}

		public virtual void SetSelectByDefault(bool state) {
			
			this.selectByDefault = state;

		}

		public override void OnLocalizationChanged() {

			base.OnLocalizationChanged();

			if (this.placeholder != null && this.placeholder is LocalizationText) {

				(this.placeholder as LocalizationText).OnLocalizationChanged();

			}

		}

		public string GetText() {

			return (this.inputField != null) ? this.inputField.text : string.Empty;

		}

		public void SetText(string text) {

			if (text == null) text = string.Empty;

			if (this.inputField != null) {
				
				this.inputField.enabled = false;
				this.inputField.text = string.Empty;
				this.inputField.text = text;
				this.inputField.enabled = true;
				
			}
			
		}

		public void SetPlaceholderText(string text) {
			
			if (this.placeholder != null) {

				this.placeholder.text = text;
				
			}
			
		}
		
		public void MoveCaretToStart(bool select = false) {

			this.inputField.selectionAnchorPosition = 0;
			this.inputField.selectionFocusPosition = 0;

			this.inputField.MoveTextStart(select);
			
		}

		public void MoveCaretToEnd(bool select = false) {

			var len = this.GetText().Length;
			this.inputField.selectionAnchorPosition = len;
			this.inputField.selectionFocusPosition = len;

			this.inputField.MoveTextEndFix(select);

		}

		public void SetCaretPosition(int position) {

			this.inputField.caretPosition = position;

		}

		public bool HasFocus() {
			
			if (this.inputField != null) {
				
				return this.inputField.isFocused;

			}

			return false;
			
		}

		public void SetFocus() {

			if (this.inputField != null) {

				this.inputField.Select();
				this.inputField.ActivateInputField();

			}

		}

		public void SetContentType(InputField.ContentType contentType) {

			if (this.inputField != null) this.inputField.contentType = contentType;

		}
		
		public void SetLineType(InputField.LineType lineType) {
			
			if (this.inputField != null) this.inputField.lineType = lineType;
			
		}
		
		public void SetCharacterLimit(int length = 0) {
			
			if (this.inputField != null) this.inputField.characterLimit = length;
			
		}
		
		public void SetOnChangeCallback(System.Action<string> onChange) {
			
			if (onChange != null) this.onChange.AddListenerDistinct(onChange);
			
		}
		
		public void SetOnEditEndCallback(System.Action<string> onEditEnd) {
			
			if (onEditEnd != null) this.onEditEnd.AddListenerDistinct(onEditEnd);
			
		}
		
		public void SetOnFocusChangedCallback(System.Action<bool> onFocus) {
			
			if (onFocus != null) this.onFocus.AddListenerDistinct(onFocus);
			
		}

		public void SetCallbacks(System.Action<string> onChange, System.Action<string> onEditEnd) {
			
			this.SetOnChangeCallback(onChange);
			this.SetOnEditEndCallback(onEditEnd);

		}
		
		private void OnChange(string input) {
			
			this.onChange.Invoke(input);
			
		}
		
		private void OnEditEnd(string input) {

			this.onEditEnd.Invoke(input);
			
		}

		public override void OnInit() {

			base.OnInit();

			this.inputField.onValidateInput = this.OnValidateChar;
			#if UNITY_5_2
			this.inputField.onValueChange.AddListener(this.OnChange);
			#else
			this.inputField.onValueChanged.AddListener(this.OnChange);
			#endif
			this.inputField.onEndEdit.AddListener(this.OnEditEnd);

			this.lastFocusValue = this.HasFocus();

		}

		public override void OnDeinit(System.Action callback) {

			base.OnDeinit(callback);

			this.inputField.onValidateInput = null;
			#if UNITY_5_2
			this.inputField.onValueChange.RemoveListener(this.OnChange);
			#else
			this.inputField.onValueChanged.RemoveListener(this.OnChange);
			#endif
			this.inputField.onEndEdit.RemoveListener(this.OnEditEnd);

			this.onChange.RemoveAllListeners();
			this.onEditEnd.RemoveAllListeners();
			this.onFocus.RemoveAllListeners();

		}

		public override void OnShowBegin() {

			base.OnShowBegin();
			
			if (this.selectByDefault == true) {
				
				this.Select();
				
			}

		}

		public char OnValidateChar(string text, int index, char addedChar) {

			if (this.convertToUppercase == true) {

				return char.ToUpper(addedChar);

			}

			return addedChar;

		}

		public virtual void LateUpdate() {

			if (this.lastFocusValue != this.HasFocus()) {

				this.onFocus.Invoke(this.HasFocus());
				this.lastFocusValue = this.HasFocus();

			}

		}

		#if UNITY_EDITOR
		public override void OnValidateEditor() {

			base.OnValidateEditor();

			if (this.gameObject.activeSelf == false) return;

			var texts = this.GetComponentsInChildren<Text>(true);
			if (texts.Length == 1) this.text = texts[0];

			if (this.text != null) this.text.supportRichText = false;

			ME.Utilities.FindReference<Extensions.InputField>(this, ref this.inputField);

			if (this.inputField != null && this.text != null) {

				this.inputField.textComponent = this.text;
				this.inputField.targetGraphic = this.text;

			}

		}
		#endif

	}

}