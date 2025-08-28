// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-7102-OUT,clip-8620-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32124,y:32671,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Fresnel,id:776,x:32124,y:32871,varname:node_776,prsc:2|EXP-3437-OUT;n:type:ShaderForge.SFN_Slider,id:3437,x:31703,y:32945,ptovrint:False,ptlb:FresnelPower,ptin:_FresnelPower,varname:node_3437,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:3;n:type:ShaderForge.SFN_TexCoord,id:745,x:31077,y:32953,varname:node_745,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Sin,id:3408,x:31703,y:33061,varname:node_3408,prsc:2|IN-508-OUT;n:type:ShaderForge.SFN_Multiply,id:886,x:31348,y:32983,varname:node_886,prsc:2|A-745-U,B-1046-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1046,x:31227,y:32877,ptovrint:False,ptlb:XTilling,ptin:_XTilling,varname:node_1046,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:400;n:type:ShaderForge.SFN_ValueProperty,id:4789,x:31102,y:33272,ptovrint:False,ptlb:YTilling,ptin:_YTilling,varname:_node_1046_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:200;n:type:ShaderForge.SFN_Multiply,id:5976,x:31411,y:33186,varname:node_5976,prsc:2|A-745-V,B-4789-OUT;n:type:ShaderForge.SFN_Sin,id:9712,x:31703,y:33241,varname:node_9712,prsc:2|IN-5976-OUT;n:type:ShaderForge.SFN_Multiply,id:8696,x:31978,y:33056,varname:node_8696,prsc:2|A-3408-OUT,B-9712-OUT;n:type:ShaderForge.SFN_Multiply,id:4137,x:32322,y:32909,varname:node_4137,prsc:2|A-776-OUT,B-8696-OUT;n:type:ShaderForge.SFN_Add,id:508,x:31537,y:32957,varname:node_508,prsc:2|A-886-OUT,B-2388-OUT;n:type:ShaderForge.SFN_Time,id:6473,x:31095,y:32559,varname:node_6473,prsc:2;n:type:ShaderForge.SFN_Add,id:8620,x:32507,y:32983,varname:node_8620,prsc:2|A-4137-OUT,B-6722-OUT;n:type:ShaderForge.SFN_Slider,id:6722,x:32162,y:33162,ptovrint:False,ptlb:OpacityBust,ptin:_OpacityBust,varname:node_6722,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.1,cur:0.1,max:1;n:type:ShaderForge.SFN_Slider,id:466,x:30950,y:32727,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_466,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5,max:100;n:type:ShaderForge.SFN_Multiply,id:2388,x:31337,y:32624,varname:node_2388,prsc:2|A-6473-T,B-466-OUT;n:type:ShaderForge.SFN_Multiply,id:7102,x:32427,y:32676,varname:node_7102,prsc:2|A-8515-OUT,B-7241-RGB;n:type:ShaderForge.SFN_Slider,id:8515,x:31967,y:32562,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_8515,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:5;proporder:7241-8515-3437-1046-4789-6722-466;pass:END;sub:END;*/

Shader "Shader Forge/SortedSquares" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _Intensity ("Intensity", Range(0, 5)) = 1
        _FresnelPower ("FresnelPower", Range(0, 3)) = 1
        _XTilling ("XTilling", Float ) = 400
        _YTilling ("YTilling", Float ) = 200
        _OpacityBust ("OpacityBust", Range(-0.1, 1)) = 0.1
        _Speed ("Speed", Range(0, 100)) = 5
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
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _FresnelPower)
                UNITY_DEFINE_INSTANCED_PROP( float, _XTilling)
                UNITY_DEFINE_INSTANCED_PROP( float, _YTilling)
                UNITY_DEFINE_INSTANCED_PROP( float, _OpacityBust)
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
                UNITY_DEFINE_INSTANCED_PROP( float, _Intensity)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float _FresnelPower_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelPower );
                float _XTilling_var = UNITY_ACCESS_INSTANCED_PROP( Props, _XTilling );
                float4 node_6473 = _Time;
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float _YTilling_var = UNITY_ACCESS_INSTANCED_PROP( Props, _YTilling );
                float _OpacityBust_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OpacityBust );
                clip(((pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelPower_var)*(sin(((i.uv0.r*_XTilling_var)+(node_6473.g*_Speed_var)))*sin((i.uv0.g*_YTilling_var))))+_OpacityBust_var) - 0.5);
////// Lighting:
////// Emissive:
                float _Intensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Intensity );
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float3 emissive = (_Intensity_var*_Color_var.rgb);
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
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _FresnelPower)
                UNITY_DEFINE_INSTANCED_PROP( float, _XTilling)
                UNITY_DEFINE_INSTANCED_PROP( float, _YTilling)
                UNITY_DEFINE_INSTANCED_PROP( float, _OpacityBust)
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float _FresnelPower_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelPower );
                float _XTilling_var = UNITY_ACCESS_INSTANCED_PROP( Props, _XTilling );
                float4 node_6473 = _Time;
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float _YTilling_var = UNITY_ACCESS_INSTANCED_PROP( Props, _YTilling );
                float _OpacityBust_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OpacityBust );
                clip(((pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelPower_var)*(sin(((i.uv0.r*_XTilling_var)+(node_6473.g*_Speed_var)))*sin((i.uv0.g*_YTilling_var))))+_OpacityBust_var) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
