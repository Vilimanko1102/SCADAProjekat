using Common;
using Modbus.FunctionParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.ModbusFunctions
{
	public static class ParseResponseHelper
	{
		public static Dictionary<Tuple<PointType, ushort>, ushort> WriteModbusFunctionParseResponse(byte[] response, PointType pointType)
		{
			Dictionary<Tuple<PointType, ushort>, ushort> setToReturn = new Dictionary<Tuple<PointType, ushort>, ushort>();

			short networkByteDispositionAddress = (short)BitConverter.ToUInt16(response, 8);
			short networkByteDispositionValue = (short)BitConverter.ToUInt16(response, 10);

			ushort usableOutputAddress = (ushort)IPAddress.NetworkToHostOrder(networkByteDispositionAddress);
			ushort usableValue = (ushort)IPAddress.NetworkToHostOrder(networkByteDispositionValue);

			setToReturn.Add(new Tuple<PointType, ushort>(pointType, usableOutputAddress), usableValue);

			return setToReturn;
		}

		public static Dictionary<Tuple<PointType, ushort>, ushort> AnalogReadModbusFunctionParseResponse(byte[] response, PointType pointType, ModbusCommandParameters commandParameters)
		{
			Dictionary<Tuple<PointType, ushort>, ushort> setToReturn = new Dictionary<Tuple<PointType, ushort>, ushort>();

			ModbusReadCommandParameters modbusReadCommandParameters = (ModbusReadCommandParameters)commandParameters;

			ushort startAddress = modbusReadCommandParameters.StartAddress;

			for (int i = 0; i < response[8]; i += 2)
			{
				short networkByteDispositionValue = (short)BitConverter.ToUInt16(response, 9 + i);
				ushort usableValue = (ushort)IPAddress.NetworkToHostOrder(networkByteDispositionValue);
				setToReturn.Add(new Tuple<PointType, ushort>(pointType, startAddress), usableValue);
				startAddress++;
			}

			return setToReturn;
		}

		public static Dictionary<Tuple<PointType, ushort>, ushort> DigitalReadModbusFunctionParseResponse(byte[] response, PointType pointType, ModbusCommandParameters commandParameters)
		{
			Dictionary<Tuple<PointType, ushort>, ushort> setToReturn = new Dictionary<Tuple<PointType, ushort>, ushort>();

			ModbusReadCommandParameters modbusReadCommandParameters = (ModbusReadCommandParameters)commandParameters;

			ushort startAddress = modbusReadCommandParameters.StartAddress;
			ushort quantityChecker = 0;

			for (int i = 0; i < response[8]; i++)
			{
				byte temp = response[9 + i];
				byte utilBitMask = 1;

				for (int j = 0; j < 8; j++)
				{
					setToReturn.Add(new Tuple<PointType, ushort>(pointType, startAddress), (ushort)(temp & utilBitMask));
					startAddress++;

					temp >>= 1;
					quantityChecker++;

					if (quantityChecker >= modbusReadCommandParameters.Quantity)
					{
						break;
					}
						
				}
			}

			return setToReturn;
		}
	}
}
