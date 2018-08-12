using UnityEngine;

[System.Serializable]
public class Season {

    [Header("Season")]
    [SerializeField] private SeasonManager.SeasonType _season;

    [Header("Dependable Objects")]
    [SerializeField] private GameObject _seasonTerrain;
    [SerializeField] private GameObject[] _otherActiveObjects;
    [SerializeField] private GameObject[] _otherInactiveObjects;

    [Header("Transition")]
    public Color _transitionColour;


    // Variables for atmosphere thickness in the skybox settings
    [Header("Atmosphere")]
    [SerializeField] private float _atmosphereThickness;
    [SerializeField] private float _exposure;
    //[SerializeField] private float _summerSun = 1f;
    //[SerializeField] private float _autumnSun = 0.5f;
    //[SerializeField] private float _winterSun = 2f;
    //[SerializeField] private float _springSun = 0.5f;

    //skybox material
    private Material _skyMat;

    [Header("Aesthetics")]
    public Texture _seasonTreeTex;

    [Header("Elements")]
    public ElementManager.ElementType[] _elementSpawnOrder;

    public void StartSeason()
    {
		_skyMat = RenderSettings.skybox;

        // Activates appropriate terrain.
        //_seasonTerrain.SetActive(true);

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

        // 
        SetAtmosphere(_atmosphereThickness, _exposure);

        // Produces effects relative to specified season.
        switch(_season)
        {
		    case SeasonManager.SeasonType.SUMMER:
                break;

            case SeasonManager.SeasonType.AUTUMN:
			    break;

		    case SeasonManager.SeasonType.WINTER:
			    break;

            case SeasonManager.SeasonType.SPRING:
                break;
        }
    }

    private void SetAtmosphere(float thickness, float exposure)
    {
        _skyMat.SetFloat("_AtmosphereThickness", thickness);
        _skyMat.SetFloat("_Exposure", exposure);
    }

    public void EndSeason()
    {
        //_seasonTerrain.SetActive(false);
    }
}
