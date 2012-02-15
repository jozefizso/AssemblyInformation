// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently,
// but are changed infrequently

#pragma once

#ifndef STRICT
#define STRICT
#endif

#include "targetver.h"

#define _ATL_APARTMENT_THREADED

#define _ATL_NO_AUTOMATIC_NAMESPACE

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS	// some CString constructors will be explicit


#define ATL_NO_ASSERT_ON_DESTROY_NONEXISTENT_WINDOW

#include "resource.h"
#include <atlbase.h>

#include <atlcom.h>
#include <atlctl.h>


// STL
#include <string>
#include <list>
typedef std::list< std::basic_string<TCHAR> > string_list;

// Win32
#include <commctrl.h>
#include <comdef.h>
#include <shlobj.h>
#include <shlguid.h>
extern ATL::CComModule _AtlModule;
// Utility macros
#define countof(x) (sizeof(x)/sizeof((x)[0]))