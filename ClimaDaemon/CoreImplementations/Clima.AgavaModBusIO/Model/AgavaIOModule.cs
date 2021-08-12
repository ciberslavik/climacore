using System;
using Clima.Basics.Services;
using Clima.Core;
using Clima.Core.IO;
using NModbus;

namespace Clima.AgavaModBusIO.Model
{
    public class AgavaIOModule
    {
        private byte _moduleID;
        private IOPinCollection _pins;

        private AgavaIOModule(byte moduleId)
        {
            _moduleID = moduleId;
            _pins = new IOPinCollection();
            _pins.AnalogOutputChanged += OnAnalogOutputChanged;
            _pins.DiscreteOutputChanged += OnDiscreteOutputChanged;
        }

        private void OnDiscreteOutputChanged(DiscretePinStateChangedEventArgs ea)
        {
            Console.WriteLine($"Discr out in module:{_moduleID} pin:{ea.Pin.PinName} to:{ea.NewState}");
        }

        public event AnalogPinValueChangedEventHandler AnalogOutputChanged;

        #region Create module functions

        public static AgavaIOModule CreateModule(byte moduleId, ushort[] signature)
        {
            var mDiCount = 0;
            var mDoCount = 0;
            var mAiCount = 0;
            var mAoCount = 0;
            var module = new AgavaIOModule(moduleId);

            for (var subNumber = 0; subNumber < signature.Length; subNumber++)
            {
                var subType = (AgavaSubModuleType) signature[subNumber];

                switch (subType)
                {
                    case AgavaSubModuleType.None:
                        continue;
                    case AgavaSubModuleType.DO:
                        for (var p = 0; p < 4; p++)
                        {
                            module.CreateDiscrOut(mDoCount);
                            mDoCount++;
                        }

                        break;
                    case AgavaSubModuleType.SYM:
                        for (var p = 0; p < 2; p++)
                        {
                            module.CreateDiscrOut(mDoCount);
                            mDoCount++;
                        }

                        break;
                    case AgavaSubModuleType.R:
                        for (var p = 0; p < 2; p++)
                        {
                            module.CreateDiscrOut(mDoCount);
                            mDoCount++;
                        }

                        break;
                    case AgavaSubModuleType.AI:
                        for (var p = 0; p < 4; p++)
                        {
                            module.CreateAnalogIn(mAiCount);
                            mAiCount++;
                        }

                        break;
                    case AgavaSubModuleType.AIO:
                        for (var p = 0; p < 2; p++)
                        {
                            module.CreateAnalogIn(mAiCount);
                            mAiCount++;
                        }

                        for (var p = 0; p < 2; p++)
                        {
                            module.CreateAnalogOut(mAoCount);
                            mAoCount++;
                        }

                        break;
                    case AgavaSubModuleType.DI:
                        for (var p = 0; p < 4; p++)
                        {
                            module.CreateDiscrIn(mDiCount);
                            mDiCount++;
                        }

                        break;
                    case AgavaSubModuleType.TMP:
                        for (var p = 0; p < 2; p++)
                        {
                            module.CreateAnalogIn(mAiCount);
                            mAiCount++;
                        }

                        break;
                    case AgavaSubModuleType.DO6:
                        for (var p = 0; p < 6; p++)
                        {
                            module.CreateDiscrOut(mDoCount);
                            mDoCount++;
                        }

                        break;
                    case AgavaSubModuleType.ENI:
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return module;
        }


        private void CreateDiscrIn(in int mDiCount)
        {
            var pinName = $"DI:{_moduleID}:{mDiCount}";
            var pin = new AgavaDInput(_moduleID, mDiCount);
            pin.PinName = pinName;
            _pins.AddDiscreteInput(pinName, pin);
        }

        private void CreateAnalogOut(in int mAoCount)
        {
            var pinName = $"AO:{_moduleID}:{mAoCount}";
            var pin = new AgavaAOutput(_moduleID, mAoCount);

            pin.PinName = pinName;
            _pins.AddAnalogOutput(pinName, pin);
        }

        private void CreateAnalogIn(in int mAiCount)
        {
            var pinName = $"AI:{_moduleID}:{mAiCount}";
            var pin = new AgavaAInput(_moduleID, mAiCount);

            pin.PinName = pinName;
            _pins.AddAnalogInput(pinName, pin);
        }

        private void CreateDiscrOut(in int mDoCount)
        {
            var pinName = $"DO:{_moduleID}:{mDoCount}";
            var pin = new AgavaDOutput(_moduleID, mDoCount);
            pin.PinName = pinName;
            _pins.AddDiscreteOutput(pinName, pin);
        }

        #endregion Create module functions

        public byte ModuleId => _moduleID;

        public bool IsDiscreteModified => _pins.IsDiscreteModified;
        public bool IsAnalogModified => _pins.IsAnalogModified;
        public IOPinCollection Pins => _pins;

        public void AcceptModify()
        {
            _pins.AcceptDiscrete();
            _pins.AcceptAnalog();
        }

        public IPin GetPinByName(string pinName)
        {
            if (pinName.Contains("DO"))
                if (_pins.DiscreteOutputs.ContainsKey(pinName))
                    return _pins.DiscreteOutputs[pinName];
            if (pinName.Contains("DI"))
                if (_pins.DiscreteInputs.ContainsKey(pinName))
                    return _pins.DiscreteInputs[pinName];
            if (pinName.Contains("AI"))
                if (_pins.AnalogInputs.ContainsKey(pinName))
                    return _pins.AnalogInputs[pinName];
            if (pinName.Contains("AO"))
                if (_pins.AnalogOutputs.ContainsKey(pinName))
                    return _pins.AnalogOutputs[pinName];

            return null;
        }

        public void SetDIRawData(ushort[] data)
        {
            for (var i = 0; i < data.Length; i++)
            for (var j = 0; j < sizeof(ushort) * 8; j++)
            {
                var pinName = $"DI:{_moduleID}:{j + i * 16}";
                if (GetPinByName(pinName) is AgavaDInput pin)
                {
                    if ((data[i] & (1 << j)) > 0)
                        pin.SetState(true);
                    else
                        pin.SetState(false);
                }
                else
                {
                    return;
                }
            }
        }

        public ushort[] GetDORawData()
        {
            var regCount = _pins.DiscreteOutputs.Count / 16 + 1;
            var result = new ushort[regCount];
            var regBuffer = new bool[_pins.DiscreteOutputs.Count];
            for (var pinIndex = 0; pinIndex < _pins.DiscreteOutputs.Count; pinIndex++)
                regBuffer[pinIndex] = _pins.DiscreteOutputs[$"DO:{_moduleID}:{pinIndex}"].State;


            return ConvertBoolArrayToUshortArray(regBuffer);
        }

        private static ushort BoolArrayToUshort(bool[] source, int offset)
        {
            ushort result = 0;
            // This assumes the array never contains more than 8 elements!

            for (var i = 0; i < source.Length - offset; i++)
                if (source[i + offset])
                    result |= (ushort) (1 << i);

            return result;
        }

        private static ushort[] ConvertBoolArrayToUshortArray(bool[] boolArr)
        {
            var byteArr = new ushort[(boolArr.Length + 15) / 16];
            for (var i = 0; i < byteArr.Length; i++) byteArr[i] = BoolArrayToUshort(boolArr, i * 16);
            return byteArr;
        }

        protected virtual void OnAnalogOutputChanged(AnalogPinValueChangedEventArgs ea)
        {
            AnalogOutputChanged?.Invoke(ea);
        }
    }
}