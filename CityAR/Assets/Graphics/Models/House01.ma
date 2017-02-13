//Maya ASCII 2016 scene
//Name: House01.ma
//Last modified: Thu, Feb 09, 2017 04:33:01 PM
//Codeset: 1252
requires maya "2016";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2016";
fileInfo "version" "2016";
fileInfo "cutIdentifier" "201502261600-953408";
fileInfo "osv" "Microsoft Windows 8 , 64-bit  (Build 9200)\n";
fileInfo "license" "education";
createNode transform -s -n "persp";
	rename -uid "2CABA0FE-409E-E936-2593-3ABA07BCB676";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 30.837188801519964 17.372096070720023 47.497596251111325 ;
	setAttr ".r" -type "double3" -11.138352729611107 42.599999999988114 1.0802089619414418e-015 ;
createNode camera -s -n "perspShape" -p "persp";
	rename -uid "46C0BB2C-43FE-7349-0B38-4EBBF6CF775E";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".coi" 53.874564571973259;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".tp" -type "double3" 0 4.9209778289506154 0 ;
	setAttr ".hc" -type "string" "viewSet -p %camera";
createNode transform -s -n "top";
	rename -uid "319CDFCD-4D0D-518F-75C1-ACBF0DF9D25A";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 100.1 0 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	rename -uid "514B703D-4237-69D5-1C1F-D99F95E48273";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
createNode transform -s -n "front";
	rename -uid "EC7B708D-4C68-F096-F2CD-B88F20EF3C78";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -1.1920928955078125e-007 11.617282186517976 100.69394935067014 ;
createNode camera -s -n "frontShape" -p "front";
	rename -uid "A31453A3-4E3E-ADB6-5AB8-909995ACBB10";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 8.7023921306866967;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
createNode transform -s -n "side";
	rename -uid "F069B224-495C-288D-DC36-D98C97BF68EF";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 100.7752706375268 10.977659542928002 3.9999999985099062 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	rename -uid "94CB5B53-4A89-004E-877C-D6B61B4D72A2";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 15.387791331161942;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
createNode transform -n "pCube1";
	rename -uid "8554589F-4639-6474-1902-D49E84D91157";
	setAttr ".t" -type "double3" 0 2 0 ;
createNode mesh -n "pCubeShape1" -p "pCube1";
	rename -uid "EDE9CC61-4E42-6692-7A59-91843810E503";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "pCube2";
	rename -uid "A3F11E9E-4C15-2BB1-7085-5BB19F2795B3";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 2 4.1 ;
createNode mesh -n "pCubeShape2" -p "pCube2";
	rename -uid "7FC9CF10-4C1E-ED8B-1D1C-16B2757EDF1D";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "pCube3";
	rename -uid "BAC42206-4E5D-EDD1-B0F5-0CB5E81360D0";
	setAttr ".t" -type "double3" 0 0.25 5 ;
createNode mesh -n "pCubeShape3" -p "pCube3";
	rename -uid "085C8519-4944-B5DE-37DF-65BD8B0B806F";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.0625 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "pCube4";
	rename -uid "1BC695D3-45F5-0B3C-225F-81BDA6A3DBED";
	setAttr ".t" -type "double3" 0 3.6762072945932052 5.1 ;
	setAttr ".r" -type "double3" 0 0 45 ;
createNode mesh -n "pCubeShape4" -p "pCube4";
	rename -uid "32026569-42ED-CE70-1DEF-9EABCE7061F8";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.375 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 6 ".pt[0:5]" -type "float3"  0.39998874 -0.39998874 -5.9952043e-014 
		-0.39998874 0.39998874 -5.9952043e-014 2.7533531e-014 1.9872992e-014 -5.9952043e-014 
		-0.39998874 0.39998874 -5.3290705e-014 2.7533531e-014 1.9872992e-014 -5.3290705e-014 
		0.39998874 -0.39998874 -5.3290705e-014;
createNode transform -n "pCube5";
	rename -uid "613F1AFB-47AB-CC60-CC16-8A925E9A134F";
	setAttr ".t" -type "double3" -1 2.1 5.8 ;
createNode mesh -n "pCubeShape5" -p "pCube5";
	rename -uid "EC28DCDF-426B-962F-5025-089E5B98CA35";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "pCube6";
	rename -uid "C6C8D6A2-4F72-346F-ED46-EB890FAB5318";
	setAttr ".t" -type "double3" 1 2.1 5.8 ;
createNode mesh -n "pCubeShape6" -p "pCube6";
	rename -uid "EF7914C7-40E0-CFB2-0E4F-3C92E870922A";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 14 ".uvst[0].uvsp[0:13]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 8 ".vt[0:7]"  -0.1 -1.60000002 0.1 0.1 -1.60000002 0.1
		 -0.1 1.60000002 0.1 0.1 1.60000002 0.1 -0.1 1.60000002 -0.1 0.1 1.60000002 -0.1 -0.1 -1.60000002 -0.1
		 0.1 -1.60000002 -0.1;
	setAttr -s 12 ".ed[0:11]"  0 1 0 2 3 0 4 5 0 6 7 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0;
	setAttr -s 6 -ch 24 ".fc[0:5]" -type "polyFaces" 
		f 4 0 5 -2 -5
		mu 0 4 0 1 3 2
		f 4 1 7 -3 -7
		mu 0 4 2 3 5 4
		f 4 2 9 -4 -9
		mu 0 4 4 5 7 6
		f 4 3 11 -1 -11
		mu 0 4 6 7 9 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "pCube7";
	rename -uid "8A80C0D1-411D-75B5-873E-AD939EBCE13D";
	setAttr ".t" -type "double3" 0 4 0 ;
	setAttr ".r" -type "double3" 0 0 45 ;
createNode mesh -n "pCubeShape7" -p "pCube7";
	rename -uid "DFC277A6-45D8-1376-35A5-9EAF7DF55705";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.375 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 6 ".pt[0:5]" -type "float3"  3.0404372 -3.0404372 0 -3.0404372 
		3.0404372 0 8.5226555 8.5226555 0 -3.0404372 3.0404372 0 8.5226555 8.5226555 0 3.0404372 
		-3.0404372 0;
createNode transform -n "pCube8";
	rename -uid "555A59E2-4DE5-B0C9-B79D-1BAE3A11271F";
	setAttr ".t" -type "double3" 0 2.1157218687521828 0 ;
	setAttr ".r" -type "double3" 0 0 45 ;
	setAttr ".s" -type "double3" 1.146037936835435 1.146037936835435 1.146037936835435 ;
createNode mesh -n "pCubeShape8" -p "pCube8";
	rename -uid "F2BAA9CA-4462-83F0-34E5-0B97CDF77288";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.375 0.375 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 12 ".pt[0:11]" -type "float3"  -4.4408921e-015 4.4408921e-015 
		0.12686668 0 1.1920929e-007 0.12686668 -1.7763568e-014 -1.7763568e-014 0.12686668 
		0 1.1920929e-007 -0.12686668 -1.7763568e-014 -1.7763568e-014 -0.12686668 -4.4408921e-015 
		4.4408921e-015 -0.12686668 4.4408921e-015 -4.4408921e-015 0.12686668 -1.7763568e-014 
		-1.7763568e-014 0.12686668 -1.7763568e-014 -1.7763568e-014 -0.12686668 4.4408921e-015 
		-4.4408921e-015 -0.12686668 -4.4408921e-015 4.4408921e-015 -0.12686668 -4.4408921e-015 
		4.4408921e-015 0.12686668;
createNode mesh -n "polySurfaceShape1" -p "pCube8";
	rename -uid "D8DF383F-435D-34BF-539D-3C97F4CF1B5C";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.625 0.25 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 10 ".uvst[0].uvsp[0:9]" -type "float2" 0.625 0 0.375 0.25
		 0.625 0.25 0.375 0.5 0.625 0.5 0.625 0.75 0.625 1 0.875 0 0.875 0.25 0.125 0.25;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 6 ".pt[0:5]" -type "float3"  3.0404372 -3.0404372 0.13318977 
		-3.0404372 3.0404372 0.13318977 8.5226555 8.5226555 0.13318977 -3.0404372 3.0404372 
		-0.13318977 8.5226555 8.5226555 -0.13318977 3.0404372 -3.0404372 -0.13318977;
	setAttr -s 6 ".vt[0:5]"  0.5 -0.5 4 -0.5 0.5 4 0.5 0.5 4 -0.5 0.5 -4
		 0.5 0.5 -4 0.5 -0.5 -4;
	setAttr -s 9 ".ed[0:8]"  1 2 0 3 4 0 0 1 0 0 2 0 1 3 0 2 4 0 3 5 0
		 4 5 0 5 0 0;
	setAttr -s 5 -ch 18 ".fc[0:4]" -type "polyFaces" 
		f 3 3 -1 -3
		mu 0 3 0 2 1
		f 4 0 5 -2 -5
		mu 0 4 1 2 4 3
		f 3 1 7 -7
		mu 0 3 3 4 5
		f 4 -9 -8 -6 -4
		mu 0 4 0 7 8 2
		f 4 2 4 6 8
		mu 0 4 6 1 9 5;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "pCube9";
	rename -uid "9D3BBD44-4DDD-6568-34CE-46B35B021973";
	setAttr ".t" -type "double3" 0 3.5450015592921695 5.1 ;
	setAttr ".r" -type "double3" 0 0 45 ;
	setAttr ".s" -type "double3" 1.1655327033389209 1.1655327033389209 1.1655327033389209 ;
createNode mesh -n "pCubeShape9" -p "pCube9";
	rename -uid "7F6DB9CD-4601-B474-5305-C6B186DC2A38";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.625 0.25 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode mesh -n "polySurfaceShape2" -p "pCube9";
	rename -uid "F1F5E6E1-4DBF-C729-E8B0-D39CE6F7EC30";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.625 0.25 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 10 ".uvst[0].uvsp[0:9]" -type "float2" 0.625 0 0.375 0.25
		 0.625 0.25 0.375 0.5 0.625 0.5 0.625 0.75 0.625 1 0.875 0 0.875 0.25 0.125 0.25;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 6 ".pt[0:5]" -type "float3"  0.39998874 -0.39998874 -5.9952043e-014 
		-0.39998874 0.39998874 -5.9952043e-014 2.7533531e-014 1.9872992e-014 -5.9952043e-014 
		-0.39998874 0.39998874 -5.3290705e-014 2.7533531e-014 1.9872992e-014 -5.3290705e-014 
		0.39998874 -0.39998874 -5.3290705e-014;
	setAttr -s 6 ".vt[0:5]"  0.5 -0.5 1.10000002 -0.5 0.5 1.10000002 0.5 0.5 1.10000002
		 -0.5 0.5 -1.10000002 0.5 0.5 -1.10000002 0.5 -0.5 -1.10000002;
	setAttr -s 9 ".ed[0:8]"  1 2 0 3 4 0 0 1 0 0 2 0 1 3 0 2 4 0 3 5 0
		 4 5 0 5 0 0;
	setAttr -s 5 -ch 18 ".fc[0:4]" -type "polyFaces" 
		f 3 3 -1 -3
		mu 0 3 0 2 1
		f 4 0 5 -2 -5
		mu 0 4 1 2 4 3
		f 3 1 7 -7
		mu 0 3 3 4 5
		f 4 -9 -8 -6 -4
		mu 0 4 0 7 8 2
		f 4 2 4 6 8
		mu 0 4 6 1 9 5;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "pCube10";
	rename -uid "AE1B3988-4028-DE7E-CB8B-7798BDA9261F";
	setAttr ".t" -type "double3" -3 2.5 4.1 ;
createNode mesh -n "pCubeShape10" -p "pCube10";
	rename -uid "80EE1246-4179-9E8D-7A31-E7A8FED88639";
	setAttr -k off ".v";
	setAttr -av ".iog[0].og[0].gid";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.375 0.125 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 18 ".pt";
	setAttr ".pt[9]" -type "float3" -2.5331974e-007 0 0 ;
	setAttr ".pt[10]" -type "float3" -2.5331974e-007 0 0 ;
	setAttr ".pt[15]" -type "float3" 1.0430813e-007 0 0 ;
	setAttr ".pt[20]" -type "float3" 1.0430813e-007 0 0 ;
	setAttr ".pt[37]" -type "float3" 0 7.4505806e-009 0 ;
	setAttr ".pt[38]" -type "float3" 0 7.4505806e-009 0 ;
createNode transform -n "pCube11";
	rename -uid "88F08E04-4734-2B2A-8980-83B45B2B2E0B";
	setAttr ".t" -type "double3" 3 2.5 4.1 ;
