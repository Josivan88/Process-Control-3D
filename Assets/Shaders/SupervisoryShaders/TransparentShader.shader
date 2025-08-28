// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|diff-6343-OUT,spec-5249-OUT,gloss-1813-OUT,normal-5964-RGB,emission-5904-OUT,alpha-7526-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:32016,y:32412,varname:node_6343,prsc:2|A-7736-RGB,B-5763-OUT;n:type:ShaderForge.SFN_Color,id:6665,x:31443,y:32426,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31804,y:32306,ptovrint:True,ptlb:Difuse,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:32143,y:32821,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:358,x:31986,y:32599,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:5;n:type:ShaderForge.SFN_Slider,id:1813,x:31986,y:32704,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8,max:1;n:type:ShaderForge.SFN_Time,id:9757,x:30870,y:32915,varname:node_9757,prsc:2;n:type:ShaderForge.SFN_Sin,id:5038,x:31077,y:32915,varname:node_5038,prsc:2|IN-9757-TTR;n:type:ShaderForge.SFN_Add,id:4378,x:31260,y:32915,varname:node_4378,prsc:2|A-5038-OUT,B-8489-OUT;n:type:ShaderForge.SFN_Vector1,id:8489,x:31077,y:33072,varname:node_8489,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:3795,x:31441,y:32915,varname:node_3795,prsc:2|A-4378-OUT,B-1071-OUT;n:type:ShaderForge.SFN_Vector1,id:1071,x:31260,y:33072,varname:node_1071,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Color,id:4820,x:31292,y:33266,ptovrint:False,ptlb:Default,ptin:_Default,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:5099,x:31292,y:33453,ptovrint:False,ptlb:SelectedColor,ptin:_SelectedColor,varname:_Default_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.0592016,c2:0.5,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:197,x:32052,y:33282,varname:node_197,prsc:2|A-4820-RGB,B-2940-OUT,T-3795-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:5834,x:32144,y:33551,ptovrint:False,ptlb:Selected,ptin:_Selected,varname:node_5834,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_Color,id:2675,x:31292,y:33636,ptovrint:False,ptlb:MinAllert,ptin:_MinAllert,varname:_SelectedColor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:5143,x:31292,y:33812,ptovrint:False,ptlb:MaxAllert,ptin:_MaxAllert,varname:_MinAllert_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:5904,x:32260,y:33282,varname:node_5904,prsc:2|A-197-OUT,B-5834-OUT;n:type:ShaderForge.SFN_Lerp,id:6985,x:31628,y:33517,varname:node_6985,prsc:2|A-5099-RGB,B-2675-RGB,T-5917-OUT;n:type:ShaderForge.SFN_Lerp,id:2940,x:31870,y:33648,varname:node_2940,prsc:2|A-6985-OUT,B-5143-RGB,T-5518-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3330,x:31292,y:34036,ptovrint:False,ptlb:DangerLevel,ptin:_DangerLevel,varname:node_3330,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:5917,x:31854,y:33963,varname:node_5917,prsc:2|A-5518-OUT,B-8588-OUT;n:type:ShaderForge.SFN_Vector1,id:8588,x:31633,y:34098,varname:node_8588,prsc:2,v1:2;n:type:ShaderForge.SFN_Lerp,id:5763,x:31804,y:32537,varname:node_5763,prsc:2|A-6665-RGB,B-5904-OUT,T-7754-OUT;n:type:ShaderForge.SFN_Multiply,id:7754,x:31682,y:32694,varname:node_7754,prsc:2|A-3795-OUT,B-5834-OUT;n:type:ShaderForge.SFN_OneMinus,id:5518,x:31587,y:33902,varname:node_5518,prsc:2|IN-3330-OUT;n:type:ShaderForge.SFN_Fresnel,id:5734,x:30826,y:32497,varname:node_5734,prsc:2|EXP-9049-OUT;n:type:ShaderForge.SFN_Vector1,id:9049,x:30571,y:32512,varname:node_9049,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:6700,x:31114,y:32544,varname:node_6700,prsc:2|A-5734-OUT,B-4916-OUT;n:type:ShaderForge.SFN_Slider,id:4916,x:30485,y:32787,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_4916,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:2;n:type:ShaderForge.SFN_Add,id:7526,x:31353,y:32630,varname:node_7526,prsc:2|A-6700-OUT,B-303-OUT;n:type:ShaderForge.SFN_Multiply,id:5249,x:32342,y:32646,varname:node_5249,prsc:2|A-358-OUT,B-7526-OUT;n:type:ShaderForge.SFN_Multiply,id:303,x:30973,y:32736,varname:node_303,prsc:2|A-4916-OUT,B-3422-OUT;n:type:ShaderForge.SFN_Vector1,id:3422,x:30642,y:32874,varname:node_3422,prsc:2,v1:0.4;proporder:6665-7736-5964-358-1813-4916-5834-3330-4820-5099-2675-5143;pass:END;sub:END;*/

