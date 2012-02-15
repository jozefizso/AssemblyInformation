// AILoader.cpp : Implementation of CAILoader

#include "stdafx.h"
#include "AILoader.h"
#include "resource.h"
#include "AILoader.h"
#include "AssemblyInformation_i.h"
//#include "ProgressDlg.h"
#include "M:\Ashutosh\My Documents\Visual Studio 2008\Projects\Common\CPP\Debug.h"

TCHAR CAILoader::szAIAppPath[_MAX_ENV];

/////////////////////////////////////////////////////////////////////////////
// CAILoader construction/destruction

CAILoader::CAILoader()
{
	TRACE_FUNCTION(_T("CAILoader::CAILoader"));
	m_hRegBmp = LoadBitmap ( _AtlModule.GetModuleInstance(),
		MAKEINTRESOURCE(IDB_REGISTERBMP) );

	/*m_hUnregBmp = LoadBitmap ( _Module.GetModuleInstance(),
		MAKEINTRESOURCE(IDB_UNREGISTERBMP) );*/
}

CAILoader::~CAILoader()
{
	TRACE_FUNCTION(_T("CAILoader::~CAILoader"));
	if ( NULL != m_hRegBmp )
		DeleteObject ( m_hRegBmp );

	if ( NULL != m_hUnregBmp )
		DeleteObject ( m_hUnregBmp );
}


/////////////////////////////////////////////////////////////////////////////
// CAILoader IShellExtInit methods

//////////////////////////////////////////////////////////////////////////
//
// Function:    Initialize()
//
// Description:
//  Reads in the list of selected folders and stores them for later use.
//
//////////////////////////////////////////////////////////////////////////