createNode mesh -n "pCubeShape11" -p "pCube11";
	rename -uid "C79E04C6-4BA4-04F3-B844-42B3F6013524";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.375 0.125 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 46 ".uvst[0].uvsp[0:45]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25 0.375 0 0.625 0 0.625 0.25 0.375 0.25 0.375 0 0.625
		 0 0.625 0.25 0.375 0.25 0.54166663 0.25 0.54166663 0 0.45833331 0.25 0.45833331 0
		 0.375 0.16666666 0.45833331 0.16666666 0.375 0.083333328 0.45833331 0.083333328 0.54166663
		 0.16666666 0.625 0.16666666 0.54166663 0.083333328 0.625 0.083333328 0.45833331 0.16666666
		 0.375 0.16666666 0.375 0.083333328 0.45833331 0.083333328 0.625 0.16666666 0.54166663
		 0.16666666 0.54166663 0.083333328 0.625 0.083333328 0.54166663 0 0.54166663 0.25
		 0.45833331 0.25 0.45833331 0;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 18 ".pt";
	setAttr ".pt[9]" -type "float3" -2.5331974e-007 0 0 ;
	setAttr ".pt[10]" -type "float3" -2.5331974e-007 0 0 ;
	setAttr ".pt[15]" -type "float3" 1.0430813e-007 0 0 ;
	setAttr ".pt[20]" -type "float3" 1.0430813e-007 0 0 ;
	setAttr ".pt[37]" -type "float3" 0 7.4505806e-009 0 ;
	setAttr ".pt[38]" -type "float3" 0 7.4505806e-009 0 ;
	setAttr -s 40 ".vt[0:39]"  -0.70000005 -2 0.099999905 0.70000005 -2 0.099999905
		 -0.70000005 2 0.099999905 0.70000005 2 0.099999905 -0.70000005 2 -0.099999905 0.70000005 2 -0.099999905
		 -0.70000005 -2 -0.099999905 0.70000005 -2 -0.099999905 -0.60000014 -1.89999998 0.099999905
		 0.5999999 -1.89999998 0.099999905 0.5999999 1.9000001 0.099999905 -0.60000014 1.9000001 0.099999905
		 -0.60000014 -1.89999998 -0.01611948 0.60000014 -1.89999998 -0.01611948 0.60000014 1.9000001 -0.01611948
		 -0.60000002 1.9000001 -0.01611948 0.047219753 1.89999962 -0.016119957 0.047219753 -1.9000001 -0.016119957
		 -0.047219276 1.89999962 -0.016119957 -0.047219276 -1.9000001 -0.016119957 -0.59999979 0.053865433 -0.01611948
		 -0.047219276 0.053865433 -0.016120434 -0.5999999 -0.05386591 -0.01611948 -0.047219276 -0.05386591 -0.016120434
		 0.047219753 0.053865433 -0.016120434 0.60000038 0.053865433 -0.01611948 0.047219753 -0.05386591 -0.016120434
		 0.60000038 -0.05386591 -0.01611948 -0.59999967 0.053865433 0.10017872 -0.047219038 0.053865433 0.10017776
		 -0.59999967 -0.05386591 0.10017872 -0.047219515 -0.05386591 0.10017776 0.047219515 0.053865433 0.10017776
		 0.60000014 0.053865433 0.10017872 0.047219515 -0.05386591 0.10017776 0.60000014 -0.05386591 0.10017872
		 0.047219515 -1.9000001 0.10017824 0.047219515 1.89999962 0.10017824 -0.047219515 1.89999962 0.10017824
		 -0.047219515 -1.9000001 0.10017824;
	setAttr -s 84 ".ed[0:83]"  0 1 0 2 3 0 4 5 0 6 7 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0 8 12 0 9 13 0 10 14 0 11 15 0 14 16 0 13 17 0 18 15 0
		 19 12 0 15 20 0 18 21 0 20 21 0 22 12 0 23 19 0 22 23 0 16 24 0 14 25 0 24 25 0 26 17 0
		 27 13 0 26 27 0 20 28 0 21 29 0 28 29 0 22 30 0 28 30 0 23 31 0 30 31 0 29 31 0 24 32 0
		 25 33 0 32 33 0 26 34 0 32 34 0 27 35 0 34 35 0 33 35 0 17 36 0 34 36 0 16 37 0 37 32 0
		 18 38 0 37 38 0 38 29 0 19 39 0 31 39 0 36 39 1 11 38 0 10 37 0 8 39 0 9 36 0 8 30 0
		 11 28 0 10 33 0 9 35 0 0 8 0 39 0 0 1 9 0 36 1 0 1 36 0 0 39 0 2 11 0 10 3 0 38 2 1
		 37 3 1 8 0 0 9 1 0 2 28 1 0 30 1 1 35 1 3 33 1 32 29 1 34 31 1;
	setAttr -s 42 -ch 160 ".fc[0:41]" -type "polyFaces" 
		f 4 1 7 -3 -7
		mu 0 4 2 3 5 4
		f 4 2 9 -4 -9
		mu 0 4 4 5 7 6
		f 4 3 11 -1 -11
		mu 0 4 6 7 9 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13
		f 4 18 20 22 -22
		mu 0 4 24 21 26 27
		f 4 -35 36 38 -40
		mu 0 4 34 35 36 37
		f 4 -26 23 -20 -25
		mu 0 4 29 28 18 25
		f 4 16 26 28 -28
		mu 0 4 20 22 30 31
		f 4 -43 44 46 -48
		mu 0 4 38 39 40 41
		f 4 -32 29 -18 -31
		mu 0 4 33 32 23 19
		f 4 -50 83 56 -58
		mu 0 4 42 40 37 45
		f 4 -23 32 34 -34
		mu 0 4 27 26 35 34
		f 4 25 37 -39 -36
		mu 0 4 28 29 37 36
		f 4 -29 40 42 -42
		mu 0 4 31 30 39 38
		f 4 31 45 -47 -44
		mu 0 4 32 33 41 40
		f 4 -30 43 49 -49
		mu 0 4 23 32 40 42
		f 4 -27 50 51 -41
		mu 0 4 30 22 43 39
		f 4 21 33 -55 -53
		mu 0 4 24 27 34 44
		f 4 24 55 -57 -38
		mu 0 4 29 25 45 37
		f 4 15 -19 52 -59
		mu 0 4 17 21 24 44
		f 4 -15 59 -51 -17
		mu 0 4 20 16 43 22
		f 4 -13 60 -56 19
		mu 0 4 18 14 45 25
		f 4 13 17 48 -62
		mu 0 4 15 19 23 42
		f 4 12 -24 35 -63
		mu 0 4 14 18 28 36
		f 4 -16 63 -33 -21
		mu 0 4 21 17 35 26
		f 4 14 27 41 -65
		mu 0 4 16 20 31 38
		f 4 -14 65 -46 30
		mu 0 4 19 15 41 33
		f 3 -68 -61 -67
		mu 0 3 8 45 14
		f 3 69 68 61
		mu 0 3 42 9 15
		f 4 0 70 57 -72
		mu 0 4 8 9 42 45
		f 3 74 72 58
		mu 0 3 44 2 17
		f 4 -54 75 -2 -75
		mu 0 4 44 43 3 2
		f 3 -76 -60 73
		mu 0 3 3 43 16
		f 3 -64 -73 78
		mu 0 3 35 17 2
		f 3 80 -66 77
		mu 0 3 1 41 15
		f 4 -79 -5 79 -37
		mu 0 4 35 2 0 36
		f 3 -80 -77 62
		mu 0 3 36 0 14
		f 4 47 -81 5 81
		mu 0 4 38 41 1 3
		f 3 -82 -74 64
		mu 0 3 38 3 16
		f 4 -83 -52 53 54
		mu 0 4 34 39 43 44
		f 4 -84 -45 82 39
		mu 0 4 37 40 39 34;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "pCube12";
	rename -uid "4D8A2900-464C-0072-DBE7-9EB05071E9C6";
	setAttr ".t" -type "double3" 0 2 4.1 ;
createNode mesh -n "pCubeShape12" -p "pCube12";
	rename -uid "DD4B1526-49B1-49C6-D100-6E86CC2759E9";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.375 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode mesh -n "polySurfaceShape3" -p "pCube12";
	rename -uid "1E8B46D5-443D-3810-ABB6-5F8206D0C3BA";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.125 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 14 ".uvst[0].uvsp[0:13]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 8 ".vt[0:7]"  -1 -1.5 0.1 1 -1.5 0.1 -1 1.5 0.1 1 1.5 0.1
		 -1 1.5 -0.1 1 1.5 -0.1 -1 -1.5 -0.1 1 -1.5 -0.1;
	setAttr -s 12 ".ed[0:11]"  0 1 0 2 3 0 4 5 0 6 7 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0;
	setAttr -s 6 -ch 24 ".fc[0:5]" -type "polyFaces" 
		f 4 0 5 -2 -5
		mu 0 4 0 1 3 2
		f 4 1 7 -3 -7
		mu 0 4 2 3 5 4
		f 4 2 9 -4 -9
		mu 0 4 4 5 7 6
		f 4 3 11 -1 -11
		mu 0 4 6 7 9 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "pPipe1";
	rename -uid "2A8B1E34-4410-6843-E7F6-FD9730FD798D";
	setAttr ".t" -type "double3" 0 11.61728236533191 4.1 ;
createNode mesh -n "pPipeShape1" -p "pPipe1";
	rename -uid "BE4066DD-4619-4813-8D88-1793AB65F772";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.47492212057113647 0.75 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "pCube13";
	rename -uid "78FACC14-4E8D-49C1-DE1E-8782A9C06251";
	setAttr ".t" -type "double3" -3 2.5 -4.1 ;
	setAttr ".s" -type "double3" 1 1 -1 ;
createNode mesh -n "pCubeShape13" -p "pCube13";
	rename -uid "ED0831CF-40F7-8DE3-3F47-BE840909929C";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.375 0.125 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 46 ".uvst[0].uvsp[0:45]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25 0.375 0 0.625 0 0.625 0.25 0.375 0.25 0.375 0 0.625
		 0 0.625 0.25 0.375 0.25 0.54166663 0.25 0.54166663 0 0.45833331 0.25 0.45833331 0
		 0.375 0.16666666 0.45833331 0.16666666 0.375 0.083333328 0.45833331 0.083333328 0.54166663
		 0.16666666 0.625 0.16666666 0.54166663 0.083333328 0.625 0.083333328 0.45833331 0.16666666
		 0.375 0.16666666 0.375 0.083333328 0.45833331 0.083333328 0.625 0.16666666 0.54166663
		 0.16666666 0.54166663 0.083333328 0.625 0.083333328 0.54166663 0 0.54166663 0.25
		 0.45833331 0.25 0.45833331 0;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 18 ".pt";
	setAttr ".pt[9]" -type "float3" -2.5331974e-007 0 0 ;
	setAttr ".pt[10]" -type "float3" -2.5331974e-007 0 0 ;
	setAttr ".pt[15]" -type "float3" 1.0430813e-007 0 0 ;
	setAttr ".pt[20]" -type "float3" 1.0430813e-007 0 0 ;
	setAttr ".pt[37]" -type "float3" 0 7.4505806e-009 0 ;
	setAttr ".pt[38]" -type "float3" 0 7.4505806e-009 0 ;
	setAttr -s 40 ".vt[0:39]"  -0.70000005 -2 0.099999905 0.70000005 -2 0.099999905
		 -0.70000005 2 0.099999905 0.70000005 2 0.099999905 -0.70000005 2 -0.099999905 0.70000005 2 -0.099999905
		 -0.70000005 -2 -0.099999905 0.70000005 -2 -0.099999905 -0.60000014 -1.89999998 0.099999905
		 0.5999999 -1.89999998 0.099999905 0.5999999 1.9000001 0.099999905 -0.60000014 1.9000001 0.099999905
		 -0.60000014 -1.89999998 -0.01611948 0.60000014 -1.89999998 -0.01611948 0.60000014 1.9000001 -0.01611948
		 -0.60000002 1.9000001 -0.01611948 0.047219753 1.89999962 -0.016119957 0.047219753 -1.9000001 -0.016119957
		 -0.047219276 1.89999962 -0.016119957 -0.047219276 -1.9000001 -0.016119957 -0.59999979 0.053865433 -0.01611948
		 -0.047219276 0.053865433 -0.016120434 -0.5999999 -0.05386591 -0.01611948 -0.047219276 -0.05386591 -0.016120434
		 0.047219753 0.053865433 -0.016120434 0.60000038 0.053865433 -0.01611948 0.047219753 -0.05386591 -0.016120434
		 0.60000038 -0.05386591 -0.01611948 -0.59999967 0.053865433 0.10017872 -0.047219038 0.053865433 0.10017776
		 -0.59999967 -0.05386591 0.10017872 -0.047219515 -0.05386591 0.10017776 0.047219515 0.053865433 0.10017776
		 0.60000014 0.053865433 0.10017872 0.047219515 -0.05386591 0.10017776 0.60000014 -0.05386591 0.10017872
		 0.047219515 -1.9000001 0.10017824 0.047219515 1.89999962 0.10017824 -0.047219515 1.89999962 0.10017824
		 -0.047219515 -1.9000001 0.10017824;
	setAttr -s 84 ".ed[0:83]"  0 1 0 2 3 0 4 5 0 6 7 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0 8 12 0 9 13 0 10 14 0 11 15 0 14 16 0 13 17 0 18 15 0
		 19 12 0 15 20 0 18 21 0 20 21 0 22 12 0 23 19 0 22 23 0 16 24 0 14 25 0 24 25 0 26 17 0
		 27 13 0 26 27 0 20 28 0 21 29 0 28 29 0 22 30 0 28 30 0 23 31 0 30 31 0 29 31 0 24 32 0
		 25 33 0 32 33 0 26 34 0 32 34 0 27 35 0 34 35 0 33 35 0 17 36 0 34 36 0 16 37 0 37 32 0
		 18 38 0 37 38 0 38 29 0 19 39 0 31 39 0 36 39 1 11 38 0 10 37 0 8 39 0 9 36 0 8 30 0
		 11 28 0 10 33 0 9 35 0 0 8 0 39 0 0 1 9 0 36 1 0 1 36 0 0 39 0 2 11 0 10 3 0 38 2 1
		 37 3 1 8 0 0 9 1 0 2 28 1 0 30 1 1 35 1 3 33 1 32 29 1 34 31 1;
	setAttr -s 42 -ch 160 ".fc[0:41]" -type "polyFaces" 
		f 4 1 7 -3 -7
		mu 0 4 2 3 5 4
		f 4 2 9 -4 -9
		mu 0 4 4 5 7 6
		f 4 3 11 -1 -11
		mu 0 4 6 7 9 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13
		f 4 18 20 22 -22
		mu 0 4 24 21 26 27
		f 4 -35 36 38 -40
		mu 0 4 34 35 36 37
		f 4 -26 23 -20 -25
		mu 0 4 29 28 18 25
		f 4 16 26 28 -28
		mu 0 4 20 22 30 31
		f 4 -43 44 46 -48
		mu 0 4 38 39 40 41
		f 4 -32 29 -18 -31
		mu 0 4 33 32 23 19
		f 4 -50 83 56 -58
		mu 0 4 42 40 37 45
		f 4 -23 32 34 -34
		mu 0 4 27 26 35 34
		f 4 25 37 -39 -36
		mu 0 4 28 29 37 36
		f 4 -29 40 42 -42
		mu 0 4 31 30 39 38
		f 4 31 45 -47 -44
		mu 0 4 32 33 41 40
		f 4 -30 43 49 -49
		mu 0 4 23 32 40 42
		f 4 -27 50 51 -41
		mu 0 4 30 22 43 39
		f 4 21 33 -55 -53
		mu 0 4 24 27 34 44
		f 4 24 55 -57 -38
		mu 0 4 29 25 45 37
		f 4 15 -19 52 -59
		mu 0 4 17 21 24 44
		f 4 -15 59 -51 -17
		mu 0 4 20 16 43 22
		f 4 -13 60 -56 19
		mu 0 4 18 14 45 25
		f 4 13 17 48 -62
		mu 0 4 15 19 23 42
		f 4 12 -24 35 -63
		mu 0 4 14 18 28 36
		f 4 -16 63 -33 -21
		mu 0 4 21 17 35 26
		f 4 14 27 41 -65
		mu 0 4 16 20 31 38
		f 4 -14 65 -46 30
		mu 0 4 19 15 41 33
		f 3 -68 -61 -67
		mu 0 3 8 45 14
		f 3 69 68 61
		mu 0 3 42 9 15
		f 4 0 70 57 -72
		mu 0 4 8 9 42 45
		f 3 74 72 58
		mu 0 3 44 2 17
		f 4 -54 75 -2 -75
		mu 0 4 44 43 3 2
		f 3 -76 -60 73
		mu 0 3 3 43 16
		f 3 -64 -73 78
		mu 0 3 35 17 2
		f 3 80 -66 77
		mu 0 3 1 41 15
		f 4 -79 -5 79 -37
		mu 0 4 35 2 0 36
		f 3 -80 -77 62
		mu 0 3 36 0 14
		f 4 47 -81 5 81
		mu 0 4 38 41 1 3
		f 3 -82 -74 64
		mu 0 3 38 3 16
		f 4 -83 -52 53 54
		mu 0 4 34 39 43 44
		f 4 -84 -45 82 39
		mu 0 4 37 40 39 34;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "pCube14";
	rename -uid "5C8C506F-4EBE-8017-A628-95A73E72887C";
	setAttr ".t" -type "double3" 3 2.5 -4.1 ;
	setAttr ".s" -type "double3" 1 1 -1 ;
