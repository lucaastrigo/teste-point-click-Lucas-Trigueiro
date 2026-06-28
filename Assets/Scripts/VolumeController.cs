using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public static VolumeController Instance;

    [Header("Sources")]
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] audioClips;
    
    private float masterVolume;
    private float ambienceVolume;
    private float sfxVolume;
    
    private const string MasterVolumeKey = "MasterVolume";
    private const string AmbienceVolumeKey = "AmbienceVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ambienceVolume = PlayerPrefs.GetFloat(AmbienceVolumeKey, 0.5f);
        sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 0.5f);
        masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1f);

        if (ambienceSlider != null) ambienceSlider.value = ambienceVolume;
        if (sfxSlider != null) sfxSlider.value = sfxVolume;
        if (masterSlider != null) masterSlider.value = masterVolume;

        ApplyVolumes();

        if (ambienceSlider != null) ambienceSlider.onValueChanged.AddListener(SetAmbienceVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        if (masterSlider != null) masterSlider.onValueChanged.AddListener(SetMasterVolume);

        if (ambienceSource != null) ambienceSource.ignoreListenerPause = true;
    }

    public void PlaySound(string soundName)
    {
        AudioClip clip = System.Array.Find(audioClips, sound => sound.name == soundName);
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    public void SetAmbienceVolume(float volume)
    {
        ambienceVolume = volume;
        ApplyVolumes();
        PlayerPrefs.SetFloat(AmbienceVolumeKey, volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        ApplyVolumes();
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        ApplyVolumes();
        PlayerPrefs.SetFloat(MasterVolumeKey, volume);
    }

    private void ApplyVolumes()
    {
        if (sfxSource != null) sfxSource.volume = sfxVolume * masterVolume;
        if (ambienceSource != null) ambienceSource.volume = ambienceVolume * masterVolume;
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
