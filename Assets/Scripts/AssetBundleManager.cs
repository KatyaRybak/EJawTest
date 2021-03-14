#pragma warning disable 0649
using System.Collections;
using System.IO;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class AssetBundleManager : MonoBehaviour
{
	
	[SerializeField]
	private JsonLoader _loader;

	private int _randomBundle;
	private AssetBundle _bundle;

	public void LoadBundle()
	{
		_loader.ReadFile();
		_randomBundle = Random.Range(0, _loader.Data.Length);
		StartCoroutine(LoadBundleRoutine());
	}

	private IEnumerator LoadBundleRoutine()
	{
		while (!Caching.ready)
		{
			yield return null;
		}


		string url = "file://" + Path.Combine(Application.streamingAssetsPath, _loader.Data[_randomBundle]._bundleName);

		using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(url))
		{
			UnityWebRequestAsyncOperation async = uwr.SendWebRequest();

			while (!async.isDone && !uwr.isNetworkError && !uwr.isHttpError)
			{
				yield return null;
			}

			if (uwr.isNetworkError || uwr.isHttpError)
			{
				Debug.LogError(uwr.error);
				yield break;
			}

			_bundle = DownloadHandlerAssetBundle.GetContent(uwr);
		}
	}

	public GeometryObjectModel InstantiatePrefab(Vector3 position)
	{
		if (_bundle == null)
		{
			Debug.LogError("There is no bundle");
			return null;
		}

		if (_bundle.Contains(_loader.Data[_randomBundle]._prefabName))
		{
			GameObject prefab = _bundle.LoadAsset<GameObject>(_loader.Data[_randomBundle]._prefabName);
			if (prefab == null)
			{
				Debug.LogError("There is no Prefab");
				return null;
			}

			return Instantiate(prefab, position, Quaternion.identity).GetComponent<GeometryObjectModel>();
		}

		return null;
	}
}