createNode mesh -n "pCubeShape14" -p "pCube14";
	rename -uid "EA836505-462F-D799-B81C-228471B27126";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.375 0.125 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 46 ".uvst[0].uvsp[0:45]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25 0.375 0 0.625 0 0.625 0.25 0.375 0.25 0.375 0 0.625
		 0 0.625 0.25 0.375 0.25 0.54166663 0.25 0.54166663 0 0.45833331 0.25 0.45833331 0
		 0.375 0.16666666 0.45833331 0.16666666 0.375 0.083333328 0.45833331 0.083333328 0.54166663
		 0.16666666 0.625 0.16666666 0.54166663 0.083333328 0.625 0.083333328 0.45833331 0.16666666
		 0.375 0.16666666 0.375 0.083333328 0.45833331 0.083333328 0.625 0.16666666 0.54166663
		 0.16666666 0.54166663 0.083333328 0.625 0.083333328 0.54166663 0 0.54166663 0.25
		 0.45833331 0.25 0.45833331 0;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 18 ".pt";
	setAttr ".pt[9]" -type "float3" -2.5331974e-007 0 0 ;
	setAttr ".pt[10]" -type "float3" -2.5331974e-007 0 0 ;
	setAttr ".pt[15]" -type "float3" 1.0430813e-007 0 0 ;
	setAttr ".pt[20]" -type "float3" 1.0430813e-007 0 0 ;
	setAttr ".pt[37]" -type "float3" 0 7.4505806e-009 0 ;
	setAttr ".pt[38]" -type "float3" 0 7.4505806e-009 0 ;
	setAttr -s 40 ".vt[0:39]"  -0.70000005 -2 0.099999905 0.70000005 -2 0.099999905
		 -0.70000005 2 0.099999905 0.70000005 2 0.099999905 -0.70000005 2 -0.099999905 0.70000005 2 -0.099999905
		 -0.70000005 -2 -0.099999905 0.70000005 -2 -0.099999905 -0.60000014 -1.89999998 0.099999905
		 0.5999999 -1.89999998 0.099999905 0.5999999 1.9000001 0.099999905 -0.60000014 1.9000001 0.099999905
		 -0.60000014 -1.89999998 -0.01611948 0.60000014 -1.89999998 -0.01611948 0.60000014 1.9000001 -0.01611948
		 -0.60000002 1.9000001 -0.01611948 0.047219753 1.89999962 -0.016119957 0.047219753 -1.9000001 -0.016119957
		 -0.047219276 1.89999962 -0.016119957 -0.047219276 -1.9000001 -0.016119957 -0.59999979 0.053865433 -0.01611948
		 -0.047219276 0.053865433 -0.016120434 -0.5999999 -0.05386591 -0.01611948 -0.047219276 -0.05386591 -0.016120434
		 0.047219753 0.053865433 -0.016120434 0.60000038 0.053865433 -0.01611948 0.047219753 -0.05386591 -0.016120434
		 0.60000038 -0.05386591 -0.01611948 -0.59999967 0.053865433 0.10017872 -0.047219038 0.053865433 0.10017776
		 -0.59999967 -0.05386591 0.10017872 -0.047219515 -0.05386591 0.10017776 0.047219515 0.053865433 0.10017776
		 0.60000014 0.053865433 0.10017872 0.047219515 -0.05386591 0.10017776 0.60000014 -0.05386591 0.10017872
		 0.047219515 -1.9000001 0.10017824 0.047219515 1.89999962 0.10017824 -0.047219515 1.89999962 0.10017824
		 -0.047219515 -1.9000001 0.10017824;
	setAttr -s 84 ".ed[0:83]"  0 1 0 2 3 0 4 5 0 6 7 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0 8 12 0 9 13 0 10 14 0 11 15 0 14 16 0 13 17 0 18 15 0
		 19 12 0 15 20 0 18 21 0 20 21 0 22 12 0 23 19 0 22 23 0 16 24 0 14 25 0 24 25 0 26 17 0
		 27 13 0 26 27 0 20 28 0 21 29 0 28 29 0 22 30 0 28 30 0 23 31 0 30 31 0 29 31 0 24 32 0
		 25 33 0 32 33 0 26 34 0 32 34 0 27 35 0 34 35 0 33 35 0 17 36 0 34 36 0 16 37 0 37 32 0
		 18 38 0 37 38 0 38 29 0 19 39 0 31 39 0 36 39 1 11 38 0 10 37 0 8 39 0 9 36 0 8 30 0
		 11 28 0 10 33 0 9 35 0 0 8 0 39 0 0 1 9 0 36 1 0 1 36 0 0 39 0 2 11 0 10 3 0 38 2 1
		 37 3 1 8 0 0 9 1 0 2 28 1 0 30 1 1 35 1 3 33 1 32 29 1 34 31 1;
	setAttr -s 42 -ch 160 ".fc[0:41]" -type "polyFaces" 
		f 4 1 7 -3 -7
		mu 0 4 2 3 5 4
		f 4 2 9 -4 -9
		mu 0 4 4 5 7 6
		f 4 3 11 -1 -11
		mu 0 4 6 7 9 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13
		f 4 18 20 22 -22
		mu 0 4 24 21 26 27
		f 4 -35 36 38 -40
		mu 0 4 34 35 36 37
		f 4 -26 23 -20 -25
		mu 0 4 29 28 18 25
		f 4 16 26 28 -28
		mu 0 4 20 22 30 31
		f 4 -43 44 46 -48
		mu 0 4 38 39 40 41
		f 4 -32 29 -18 -31
		mu 0 4 33 32 23 19
		f 4 -50 83 56 -58
		mu 0 4 42 40 37 45
		f 4 -23 32 34 -34
		mu 0 4 27 26 35 34
		f 4 25 37 -39 -36
		mu 0 4 28 29 37 36
		f 4 -29 40 42 -42
		mu 0 4 31 30 39 38
		f 4 31 45 -47 -44
		mu 0 4 32 33 41 40
		f 4 -30 43 49 -49
		mu 0 4 23 32 40 42
		f 4 -27 50 51 -41
		mu 0 4 30 22 43 39
		f 4 21 33 -55 -53
		mu 0 4 24 27 34 44
		f 4 24 55 -57 -38
		mu 0 4 29 25 45 37
		f 4 15 -19 52 -59
		mu 0 4 17 21 24 44
		f 4 -15 59 -51 -17
		mu 0 4 20 16 43 22
		f 4 -13 60 -56 19
		mu 0 4 18 14 45 25
		f 4 13 17 48 -62
		mu 0 4 15 19 23 42
		f 4 12 -24 35 -63
		mu 0 4 14 18 28 36
		f 4 -16 63 -33 -21
		mu 0 4 21 17 35 26
		f 4 14 27 41 -65
		mu 0 4 16 20 31 38
		f 4 -14 65 -46 30
		mu 0 4 19 15 41 33
		f 3 -68 -61 -67
		mu 0 3 8 45 14
		f 3 69 68 61
		mu 0 3 42 9 15
		f 4 0 70 57 -72
		mu 0 4 8 9 42 45
		f 3 74 72 58
		mu 0 3 44 2 17
		f 4 -54 75 -2 -75
		mu 0 4 44 43 3 2
		f 3 -76 -60 73
		mu 0 3 3 43 16
		f 3 -64 -73 78
		mu 0 3 35 17 2
		f 3 80 -66 77
		mu 0 3 1 41 15
		f 4 -79 -5 79 -37
		mu 0 4 35 2 0 36
		f 3 -80 -77 62
		mu 0 3 36 0 14
		f 4 47 -81 5 81
		mu 0 4 38 41 1 3
		f 3 -82 -74 64
		mu 0 3 38 3 16
		f 4 -83 -52 53 54
		mu 0 4 34 39 43 44
		f 4 -84 -45 82 39
		mu 0 4 37 40 39 34;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "pCube15";
	rename -uid "56818AC3-47D7-EFE3-C737-82B3F735C1F0";
	setAttr ".t" -type "double3" 0 2.5 -4.1 ;
	setAttr ".s" -type "double3" 1 1 -1 ;
createNode mesh -n "pCubeShape15" -p "pCube15";
	rename -uid "C746898F-4069-DD6E-A5D1-A39E26C36024";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.375 0.125 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 46 ".uvst[0].uvsp[0:45]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25 0.375 0 0.625 0 0.625 0.25 0.375 0.25 0.375 0 0.625
		 0 0.625 0.25 0.375 0.25 0.54166663 0.25 0.54166663 0 0.45833331 0.25 0.45833331 0
		 0.375 0.16666666 0.45833331 0.16666666 0.375 0.083333328 0.45833331 0.083333328 0.54166663
		 0.16666666 0.625 0.16666666 0.54166663 0.083333328 0.625 0.083333328 0.45833331 0.16666666
		 0.375 0.16666666 0.375 0.083333328 0.45833331 0.083333328 0.625 0.16666666 0.54166663
		 0.16666666 0.54166663 0.083333328 0.625 0.083333328 0.54166663 0 0.54166663 0.25
		 0.45833331 0.25 0.45833331 0;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 18 ".pt";
	setAttr ".pt[9]" -type "float3" -2.5331974e-007 0 0 ;
	setAttr ".pt[10]" -type "float3" -2.5331974e-007 0 0 ;
	setAttr ".pt[15]" -type "float3" 1.0430813e-007 0 0 ;
	setAttr ".pt[20]" -type "float3" 1.0430813e-007 0 0 ;
	setAttr ".pt[37]" -type "float3" 0 7.4505806e-009 0 ;
	setAttr ".pt[38]" -type "float3" 0 7.4505806e-009 0 ;
	setAttr -s 40 ".vt[0:39]"  -0.70000005 -2 0.099999905 0.70000005 -2 0.099999905
		 -0.70000005 2 0.099999905 0.70000005 2 0.099999905 -0.70000005 2 -0.099999905 0.70000005 2 -0.099999905
		 -0.70000005 -2 -0.099999905 0.70000005 -2 -0.099999905 -0.60000014 -1.89999998 0.099999905
		 0.5999999 -1.89999998 0.099999905 0.5999999 1.9000001 0.099999905 -0.60000014 1.9000001 0.099999905
		 -0.60000014 -1.89999998 -0.01611948 0.60000014 -1.89999998 -0.01611948 0.60000014 1.9000001 -0.01611948
		 -0.60000002 1.9000001 -0.01611948 0.047219753 1.89999962 -0.016119957 0.047219753 -1.9000001 -0.016119957
		 -0.047219276 1.89999962 -0.016119957 -0.047219276 -1.9000001 -0.016119957 -0.59999979 0.053865433 -0.01611948
		 -0.047219276 0.053865433 -0.016120434 -0.5999999 -0.05386591 -0.01611948 -0.047219276 -0.05386591 -0.016120434
		 0.047219753 0.053865433 -0.016120434 0.60000038 0.053865433 -0.01611948 0.047219753 -0.05386591 -0.016120434
		 0.60000038 -0.05386591 -0.01611948 -0.59999967 0.053865433 0.10017872 -0.047219038 0.053865433 0.10017776
		 -0.59999967 -0.05386591 0.10017872 -0.047219515 -0.05386591 0.10017776 0.047219515 0.053865433 0.10017776
		 0.60000014 0.053865433 0.10017872 0.047219515 -0.05386591 0.10017776 0.60000014 -0.05386591 0.10017872
		 0.047219515 -1.9000001 0.10017824 0.047219515 1.89999962 0.10017824 -0.047219515 1.89999962 0.10017824
		 -0.047219515 -1.9000001 0.10017824;
	setAttr -s 84 ".ed[0:83]"  0 1 0 2 3 0 4 5 0 6 7 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0 8 12 0 9 13 0 10 14 0 11 15 0 14 16 0 13 17 0 18 15 0
		 19 12 0 15 20 0 18 21 0 20 21 0 22 12 0 23 19 0 22 23 0 16 24 0 14 25 0 24 25 0 26 17 0
		 27 13 0 26 27 0 20 28 0 21 29 0 28 29 0 22 30 0 28 30 0 23 31 0 30 31 0 29 31 0 24 32 0
		 25 33 0 32 33 0 26 34 0 32 34 0 27 35 0 34 35 0 33 35 0 17 36 0 34 36 0 16 37 0 37 32 0
		 18 38 0 37 38 0 38 29 0 19 39 0 31 39 0 36 39 1 11 38 0 10 37 0 8 39 0 9 36 0 8 30 0
		 11 28 0 10 33 0 9 35 0 0 8 0 39 0 0 1 9 0 36 1 0 1 36 0 0 39 0 2 11 0 10 3 0 38 2 1
		 37 3 1 8 0 0 9 1 0 2 28 1 0 30 1 1 35 1 3 33 1 32 29 1 34 31 1;
	setAttr -s 42 -ch 160 ".fc[0:41]" -type "polyFaces" 
		f 4 1 7 -3 -7
		mu 0 4 2 3 5 4
		f 4 2 9 -4 -9
		mu 0 4 4 5 7 6
		f 4 3 11 -1 -11
		mu 0 4 6 7 9 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13
		f 4 18 20 22 -22
		mu 0 4 24 21 26 27
		f 4 -35 36 38 -40
		mu 0 4 34 35 36 37
		f 4 -26 23 -20 -25
		mu 0 4 29 28 18 25
		f 4 16 26 28 -28
		mu 0 4 20 22 30 31
		f 4 -43 44 46 -48
		mu 0 4 38 39 40 41
		f 4 -32 29 -18 -31
		mu 0 4 33 32 23 19
		f 4 -50 83 56 -58
		mu 0 4 42 40 37 45
		f 4 -23 32 34 -34
		mu 0 4 27 26 35 34
		f 4 25 37 -39 -36
		mu 0 4 28 29 37 36
		f 4 -29 40 42 -42
		mu 0 4 31 30 39 38
		f 4 31 45 -47 -44
		mu 0 4 32 33 41 40
		f 4 -30 43 49 -49
		mu 0 4 23 32 40 42
		f 4 -27 50 51 -41
		mu 0 4 30 22 43 39
		f 4 21 33 -55 -53
		mu 0 4 24 27 34 44
		f 4 24 55 -57 -38
		mu 0 4 29 25 45 37
		f 4 15 -19 52 -59
		mu 0 4 17 21 24 44
		f 4 -15 59 -51 -17
		mu 0 4 20 16 43 22
		f 4 -13 60 -56 19
		mu 0 4 18 14 45 25
		f 4 13 17 48 -62
		mu 0 4 15 19 23 42
		f 4 12 -24 35 -63
		mu 0 4 14 18 28 36
		f 4 -16 63 -33 -21
		mu 0 4 21 17 35 26
		f 4 14 27 41 -65
		mu 0 4 16 20 31 38
		f 4 -14 65 -46 30
		mu 0 4 19 15 41 33
		f 3 -68 -61 -67
		mu 0 3 8 45 14
		f 3 69 68 61
		mu 0 3 42 9 15
		f 4 0 70 57 -72
		mu 0 4 8 9 42 45
		f 3 74 72 58
		mu 0 3 44 2 17
		f 4 -54 75 -2 -75
		mu 0 4 44 43 3 2
		f 3 -76 -60 73
		mu 0 3 3 43 16
		f 3 -64 -73 78
		mu 0 3 35 17 2
		f 3 80 -66 77
		mu 0 3 1 41 15
		f 4 -79 -5 79 -37
		mu 0 4 35 2 0 36
		f 3 -80 -77 62
		mu 0 3 36 0 14
		f 4 47 -81 5 81
		mu 0 4 38 41 1 3
		f 3 -82 -74 64
		mu 0 3 38 3 16
		f 4 -83 -52 53 54
		mu 0 4 34 39 43 44
		f 4 -84 -45 82 39
		mu 0 4 37 40 39 34;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode transform -n "pPipe2";
	rename -uid "1762D178-40F4-1313-B2CB-4E8958194F37";
	setAttr ".t" -type "double3" 0 11.61728236533191 -4.1 ;
	setAttr ".s" -type "double3" 1 1 -1 ;
