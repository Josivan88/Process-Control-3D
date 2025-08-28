// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.16 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.16;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:32896,y:32772,varname:node_3138,prsc:2|emission-538-OUT,clip-3387-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32385,y:32372,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Multiply,id:538,x:32594,y:32382,varname:node_538,prsc:2|A-7241-RGB,B-507-OUT;n:type:ShaderForge.SFN_Slider,id:507,x:32228,y:32555,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_507,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:4;n:type:ShaderForge.SFN_TexCoord,id:943,x:31862,y:32938,varname:node_943,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:1347,x:32031,y:32938,varname:node_1347,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-943-UVOUT;n:type:ShaderForge.SFN_Length,id:886,x:32209,y:32938,varname:node_886,prsc:2|IN-1347-OUT;n:type:ShaderForge.SFN_Step,id:6782,x:32129,y:33139,varname:node_6782,prsc:2|A-886-OUT,B-4372-OUT;n:type:ShaderForge.SFN_Slider,id:4372,x:31624,y:33152,ptovrint:False,ptlb:OutherArc,ptin:_OutherArc,varname:node_4372,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:5445,x:31624,y:33252,ptovrint:False,ptlb:InnerArc,ptin:_InnerArc,varname:_OutherArc_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8974359,max:1;n:type:ShaderForge.SFN_Step,id:6032,x:32129,y:33287,varname:node_6032,prsc:2|A-886-OUT,B-5445-OUT;n:type:ShaderForge.SFN_Subtract,id:9197,x:32286,y:33217,varname:node_9197,prsc:2|A-6782-OUT,B-6032-OUT;n:type:ShaderForge.SFN_ComponentMask,id:7564,x:32209,y:32769,varname:node_7564,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1347-OUT;n:type:ShaderForge.SFN_ArcTan2,id:1284,x:32385,y:32769,varname:node_1284,prsc:2|A-7564-G,B-7564-R;n:type:ShaderForge.SFN_RemapRange,id:4989,x:32565,y:32769,varname:node_4989,prsc:2,frmn:-3.14,frmx:3.14,tomn:0,tomx:1|IN-1284-OUT;n:type:ShaderForge.SFN_Slider,id:4360,x:32198,y:32659,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_4360,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Step,id:5440,x:32565,y:32961,varname:node_5440,prsc:2|A-4989-OUT,B-8382-OUT;n:type:ShaderForge.SFN_OneMinus,id:8382,x:32532,y:32605,varname:node_8382,prsc:2|IN-4360-OUT;n:type:ShaderForge.SFN_OneMinus,id:5903,x:32702,y:32956,varname:node_5903,prsc:2|IN-5440-OUT;n:type:ShaderForge.SFN_Multiply,id:3387,x:32642,y:33152,varname:node_3387,prsc:2|A-5903-OUT,B-9197-OUT;proporder:7241-507-4372-5445-4360;pass:END;sub:END;*/

Shader "Shader Forge/CircularIndicator" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _Intensity ("Intensity", Range(0, 4)) = 1
        _OutherArc ("OutherArc", Range(0, 1)) = 1
        _InnerArc ("InnerArc", Range(0, 1)) = 0.8974359
        _Value ("Value", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _Intensity;
            uniform float _OutherArc;
            uniform float _InnerArc;
            uniform float _Value;
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
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float2 node_1347 = (i.uv0*2.0+-1.0);
                float2 node_7564 = node_1347.rg;
                float node_4989 = (atan2(node_7564.g,node_7564.r)*0.1592357+0.5);
                float node_5440 = step(node_4989,(1.0 - _Value));
                float node_886 = length(node_1347);
                clip(((1.0 - node_5440)*(step(node_886,_OutherArc)-step(node_886,_InnerArc))) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (_Color.rgb*_Intensity);
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
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _OutherArc;
            uniform float _InnerArc;
            uniform float _Value;
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
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float2 node_1347 = (i.uv0*2.0+-1.0);
                float2 node_7564 = node_1347.rg;
                float node_4989 = (atan2(node_7564.g,node_7564.r)*0.1592357+0.5);
                float node_5440 = step(node_4989,(1.0 - _Value));
                float node_886 = length(node_1347);
                clip(((1.0 - node_5440)*(step(node_886,_OutherArc)-step(node_886,_InnerArc))) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
