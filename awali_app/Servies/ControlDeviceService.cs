using Airfare.DataContext;
using Airfare.Models;
using Google.Cloud.Firestore;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class ControlDeviceService
    {
        public async Task<string> UploadDevice(ControlDeviceModel controlDevice)
        {
            try
            {
                FireBaseDataContext firebase = new(); // get the singleton instance or use DI
                CollectionReference devicesCollection = firebase.Database.Collection("devices");
                Dictionary<string, object> deviceDictionary = new()
                {
                    { "deviceId",controlDevice.DeviceId},
                    { "expirationDate",controlDevice.ExpirationDate},
                    { "machineName",controlDevice.MachineName},
                };
                var docRef = await devicesCollection.AddAsync(deviceDictionary);
                return docRef.Id;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Growl.Error(e.Message);
                return null;
            }
        }

        public async Task<List<ControlDeviceModel>> GetAllDevices()
        {
            try
            {
                FireBaseDataContext firebase = new(); // get the singleton instance or use DI
                CollectionReference devicesCollection = firebase.Database.Collection("devices");
                QuerySnapshot devicesSnapshot = await devicesCollection.GetSnapshotAsync();

                List<ControlDeviceModel> devices = devicesSnapshot.Documents.Select(deviceDocument => new ControlDeviceModel
                {
                    Id = deviceDocument.Id,
                    DeviceId = deviceDocument.GetValue<string>("deviceId"),
                    MachineName = deviceDocument.GetValue<string>("machineName"),
                    ExpirationDate = deviceDocument.GetValue<DateTime>("expirationDate")
                }).ToList();

                return devices;
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Growl.Error("انقطع الاتصال بالإنترنت");
                return new List<ControlDeviceModel>();
            }
        }

        public async Task<ControlDeviceModel> GetDeviceById(string deviceId)
        {
            try
            {
                FireBaseDataContext firebase = new(); // get the singleton instance or use DI
                CollectionReference devicesCollection = firebase.Database.Collection("devices");
                DocumentSnapshot deviceDocument = await devicesCollection.Document(deviceId).GetSnapshotAsync();

                if (deviceDocument.Exists)
                {
                    ControlDeviceModel device = new ControlDeviceModel
                    {
                        Id = deviceDocument.Id,
                        DeviceId = deviceDocument.GetValue<string>("deviceId"),
                        MachineName = deviceDocument.GetValue<string>("machineName"),
                        ExpirationDate = deviceDocument.GetValue<DateTime>("expirationDate")
                    };
                    return device;
                }
                else
                {
                    // Device not found
                    return null;
                }
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message, this);
                Growl.Error("انقطع الاتصال بالإنترنت");
                return null;
            }
        }



    }
}
