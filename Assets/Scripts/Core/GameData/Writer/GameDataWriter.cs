using System;
using System.IO;
namespace Sram.Core.GameData.Writer
{
	public interface IWritableGameData{
		byte[] ToByteArray ();
	}

	public class GameDataWriter : IDisposable
	{
		protected FileStream _fs;
		
		public GameDataWriter(string filename)
		{
			_fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
		}
		
		public void Write(IWritableGameData data)
		{
			if(_fs == null)
				throw new Exception("Cannot call write on closed GameDataWriter");
			byte[] buff = data.ToByteArray();
			_fs.Write(buff, 0, buff.Length);
		}
		
		public void Close()
		{
			if (_fs != null) {
				_fs.Close ();
			}
		}

		~GameDataWriter(){
			Dispose ();
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			if (_fs != null) {
				_fs.Dispose();
				_fs = null;
			}
		}

		#endregion
	}
}