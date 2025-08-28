// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32641,y:32793,varname:node_3138,prsc:2|emission-1335-RGB;n:type:ShaderForge.SFN_FragmentPosition,id:7391,x:30885,y:32612,varname:node_7391,prsc:2;n:type:ShaderForge.SFN_Slider,id:5446,x:30728,y:32789,ptovrint:False,ptlb:X0,ptin:_X0,varname:node_5446,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-30,cur:0,max:30;n:type:ShaderForge.SFN_Slider,id:3476,x:30728,y:32912,ptovrint:False,ptlb:Y0,ptin:_Y0,varname:_X1,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-30,cur:0,max:30;n:type:ShaderForge.SFN_Slider,id:9979,x:30728,y:33037,ptovrint:False,ptlb:Z0,ptin:_Z0,varname:_X2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-30,cur:0,max:30;n:type:ShaderForge.SFN_Slider,id:452,x:30728,y:33168,ptovrint:False,ptlb:S,ptin:_S,varname:_Z1,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-30,cur:1,max:30;n:type:ShaderForge.SFN_TexCoord,id:8028,x:31987,y:33009,varname:node_8028,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:6572,x:32204,y:32892,varname:node_6572,prsc:2|A-1699-OUT,B-8028-V;n:type:ShaderForge.SFN_Tex2d,id:1335,x:32413,y:32892,ptovrint:False,ptlb:GradientMap,ptin:_GradientMap,varname:node_1335,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1d775ca7130832545bf60f47be4a9e47,ntxv:0,isnm:False|UVIN-6572-OUT;n:type:ShaderForge.SFN_Clamp,id:1699,x:31987,y:32850,varname:node_1699,prsc:2|IN-7339-OUT,MIN-5016-OUT,MAX-375-OUT;n:type:ShaderForge.SFN_Vector1,id:5016,x:31796,y:32917,varname:node_5016,prsc:2,v1:0.001;n:type:ShaderForge.SFN_Vector1,id:375,x:31796,y:32994,varname:node_375,prsc:2,v1:0.999;n:type:ShaderForge.SFN_Code,id:7339,x:31158,y:32817,varname:node_7339,prsc:2,code:cgBlAHQAdQByAG4AIABzAHEAcgB0ACgAKABYAC0AWAAwACkAKgAoAFgALQBYADAAKQArACgAWQAtAFkAMAApACoAKABZAC0AWQAwACkAKwAoAFoALQBaADAAKQAqACgAWgAtAFoAMAApACkALwBTADsA,output:0,fname:Interpolator,width:589,height:379,input:0,input:0,input:0,input:0,input:0,input:0,input:0,input_1_label:X,input_2_label:Y,input_3_label:Z,input_4_label:X0,input_5_label:Y0,input_6_label:Z0,input_7_label:S|A-7391-X,B-7391-Y,C-7391-Z,D-5446-OUT,E-3476-OUT,F-9979-OUT,G-452-OUT;proporder:5446-3476-9979-452-1335;pass:END;sub:END;*/

Shader "Shader Forge/PropertyGradient" {
    Properties {
        _X0 ("X0", Range(-30, 30)) = 0
        _Y0 ("Y0", Range(-30, 30)) = 0
        _Z0 ("Z0", Range(-30, 30)) = 0
        _S ("S", Range(-30, 30)) = 1
        _GradientMap ("GradientMap", 2D) = "white" {}
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
            uniform sampler2D _GradientMap; uniform float4 _GradientMap_ST;
            float Interpolator( float X , float Y , float Z , float X0 , float Y0 , float Z0 , float S ){
            return sqrt((X-X0)*(X-X0)+(Y-Y0)*(Y-Y0)+(Z-Z0)*(Z-Z0))/S;
            }
            
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _X0)
                UNITY_DEFINE_INSTANCED_PROP( float, _Y0)
                UNITY_DEFINE_INSTANCED_PROP( float, _Z0)
                UNITY_DEFINE_INSTANCED_PROP( float, _S)
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
                float _X0_var = UNITY_ACCESS_INSTANCED_PROP( Props, _X0 );
                float _Y0_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Y0 );
                float _Z0_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Z0 );
                float _S_var = UNITY_ACCESS_INSTANCED_PROP( Props, _S );
                float2 node_6572 = float2(clamp(Interpolator( i.posWorld.r , i.posWorld.g , i.posWorld.b , _X0_var , _Y0_var , _Z0_var , _S_var ),0.001,0.999),i.uv0.g);
                float4 _GradientMap_var = tex2D(_GradientMap,TRANSFORM_TEX(node_6572, _GradientMap));
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
