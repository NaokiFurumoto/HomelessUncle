using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Carbon
{
	/// <summary>
	/// Serialization Utils
	/// </summary>
	public static class SerializationUtils
	{
		/// <summary>
		/// Deep-copy a serializable class
		/// </summary>
		public static T DeepCopy<T>(T src)
		{
			using (var memoryStream = new MemoryStream()) {
				T result;

				try {
					var binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, src);
					memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
					result = (T)binaryFormatter.Deserialize(memoryStream);
				}
				catch {
					DebugUtils.Warning("Class to copy includes non-serializable class.");
					result = default;
				}

				return result;
			}
		}

		/// <summary>
		/// Compare serializable classes
		/// </summary>
		public static bool Equals<T>(T x, T y)
		{
			using (var memoryStream = new MemoryStream()) {
				var binaryFormatter = new BinaryFormatter();

				byte[] bX;
				byte[] bY;

				try {
					binaryFormatter.Serialize(memoryStream, x);
					bX = memoryStream.ToArray();

					memoryStream.SetLength(0);

					binaryFormatter.Serialize(memoryStream, y);
					bY = memoryStream.ToArray();
				}
				catch {
					DebugUtils.Warning("Class to compare includes non-serializable class.");
					bX = null;
					bY = null;
				}

				if (bX == null || bY == null) {
					return false;
				}

				if (bX.Length != bY.Length) {
					return false;
				}

				int bXLength = bX.Length;
				for (int i = 0; i < bXLength; i++) {
					if (bX[i] != bY[i]) {
						return false;
					}
				}

				return true;
			}
		}
	}
}
