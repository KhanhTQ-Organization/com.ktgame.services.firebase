using Cysharp.Threading.Tasks;
using com.ktgame.core;

#if FIREBASE
using Firebase;
#endif

namespace com.ktgame.services.firebase
{
	[Service(typeof(IFirebaseService))]
	public class FirebaseService : IFirebaseService
	{
		public int Priority => 0;
		public bool Initialized { get; set; }

		private bool _connecting;

		public async UniTask OnInitialize(IArchitecture architecture)
		{
			await UniTask.WaitUntil(() => _connecting == false);

			if (Initialized)
			{
				return;
			}

			_connecting = true;
#if FIREBASE
			var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask();
			Initialized = dependencyStatus == DependencyStatus.Available;
#endif
			_connecting = false;
		}
	}
}