createNode mesh -n "pPipeShape2" -p "pPipe2";
	rename -uid "342BC051-4FF8-4366-E30C-7A86590BEE6A";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.47492212057113647 0.75 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 90 ".uvst[0].uvsp[0:89]" -type "float2" 0 1 0.050000001 1
		 0.1 1 0.15000001 1 0.2 1 0.25 1 0.30000001 1 0.35000002 1 0.40000004 1 0.45000005
		 1 0.50000006 1 0.55000007 1 0.60000008 1 0.6500001 1 0.70000011 1 0.75000012 1 0.80000013
		 1 0.85000014 1 0.90000015 1 0.95000017 1 0.50000006 0.75 0.050000001 0.75 0.1 0.75
		 0.15000001 0.75 0.2 0.75 0.25 0.75 0.30000001 0.75 0.35000002 0.75 0.40000004 0.75
		 0.45000005 0.75 0.50000006 0.75 0.55000007 0.75 0.60000008 0.75 0.6500001 0.75 0.70000011
		 0.75 0.75000012 0.75 0.80000013 0.75 0.85000014 0.75 0.90000015 0.75 0.95000017 0.75
		 0 0.5 0.050000001 0.5 0.1 0.5 0.15000001 0.5 0.2 0.5 0.25 0.5 0.30000001 0.5 0.35000002
		 0.5 0.40000004 0.5 0.45000005 0.5 0.50000006 0.5 0.55000007 0.5 0.60000008 0.5 0.6500001
		 0.5 0.70000011 0.5 0.75000012 0.5 0.80000013 0.5 0.85000014 0.5 0.90000015 0.5 0.95000017
		 0.5 1.000000119209 0.5 0 0.25 0.050000001 0.25 0.1 0.25 0.15000001 0.25 0.2 0.25
		 0.25 0.25 0.30000001 0.25 0.35000002 0.25 0.40000004 0.25 0.45000005 0.25 0.50000006
		 0.25 0.55000007 0.25 0.60000008 0.25 0.6500001 0.25 0.70000011 0.25 0.75000012 0.25
		 0.80000013 0.25 0.85000014 0.25 0.90000015 0.25 0.95000017 0.25 1.000000119209 0.25
		 0.51164043 1 0.51109409 0.75 0.51929545 0.75 0.51888454 1 0.43050539 1 0.43108305
		 0.75 0.43829581 1 0.43875015 0.75;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 88 ".vt[0:87]"  0.79015028 0.12514751 -0.03943634 0.71280539 0.3631922 -0.03943634
		 0.56568521 0.56568539 -0.03943634 0.36319327 0.71279895 -0.03943634 0.12514746 0.79015064 -0.03943634
		 -0.12514758 0.79013926 -0.03943634 -0.36319327 0.71279931 -0.03943634 -0.56568664 0.56567496 -0.03943634
		 -0.71280545 0.36319238 -0.03943634 -0.79015136 0.12513661 -0.03943634 -0.79015124 -0.12515925 -0.03943634
		 -0.71280521 -0.36320376 -0.03943634 -0.56568551 -0.56569701 -0.03943634 -0.36319447 -0.71280593 -0.03943634
		 -0.12514722 -0.79016215 -0.03943634 0.1251463 -0.79015106 -0.03943634 0.36319244 -0.71280617 -0.03943634
		 0.56568575 -0.5656867 -0.03943634 0.71280628 -0.36320382 -0.03943634 0.79015118 -0.12514824 -0.03943634
		 0.79015028 0.12514751 0.099999905 0.71280539 0.3631922 0.099999905 0.56568521 0.56568539 0.099999905
		 0.36319327 0.71279895 0.099999905 0.12514746 0.79015064 0.099999905 -0.12514758 0.79013926 0.099999905
		 -0.36319327 0.71279931 0.099999905 -0.56568664 0.56567496 0.099999905 -0.71280545 0.36319238 0.099999905
		 -0.79015136 0.12513661 0.099999905 -0.79015124 -0.12515925 0.099999905 -0.71280521 -0.36320376 0.099999905
		 -0.56568551 -0.56569701 0.099999905 -0.36319447 -0.71280593 0.099999905 -0.12514722 -0.79016215 0.099999905
		 0.1251463 -0.79015106 0.099999905 0.36319244 -0.71280617 0.099999905 0.56568575 -0.5656867 0.099999905
		 0.71280628 -0.36320382 0.099999905 0.79015118 -0.12514824 0.099999905 0.98768908 0.15642299 0.099999905
		 0.89100665 0.4539907 0.099999905 0.70710671 0.70710629 0.099999905 0.4539907 0.8909952 0.099999905
		 0.15643407 0.9876883 0.099999905 -0.15643579 0.98768526 0.099999905 -0.45399171 0.89100635 0.099999905
		 -0.70710754 0.70709532 0.099999905 -0.89100689 0.45399094 0.099999905 -0.98768955 0.15642577 0.099999905
		 -0.9876886 -0.15643451 0.099999905 -0.89100724 -0.45399854 0.099999905 -0.70710701 -0.70711792 0.099999905
		 -0.45399213 -0.8910079 0.099999905 -0.15643401 -0.98770082 0.099999905 0.15643449 -0.98768932 0.099999905
		 0.45399177 -0.89101887 0.099999905 0.70710707 -0.7071079 0.099999905 0.89100832 -0.45399839 0.099999905
		 0.98768908 -0.15644613 0.099999905 0.98768908 0.15642299 -0.099999905 0.89100665 0.4539907 -0.099999905
		 0.70710671 0.70710629 -0.099999905 0.4539907 0.8909952 -0.099999905 0.15643407 0.9876883 -0.099999905
		 -0.15643579 0.98768526 -0.099999905 -0.45399171 0.89100635 -0.099999905 -0.70710754 0.70709532 -0.099999905
		 -0.89100689 0.45399094 -0.099999905 -0.98768955 0.15642577 -0.099999905 -0.9876886 -0.15643451 -0.099999905
		 -0.89100724 -0.45399854 -0.099999905 -0.70710701 -0.70711792 -0.099999905 -0.45399213 -0.8910079 -0.099999905
		 -0.15643401 -0.98770082 -0.099999905 0.15643449 -0.98768932 -0.099999905 0.45399177 -0.89101887 -0.099999905
		 0.70710707 -0.7071079 -0.099999905 0.89100832 -0.45399839 -0.099999905 0.98768908 -0.15644613 -0.099999905
		 -0.12514745 -0.12676609 -0.03943634 -0.12514745 -0.12676609 0.099999905 0.12514651 -0.12534423 0.099999905
		 0.12514703 -0.12467694 -0.03943634 0.1251473 0.1278446 -0.03943634 0.1251473 0.1278446 0.099999905
		 -0.12514715 0.12729071 -0.03943634 -0.12514715 0.12729071 0.099999905;
	setAttr -s 160 ".ed[0:159]"  0 1 0 1 2 0 2 3 0 3 4 0 5 6 0 6 7 0 7 8 0
		 8 9 0 10 11 0 11 12 0 12 13 0 13 14 0 15 16 0 16 17 0 17 18 0 18 19 0 20 21 0 21 22 0
		 22 23 0 23 24 0 24 25 1 25 26 0 26 27 0 27 28 0 28 29 0 29 30 1 30 31 0 31 32 0 32 33 0
		 33 34 0 34 35 1 35 36 0 36 37 0 37 38 0 38 39 0 39 20 1 40 41 0 41 42 0 42 43 0 43 44 0
		 44 45 0 45 46 0 46 47 0 47 48 0 48 49 0 49 50 0 50 51 0 51 52 0 52 53 0 53 54 0 54 55 0
		 55 56 0 56 57 0 57 58 0 58 59 0 59 40 0 60 61 0 61 62 0 62 63 0 63 64 0 64 65 0 65 66 0
		 66 67 0 67 68 0 68 69 0 69 70 0 70 71 0 71 72 0 72 73 0 73 74 0 74 75 0 75 76 0 76 77 0
		 77 78 0 78 79 0 79 60 0 0 20 0 1 21 1 2 22 1 3 23 1 4 24 0 5 25 0 6 26 1 7 27 1 8 28 1
		 9 29 0 10 30 0 11 31 1 12 32 1 13 33 1 14 34 0 15 35 0 16 36 1 17 37 1 18 38 1 19 39 0
		 20 40 1 21 41 1 22 42 1 23 43 1 24 44 1 25 45 1 26 46 1 27 47 1 28 48 1 29 49 1 30 50 1
		 31 51 1 32 52 1 33 53 1 34 54 1 35 55 1 36 56 1 37 57 1 38 58 1 39 59 1 40 60 1 41 61 1
		 42 62 1 43 63 1 44 64 1 45 65 1 46 66 1 47 67 1 48 68 1 49 69 1 50 70 1 51 71 1 52 72 1
		 53 73 1 54 74 1 55 75 1 56 76 1 57 77 1 58 78 1 59 79 1 25 87 0 24 85 0 5 86 0 4 84 0
		 80 14 0 81 34 0 80 81 0 82 35 0 83 15 0 82 83 0 85 82 1 84 85 0 87 81 1 86 87 0 87 85 1
		 82 81 1 30 81 0 29 87 0 9 86 0 10 80 0 20 85 0 39 82 0 19 83 0 0 84 0;
	setAttr -s 74 -ch 320 ".fc[0:73]" -type "polyFaces" 
		f 4 -1 76 16 -78
		mu 0 4 1 0 20 21
		f 4 -2 77 17 -79
		mu 0 4 2 1 21 22
		f 4 -3 78 18 -80
		mu 0 4 3 2 22 23
		f 4 -4 79 19 -81
		mu 0 4 4 3 23 24
		f 4 -5 81 21 -83
		mu 0 4 6 5 25 26
		f 4 -6 82 22 -84
		mu 0 4 7 6 26 27
		f 4 -7 83 23 -85
		mu 0 4 8 7 27 28
		f 4 -8 84 24 -86
		mu 0 4 9 8 28 29
		f 4 -9 86 26 -88
		mu 0 4 11 10 30 31
		f 4 -10 87 27 -89
		mu 0 4 12 11 31 32
		f 4 -11 88 28 -90
		mu 0 4 13 12 32 33
		f 4 -12 89 29 -91
		mu 0 4 14 13 33 34
		f 4 -13 91 31 -93
		mu 0 4 16 15 35 36
		f 4 -14 92 32 -94
		mu 0 4 17 16 36 37
		f 4 -15 93 33 -95
		mu 0 4 18 17 37 38
		f 4 -16 94 34 -96
		mu 0 4 19 18 38 39
		f 4 -17 96 36 -98
		mu 0 4 21 20 40 41
		f 4 -18 97 37 -99
		mu 0 4 22 21 41 42
		f 4 -19 98 38 -100
		mu 0 4 23 22 42 43
		f 4 -20 99 39 -101
		mu 0 4 24 23 43 44
		f 4 -21 100 40 -102
		mu 0 4 25 24 44 45
		f 4 -22 101 41 -103
		mu 0 4 26 25 45 46
		f 4 -23 102 42 -104
		mu 0 4 27 26 46 47
		f 4 -24 103 43 -105
		mu 0 4 28 27 47 48
		f 4 -25 104 44 -106
		mu 0 4 29 28 48 49
		f 4 -26 105 45 -107
		mu 0 4 30 29 49 50
		f 4 -27 106 46 -108
		mu 0 4 31 30 50 51
		f 4 -28 107 47 -109
		mu 0 4 32 31 51 52
		f 4 -29 108 48 -110
		mu 0 4 33 32 52 53
		f 4 -30 109 49 -111
		mu 0 4 34 33 53 54
		f 4 -31 110 50 -112
		mu 0 4 35 34 54 55
		f 4 -32 111 51 -113
		mu 0 4 36 35 55 56
		f 4 -33 112 52 -114
		mu 0 4 37 36 56 57
		f 4 -34 113 53 -115
		mu 0 4 38 37 57 58
		f 4 -35 114 54 -116
		mu 0 4 39 38 58 59
		f 4 -36 115 55 -97
		mu 0 4 20 39 59 60
		f 4 -37 116 56 -118
		mu 0 4 41 40 61 62
		f 4 -38 117 57 -119
		mu 0 4 42 41 62 63
		f 4 -39 118 58 -120
		mu 0 4 43 42 63 64
		f 4 -40 119 59 -121
		mu 0 4 44 43 64 65
		f 4 -41 120 60 -122
		mu 0 4 45 44 65 66
		f 4 -42 121 61 -123
		mu 0 4 46 45 66 67
		f 4 -43 122 62 -124
		mu 0 4 47 46 67 68
		f 4 -44 123 63 -125
		mu 0 4 48 47 68 69
		f 4 -45 124 64 -126
		mu 0 4 49 48 69 70
		f 4 -46 125 65 -127
		mu 0 4 50 49 70 71
		f 4 -47 126 66 -128
		mu 0 4 51 50 71 72
		f 4 -48 127 67 -129
		mu 0 4 52 51 72 73
		f 4 -49 128 68 -130
		mu 0 4 53 52 73 74
		f 4 -50 129 69 -131
		mu 0 4 54 53 74 75
		f 4 -51 130 70 -132
		mu 0 4 55 54 75 76
		f 4 -52 131 71 -133
		mu 0 4 56 55 76 77
		f 4 -53 132 72 -134
		mu 0 4 57 56 77 78
		f 4 -54 133 73 -135
		mu 0 4 58 57 78 79
		f 4 -55 134 74 -136
		mu 0 4 59 58 79 80
		f 4 -56 135 75 -117
		mu 0 4 60 59 80 81
		f 20 -57 -76 -75 -74 -73 -72 -71 -70 -69 -68 -67 -66 -65 -64 -63 -62 -61 -60 -59 -58
		mu 0 20 62 81 80 79 78 77 76 75 74 73 72 71 70 69 68 67 66 65 64 63
		f 4 20 136 150 -138
		mu 0 4 24 25 89 87
		f 4 -82 138 149 -137
		mu 0 4 25 5 88 89
		f 4 -143 140 90 -142
		mu 0 4 83 82 14 34
		f 4 -146 143 -92 -145
		mu 0 4 85 84 35 15
		f 4 80 137 -148 -140
		mu 0 4 4 24 87 86
		f 4 151 141 30 -144
		mu 0 4 84 83 34 35
		f 4 -151 148 -152 -147
		mu 0 4 87 89 83 84
		f 4 25 152 -149 -154
		mu 0 4 29 30 83 89
		f 4 85 153 -150 -155
		mu 0 4 9 29 89 88
		f 4 -87 155 142 -153
		mu 0 4 30 10 82 83
		f 4 35 156 146 -158
		mu 0 4 39 20 87 84
		f 4 95 157 145 -159
		mu 0 4 19 39 84 85
		f 4 -77 159 147 -157
		mu 0 4 20 0 86 87
		f 6 1 2 3 139 -160 0
		mu 0 6 1 2 3 4 86 0
		f 6 5 6 7 154 -139 4
		mu 0 6 6 7 8 9 88 5
		f 6 9 10 11 -141 -156 8
		mu 0 6 11 12 13 14 82 10
		f 6 13 14 15 158 144 12
		mu 0 6 16 17 18 19 85 15;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode lightLinker -s -n "lightLinker1";
	rename -uid "27402E66-4F98-FD54-AC0C-8CA7D6CD900B";
	setAttr -s 2 ".lnk";
	setAttr -s 2 ".slnk";
