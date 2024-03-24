// <copyright file="dllmain.cpp" company="www.feucht.us">
// Copyright (c) 2018 www.feucht.us. All rights reserved.
// </copyright>
// <author>Jonathan Feucht</author>
// <date>1/21/2018</date>
// <summary>Implements the dllmain class</summary>

#include "stdafx.h"

/// <summary>DLL main.</summary>
/// <param name="hModule">The module.</param>
/// <param name="ul_reason_for_call">The ul reason for call.</param>
/// <param name="lpReserved">The reserved.</param>
/// <returns>An APIENTRY.</returns>
BOOL APIENTRY DllMain(HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
) {
	switch (ul_reason_for_call) {
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			break;
	}
	return TRUE;
}

