// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33446,y:32681,varname:node_3138,prsc:2|emission-3482-OUT,alpha-9565-OUT;n:type:ShaderForge.SFN_TexCoord,id:2390,x:31244,y:32766,varname:node_2390,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ComponentMask,id:5553,x:31429,y:32766,varname:node_5553,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2390-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3535,x:31853,y:33115,varname:node_3535,prsc:2|A-9332-OUT,B-4653-OUT;n:type:ShaderForge.SFN_Cos,id:7055,x:32029,y:33115,varname:node_7055,prsc:2|IN-3535-OUT;n:type:ShaderForge.SFN_Append,id:4006,x:32208,y:33115,varname:node_4006,prsc:2|A-7055-OUT,B-7055-OUT;n:type:ShaderForge.SFN_Desaturate,id:2579,x:32388,y:33115,varname:node_2579,prsc:2|COL-4006-OUT;n:type:ShaderForge.SFN_Step,id:6866,x:32584,y:33182,varname:node_6866,prsc:2|A-2579-OUT,B-3435-OUT;n:type:ShaderForge.SFN_Slider,id:6341,x:31964,y:33359,ptovrint:False,ptlb:WaveLength,ptin:_WaveLength,varname:node_6341,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.111,cur:0.5,max:1;n:type:ShaderForge.SFN_OneMinus,id:8014,x:32767,y:33137,varname:node_8014,prsc:2|IN-6866-OUT;n:type:ShaderForge.SFN_Add,id:4653,x:31620,y:33154,varname:node_4653,prsc:2|A-9334-OUT,B-8689-OUT;n:type:ShaderForge.SFN_Time,id:981,x:31138,y:33114,varname:node_981,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9565,x:32995,y:32902,varname:node_9565,prsc:2|A-7626-OUT,B-186-OUT;n:type:ShaderForge.SFN_Multiply,id:9334,x:31394,y:33197,varname:node_9334,prsc:2|A-981-T,B-1972-OUT;n:type:ShaderForge.SFN_Slider,id:1972,x:31007,y:33346,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_1972,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4,max:5;n:type:ShaderForge.SFN_Power,id:7626,x:32777,y:32603,varname:node_7626,prsc:2|VAL-8689-OUT,EXP-3411-OUT;n:type:ShaderForge.SFN_Slider,id:3411,x:32355,y:32575,ptovrint:False,ptlb:Fade,ptin:_Fade,varname:node_3411,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5,max:10;n:type:ShaderForge.SFN_Sqrt,id:9105,x:32336,y:32717,varname:node_9105,prsc:2|IN-9333-OUT;n:type:ShaderForge.SFN_Multiply,id:6263,x:31950,y:32648,varname:node_6263,prsc:2|A-8254-OUT,B-8254-OUT;n:type:ShaderForge.SFN_Add,id:9333,x:32136,y:32717,varname:node_9333,prsc:2|A-6263-OUT,B-1951-OUT;n:type:ShaderForge.SFN_Multiply,id:1951,x:31950,y:32806,varname:node_1951,prsc:2|A-1732-OUT,B-1732-OUT;n:type:ShaderForge.SFN_Subtract,id:8254,x:31744,y:32658,varname:node_8254,prsc:2|A-5553-R,B-5736-OUT;n:type:ShaderForge.SFN_Vector1,id:5736,x:31518,y:32604,varname:node_5736,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Subtract,id:1732,x:31744,y:32806,varname:node_1732,prsc:2|A-5553-G,B-5736-OUT;n:type:ShaderForge.SFN_OneMinus,id:8689,x:32539,y:32728,varname:node_8689,prsc:2|IN-9105-OUT;n:type:ShaderForge.SFN_Color,id:5366,x:32995,y:32675,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5366,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.03875834,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:186,x:33148,y:33137,varname:node_186,prsc:2|A-2579-OUT,B-8014-OUT,T-3459-OUT;n:type:ShaderForge.SFN_Slider,id:1796,x:32772,y:33399,ptovrint:False,ptlb:Softness,ptin:_Softness,varname:node_1796,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4,max:1;n:type:ShaderForge.SFN_Slider,id:3208,x:33117,y:32570,ptovrint:False,ptlb:EmissionIntensity,ptin:_EmissionIntensity,varname:node_3208,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4,max:10;n:type:ShaderForge.SFN_Multiply,id:3482,x:33206,y:32662,varname:node_3482,prsc:2|A-3208-OUT,B-5366-RGB;n:type:ShaderForge.SFN_OneMinus,id:3459,x:33201,y:33374,varname:node_3459,prsc:2|IN-1796-OUT;n:type:ShaderForge.SFN_Slider,id:9332,x:31166,y:33008,ptovrint:False,ptlb:Frequency,ptin:_Frequency,varname:node_9332,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:20,max:60;n:type:ShaderForge.SFN_OneMinus,id:3435,x:32365,y:33320,varname:node_3435,prsc:2|IN-6341-OUT;proporder:5366-3208-1972-6341-3411-1796-9332;pass:END;sub:END;*/

Shader "Shader Forge/RadioWaves" {
    Properties {
        _Color ("Color", Color) = (1,0.03875834,0,1)
        _EmissionIntensity ("EmissionIntensity", Range(0, 10)) = 4
        _Speed ("Speed", Range(0, 5)) = 0.4
        _WaveLength ("WaveLength", Range(0.111, 1)) = 0.5
        _Fade ("Fade", Range(0, 10)) = 5
        _Softness ("Softness", Range(0, 1)) = 0.4
        _Frequency ("Frequency", Range(0, 60)) = 20
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _WaveLength;
            uniform float _Speed;
            uniform float _Fade;
            uniform float4 _Color;
            uniform float _Softness;
            uniform float _EmissionIntensity;
            uniform float _Frequency;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float3 emissive = (_EmissionIntensity*_Color.rgb);
                float3 finalColor = emissive;
                float2 node_5553 = i.uv0.rg;
                float node_5736 = 0.5;
                float node_8254 = (node_5553.r-node_5736);
                float node_1732 = (node_5553.g-node_5736);
                float node_8689 = (1.0 - sqrt(((node_8254*node_8254)+(node_1732*node_1732))));
                float4 node_981 = _Time;
                float node_7055 = cos((_Frequency*((node_981.g*_Speed)+node_8689)));
                float node_2579 = dot(float2(node_7055,node_7055),float3(0.3,0.59,0.11));
                return fixed4(finalColor,(pow(node_8689,_Fade)*lerp(node_2579,(1.0 - step(node_2579,(1.0 - _WaveLength))),(1.0 - _Softness))));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
