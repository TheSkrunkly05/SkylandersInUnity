using PInvoke;
using System;
using System.Runtime.InteropServices;

public class PortalConnection
    {
        private IntPtr devicePtr;
        private HidDevice device;
        ushort id = 0;

        public PortalConnection()
        {
            devicePtr = HidApi.hid_open(0x1430, 0x0150, null);
            if (devicePtr == IntPtr.Zero)
            {
                throw new Exception("No portal found");
            }

            device = Marshal.PtrToStructure<HidDevice>(devicePtr);
        }

        public void Write(byte[] input)
        {
            IntPtr inputPtr = Marshal.AllocHGlobal(input.Length);
            Marshal.Copy(input, 0, inputPtr, input.Length);

            Kernel32.DeviceIoControl(new Kernel32.SafeObjectHandle(device.device_handle, false), 721301, inputPtr, 0x21, IntPtr.Zero, 0, out int returned, new IntPtr());
        }

        public byte[] Read()
        {
            byte[] output = new byte[0x20];
            HidApi.hid_read(devicePtr, output, 0x20);
            return output;
        }

        public void Close()
        {
            HidApi.hid_close(devicePtr);
        }
    }
