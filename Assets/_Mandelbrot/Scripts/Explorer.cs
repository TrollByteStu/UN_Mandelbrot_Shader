using Unity.Mathematics;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale;

    Vector2 smoothpos;
    float smoothscale;
    void UpdateShader()
    {
        smoothpos = Vector2.Lerp(smoothpos, pos, .1f);
        smoothscale = math.lerp(smoothscale, scale, .1f);
        float aspect = (float)Screen.width / (float)Screen.height;
        float scalex = smoothscale;
        float scaley = smoothscale;

        if (aspect > 1f)
            scaley /= aspect;
        else
            scalex *= aspect;
        mat.SetVector("_Area", new Vector4(smoothpos.x, smoothpos.y,scalex,scaley));

    }
    void HandleInputs()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
            scale *= .99f;
        if (Input.GetKey(KeyCode.KeypadMinus))
            scale *= 1.01f;

        if (Input.GetKey(KeyCode.A))
            pos.x -= .01f * scale;
        if (Input.GetKey(KeyCode.D))
            pos.x += .01f * scale;

        if (Input.GetKey(KeyCode.S))
            pos.y -= .01f * scale;
        if (Input.GetKey(KeyCode.W))
            pos.y += .01f * scale;
    }
    private void FixedUpdate()
    {
        HandleInputs();
        UpdateShader();
    }

}
