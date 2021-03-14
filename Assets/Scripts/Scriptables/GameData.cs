using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "CreateGameData")]
public class GameData : ScriptableObject
{
    [SerializeField]
    private int _observableTime = 2;
    
    private GeometryObjectModel _objectModel;

    public int ObservableTime => _observableTime;
}
