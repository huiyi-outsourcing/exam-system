using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace ExamSystem.utils {
    public class AuthHelper {
        private AuthHelper() {
        }

        public static String getMachineID() {
            String mid = "";
            String info = "talent" + getBIOSerialNumber() + getMotherBoardSerialNumber();

            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] source = System.Text.Encoding.UTF8.GetBytes(info);
            byte[] target = md5provider.ComputeHash(source);

            mid = Convert.ToBase64String(target).Substring(0, 20);
            return mid;
        }

        public static String generateAuthrizationCode(String id) {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] source = System.Text.Encoding.UTF8.GetBytes(id);
            byte[] target = provider.ComputeHash(source);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < target.Length; ++i) {
                sb.Append(target[i].ToString("x2"));
            }

            return sb.ToString().Substring(0, 8);
        }

        public static String getBIOSerialNumber() {
            string bios = string.Empty;

            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            ManagementObjectCollection moc = mos.Get();

            foreach (ManagementObject mo in moc) {
                bios = mo["SerialNumber"].ToString();
            }

            return bios;
        }

        public static String getMotherBoardSerialNumber() {
            string motherBoard = string.Empty;

            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection moc = mos.Get();

            foreach (ManagementObject mo in moc) {
                motherBoard = mo["SerialNumber"].ToString();
            }

            return motherBoard;
        }

        public static Boolean authorize(String id, String authcode) {
            String rightcode = generateAuthrizationCode(id);

            return authcode.Equals(rightcode);
        }

        private static String getMacInfo() {
            string mac = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            try {
                foreach (ManagementObject mo in moc) {
                    if ((bool)mo["IPEnabled"] == true) {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }

                return mac;
            } catch {
                return "unknown";
            } finally {
                moc = null;
                mc = null;
            }
        }

        private static string getDiskID() {
            string diskid = "";
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();

            try {
                foreach (ManagementObject mo in moc) {
                    diskid = mo.Properties["Model"].Value.ToString();
                }

                return diskid;
            } catch {
                return "unknown";
            } finally {
                moc = null;
                mc = null;
            }
        }

        private static String getCpuId() {
            string cpuInfo = "";
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();

            try {
                foreach (ManagementObject mo in moc) {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }

                return cpuInfo;
            } catch {
                return "unknown";
            } finally {
                moc = null;
                mc = null;
            }
        }
    }
}
