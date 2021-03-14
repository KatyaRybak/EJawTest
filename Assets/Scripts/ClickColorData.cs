using System;
using UnityEngine;

[Serializable]
public class ClickColorData 
{
	[SerializeField]
	private string _objectType = "";
	[SerializeField]
	private int _minClickCount = 0;
	[SerializeField]
	private int _maxClickCount = 0;
	[SerializeField]
	private Color _color = Color.white;

	public Color Color => _color;
	
	public string ObjectType => _objectType;

	public bool InRange(int clickCount)
	{
		return (clickCount >= _minClickCount && clickCount <= _maxClickCount);
	}
}
