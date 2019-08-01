// Created by Jordi van Os

Shader "Custom/HologramRadar"
{
    Properties
    {
        _MainTex ("Brightness Texture (a)", 2D) = "white" {}
        [HDR] _Colour("Colour", Color) = (1,1,1,0.75)
        _MinimumBrightness ("Min Brightness", Range(0.0, 1.0)) = 0.0
        _ArtifactScale ("Artifact Scale", Float) = 500.0
        _ArtifactGap ("Artifact Gap", Range(0.0, 2.0)) = 0.5
        _ArtifactTransparency ("Artifact Transparency", Range(0.0,1.0)) = 0.25
        _ArtifactBrightnessScaler ("Artifact Brightness Scaler", Float) = 0.75
        _ArtifactMovement ("Artifact Movement", Float) = 0.01
        _EdgeFade ("Edge Fade", Range(0.0, 1.0)) = 0.9

    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off                  // Don't write to the depth texture
        Cull Off                    // Also render the backface
        Blend SrcAlpha One			// Blend with the environment

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 pos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Colour;
            float _MinimumBrightness;
            float _ArtifactScale;
            float _ArtifactGap;
            float _ArtifactTransparency;
            float _ArtifactBrightnessScaler;
            float _ArtifactMovement;
            float _EdgeFade;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = v.vertex;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Base brightness is from grayscale image
                float brightness = tex2D(_MainTex, i.uv).r;
                brightness = max(brightness, _MinimumBrightness);

                // Artifact intensity based distance from centre
                float distanceFromCentre = length(i.uv * 2.0 - 1.0);
                float artifactPos = distanceFromCentre * _ArtifactScale - _ArtifactMovement * _Time.y;
                float artifact = -artifactPos % 1.0;
                artifact = 1.0 - artifact;

                // Make artifact less intense at bright spots and gap size
                float artifactGap = _ArtifactGap * (1.0 - brightness * _ArtifactBrightnessScaler);

                // Scale artifact based on given gap size
                artifact = smoothstep(artifactGap - 1.0, artifactGap, artifact);

                // Limit artifact by given transparency
                artifact = max(artifact, _ArtifactTransparency);

                // Scale brightness by artifact
                brightness *= artifact;

                // Fade around the edges
                brightness *= 1.0 - smoothstep(_EdgeFade, 1.0, distanceFromCentre);

                fixed4 colour = _Colour * brightness;
                return colour;
            }
            ENDCG
        }
    }
}