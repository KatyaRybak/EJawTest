using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeometryObjectModel : MonoBehaviour
{
	[SerializeField]
	private string _objectType = "";
	private int _clickCount;
	private Color _color;

	private CompositeDisposable _disposables;
	private GameData _gameData;
	private GeometryObjectsData _geometryObjectsData;
	private ClickColorData _clickColorData;
	
	private void Awake()
	{
		_gameData = Resources.Load<GameData>("GameData");
		_geometryObjectsData = Resources.Load<GeometryObjectsData>("GeometryObjectsData");
		_clickColorData = _geometryObjectsData.GetClickData(_objectType);
	}

	void Start()
	{
		Observable.Timer(TimeSpan.FromSeconds(_gameData.ObservableTime))
			.Repeat()
			.Subscribe(_ => { ChangeColor(); }).AddTo(_disposables);
	}

	private void ChangeColor()
	{
		_color = Random.ColorHSV();
		GetComponent<Renderer>().material.color = _color;
	}

	private void OnEnable()
	{
		_disposables = new CompositeDisposable();
	}

	public void IncreaseClickCount()
	{
		_clickCount++;
		if (_clickColorData.InRange(_clickCount))
		{
			GetComponent<Renderer>().material.color = _clickColorData.Color;
		}
	}

	private void OnDisable()
	{
		_disposables?.Dispose();
	}
}