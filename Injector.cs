using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace V2MemInjector
{
    class Injector
    {
		/// <summary>
		/// I copied a lot of the code from here:
		/// https://guidedhacking.com/threads/c-dll-injector-tutorial-how-to-inject-a-dll.14915/
		/// I'm sorry, you can take my programmer card away, I don't care, I don't want to reinvent the wheel
		/// Hopefully the Copyright allows this :/
		/// </summary>

		public const int MAX_PATH = 260;
		private const int INVALID_HANDLE_VALUE = -1;

		[Flags]
		public enum ProcessAccessFlags : uint
		{
			All = 0x001F0FFF,
			Terminate = 0x00000001,
			CreateThread = 0x00000002,
			VirtualMemoryOperation = 0x00000008,
			VirtualMemoryRead = 0x00000010,
			VirtualMemoryWrite = 0x00000020,
			DuplicateHandle = 0x00000040,
			CreateProcess = 0x000000080,
			SetQuota = 0x00000100,
			SetInformation = 0x00000200,
			QueryInformation = 0x00000400,
			QueryLimitedInformation = 0x00001000,
			Synchronize = 0x00100000
		}
		[Flags]
		private enum SnapshotFlags : uint
		{
			HeapList = 0x00000001,
			Process = 0x00000002,
			Thread = 0x00000004,
			Module = 0x00000008,
			Module32 = 0x00000010,
			Inherit = 0x80000000,
			All = 0x0000001F,
			NoHeaps = 0x40000000
		}

		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
		public struct PROCESSENTRY32
		{
			public uint dwSize;
			public uint cntUsage;
			public uint th32ProcessID;
			public IntPtr th32DefaultHeapID;
			public uint th32ModuleID;
			public uint cntThreads;
			public uint th32ParentProcessID;
			public int pcPriClassBase;
			public uint dwFlags;
			[System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string szExeFile;
		};

		[StructLayout(LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
		public struct MODULEENTRY32
		{
			internal uint dwSize;
			internal uint th32ModuleID;
			internal uint th32ProcessID;
			internal uint GlblcntUsage;
			internal uint ProccntUsage;
			internal IntPtr modBaseAddr;
			internal uint modBaseSize;
			internal IntPtr hModule;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string szModule;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string szExePath;
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadProcessMemory(
		IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(
		IntPtr hProcess, IntPtr lpBaseAddress, [MarshalAs(UnmanagedType.AsAny)] object lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);

		[DllImport("kernel32.dll")]
		private static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		[DllImport("kernel32.dll")]
		private static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		[DllImport("kernel32.dll")]
		private static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

		[DllImport("kernel32.dll")]
		private static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr hHandle);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetModuleHandle(string moduleName);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr CreateToolhelp32Snapshot(SnapshotFlags dwFlags, int th32ProcessID);

		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		[DllImport("kernel32.dll")]
		private static extern IntPtr CreateRemoteThread(IntPtr hProcess,
		   IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress,
		   IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);

		[Flags]
		public enum AllocationType
		{
			Commit = 0x1000,
			Reserve = 0x2000,
			Decommit = 0x4000,
			Release = 0x8000,
			Reset = 0x80000,
			Physical = 0x400000,
			TopDown = 0x100000,
			WriteWatch = 0x200000,
			LargePages = 0x20000000
		}

		[Flags]
		public enum MemoryProtection
		{
			Execute = 0x10,
			ExecuteRead = 0x20,
			ExecuteReadWrite = 0x40,
			ExecuteWriteCopy = 0x80,
			NoAccess = 0x01,
			ReadOnly = 0x02,
			ReadWrite = 0x04,
			WriteCopy = 0x08,
			GuardModifierflag = 0x100,
			NoCacheModifierflag = 0x200,
			WriteCombineModifierflag = 0x400
		}

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
							uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

		public static IntPtr FindDMAAddy(IntPtr hProc, IntPtr ptr, int[] offsets)
		{
			var buffer = new byte[IntPtr.Size];

			foreach (int i in offsets)
			{
				ReadProcessMemory(hProc, ptr, buffer, buffer.Length, out
				var read);
				ptr = (IntPtr.Size == 4) ? IntPtr.Add(new IntPtr(BitConverter.ToInt32(buffer, 0)), i) : ptr = IntPtr.Add(new IntPtr(BitConverter.ToInt64(buffer, 0)), i);
			}
			return ptr;
		}
		
		private static Process[] procs;
		public static void FindProgram()
        {
            procs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension("v2game.exe"));

            if (procs.Length == 0)
            {
                MessageBox.Show("Cannot find the game!");
                return;
            } else if (procs.Length > 1)
            {
                MessageBox.Show("More than one instance of Victoria 2 detected. This will inject the DLLs into multiple instances of the game.");
            } else
            {
				MessageBox.Show("Game found successfully!");
            }
        }
		public static void Inject(string mod)
        {
            for (int i = 0; i < procs.Length; i++)
            {
                if (procs[i].Handle != IntPtr.Zero)
                {
                    //proc.Handle = managed
                    IntPtr loc = VirtualAllocEx(procs[i].Handle, IntPtr.Zero, MAX_PATH, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ReadWrite);

                    if (loc.Equals(0))
                    {
						MessageBox.Show("Error with injecting!");
                        return;
                    }

					IntPtr bytesRead = IntPtr.Zero;
					bool result = WriteProcessMemory(procs[i].Handle, loc, mod.ToCharArray(), mod.Length, out bytesRead);

					if (!result || bytesRead.Equals(0))
					{
						MessageBox.Show("Error with injecting!");
						return;
					}

					IntPtr loadlibAddy = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

					IntPtr hThread = CreateRemoteThread(procs[i].Handle, IntPtr.Zero, 0, loadlibAddy, loc, 0, out _);
				}
				procs[i].Dispose();
			}
        }
	}
}
