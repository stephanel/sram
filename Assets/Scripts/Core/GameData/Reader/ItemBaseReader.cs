using System;
namespace Sram.Core.GameData.Reader
{
	public class ItemBaseReader : GameDataReader
	{
		protected ItemBase _data;

		public ItemBase Data
		{
			get
			{
				return _data;
			}
		}

		public override bool Read()
		{
			if(!EOF)
			{
				_data = ItemBase.ReadBlock(_br);
				_lPosition += ItemBase.ItemBaseSize.Size;
				return true;
			}
			else
			{
				return false;
			}
		}

		public ItemBaseReader (string fileName) : base(fileName)
		{
		}
	}
}

