// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:True,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1,x:35014,y:32727,varname:node_1,prsc:2|diff-257-OUT,spec-134-OUT,normal-12-RGB,emission-271-OUT,transm-925-OUT,clip-11-A,voffset-605-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:34158,y:32446,ptovrint:False,ptlb:Difuse,ptin:_Difuse,varname:node_541,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:3,x:34158,y:32258,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7430,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:4,x:34346,y:32331,varname:node_4,prsc:2|A-3-RGB,B-2-RGB;n:type:ShaderForge.SFN_Tex2d,id:11,x:33986,y:32631,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:node_3013,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:12,x:33986,y:32446,ptovrint:False,ptlb:Bump,ptin:_Bump,varname:node_7362,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Color,id:80,x:34150,y:32638,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:node_4799,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1617647,c2:0.1617647,c3:0.1617647,c4:1;n:type:ShaderForge.SFN_Time,id:99,x:32798,y:32829,varname:node_99,prsc:2;n:type:ShaderForge.SFN_Sin,id:101,x:34368,y:33497,varname:node_101,prsc:2|IN-108-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:102,x:34344,y:33427,ptovrint:False,ptlb:Move,ptin:_Move,varname:node_5260,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True;n:type:ShaderForge.SFN_Multiply,id:103,x:33980,y:33497,varname:node_103,prsc:2|A-177-OUT,B-105-RGB;n:type:ShaderForge.SFN_VertexColor,id:105,x:33563,y:33482,varname:node_105,prsc:2;n:type:ShaderForge.SFN_Add,id:108,x:34186,y:33497,varname:node_108,prsc:2|A-103-OUT,B-198-OUT;n:type:ShaderForge.SFN_Multiply,id:134,x:34450,y:32584,varname:node_134,prsc:2|A-80-RGB,B-11-RGB;n:type:ShaderForge.SFN_FragmentPosition,id:162,x:32798,y:33167,varname:node_162,prsc:2;n:type:ShaderForge.SFN_Multiply,id:177,x:33005,y:32984,varname:node_177,prsc:2|A-99-T,B-1093-OUT;n:type:ShaderForge.SFN_Vector1,id:196,x:32798,y:33310,varname:node_196,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:198,x:33103,y:33158,varname:node_198,prsc:2|A-162-XYZ,B-196-OUT;n:type:ShaderForge.SFN_Multiply,id:257,x:34595,y:32379,varname:node_257,prsc:2|A-4-OUT,B-105-RGB;n:type:ShaderForge.SFN_Multiply,id:271,x:34712,y:32627,varname:node_271,prsc:2|A-257-OUT,B-274-OUT;n:type:ShaderForge.SFN_Vector1,id:274,x:34450,y:32725,varname:node_274,prsc:2,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:288,x:32491,y:33514,ptovrint:False,ptlb:Agitation,ptin:_Agitation,varname:node_6720,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:605,x:34656,y:33295,varname:node_605,prsc:2|A-865-OUT,B-105-RGB,C-102-OUT,D-1025-OUT;n:type:ShaderForge.SFN_Multiply,id:801,x:34219,y:33710,varname:node_801,prsc:2|A-811-OUT,B-823-OUT;n:type:ShaderForge.SFN_Multiply,id:803,x:34219,y:33841,varname:node_803,prsc:2|A-809-OUT,B-823-OUT;n:type:ShaderForge.SFN_Multiply,id:805,x:34219,y:33986,varname:node_805,prsc:2|A-807-OUT,B-823-OUT;n:type:ShaderForge.SFN_Add,id:807,x:33980,y:33986,varname:node_807,prsc:2|A-817-OUT,B-821-OUT;n:type:ShaderForge.SFN_Add,id:809,x:33980,y:33842,varname:node_809,prsc:2|A-815-OUT,B-821-OUT;n:type:ShaderForge.SFN_Add,id:811,x:33980,y:33711,varname:node_811,prsc:2|A-813-OUT,B-821-OUT;n:type:ShaderForge.SFN_Sin,id:813,x:33744,y:33711,varname:node_813,prsc:2|IN-829-OUT;n:type:ShaderForge.SFN_Sin,id:815,x:33744,y:33842,varname:node_815,prsc:2|IN-827-OUT;n:type:ShaderForge.SFN_Sin,id:817,x:33744,y:33986,varname:node_817,prsc:2|IN-819-OUT;n:type:ShaderForge.SFN_Add,id:819,x:33548,y:33986,varname:node_819,prsc:2|A-837-OUT,B-177-OUT,C-1067-OUT;n:type:ShaderForge.SFN_Vector1,id:821,x:33548,y:34143,varname:node_821,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:823,x:33548,y:34209,varname:node_823,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:825,x:33548,y:34273,varname:node_825,prsc:2,v1:0.33;n:type:ShaderForge.SFN_Add,id:827,x:33548,y:33842,varname:node_827,prsc:2|A-835-OUT,B-177-OUT,C-1067-OUT;n:type:ShaderForge.SFN_Add,id:829,x:33548,y:33711,varname:node_829,prsc:2|A-831-OUT,B-177-OUT,C-1067-OUT;n:type:ShaderForge.SFN_Multiply,id:831,x:33139,y:33721,varname:node_831,prsc:2|A-162-X,B-833-OUT;n:type:ShaderForge.SFN_Vector1,id:833,x:32801,y:33695,varname:node_833,prsc:2,v1:50;n:type:ShaderForge.SFN_Multiply,id:835,x:33139,y:33872,varname:node_835,prsc:2|A-162-Y,B-833-OUT;n:type:ShaderForge.SFN_Multiply,id:837,x:33139,y:34017,varname:node_837,prsc:2|A-162-Z,B-833-OUT;n:type:ShaderForge.SFN_Append,id:847,x:34442,y:33735,varname:node_847,prsc:2|A-801-OUT,B-803-OUT;n:type:ShaderForge.SFN_Append,id:864,x:34627,y:33735,varname:node_864,prsc:2|A-847-OUT,B-805-OUT;n:type:ShaderForge.SFN_OneMinus,id:865,x:34796,y:33735,varname:node_865,prsc:2|IN-864-OUT;n:type:ShaderForge.SFN_Slider,id:925,x:34293,y:32883,ptovrint:False,ptlb:Leaf_Scattering,ptin:_Leaf_Scattering,varname:node_4166,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector1,id:1024,x:32491,y:33598,varname:node_1024,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:1025,x:32740,y:33499,varname:node_1025,prsc:2|A-288-OUT,B-1024-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1067,x:33321,y:34143,ptovrint:False,ptlb:Phase,ptin:_Phase,varname:node_8805,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:1068,x:32569,y:33046,ptovrint:False,ptlb:Frequency,ptin:_Frequency,varname:node_6357,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:1093,x:32798,y:32984,varname:node_1093,prsc:2|A-1094-OUT,B-1068-OUT;n:type:ShaderForge.SFN_Vector1,id:1094,x:32569,y:32946,varname:node_1094,prsc:2,v1:10;proporder:3-2-80-12-11-925-102-288-1068-1067;pass:END;sub:END;*/

