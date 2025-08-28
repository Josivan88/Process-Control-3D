// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1,x:34828,y:32712,varname:node_1,prsc:2|diff-6-OUT,emission-38-OUT,alpha-16-OUT;n:type:ShaderForge.SFN_Slider,id:3,x:33794,y:33031,ptovrint:False,ptlb:Blend,ptin:_Blend,varname:node_6710,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_DepthBlend,id:4,x:34153,y:33010,varname:node_4,prsc:2|DIST-3-OUT;n:type:ShaderForge.SFN_Tex2d,id:5,x:34352,y:32617,ptovrint:False,ptlb:Difuse,ptin:_Difuse,varname:node_6285,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9866-OUT;n:type:ShaderForge.SFN_Multiply,id:6,x:34549,y:32617,varname:node_6,prsc:2|A-7-RGB,B-5-RGB;n:type:ShaderForge.SFN_Color,id:7,x:34487,y:32387,ptovrint:False,ptlb:color,ptin:_color,varname:node_6838,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:13,x:34153,y:32822,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:node_1139,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9866-OUT;n:type:ShaderForge.SFN_Desaturate,id:14,x:34352,y:32844,varname:node_14,prsc:2|COL-13-RGB;n:type:ShaderForge.SFN_Multiply,id:16,x:34563,y:33128,varname:node_16,prsc:2|A-9298-OUT,B-6011-OUT,C-9062-OUT;n:type:ShaderForge.SFN_Rotator,id:27,x:33645,y:32483,varname:node_27,prsc:2|UVIN-29-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:29,x:32554,y:32456,varname:node_29,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector1,id:36,x:34352,y:32783,varname:node_36,prsc:2,v1:0.7;n:type:ShaderForge.SFN_Multiply,id:38,x:34549,y:32795,varname:node_38,prsc:2|A-6-OUT,B-36-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:39,x:32605,y:32616,varname:node_39,prsc:2;n:type:ShaderForge.SFN_Sin,id:47,x:33564,y:32651,varname:node_47,prsc:2|IN-48-OUT;n:type:ShaderForge.SFN_Add,id:48,x:33382,y:32651,varname:node_48,prsc:2|A-39-X,B-39-Y,C-39-Z,D-1642-OUT;n:type:ShaderForge.SFN_Slider,id:49,x:33407,y:32818,ptovrint:False,ptlb:AmplitudeRotation,ptin:_AmplitudeRotation,varname:node_868,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:360;n:type:ShaderForge.SFN_Multiply,id:51,x:34153,y:32659,varname:node_51,prsc:2|A-3003-OUT,B-49-OUT;n:type:ShaderForge.SFN_Lerp,id:2940,x:33531,y:32244,varname:node_2940,prsc:2|A-2960-RGB,B-2945-OUT,T-2943-OUT;n:type:ShaderForge.SFN_Vector1,id:2943,x:33353,y:32358,varname:node_2943,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Vector3,id:2945,x:33205,y:32062,varname:node_2945,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Tex2d,id:2960,x:33205,y:31888,ptovrint:False,ptlb:FlowMap,ptin:_FlowMap,varname:node_2824,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2998-OUT;n:type:ShaderForge.SFN_Add,id:2961,x:33795,y:32251,varname:node_2961,prsc:2|A-2940-OUT,B-29-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:2970,x:33964,y:32281,varname:node_2970,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2961-OUT;n:type:ShaderForge.SFN_Append,id:2979,x:34165,y:32319,varname:node_2979,prsc:2|A-2970-R,B-2970-G;n:type:ShaderForge.SFN_Time,id:2997,x:32436,y:31941,varname:node_2997,prsc:2;n:type:ShaderForge.SFN_Add,id:2998,x:32916,y:32016,varname:node_2998,prsc:2|A-1642-OUT,B-29-UVOUT;n:type:ShaderForge.SFN_Add,id:3000,x:33774,y:32660,varname:node_3000,prsc:2|A-47-OUT,B-3001-OUT;n:type:ShaderForge.SFN_Vector1,id:3001,x:33578,y:32941,varname:node_3001,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:3003,x:33951,y:32660,varname:node_3003,prsc:2|A-3000-OUT,B-3005-OUT;n:type:ShaderForge.SFN_Vector1,id:3005,x:33774,y:32911,varname:node_3005,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:9298,x:34549,y:32963,varname:node_9298,prsc:2|A-14-OUT,B-4-OUT;n:type:ShaderForge.SFN_Vector1,id:6011,x:34325,y:33199,varname:node_6011,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:1642,x:32660,y:32058,varname:node_1642,prsc:2|A-2997-T,B-9493-OUT;n:type:ShaderForge.SFN_Vector1,id:9493,x:32437,y:32160,varname:node_9493,prsc:2,v1:-0.15;n:type:ShaderForge.SFN_Add,id:9866,x:34141,y:32459,varname:node_9866,prsc:2|A-2979-OUT,B-9144-OUT;n:type:ShaderForge.SFN_Multiply,id:9144,x:32637,y:32249,varname:node_9144,prsc:2|A-2997-T,B-1600-OUT;n:type:ShaderForge.SFN_Vector1,id:1600,x:32414,y:32351,varname:node_1600,prsc:2,v1:-0.02;n:type:ShaderForge.SFN_Slider,id:9062,x:34227,y:33319,ptovrint:False,ptlb:MasterOpacity,ptin:_MasterOpacity,varname:node_9062,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_OneMinus,id:2667,x:34371,y:33010,varname:node_2667,prsc:2|IN-4-OUT;proporder:9062-3-5-7-13-49-2960;pass:END;sub:END;*/