createNode displayLayerManager -n "layerManager";
	rename -uid "BD333C57-4939-2822-B4FB-D4942B6099EC";
createNode displayLayer -n "defaultLayer";
	rename -uid "F3305F2B-4EE7-31EB-CD8F-76811EBFBF78";
createNode renderLayerManager -n "renderLayerManager";
	rename -uid "4E2A9E9D-4C2C-77D3-7B0F-2FAF4E83ADCA";
createNode renderLayer -n "defaultRenderLayer";
	rename -uid "720BE897-4319-AB8C-0930-B39D8A217D22";
	setAttr ".g" yes;
createNode polyCube -n "polyCube1";
	rename -uid "F5B64FD9-433B-BDB1-D7B0-07ACF0EE5915";
	setAttr ".w" 10;
	setAttr ".h" 4;
	setAttr ".d" 8;
	setAttr ".cuv" 4;
createNode polyCube -n "polyCube2";
	rename -uid "72C2DC66-45E6-AFDC-47F8-98A1B387A5B8";
	setAttr ".w" 2;
	setAttr ".h" 3;
	setAttr ".d" 0.2;
	setAttr ".cuv" 4;
createNode polyCube -n "polyCube3";
	rename -uid "817A67A5-49E0-2A3F-EFCD-718CD75078E2";
	setAttr ".w" 2.4;
	setAttr ".h" 0.5;
	setAttr ".d" 2;
	setAttr ".sh" 2;
	setAttr ".cuv" 4;
createNode polyExtrudeFace -n "polyExtrudeFace1";
	rename -uid "CF15B36F-465C-9B37-7F3F-13B4C19FE901";
	setAttr ".ics" -type "componentList" 1 "f[0]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0.25 5 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 0.125 6 ;
	setAttr ".rs" 43543;
	setAttr ".c[0]"  0 1 1;
	setAttr ".tk" 0.40000000596046448;
	setAttr ".cbn" -type "double3" -1.2000000476837158 0 6 ;
	setAttr ".cbx" -type "double3" 1.2000000476837158 0.25 6 ;
createNode polyCube -n "polyCube4";
	rename -uid "E44F3859-45CA-12F1-899A-AF93D9D2ADE1";
	setAttr ".d" 2.2;
	setAttr ".cuv" 4;
createNode deleteComponent -n "deleteComponent1";
	rename -uid "08730E55-4A25-05B0-BA25-E08A4150CBFA";
	setAttr ".dc" -type "componentList" 1 "e[10]";
createNode deleteComponent -n "deleteComponent2";
	rename -uid "DC081CB1-40D8-6C22-ED98-99A448500FF3";
	setAttr ".dc" -type "componentList" 2 "vtx[0]" "vtx[6]";
createNode polyCube -n "polyCube5";
	rename -uid "9B044D43-4412-BB4C-860C-75A7B48B809C";
	setAttr ".w" 0.2;
	setAttr ".h" 3.2;
	setAttr ".d" 0.2;
	setAttr ".cuv" 4;
createNode script -n "uiConfigurationScriptNode";
	rename -uid "B13AA241-4417-308C-26D0-9C99B6B17A74";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"top\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n"
		+ "                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n"
		+ "                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n"
		+ "                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 1\n                -height 1\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n"
		+ "                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n"
		+ "            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n"
		+ "            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n"
		+ "        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"side\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n"
		+ "                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n"
		+ "                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n"
		+ "                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 1\n                -height 1\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n"
		+ "            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n"
		+ "            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n"
		+ "            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"front\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n"
		+ "                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n"
		+ "                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n"
		+ "                -width 1\n                -height 1\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n"
		+ "            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n"
		+ "            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n"
		+ "            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 1\n                -headsUpDisplay 1\n"
		+ "                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n"
		+ "                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n"
		+ "                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 1800\n                -height 1090\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 1\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n"
		+ "            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n"
		+ "            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1800\n            -height 1090\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n"
		+ "            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n                -docTag \"isolOutln_fromSeln\" \n                -showShapes 1\n                -showReferenceNodes 1\n                -showReferenceMembers 1\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n                -showContainerContents 1\n                -ignoreDagHierarchy 0\n"
		+ "                -expandConnections 0\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n"
		+ "                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 0\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -docTag \"isolOutln_fromSeln\" \n            -showShapes 1\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n"
		+ "            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n"
		+ "            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"graphEditor\" -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n"
		+ "                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n"
		+ "                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n"
		+ "                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n"
		+ "                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n"
		+ "                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n"
		+ "                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dopeSheetPanel\" -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n"
		+ "                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n"
		+ "                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n"
		+ "                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n"
		+ "                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n"
		+ "                -ignoreOutlinerColor 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"clipEditorPanel\" -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"sequenceEditorPanel\" -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n"
		+ "                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperGraphPanel\" -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n"
		+ "                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n"
		+ "                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperShadePanel\" -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"visorPanel\" -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"nodeEditorPanel\" -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n"
		+ "                -gridVisibility 1\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -activeTab -1\n                -editorMode \"default\" \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n"
		+ "                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -activeTab -1\n                -editorMode \"default\" \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"createNodePanel\" -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"polyTexturePlacementPanel\" -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"renderWindowPanel\" -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"blendShapePanel\" (localizedPanelLabel(\"Blend Shape\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\tblendShapePanel -unParent -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels ;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tblendShapePanel -edit -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynRelEdPanel\" -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"relationshipPanel\" -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"referenceEditorPanel\" -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"componentEditorPanel\" -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynPaintScriptedPanelType\" -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"scriptEditorPanel\" -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"profilerPanel\" (localizedPanelLabel(\"Profiler Tool\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"profilerPanel\" -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-defaultImage \"vacantCell.xP:/\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"vertical2\\\" -ps 1 20 100 -ps 2 80 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Outliner\")) \n\t\t\t\t\t\"outlinerPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\\\"Outliner\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\noutlinerEditor -e \\n    -docTag \\\"isolOutln_fromSeln\\\" \\n    -showShapes 1\\n    -showReferenceNodes 1\\n    -showReferenceMembers 1\\n    -showAttributes 0\\n    -showConnected 0\\n    -showAnimCurvesOnly 0\\n    -showMuteInfo 0\\n    -organizeByLayer 1\\n    -showAnimLayerWeight 1\\n    -autoExpandLayers 1\\n    -autoExpand 0\\n    -showDagOnly 1\\n    -showAssets 1\\n    -showContainedOnly 1\\n    -showPublishedAsConnected 0\\n    -showContainerContents 1\\n    -ignoreDagHierarchy 0\\n    -expandConnections 0\\n    -showUpstreamCurves 1\\n    -showUnitlessCurves 1\\n    -showCompounds 1\\n    -showLeafs 1\\n    -showNumericAttrsOnly 0\\n    -highlightActive 1\\n    -autoSelectNewObjects 0\\n    -doNotSelectNewObjects 0\\n    -dropIsParent 1\\n    -transmitFilters 0\\n    -setFilter \\\"defaultSetFilter\\\" \\n    -showSetMembers 1\\n    -allowMultiSelection 1\\n    -alwaysToggleSelect 0\\n    -directSelect 0\\n    -displayMode \\\"DAG\\\" \\n    -expandObjects 0\\n    -setsIgnoreFilters 1\\n    -containersIgnoreFilters 0\\n    -editAttrName 0\\n    -showAttrValues 0\\n    -highlightSecondary 0\\n    -showUVAttrsOnly 0\\n    -showTextureNodesOnly 0\\n    -attrAlphaOrder \\\"default\\\" \\n    -animLayerFilterOptions \\\"allAffecting\\\" \\n    -sortOrder \\\"none\\\" \\n    -longNames 0\\n    -niceNames 1\\n    -showNamespace 1\\n    -showPinIcons 0\\n    -mapMotionTrails 0\\n    -ignoreHiddenAttribute 0\\n    -ignoreOutlinerColor 0\\n    $editorName\"\n"
		+ "\t\t\t\t\t\"outlinerPanel -edit -l (localizedPanelLabel(\\\"Outliner\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\noutlinerEditor -e \\n    -docTag \\\"isolOutln_fromSeln\\\" \\n    -showShapes 1\\n    -showReferenceNodes 1\\n    -showReferenceMembers 1\\n    -showAttributes 0\\n    -showConnected 0\\n    -showAnimCurvesOnly 0\\n    -showMuteInfo 0\\n    -organizeByLayer 1\\n    -showAnimLayerWeight 1\\n    -autoExpandLayers 1\\n    -autoExpand 0\\n    -showDagOnly 1\\n    -showAssets 1\\n    -showContainedOnly 1\\n    -showPublishedAsConnected 0\\n    -showContainerContents 1\\n    -ignoreDagHierarchy 0\\n    -expandConnections 0\\n    -showUpstreamCurves 1\\n    -showUnitlessCurves 1\\n    -showCompounds 1\\n    -showLeafs 1\\n    -showNumericAttrsOnly 0\\n    -highlightActive 1\\n    -autoSelectNewObjects 0\\n    -doNotSelectNewObjects 0\\n    -dropIsParent 1\\n    -transmitFilters 0\\n    -setFilter \\\"defaultSetFilter\\\" \\n    -showSetMembers 1\\n    -allowMultiSelection 1\\n    -alwaysToggleSelect 0\\n    -directSelect 0\\n    -displayMode \\\"DAG\\\" \\n    -expandObjects 0\\n    -setsIgnoreFilters 1\\n    -containersIgnoreFilters 0\\n    -editAttrName 0\\n    -showAttrValues 0\\n    -highlightSecondary 0\\n    -showUVAttrsOnly 0\\n    -showTextureNodesOnly 0\\n    -attrAlphaOrder \\\"default\\\" \\n    -animLayerFilterOptions \\\"allAffecting\\\" \\n    -sortOrder \\\"none\\\" \\n    -longNames 0\\n    -niceNames 1\\n    -showNamespace 1\\n    -showPinIcons 0\\n    -mapMotionTrails 0\\n    -ignoreHiddenAttribute 0\\n    -ignoreOutlinerColor 0\\n    $editorName\"\n"
		+ "\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 1\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 1800\\n    -height 1090\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 1\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 1800\\n    -height 1090\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        setFocus `paneLayout -q -p1 $gMainPane`;\n        sceneUIReplacement -deleteRemaining;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 12 -divisions 20 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	rename -uid "2D734184-46AF-F46F-82DB-62BC14F0B154";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 120 -ast 1 -aet 200 ";
	setAttr ".st" 6;
createNode polyCube -n "polyCube6";
	rename -uid "3B6A0A00-4E0B-8F9F-CC03-EC9AC7E169FB";
	setAttr ".d" 8;
	setAttr ".cuv" 4;
createNode deleteComponent -n "deleteComponent3";
	rename -uid "1C05E79F-4F7C-40DD-8093-DAB9A46D7ED8";
	setAttr ".dc" -type "componentList" 1 "e[10]";
createNode deleteComponent -n "deleteComponent4";
	rename -uid "F5C68B13-4B47-8070-216E-5BA42E003377";
	setAttr ".dc" -type "componentList" 2 "vtx[0]" "vtx[6]";
createNode polyExtrudeFace -n "polyExtrudeFace2";
	rename -uid "FA2A802E-457E-29E8-F158-3D8318F02124";
	setAttr ".ics" -type "componentList" 2 "f[1]" "f[3]";
	setAttr ".ix" -type "matrix" 0.70710678118654746 0.70710678118654757 0 0 -0.70710678118654757 0.70710678118654746 0 0
		 0 0 1 0 0 4 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -8.8817842e-016 7.8765135 0 ;
	setAttr ".rs" 41976;
	setAttr ".c[0]"  0 1 1;
	setAttr ".tk" 0.30000001192092896;
	setAttr ".cbn" -type "double3" -5.006934335414142 3.9999999999999996 -4 ;
	setAttr ".cbx" -type "double3" 5.006934335414142 16.759961758421049 4 ;
createNode deleteComponent -n "deleteComponent5";
	rename -uid "9142F90B-43E2-1408-11C6-4C9F559FAF2D";
	setAttr ".dc" -type "componentList" 1 "f[4]";
createNode deleteComponent -n "deleteComponent6";
	rename -uid "8424FDA5-4B52-D048-68D8-D1B185DE8681";
	setAttr ".dc" -type "componentList" 1 "f[0]";
createNode deleteComponent -n "deleteComponent7";
	rename -uid "E660962F-4F9F-D152-5431-A79915057943";
	setAttr ".dc" -type "componentList" 1 "f[1]";
createNode polyBridgeEdge -n "polyBridgeEdge1";
	rename -uid "9E410F30-405A-D34F-4D53-5CA04D96AB3A";
	setAttr ".ics" -type "componentList" 1 "e[0:1]";
	setAttr ".ix" -type "matrix" 0.70710678118654746 0.70710678118654757 0 0 -0.70710678118654757 0.70710678118654746 0 0
		 0 0 1 0 0 4 0 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 2;
	setAttr ".sv2" 3;
createNode polyBridgeEdge -n "polyBridgeEdge2";
	rename -uid "6359796F-4E26-ABCE-73A5-7A97A847A468";
	setAttr ".ics" -type "componentList" 2 "e[2]" "e[4]";
	setAttr ".ix" -type "matrix" 0.70710678118654746 0.70710678118654757 0 0 -0.70710678118654757 0.70710678118654746 0 0
		 0 0 1 0 0 4 0 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 0;
	setAttr ".sv2" 4;
