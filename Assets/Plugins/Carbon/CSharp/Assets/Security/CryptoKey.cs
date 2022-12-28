namespace Carbon.Security
{
	public struct CryptoKey<T>
	{
		//public const int	PRIME_32 = 16777619;
		//public const long	PRIME_64 = 1099511628211;

		T key;

		public CryptoKey(T initKey)
		{
			key = initKey;
		}
		public static implicit operator T(CryptoKey<T> v)
		{
			return v.key;
		}
		public static implicit operator CryptoKey<T>(T v)
		{
			return new CryptoKey<T>(v);
		}
	}
}
