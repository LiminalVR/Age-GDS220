using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeasonManager : MonoBehaviour {

    // Globably accessable enum to distinguish season.
    public enum SeasonType { SUMMER, WINTER, AUTUMN, SPRING }
    public static SeasonType _currentSeasonType;

    [Header("Seasons")]
    [SerializeField] private Season[] _seasons;
    private Season _currentSeason;
    [SerializeField] private int _currentSeasonNum = 0;

    [Header("Transition")]
    [SerializeField] private float _fadeOutDuration;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private Image _fadeMask;

    // Formating.
    [Space()]
    [Space()]
    [Header("---Aesthetics---")]

    [Header("Trees")]
    [SerializeField] private int _treeMatSelectIndex;
    private Material[] _treeMaterials;
    private int _albedoID;

    /*
    [Header("Sun")]
    [SerializeField] private GameObject _sun;
    [SerializeField] private Vector3 _sunTargetRotation;
    [SerializeField] private float _sunRotationDuration;
    */

    private ColourMaster _colourMaster;
    private ElementManager _elementManager;

    private void Start()
    {
        _elementManager = GetComponent<ElementManager>();
        _colourMaster = new ColourMaster();

        SetupTrees();

        SetupSeasons();
    }

    // Finds all labelled trees in scene and saves a reference to the specified material.
    private void SetupTrees()
    {
        // Saves the albedoID specific to the current game for optimisation purposes.
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
        // Selects correct current season and implements its specific season actions.

        _currentSeason = _seasons[_currentSeasonNum];
        
        switch (_currentSeasonNum)
        {
            case 0:
                _currentSeasonType = SeasonType.SUMMER;
                break;
            case 1:
                _currentSeasonType = SeasonType.AUTUMN;
                break;
            case 2:
                _currentSeasonType = SeasonType.WINTER;
                break;
            case 3:
                _currentSeasonType = SeasonType.SPRING;
                break;
            default:
                break;
        }
 
        _currentSeason.StartSeason();

        // Resets elements.
        _elementManager.ResetElementOrder(_currentSeason._elementSpawnOrder);

        // Applying global aesthetic changes.
        ChangeTrees();

        //Currently sun rotation is done within SceneElement
        //StartCoroutine(RotateObject(_sun, _sunTargetRotation, _sunRotationDuration));

        // Fade in.
        StartCoroutine(ManipulateFadeMask(_fadeInDuration, Color.clear));
    }

    // Simple lerp to fade in or out the mask.
    public IEnumerator ManipulateFadeMask(float duration, Color desiredColour)
    {
        // Starting Values.
        var step = 0.0f;
        Color startColour = _fadeMask.color;
        Color targetColour = desiredColour;

        while(step < 1)
        {
            step += Time.deltaTime / duration;
            _fadeMask.color = Color.Lerp(startColour, targetColour, step);
            yield return null;
        }

        yield return null;
    }

    // Changes the season.
    public IEnumerator ChangeSeason()
    {
        // Fades out scene.
        StartCoroutine(ManipulateFadeMask(_fadeOutDuration, _currentSeason._transitionColour));

        yield return new WaitForSeconds(_fadeOutDuration);

        _currentSeason.EndSeason();

        // Changes current season index, ready for setup.
        _currentSeasonNum++;

        SetupSeasons();

        yield return null;
    }

    // Changes the trees.
    private void ChangeTrees()
    {
        foreach(Material mat in _treeMaterials)
        {
            mat.SetTexture(_albedoID, _currentSeason._seasonTreeTex);
        }
    }

    /*
    // Rotates an object's Transform over a set duration to a specific target.
    public IEnumerator RotateObject(GameObject obj, Vector3 target, float duration)
    {
        // Starting values.
        Quaternion startRot = obj.transform.rotation;
        Quaternion targetRot = Quaternion.Euler(target);
        float step = 0.0f;

        while(step < 1)
        {
            step += Time.deltaTime / duration;

            obj.transform.rotation = Quaternion.Lerp(startRot, targetRot, step);
            yield return null;
        }

        yield return null;
    }
    */
}