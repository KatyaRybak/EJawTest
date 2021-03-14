#pragma warning disable 0649
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private AssetBundleManager _bundleManager;
	private GeometryObjectModel _currentGeometryModel;
	private Camera _mainCamera;

	private void Start()
	{
		_bundleManager.LoadBundle();
		_mainCamera = Camera.main;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (_currentGeometryModel == null)
			{
				if (Physics.Raycast(ray, out hit,
					Mathf.Infinity))
				{
					_currentGeometryModel = _bundleManager.InstantiatePrefab(hit.point);
				}
			}
			else
			{
				if (Physics.Raycast(ray, out hit,
					Mathf.Infinity, 1 << 8))
				{
					_currentGeometryModel.IncreaseClickCount();
				}
			}
		}
	}
}