using UnityEngine;

[CreateAssetMenu(fileName = "TemplatePattern", menuName = "TemplateBulletPattern")]
public class TemplateBulletPattern : ScriptableObject
{
    [SerializeField] private string stats = "";
    [field: SerializeField] public BulletVisual visual { get; private set; } = BulletVisual.Round_Red;
    [field: SerializeField] public float frequency { get; private set; } = 0.5f;
    [field: SerializeField] public int totalBulletsShot { get; private set; } = 10;
    [field: SerializeField] public float speed { get; private set; } = 0f;
    [field: SerializeField] public float angle { get; private set; } = 0f;
    [field: SerializeField] public float angularVelocity { get; private set; } = 0f;
    [field: SerializeField] public float angularAcceleration { get; private set; } = 0f;

    [field: SerializeField] public int[] splitAngles { get; private set; } = new int[] {};

    void OnValidate() {
        stats = $"Total Duration: {frequency * totalBulletsShot}";
    }
}
