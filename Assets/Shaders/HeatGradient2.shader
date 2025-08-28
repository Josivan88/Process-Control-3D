// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:35386,y:32916,varname:node_3138,prsc:2|emission-8579-OUT,olwid-2017-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:6163,x:31436,y:33016,varname:node_6163,prsc:2;n:type:ShaderForge.SFN_ObjectPosition,id:7375,x:31436,y:33171,varname:node_7375,prsc:2;n:type:ShaderForge.SFN_Subtract,id:3197,x:31698,y:33089,varname:node_3197,prsc:2|A-6163-XYZ,B-7375-XYZ;n:type:ShaderForge.SFN_ComponentMask,id:5941,x:31894,y:33089,varname:node_5941,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-6163-XYZ;n:type:ShaderForge.SFN_Slider,id:7205,x:32229,y:32637,ptovrint:False,ptlb:A,ptin:_A,varname:node_7205,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:3;n:type:ShaderForge.SFN_Add,id:6589,x:33291,y:33099,varname:node_6589,prsc:2|A-5047-OUT,B-5459-OUT,C-8384-OUT,D-3242-OUT,E-8860-OUT;n:type:ShaderForge.SFN_Tex2d,id:5185,x:34404,y:32793,ptovrint:False,ptlb:Grad1,ptin:_Grad1,varname:node_5185,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7728da4f54fc4d54d8828887e6dee5d0,ntxv:0,isnm:False|UVIN-9718-OUT;n:type:ShaderForge.SFN_Tex2d,id:8856,x:34404,y:32985,ptovrint:False,ptlb:Grad2,ptin:_Grad2,varname:_Grad2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f00e7b3439f763c4981b55b9c2a1c897,ntxv:0,isnm:False|UVIN-9718-OUT;n:type:ShaderForge.SFN_TexCoord,id:3142,x:33987,y:32800,varname:node_3142,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:9718,x:34173,y:32937,varname:node_9718,prsc:2|A-1898-OUT,B-3142-V;n:type:ShaderForge.SFN_Lerp,id:2101,x:34710,y:32990,varname:node_2101,prsc:2|A-5185-RGB,B-8856-RGB,T-3044-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:3044,x:34404,y:33201,ptovrint:False,ptlb:Grad,ptin:_Grad,varname:node_3044,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True;n:type:ShaderForge.SFN_Slider,id:2873,x:32229,y:32832,ptovrint:False,ptlb:X0,ptin:_X0,varname:node_2873,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-3,cur:0,max:3;n:type:ShaderForge.SFN_Slider,id:9165,x:32229,y:32959,ptovrint:False,ptlb:B,ptin:_B,varname:node_9165,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:3;n:type:ShaderForge.SFN_Slider,id:4911,x:32229,y:33114,ptovrint:False,ptlb:Y0,ptin:_Y0,varname:node_4911,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:-2,max:2;n:type:ShaderForge.SFN_Slider,id:2774,x:32229,y:33231,ptovrint:False,ptlb:C,ptin:_C,varname:node_2774,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:2;n:type:ShaderForge.SFN_Slider,id:2701,x:32229,y:33406,ptovrint:False,ptlb:Z0,ptin:_Z0,varname:node_2701,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:2,max:2;n:type:ShaderForge.SFN_Slider,id:2017,x:34597,y:33444,ptovrint:False,ptlb:Outline,ptin:_Outline,varname:node_2017,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.005;n:type:ShaderForge.SFN_Code,id:8860,x:32422,y:33592,varname:node_8860,prsc:2,code:cgBlAHQAdQByAG4AIABBACoAKAAxAC8AcwBxAHIAdAAoADQAKgAzAC4AMQA0ADEANQAqAEsAKgB0ACkAKQAqAGUAeABwACgALQAoACgAQgAtAEUAKQAqACgAQgAtAEUAKQArACgAQwAtAEYAKQAqACgAQwAtAEYAKQArACgARAAtAEcAKQAqACgARAAtAEcAKQApAC8AKAA0ACoASwAqAHQAKQApADsA,output:0,fname:FunctionCirc,width:774,height:277,input:0,input:0,input:0,input:0,input:0,input:0,input:0,input:0,input:0,input_1_label:A,input_2_label:B,input_3_label:C,input_4_label:D,input_5_label:E,input_6_label:F,input_7_label:G,input_8_label:K,input_9_label:t|A-2098-OUT,B-5941-R,C-5941-G,D-5941-B,E-6324-OUT,F-5973-OUT,G-219-OUT,H-8133-OUT,I-8170-OUT;n:type:ShaderForge.SFN_Slider,id:2098,x:31914,y:33577,ptovrint:False,ptlb:CircHeatSource,ptin:_CircHeatSource,varname:node_2098,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.47,max:10;n:type:ShaderForge.SFN_Slider,id:6324,x:31914,y:33693,ptovrint:False,ptlb:Xoffset,ptin:_Xoffset,varname:node_6324,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Slider,id:5973,x:31914,y:33809,ptovrint:False,ptlb:Yoffset,ptin:_Yoffset,varname:node_5973,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Slider,id:219,x:31914,y:33926,ptovrint:False,ptlb:Zoffset,ptin:_Zoffset,varname:node_219,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Multiply,id:1898,x:34006,y:33125,varname:node_1898,prsc:2|A-9293-OUT,B-6579-OUT;n:type:ShaderForge.SFN_Slider,id:6579,x:33213,y:33525,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_6579,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.02,cur:0.5,max:1;n:type:ShaderForge.SFN_Tex2d,id:4154,x:32346,y:33993,ptovrint:False,ptlb:HeatMap,ptin:_HeatMap,varname:node_4154,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:07b3ae6222c746442bb13effcd09aeab,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Slider,id:4738,x:32336,y:34191,ptovrint:False,ptlb:HeatMapIntensity,ptin:_HeatMapIntensity,varname:node_4738,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:8170,x:31192,y:33429,ptovrint:False,ptlb:TimeAdvance,ptin:_TimeAdvance,varname:node_8170,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.0002,cur:0.0008,max:0.1;n:type:ShaderForge.SFN_Desaturate,id:3242,x:32894,y:33974,varname:node_3242,prsc:2|COL-7671-OUT;n:type:ShaderForge.SFN_Lerp,id:7671,x:32713,y:33974,varname:node_7671,prsc:2|A-2770-OUT,B-4154-RGB,T-4738-OUT;n:type:ShaderForge.SFN_Vector1,id:2770,x:32536,y:33949,varname:node_2770,prsc:2,v1:0;n:type:ShaderForge.SFN_ToggleProperty,id:9547,x:34675,y:33302,ptovrint:False,ptlb:GrayScale,ptin:_GrayScale,varname:node_9547,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_Lerp,id:8579,x:35032,y:33023,varname:node_8579,prsc:2|A-2101-OUT,B-1898-OUT,T-9547-OUT;n:type:ShaderForge.SFN_Code,id:5047,x:32620,y:32661,varname:node_5047,prsc:2,code:cgBlAHQAdQByAG4AIABBACoAKAAxAC8AcwBxAHIAdAAoADQAKgAzAC4AMQA0ADEANQAqAEsAKgB0ACkAKQAqAGUAeABwACgALQAoAFgALQBYADAAKQAqACgAWAAtAFgAMAApAC8AKAA0ACoASwAqAHQAKQApADsA,output:0,fname:FunctionX,width:523,height:148,input:0,input:0,input:0,input:0,input:0,input_1_label:A,input_2_label:X,input_3_label:X0,input_4_label:K,input_5_label:t|A-7205-OUT,B-5941-R,C-2873-OUT,D-8133-OUT,E-8170-OUT;n:type:ShaderForge.SFN_Slider,id:8133,x:31357,y:32898,ptovrint:False,ptlb:K,ptin:_K,varname:node_8133,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.08,cur:100,max:100;n:type:ShaderForge.SFN_Code,id:5459,x:32624,y:32974,varname:node_5459,prsc:2,code:cgBlAHQAdQByAG4AIABCACoAKAAxAC8AcwBxAHIAdAAoADQAKgAzAC4AMQA0ADEANQAqAEsAKgB0ACkAKQAqAGUAeABwACgALQAoAFkALQBZADAAKQAqACgAWQAtAFkAMAApAC8AKAA0ACoASwAqAHQAKQApADsA,output:0,fname:FunctionY,width:523,height:148,input:0,input:0,input:0,input:0,input:0,input_1_label:B,input_2_label:Y,input_3_label:Y0,input_4_label:K,input_5_label:t|A-9165-OUT,B-5941-G,C-4911-OUT,D-8133-OUT,E-8170-OUT;n:type:ShaderForge.SFN_Code,id:8384,x:32619,y:33275,varname:node_8384,prsc:2,code:cgBlAHQAdQByAG4AIABDACoAKAAxAC8AcwBxAHIAdAAoADQAKgAzAC4AMQA0ADEANQAqAEsAKgB0ACkAKQAqAGUAeABwACgALQAoAFoALQBaADAAKQAqACgAWgAtAFoAMAApAC8AKAA0ACoASwAqAHQAKQApADsA,output:0,fname:FunctionZ,width:498,height:159,input:0,input:0,input:0,input:0,input:0,input_1_label:C,input_2_label:Z,input_3_label:Z0,input_4_label:K,input_5_label:t|A-2774-OUT,B-5941-B,C-2701-OUT,D-8133-OUT,E-8170-OUT;n:type:ShaderForge.SFN_Add,id:1026,x:33596,y:33154,varname:node_1026,prsc:2|A-6589-OUT,B-1987-OUT;n:type:ShaderForge.SFN_Slider,id:1987,x:33213,y:33324,ptovrint:False,ptlb:D,ptin:_D,varname:node_1987,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.0001,cur:0.01,max:0.99;n:type:ShaderForge.SFN_Add,id:3841,x:33558,y:32879,varname:node_3841,prsc:2|A-7205-OUT,B-9165-OUT,C-2774-OUT,D-2098-OUT,E-1987-OUT;n:type:ShaderForge.SFN_Divide,id:9293,x:33790,y:33113,varname:node_9293,prsc:2|A-1026-OUT,B-528-OUT;n:type:ShaderForge.SFN_Add,id:528,x:33779,y:32928,varname:node_528,prsc:2|A-3841-OUT,B-3242-OUT;proporder:5185-8856-3044-9547-2017-8133-6579-7205-2873-9165-4911-2774-2701-1987-2098-6324-5973-219-4154-4738-8170;pass:END;sub:END;*/

