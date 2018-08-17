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
    [Header("Autumn")]
    [SerializeField] private AudioClip _deBloomAC;
    #endregion

    #region Winter

    #endregion

    #region Spring
    [Header("Spring")]
	[SerializeField] private ParticleSystem _soilDumpPT;
    [SerializeField] private ParticleSystem _firePT;
    [SerializeField] private AudioSource _asFire;
    [SerializeField] private AudioClip _soilDumpAC;
    #endregion

    //Fixes campfire and causes dirt tufts
    protected override void EnactSummerActions()
    {
        _campAnim.SetBool("cFireDead", false);
        _campSoilPT.Play();
    }


    protected override void EnactAutumnActions()
    {
        foreach (ParticleSystem p in _elementManager._flowerSoilTuftPT)
        {
            p.Play();
        }

        foreach (Animator a in _elementManager._flowerAnims)
        {
            a.SetBool("Bloomed", false);
        }

        StartCoroutine(AudioDelay(4.9f, _deBloomAC));
    }

    protected override void EnactWinterActions()
    {
        foreach (ParticleSystem p in _elementManager._dandelionBloomPT)
        {
            p.Play();
        }

        foreach (ParticleSystem p in _elementManager._dandelionStillPT)
        {
            p.Play();
        }
    }

	//Dumps soil on campfire, extinguising it and knocking down
    protected override void EnactSpringActions()
    {
        _as.PlayOneShot(_soilDumpAC);
        _asFire.Stop();
        _soilDumpPT.Play();
        _firePT.Stop();
        _campAnim.SetBool("cFireDead", true);
    }

    private IEnumerator AudioDelay(float _delay, AudioClip _ac)
    {
        _activeCoroutines++;
        yield return new WaitForSeconds(_delay);

        _as.PlayOneShot(_ac);

        _activeCoroutines--;
        CalculateActiveStatus();
        yield return null;
    }
}