// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:True,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1,x:35476,y:32610,varname:node_1,prsc:2|diff-1778-OUT,spec-15-OUT,gloss-15-OUT,normal-960-OUT,emission-1257-OUT,amspl-250-OUT,alpha-1250-OUT,refract-880-OUT,voffset-1434-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33882,y:32606,ptovrint:False,ptlb:Difuse,ptin:_Difuse,varname:node_7827,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8,x:34156,y:32468,varname:node_8,prsc:2|A-9-RGB,B-2-RGB;n:type:ShaderForge.SFN_Color,id:9,x:33860,y:32428,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_2239,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9960784,c2:0.972549,c3:0.8862745,c4:1;n:type:ShaderForge.SFN_Slider,id:15,x:34491,y:32841,ptovrint:False,ptlb:Specular,ptin:_Specular,cmnt:To Specular and Gloss,varname:node_1578,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4,max:0.5;n:type:ShaderForge.SFN_FragmentPosition,id:46,x:32830,y:33190,varname:node_46,prsc:2;n:type:ShaderForge.SFN_Time,id:48,x:32303,y:33390,varname:node_48,prsc:2;n:type:ShaderForge.SFN_Sin,id:49,x:33674,y:33384,varname:node_49,prsc:2|IN-50-OUT;n:type:ShaderForge.SFN_Add,id:50,x:33019,y:33294,varname:node_50,prsc:2|A-906-OUT,B-46-X;n:type:ShaderForge.SFN_Slider,id:57,x:33351,y:34188,ptovrint:False,ptlb:High_Frequency_Heigth,ptin:_High_Frequency_Heigth,varname:node_8090,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.06,max:0.5;n:type:ShaderForge.SFN_TexCoord,id:66,x:32096,y:33654,varname:node_66,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:68,x:32557,y:33664,varname:node_68,prsc:2|A-913-OUT,B-66-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:69,x:33527,y:33750,ptovrint:False,ptlb:DisplaceMap,ptin:_DisplaceMap,varname:node_8792,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-68-OUT;n:type:ShaderForge.SFN_Add,id:71,x:34187,y:33551,varname:node_71,prsc:2|A-2978-OUT,B-1025-OUT,C-98-OUT;n:type:ShaderForge.SFN_Multiply,id:98,x:33760,y:33750,varname:node_98,prsc:2|A-57-OUT,B-69-RGB,C-1085-OUT;n:type:ShaderForge.SFN_Fresnel,id:247,x:33433,y:33039,varname:node_247,prsc:2;n:type:ShaderForge.SFN_Slider,id:248,x:33276,y:33204,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_9675,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7,max:1;n:type:ShaderForge.SFN_OneMinus,id:249,x:33674,y:33143,varname:node_249,prsc:2|IN-248-OUT;n:type:ShaderForge.SFN_Multiply,id:250,x:34673,y:33050,cmnt:To Specular Reflection,varname:node_250,prsc:2|A-441-RGB,B-253-OUT,C-460-OUT;n:type:ShaderForge.SFN_Multiply,id:252,x:33674,y:33002,varname:node_252,prsc:2|A-247-OUT,B-248-OUT;n:type:ShaderForge.SFN_Add,id:253,x:33870,y:33227,varname:node_253,prsc:2|A-252-OUT,B-249-OUT;n:type:ShaderForge.SFN_Tex2d,id:325,x:33097,y:32661,ptovrint:False,ptlb:Bump,ptin:_Bump,varname:node_2768,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True|UVIN-68-OUT;n:type:ShaderForge.SFN_Cubemap,id:441,x:34153,y:32914,ptovrint:False,ptlb:Cube,ptin:_Cube,varname:node_7318,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,pvfc:0;n:type:ShaderForge.SFN_Slider,id:460,x:33517,y:33310,ptovrint:False,ptlb:Reflection,ptin:_Reflection,varname:node_1543,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.85,max:1;n:type:ShaderForge.SFN_NormalVector,id:835,x:34673,y:33182,prsc:2,pt:False;n:type:ShaderForge.SFN_DepthBlend,id:850,x:34168,y:34415,varname:node_850,prsc:2|DIST-2912-OUT;n:type:ShaderForge.SFN_Slider,id:851,x:33791,y:34588,ptovrint:False,ptlb:Turbidity,ptin:_Turbidity,varname:node_5962,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_ComponentMask,id:876,x:34384,y:33319,varname:node_876,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-325-RGB;n:type:ShaderForge.SFN_Slider,id:878,x:34030,y:33422,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:node_3411,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2,max:2;n:type:ShaderForge.SFN_Multiply,id:880,x:34426,y:33140,cmnt:To refraction,varname:node_880,prsc:2|A-876-OUT,B-878-OUT,C-850-OUT;n:type:ShaderForge.SFN_Slider,id:898,x:32159,y:33287,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_9502,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:3;n:type:ShaderForge.SFN_Multiply,id:906,x:32543,y:33325,varname:node_906,prsc:2|A-898-OUT,B-48-T;n:type:ShaderForge.SFN_Multiply,id:913,x:32543,y:33488,varname:node_913,prsc:2|A-898-OUT,B-48-TSL;n:type:ShaderForge.SFN_Slider,id:926,x:33791,y:34689,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_618,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.88,max:1;n:type:ShaderForge.SFN_Multiply,id:928,x:34385,y:34479,varname:node_928,prsc:2|A-850-OUT,B-926-OUT;n:type:ShaderForge.SFN_Multiply,id:934,x:32536,y:33817,varname:node_934,prsc:2|A-913-OUT,B-935-OUT;n:type:ShaderForge.SFN_Vector1,id:935,x:32333,y:33887,varname:node_935,prsc:2,v1:-1;n:type:ShaderForge.SFN_Vector1,id:939,x:34072,y:33082,varname:node_939,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Lerp,id:940,x:33434,y:32647,varname:node_940,prsc:2|A-325-RGB,B-949-RGB,T-939-OUT;n:type:ShaderForge.SFN_Tex2d,id:949,x:33890,y:33067,ptovrint:False,ptlb:Bump2,ptin:_Bump2,varname:node_8029,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True|UVIN-1041-OUT;n:type:ShaderForge.SFN_Normalize,id:960,x:34153,y:32669,cmnt:To normal,varname:node_960,prsc:2|IN-940-OUT;n:type:ShaderForge.SFN_Tex2d,id:1023,x:33527,y:33989,ptovrint:False,ptlb:DisplaceMap2,ptin:_DisplaceMap2,varname:node_8955,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1041-OUT;n:type:ShaderForge.SFN_Multiply,id:1025,x:33760,y:33882,varname:node_1025,prsc:2|A-1085-OUT,B-1023-RGB,C-57-OUT;n:type:ShaderForge.SFN_Add,id:1041,x:32536,y:33987,varname:node_1041,prsc:2|A-934-OUT,B-66-UVOUT;n:type:ShaderForge.SFN_ToggleProperty,id:1063,x:34166,y:33841,ptovrint:False,ptlb:VertexDisplacement,ptin:_VertexDisplacement,varname:node_6620,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True;n:type:ShaderForge.SFN_Vector1,id:1085,x:33527,y:33916,varname:node_1085,prsc:2,v1:2;n:type:ShaderForge.SFN_Color,id:1195,x:33839,y:31835,ptovrint:False,ptlb:FoamColor,ptin:_FoamColor,varname:node_2431,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9882353,c2:0.9686275,c3:0.8823529,c4:1;n:type:ShaderForge.SFN_Add,id:1250,x:34620,y:34000,cmnt:To Alpha,varname:node_1250,prsc:2|A-928-OUT,B-1810-OUT;n:type:ShaderForge.SFN_Tex2d,id:1256,x:33860,y:32212,ptovrint:False,ptlb:FoamTexture,ptin:_FoamTexture,varname:node_9026,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2928-OUT;n:type:ShaderForge.SFN_Multiply,id:1257,x:34619,y:32322,cmnt:To emit,varname:node_1257,prsc:2|A-1195-RGB,B-1256-RGB,C-1712-OUT,D-1791-RGB;n:type:ShaderForge.SFN_VertexColor,id:1386,x:34673,y:33531,varname:node_1386,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1434,x:35062,y:33272,cmnt:To Vertex Offset,varname:node_1434,prsc:2|A-71-OUT,B-1063-OUT,C-835-OUT,D-3758-RGB;n:type:ShaderForge.SFN_Slider,id:1684,x:34487,y:34738,ptovrint:False,ptlb:FoamFactor,ptin:_FoamFactor,varname:node_1883,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.410218,max:5;n:type:ShaderForge.SFN_DepthBlend,id:1710,x:34848,y:34619,varname:node_1710,prsc:2|DIST-1684-OUT;n:type:ShaderForge.SFN_OneMinus,id:1712,x:34848,y:34470,varname:node_1712,prsc:2|IN-1710-OUT;n:type:ShaderForge.SFN_Add,id:1778,x:34619,y:32556,cmnt:To difuse,varname:node_1778,prsc:2|A-1257-OUT,B-8-OUT,C-4031-OUT;n:type:ShaderForge.SFN_Tex2d,id:1791,x:33860,y:32015,ptovrint:False,ptlb:FoamNoise,ptin:_FoamNoise,varname:node_9728,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2776-OUT;n:type:ShaderForge.SFN_Desaturate,id:1798,x:34816,y:32659,varname:node_1798,prsc:2|COL-1257-OUT;n:type:ShaderForge.SFN_Multiply,id:1810,x:34610,y:34164,varname:node_1810,prsc:2|A-1798-OUT,B-1712-OUT;n:type:ShaderForge.SFN_Add,id:2240,x:33175,y:31944,varname:node_2240,prsc:2|A-2253-R,B-2635-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2253,x:32956,y:31934,varname:node_2253,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-66-UVOUT;n:type:ShaderForge.SFN_Append,id:2262,x:33352,y:31990,varname:node_2262,prsc:2|A-2240-OUT,B-2253-G;n:type:ShaderForge.SFN_Multiply,id:2635,x:33326,y:32215,varname:node_2635,prsc:2|A-2744-OUT,B-2636-OUT;n:type:ShaderForge.SFN_Vector1,id:2636,x:33097,y:32215,varname:node_2636,prsc:2,v1:0.02;n:type:ShaderForge.SFN_Sin,id:2744,x:32543,y:33167,varname:node_2744,prsc:2|IN-906-OUT;n:type:ShaderForge.SFN_Add,id:2776,x:33608,y:32004,varname:node_2776,prsc:2|A-2262-OUT,B-2813-OUT,C-960-OUT,D-2939-OUT;n:type:ShaderForge.SFN_Multiply,id:2813,x:33344,y:32384,varname:node_2813,prsc:2|A-2845-OUT,B-48-T;n:type:ShaderForge.SFN_Vector1,id:2845,x:33097,y:32384,varname:node_2845,prsc:2,v1:0.05;n:type:ShaderForge.SFN_Power,id:2874,x:35050,y:34233,varname:node_2874,prsc:2|VAL-1712-OUT,EXP-2875-OUT;n:type:ShaderForge.SFN_Vector1,id:2875,x:34848,y:34115,varname:node_2875,prsc:2,v1:3;n:type:ShaderForge.SFN_OneMinus,id:2910,x:33796,y:34408,varname:node_2910,prsc:2|IN-851-OUT;n:type:ShaderForge.SFN_Multiply,id:2912,x:33973,y:34408,varname:node_2912,prsc:2|A-2910-OUT,B-2914-OUT;n:type:ShaderForge.SFN_Vector1,id:2914,x:33760,y:34040,varname:node_2914,prsc:2,v1:5;n:type:ShaderForge.SFN_Add,id:2928,x:33624,y:32212,varname:node_2928,prsc:2|A-2262-OUT,B-2939-OUT;n:type:ShaderForge.SFN_Lerp,id:2939,x:33601,y:31858,varname:node_2939,prsc:2|A-960-OUT,B-2940-OUT,T-2942-OUT;n:type:ShaderForge.SFN_Vector3,id:2940,x:33341,y:31776,varname:node_2940,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Vector1,id:2942,x:33341,y:31862,varname:node_2942,prsc:2,v1:0.9;n:type:ShaderForge.SFN_Multiply,id:2978,x:33990,y:33498,varname:node_2978,prsc:2|A-49-OUT,B-3705-OUT,C-4195-OUT;n:type:ShaderForge.SFN_Vector1,id:2979,x:34142,y:33691,varname:node_2979,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:3191,x:33019,y:33529,varname:node_3191,prsc:2|A-46-Z,B-3210-OUT;n:type:ShaderForge.SFN_Vector1,id:3210,x:32818,y:33649,varname:node_3210,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Power,id:3485,x:34673,y:33697,varname:node_3485,prsc:2|VAL-71-OUT,EXP-3487-OUT;n:type:ShaderForge.SFN_Vector1,id:3487,x:34398,y:33806,varname:node_3487,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:3559,x:34673,y:33836,varname:node_3559,prsc:2|A-3485-OUT,B-3487-OUT;n:type:ShaderForge.SFN_Multiply,id:3661,x:34183,y:31868,varname:node_3661,prsc:2|A-1195-RGB,B-1791-RGB,C-1256-RGB;n:type:ShaderForge.SFN_Add,id:3691,x:33283,y:33618,varname:node_3691,prsc:2|A-3191-OUT,B-3867-OUT;n:type:ShaderForge.SFN_Sin,id:3705,x:33674,y:33504,varname:node_3705,prsc:2|IN-3691-OUT;n:type:ShaderForge.SFN_Tex2d,id:3758,x:34564,y:33414,ptovrint:False,ptlb:AtenuationMap,ptin:_AtenuationMap,varname:node_7629,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-66-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3867,x:33019,y:33676,varname:node_3867,prsc:2|A-906-OUT,B-3210-OUT;n:type:ShaderForge.SFN_Multiply,id:3869,x:32353,y:34429,varname:node_3869,prsc:2|A-898-OUT,B-3888-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:3871,x:32331,y:34272,varname:node_3871,prsc:2;n:type:ShaderForge.SFN_Add,id:3873,x:32825,y:34329,varname:node_3873,prsc:2|A-3869-OUT,B-3871-X;n:type:ShaderForge.SFN_Multiply,id:3875,x:32615,y:34471,varname:node_3875,prsc:2|A-3871-Z,B-3879-OUT;n:type:ShaderForge.SFN_Multiply,id:3877,x:32615,y:34604,varname:node_3877,prsc:2|A-3869-OUT,B-3879-OUT;n:type:ShaderForge.SFN_Vector1,id:3879,x:32353,y:34570,varname:node_3879,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Add,id:3881,x:32825,y:34471,varname:node_3881,prsc:2|A-3875-OUT,B-3877-OUT;n:type:ShaderForge.SFN_Sin,id:3883,x:33058,y:34329,varname:node_3883,prsc:2|IN-3873-OUT;n:type:ShaderForge.SFN_Sin,id:3885,x:33058,y:34471,varname:node_3885,prsc:2|IN-3881-OUT;n:type:ShaderForge.SFN_Multiply,id:3887,x:33272,y:34354,varname:node_3887,prsc:2|A-3883-OUT,B-3885-OUT;n:type:ShaderForge.SFN_Add,id:3888,x:32113,y:34468,varname:node_3888,prsc:2|A-48-T,B-3889-OUT;n:type:ShaderForge.SFN_Vector1,id:3889,x:31916,y:34538,varname:node_3889,prsc:2,v1:-1;n:type:ShaderForge.SFN_Add,id:3893,x:33458,y:34292,varname:node_3893,prsc:2|A-3887-OUT,B-1791-RGB,C-3895-OUT;n:type:ShaderForge.SFN_Vector1,id:3895,x:33272,y:34505,varname:node_3895,prsc:2,v1:-0.9;n:type:ShaderForge.SFN_Add,id:3897,x:33652,y:34292,varname:node_3897,prsc:2|A-3893-OUT,B-3559-OUT;n:type:ShaderForge.SFN_Desaturate,id:3929,x:34281,y:34169,varname:node_3929,prsc:2|COL-250-OUT;n:type:ShaderForge.SFN_Normalize,id:3940,x:34619,y:32704,varname:node_3940,prsc:2|IN-4031-OUT;n:type:ShaderForge.SFN_Vector1,id:3947,x:33272,y:34580,varname:node_3947,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:4031,x:33871,y:34251,varname:node_4031,prsc:2|A-3897-OUT,B-3947-OUT,C-57-OUT;n:type:ShaderForge.SFN_Multiply,id:4154,x:34774,y:32841,cmnt:To Specular Reflection,varname:node_4154,prsc:2|A-15-OUT,B-250-OUT;n:type:ShaderForge.SFN_Slider,id:4195,x:33596,y:33645,ptovrint:False,ptlb:Low_Frequency_Heigth,ptin:_Low_Frequency_Heigth,varname:node_5295,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.08,max:0.5;proporder:9-2-15-1063-69-325-1023-949-4195-57-441-248-460-851-878-898-926-1195-1256-1684-1791-3758;pass:END;sub:END;*/

