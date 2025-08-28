// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-7610-RGB,olwid-7785-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:3130,x:30372,y:32559,varname:node_3130,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:7712,x:32046,y:32881,varname:node_7712,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:7649,x:32263,y:32764,varname:node_7649,prsc:2|A-7136-OUT,B-7712-V;n:type:ShaderForge.SFN_Tex2d,id:7610,x:32472,y:32764,ptovrint:False,ptlb:GradientMap,ptin:_GradientMap,varname:node_1335,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1d775ca7130832545bf60f47be4a9e47,ntxv:0,isnm:False|UVIN-7649-OUT;n:type:ShaderForge.SFN_Clamp,id:7136,x:32046,y:32722,varname:node_7136,prsc:2|IN-6852-OUT,MIN-4208-OUT,MAX-861-OUT;n:type:ShaderForge.SFN_Vector1,id:4208,x:31855,y:32789,varname:node_4208,prsc:2,v1:0.001;n:type:ShaderForge.SFN_Vector1,id:861,x:31855,y:32866,varname:node_861,prsc:2,v1:0.999;n:type:ShaderForge.SFN_Code,id:6852,x:30873,y:32707,varname:node_6852,prsc:2,code:ZgBsAG8AYQB0ACAASQBuAHYARAAwACAAPQAgADEALwAoACgAWAAtAFAAMAAuAHgAKQAqACgAWAAtAFAAMAAuAHgAKQArACgAWQAtAFAAMAAuAHkAKQAqACgAWQAtAFAAMAAuAHkAKQArACgAWgAtAFAAMAAuAHoAKQAqACgAWgAtAFAAMAAuAHoAKQApADsACgBmAGwAbwBhAHQAIABJAG4AdgBEADEAIAA9ACAAMQAvACgAKABYAC0AUAAxAC4AeAApACoAKABYAC0AUAAxAC4AeAApACsAKABZAC0AUAAxAC4AeQApACoAKABZAC0AUAAxAC4AeQApACsAKABaAC0AUAAxAC4AegApACoAKABaAC0AUAAxAC4AegApACkAOwAKAGYAbABvAGEAdAAgAEkAbgB2AEQAMgAgAD0AIAAxAC8AKAAoAFgALQBQADIALgB4ACkAKgAoAFgALQBQADIALgB4ACkAKwAoAFkALQBQADIALgB5ACkAKgAoAFkALQBQADIALgB5ACkAKwAoAFoALQBQADIALgB6ACkAKgAoAFoALQBQADIALgB6ACkAKQA7AAoAZgBsAG8AYQB0ACAASQBuAHYARAAzACAAPQAgADEALwAoACgAWAAtAFAAMwAuAHgAKQAqACgAWAAtAFAAMwAuAHgAKQArACgAWQAtAFAAMwAuAHkAKQAqACgAWQAtAFAAMwAuAHkAKQArACgAWgAtAFAAMwAuAHoAKQAqACgAWgAtAFAAMwAuAHoAKQApADsACgByAGUAdAB1AHIAbgAgACgAUAAwAC4AdwAqAEkAbgB2AEQAMAArAFAAMQAuAHcAKgBJAG4AdgBEADEAKwBQADIALgB3ACoASQBuAHYARAAyACsAUAAzAC4AdwAqAEkAbgB2AEQAMwApAC8AKABJAG4AdgBEADAAKwBJAG4AdgBEADEAKwBJAG4AdgBEADIAKwBJAG4AdgBEADMAKQA7AA==,output:0,fname:Interpolator,width:663,height:212,input:0,input:0,input:0,input:3,input:3,input:3,input:3,input_1_label:X,input_2_label:Y,input_3_label:Z,input_4_label:P0,input_5_label:P1,input_6_label:P2,input_7_label:P3|A-3130-X,B-3130-Y,C-3130-Z,D-4109-OUT,E-6449-OUT,F-4359-OUT,G-2827-OUT;n:type:ShaderForge.SFN_Vector4Property,id:6361,x:30168,y:32715,ptovrint:False,ptlb:P0,ptin:_P0,varname:node_6361,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Append,id:4109,x:30372,y:32715,varname:node_4109,prsc:2|A-6361-X,B-6361-Y,C-6361-Z,D-6361-W;n:type:ShaderForge.SFN_Vector4Property,id:796,x:30168,y:32891,ptovrint:False,ptlb:P1,ptin:_P1,varname:_P1,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Append,id:6449,x:30372,y:32891,varname:node_6449,prsc:2|A-796-X,B-796-Y,C-796-Z,D-796-W;n:type:ShaderForge.SFN_Vector4Property,id:3095,x:30168,y:33074,ptovrint:False,ptlb:P2,ptin:_P2,varname:_P2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Append,id:4359,x:30372,y:33074,varname:node_4359,prsc:2|A-3095-X,B-3095-Y,C-3095-Z,D-3095-W;n:type:ShaderForge.SFN_Vector4Property,id:3025,x:30168,y:33252,ptovrint:False,ptlb:P3,ptin:_P3,varname:_P3,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Append,id:2827,x:30372,y:33252,varname:node_2827,prsc:2|A-3025-X,B-3025-Y,C-3025-Z,D-3025-W;n:type:ShaderForge.SFN_Slider,id:7785,x:32367,y:33031,ptovrint:False,ptlb:Outline,ptin:_Outline,varname:node_7785,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.01,max:0.1;proporder:7610-6361-796-3095-3025-7785;pass:END;sub:END;*/