createNode polyExtrudeFace -n "polyExtrudeFace3";
	rename -uid "63284E78-4BE6-F23E-304C-CE9BD28C264A";
	setAttr ".ics" -type "componentList" 2 "f[1]" "f[3]";
	setAttr ".ix" -type "matrix" 0.70710678118654746 0.70710678118654757 0 0 -0.70710678118654757 0.70710678118654746 0 0
		 0 0 1 0 0 3.6762072945932052 5.0999999999999996 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 3.6762073 5.0999999 ;
	setAttr ".rs" 53878;
	setAttr ".c[0]"  0 1 1;
	setAttr ".tk" 0.20000000298023224;
	setAttr ".cbn" -type "double3" -1.2727763252032664 3.6762072945932052 3.9999999761581417 ;
	setAttr ".cbx" -type "double3" 1.2727763252032664 4.3833140757797526 6.2000000238418576 ;
createNode deleteComponent -n "deleteComponent8";
	rename -uid "4F8797E7-4276-3748-95B3-5B980B078EB3";
	setAttr ".dc" -type "componentList" 1 "f[4]";
createNode deleteComponent -n "deleteComponent9";
	rename -uid "6074AA28-4C35-06E6-14C6-ABA8A7FF1B62";
	setAttr ".dc" -type "componentList" 1 "f[0]";
createNode deleteComponent -n "deleteComponent10";
	rename -uid "6F29B12B-44B6-E7F0-92A1-F9B2997A0AC6";
	setAttr ".dc" -type "componentList" 1 "f[1]";
createNode polyBridgeEdge -n "polyBridgeEdge3";
	rename -uid "D9BD4756-47EC-AFD7-84BC-F7A9C2A72E70";
	setAttr ".ics" -type "componentList" 1 "e[0:1]";
	setAttr ".ix" -type "matrix" 0.70710678118654746 0.70710678118654757 0 0 -0.70710678118654757 0.70710678118654746 0 0
		 0 0 1 0 0 3.6762072945932052 5.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 2;
	setAttr ".sv2" 3;
createNode polyBridgeEdge -n "polyBridgeEdge4";
	rename -uid "5E204C19-4E4A-85CD-9BAC-72900AA2BE88";
	setAttr ".ics" -type "componentList" 2 "e[2]" "e[4]";
	setAttr ".ix" -type "matrix" 0.70710678118654746 0.70710678118654757 0 0 -0.70710678118654757 0.70710678118654746 0 0
		 0 0 1 0 0 3.6762072945932052 5.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 0;
	setAttr ".sv2" 4;
createNode polyCube -n "polyCube7";
	rename -uid "C915D7AF-4DB5-0FAA-6795-5092C9D46853";
	setAttr ".w" 1.4;
	setAttr ".h" 4;
	setAttr ".d" 0.2;
	setAttr ".cuv" 4;
createNode polyExtrudeFace -n "polyExtrudeFace4";
	rename -uid "E3011A38-499A-E6A9-A861-179530E17058";
	setAttr ".ics" -type "componentList" 1 "f[0]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -3 2.5 4.1999998 ;
	setAttr ".rs" 48288;
	setAttr ".off" 0.10000000149011612;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -3.699999988079071 0.5 4.2000000014901158 ;
	setAttr ".cbx" -type "double3" -2.300000011920929 4.5 4.2000000014901158 ;
createNode polyExtrudeFace -n "polyExtrudeFace5";
	rename -uid "6D3E2CCB-4A0C-FA56-0441-C7916478B002";
	setAttr ".ics" -type "componentList" 1 "f[0]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -3 2.5 4.1999998 ;
	setAttr ".rs" 49376;
	setAttr ".c[0]"  0 1 1;
	setAttr ".tk" -0.15000000596046448;
	setAttr ".cbn" -type "double3" -3.6000001430511475 0.60000002384185791 4.199999904632568 ;
	setAttr ".cbx" -type "double3" -2.3999998569488525 4.4000000953674316 4.199999904632568 ;
createNode deleteComponent -n "deleteComponent11";
	rename -uid "DA230953-4155-ABCA-0C53-A19E9B0AF9A8";
	setAttr ".dc" -type "componentList" 1 "f[0]";
createNode polyBridgeEdge -n "polyBridgeEdge5";
	rename -uid "DB5D56A1-4779-6733-26FA-87B43E209979";
	setAttr ".ics" -type "componentList" 2 "e[24]" "e[27]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 2;
	setAttr ".sv1" 13;
	setAttr ".sv2" 15;
createNode polyCloseBorder -n "polyCloseBorder1";
	rename -uid "A1993544-411C-6FCC-A2C1-6DB22995B541";
	setAttr ".ics" -type "componentList" 7 "e[0]" "e[3]" "e[14]" "e[22]" "e[29]" "e[31:32]" "e[34:35]";
createNode polyTweak -n "polyTweak1";
	rename -uid "612B6DAA-4D3A-7661-40D9-03A3F8D8AC9E";
	setAttr ".uopa" yes;
	setAttr -s 9 ".tk";
	setAttr ".tk[12]" -type "float3" 0 -2.9802322e-008 0.033880536 ;
	setAttr ".tk[13]" -type "float3" 0 -2.9802322e-008 0.033880536 ;
	setAttr ".tk[14]" -type "float3" 0 0 0.033880536 ;
	setAttr ".tk[15]" -type "float3" 0 0 0.033880536 ;
	setAttr ".tk[16]" -type "float3" -0.15278041 0 0.033880536 ;
	setAttr ".tk[17]" -type "float3" -0.15278041 -5.9604645e-008 0.033880536 ;
	setAttr ".tk[18]" -type "float3" 0.15278041 0 0.033880536 ;
	setAttr ".tk[19]" -type "float3" 0.15278041 -5.9604645e-008 0.033880536 ;
createNode deleteComponent -n "deleteComponent12";
	rename -uid "F313704E-4184-44CF-BCBC-538AB6F57680";
	setAttr ".dc" -type "componentList" 1 "f[9]";
createNode polyCloseBorder -n "polyCloseBorder2";
	rename -uid "249C6CFA-4ABB-F4EC-5DBF-AEA5E8A4EA46";
	setAttr ".ics" -type "componentList" 1 "e[*]";
createNode deleteComponent -n "deleteComponent13";
	rename -uid "CB41506A-421E-4B74-BC30-41BDD56106F7";
	setAttr ".dc" -type "componentList" 1 "e[22]";
createNode deleteComponent -n "deleteComponent14";
	rename -uid "8A777E7E-4FBA-ACBB-2599-E6B339A76C54";
	setAttr ".dc" -type "componentList" 1 "f[10]";
createNode polyCloseBorder -n "polyCloseBorder3";
	rename -uid "9AF4BA2E-44CD-D5C7-A84C-CFBF61C94300";
	setAttr ".ics" -type "componentList" 1 "e[*]";
createNode deleteComponent -n "deleteComponent15";
	rename -uid "E6440C7D-4A37-5DB6-B392-BDB0521317CC";
	setAttr ".dc" -type "componentList" 1 "e[25]";
createNode deleteComponent -n "deleteComponent16";
	rename -uid "0E608D90-4961-F6BD-B958-DDA9D62599DC";
	setAttr ".dc" -type "componentList" 1 "f[11]";
createNode deleteComponent -n "deleteComponent17";
	rename -uid "705648A5-43C9-C8E3-F352-18ACF48C30F4";
	setAttr ".dc" -type "componentList" 1 "f[12]";
createNode polyBridgeEdge -n "polyBridgeEdge6";
	rename -uid "13044286-4DB5-1C71-EFD9-74A4182817F3";
	setAttr ".ics" -type "componentList" 1 "e[31:32]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 2;
	setAttr ".sv1" 18;
	setAttr ".sv2" 12;
createNode polyBridgeEdge -n "polyBridgeEdge7";
	rename -uid "B45676E9-4C67-08B3-DCC7-74AA80E46DEF";
	setAttr ".ics" -type "componentList" 1 "e[26:27]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 2;
	setAttr ".sv1" 14;
	setAttr ".sv2" 17;
createNode polyTweak -n "polyTweak2";
	rename -uid "5F9F41E1-4160-F4AE-A5E0-B9A95B9749AB";
	setAttr ".uopa" yes;
	setAttr -s 13 ".tk";
	setAttr ".tk[20]" -type "float3" 0 -0.57946777 0 ;
	setAttr ".tk[21]" -type "float3" 0 -0.57946754 0 ;
	setAttr ".tk[22]" -type "float3" 0 0.57946765 0 ;
	setAttr ".tk[23]" -type "float3" 0 0.57946777 0 ;
	setAttr ".tk[24]" -type "float3" 0 -0.57946754 0 ;
	setAttr ".tk[25]" -type "float3" 0 -0.57946777 0 ;
	setAttr ".tk[26]" -type "float3" 0 0.57946777 0 ;
	setAttr ".tk[27]" -type "float3" 0 0.57946765 0 ;
createNode deleteComponent -n "deleteComponent18";
	rename -uid "F2A24721-4BAD-F67D-7E31-74B94C195DAF";
	setAttr ".dc" -type "componentList" 1 "f[9]";
createNode deleteComponent -n "deleteComponent19";
	rename -uid "2D5F51C9-40CA-C90C-E823-828E794579E2";
	setAttr ".dc" -type "componentList" 1 "f[9]";
createNode polyCloseBorder -n "polyCloseBorder4";
	rename -uid "BD513C48-419F-5A10-9F74-B9A00E8691E8";
	setAttr ".ics" -type "componentList" 1 "e[*]";
createNode deleteComponent -n "deleteComponent20";
	rename -uid "C174B366-42EB-FCEC-9CA0-72882221982C";
	setAttr ".dc" -type "componentList" 1 "e[31]";
createNode deleteComponent -n "deleteComponent21";
	rename -uid "05C88C7F-4A36-E407-7087-40A123A6F06D";
	setAttr ".dc" -type "componentList" 1 "e[28]";
createNode polyExtrudeFace -n "polyExtrudeFace6";
	rename -uid "D64CBE52-4E0B-2D4D-E523-6393247F10AB";
	setAttr ".ics" -type "componentList" 3 "f[12]" "f[15]" "f[19]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -2.9999998 2.4999998 4.0838799 ;
	setAttr ".rs" 62981;
	setAttr ".lt" -type "double3" 3.5907490183175754e-016 0 -0.033701441189091706 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".tk" 0.15000000596046448;
	setAttr ".cbn" -type "double3" -3.5999999046325684 0.59999990463256836 4.0838795661926266 ;
	setAttr ".cbx" -type "double3" -2.3999996185302734 4.3999996185302734 4.083880519866943 ;
createNode deleteComponent -n "deleteComponent22";
	rename -uid "698104DD-407B-5E56-F2D9-42B7BD47A649";
	setAttr ".dc" -type "componentList" 1 "f[10]";
createNode deleteComponent -n "deleteComponent23";
	rename -uid "F7FCD226-40F9-002C-244B-0FBC35C2078F";
	setAttr ".dc" -type "componentList" 1 "f[27]";
createNode deleteComponent -n "deleteComponent24";
	rename -uid "E55478CF-4BCB-A793-6CFF-06902C2CCA67";
	setAttr ".dc" -type "componentList" 1 "f[9]";
createNode polyBridgeEdge -n "polyBridgeEdge8";
	rename -uid "BD40C91D-4E72-1A5F-2CA2-90AA5CDEA1BE";
	setAttr ".ics" -type "componentList" 2 "e[23]" "e[63]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 11;
	setAttr ".sv2" 18;
createNode polyBridgeEdge -n "polyBridgeEdge9";
	rename -uid "E3841DBE-4414-1984-A2E2-C988F50C0309";
	setAttr ".ics" -type "componentList" 2 "e[22]" "e[61]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 14;
	setAttr ".sv2" 37;
createNode polyCloseBorder -n "polyCloseBorder5";
	rename -uid "9846439C-4856-227A-8CEA-65BB62C189E8";
	setAttr ".ics" -type "componentList" 2 "e[21]" "e[59]";
createNode deleteComponent -n "deleteComponent25";
	rename -uid "72B867A4-49C4-D27C-97D1-5EB75D378321";
	setAttr ".dc" -type "componentList" 1 "f[31]";
createNode deleteComponent -n "deleteComponent26";
	rename -uid "BDF7EBC6-41F4-046C-8459-DC8BE82BCD1B";
	setAttr ".dc" -type "componentList" 1 "f[28]";
createNode polyBridgeEdge -n "polyBridgeEdge10";
	rename -uid "8C980A9A-4F04-AAF6-F2F3-4F8CF00EB7CD";
	setAttr ".ics" -type "componentList" 2 "e[20]" "e[65]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 12;
	setAttr ".sv2" 39;
createNode polyBridgeEdge -n "polyBridgeEdge11";
	rename -uid "43A1A140-43C8-0F46-3A55-7E86EBC858FE";
	setAttr ".ics" -type "componentList" 2 "e[21]" "e[58]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 9;
	setAttr ".sv2" 17;
createNode deleteComponent -n "deleteComponent27";
	rename -uid "684B21A9-40C5-F918-F8E2-889472BD09A2";
	setAttr ".dc" -type "componentList" 1 "f[15]";
createNode deleteComponent -n "deleteComponent28";
	rename -uid "3CFE4515-4397-B8BC-86B5-4BB1CA1F2189";
	setAttr ".dc" -type "componentList" 1 "f[22]";
createNode deleteComponent -n "deleteComponent29";
	rename -uid "D1548EFB-4405-3EA7-30B5-BBAA9FBC9D7A";
	setAttr ".dc" -type "componentList" 1 "f[15]";
createNode deleteComponent -n "deleteComponent30";
	rename -uid "296124E4-4E7A-9AA8-0970-27ABE680183B";
	setAttr ".dc" -type "componentList" 1 "f[17]";
createNode polyBridgeEdge -n "polyBridgeEdge12";
	rename -uid "98BA1717-4F57-FA5D-5B17-85818D7C2CAA";
	setAttr ".ics" -type "componentList" 2 "e[20]" "e[43]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 8;
	setAttr ".sv2" 22;
createNode polyBridgeEdge -n "polyBridgeEdge13";
	rename -uid "500FDF1D-4836-56D7-FDA1-17A147E5E169";
	setAttr ".ics" -type "componentList" 2 "e[23]" "e[40]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 15;
	setAttr ".sv2" 28;
createNode polyBridgeEdge -n "polyBridgeEdge14";
	rename -uid "3EF5715A-4D92-0F05-DBB9-958D50D8910B";
	setAttr ".ics" -type "componentList" 2 "e[22]" "e[49]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 10;
	setAttr ".sv2" 25;
createNode polyBridgeEdge -n "polyBridgeEdge15";
	rename -uid "15A6D3B0-47E5-307D-B715-D199FB17FB64";
	setAttr ".ics" -type "componentList" 2 "e[21]" "e[53]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 13;
	setAttr ".sv2" 35;
createNode deleteComponent -n "deleteComponent31";
	rename -uid "E40F7FCC-4DAF-03F6-624C-4BAB582A27ED";
	setAttr ".dc" -type "componentList" 1 "f[5]";
createNode polyCloseBorder -n "polyCloseBorder6";
	rename -uid "315959C0-4D4F-7B06-8F42-63B7CEB9E9EB";
	setAttr ".ics" -type "componentList" 1 "e[*]";
