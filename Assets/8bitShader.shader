Shader "Custom/8bitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorSteps ("Color Steps", Float) = 8
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _ColorSteps;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the original texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Quantize the color to a limited palette
                col.r = floor(col.r * _ColorSteps + 0.5) / _ColorSteps;
                col.g = floor(col.g * _ColorSteps + 0.5) / _ColorSteps;
                col.b = floor(col.b * _ColorSteps + 0.5) / _ColorSteps;

                // Apply gamma correction for better brightness
                col.rgb = pow(col.rgb, 2.2); // Convert to linear space for correct quantization
                col.rgb = pow(col.rgb, 1.0 / 2.2); // Convert back to gamma space

                return col;
            }
            ENDCG
        }
    }
}
