// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the EXAMPLEDLLMOD_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// EXAMPLEDLLMOD_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef EXAMPLEDLLMOD_EXPORTS
#define EXAMPLEDLLMOD_API __declspec(dllexport)
#else
#define EXAMPLEDLLMOD_API __declspec(dllimport)
#endif

// This class is exported from the dll
class EXAMPLEDLLMOD_API CExampleDLLMod {
public:
	CExampleDLLMod(void);
	// TODO: add your methods here.
};

extern EXAMPLEDLLMOD_API int nExampleDLLMod;

EXAMPLEDLLMOD_API int fnExampleDLLMod(void);