Shader "Shader Forge/PerfectSmoke" {
    Properties {
        _MasterOpacity ("MasterOpacity", Range(0, 1)) = 0
        _Blend ("Blend", Range(-1, 1)) = 0
        _Difuse ("Difuse", 2D) = "white" {}
        _color ("color", Color) = (0.5,0.5,0.5,1)
        _Alpha ("Alpha", 2D) = "white" {}
        _AmplitudeRotation ("AmplitudeRotation", Range(0, 360)) = 0
        _FlowMap ("FlowMap", 2D) = "white" {}
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
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu switch 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _CameraDepthTexture;
            uniform float _Blend;
            uniform sampler2D _Difuse; uniform float4 _Difuse_ST;
            uniform float4 _color;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform float _MasterOpacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_2997 = _Time;
                float node_1642 = (node_2997.g*(-0.15));
                float2 node_2998 = (node_1642+i.uv0);
                float4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(node_2998, _FlowMap));
                float2 node_2970 = (lerp(_FlowMap_var.rgb,float3(0,0,1),0.8)+float3(i.uv0,0.0)).rg;
                float2 node_9866 = (float2(node_2970.r,node_2970.g)+(node_2997.g*(-0.02)));
                float4 _Difuse_var = tex2D(_Difuse,TRANSFORM_TEX(node_9866, _Difuse));
                float3 node_6 = (_color.rgb*_Difuse_var.rgb);
                float3 diffuseColor = node_6;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (node_6*0.7);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float4 _Alpha_var = tex2D(_Alpha,TRANSFORM_TEX(node_9866, _Alpha));
                float node_4 = saturate((sceneZ-partZ)/_Blend);
                fixed4 finalRGBA = fixed4(finalColor,((dot(_Alpha_var.rgb,float3(0.3,0.59,0.11))+node_4)*0.5*_MasterOpacity));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu switch 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _CameraDepthTexture;
            uniform float _Blend;
            uniform sampler2D _Difuse; uniform float4 _Difuse_ST;
            uniform float4 _color;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform float _MasterOpacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                UNITY_LIGHT_ATTENUATION(attenuation,i, i.posWorld.xyz);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_2997 = _Time;
                float node_1642 = (node_2997.g*(-0.15));
                float2 node_2998 = (node_1642+i.uv0);
                float4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(node_2998, _FlowMap));
                float2 node_2970 = (lerp(_FlowMap_var.rgb,float3(0,0,1),0.8)+float3(i.uv0,0.0)).rg;
                float2 node_9866 = (float2(node_2970.r,node_2970.g)+(node_2997.g*(-0.02)));
                float4 _Difuse_var = tex2D(_Difuse,TRANSFORM_TEX(node_9866, _Difuse));
                float3 node_6 = (_color.rgb*_Difuse_var.rgb);
                float3 diffuseColor = node_6;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float4 _Alpha_var = tex2D(_Alpha,TRANSFORM_TEX(node_9866, _Alpha));
                float node_4 = saturate((sceneZ-partZ)/_Blend);
                fixed4 finalRGBA = fixed4(finalColor * ((dot(_Alpha_var.rgb,float3(0.3,0.59,0.11))+node_4)*0.5*_MasterOpacity),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
