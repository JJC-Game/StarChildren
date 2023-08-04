using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SaveData_Settings;

public class AudioVolumeController : Singleton<AudioVolumeController>
{
    [SerializeField, Label("音量調整可能")] public bool controllFlg;

    [Header("音量調節のSlider")]
    [SerializeField, Label("BGM")] public Slider BGMSlider;
    [SerializeField, Label("SE")] public Slider SESlider;
    //[SerializeField, Label("環境音")]  public Slider EnvironSlider;
    //[SerializeField, Label("Voice")]   public Slider VoiceSlider;

    //[SerializeField, Label("BGM音量数値")] public TextMeshProUGUI BGMVolText;
    //[SerializeField, Label("SE音量数値")] public TextMeshProUGUI SEVolText;
    //[SerializeField, Label("環境音量数値")]  public TextMeshProUGUI EnvironVolText;
    //[SerializeField, Label("Voice音量数値")] public TextMeshProUGUI VoiceVolText;


    void Start()
    {
        VolumeReflection(); //起動時に1度保存した設定を反映させる
    }

    void Update()
    {
        //音量設定ウィンドウを開いている時は常に音量を変更する状態
        if (controllFlg) VolumeChange();
    }

    //音量設定メニューを開閉する際、音量調整可能かどうかを切り替え
    public void VolumeControll(bool flg)
    {
        controllFlg = flg;
    }

    //音量設定メニューを開く時のみ、保存した音量をSliderに反映させる
    public void VolumeReflection()
    {
        BGMSlider.value = Load.bgm;
        SESlider.value = Load.se;
        //VoiceSlider.value = Load.vo;
        //EnvironSlider.value = Load.en;

        VolumeChange();
    }
    
    //音量設定の変化
    public void VolumeChange()
    {
        //テキストの反映
        //BGMVolText.text = BGMSlider.value.ToString("0");
        //SEVolText.text = SESlider.value.ToString("0");
        //EnvironVolText.text = EnvironSlider.value.ToString("0");
        //VoiceVolText.text   = VoiceSlider.value.ToString("0");

        //Sliderの値をAudioMixerに反映
        SoundManager.Instance.VolumeChange((int)BGMSlider.value, (int)SESlider.value);
    }

    //スライダーの値を保存
    public void VolumeSave()
    {
        int bgm = (int)BGMSlider.value;
        int se = (int)SESlider.value;
        //int vo  = (int)VoiceSlider.value;
        //int en  = (int)EnvironSlider.value;

        Save.Audio(bgm, se);
    }
}