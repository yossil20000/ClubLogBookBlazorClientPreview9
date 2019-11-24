﻿using Microsoft.AspNetCore.Components;

namespace NodaTimePicker
{
	/// <summary>Base class for any picker components.</summary>
	/// <typeparam name="TState"></typeparam>
	public abstract class PickerComponentBase : ComponentBase
	{ 
		/// <summary>Any CSS classes to be applied to the wrapper element.</summary>
		[Parameter] public string Class { get; set; }
		/// <summary>Any CSS styles to be applied to the wrapper element.</summary>
		[Parameter] public string Style { get; set; }
		/// <summary>The maximum width of the wrapper element. Must be a valid CSS width value.</summary>
		[Parameter] public string MaxWidth { get; set; }
		/// <summary>The width of the wrapper element. Must be a valid CSS width value.</summary>
		[Parameter] public string Width { get; set; } = "250px";
		/// <summary>If true and <see cref="Inline"/> is true, the Clear button will be displayed. If false, or <see cref="Inline"/> is false, it will be hidden.</summary>
		[Parameter] public bool ShowClose { get; set; } = false;
		// Parameters not yet in use
		[Parameter] public bool CloseOnSelect { get; set; } = true;			
	}
}
