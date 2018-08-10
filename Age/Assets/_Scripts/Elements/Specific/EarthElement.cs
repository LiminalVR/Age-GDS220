using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElement : BaseElement {

	#region Summer
	[Header("Summer")]
	[SerializeField] private Animator _campAnim;
	[SerializeField] private ParticleSystem _campSoilPT;
    #endregion

    #region Autumn
    //[Header("Autumn")]
    private List<Animator> _flowerAnims;
    public GameObject[] _stemBase;
    public float _scaleDuration;
    public Vector3 _scaleTarget;
    #endregion

    #region Winter
    //[Header("Winter")]
    private List<ParticleSystem> _flowerSoilTuftPT;
    #endregion

    #region Spring
    [Header("Spring")]
	[SerializeField] private ParticleSystem _soilDumpPT;
    [SerializeField] private ParticleSystem _firePT;
	#endregion

	private void Start()
	{
        //List initialise
        _flowerSoilTuftPT = new List<ParticleSystem>();
        _flowerAnims = new List<Animator>();

        //Particle lists
        var _findParticles = FindObjectsOfType<ParticleSystem>();

		foreach (ParticleSystem p in _findParticles) {
			switch (p.tag) {
            case ("SoilTuftParticle"):
                _flowerSoilTuftPT.Add(p);
                break;
                default:
				break;
			}
		}

        //Object arrays
        _stemBase = GameObject.FindGameObjectsWithTag("StemBase");

        //Find animators on flowers only
        Animator _findAnimators;

        foreach (GameObject a in _stemBase)
        {
            _findAnimators = a.GetComponentInParent<Animator>();

            if (_findAnimators != null)
                _flowerAnims.Add(_findAnimators);
        }
    }

	//Fixes campfire and causes dirt tufts
    protected override void EnactSummerActions(bool initialAction)
    {
        _campAnim.SetBool("cFireDead", false);
        _campSoilPT.Play();

        if (initialAction)
        {
            
        }
        else
        {
		
        }
    }


    protected override void EnactAutumnActions(bool initialAction)
    {
        foreach (ParticleSystem p in _flowerSoilTuftPT)
        {
            p.Play();
        }

        if (initialAction)
        {
			
        }
        else
        {

        }
    }

	//
    protected override void EnactWinterActions(bool initialAction)
    {
        

        if (initialAction)
		{
			
        }
        else
        {
            
        }
    }

	//Dumps soil on campfire, extinguising it and knocking down
    protected override void EnactSpringActions(bool initialAction)
    {
        _soilDumpPT.Play();
        _firePT.Stop();
        _campAnim.SetBool("cFireDead", true);

        if (initialAction)
        {
			
        }
        else
        {

        }
    }

    public void ScaleDoodad(GameObject[] _objectArray, float _scaleDuration, Vector3 _scaleTarget)
    {
        foreach (GameObject g in _objectArray)
        {
            StartCoroutine(ScaleOverTime(g, _scaleDuration, _scaleTarget));
        }
    }
    
	private IEnumerator ScaleOverTime(GameObject obj, float duration, Vector3 scale)
	{
		Vector3 originalScale = obj.transform.localScale;

		float currentTime = 0.0f;

		do
		{
			obj.transform.localScale = Vector3.Lerp(originalScale, scale, currentTime / duration);
			currentTime += Time.deltaTime;
			yield return null;
		}
		while(currentTime <= duration);
	}
    
}