using System.Collections.Generic;
using NFive.SDK.Core.Input;
using NFive.SDK.Client.Input;

namespace Gaston11276.SimpleUi
{
	

	public class Editbox : Textbox
	{

		

		

		public Editbox()
		{
			

		}

		

	

		public void OnKey(int state, int keycode)
		{
			if ((flags & SELECTED) != 0)
			{
				//System.Console.WriteLine($"{name}: KeyCode: {e.KeyCode}");
				//System.Console.WriteLine($"{name}: KeyData {e.KeyData}");
				//System.Console.WriteLine($"{name}: KeyValue {e.KeyValue}");



				
				if (state == 3 && keycode >= 48 && keycode <= 57)
				{
					// Number keys pressed so need to so special processing
					// also check if shift pressed
				}
				else if (state == 3 && keycode >= 65 && keycode <= 90)
				{
					
					string input = ((char)keycode).ToString();

					if (text.Length == 0)
					{
						text += input.ToUpper();
					}
					else
					{
						text += input.ToLower();
					}
					
					
					TextChanged();
				}
				
				else if (state == 3 && keycode == 8)
				{
					// Backspace
					//System.Console.WriteLine($"{name}: Backspace, removing last character from \"{text}\"");
					//System.Console.WriteLine($"{name}: Text length: {text.Length}");
					if (text.Length > 0)
					{
						//System.Console.WriteLine($"{name}: Removing {text.Substring(text.Length - 1, 1)} at index {text.Length - 1}");
						text = text.Remove(text.Length - 1, 1);
						TextChanged();
					}
				}
				
				

				//text.Trim();
				//System.Console.WriteLine($"{name}: Current text: \"{text}\"");
				//Redraw();
			}

			//text.Insert(text.Length, e.KeyCode.ToString());
		}


	}
}
