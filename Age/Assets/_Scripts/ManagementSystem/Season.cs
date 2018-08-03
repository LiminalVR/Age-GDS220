using UnityEngine;

[System.Serializable]
public class Season {

    [Header("Season")]
    [SerializeField] private SeasonManager.SeasonType _season;

    [Header("Dependable Objects")]
    [SerializeField] private GameObject _seasonTerrain;
    [SerializeField] private GameObject[] _otherActiveObjects;
    [SerializeField] private GameObject[] _otherInactiveObjects;


    //variables for atmosphere thickness in the skybox settings
	float summerSun=1f;
	float autumnSun=0.5f;
	float winterSun=2f;
	float springSun=0.5f;

	//skybox material
	Material skyMat;

    [Header("Aesthetics")]
    public Texture _seasonTreeTex;

    [Header("Elements")]
    public ElementManager.ElementType[] _elementSpawnOrder;

    public void StartSeason()
    {
		skyMat = RenderSettings.skybox;
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
			if(_season == SeasonManager.SeasonType.SUMMER){
				skyMat.SetFloat ("_AtmosphereThickness", summerSun); //Adjusts skybox atmosphere thickness 
			}
			break;

       case SeasonManager.SeasonType.AUTUMN:
			if(_season == SeasonManager.SeasonType.AUTUMN){
				skyMat.SetFloat ("_AtmosphereThickness", autumnSun); //Adjusts skybox atmosphere thickness 	
			}
			break;

		case SeasonManager.SeasonType.WINTER:
			if(_season == SeasonManager.SeasonType.WINTER){
				skyMat.SetFloat ("_AtmosphereThickness", winterSun); //Adjusts skybox atmosphere thickness 	
			}
			break;

       case SeasonManager.SeasonType.SPRING:
			if(_season == SeasonManager.SeasonType.SPRING){
				skyMat.SetFloat ("_AtmosphereThickness", springSun); //Adjusts skybox atmosphere thickness 	
			}
			break;
        }
    }

    public void EndSeason()
    {
        _seasonTerrain.SetActive(false);
    }
}
