// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33426,y:32764,varname:node_2865,prsc:2|emission-6677-OUT;n:type:ShaderForge.SFN_SceneColor,id:3929,x:32281,y:32811,varname:node_3929,prsc:2|UVIN-4854-OUT;n:type:ShaderForge.SFN_ScreenPos,id:1285,x:31200,y:33927,varname:node_1285,prsc:2,sctp:2;n:type:ShaderForge.SFN_Slider,id:7237,x:30797,y:32517,ptovrint:False,ptlb:Offset,ptin:_Offset,varname:node_7237,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.01;n:type:ShaderForge.SFN_Add,id:8727,x:31899,y:32811,varname:node_8727,prsc:2|A-4975-OUT,B-1285-U;n:type:ShaderForge.SFN_Append,id:4854,x:32087,y:32811,varname:node_4854,prsc:2|A-8727-OUT,B-1285-V;n:type:ShaderForge.SFN_Add,id:4680,x:31899,y:33039,varname:node_4680,prsc:2|A-4975-OUT,B-1285-V;n:type:ShaderForge.SFN_Append,id:525,x:32087,y:33039,varname:node_525,prsc:2|A-1285-U,B-4680-OUT;n:type:ShaderForge.SFN_SceneColor,id:6222,x:32281,y:33039,varname:node_6222,prsc:2|UVIN-525-OUT;n:type:ShaderForge.SFN_Add,id:8255,x:32750,y:32902,varname:node_8255,prsc:2|A-3929-RGB,B-6222-RGB,C-6414-RGB,D-6899-RGB,E-3471-RGB;n:type:ShaderForge.SFN_Multiply,id:6677,x:33166,y:32955,varname:node_6677,prsc:2|A-8255-OUT,B-3550-OUT;n:type:ShaderForge.SFN_Vector1,id:3550,x:33166,y:33125,varname:node_3550,prsc:2,v1:0.2;n:type:ShaderForge.SFN_DepthBlend,id:6111,x:31225,y:32407,varname:node_6111,prsc:2|DIST-6726-OUT;n:type:ShaderForge.SFN_Slider,id:6726,x:30797,y:32387,ptovrint:False,ptlb:Distance,ptin:_Distance,varname:node_6726,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.1,cur:0,max:0.1;n:type:ShaderForge.SFN_Multiply,id:4975,x:31225,y:32573,varname:node_4975,prsc:2|A-6111-OUT,B-7237-OUT;n:type:ShaderForge.SFN_SceneColor,id:6414,x:32281,y:32634,varname:node_6414,prsc:2|UVIN-1285-UVOUT;n:type:ShaderForge.SFN_Append,id:6168,x:32087,y:33227,varname:node_6168,prsc:2|A-5048-OUT,B-1285-V;n:type:ShaderForge.SFN_SceneColor,id:6899,x:32281,y:33227,varname:node_6899,prsc:2|UVIN-6168-OUT;n:type:ShaderForge.SFN_Subtract,id:5048,x:31899,y:33227,varname:node_5048,prsc:2|A-1285-U,B-4975-OUT;n:type:ShaderForge.SFN_SceneColor,id:3471,x:32281,y:33392,varname:node_3471,prsc:2|UVIN-6238-OUT;n:type:ShaderForge.SFN_Append,id:6238,x:32087,y:33392,varname:node_6238,prsc:2|A-1285-U,B-8359-OUT;n:type:ShaderForge.SFN_Subtract,id:8359,x:31899,y:33392,varname:node_8359,prsc:2|A-1285-V,B-4975-OUT;n:type:ShaderForge.SFN_Add,id:3861,x:32726,y:33775,varname:node_3861,prsc:2;n:type:ShaderForge.SFN_Add,id:2404,x:32932,y:33304,varname:node_2404,prsc:2|A-8255-OUT,B-3861-OUT;n:type:ShaderForge.SFN_Add,id:5577,x:31899,y:33595,varname:node_5577,prsc:2|A-4975-OUT,B-1285-U;n:type:ShaderForge.SFN_Append,id:2659,x:32091,y:33683,varname:node_2659,prsc:2|A-5577-OUT,B-6080-OUT;n:type:ShaderForge.SFN_SceneColor,id:3318,x:32285,y:33683,varname:node_3318,prsc:2|UVIN-2659-OUT;n:type:ShaderForge.SFN_Add,id:6080,x:31899,y:33748,varname:node_6080,prsc:2|A-4975-OUT,B-1285-V;n:type:ShaderForge.SFN_Add,id:9856,x:31897,y:33929,varname:node_9856,prsc:2|A-4975-OUT,B-1285-U;n:type:ShaderForge.SFN_Append,id:4535,x:32089,y:34006,varname:node_4535,prsc:2|A-9856-OUT,B-4681-OUT;n:type:ShaderForge.SFN_SceneColor,id:8854,x:32283,y:34006,varname:node_8854,prsc:2|UVIN-4535-OUT;n:type:ShaderForge.SFN_Subtract,id:4681,x:31897,y:34066,varname:node_4681,prsc:2|A-4975-OUT,B-1285-V;n:type:ShaderForge.SFN_Add,id:5462,x:31897,y:34385,varname:node_5462,prsc:2|A-4975-OUT,B-1285-V;n:type:ShaderForge.SFN_Subtract,id:8077,x:31897,y:34233,varname:node_8077,prsc:2|A-4975-OUT,B-1285-U;n:type:ShaderForge.SFN_Append,id:7009,x:32090,y:34324,varname:node_7009,prsc:2|A-8077-OUT,B-5462-OUT;n:type:ShaderForge.SFN_SceneColor,id:5389,x:32284,y:34324,varname:node_5389,prsc:2|UVIN-7009-OUT;n:type:ShaderForge.SFN_SceneColor,id:7252,x:32281,y:34652,varname:node_7252,prsc:2|UVIN-923-OUT;n:type:ShaderForge.SFN_Append,id:923,x:32087,y:34652,varname:node_923,prsc:2|A-4162-OUT,B-6441-OUT;n:type:ShaderForge.SFN_Subtract,id:4162,x:31894,y:34561,varname:node_4162,prsc:2|A-4975-OUT,B-1285-U;n:type:ShaderForge.SFN_Subtract,id:6441,x:31894,y:34729,varname:node_6441,prsc:2|A-4975-OUT,B-1285-V;proporder:7237-6726;pass:END;sub:END;*/

