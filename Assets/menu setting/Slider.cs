using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        // Thiết lập khoảng 0..1 (nếu chưa)
        slider.minValue = 0f;
        slider.maxValue = 1f;

        // Set giá trị ban đầu = volume đã lưu
        float v = PlayerPrefs.GetFloat(AudioManager.VolumeKey, 0.5f);
        slider.value = v;

        // Lắng nghe thay đổi và đẩy vào AudioManager
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        if (AudioManager.instance != null)
            AudioManager.instance.SetVolume(value); // sẽ auto save
    }
}