createNode polyBridgeEdge -n "polyBridgeEdge16";
	rename -uid "3909701C-4FBC-2EA7-CB06-6E8BBF003E9D";
	setAttr ".ics" -type "componentList" 2 "e[0]" "e[67]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 0;
	setAttr ".sv2" 39;
createNode polySplit -n "polySplit1";
	rename -uid "ECB24E5E-44B8-772E-980D-CEB9F729BB28";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483575 -2147483574;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyCloseBorder -n "polyCloseBorder7";
	rename -uid "9305A88D-4EEB-5478-EB4C-259D10551D1B";
	setAttr ".ics" -type "componentList" 1 "e[*]";
createNode deleteComponent -n "deleteComponent32";
	rename -uid "B26724A8-46B6-5CEB-B653-FEB4C1BBC714";
	setAttr ".dc" -type "componentList" 1 "f[31]";
createNode polyBridgeEdge -n "polyBridgeEdge17";
	rename -uid "64B2A372-45FC-A3B7-5A50-54B391ABAB91";
	setAttr ".ics" -type "componentList" 2 "e[0]" "e[68]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 0;
	setAttr ".sv2" 9;
createNode polySplit -n "polySplit2";
	rename -uid "B811BFE1-4E89-3F97-FA91-A982C54C2A2F";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483580 -2147483635;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode deleteComponent -n "deleteComponent33";
	rename -uid "B34E8139-4919-766E-B72E-3C935AC966F0";
	setAttr ".dc" -type "componentList" 1 "f[33]";
createNode polyBridgeEdge -n "polyBridgeEdge18";
	rename -uid "A0DCE3AE-40EF-7F48-F2EA-3E96CD81E316";
	setAttr ".ics" -type "componentList" 2 "e[0]" "e[64]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 0;
	setAttr ".sv2" 36;
createNode deleteComponent -n "deleteComponent34";
	rename -uid "A41880A0-450F-541C-D895-269AC43A9AF5";
	setAttr ".dc" -type "componentList" 1 "f[6]";
createNode polyBridgeEdge -n "polyBridgeEdge19";
	rename -uid "91CA73C0-422F-A160-8AF8-5F866C6C849C";
	setAttr ".ics" -type "componentList" 2 "e[1]" "e[64]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 3;
	setAttr ".sv2" 11;
createNode polyBridgeEdge -n "polyBridgeEdge20";
	rename -uid "9F5AF972-4CE6-7F56-A67F-179B67B64A5B";
	setAttr ".ics" -type "componentList" 2 "e[65]" "e[79]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 37;
	setAttr ".sv2" 3;
createNode deleteComponent -n "deleteComponent35";
	rename -uid "9D7FFB97-4CE3-E6F1-C391-EBB60E8B4B13";
	setAttr ".dc" -type "componentList" 1 "e[79]";
createNode polySplit -n "polySplit3";
	rename -uid "40D2288D-49B9-2F60-1478-CE95A4B9011F";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483589 -2147483632;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit4";
	rename -uid "76D44505-46C6-7DCA-76C8-F2AD9F0BA7DB";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483589 -2147483569;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode deleteComponent -n "deleteComponent36";
	rename -uid "35E4585C-4141-FEB9-73D9-4D996CA36AF1";
	setAttr ".dc" -type "componentList" 1 "f[6]";
createNode deleteComponent -n "deleteComponent37";
	rename -uid "0E2B4357-4D38-39EB-B584-3DB6B18A2F05";
	setAttr ".dc" -type "componentList" 1 "f[5]";
createNode polyBridgeEdge -n "polyBridgeEdge21";
	rename -uid "B5FF2D37-4AA7-EABF-6C0B-BEB4E06E12AC";
	setAttr ".ics" -type "componentList" 2 "e[4]" "e[63]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 2;
	setAttr ".sv2" 28;
createNode polyBridgeEdge -n "polyBridgeEdge22";
	rename -uid "992D9A59-4687-8695-5952-D6A95E512FAC";
	setAttr ".ics" -type "componentList" 2 "e[62]" "e[76]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 8;
	setAttr ".sv2" 28;
createNode polyBridgeEdge -n "polyBridgeEdge23";
	rename -uid "028FD379-4854-CFD6-9932-D1B31358DDD4";
	setAttr ".ics" -type "componentList" 2 "e[5]" "e[64]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 1;
	setAttr ".sv2" 10;
createNode polyBridgeEdge -n "polyBridgeEdge24";
	rename -uid "00E4A142-45FD-6B53-F60B-DB8127EF945D";
	setAttr ".ics" -type "componentList" 2 "e[65]" "e[78]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -3 2.5 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 35;
	setAttr ".sv2" 1;
createNode deleteComponent -n "deleteComponent38";
	rename -uid "EFDF2CEB-49B6-E9B4-6AEB-42862F0ED1A4";
	setAttr ".dc" -type "componentList" 1 "e[76]";
createNode deleteComponent -n "deleteComponent39";
	rename -uid "A8AFB331-481C-619A-6863-4DA869990736";
	setAttr ".dc" -type "componentList" 1 "e[77]";
createNode polySplit -n "polySplit5";
	rename -uid "3741FBE3-4A6C-5737-CC26-639F65643D89";
	setAttr -s 2 ".e[0:1]"  0 0;
	setAttr -s 2 ".d[0:1]"  -2147483576 -2147483612;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit6";
	rename -uid "25C6B6D5-4F8E-3BC5-C1A4-7CB01BA20204";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483644 -2147483612;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit7";
	rename -uid "8B760C2C-4C38-F054-8F6E-DE8034531217";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483643 -2147483601;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit8";
	rename -uid "EF1AA303-42FC-27C5-89D2-45AF7FEB7B48";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483643 -2147483601;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit9";
	rename -uid "417E8EC0-4AAC-4F68-48D9-11BBECA0BCC7";
	setAttr -s 2 ".e[0:1]"  0 0;
	setAttr -s 2 ".d[0:1]"  -2147483604 -2147483609;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit10";
	rename -uid "C859A0F1-4C86-9B6F-A52B-40A34D7540EC";
	setAttr -s 2 ".e[0:1]"  1 1;
	setAttr -s 2 ".d[0:1]"  -2147483604 -2147483609;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyExtrudeFace -n "polyExtrudeFace7";
	rename -uid "B4BB9685-4A52-984A-79BC-56B5B1566CCE";
	setAttr ".ics" -type "componentList" 1 "f[0]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 2 4.0999999999999996 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 2 4.1999998 ;
	setAttr ".rs" 35389;
	setAttr ".off" 0.15000000596046448;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1 0.5 4.2000000014901158 ;
	setAttr ".cbx" -type "double3" 1 3.5 4.2000000014901158 ;
createNode polyExtrudeFace -n "polyExtrudeFace8";
	rename -uid "F0E4A6D7-4ABF-4DD2-A8D7-C094095F5790";
	setAttr ".ics" -type "componentList" 1 "f[0]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 2 4.0999999999999996 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 1.925 4.1999998 ;
	setAttr ".rs" 56786;
	setAttr ".c[0]"  0 1 1;
	setAttr ".tk" -0.15000000596046448;
	setAttr ".cbn" -type "double3" -0.85000002384185791 0.5 4.199999904632568 ;
	setAttr ".cbx" -type "double3" 0.85000002384185791 3.3499999046325684 4.199999904632568 ;
createNode polyTweak -n "polyTweak3";
	rename -uid "FC6DB83D-4B13-DA59-6F3B-62A06204608D";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk[8:9]" -type "float3"  0 -0.14999993 0 0 -0.14999993
		 0;
createNode deleteComponent -n "deleteComponent40";
	rename -uid "757397B2-4E36-5DF4-12D6-AB974EB0EABB";
	setAttr ".dc" -type "componentList" 3 "f[3]" "f[6]" "f[10]";
createNode polyCloseBorder -n "polyCloseBorder8";
	rename -uid "4812597D-4281-B0C2-5033-F284E111508F";
	setAttr ".ics" -type "componentList" 1 "e[*]";
createNode polySplit -n "polySplit11";
	rename -uid "B1D4E4E7-4B3A-2016-285A-30A8EB364ABF";
	setAttr -s 2 ".e[0:1]"  0 0;
	setAttr -s 2 ".d[0:1]"  -2147483628 -2147483646;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit12";
	rename -uid "362CFE2B-4A22-D7CD-4CAA-828B470D992F";
	setAttr -s 2 ".e[0:1]"  1 1;
	setAttr -s 2 ".d[0:1]"  -2147483629 -2147483646;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyPipe -n "polyPipe1";
	rename -uid "BEBC3C84-4916-A909-0A7A-698F70BAA824";
	setAttr ".h" 0.4;
	setAttr ".t" 0.2;
	setAttr ".sc" 0;
createNode deleteComponent -n "deleteComponent41";
	rename -uid "F4E2388A-4F8E-E2CA-ACBD-449F6DED03BD";
	setAttr ".dc" -type "componentList" 1 "f[60:79]";
createNode polyCloseBorder -n "polyCloseBorder9";
	rename -uid "2325BC8D-4FE8-2977-41B7-E6AA61320F3C";
	setAttr ".ics" -type "componentList" 1 "e[60:79]";
createNode polyTweak -n "polyTweak4";
	rename -uid "1D94F6A2-4553-8313-6533-CA983BA7E8E1";
	setAttr ".uopa" yes;
	setAttr -s 22 ".tk";
	setAttr ".tk[0]" -type "float3" 0 0.060563702 1.3447843e-017 ;
	setAttr ".tk[1]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[2]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[3]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[4]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[5]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[6]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[7]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[8]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[9]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[10]" -type "float3" 0 0.060563702 1.3447843e-017 ;
	setAttr ".tk[11]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[12]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[13]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[14]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[15]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[16]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[17]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[18]" -type "float3" 0 0.060563702 0 ;
	setAttr ".tk[19]" -type "float3" 0 0.060563702 0 ;
createNode deleteComponent -n "deleteComponent42";
	rename -uid "C0F92F23-4BF2-4FA7-C87B-E2BF556F71B2";
	setAttr ".dc" -type "componentList" 2 "f[4]" "f[14]";
createNode deleteComponent -n "deleteComponent43";
	rename -uid "FCD5D508-492C-5A23-03AE-F5854D56B04D";
	setAttr ".dc" -type "componentList" 2 "f[8]" "f[17]";
createNode polyBridgeEdge -n "polyBridgeEdge25";
	rename -uid "676D2825-45EE-8F7B-A677-CCB005BF7993";
	setAttr ".ics" -type "componentList" 2 "e[20]" "e[30]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 24;
	setAttr ".sv2" 34;
createNode polyBridgeEdge -n "polyBridgeEdge26";
	rename -uid "A276272F-44F8-C61F-E425-B889D31D1845";
	setAttr ".ics" -type "componentList" 2 "e[81]" "e[90]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 25;
	setAttr ".sv2" 14;
createNode polyBridgeEdge -n "polyBridgeEdge27";
	rename -uid "5B884BEF-447D-482B-91B3-5CB4536F6B29";
	setAttr ".ics" -type "componentList" 2 "e[80]" "e[91]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 4;
	setAttr ".sv2" 35;
createNode polyBridgeEdge -n "polyBridgeEdge28";
	rename -uid "6289677D-4AB1-862A-465D-929AAC30E5F5";
	setAttr ".ics" -type "componentList" 2 "e[25]" "e[35]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 29;
	setAttr ".sv2" 39;
createNode polyBridgeEdge -n "polyBridgeEdge29";
	rename -uid "1ED114AB-44C8-EEBA-721F-ABBA6D8D78F4";
	setAttr ".ics" -type "componentList" 2 "e[86]" "e[95]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 30;
	setAttr ".sv2" 19;
createNode polyBridgeEdge -n "polyBridgeEdge30";
	rename -uid "8AB429CF-4372-1A29-93B9-8D8ED93C75C3";
	setAttr ".ics" -type "componentList" 2 "e[76]" "e[85]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 20;
	setAttr ".sv2" 9;
createNode polySplit -n "polySplit13";
	rename -uid "5C817093-4901-3CE1-F568-C09F66766FD8";
	setAttr -s 2 ".e[0:1]"  0.58142298 0.58020902;
	setAttr -s 2 ".d[0:1]"  -2147483510 -2147483512;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit14";
	rename -uid "8630E8C1-4B2F-206D-0B86-9795E3FEA785";
	setAttr -s 2 ".e[0:1]"  0.58053702 0.57979;
	setAttr -s 2 ".d[0:1]"  -2147483511 -2147483509;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit15";
	rename -uid "F2BC14A2-4386-CFE3-5895-6DA645FB51B3";
	setAttr -s 2 ".e[0:1]"  0.72284901 0.723728;
	setAttr -s 2 ".d[0:1]"  -2147483509 -2147483511;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit16";
	rename -uid "13FE7457-4E3E-5458-BFEA-5BA83AE6A26D";
	setAttr -s 2 ".e[0:1]"  0.71967399 0.72292;
	setAttr -s 2 ".d[0:1]"  -2147483510 -2147483512;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyTweak -n "polyTweak5";
	rename -uid "0A3B2BC9-488E-3FCA-EDA4-FCA84B6FCBE9";
	setAttr ".uopa" yes;
	setAttr -s 19 ".tk";
	setAttr ".tk[80]" -type "float3" 0.00030010194 0 -0.0018947721 ;
	setAttr ".tk[82]" -type "float3" 0.00030163844 0 -0.0019044748 ;
	setAttr ".tk[83]" -type "float3" 0.00022141426 0 -0.001397955 ;
	setAttr ".tk[84]" -type "float3" 0 0 1.4901161e-008 ;
	setAttr ".tk[85]" -type "float3" 0.00025964156 -2.7755576e-017 -0.0016392916 ;
	setAttr ".tk[86]" -type "float3" -0.00024961308 6.9388939e-018 0.0015760064 ;
	setAttr ".tk[87]" -type "float3" -3.7252903e-009 0 -2.9802322e-008 ;
createNode deleteComponent -n "deleteComponent44";
	rename -uid "D2E97747-490B-7AE0-DE44-928FB5709B6F";
	setAttr ".dc" -type "componentList" 1 "f[60]";
createNode deleteComponent -n "deleteComponent45";
	rename -uid "74A2E56F-45C8-36EB-0123-E5A2F534856C";
	setAttr ".dc" -type "componentList" 1 "f[60]";
createNode deleteComponent -n "deleteComponent46";
	rename -uid "5E1F4591-4B78-2C87-C1B2-7999655C6C5A";
	setAttr ".dc" -type "componentList" 1 "f[60]";
createNode deleteComponent -n "deleteComponent47";
	rename -uid "31442358-4D7B-6CA4-71C4-99B1460D0893";
	setAttr ".dc" -type "componentList" 1 "f[63]";
createNode deleteComponent -n "deleteComponent48";
	rename -uid "823C8544-48CE-8378-E143-AA88349E6E19";
	setAttr ".dc" -type "componentList" 1 "f[59]";
