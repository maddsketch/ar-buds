using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStateManager
{   
    public enum APP_STATE { LOADING, TRACKING_MARKER, TRACKED_TO_MARKER }
    public enum TRACKING_STATE { TRACKING, TRACKING_FOUND }
    private TRACKING_STATE m_trackingState;
    public TRACKING_STATE TrackingState
    {
        set { m_trackingState = value; }
        get { return m_trackingState; }
    }

    public enum PREFERENCE_TYPE : int
    {
        BG_AUDIO = 0,
        SFX_VOLUME = 1        
    }
   
    private static AppStateManager instance;
    public static AppStateManager stateManager
    {
        get
        {
            if (instance == null)
            {
                instance = new AppStateManager();
            }

            return instance;
        }
    }
 
    private AudioSettingsToken _settingsToken = null;
    public AudioSettingsToken audioSettings
    {
        get { return _settingsToken; }
    }

    //** Empty constructor
    private AppStateManager()
    {
        _settingsToken = new AudioSettingsToken();
        //InitAudioSettingsToken();
    }

    private void InitAudioSettingsToken()
    {
        Vector2 lastSetting = PlayerPreferencesManager.GetAudioSettings();
        if (lastSetting.x == -1.0f) // TO DO: rewrite this so you're referring to a constant, not "x" in the setting
            _settingsToken.SetBackgroundMusicVolume(0.0f);
        else
            _settingsToken.SetBackgroundMusicVolume(lastSetting.x);

        if (lastSetting.y == -1.0f) // TO DO: rewrite this so you're referring to a constant, not "y" in the setting
            _settingsToken.SetSFXVolume(0.0f);
        else
            _settingsToken.SetSFXVolume(lastSetting.y);

    }
    
    #region Audio methods
    public void SetAudioSettings(float bgAudioVolume, float sfxVolume)
    {
        if (_settingsToken == null)
            _settingsToken = new AudioSettingsToken();

        _settingsToken.SetBackgroundMusicVolume(bgAudioVolume);
        _settingsToken.SetSFXVolume(sfxVolume);
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        if (_settingsToken == null)
            _settingsToken = new AudioSettingsToken();

        _settingsToken.SetBackgroundMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (_settingsToken == null)
            _settingsToken = new AudioSettingsToken();

        _settingsToken.SetSFXVolume(volume);
    }
    #endregion

    public void WriteAudioSettingsToPreferences()
    {
        if (_settingsToken != null)
        {
            PlayerPreferencesManager.WriteAudioSettings(_settingsToken);
            Debug.Log("Audio settings written to PlayerPrefs");
        }
        else
            Debug.Log("Error: unable to write audio settings to PlayerPrefs");
    }
      

    /// <summary>
    /// AudioSettingsToken
    /// 
    /// Used to wrap and pass around audio settings to/from other classes
    /// </summary>    
    public class AudioSettingsToken // TODO: rewrite so this returns a static reference - only ever want one token live
    {
        private float _backgroundVolume;
        public float backgroundAudioVolume
        {
            get { return _backgroundVolume; }

        }

        private float _sfxVolume;
        public float sfxVolume
        {
            get { return _sfxVolume; }
        }

        public void SetBackgroundMusicVolume(float volume)
        {
            _backgroundVolume = volume;
        }

        public void SetSFXVolume(float volume)
        {
            _sfxVolume = volume;
        }

    } // end inner class AudioSettingsToken

    /// <summary>
    /// PlayerPreferencesManager
    /// 
    /// Reads/writes to Unity PlayerPrefs.
    /// Note: Only AppStateManager has access to this class
    /// </summary>
    class PlayerPreferencesManager // TO DO: want to type an object so read/write calls are generic
    {
        
        private static string BG_AUDIO = "BG_AUDIO_VOLUME";
        private static string SFX_VOLUME = "SFX_VOLUME";
        private static string TandS_AGREEMENT = "TandS_AGREEMENT";

        public static void WriteAudioSettings(AudioSettingsToken settingsToken)
        {
            PlayerPrefs.SetFloat(BG_AUDIO, settingsToken.backgroundAudioVolume);
            PlayerPrefs.SetFloat(SFX_VOLUME, settingsToken.sfxVolume);
        }

        public static Vector2 GetAudioSettings()
        {
            // BY DEFAULT, THE VOLUME IS SET TO 0.5;
            float bgAudio = 0.5f;
            float sfx = 0.5f;

            if (PlayerPrefs.HasKey(BG_AUDIO))
                bgAudio = PlayerPrefs.GetFloat(BG_AUDIO);

            if (PlayerPrefs.HasKey(BG_AUDIO))
                sfx = PlayerPrefs.GetFloat(SFX_VOLUME);

            return new Vector2(bgAudio, sfx);
        }


    } // end class PlayerPrefs
   

} // end AppStateManager class

