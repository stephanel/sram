using System;
using System.IO;
namespace Sram.Core.GameData.Reader
{
	public abstract class GameDataReader
	{
		protected BinaryReader _br; 
		protected string _sFileName;
		protected long _lLength = -1;
		protected bool _bOpen = false;
		protected long _lPosition = 0;

		public GameDataReader(string fileName)
		{
			_sFileName = fileName;
		}
		
		
		public void Close()
		{
			_bOpen = false;
			_br.Close();
			_br = null;
		}
		

		public bool EOF
		{
			get
			{	
				return (!_bOpen || (_lPosition >= _lLength));
			}
		}
		
		public void Open()
		{
			FileStream fs = new FileStream(_sFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			_br = new BinaryReader(fs);
			_lLength = fs.Length;
			_lPosition = 0;
			_bOpen = true;
		}
		
		public abstract bool Read();

	}
}