Shader "Shader Forge/TransparentShader" {
    Properties {
        _Color ("Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _MainTex ("Difuse", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Specular ("Specular", Range(0, 5)) = 1
        _Gloss ("Gloss", Range(0, 1)) = 0.8
        _Opacity ("Opacity", Range(0, 2)) = 0.5
        [MaterialToggle] _Selected ("Selected", Float ) = 0
        _DangerLevel ("DangerLevel", Float ) = 1
        _Default ("Default", Color) = (0,0,0,1)
        _SelectedColor ("SelectedColor", Color) = (0.0592016,0.5,0,1)
        _MinAllert ("MinAllert", Color) = (1,1,0,1)
        _MaxAllert ("MaxAllert", Color) = (1,0,0,1)
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Specular)
                UNITY_DEFINE_INSTANCED_PROP( float, _Gloss)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Default)
                UNITY_DEFINE_INSTANCED_PROP( float4, _SelectedColor)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _Selected)
                UNITY_DEFINE_INSTANCED_PROP( float4, _MinAllert)
                UNITY_DEFINE_INSTANCED_PROP( float4, _MaxAllert)
                UNITY_DEFINE_INSTANCED_PROP( float, _DangerLevel)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                UNITY_FOG_COORDS(7)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD8;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float _Gloss_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Gloss );
                float gloss = _Gloss_var;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float _Specular_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Specular );
                float _Opacity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity );
                float node_7526 = ((pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0)*_Opacity_var)+(_Opacity_var*0.4));
                float node_5249 = (_Specular_var*node_7526);
                float3 specularColor = float3(node_5249,node_5249,node_5249);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 indirectSpecular = (gi.indirect.specular)*specularColor;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float4 _Default_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Default );
                float4 _SelectedColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SelectedColor );
                float4 _MinAllert_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MinAllert );
                float _DangerLevel_var = UNITY_ACCESS_INSTANCED_PROP( Props, _DangerLevel );
                float node_5518 = (1.0 - _DangerLevel_var);
                float4 _MaxAllert_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MaxAllert );
                float4 node_9757 = _Time;
                float node_3795 = ((sin(node_9757.a)+1.5)*0.5);
                float _Selected_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Selected );
                float3 node_5904 = (lerp(_Default_var.rgb,lerp(lerp(_SelectedColor_var.rgb,_MinAllert_var.rgb,(node_5518*2.0)),_MaxAllert_var.rgb,node_5518),node_3795)*_Selected_var);
                float3 diffuseColor = (_MainTex_var.rgb*lerp(_Color_var.rgb,node_5904,(node_3795*_Selected_var)));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = node_5904;
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,node_7526);
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Specular)
                UNITY_DEFINE_INSTANCED_PROP( float, _Gloss)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Default)
                UNITY_DEFINE_INSTANCED_PROP( float4, _SelectedColor)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _Selected)
                UNITY_DEFINE_INSTANCED_PROP( float4, _MinAllert)
                UNITY_DEFINE_INSTANCED_PROP( float4, _MaxAllert)
                UNITY_DEFINE_INSTANCED_PROP( float, _DangerLevel)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float _Gloss_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Gloss );
                float gloss = _Gloss_var;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float _Specular_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Specular );
                float _Opacity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity );
                float node_7526 = ((pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0)*_Opacity_var)+(_Opacity_var*0.4));
                float node_5249 = (_Specular_var*node_7526);
                float3 specularColor = float3(node_5249,node_5249,node_5249);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float4 _Default_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Default );
                float4 _SelectedColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SelectedColor );
                float4 _MinAllert_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MinAllert );
                float _DangerLevel_var = UNITY_ACCESS_INSTANCED_PROP( Props, _DangerLevel );
                float node_5518 = (1.0 - _DangerLevel_var);
                float4 _MaxAllert_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MaxAllert );
                float4 node_9757 = _Time;
                float node_3795 = ((sin(node_9757.a)+1.5)*0.5);
                float _Selected_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Selected );
                float3 node_5904 = (lerp(_Default_var.rgb,lerp(lerp(_SelectedColor_var.rgb,_MinAllert_var.rgb,(node_5518*2.0)),_MaxAllert_var.rgb,node_5518),node_3795)*_Selected_var);
                float3 diffuseColor = (_MainTex_var.rgb*lerp(_Color_var.rgb,node_5904,(node_3795*_Selected_var)));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * node_7526,0);
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
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Specular)
                UNITY_DEFINE_INSTANCED_PROP( float, _Gloss)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Default)
                UNITY_DEFINE_INSTANCED_PROP( float4, _SelectedColor)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _Selected)
                UNITY_DEFINE_INSTANCED_PROP( float4, _MinAllert)
                UNITY_DEFINE_INSTANCED_PROP( float4, _MaxAllert)
                UNITY_DEFINE_INSTANCED_PROP( float, _DangerLevel)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _Default_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Default );
                float4 _SelectedColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SelectedColor );
                float4 _MinAllert_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MinAllert );
                float _DangerLevel_var = UNITY_ACCESS_INSTANCED_PROP( Props, _DangerLevel );
                float node_5518 = (1.0 - _DangerLevel_var);
                float4 _MaxAllert_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MaxAllert );
                float4 node_9757 = _Time;
                float node_3795 = ((sin(node_9757.a)+1.5)*0.5);
                float _Selected_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Selected );
                float3 node_5904 = (lerp(_Default_var.rgb,lerp(lerp(_SelectedColor_var.rgb,_MinAllert_var.rgb,(node_5518*2.0)),_MaxAllert_var.rgb,node_5518),node_3795)*_Selected_var);
                o.Emission = node_5904;
                
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float3 diffColor = (_MainTex_var.rgb*lerp(_Color_var.rgb,node_5904,(node_3795*_Selected_var)));
                float _Specular_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Specular );
                float _Opacity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity );
                float node_7526 = ((pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0)*_Opacity_var)+(_Opacity_var*0.4));
                float node_5249 = (_Specular_var*node_7526);
                float3 specColor = float3(node_5249,node_5249,node_5249);
                float _Gloss_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Gloss );
                float roughness = 1.0 - _Gloss_var;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
