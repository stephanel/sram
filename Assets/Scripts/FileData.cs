using System.IO;
namespace Sram
{
	public class FileData
	{
		public static FileData GetData ()
		{
			return new FileData(){

			};
		}

		public void Write(string filepath){
			using (FileStream stream = new FileStream(filepath, FileMode.OpenOrCreate)) 
			{
				using(BinaryWriter writer = new BinaryWriter(stream))
				{
					// write content here
					//writer.Write(@"...");
				}

			}
		}

		public string Read(string filepath){
			using (FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read)) 
			{
				using(BinaryReader reader = new BinaryReader(stream))
				{
					// read content here
					// reader.ReadString();
					return null;
				}
				
			}
		}
	}
}

