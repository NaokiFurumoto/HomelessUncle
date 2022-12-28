using System;

namespace Carbon
{
	public interface IDataReceiverNode<T>
	{
		void OnReceive(T data, Action onComplete = null);
	}
}