HRESULT CAILoader::Initialize (LPCITEMIDLIST pidlFolder, LPDATAOBJECT pDO, HKEY hProgID )
{
	TRACE_FUNCTION(_T("CAILoader::Initialize"));
	TCHAR     szFile[MAX_PATH]/*, szCurrDir[MAX_PATH]*/;
	UINT      uNumFiles;
	HDROP     hdrop;
	FORMATETC etc = { CF_HDROP, NULL, DVASPECT_CONTENT, -1, TYMED_HGLOBAL };
	STGMEDIUM stg = { TYMED_HGLOBAL };
	//HINSTANCE hinst;
	bool      bChangedDir = false;
	DWORD     dwLoadLibFlags = 0;
	//HRESULT (STDAPICALLTYPE* pfn)();
	
	if ( (GetVersion() & 0x80000000) == 0 )
		dwLoadLibFlags = DONT_RESOLVE_DLL_REFERENCES;

	// Read the list of folders from the data object.  They're stored in HDROP
	// form, so just get the HDROP handle and then use the drag 'n' drop APIs
	// on it.
	if ( FAILED( pDO->GetData ( &etc, &stg ) ))
		return E_INVALIDARG;

	// Get an HDROP handle.
	hdrop = (HDROP) GlobalLock ( stg.hGlobal );

	if ( NULL == hdrop )
	{
		ReleaseStgMedium ( &stg );
		return E_INVALIDARG;
	}

	// Determine how many files are involved in this operation.
	uNumFiles = DragQueryFile ( hdrop, 0xFFFFFFFF, NULL, 0 );

	if(uNumFiles != 1) return E_INVALIDARG;
	for ( UINT uFile = 0; uFile < uNumFiles; uFile++ )
	{
		// Get the next filename.
		if ( 0 == DragQueryFile ( hdrop, uFile, szFile, MAX_PATH ) )
			continue;

		ATLTRACE("Checking file <%s>\n", szFile);
		TCHAR* pszExtensionPtr;

		pszExtensionPtr = PathFindExtension(szFile);
		if(NULL != pszExtensionPtr)
		{
			//pszExtensionPtr++;
			TCHAR szExtension[MAX_PATH];
			
			lstrcpyn ( szExtension, pszExtensionPtr, countof(szExtension) );
			
			//_tcslwr(szExtension);
			_tcslwr_s(szExtension, countof(szExtension));
			ATLTRACE("Extension is <%s>\n", szExtension);
			if(_tcscmp(szExtension, _T(".exe")) == 0 || _tcscmp(szExtension, _T(".dll")) == 0)
			{
				m_lsFiles.push_back ( szFile );
				ATLTRACE("Accepted file <%s>\n", szFile);
			}
		}

		
		// If this is the first time thru the for loop, we need to
		// change the current directory to the one that was open in
		// explorer.  This is done so the DLLs we're about to load
		// can load other DLLs they depend on to be in the same
		// directory.
		//if ( !bChangedDir )
		//{
		//	TCHAR szFolder[MAX_PATH];

		//	lstrcpyn ( szFolder, szFile, countof(szFolder) );
		//	PathRemoveFileSpec ( szFolder );

		//	// Save the current directory, and then change it.
		//	GetCurrentDirectory ( MAX_PATH, szCurrDir );

		//	if ( SetCurrentDirectory ( szFolder ) )
		//		bChangedDir = true;
		//}   // end if

		// Try & load the DLL.
		//hinst = LoadLibraryEx ( szFile, NULL, dwLoadLibFlags );

		//if ( NULL == hinst )
		//{
		//	ATLTRACE("Error: LoadLibraryEx() failed, error = %lu\n", GetLastError());
		//	continue;
		//}

		//// Get the address of DllRegisterServer();
		//(FARPROC&) pfn = GetProcAddress ( hinst, "DllRegisterServer" );

		//// If it wasn't found, skip the file.
		//if ( NULL == pfn )
		//{
		//	FreeLibrary ( hinst );
		//	ATLTRACE("DllRegisterServer export not found\n");
		//	continue;
		//}

		//// Get the address of DllUnregisterServer();
		//(FARPROC&) pfn = GetProcAddress ( hinst, "DllUnregisterServer" );

		//// If it was found, we can operate on the file, so add it to
		//// our list o' files that we will register/unregister.
		//if ( NULL != pfn )
		//	m_lsFiles.push_back ( szFile );
		//else
		//	ATLTRACE("DllUnregisterServer export not found\n");

		//FreeLibrary ( hinst );
	}   // end for

	// Change back to the original directory if we changed it.
	/*if ( bChangedDir )
		SetCurrentDirectory ( szCurrDir );*/

	// Release resources.
	GlobalUnlock ( stg.hGlobal );
	ReleaseStgMedium ( &stg );

	// If we found any files we can work with, return S_OK.  Otherwise,
	// return E_INVALIDARG so we don't get called again for this right-click
	// operation.
	return (m_lsFiles.size() > 0) ? S_OK : E_INVALIDARG;
}


/////////////////////////////////////////////////////////////////////////////
// CAILoader IContextMenu methods

//////////////////////////////////////////////////////////////////////////
//
// Function:    QueryContextMenu()
//
// Description:
//  Adds our items to the supplied menu.
//
//////////////////////////////////////////////////////////////////////////

HRESULT CAILoader::QueryContextMenu (HMENU hmenu, UINT uMenuIndex, UINT uidFirstCmd, UINT uidLastCmd, UINT uFlags )
{
	TRACE_FUNCTION(_T("CAILoader::QueryContextMenu"));
	UINT uCmdID = uidFirstCmd;

	// If the flags include CMF_DEFAULTONLY then we shouldn't do anything.
	if ( uFlags & CMF_DEFAULTONLY )
		return MAKE_HRESULT ( SEVERITY_SUCCESS, FACILITY_NULL, 0 );

	// Add our register/unregister items.
	InsertMenu ( hmenu, uMenuIndex, MF_STRING | MF_BYPOSITION, uCmdID++,
		_T("Assembly Information") );

	// Set the bitmap for the register item.
	if ( NULL != m_hRegBmp )
		SetMenuItemBitmaps ( hmenu, uMenuIndex, MF_BYPOSITION, m_hRegBmp, NULL );

	uMenuIndex++;

	//InsertMenu ( hmenu, uMenuIndex, MF_STRING | MF_BYPOSITION, uCmdID++,
	//	_T("Unregister server(s)") );

	//// Set the bitmap for the unregister item.
	//if ( NULL != m_hUnregBmp )
	//	SetMenuItemBitmaps ( hmenu, uMenuIndex, MF_BYPOSITION, m_hUnregBmp, NULL );

	//uMenuIndex++;

	// The return value tells the shell how many top-level items we added.
	return MAKE_HRESULT ( SEVERITY_SUCCESS, FACILITY_NULL, 1 );
}


