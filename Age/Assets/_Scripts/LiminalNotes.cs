using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiminalNotes : MonoBehaviour {

    void EndExperience()
    {
        // End Experience.
        // using Liminal.SDK.Core;
        // ExperienceApp.End();

        // Fading Out.
        // using Liminal.Core.Fader;
        // var fade = ScreenFader.Instance;
        // fader.FadeTo(Color fadeColor, float fadeDuration);
    }

    void Input()
    {
        // Inputs shared between all devices.
        // "VRButton.One".
        // "VRButton.DPadUp".
        // "VRButton.DPadDown".
        // "VRButton.DPadLeft".
        // "VRButton.DPadRight".

        // Other Inputs (use not recommended, can be used for non-essential functionality).
        // "VRButton.Trigger".
        // "VRButton.Touch".
    }

    #region "Do not's"

    // Do not change "Time.timeScale". Leave at default of "1".
    // Do not change "QualitySetttings".
    // Do not change "Graphics".
    // Do not use "VRButton.Back".
    // Do not use any other script but "C#".
    // Do not use "Curvy".
    // Do not use "PostProcessing".
    // Do not use default parameters in methods.
    // Discuss with a lecturer about "Static Variables".
    // Avoid using "Static Variables".
    // Do not call "DontDestroyOnLoad()".


    #endregion

    #region "Do's:

    // Unity is running "2017.2.0p4".
    // Discuss with lecturer about ".NET 3.5" and "Mono".
    // Ensure sounds are normalised.
    // Ensure experiences maintain at least 60fps accross all devices.
    // Ensure single scene.
    // Ensure only C# scripts.
    // Ensure [Serializable] classes use constructors >>>without<<< arguments.
    // Ensure "Scriptable Objects" need a "[PreferBinarySerialization]" attribute.
    // Ensure we own all rights to content or ensure they comply with the conditions of the original content owner.
    // Ensure all assets comply with "re-use for commercial purpose".
    // Use Liminal fader to end experience.
    // Ensure experience is designed to work well for users with or without handheld controllers.
    // Ensure experience is designed to work for all input devices (See "void Input()" for details).
    // Ensure "Graphics Jobs" are not used.
    // Ensure Experiences support "Single Pass Rendering".
    // Ensure scene setup abides Liminal's template.

    // Ask lecturer about "Graphics Jobs".
    // Discuss use of Oculus Utils with Liminal.

    #endregion
}
