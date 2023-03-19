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
        public async Task UploadDevice(ControlDeviceModel controlDevice)
        {
            await Task.Run(async () =>
            {
                try
                {
                    FireBaseDataContext firebase = new();
                    CollectionReference devicesCollection = firebase.Database.Collection("devices");
                    Dictionary<string, object> deviceDictionary = new()
                    {
                        { "deviceId",controlDevice.DeviceId},
                        { "expirationDate",controlDevice.ExpirationDate},
                        { "machineName",controlDevice.MachineName},
                    };
                    var questionDocument = await devicesCollection.AddAsync(deviceDictionary);
                    
                }
                catch (Exception e)
                {
                    Growl.Error(e.Message);
                }
            });
        }

        public async Task<List<ControlDeviceModel>> GetAllDevices()
        {
            List<ControlDeviceModel> devices = new();
            await Task.Run(async () =>
            {
                try
                {
                    FireBaseDataContext firebase = new();

                    CollectionReference devicesCollection = firebase.Database.Collection("devices");

                    QuerySnapshot devicesSnapshot = await devicesCollection.GetSnapshotAsync();

                    foreach (DocumentSnapshot deviceDocument in devicesSnapshot.Documents)
                    {
                        devices.Add(new()
                        {
                            Id = deviceDocument.Id,
                            DeviceId = deviceDocument.GetValue<string>("deviceId"),
                            MachineName = deviceDocument.GetValue<string>("machineName"),
                            ExpirationDate = deviceDocument.GetValue<DateTime>("expirationDate")
                        });
                    }

                }
                catch (Exception e)
                {
                    Growl.Error("انقطع الاتصال بالإنترنت");
                }
            });
            return devices;
        }
    }
}
