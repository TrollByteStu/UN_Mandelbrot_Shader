using Unity.Mathematics;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale, angle;

    Vector2 smoothpos;
    float smoothscale, smoothAngle;
    void UpdateShader()
    {
        smoothpos = Vector2.Lerp(smoothpos, pos, .1f);
        smoothscale = math.lerp(smoothscale, scale, .1f);
        smoothAngle = math.lerp(smoothAngle, angle, .1f);

        float aspect = (float)Screen.width / (float)Screen.height;

        float scalex = smoothscale;
        float scaley = smoothscale;

        if (aspect > 1f)
            scaley /= aspect;
        else
            scalex *= aspect;
        mat.SetVector("_Area", new Vector4(smoothpos.x, smoothpos.y,scalex,scaley));
        mat.SetFloat("_Angle", smoothAngle);

    }
    void HandleInputs()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
            scale *= .99f;
        if (Input.GetKey(KeyCode.KeypadMinus))
            scale *= 1.01f;

        if (Input.GetKey(KeyCode.E))
            angle -= .01f;
        if (Input.GetKey(KeyCode.Q))
            angle += .01f;

        Vector2 dir = new Vector2(.01f * scale, 0);
        float s = math.sin(angle);
        float c = math.cos(angle);
        dir = new Vector2(dir.x*c,dir.x*s);

        if (Input.GetKey(KeyCode.A))
            pos -= dir;
        if (Input.GetKey(KeyCode.D))
            pos += dir;

        dir = new Vector2(-dir.y, dir.x);
        if (Input.GetKey(KeyCode.S))
            pos -= dir;
        if (Input.GetKey(KeyCode.W))
            pos += dir;


    }
    private void FixedUpdate()
    {
        HandleInputs();
        UpdateShader();
    }

}
