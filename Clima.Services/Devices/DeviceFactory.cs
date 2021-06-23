using System;
using Clima.Core.Alarm;
using Clima.Services.Configuration;
using Clima.Services.Devices.Configs;
using Clima.Services.IO;

namespace Clima.Services.Devices
{
    public class DeviceFactory:IDeviceFactory
    {
        private readonly IIOService _io;
        private readonly IAlarmManager _alarmManager;
        private readonly IConfigurationStorage _configStorage;
        private readonly DeviceFactoryConfig _config;
        
        public DeviceFactory(IConfigurationStorage configStorage, IIOService io,IAlarmManager _alarmManager)
        {
            _configStorage = configStorage;
            if (!_configStorage.Exist("DeviceFactory"))
            {
                var cfg = new DeviceFactoryConfig();
                cfg.FcConfig.Add(new FCConfig());
                cfg.FcConfig[0].EnablePinName = "DO:0";
                cfg.FcConfig[0].AlarmPinName = "DI:0";
                cfg.FcConfig[0].AnalogPinName = "AO:0";
                cfg.FcConfig[0].FCName = "FC:0";
                cfg.FcConfig[0].StartUpTime = 1000;
                
                _configStorage.RegisterConfig("DeviceFactory", cfg);
            }

            _io = io;
            this._alarmManager = _alarmManager;
            _config = _configStorage.GetConfig<DeviceFactoryConfig>("DeviceFactory");
        }

        public FrequencyConverter CreateFrequencyConverter(int number)
        {
            
            FCConfig fcCfg = _config.FcConfig[number];
            
            var fc = new FrequencyConverter();
            
            //Set fc pins
            fc.EnablePin = _io.DiscreteOutputs[fcCfg.EnablePinName];
            fc.AlarmPin = _io.DiscreteInputs[fcCfg.AlarmPinName];
            fc.AnalogPin = _io.AnalogOutputs[fcCfg.AnalogPinName];
            fc.StartUpTime = fcCfg.StartUpTime;
            //Register fc alarm in alarm manager
            _alarmManager.AddNotifier(fc);
            
            fc.InitDevice();
            
            return fc;
        }

        public DiscreteFanController CreateFanController()
        {
            
            throw new NotImplementedException();
        }

        public ServoController CreateServoController(int number)
        {
            throw new NotImplementedException();
        }

        public HeaterController CreateHeaterController(int number)
        {
            throw new NotImplementedException();
        }
    }
}