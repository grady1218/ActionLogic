Shader "MyShader/TestShader"
{
    Properties{
        _color("Color",Color) = (1,1,1,1)
    }
    SubShader{

        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct VertInput{
                float4 pos : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct VertOutput{
                float4 sv_pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            VertOutput vert(VertInput input){
                VertOutput output;
                output.sv_pos = UnityObjectToClipPos(input.pos);
                output.uv = input.uv;
                return output;
            }

            float4 _color;

            float4 frag(VertOutput output) : SV_Target{
                float3 color = (1,1,1);
                float range = _Time;
                float sin = _SinTime;
                color -= _color * sin;
                return float4(color,1);

            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}