Shader "Shader Forge/HeatGradient2" {
    Properties {
        _Grad1 ("Grad1", 2D) = "white" {}
        _Grad2 ("Grad2", 2D) = "white" {}
        [MaterialToggle] _Grad ("Grad", Float ) = 1
        [MaterialToggle] _GrayScale ("GrayScale", Float ) = 0
        _Outline ("Outline", Range(0, 0.005)) = 0
        _K ("K", Range(0.08, 100)) = 100
        _Scale ("Scale", Range(0.02, 1)) = 0.5
        _A ("A", Range(0, 3)) = 0
        _X0 ("X0", Range(-3, 3)) = 0
        _B ("B", Range(0, 3)) = 0
        _Y0 ("Y0", Range(-2, 2)) = -2
        _C ("C", Range(0, 2)) = 0
        _Z0 ("Z0", Range(-2, 2)) = 2
        _D ("D", Range(0.0001, 0.99)) = 0.01
        _CircHeatSource ("CircHeatSource", Range(0, 10)) = 0.47
        _Xoffset ("Xoffset", Range(-2, 2)) = 0
        _Yoffset ("Yoffset", Range(-2, 2)) = 0
        _Zoffset ("Zoffset", Range(-2, 2)) = 0
        _HeatMap ("HeatMap", 2D) = "black" {}
        _HeatMapIntensity ("HeatMapIntensity", Range(0, 1)) = 1
        _TimeAdvance ("TimeAdvance", Range(0.0002, 0.1)) = 0.0008
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
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Outline;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_Outline,1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
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
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _A;
            uniform sampler2D _Grad1; uniform float4 _Grad1_ST;
            uniform sampler2D _Grad2; uniform float4 _Grad2_ST;
            uniform fixed _Grad;
            uniform float _X0;
            uniform float _B;
            uniform float _Y0;
            uniform float _C;
            uniform float _Z0;
            float FunctionCirc( float A , float B , float C , float D , float E , float F , float G , float K , float t ){
            return A*(1/sqrt(4*3.1415*K*t))*exp(-((B-E)*(B-E)+(C-F)*(C-F)+(D-G)*(D-G))/(4*K*t));
            }
            
            uniform float _CircHeatSource;
            uniform float _Xoffset;
            uniform float _Yoffset;
            uniform float _Zoffset;
            uniform float _Scale;
            uniform sampler2D _HeatMap; uniform float4 _HeatMap_ST;
            uniform float _HeatMapIntensity;
            uniform float _TimeAdvance;
            uniform fixed _GrayScale;
            float FunctionX( float A , float X , float X0 , float K , float t ){
            return A*(1/sqrt(4*3.1415*K*t))*exp(-(X-X0)*(X-X0)/(4*K*t));
            }
            
            uniform float _K;
            float FunctionY( float B , float Y , float Y0 , float K , float t ){
            return B*(1/sqrt(4*3.1415*K*t))*exp(-(Y-Y0)*(Y-Y0)/(4*K*t));
            }
            
            float FunctionZ( float C , float Z , float Z0 , float K , float t ){
            return C*(1/sqrt(4*3.1415*K*t))*exp(-(Z-Z0)*(Z-Z0)/(4*K*t));
            }
            
            uniform float _D;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float3 node_5941 = i.posWorld.rgb.rgb;
                float node_2770 = 0.0;
                float4 _HeatMap_var = tex2D(_HeatMap,TRANSFORM_TEX(i.uv0, _HeatMap));
                float node_3242 = dot(lerp(float3(node_2770,node_2770,node_2770),_HeatMap_var.rgb,_HeatMapIntensity),float3(0.3,0.59,0.11));
                float node_1898 = ((((FunctionX( _A , node_5941.r , _X0 , _K , _TimeAdvance )+FunctionY( _B , node_5941.g , _Y0 , _K , _TimeAdvance )+FunctionZ( _C , node_5941.b , _Z0 , _K , _TimeAdvance )+node_3242+FunctionCirc( _CircHeatSource , node_5941.r , node_5941.g , node_5941.b , _Xoffset , _Yoffset , _Zoffset , _K , _TimeAdvance ))+_D)/((_A+_B+_C+_CircHeatSource+_D)+node_3242))*_Scale);
                float2 node_9718 = float2(node_1898,i.uv0.g);
                float4 _Grad1_var = tex2D(_Grad1,TRANSFORM_TEX(node_9718, _Grad1));
                float4 _Grad2_var = tex2D(_Grad2,TRANSFORM_TEX(node_9718, _Grad2));
                float3 emissive = lerp(lerp(_Grad1_var.rgb,_Grad2_var.rgb,_Grad),float3(node_1898,node_1898,node_1898),_GrayScale);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