Shader "Shader Forge/FrostedGlass" {
    Properties {
        _Offset ("Offset", Range(0, 0.01)) = 0
        _Distance ("Distance", Range(-0.1, 0.1)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
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
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu switch 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _CameraDepthTexture;
            uniform float _Offset;
            uniform float _Distance;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 projPos : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float node_4975 = (saturate((sceneZ-partZ)/_Distance)*_Offset);
                float3 node_8255 = (tex2D( _GrabTexture, float2((node_4975+sceneUVs.r),sceneUVs.g)).rgb+tex2D( _GrabTexture, float2(sceneUVs.r,(node_4975+sceneUVs.g))).rgb+tex2D( _GrabTexture, sceneUVs.rg).rgb+tex2D( _GrabTexture, float2((sceneUVs.r-node_4975),sceneUVs.g)).rgb+tex2D( _GrabTexture, float2(sceneUVs.r,(sceneUVs.g-node_4975))).rgb);
                float3 emissive = (node_8255*0.2);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu switch 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _CameraDepthTexture;
            uniform float _Offset;
            uniform float _Distance;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 projPos : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float node_4975 = (saturate((sceneZ-partZ)/_Distance)*_Offset);
                float3 node_8255 = (tex2D( _GrabTexture, float2((node_4975+sceneUVs.r),sceneUVs.g)).rgb+tex2D( _GrabTexture, float2(sceneUVs.r,(node_4975+sceneUVs.g))).rgb+tex2D( _GrabTexture, sceneUVs.rg).rgb+tex2D( _GrabTexture, float2((sceneUVs.r-node_4975),sceneUVs.g)).rgb+tex2D( _GrabTexture, float2(sceneUVs.r,(sceneUVs.g-node_4975))).rgb);
                o.Emission = (node_8255*0.2);
                
                float3 diffColor = float3(0,0,0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0, specColor, specularMonochrome );
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