Shader "Shader Forge/Water" {
    Properties {
        _Color ("Color", Color) = (0.9960784,0.972549,0.8862745,1)
        _Difuse ("Difuse", 2D) = "white" {}
        _Specular ("Specular", Range(0, 0.5)) = 0.4
        [MaterialToggle] _VertexDisplacement ("VertexDisplacement", Float ) = 1
        _DisplaceMap ("DisplaceMap", 2D) = "white" {}
        _Bump ("Bump", 2D) = "bump" {}
        _DisplaceMap2 ("DisplaceMap2", 2D) = "white" {}
        _Bump2 ("Bump2", 2D) = "bump" {}
        _Low_Frequency_Heigth ("Low_Frequency_Heigth", Range(0, 0.5)) = 0.08
        _High_Frequency_Heigth ("High_Frequency_Heigth", Range(0, 0.5)) = 0.06
        _Cube ("Cube", Cube) = "_Skybox" {}
        _Fresnel ("Fresnel", Range(0, 1)) = 0.7
        _Reflection ("Reflection", Range(0, 1)) = 0.85
        _Turbidity ("Turbidity", Range(0, 1)) = 1
        _Refraction ("Refraction", Range(0, 2)) = 0.2
        _Speed ("Speed", Range(0, 3)) = 1
        _Opacity ("Opacity", Range(0, 1)) = 0.88
        _FoamColor ("FoamColor", Color) = (0.9882353,0.9686275,0.8823529,1)
        _FoamTexture ("FoamTexture", 2D) = "white" {}
        _FoamFactor ("FoamFactor", Range(0, 5)) = 2.410218
        _FoamNoise ("FoamNoise", 2D) = "white" {}
        _AtenuationMap ("AtenuationMap", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
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
            uniform sampler2D _GrabTexture;
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _Difuse; uniform float4 _Difuse_ST;
            uniform sampler2D _DisplaceMap; uniform float4 _DisplaceMap_ST;
            uniform sampler2D _Bump; uniform float4 _Bump_ST;
            uniform samplerCUBE _Cube;
            uniform sampler2D _Bump2; uniform float4 _Bump2_ST;
            uniform sampler2D _DisplaceMap2; uniform float4 _DisplaceMap2_ST;
            uniform sampler2D _FoamTexture; uniform float4 _FoamTexture_ST;
            uniform sampler2D _FoamNoise; uniform float4 _FoamNoise_ST;
            uniform sampler2D _AtenuationMap; uniform float4 _AtenuationMap_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Specular)
                UNITY_DEFINE_INSTANCED_PROP( float, _High_Frequency_Heigth)
                UNITY_DEFINE_INSTANCED_PROP( float, _Fresnel)
                UNITY_DEFINE_INSTANCED_PROP( float, _Reflection)
                UNITY_DEFINE_INSTANCED_PROP( float, _Turbidity)
                UNITY_DEFINE_INSTANCED_PROP( float, _Refraction)
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _VertexDisplacement)
                UNITY_DEFINE_INSTANCED_PROP( float4, _FoamColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _FoamFactor)
                UNITY_DEFINE_INSTANCED_PROP( float, _Low_Frequency_Heigth)
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
                float4 projPos : TEXCOORD7;
                UNITY_FOG_COORDS(8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
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
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float4 node_48 = _Time;
                float node_906 = (_Speed_var*node_48.g);
                float node_3210 = 0.1;
                float _Low_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Low_Frequency_Heigth );
                float node_1085 = 2.0;
                float node_913 = (_Speed_var*node_48.r);
                float2 node_1041 = ((node_913*(-1.0))+o.uv0);
                float4 _DisplaceMap2_var = tex2Dlod(_DisplaceMap2,float4(TRANSFORM_TEX(node_1041, _DisplaceMap2),0.0,0));
                float _High_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _High_Frequency_Heigth );
                float2 node_68 = (node_913+o.uv0);
                float4 _DisplaceMap_var = tex2Dlod(_DisplaceMap,float4(TRANSFORM_TEX(node_68, _DisplaceMap),0.0,0));
                float3 node_71 = ((sin((node_906+mul(unity_ObjectToWorld, v.vertex).r))*sin(((mul(unity_ObjectToWorld, v.vertex).b*node_3210)+(node_906*node_3210)))*_Low_Frequency_Heigth_var)+(node_1085*_DisplaceMap2_var.rgb*_High_Frequency_Heigth_var)+(_High_Frequency_Heigth_var*_DisplaceMap_var.rgb*node_1085));
                float _VertexDisplacement_var = UNITY_ACCESS_INSTANCED_PROP( Props, _VertexDisplacement );
                float4 _AtenuationMap_var = tex2Dlod(_AtenuationMap,float4(TRANSFORM_TEX(o.uv0, _AtenuationMap),0.0,0));
                v.vertex.xyz += (node_71*_VertexDisplacement_var*v.normal*_AtenuationMap_var.rgb);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float4 node_48 = _Time;
                float node_913 = (_Speed_var*node_48.r);
                float2 node_68 = (node_913+i.uv0);
                float3 _Bump_var = UnpackNormal(tex2D(_Bump,TRANSFORM_TEX(node_68, _Bump)));
                float2 node_1041 = ((node_913*(-1.0))+i.uv0);
                float3 _Bump2_var = UnpackNormal(tex2D(_Bump2,TRANSFORM_TEX(node_1041, _Bump2)));
                float3 node_960 = normalize(lerp(_Bump_var.rgb,_Bump2_var.rgb,0.5)); // To normal
                float3 normalLocal = node_960;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float _Refraction_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Refraction );
                float _Turbidity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Turbidity );
                float node_850 = saturate((sceneZ-partZ)/((1.0 - _Turbidity_var)*5.0));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (_Bump_var.rgb.rg*_Refraction_var*node_850);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float _Specular_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Specular ); // To Specular and Gloss
                float gloss = _Specular_var;
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
                float _Fresnel_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Fresnel );
                float _Reflection_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Reflection );
                float3 node_250 = (texCUBE(_Cube,viewReflectDirection).rgb*(((1.0-max(0,dot(normalDirection, viewDirection)))*_Fresnel_var)+(1.0 - _Fresnel_var))*_Reflection_var); // To Specular Reflection
                float3 specularColor = float3(_Specular_var,_Specular_var,_Specular_var);
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*normTerm*specularColor;
                float3 indirectSpecular = (0 + node_250)*specularColor;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float4 _FoamColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FoamColor );
                float2 node_2253 = i.uv0.rg;
                float node_906 = (_Speed_var*node_48.g);
                float2 node_2262 = float2((node_2253.r+(sin(node_906)*0.02)),node_2253.g);
                float3 node_2939 = lerp(node_960,float3(0,0,1),0.9);
                float3 node_2928 = (float3(node_2262,0.0)+node_2939);
                float4 _FoamTexture_var = tex2D(_FoamTexture,TRANSFORM_TEX(node_2928, _FoamTexture));
                float _FoamFactor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FoamFactor );
                float node_1712 = (1.0 - saturate((sceneZ-partZ)/_FoamFactor_var));
                float3 node_2776 = (float3(node_2262,0.0)+(0.05*node_48.g)+node_960+node_2939);
                float4 _FoamNoise_var = tex2D(_FoamNoise,TRANSFORM_TEX(node_2776, _FoamNoise));
                float3 node_1257 = (_FoamColor_var.rgb*_FoamTexture_var.rgb*node_1712*_FoamNoise_var.rgb); // To emit
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float4 _Difuse_var = tex2D(_Difuse,TRANSFORM_TEX(i.uv0, _Difuse));
                float node_3869 = (_Speed_var*(node_48.g+(-1.0)));
                float node_3879 = 0.1;
                float node_3210 = 0.1;
                float _Low_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Low_Frequency_Heigth );
                float node_1085 = 2.0;
                float4 _DisplaceMap2_var = tex2D(_DisplaceMap2,TRANSFORM_TEX(node_1041, _DisplaceMap2));
                float _High_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _High_Frequency_Heigth );
                float4 _DisplaceMap_var = tex2D(_DisplaceMap,TRANSFORM_TEX(node_68, _DisplaceMap));
                float3 node_71 = ((sin((node_906+i.posWorld.r))*sin(((i.posWorld.b*node_3210)+(node_906*node_3210)))*_Low_Frequency_Heigth_var)+(node_1085*_DisplaceMap2_var.rgb*_High_Frequency_Heigth_var)+(_High_Frequency_Heigth_var*_DisplaceMap_var.rgb*node_1085));
                float node_3487 = 10.0;
                float3 node_4031 = ((((sin((node_3869+i.posWorld.r))*sin(((i.posWorld.b*node_3879)+(node_3869*node_3879))))+_FoamNoise_var.rgb+(-0.9))+(pow(node_71,node_3487)*node_3487))*0.1*_High_Frequency_Heigth_var);
                float3 diffuseColor = (node_1257+(_Color_var.rgb*_Difuse_var.rgb)+node_4031);
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = node_1257;
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                float _Opacity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity );
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,((node_850*_Opacity_var)+(dot(node_1257,float3(0.3,0.59,0.11))*node_1712))),1);
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
            uniform sampler2D _GrabTexture;
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _Difuse; uniform float4 _Difuse_ST;
            uniform sampler2D _DisplaceMap; uniform float4 _DisplaceMap_ST;
            uniform sampler2D _Bump; uniform float4 _Bump_ST;
            uniform sampler2D _Bump2; uniform float4 _Bump2_ST;
            uniform sampler2D _DisplaceMap2; uniform float4 _DisplaceMap2_ST;
            uniform sampler2D _FoamTexture; uniform float4 _FoamTexture_ST;
            uniform sampler2D _FoamNoise; uniform float4 _FoamNoise_ST;
            uniform sampler2D _AtenuationMap; uniform float4 _AtenuationMap_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Specular)
                UNITY_DEFINE_INSTANCED_PROP( float, _High_Frequency_Heigth)
                UNITY_DEFINE_INSTANCED_PROP( float, _Turbidity)
                UNITY_DEFINE_INSTANCED_PROP( float, _Refraction)
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _VertexDisplacement)
                UNITY_DEFINE_INSTANCED_PROP( float4, _FoamColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _FoamFactor)
                UNITY_DEFINE_INSTANCED_PROP( float, _Low_Frequency_Heigth)
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
                float4 projPos : TEXCOORD7;
                LIGHTING_COORDS(8,9)
                UNITY_FOG_COORDS(10)
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
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float4 node_48 = _Time;
                float node_906 = (_Speed_var*node_48.g);
                float node_3210 = 0.1;
                float _Low_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Low_Frequency_Heigth );
                float node_1085 = 2.0;
                float node_913 = (_Speed_var*node_48.r);
                float2 node_1041 = ((node_913*(-1.0))+o.uv0);
                float4 _DisplaceMap2_var = tex2Dlod(_DisplaceMap2,float4(TRANSFORM_TEX(node_1041, _DisplaceMap2),0.0,0));
                float _High_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _High_Frequency_Heigth );
                float2 node_68 = (node_913+o.uv0);
                float4 _DisplaceMap_var = tex2Dlod(_DisplaceMap,float4(TRANSFORM_TEX(node_68, _DisplaceMap),0.0,0));
                float3 node_71 = ((sin((node_906+mul(unity_ObjectToWorld, v.vertex).r))*sin(((mul(unity_ObjectToWorld, v.vertex).b*node_3210)+(node_906*node_3210)))*_Low_Frequency_Heigth_var)+(node_1085*_DisplaceMap2_var.rgb*_High_Frequency_Heigth_var)+(_High_Frequency_Heigth_var*_DisplaceMap_var.rgb*node_1085));
                float _VertexDisplacement_var = UNITY_ACCESS_INSTANCED_PROP( Props, _VertexDisplacement );
                float4 _AtenuationMap_var = tex2Dlod(_AtenuationMap,float4(TRANSFORM_TEX(o.uv0, _AtenuationMap),0.0,0));
                v.vertex.xyz += (node_71*_VertexDisplacement_var*v.normal*_AtenuationMap_var.rgb);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float4 node_48 = _Time;
                float node_913 = (_Speed_var*node_48.r);
                float2 node_68 = (node_913+i.uv0);
                float3 _Bump_var = UnpackNormal(tex2D(_Bump,TRANSFORM_TEX(node_68, _Bump)));
                float2 node_1041 = ((node_913*(-1.0))+i.uv0);
                float3 _Bump2_var = UnpackNormal(tex2D(_Bump2,TRANSFORM_TEX(node_1041, _Bump2)));
                float3 node_960 = normalize(lerp(_Bump_var.rgb,_Bump2_var.rgb,0.5)); // To normal
                float3 normalLocal = node_960;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float _Refraction_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Refraction );
                float _Turbidity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Turbidity );
                float node_850 = saturate((sceneZ-partZ)/((1.0 - _Turbidity_var)*5.0));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (_Bump_var.rgb.rg*_Refraction_var*node_850);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float _Specular_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Specular ); // To Specular and Gloss
                float gloss = _Specular_var;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Specular_var,_Specular_var,_Specular_var);
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*normTerm*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _FoamColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FoamColor );
                float2 node_2253 = i.uv0.rg;
                float node_906 = (_Speed_var*node_48.g);
                float2 node_2262 = float2((node_2253.r+(sin(node_906)*0.02)),node_2253.g);
                float3 node_2939 = lerp(node_960,float3(0,0,1),0.9);
                float3 node_2928 = (float3(node_2262,0.0)+node_2939);
                float4 _FoamTexture_var = tex2D(_FoamTexture,TRANSFORM_TEX(node_2928, _FoamTexture));
                float _FoamFactor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FoamFactor );
                float node_1712 = (1.0 - saturate((sceneZ-partZ)/_FoamFactor_var));
                float3 node_2776 = (float3(node_2262,0.0)+(0.05*node_48.g)+node_960+node_2939);
                float4 _FoamNoise_var = tex2D(_FoamNoise,TRANSFORM_TEX(node_2776, _FoamNoise));
                float3 node_1257 = (_FoamColor_var.rgb*_FoamTexture_var.rgb*node_1712*_FoamNoise_var.rgb); // To emit
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float4 _Difuse_var = tex2D(_Difuse,TRANSFORM_TEX(i.uv0, _Difuse));
                float node_3869 = (_Speed_var*(node_48.g+(-1.0)));
                float node_3879 = 0.1;
                float node_3210 = 0.1;
                float _Low_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Low_Frequency_Heigth );
                float node_1085 = 2.0;
                float4 _DisplaceMap2_var = tex2D(_DisplaceMap2,TRANSFORM_TEX(node_1041, _DisplaceMap2));
                float _High_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _High_Frequency_Heigth );
                float4 _DisplaceMap_var = tex2D(_DisplaceMap,TRANSFORM_TEX(node_68, _DisplaceMap));
                float3 node_71 = ((sin((node_906+i.posWorld.r))*sin(((i.posWorld.b*node_3210)+(node_906*node_3210)))*_Low_Frequency_Heigth_var)+(node_1085*_DisplaceMap2_var.rgb*_High_Frequency_Heigth_var)+(_High_Frequency_Heigth_var*_DisplaceMap_var.rgb*node_1085));
                float node_3487 = 10.0;
                float3 node_4031 = ((((sin((node_3869+i.posWorld.r))*sin(((i.posWorld.b*node_3879)+(node_3869*node_3879))))+_FoamNoise_var.rgb+(-0.9))+(pow(node_71,node_3487)*node_3487))*0.1*_High_Frequency_Heigth_var);
                float3 diffuseColor = (node_1257+(_Color_var.rgb*_Difuse_var.rgb)+node_4031);
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                float _Opacity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity );
                fixed4 finalRGBA = fixed4(finalColor * ((node_850*_Opacity_var)+(dot(node_1257,float3(0.3,0.59,0.11))*node_1712)),0);
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
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #pragma multi_compile_instancing
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
            #pragma target 3.0
            uniform sampler2D _DisplaceMap; uniform float4 _DisplaceMap_ST;
            uniform sampler2D _DisplaceMap2; uniform float4 _DisplaceMap2_ST;
            uniform sampler2D _AtenuationMap; uniform float4 _AtenuationMap_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _High_Frequency_Heigth)
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _VertexDisplacement)
                UNITY_DEFINE_INSTANCED_PROP( float, _Low_Frequency_Heigth)
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
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                float2 uv2 : TEXCOORD3;
                float4 posWorld : TEXCOORD4;
                float3 normalDir : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float4 node_48 = _Time;
                float node_906 = (_Speed_var*node_48.g);
                float node_3210 = 0.1;
                float _Low_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Low_Frequency_Heigth );
                float node_1085 = 2.0;
                float node_913 = (_Speed_var*node_48.r);
                float2 node_1041 = ((node_913*(-1.0))+o.uv0);
                float4 _DisplaceMap2_var = tex2Dlod(_DisplaceMap2,float4(TRANSFORM_TEX(node_1041, _DisplaceMap2),0.0,0));
                float _High_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _High_Frequency_Heigth );
                float2 node_68 = (node_913+o.uv0);
                float4 _DisplaceMap_var = tex2Dlod(_DisplaceMap,float4(TRANSFORM_TEX(node_68, _DisplaceMap),0.0,0));
                float3 node_71 = ((sin((node_906+mul(unity_ObjectToWorld, v.vertex).r))*sin(((mul(unity_ObjectToWorld, v.vertex).b*node_3210)+(node_906*node_3210)))*_Low_Frequency_Heigth_var)+(node_1085*_DisplaceMap2_var.rgb*_High_Frequency_Heigth_var)+(_High_Frequency_Heigth_var*_DisplaceMap_var.rgb*node_1085));
                float _VertexDisplacement_var = UNITY_ACCESS_INSTANCED_PROP( Props, _VertexDisplacement );
                float4 _AtenuationMap_var = tex2Dlod(_AtenuationMap,float4(TRANSFORM_TEX(o.uv0, _AtenuationMap),0.0,0));
                v.vertex.xyz += (node_71*_VertexDisplacement_var*v.normal*_AtenuationMap_var.rgb);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
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
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
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
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _Difuse; uniform float4 _Difuse_ST;
            uniform sampler2D _DisplaceMap; uniform float4 _DisplaceMap_ST;
            uniform sampler2D _Bump; uniform float4 _Bump_ST;
            uniform sampler2D _Bump2; uniform float4 _Bump2_ST;
            uniform sampler2D _DisplaceMap2; uniform float4 _DisplaceMap2_ST;
            uniform sampler2D _FoamTexture; uniform float4 _FoamTexture_ST;
            uniform sampler2D _FoamNoise; uniform float4 _FoamNoise_ST;
            uniform sampler2D _AtenuationMap; uniform float4 _AtenuationMap_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Specular)
                UNITY_DEFINE_INSTANCED_PROP( float, _High_Frequency_Heigth)
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _VertexDisplacement)
                UNITY_DEFINE_INSTANCED_PROP( float4, _FoamColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _FoamFactor)
                UNITY_DEFINE_INSTANCED_PROP( float, _Low_Frequency_Heigth)
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
                float4 projPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float4 node_48 = _Time;
                float node_906 = (_Speed_var*node_48.g);
                float node_3210 = 0.1;
                float _Low_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Low_Frequency_Heigth );
                float node_1085 = 2.0;
                float node_913 = (_Speed_var*node_48.r);
                float2 node_1041 = ((node_913*(-1.0))+o.uv0);
                float4 _DisplaceMap2_var = tex2Dlod(_DisplaceMap2,float4(TRANSFORM_TEX(node_1041, _DisplaceMap2),0.0,0));
                float _High_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _High_Frequency_Heigth );
                float2 node_68 = (node_913+o.uv0);
                float4 _DisplaceMap_var = tex2Dlod(_DisplaceMap,float4(TRANSFORM_TEX(node_68, _DisplaceMap),0.0,0));
                float3 node_71 = ((sin((node_906+mul(unity_ObjectToWorld, v.vertex).r))*sin(((mul(unity_ObjectToWorld, v.vertex).b*node_3210)+(node_906*node_3210)))*_Low_Frequency_Heigth_var)+(node_1085*_DisplaceMap2_var.rgb*_High_Frequency_Heigth_var)+(_High_Frequency_Heigth_var*_DisplaceMap_var.rgb*node_1085));
                float _VertexDisplacement_var = UNITY_ACCESS_INSTANCED_PROP( Props, _VertexDisplacement );
                float4 _AtenuationMap_var = tex2Dlod(_AtenuationMap,float4(TRANSFORM_TEX(o.uv0, _AtenuationMap),0.0,0));
                v.vertex.xyz += (node_71*_VertexDisplacement_var*v.normal*_AtenuationMap_var.rgb);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _FoamColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FoamColor );
                float2 node_2253 = i.uv0.rg;
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float4 node_48 = _Time;
                float node_906 = (_Speed_var*node_48.g);
                float2 node_2262 = float2((node_2253.r+(sin(node_906)*0.02)),node_2253.g);
                float node_913 = (_Speed_var*node_48.r);
                float2 node_68 = (node_913+i.uv0);
                float3 _Bump_var = UnpackNormal(tex2D(_Bump,TRANSFORM_TEX(node_68, _Bump)));
                float2 node_1041 = ((node_913*(-1.0))+i.uv0);
                float3 _Bump2_var = UnpackNormal(tex2D(_Bump2,TRANSFORM_TEX(node_1041, _Bump2)));
                float3 node_960 = normalize(lerp(_Bump_var.rgb,_Bump2_var.rgb,0.5)); // To normal
                float3 node_2939 = lerp(node_960,float3(0,0,1),0.9);
                float3 node_2928 = (float3(node_2262,0.0)+node_2939);
                float4 _FoamTexture_var = tex2D(_FoamTexture,TRANSFORM_TEX(node_2928, _FoamTexture));
                float _FoamFactor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FoamFactor );
                float node_1712 = (1.0 - saturate((sceneZ-partZ)/_FoamFactor_var));
                float3 node_2776 = (float3(node_2262,0.0)+(0.05*node_48.g)+node_960+node_2939);
                float4 _FoamNoise_var = tex2D(_FoamNoise,TRANSFORM_TEX(node_2776, _FoamNoise));
                float3 node_1257 = (_FoamColor_var.rgb*_FoamTexture_var.rgb*node_1712*_FoamNoise_var.rgb); // To emit
                o.Emission = node_1257;
                
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float4 _Difuse_var = tex2D(_Difuse,TRANSFORM_TEX(i.uv0, _Difuse));
                float node_3869 = (_Speed_var*(node_48.g+(-1.0)));
                float node_3879 = 0.1;
                float node_3210 = 0.1;
                float _Low_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Low_Frequency_Heigth );
                float node_1085 = 2.0;
                float4 _DisplaceMap2_var = tex2D(_DisplaceMap2,TRANSFORM_TEX(node_1041, _DisplaceMap2));
                float _High_Frequency_Heigth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _High_Frequency_Heigth );
                float4 _DisplaceMap_var = tex2D(_DisplaceMap,TRANSFORM_TEX(node_68, _DisplaceMap));
                float3 node_71 = ((sin((node_906+i.posWorld.r))*sin(((i.posWorld.b*node_3210)+(node_906*node_3210)))*_Low_Frequency_Heigth_var)+(node_1085*_DisplaceMap2_var.rgb*_High_Frequency_Heigth_var)+(_High_Frequency_Heigth_var*_DisplaceMap_var.rgb*node_1085));
                float node_3487 = 10.0;
                float3 node_4031 = ((((sin((node_3869+i.posWorld.r))*sin(((i.posWorld.b*node_3879)+(node_3869*node_3879))))+_FoamNoise_var.rgb+(-0.9))+(pow(node_71,node_3487)*node_3487))*0.1*_High_Frequency_Heigth_var);
                float3 diffColor = (node_1257+(_Color_var.rgb*_Difuse_var.rgb)+node_4031);
                float _Specular_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Specular ); // To Specular and Gloss
                float3 specColor = float3(_Specular_var,_Specular_var,_Specular_var);
                float roughness = 1.0 - _Specular_var;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
