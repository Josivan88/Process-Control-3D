// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|emission-8669-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32139,y:32705,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4811321,c2:0.4811321,c3:0.4811321,c4:1;n:type:ShaderForge.SFN_TexCoord,id:8208,x:31096,y:32319,varname:node_8208,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:697,x:32168,y:32905,ptovrint:False,ptlb:BaseTexture,ptin:_BaseTexture,varname:node_697,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-9826-OUT;n:type:ShaderForge.SFN_Append,id:2980,x:31332,y:32873,varname:node_2980,prsc:2|A-4899-OUT,B-3597-OUT;n:type:ShaderForge.SFN_Multiply,id:8669,x:32413,y:32807,varname:node_8669,prsc:2|A-1304-RGB,B-697-RGB,C-4899-OUT;n:type:ShaderForge.SFN_Time,id:8524,x:31947,y:31866,varname:node_8524,prsc:2;n:type:ShaderForge.SFN_Code,id:4899,x:31989,y:32133,varname:node_4899,prsc:2,code:cgBlAHQAdQByAG4AIABlAHgAcAAoAC0AQQAqACgAKABYAC0AWAAwACkAKgAoAFgALQBYADAAKQArAFkAKgBZACkAKQA7AA==,output:0,fname:FunctionX,width:519,height:177,input:0,input:0,input:0,input:0,input_1_label:X,input_2_label:Y,input_3_label:A,input_4_label:X0|A-2744-OUT,B-3061-OUT,C-3052-OUT,D-8087-OUT;n:type:ShaderForge.SFN_ArcTan2,id:3597,x:32363,y:32541,varname:node_3597,prsc:2,attp:2|A-3061-OUT,B-2744-OUT;n:type:ShaderForge.SFN_ComponentMask,id:6207,x:31498,y:32953,varname:node_6207,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2980-OUT;n:type:ShaderForge.SFN_Add,id:2196,x:31715,y:32994,varname:node_2196,prsc:2|A-6207-R,B-8043-OUT;n:type:ShaderForge.SFN_Append,id:9826,x:31907,y:32994,varname:node_9826,prsc:2|A-2196-OUT,B-6207-G;n:type:ShaderForge.SFN_Multiply,id:8043,x:32234,y:31889,varname:node_8043,prsc:2|A-8524-T,B-5519-OUT;n:type:ShaderForge.SFN_Slider,id:5519,x:31790,y:32030,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_5519,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3,max:3;n:type:ShaderForge.SFN_RemapRange,id:2744,x:31590,y:32218,varname:node_2744,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-8208-U;n:type:ShaderForge.SFN_RemapRange,id:3061,x:31590,y:32393,varname:node_3061,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-8208-V;n:type:ShaderForge.SFN_FragmentPosition,id:4561,x:31000,y:31992,varname:node_4561,prsc:2;n:type:ShaderForge.SFN_ObjectPosition,id:1098,x:31000,y:32157,varname:node_1098,prsc:2;n:type:ShaderForge.SFN_Subtract,id:1901,x:31263,y:32094,varname:node_1901,prsc:2|A-4561-XYZ,B-1098-XYZ;n:type:ShaderForge.SFN_ComponentMask,id:2752,x:31459,y:32002,varname:node_2752,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-1901-OUT;n:type:ShaderForge.SFN_Slider,id:3052,x:31079,y:32532,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_3052,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Slider,id:8087,x:31253,y:32699,ptovrint:False,ptlb:X0,ptin:_X0,varname:node_8087,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:2;proporder:1304-697-5519-3052-8087;pass:END;sub:END;*/

Shader "Shader Forge/FlowShader" {
    Properties {
        _Color ("Color", Color) = (0.4811321,0.4811321,0.4811321,1)
        _BaseTexture ("BaseTexture", 2D) = "white" {}
        _Speed ("Speed", Range(0, 3)) = 3
        _Scale ("Scale", Range(0, 2)) = 1
        _X0 ("X0", Range(0, 2)) = 0
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
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _BaseTexture; uniform float4 _BaseTexture_ST;
            float FunctionX( float X , float Y , float A , float X0 ){
            return exp(-A*((X-X0)*(X-X0)+Y*Y));
            }
            
            uniform float _Speed;
            uniform float _Scale;
            uniform float _X0;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_2744 = (i.uv0.r*2.0+-1.0);
                float node_3061 = (i.uv0.g*2.0+-1.0);
                float node_4899 = FunctionX( node_2744 , node_3061 , _Scale , _X0 );
                float2 node_6207 = float2(node_4899,((atan2(node_3061,node_2744)/6.28318530718)+0.5)).rg;
                float4 node_8524 = _Time;
                float2 node_9826 = float2((node_6207.r+(node_8524.g*_Speed)),node_6207.g);
                float4 _BaseTexture_var = tex2D(_BaseTexture,TRANSFORM_TEX(node_9826, _BaseTexture));
                float3 emissive = (_Color.rgb*_BaseTexture_var.rgb*node_4899);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
