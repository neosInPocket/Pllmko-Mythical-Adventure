using UnityEngine;

public class PlayerDataReader : DataReader<GameDataObject>
{
    protected override string fileName => "/PlayerGameData.json";
}