Shader "Shader Forge/PerfectLeave_SF" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Difuse ("Difuse", 2D) = "white" {}
        _Specular ("Specular", Color) = (0.1617647,0.1617647,0.1617647,1)
        _Bump ("Bump", 2D) = "bump" {}
        _Alpha ("Alpha", 2D) = "white" {}
        _Leaf_Scattering ("Leaf_Scattering", Range(0, 1)) = 0
        [MaterialToggle] _Move ("Move", Float ) = 1
        _Agitation ("Agitation", Float ) = 0.1
        _Frequency ("Frequency", Float ) = 0.2
        _Phase ("Phase", Float ) = 0
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu switch 
            #pragma target 3.0
            uniform sampler2D _Difuse; uniform float4 _Difuse_ST;
            uniform float4 _Color;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            uniform sampler2D _Bump; uniform float4 _Bump_ST;
            uniform float4 _Specular;
            uniform fixed _Move;
            uniform float _Agitation;
            uniform float _Leaf_Scattering;
            uniform float _Phase;
            uniform float _Frequency;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
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
                float node_833 = 50.0;
                float4 node_99 = _Time;
                float node_177 = (node_99.g*(10.0*_Frequency));
                float node_821 = 1.0;
                float node_823 = 0.5;
                v.vertex.xyz += ((1.0 - float3(float2(((sin(((mul(unity_ObjectToWorld, v.vertex).r*node_833)+node_177+_Phase))+node_821)*node_823),((sin(((mul(unity_ObjectToWorld, v.vertex).g*node_833)+node_177+_Phase))+node_821)*node_823)),((sin(((mul(unity_ObjectToWorld, v.vertex).b*node_833)+node_177+_Phase))+node_821)*node_823)))*o.vertexColor.rgb*_Move*(_Agitation*0.1));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Bump_var = UnpackNormal(tex2D(_Bump,TRANSFORM_TEX(i.uv0, _Bump)));
                float3 normalLocal = _Bump_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _Alpha_var = tex2D(_Alpha,TRANSFORM_TEX(i.uv0, _Alpha));
                clip(_Alpha_var.a - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                UNITY_LIGHT_ATTENUATION(attenuation,i, i.posWorld.xyz);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.5;
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
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = (_Specular.rgb*_Alpha_var.rgb);
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*normTerm*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float3 forwardLight = max(0.0, NdotL );
                float3 backLight = max(0.0, -NdotL ) * float3(_Leaf_Scattering,_Leaf_Scattering,_Leaf_Scattering);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = (forwardLight+backLight) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float4 _Difuse_var = tex2D(_Difuse,TRANSFORM_TEX(i.uv0, _Difuse));
                float3 node_257 = ((_Color.rgb*_Difuse_var.rgb)*i.vertexColor.rgb);
                float3 diffuseColor = node_257;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (node_257*0.1);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu switch 
            #pragma target 3.0
            uniform sampler2D _Difuse; uniform float4 _Difuse_ST;
            uniform float4 _Color;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            uniform sampler2D _Bump; uniform float4 _Bump_ST;
            uniform float4 _Specular;
            uniform fixed _Move;
            uniform float _Agitation;
            uniform float _Leaf_Scattering;
            uniform float _Phase;
            uniform float _Frequency;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float node_833 = 50.0;
                float4 node_99 = _Time;
                float node_177 = (node_99.g*(10.0*_Frequency));
                float node_821 = 1.0;
                float node_823 = 0.5;
                v.vertex.xyz += ((1.0 - float3(float2(((sin(((mul(unity_ObjectToWorld, v.vertex).r*node_833)+node_177+_Phase))+node_821)*node_823),((sin(((mul(unity_ObjectToWorld, v.vertex).g*node_833)+node_177+_Phase))+node_821)*node_823)),((sin(((mul(unity_ObjectToWorld, v.vertex).b*node_833)+node_177+_Phase))+node_821)*node_823)))*o.vertexColor.rgb*_Move*(_Agitation*0.1));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Bump_var = UnpackNormal(tex2D(_Bump,TRANSFORM_TEX(i.uv0, _Bump)));
                float3 normalLocal = _Bump_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _Alpha_var = tex2D(_Alpha,TRANSFORM_TEX(i.uv0, _Alpha));
                clip(_Alpha_var.a - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                UNITY_LIGHT_ATTENUATION(attenuation,i, i.posWorld.xyz);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = (_Specular.rgb*_Alpha_var.rgb);
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*normTerm*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float3 forwardLight = max(0.0, NdotL );
                float3 backLight = max(0.0, -NdotL ) * float3(_Leaf_Scattering,_Leaf_Scattering,_Leaf_Scattering);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = (forwardLight+backLight) * attenColor;
                float4 _Difuse_var = tex2D(_Difuse,TRANSFORM_TEX(i.uv0, _Difuse));
                float3 node_257 = ((_Color.rgb*_Difuse_var.rgb)*i.vertexColor.rgb);
                float3 diffuseColor = node_257;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu switch 
            #pragma target 3.0
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            uniform fixed _Move;
            uniform float _Agitation;
            uniform float _Phase;
            uniform float _Frequency;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                float2 uv2 : TEXCOORD3;
                float4 posWorld : TEXCOORD4;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                float node_833 = 50.0;
                float4 node_99 = _Time;
                float node_177 = (node_99.g*(10.0*_Frequency));
                float node_821 = 1.0;
                float node_823 = 0.5;
                v.vertex.xyz += ((1.0 - float3(float2(((sin(((mul(unity_ObjectToWorld, v.vertex).r*node_833)+node_177+_Phase))+node_821)*node_823),((sin(((mul(unity_ObjectToWorld, v.vertex).g*node_833)+node_177+_Phase))+node_821)*node_823)),((sin(((mul(unity_ObjectToWorld, v.vertex).b*node_833)+node_177+_Phase))+node_821)*node_823)))*o.vertexColor.rgb*_Move*(_Agitation*0.1));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _Alpha_var = tex2D(_Alpha,TRANSFORM_TEX(i.uv0, _Alpha));
                clip(_Alpha_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu switch 
            #pragma target 3.0
            uniform sampler2D _Difuse; uniform float4 _Difuse_ST;
            uniform float4 _Color;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            uniform float4 _Specular;
            uniform fixed _Move;
            uniform float _Agitation;
            uniform float _Phase;
            uniform float _Frequency;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                float node_833 = 50.0;
                float4 node_99 = _Time;
                float node_177 = (node_99.g*(10.0*_Frequency));
                float node_821 = 1.0;
                float node_823 = 0.5;
                v.vertex.xyz += ((1.0 - float3(float2(((sin(((mul(unity_ObjectToWorld, v.vertex).r*node_833)+node_177+_Phase))+node_821)*node_823),((sin(((mul(unity_ObjectToWorld, v.vertex).g*node_833)+node_177+_Phase))+node_821)*node_823)),((sin(((mul(unity_ObjectToWorld, v.vertex).b*node_833)+node_177+_Phase))+node_821)*node_823)))*o.vertexColor.rgb*_Move*(_Agitation*0.1));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : SV_Target {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _Difuse_var = tex2D(_Difuse,TRANSFORM_TEX(i.uv0, _Difuse));
                float3 node_257 = ((_Color.rgb*_Difuse_var.rgb)*i.vertexColor.rgb);
                o.Emission = (node_257*0.1);
                
                float3 diffColor = node_257;
                float4 _Alpha_var = tex2D(_Alpha,TRANSFORM_TEX(i.uv0, _Alpha));
                float3 specColor = (_Specular.rgb*_Alpha_var.rgb);
                o.Albedo = diffColor + specColor * 0.125; // No gloss connected. Assume it's 0.5
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
