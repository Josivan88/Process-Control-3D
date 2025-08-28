// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34183,y:32688,varname:node_3138,prsc:2|emission-3662-OUT;n:type:ShaderForge.SFN_Multiply,id:1779,x:32345,y:32521,varname:node_1779,prsc:2|A-608-OUT,B-377-OUT;n:type:ShaderForge.SFN_Code,id:8636,x:33128,y:32603,varname:node_8636,prsc:2,code:IAAgAGYAbABvAGEAdAAgAHIAZQB0ACAAPQAgADAAOwANAAoAIAAgAGkAbgB0ACAAaQB0AGUAcgBhAHQAaQBvAG4AcwAgAD0AIAA2ADsADQAKACAAIABmAG8AcgAgACgAaQBuAHQAIABpACAAPQAgADAAOwAgAGkAIAA8ACAAaQB0AGUAcgBhAHQAaQBvAG4AcwA7ACAAKwArAGkAKQANAAoAIAAgAHsADQAKACAAIAAgACAAIABmAGwAbwBhAHQAMgAgAHAAIAA9ACAAZgBsAG8AbwByACgAVQBWACAAKgAgACgAaQArADEAKQApADsADQAKACAAIAAgACAAIABmAGwAbwBhAHQAMgAgAGYAIAA9ACAAZgByAGEAYwAoAFUAVgAgACoAIAAoAGkAKwAxACkAKQA7AA0ACgAgACAAIAAgACAAZgAgAD0AIABmACAAKgAgAGYAIAAqACAAKAAzAC4AMAAgAC0AIAAyAC4AMAAgACoAIABmACkAOwANAAoAIAAgACAAIAAgAGYAbABvAGEAdAAgAG4AIAA9ACAAcAAuAHgAIAArACAAcAAuAHkAIAAqACAANQA3AC4AMAA7AA0ACgAgACAAIAAgACAAZgBsAG8AYQB0ADQAIABuAG8AaQBzAGUAIAA9ACAAZgBsAG8AYQB0ADQAKABuACwAIABuACAAKwAgADEALAAgAG4AIAArACAANQA3AC4AMAAsACAAbgAgACsAIAA1ADgALgAwACkAOwANAAoAIAAgACAAIAAgAG4AbwBpAHMAZQAgAD0AIABmAHIAYQBjACgAcwBpAG4AKABuAG8AaQBzAGUAKQAqADQAMwA3AC4ANQA4ADUANAA1ADMAKQA7AA0ACgAgACAAIAAgACAAcgBlAHQAIAArAD0AIABsAGUAcgBwACgAbABlAHIAcAAoAG4AbwBpAHMAZQAuAHgALAAgAG4AbwBpAHMAZQAuAHkALAAgAGYALgB4ACkALAAgAGwAZQByAHAAKABuAG8AaQBzAGUALgB6ACwAIABuAG8AaQBzAGUALgB3ACwAIABmAC4AeAApACwAIABmAC4AeQApACAAKgAgACgAIABpAHQAZQByAGEAdABpAG8AbgBzACAALwAgACgAaQArADEAKQApADsADQAKACAAIAB9AA0ACgAgACAAcgBlAHQAdQByAG4AIAByAGUAdAAvACgAMgAqAGkAdABlAHIAYQB0AGkAbwBuAHMAKQA7AA==,output:0,fname:Noise,width:911,height:386,input:1,input_1_label:UV|A-1600-OUT;n:type:ShaderForge.SFN_TexCoord,id:8480,x:31871,y:32331,varname:node_8480,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:3222,x:32498,y:32730,varname:node_3222,prsc:2|A-1779-OUT,B-8635-OUT;n:type:ShaderForge.SFN_Slider,id:377,x:32004,y:32678,ptovrint:False,ptlb:Frequency,ptin:_Frequency,varname:node_377,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:2,cur:10,max:50;n:type:ShaderForge.SFN_Slider,id:8635,x:32004,y:32775,ptovrint:False,ptlb:Seed,ptin:_Seed,varname:node_8635,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.2,cur:13.40513,max:50;n:type:ShaderForge.SFN_Step,id:2354,x:33684,y:32720,varname:node_2354,prsc:2|A-8636-OUT,B-2504-OUT;n:type:ShaderForge.SFN_Slider,id:2504,x:32004,y:32871,ptovrint:False,ptlb:CutOut,ptin:_CutOut,varname:node_2504,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6,max:1;n:type:ShaderForge.SFN_Lerp,id:3662,x:33925,y:32835,varname:node_3662,prsc:2|A-8636-OUT,B-5135-OUT,T-8354-OUT;n:type:ShaderForge.SFN_Slider,id:8354,x:32004,y:32959,ptovrint:False,ptlb:Mixer,ptin:_Mixer,varname:node_8354,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_OneMinus,id:5135,x:33865,y:32668,varname:node_5135,prsc:2|IN-2354-OUT;n:type:ShaderForge.SFN_ComponentMask,id:8314,x:32524,y:32521,varname:node_8314,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1779-OUT;n:type:ShaderForge.SFN_Multiply,id:769,x:32763,y:32391,varname:node_769,prsc:2|A-8314-R,B-5004-OUT;n:type:ShaderForge.SFN_Multiply,id:2324,x:32763,y:32593,varname:node_2324,prsc:2|A-8314-G,B-5917-OUT;n:type:ShaderForge.SFN_Slider,id:5004,x:32407,y:32287,ptovrint:False,ptlb:stretchX,ptin:_stretchX,varname:node_5004,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Slider,id:5917,x:32390,y:32394,ptovrint:False,ptlb:stretchY,ptin:_stretchY,varname:_stretchX_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Append,id:1600,x:32941,y:32415,varname:node_1600,prsc:2|A-769-OUT,B-2324-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:9861,x:31664,y:32097,varname:node_9861,prsc:2;n:type:ShaderForge.SFN_Slider,id:3056,x:31783,y:32527,ptovrint:False,ptlb:UV-WorldPos,ptin:_UVWorldPos,varname:node_3056,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Lerp,id:608,x:32106,y:32268,varname:node_608,prsc:2|A-4377-OUT,B-8480-UVOUT,T-3056-OUT;n:type:ShaderForge.SFN_Append,id:4377,x:32056,y:32100,varname:node_4377,prsc:2|A-9861-X,B-9861-Z;n:type:ShaderForge.SFN_Add,id:7727,x:31870,y:32075,varname:node_7727,prsc:2|A-9861-X,B-9861-Y;n:type:ShaderForge.SFN_Add,id:4052,x:31870,y:32202,varname:node_4052,prsc:2|A-9861-Y,B-9861-Z;proporder:377-8635-2504-8354-5004-5917-3056;pass:END;sub:END;*/

