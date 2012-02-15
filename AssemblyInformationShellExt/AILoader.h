// AILoader.h : Declaration of the CAILoader

#pragma once
#include "resource.h"       // main symbols



#include "AssemblyInformation_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CAILoader


class ATL_NO_VTABLE CAILoader :
    public CComObjectRootEx<CComSingleThreadModel>,
    public CComCoClass<CAILoader, &CLSID_AILoader>,
    public IShellExtInit,
    public IContextMenu
{
public:
    CAILoader();
    ~CAILoader();

    // IShellExtInit
    STDMETHOD(Initialize)(LPCITEMIDLIST, LPDATAOBJECT, HKEY);

    // IContextMenu
    STDMETHOD(GetCommandString)(UINT_PTR, UINT, UINT*, LPSTR, UINT);
    STDMETHOD(InvokeCommand)(LPCMINVOKECOMMANDINFO);
    STDMETHOD(QueryContextMenu)(HMENU, UINT, UINT, UINT, UINT);

    DECLARE_REGISTRY_RESOURCEID(IDR_AILOADER)
    DECLARE_NOT_AGGREGATABLE(CAILoader)
    DECLARE_PROTECT_FINAL_CONSTRUCT()

    BEGIN_COM_MAP(CAILoader)
        COM_INTERFACE_ENTRY(IShellExtInit)
        COM_INTERFACE_ENTRY(IContextMenu)
    END_COM_MAP()

protected:
    HBITMAP     m_hRegBmp;
    HBITMAP     m_hUnregBmp;
    string_list m_lsFiles;
public:
	static TCHAR szAIAppPath[_MAX_ENV];
};
