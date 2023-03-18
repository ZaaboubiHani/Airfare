using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net.NetworkInformation;

namespace Airfare.Functions
{
    static public class DeviceManagement
    {
        static public string GetMacAddress()
        {
            string macAddresses = string.Empty;


            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddresses += nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return macAddresses;
        }



        public static string GetProcessorId()
        {
            string ProcessorId = string.Empty;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_Processor");
                foreach (ManagementObject mo in searcher.Get())
                {
                    if (mo["ProcessorId"] != null)
                    {
                        ProcessorId = mo["ProcessorId"].ToString();
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ProcessorId;
        }

        public static string GetSerialNumber()
        {
            string SerialNumber = string.Empty;
            try
            {
                ManagementObjectSearcher baseboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_MotherboardDevice");

                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    if (queryObj["SerialNumber"] != null)
                    {
                        SerialNumber = queryObj["SerialNumber"].ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return SerialNumber;
        }
    }
}
