using System;
using Xamvvm;
using Xamarin.Forms;

namespace Examples
{
	public class DetailPageModel : BasePageModel
	{
		public void Init(string text, Color color)
		{
			Text = text;
			Color = color;
		}

		public Color Color
		{
			get { return GetField<Color>(); }
			set { SetField(value); }
		}

		public string Text
		{
			get { return GetField<string>(); }
			set { SetField(value); }
		}
	}
}