Shader "Shader Forge/PropertyGradientV2" {
    Properties {
        _GradientMap ("GradientMap", 2D) = "white" {}
        _P0 ("P0", Vector) = (0,0,0,0)
        _P1 ("P1", Vector) = (0,0,0,0)
        _P2 ("P2", Vector) = (0,0,0,0)
        _P3 ("P3", Vector) = (0,0,0,0)
        _Outline ("Outline", Range(0, 0.1)) = 0.01
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Outline)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                float _Outline_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Outline );
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_Outline_var,1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                return fixed4(float3(0,0,0),0);
            }
            ENDCG
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
            uniform sampler2D _GradientMap; uniform float4 _GradientMap_ST;
            float Interpolator( float X , float Y , float Z , float4 P0 , float4 P1 , float4 P2 , float4 P3 ){
            float InvD0 = 1/((X-P0.x)*(X-P0.x)+(Y-P0.y)*(Y-P0.y)+(Z-P0.z)*(Z-P0.z));
            float InvD1 = 1/((X-P1.x)*(X-P1.x)+(Y-P1.y)*(Y-P1.y)+(Z-P1.z)*(Z-P1.z));
            float InvD2 = 1/((X-P2.x)*(X-P2.x)+(Y-P2.y)*(Y-P2.y)+(Z-P2.z)*(Z-P2.z));
            float InvD3 = 1/((X-P3.x)*(X-P3.x)+(Y-P3.y)*(Y-P3.y)+(Z-P3.z)*(Z-P3.z));
            return (P0.w*InvD0+P1.w*InvD1+P2.w*InvD2+P3.w*InvD3)/(InvD0+InvD1+InvD2+InvD3);
            }
            
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _P0)
                UNITY_DEFINE_INSTANCED_PROP( float4, _P1)
                UNITY_DEFINE_INSTANCED_PROP( float4, _P2)
                UNITY_DEFINE_INSTANCED_PROP( float4, _P3)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
////// Lighting:
////// Emissive:
                float4 _P0_var = UNITY_ACCESS_INSTANCED_PROP( Props, _P0 );
                float4 _P1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _P1 );
                float4 _P2_var = UNITY_ACCESS_INSTANCED_PROP( Props, _P2 );
                float4 _P3_var = UNITY_ACCESS_INSTANCED_PROP( Props, _P3 );
                float2 node_7649 = float2(clamp(Interpolator( i.posWorld.r , i.posWorld.g , i.posWorld.b , float4(_P0_var.r,_P0_var.g,_P0_var.b,_P0_var.a) , float4(_P1_var.r,_P1_var.g,_P1_var.b,_P1_var.a) , float4(_P2_var.r,_P2_var.g,_P2_var.b,_P2_var.a) , float4(_P3_var.r,_P3_var.g,_P3_var.b,_P3_var.a) ),0.001,0.999),i.uv0.g);
                float4 _GradientMap_var = tex2D(_GradientMap,TRANSFORM_TEX(node_7649, _GradientMap));
                float3 emissive = _GradientMap_var.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
