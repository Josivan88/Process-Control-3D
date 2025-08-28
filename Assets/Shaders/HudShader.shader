// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:6,wrdp:False,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1,x:33747,y:32712,varname:node_1,prsc:2|emission-2-OUT,clip-13-OUT;n:type:ShaderForge.SFN_Multiply,id:2,x:33446,y:32751,varname:node_2,prsc:2|A-3-RGB,B-4-OUT,C-5-RGB,D-24-OUT;n:type:ShaderForge.SFN_Tex2d,id:3,x:33213,y:32631,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_6601,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:4,x:33070,y:32873,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_5551,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:10;n:type:ShaderForge.SFN_Color,id:5,x:33205,y:32238,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_2393,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:10,x:33071,y:33179,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_3011,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:11,x:32975,y:32982,ptovrint:False,ptlb:OpacityMap,ptin:_OpacityMap,varname:node_8143,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:13,x:33462,y:32991,varname:node_13,prsc:2|A-14-OUT,B-10-OUT;n:type:ShaderForge.SFN_Desaturate,id:14,x:33213,y:32973,varname:node_14,prsc:2|COL-11-RGB;n:type:ShaderForge.SFN_Color,id:23,x:33062,y:32238,ptovrint:False,ptlb:SecColor,ptin:_SecColor,varname:node_8420,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:24,x:33426,y:32385,varname:node_24,prsc:2|A-5-RGB,B-23-RGB,T-26-OUT;n:type:ShaderForge.SFN_Slider,id:26,x:33048,y:32457,ptovrint:False,ptlb:LerpColor,ptin:_LerpColor,varname:node_6649,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:5-23-26-3-4-11-10;pass:END;sub:END;*/

Shader "HUD/HudShader" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _SecColor ("SecColor", Color) = (1,1,1,1)
        _LerpColor ("LerpColor", Range(0, 1)) = 0
        _Texture ("Texture", 2D) = "white" {}
        _Intensity ("Intensity", Range(-1, 10)) = 1
        _OpacityMap ("OpacityMap", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 1
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
            Cull Off
            ZTest Always
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu switch 
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Intensity;
            uniform float4 _Color;
            uniform float _Opacity;
            uniform sampler2D _OpacityMap; uniform float4 _OpacityMap_ST;
            uniform float4 _SecColor;
            uniform float _LerpColor;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _OpacityMap_var = tex2D(_OpacityMap,TRANSFORM_TEX(i.uv0, _OpacityMap));
                clip((dot(_OpacityMap_var.rgb,float3(0.3,0.59,0.11))*_Opacity) - 0.5);
////// Lighting:
////// Emissive:
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 emissive = (_Texture_var.rgb*_Intensity*_Color.rgb*lerp(_Color.rgb,_SecColor.rgb,_LerpColor));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu switch 
            #pragma target 3.0
            uniform float _Opacity;
            uniform sampler2D _OpacityMap; uniform float4 _OpacityMap_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _OpacityMap_var = tex2D(_OpacityMap,TRANSFORM_TEX(i.uv0, _OpacityMap));
                clip((dot(_OpacityMap_var.rgb,float3(0.3,0.59,0.11))*_Opacity) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
