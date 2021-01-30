// ExampleDLLMod.cpp : Defines the exported functions for the DLL.
//

#include "pch.h"
#include "framework.h"
#include "ExampleDLLMod.h"


// This is an example of an exported variable
EXAMPLEDLLMOD_API int nExampleDLLMod=0;

// This is an example of an exported function.
EXAMPLEDLLMOD_API int fnExampleDLLMod(void)
{
    return 0;
}

// This is the constructor of a class that has been exported.
CExampleDLLMod::CExampleDLLMod()
{
    return;
}