createNode polySplit -n "polySplit17";
	rename -uid "B0666B8D-4C14-9C18-0A70-E8A3F3B9BFE7";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483512 -2147483502;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit18";
	rename -uid "A19F74F7-4D08-AA8A-DA20-CB94CABF28DA";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483505 -2147483500;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyBridgeEdge -n "polyBridgeEdge31";
	rename -uid "B9D91329-4215-6D48-D442-3E91D2529A5D";
	setAttr ".ics" -type "componentList" 2 "e[25]" "e[148]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 29;
	setAttr ".sv2" 81;
createNode polyBridgeEdge -n "polyBridgeEdge32";
	rename -uid "8DF795A5-4A08-EAFD-BBE0-8DBD86DB4232";
	setAttr ".ics" -type "componentList" 2 "e[85]" "e[149]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 9;
	setAttr ".sv2" 87;
createNode polyBridgeEdge -n "polyBridgeEdge33";
	rename -uid "653F9D75-4E6E-67AC-F338-4788BB705516";
	setAttr ".ics" -type "componentList" 3 "e[62]" "e[86]" "e[142]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 30;
	setAttr ".sv2" 80;
createNode polyBridgeEdge -n "polyBridgeEdge34";
	rename -uid "D663BC41-4D94-35AA-5056-968BE27C8669";
	setAttr ".ics" -type "componentList" 2 "e[35]" "e[146]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 39;
	setAttr ".sv2" 85;
createNode polyBridgeEdge -n "polyBridgeEdge35";
	rename -uid "2EF18744-41C5-E756-1EEF-8B81AFD28280";
	setAttr ".ics" -type "componentList" 2 "e[95]" "e[145]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 19;
	setAttr ".sv2" 82;
createNode polyBridgeEdge -n "polyBridgeEdge36";
	rename -uid "F78DBBEA-44B2-D3BC-E209-668C12F028A6";
	setAttr ".ics" -type "componentList" 2 "e[76]" "e[147]";
	setAttr ".ix" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 11.61728236533191 4.0999999999999996 1;
	setAttr ".c[0]"  0 1 1;
	setAttr ".dv" 0;
	setAttr ".sv1" 20;
	setAttr ".sv2" 84;
createNode polyCloseBorder -n "polyCloseBorder10";
	rename -uid "30F941E2-4535-84A6-27A6-6F9DD963DCF4";
	setAttr ".ics" -type "componentList" 1 "e[*]";
createNode transformGeometry -n "transformGeometry1";
	rename -uid "30986C3C-4AB9-80BD-733A-B4A8B0244E61";
	setAttr ".txf" -type "matrix" 0.98768834059513777 0.15643446504023087 0 0 -2.7755575615628914e-017 2.2204460492503131e-016 1 0
		 0.15643446504023087 -0.98768834059513777 2.2204460492503131e-016 0 0 0 0 1;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr ".fprt" yes;
select -ne :renderPartition;
	setAttr -s 2 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 4 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :initialShadingGroup;
	setAttr -s 17 ".dsm";
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :ikSystem;
	setAttr -s 4 ".sol";
connectAttr "polyCube1.out" "pCubeShape1.i";
connectAttr "polyCube2.out" "pCubeShape2.i";
connectAttr "polyExtrudeFace1.out" "pCubeShape3.i";
connectAttr "deleteComponent2.og" "pCubeShape4.i";
connectAttr "polyCube5.out" "pCubeShape5.i";
connectAttr "deleteComponent4.og" "pCubeShape7.i";
connectAttr "polyBridgeEdge2.out" "pCubeShape8.i";
connectAttr "polyBridgeEdge4.out" "pCubeShape9.i";
connectAttr "polySplit10.out" "pCubeShape10.i";
connectAttr "polySplit12.out" "pCubeShape12.i";
connectAttr "transformGeometry1.og" "pPipeShape1.i";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "polyCube3.out" "polyExtrudeFace1.ip";
connectAttr "pCubeShape3.wm" "polyExtrudeFace1.mp";
connectAttr "polyCube4.out" "deleteComponent1.ig";
connectAttr "deleteComponent1.og" "deleteComponent2.ig";
connectAttr "polyCube6.out" "deleteComponent3.ig";
connectAttr "deleteComponent3.og" "deleteComponent4.ig";
connectAttr "polySurfaceShape1.o" "polyExtrudeFace2.ip";
connectAttr "pCubeShape8.wm" "polyExtrudeFace2.mp";
connectAttr "polyExtrudeFace2.out" "deleteComponent5.ig";
connectAttr "deleteComponent5.og" "deleteComponent6.ig";
connectAttr "deleteComponent6.og" "deleteComponent7.ig";
connectAttr "deleteComponent7.og" "polyBridgeEdge1.ip";
connectAttr "pCubeShape8.wm" "polyBridgeEdge1.mp";
connectAttr "polyBridgeEdge1.out" "polyBridgeEdge2.ip";
connectAttr "pCubeShape8.wm" "polyBridgeEdge2.mp";
connectAttr "polySurfaceShape2.o" "polyExtrudeFace3.ip";
connectAttr "pCubeShape9.wm" "polyExtrudeFace3.mp";
connectAttr "polyExtrudeFace3.out" "deleteComponent8.ig";
connectAttr "deleteComponent8.og" "deleteComponent9.ig";
connectAttr "deleteComponent9.og" "deleteComponent10.ig";
connectAttr "deleteComponent10.og" "polyBridgeEdge3.ip";
connectAttr "pCubeShape9.wm" "polyBridgeEdge3.mp";
connectAttr "polyBridgeEdge3.out" "polyBridgeEdge4.ip";
connectAttr "pCubeShape9.wm" "polyBridgeEdge4.mp";
connectAttr "polyCube7.out" "polyExtrudeFace4.ip";
connectAttr "pCubeShape10.wm" "polyExtrudeFace4.mp";
connectAttr "polyExtrudeFace4.out" "polyExtrudeFace5.ip";
connectAttr "pCubeShape10.wm" "polyExtrudeFace5.mp";
connectAttr "polyExtrudeFace5.out" "deleteComponent11.ig";
connectAttr "deleteComponent11.og" "polyBridgeEdge5.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge5.mp";
connectAttr "polyTweak1.out" "polyCloseBorder1.ip";
connectAttr "polyBridgeEdge5.out" "polyTweak1.ip";
connectAttr "polyCloseBorder1.out" "deleteComponent12.ig";
connectAttr "deleteComponent12.og" "polyCloseBorder2.ip";
connectAttr "polyCloseBorder2.out" "deleteComponent13.ig";
connectAttr "deleteComponent13.og" "deleteComponent14.ig";
connectAttr "deleteComponent14.og" "polyCloseBorder3.ip";
connectAttr "polyCloseBorder3.out" "deleteComponent15.ig";
connectAttr "deleteComponent15.og" "deleteComponent16.ig";
connectAttr "deleteComponent16.og" "deleteComponent17.ig";
connectAttr "deleteComponent17.og" "polyBridgeEdge6.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge6.mp";
connectAttr "polyBridgeEdge6.out" "polyBridgeEdge7.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge7.mp";
connectAttr "polyBridgeEdge7.out" "polyTweak2.ip";
connectAttr "polyTweak2.out" "deleteComponent18.ig";
connectAttr "deleteComponent18.og" "deleteComponent19.ig";
connectAttr "deleteComponent19.og" "polyCloseBorder4.ip";
connectAttr "polyCloseBorder4.out" "deleteComponent20.ig";
connectAttr "deleteComponent20.og" "deleteComponent21.ig";
connectAttr "deleteComponent21.og" "polyExtrudeFace6.ip";
connectAttr "pCubeShape10.wm" "polyExtrudeFace6.mp";
connectAttr "polyExtrudeFace6.out" "deleteComponent22.ig";
connectAttr "deleteComponent22.og" "deleteComponent23.ig";
connectAttr "deleteComponent23.og" "deleteComponent24.ig";
connectAttr "deleteComponent24.og" "polyBridgeEdge8.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge8.mp";
connectAttr "polyBridgeEdge8.out" "polyBridgeEdge9.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge9.mp";
connectAttr "polyBridgeEdge9.out" "polyCloseBorder5.ip";
connectAttr "polyCloseBorder5.out" "deleteComponent25.ig";
connectAttr "deleteComponent25.og" "deleteComponent26.ig";
connectAttr "deleteComponent26.og" "polyBridgeEdge10.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge10.mp";
connectAttr "polyBridgeEdge10.out" "polyBridgeEdge11.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge11.mp";
connectAttr "polyBridgeEdge11.out" "deleteComponent27.ig";
connectAttr "deleteComponent27.og" "deleteComponent28.ig";
connectAttr "deleteComponent28.og" "deleteComponent29.ig";
connectAttr "deleteComponent29.og" "deleteComponent30.ig";
connectAttr "deleteComponent30.og" "polyBridgeEdge12.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge12.mp";
connectAttr "polyBridgeEdge12.out" "polyBridgeEdge13.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge13.mp";
connectAttr "polyBridgeEdge13.out" "polyBridgeEdge14.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge14.mp";
connectAttr "polyBridgeEdge14.out" "polyBridgeEdge15.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge15.mp";
connectAttr "polyBridgeEdge15.out" "deleteComponent31.ig";
connectAttr "deleteComponent31.og" "polyCloseBorder6.ip";
connectAttr "polyCloseBorder6.out" "polyBridgeEdge16.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge16.mp";
connectAttr "polyBridgeEdge16.out" "polySplit1.ip";
connectAttr "polySplit1.out" "polyCloseBorder7.ip";
connectAttr "polyCloseBorder7.out" "deleteComponent32.ig";
connectAttr "deleteComponent32.og" "polyBridgeEdge17.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge17.mp";
connectAttr "polyBridgeEdge17.out" "polySplit2.ip";
connectAttr "polySplit2.out" "deleteComponent33.ig";
connectAttr "deleteComponent33.og" "polyBridgeEdge18.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge18.mp";
connectAttr "polyBridgeEdge18.out" "deleteComponent34.ig";
connectAttr "deleteComponent34.og" "polyBridgeEdge19.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge19.mp";
connectAttr "polyBridgeEdge19.out" "polyBridgeEdge20.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge20.mp";
connectAttr "polyBridgeEdge20.out" "deleteComponent35.ig";
connectAttr "deleteComponent35.og" "polySplit3.ip";
connectAttr "polySplit3.out" "polySplit4.ip";
connectAttr "polySplit4.out" "deleteComponent36.ig";
connectAttr "deleteComponent36.og" "deleteComponent37.ig";
connectAttr "deleteComponent37.og" "polyBridgeEdge21.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge21.mp";
connectAttr "polyBridgeEdge21.out" "polyBridgeEdge22.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge22.mp";
connectAttr "polyBridgeEdge22.out" "polyBridgeEdge23.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge23.mp";
connectAttr "polyBridgeEdge23.out" "polyBridgeEdge24.ip";
connectAttr "pCubeShape10.wm" "polyBridgeEdge24.mp";
connectAttr "polyBridgeEdge24.out" "deleteComponent38.ig";
connectAttr "deleteComponent38.og" "deleteComponent39.ig";
connectAttr "deleteComponent39.og" "polySplit5.ip";
connectAttr "polySplit5.out" "polySplit6.ip";
connectAttr "polySplit6.out" "polySplit7.ip";
connectAttr "polySplit7.out" "polySplit8.ip";
connectAttr "polySplit8.out" "polySplit9.ip";
connectAttr "polySplit9.out" "polySplit10.ip";
connectAttr "polySurfaceShape3.o" "polyExtrudeFace7.ip";
connectAttr "pCubeShape12.wm" "polyExtrudeFace7.mp";
connectAttr "polyTweak3.out" "polyExtrudeFace8.ip";
connectAttr "pCubeShape12.wm" "polyExtrudeFace8.mp";
connectAttr "polyExtrudeFace7.out" "polyTweak3.ip";
connectAttr "polyExtrudeFace8.out" "deleteComponent40.ig";
connectAttr "deleteComponent40.og" "polyCloseBorder8.ip";
connectAttr "polyCloseBorder8.out" "polySplit11.ip";
connectAttr "polySplit11.out" "polySplit12.ip";
connectAttr "polyPipe1.out" "deleteComponent41.ig";
connectAttr "polyTweak4.out" "polyCloseBorder9.ip";
connectAttr "deleteComponent41.og" "polyTweak4.ip";
connectAttr "polyCloseBorder9.out" "deleteComponent42.ig";
connectAttr "deleteComponent42.og" "deleteComponent43.ig";
connectAttr "deleteComponent43.og" "polyBridgeEdge25.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge25.mp";
connectAttr "polyBridgeEdge25.out" "polyBridgeEdge26.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge26.mp";
connectAttr "polyBridgeEdge26.out" "polyBridgeEdge27.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge27.mp";
connectAttr "polyBridgeEdge27.out" "polyBridgeEdge28.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge28.mp";
connectAttr "polyBridgeEdge28.out" "polyBridgeEdge29.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge29.mp";
connectAttr "polyBridgeEdge29.out" "polyBridgeEdge30.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge30.mp";
connectAttr "polyBridgeEdge30.out" "polySplit13.ip";
connectAttr "polySplit13.out" "polySplit14.ip";
connectAttr "polySplit14.out" "polySplit15.ip";
connectAttr "polySplit15.out" "polySplit16.ip";
connectAttr "polySplit16.out" "polyTweak5.ip";
connectAttr "polyTweak5.out" "deleteComponent44.ig";
connectAttr "deleteComponent44.og" "deleteComponent45.ig";
connectAttr "deleteComponent45.og" "deleteComponent46.ig";
connectAttr "deleteComponent46.og" "deleteComponent47.ig";
connectAttr "deleteComponent47.og" "deleteComponent48.ig";
connectAttr "deleteComponent48.og" "polySplit17.ip";
connectAttr "polySplit17.out" "polySplit18.ip";
connectAttr "polySplit18.out" "polyBridgeEdge31.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge31.mp";
connectAttr "polyBridgeEdge31.out" "polyBridgeEdge32.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge32.mp";
connectAttr "polyBridgeEdge32.out" "polyBridgeEdge33.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge33.mp";
connectAttr "polyBridgeEdge33.out" "polyBridgeEdge34.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge34.mp";
connectAttr "polyBridgeEdge34.out" "polyBridgeEdge35.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge35.mp";
connectAttr "polyBridgeEdge35.out" "polyBridgeEdge36.ip";
connectAttr "pPipeShape1.wm" "polyBridgeEdge36.mp";
connectAttr "polyBridgeEdge36.out" "polyCloseBorder10.ip";
connectAttr "polyCloseBorder10.out" "transformGeometry1.ig";
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
connectAttr "pCubeShape1.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape2.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape3.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape4.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape5.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape6.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape7.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape8.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape9.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape10.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape11.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape12.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pPipeShape1.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape13.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape14.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape15.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pPipeShape2.iog" ":initialShadingGroup.dsm" -na;
// End of House01.ma
