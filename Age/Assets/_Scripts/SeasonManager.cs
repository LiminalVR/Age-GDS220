using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeasonManager : MonoBehaviour {

    public enum SeasonType { SUMMER, WINTER, AUTUMN, SPRING }
    public static SeasonType _currentSeasonType;

    [Header("Seasons")]
    [SerializeField] private Season[] _seasons;
    private Season _currentSeason;
    private int _currentSeasonNum = 0;

    [Header("Transition")]
    [SerializeField] private float _fadeOutDuration;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private Image _fadeMask;

    [Header("Aesthetics")]
    [SerializeField] private int _treeMatSelectIndex;
    private Material[] _treeMaterials;
    private int _albedoID;

    private ElementManager _elementManager;

    private void Start()
    {
        _elementManager = GetComponent<ElementManager>();

        SetupTrees();

        SetupSeasons();
    }

    private void SetupTrees()
    {
        _albedoID = Shader.PropertyToID("_MainTex");

        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        List<Material> treeMaterials = new List<Material>();

        foreach(GameObject tree in trees)
        {
            Material[] materials = tree.GetComponent<Renderer>().materials;
            treeMaterials.Add(materials[_treeMatSelectIndex]);
        }

        _treeMaterials = treeMaterials.ToArray();
    }

    private void SetupSeasons()
    {
        _currentSeason = _seasons[_currentSeasonNum];
        _currentSeason.StartSeason();
        _elementManager.ResetElementOrder(_currentSeason._elementSpawnOrder);
        ChangeTrees();
        StartCoroutine(ManipulateFadeMask(_fadeInDuration, 0));
    }

    public IEnumerator ManipulateFadeMask(float duration, float targetAlpha)
    {
        var step = 0.0f;
        Color startColour = _fadeMask.color;

        while(step < 1)
        {
            step += Time.deltaTime / duration;
            _fadeMask.color = Color.Lerp(startColour, new Color(startColour.r, startColour.g, startColour.b, targetAlpha), step);
            yield return null;
        }

        yield return null;
    }

    public IEnumerator ChangeSeason()
    {
        StartCoroutine(ManipulateFadeMask(_fadeOutDuration, 1));

        yield return new WaitForSeconds(_fadeOutDuration);

        _currentSeason.EndSeason();

        _currentSeasonNum++;

        SetupSeasons();

        yield return null;
    }

    private void ChangeTrees()
    {
        foreach(Material mat in _treeMaterials)
        {
            mat.SetTexture(_albedoID, _currentSeason._seasonTreeTex);
        }
    }
}
