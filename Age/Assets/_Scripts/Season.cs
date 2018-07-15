using UnityEngine;

[System.Serializable]
public class Season {

    [Header("Season")]
    [SerializeField] private SeasonManager.SeasonType _season;

    [Header("Dependable Objects")]
    [SerializeField] private GameObject _seasonTerrain;
    [SerializeField] private GameObject[] _otherActiveObjects;
    [SerializeField] private GameObject[] _otherInactiveObjects;

    [Header("Elements")]
    [SerializeField] public ElementManager.ElementType[] _elementSpawnOrder;

    public void StartSeason()
    {
        // Activates appropriate terrain.
        _seasonTerrain.SetActive(true);

        // Activates other necessary GameObjects. 
        foreach(GameObject go in _otherActiveObjects)
        {
            go.SetActive(true);
        }

        // Deactives other unnecessary GameObjects.
        foreach(GameObject go in _otherInactiveObjects)
        {
            go.SetActive(false);
        }

        // Produces effects relative to specified season.
        switch(_season)
        {
            case SeasonManager.SeasonType.SUMMER:
                break;

            case SeasonManager.SeasonType.WINTER:
                break;

            case SeasonManager.SeasonType.AUTUMN:
                break;

            case SeasonManager.SeasonType.SPRING:
                break;
        }
    }

    public void EndSeason()
    {
        _seasonTerrain.SetActive(false);
    }
}
