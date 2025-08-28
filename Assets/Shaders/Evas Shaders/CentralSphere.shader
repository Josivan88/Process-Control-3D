// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33261,y:32711,varname:node_3138,prsc:2|emission-8797-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32368,y:32534,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Slider,id:1309,x:31725,y:32764,ptovrint:False,ptlb:MaskIntensity,ptin:_MaskIntensity,varname:node_1309,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Multiply,id:738,x:32600,y:32831,varname:node_738,prsc:2|A-7241-RGB,B-1320-OUT,C-3793-RGB;n:type:ShaderForge.SFN_Tex2d,id:3793,x:32371,y:32967,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_3793,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-2334-OUT;n:type:ShaderForge.SFN_TexCoord,id:6077,x:31662,y:32929,varname:node_6077,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:2334,x:32170,y:32967,varname:node_2334,prsc:2|A-6077-U,B-518-OUT;n:type:ShaderForge.SFN_Time,id:7457,x:31354,y:33152,varname:node_7457,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4928,x:31641,y:33194,varname:node_4928,prsc:2|A-7457-T,B-1555-OUT;n:type:ShaderForge.SFN_Slider,id:1555,x:31244,y:33323,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_1555,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:2;n:type:ShaderForge.SFN_Add,id:1855,x:32814,y:32848,varname:node_1855,prsc:2|A-738-OUT,B-3582-OUT,C-7053-OUT;n:type:ShaderForge.SFN_Multiply,id:3582,x:32617,y:33140,varname:node_3582,prsc:2|A-6867-OUT,B-9039-OUT,C-7241-RGB;n:type:ShaderForge.SFN_Fresnel,id:6867,x:32361,y:33181,varname:node_6867,prsc:2|EXP-8218-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8218,x:32170,y:33263,ptovrint:False,ptlb:Fresnel2,ptin:_Fresnel2,varname:node_8218,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.5;n:type:ShaderForge.SFN_Slider,id:9039,x:32285,y:33401,ptovrint:False,ptlb:Fresnel2Intensity,ptin:_Fresnel2Intensity,varname:node_9039,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.9,max:1;n:type:ShaderForge.SFN_Multiply,id:8797,x:33048,y:32811,varname:node_8797,prsc:2|A-1855-OUT,B-5670-OUT;n:type:ShaderForge.SFN_Slider,id:5670,x:32635,y:32685,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_5670,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:5;n:type:ShaderForge.SFN_Tex2d,id:1602,x:31803,y:32565,ptovrint:False,ptlb:MaskTexture,ptin:_MaskTexture,varname:node_1602,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Power,id:1320,x:32259,y:32751,varname:node_1320,prsc:2|VAL-1602-RGB,EXP-1309-OUT;n:type:ShaderForge.SFN_Vector1,id:7053,x:32814,y:33052,varname:node_7053,prsc:2,v1:0.01;n:type:ShaderForge.SFN_Add,id:518,x:31938,y:33044,varname:node_518,prsc:2|A-6077-V,B-4928-OUT;proporder:7241-5670-3793-1602-1309-1555-8218-9039;pass:END;sub:END;*/

Shader "Shader Forge/CentralSphere" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _Intensity ("Intensity", Range(0, 5)) = 1
        _Texture ("Texture", 2D) = "white" {}
        _MaskTexture ("MaskTexture", 2D) = "white" {}
        _MaskIntensity ("MaskIntensity", Range(0, 2)) = 1
        _Speed ("Speed", Range(0, 2)) = 0.5
        _Fresnel2 ("Fresnel2", Float ) = 1.5
        _Fresnel2Intensity ("Fresnel2Intensity", Range(0, 1)) = 0.9
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform sampler2D _MaskTexture; uniform float4 _MaskTexture_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _MaskIntensity)
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
                UNITY_DEFINE_INSTANCED_PROP( float, _Fresnel2)
                UNITY_DEFINE_INSTANCED_PROP( float, _Fresnel2Intensity)
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
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float4 _MaskTexture_var = tex2D(_MaskTexture,TRANSFORM_TEX(i.uv0, _MaskTexture));
                float _MaskIntensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MaskIntensity );
                float4 node_7457 = _Time;
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float2 node_2334 = float2(i.uv0.r,(i.uv0.g+(node_7457.g*_Speed_var)));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_2334, _Texture));
                float _Fresnel2_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Fresnel2 );
                float _Fresnel2Intensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Fresnel2Intensity );
                float _Intensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Intensity );
                float3 emissive = (((_Color_var.rgb*pow(_MaskTexture_var.rgb,_MaskIntensity_var)*_Texture_var.rgb)+(pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel2_var)*_Fresnel2Intensity_var*_Color_var.rgb)+0.01)*_Intensity_var);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