Shader "Shader Forge/PerlinNoise" {
    Properties {
        _Frequency ("Frequency", Range(2, 50)) = 10
        _Seed ("Seed", Range(0.2, 50)) = 13.40513
        _CutOut ("CutOut", Range(0, 1)) = 0.6
        _Mixer ("Mixer", Range(0, 1)) = 0.1
        _stretchX ("stretchX", Range(0, 2)) = 1
        _stretchY ("stretchY", Range(0, 2)) = 1
        _UVWorldPos ("UV-WorldPos", Range(0, 1)) = 1
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
            float Noise( float2 UV ){
              float ret = 0;
              int iterations = 6;
              for (int i = 0; i < iterations; ++i)
              {
                 float2 p = floor(UV * (i+1));
                 float2 f = frac(UV * (i+1));
                 f = f * f * (3.0 - 2.0 * f);
                 float n = p.x + p.y * 57.0;
                 float4 noise = float4(n, n + 1, n + 57.0, n + 58.0);
                 noise = frac(sin(noise)*437.585453);
                 ret += lerp(lerp(noise.x, noise.y, f.x), lerp(noise.z, noise.w, f.x), f.y) * ( iterations / (i+1));
              }
              return ret/(2*iterations);
            }
            
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Frequency)
                UNITY_DEFINE_INSTANCED_PROP( float, _CutOut)
                UNITY_DEFINE_INSTANCED_PROP( float, _Mixer)
                UNITY_DEFINE_INSTANCED_PROP( float, _stretchX)
                UNITY_DEFINE_INSTANCED_PROP( float, _stretchY)
                UNITY_DEFINE_INSTANCED_PROP( float, _UVWorldPos)
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
                float _UVWorldPos_var = UNITY_ACCESS_INSTANCED_PROP( Props, _UVWorldPos );
                float _Frequency_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Frequency );
                float2 node_1779 = (lerp(float2(i.posWorld.r,i.posWorld.b),i.uv0,_UVWorldPos_var)*_Frequency_var);
                float2 node_8314 = node_1779.rg;
                float _stretchX_var = UNITY_ACCESS_INSTANCED_PROP( Props, _stretchX );
                float _stretchY_var = UNITY_ACCESS_INSTANCED_PROP( Props, _stretchY );
                float node_8636 = Noise( float2((node_8314.r*_stretchX_var),(node_8314.g*_stretchY_var)) );
                float _CutOut_var = UNITY_ACCESS_INSTANCED_PROP( Props, _CutOut );
                float _Mixer_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Mixer );
                float node_3662 = lerp(node_8636,(1.0 - step(node_8636,_CutOut_var)),_Mixer_var);
                float3 emissive = float3(node_3662,node_3662,node_3662);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