//////////////////////////////////////////////////////////////////////////
//
// Function:    GetCommandString()
//
// Description:
//  Sets the flyby help string for the Explorer status bar.
//
//////////////////////////////////////////////////////////////////////////

HRESULT CAILoader::GetCommandString (
	UINT_PTR uCmdID, UINT uFlags, UINT* puReserved, LPSTR szName, UINT cchMax )
{
	TRACE_FUNCTION(_T("CAILoader::GetCommandString"));
	USES_CONVERSION;
	LPCTSTR szPrompt;

	if ( uFlags & GCS_HELPTEXT )
	{
		switch ( uCmdID )
		{
		case 0:
			szPrompt = _T("Shows Assembly Information");
			break;

		/*case 1:
			szPrompt = _T("Unregister all selected COM servers");
			break;*/

		default:
			ATLASSERT(0);           // should never get here
			return E_INVALIDARG;
			break;
		}

		// Copy the help text into the supplied buffer.  If the shell wants
		// a Unicode string, we need to case szName to an LPCWSTR.
		if ( uFlags & GCS_UNICODE )
			lstrcpynW ( (LPWSTR) szName, T2CW(szPrompt), cchMax );
		else
			lstrcpynA ( szName, T2CA(szPrompt), cchMax );
	}
	else if ( uFlags & GCS_VERB )
	{
		// Copy the verb name into the supplied buffer.  If the shell wants
		// a Unicode string, we need to case szName to an LPCWSTR.
		switch ( uCmdID )
		{
		case 0:
			if ( uFlags & GCS_UNICODE )
				lstrcpynW ( (LPWSTR) szName, L"AssemblyInformation", cchMax );
			else
				lstrcpynA ( szName, "AssemblyInformation", cchMax );
			break;

		/*case 1:
			if ( uFlags & GCS_UNICODE )
				lstrcpynW ( (LPWSTR) szName, L"DllUnregSvr", cchMax );
			else
				lstrcpynA ( szName, "DllUnregSvr", cchMax );
			break;*/

		default:
			ATLASSERT(0);           // should never get here
			return E_INVALIDARG;
			break;
		}
	}

	return S_OK;
}


//////////////////////////////////////////////////////////////////////////
//
// Function:    InvokeCommand()
//
// Description:
//  Carries out the selected command.
//
//////////////////////////////////////////////////////////////////////////

HRESULT CAILoader::InvokeCommand ( LPCMINVOKECOMMANDINFO pInfo )
{
	TRACE_FUNCTION(_T("CAILoader::InvokeCommand"));
	// If lpVerb really points to a string, ignore this function call and bail out.  
	if ( 0 != HIWORD( pInfo->lpVerb ) )
		return E_INVALIDARG;

	// Check that lpVerb is one of our commands (0 or 1)
	switch ( LOWORD(pInfo->lpVerb) )
	{
	case 0:
	case 1:
		{
			TCHAR szFile[_MAX_ENV];
			std::basic_string<TCHAR>::size_type spacePos = m_lsFiles.front().find(_T(" "));
			if(spacePos < 0 || spacePos > m_lsFiles.front().length())
			{
				//_tcscpy(szFile, m_lsFiles.front().c_str());
				_tcscpy_s(szFile, countof(szFile), m_lsFiles.front().c_str());
			}
			else
			{
				_stprintf_s(szFile, countof(szFile), _T("\"%s\""), m_lsFiles.front().c_str());
			}
			
			ATLTRACE("Opening AI Launcher for <%s>\n", szFile);
			ShellExecute(0, _T("open"), szAIAppPath, szFile, _T(""), SW_SHOW);
			return S_OK;
		}
		break;

	default:
		ATLASSERT(0);               // should never get here
		return E_INVALIDARG;
		break;
	}
}


