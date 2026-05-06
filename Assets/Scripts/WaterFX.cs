using UnityEngine;

public class WaterFX : MonoBehaviour
{
    public Texture2D softTexture;

    void Start()
    {
        CreateMainStream();
        CreateSpray();
        CreateSplash();
    }

    // 1) CHORRO PRINCIPAL
    void CreateMainStream()
    {
        var go = new GameObject("Water_Main");
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.Euler(90, 0, 0);

        var ps = go.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.startSpeed = 5f;
        main.startSize = 0.035f;
        main.startLifetime = 0.6f;
        main.gravityModifier = 2f;

        var emission = ps.emission;
        emission.rateOverTime = 350;

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(0.02f, 0.02f, 0.02f);

        var col = ps.colorOverLifetime;
        col.enabled = true;
        Gradient g = new Gradient();
        g.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.6f,0.8f,1f), 0f),
                new GradientColorKey(new Color(0.6f,0.8f,1f), 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.4f, 0f),
                new GradientAlphaKey(0f, 1f)
            }
        );
        col.color = g;

        ApplyMaterial(ps);
    }

    // 2) GOTITAS / SPRAY
    void CreateSpray()
    {
        var go = new GameObject("Water_Spray");
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.Euler(90, 0, 0);

        var ps = go.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.startSpeed = 2f;
        main.startSize = 0.01f;
        main.startLifetime = 0.4f;
        main.gravityModifier = 1f;

        var emission = ps.emission;
        emission.rateOverTime = 80;

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 15f;
        shape.radius = 0.02f;

        ApplyMaterial(ps);
    }

    // 3) SPLASH EN EL SUELO
    void CreateSplash()
    {
        var go = new GameObject("Water_Splash");
        go.transform.parent = transform;
        go.transform.localPosition = new Vector3(0, -1f, 0);

        var ps = go.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.startSpeed = 1.5f;
        main.startSize = 0.02f;
        main.startLifetime = 0.5f;
        main.gravityModifier = 0.5f;

        var emission = ps.emission;
        emission.rateOverTime = 50;

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 25f;

        ApplyMaterial(ps);
    }

    // MATERIAL COMPARTIDO
    void ApplyMaterial(ParticleSystem ps)
    {
        var rend = ps.GetComponent<ParticleSystemRenderer>();

        var mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        mat.SetFloat("_Surface", 1); // Transparent
        mat.SetColor("_BaseColor", new Color(0.6f, 0.8f, 1f, 0.25f));

        if (softTexture != null)
            mat.SetTexture("_BaseMap", softTexture);

        rend.material = mat;
    }
}